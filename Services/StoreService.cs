namespace StoreAutomatization.Services;

using StoreAutomatization.Infrastructure.Logging;
using StoreAutomatization.Infrastructure.Security;
using StoreAutomatization.Models;
using StoreAutomatization.Repositories;
using System.Threading.Channels;

public class StoreService
{
    private readonly StoreRepository _repo;

    public StoreService(StoreRepository repo) { _repo = repo; }

    public void PrintFullProductInfo()
    {
        var products = _repo.GetAllProducts();
        foreach (var product in products)
        {
            var category = _repo.GetCategoryById(product.category_id);
            string categoryName = category != null ? category.name : "Невідома категорія";
            Logger.LogInfo("[ID: {ProductId}] {ProductName} ({CategoryName}) - Ціна: {Price} грн | Залишок: {AvailableCount} шт.", 
                product.id, product.name, categoryName, product.price, product.count);
            if (product.info != null)
                Logger.LogInfo("    -> Склад: {Compositions} | Калорійність: {Calories} ккал | Зберігання: {StorageConditions}", 
                    product.info.compositions, product.info.calories, product.info.storage_conditions);
        }
    }

    public void PrintAllReceiptsDetails()
    {
        var receipts = _repo.GetAllReceipts();
        foreach (var receipt in receipts)
        {
            string statusStr = receipt.state == ReceiptState.Open ? "Відкритий" : receipt.state == ReceiptState.Paid ? "Оплачений" : "Скасований";
            Logger.LogInfo("Чек #{ReceiptId} | Магазин: {ShopName} | Покупець: {CustomerName}", 
                receipt.id, receipt.shop_name, receipt.customer_name);
            Logger.LogInfo("Час: {ReceiptTime} | Статус: {Status} | Загальна сума: {Total} грн", 
                receipt.time, statusStr, receipt.GetTotal());
            Logger.LogInfo("Позиції в чеку:");
            
            if(receipt.items.Count == 0)
            {
                Logger.LogInfo("Чек #{ReceiptId} порожній.", receipt.id);
                continue;
            }

            foreach (var item in receipt.items)
            {
                var product = _repo.GetProductById(item.product_id);
                string productName = product != null ? product.name : "Невідомий товар";

                Logger.LogInfo("  - {ProductName}: {Count} шт. x {Price} грн = {Subtotal} грн",
                    productName, item.count, item.price, item.count * item.price);
            }
            Logger.LogInfo(new string('-', 30));
        }
    }

    public void ScanProductToReceipt(int receiptId, int productId, int countToBuy)
    {
        if (SecurityManager.CurrentRole != UserRole.Cashier)
        {
            Logger.LogError("Відмовлено в доступі: Сканувати товари може лише Касир.");
            return;
        }

        var receipt = _repo.GetReceiptById(receiptId);
        var product = _repo.GetProductById(productId);

        if(receipt == null)
        {
            Logger.LogError("Чек #{ReceiptId} не знайдено.", receiptId);
            return;
        }
        if (product == null)
        {
            Logger.LogError("Товар #{ProductId} не знайдено.", productId);
            return;
        }
        if(product.count < countToBuy)
        {
            Logger.LogError("Відмова: Недостатньо товару '{ProductName}'. В наявності: {AvailableCount}, Замовлено: {CountToBuy}.",
                product.name, product.count, countToBuy);
            return;
        }

        try
        {
            receipt.AddItem(productId, countToBuy, product.price);
            product.count -= countToBuy;

            _repo.UpdateProduct(product);
            _repo.UpdateReceipt(receipt);
            Logger.LogInfo("Товар '{ProductName}' ({CountToBuy} шт. x {Price} грн) додано до чека #{ReceiptId}.",
                product.name, countToBuy, product.price, receiptId);
        }
        catch (Exception ex)
        {
            Logger.LogError("Помилка додавання товару до чека #{ReceiptId}: {Message}", receiptId, ex.Message);
        }
    }

    public int OpenNewReceipt(string customerName, string shopName)
    {
        if (SecurityManager.CurrentRole != UserRole.Cashier)
        {
            Logger.LogError("Відмовлено в доступі: створювати чеки може лише Касир.");
            return -1;
        }

        int newId = _repo.GetAllReceipts().Count > 0 ? _repo.GetAllReceipts().Max(r => r.id) + 1 : 1;
        
        var receipt = new Receipt
        {
            id = newId,
            time = DateTime.Now,
            customer_name = customerName,
            shop_name = shopName
        };

        _repo.AddReceipt(receipt);
        Logger.LogInfo("Відкрито новий чек #{ReceiptId} для покупця {CustomerName}.", newId, customerName);

        return newId;
    }

    public decimal CloseReceipt(int receiptId, decimal cashTendered)
    {
        if (SecurityManager.CurrentRole != UserRole.Cashier)
        {
            Logger.LogError("Відмовлено в доступі: Закривати чеки може лише Касир.");
            return -1;
        }

        var receipt = _repo.GetReceiptById(receiptId);
        if(receipt == null)
        {
            Logger.LogError("Чек #{ReceiptId} не знайдено.", receiptId);
            return -1;
        }

        decimal total = receipt.GetTotal();
        if(cashTendered < total)
        {
            Logger.LogError("Недостатньо коштів для оплати чека #{ReceiptId}. До сплати: {Total} грн, надано: {Cash} грн.",
                receiptId, total, cashTendered);
            return -1;
        }
        try
        {
            receipt.Pay();
            _repo.UpdateReceipt(receipt);
            decimal change = cashTendered - total;
            Logger.LogInfo("Чек #{ReceiptId} оплачено. До сплати: {Total} грн | Внесено: {Cash} грн | Решта: {Change} грн.",
                receiptId, total, cashTendered, change);
            return change;
        }
        catch (Exception ex)
        {
            Logger.LogError("Помилка оплати чека #{ReceiptId}: {Message}", receiptId, ex.Message);
            return -1;
        }
        
    }

    public void CancelReceipt(int receiptId)
    {
        if (SecurityManager.CurrentRole != UserRole.Cashier)
        {
            Logger.LogError("Відмовлено в доступі: скасовувати чеки може лише Касир.");
            return;
        }

        var receipt = _repo.GetReceiptById(receiptId);
        if (receipt == null) return;

        try
        {
            receipt.Cancel();
            foreach (var item in receipt.items)
            {
                var product = _repo.GetProductById(item.product_id);
                if (product != null)
                {
                    product.count += item.count;
                    _repo.UpdateProduct(product);
                    Logger.LogInfo("Повернено на склад: '{ProductName}' (+{Count} шт.). Новий залишок: {NewCount} шт.",
                    product.name, item.count, product.count);
                }
            }
            _repo.UpdateReceipt(receipt);
            Logger.LogInfo("Чек #{ReceiptId} було СКАСОВАНО.", receiptId);
        }
        catch (Exception ex)
        {
            Logger.LogError("Помилка скасування чека #{ReceiptId}: {Message}", receiptId, ex.Message);
        }
    }

    public void AddNewProduct(string name, int categoryId, int count, decimal price, string compositions, int calories, string storageConditions)
    {
        if(SecurityManager.CurrentRole != UserRole.Admin)
        {
            Logger.LogError("Відмовлено в доступі: Додавати нові товари може лише Адміністратор.");
            return;
        }

        int newId = _repo.GetAllProducts().Count > 0 ? _repo.GetAllProducts().Max(p => p.id) + 1 : 1;
        
        var product = new Product
        {
            id = newId,
            name = name,
            category_id = categoryId,
            count = count,
            price = price
        };

        product.SetInfo(compositions, calories, storageConditions);
        _repo.AddProduct(product);
        Logger.LogInfo("Адміністратор додав новий товар: '{ProductName}' (ID: {ProductId}, Ціна: {Price} грн) у кількості {Count} шт.",
            name, newId, price, count);
    }
    public void PrintProductStock(int productId)
    {
        var product = _repo.GetProductById(productId);
        if (product == null)
        {
            Logger.LogError("Товар #{ProductId} не знайдено.", productId);
            return;
        }

        Logger.LogInfo("Залишок на складі -> [ID: {ProductId}] '{ProductName}': {Count} шт. (Ціна: {Price} грн)",
            product.id, product.name, product.count, product.price);
    }
}
