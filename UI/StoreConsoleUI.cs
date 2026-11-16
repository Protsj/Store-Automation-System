namespace StoreAutomatization.UI;

using StoreAutomatization.Services;
using StoreAutomatization.Infrastructure.Security;
using StoreAutomatization.Infrastructure.Logging;
public class StoreConsoleUI
{
    private readonly IStoreService _service;

    public StoreConsoleUI(IStoreService service) { _service = service; }

    public void RunDemoScenarios()
    {
        Console.WriteLine("\n=== ДЕТАЛЬНИЙ КАТАЛОГ ТОВАРІВ (Доступно всім) ===");
        _service.PrintFullProductInfo();

        Console.WriteLine("\n=== ТЕСТИ БЕЗПЕКИ ТА АВТОРИЗАЦІЇ ===");
        Logger.LogInfo("Спроба входу з невірним паролем...");
        SecurityManager.Authenticate("admin", "wrong_pass");
        Console.WriteLine("");
        Logger.LogInfo("Дії без авторизації (гість намагається додати товар та відкрити чек)...");
        _service.AddNewProduct("Спроба злому", 1, 1, 100.00m, "-", 0, "-");
        _service.OpenNewReceipt("Гість", "АТБ");

        Console.WriteLine("\n=== РОБОТА АДМІНІСТРАТОРА ===");
        SecurityManager.Authenticate("admin", "admin123");
        _service.AddNewProduct("Енергетик Monster", 30, 50, 65.00m, "Вода, таурин, кофеїн", 45, "Від 0°C до +25°C");
        Logger.LogInfo("Перевірка RBAC: Адміністратор намагається відкрити чек як касир...");
        _service.OpenNewReceipt("Тест", "АТБ");
        SecurityManager.Logout();

        Console.WriteLine("\n=== РОБОТА КАСИРА: УСПІШНИЙ ПРОДАЖ ТА ВАЛІДАЦІЇ ===");
        SecurityManager.Authenticate("cashier", "cashier123");

        int activeReceiptId = _service.OpenNewReceipt("Іван", "АТБ");
        if (activeReceiptId != -1)
        {
            Logger.LogInfo("Спроба відсканувати неіснуючий товар та чек...");
            _service.ScanProductToReceipt(activeReceiptId, 999, 1);
            _service.ScanProductToReceipt(999, 1, 1);
            Console.WriteLine("");
            Logger.LogInfo("Спроба купити більше товару, ніж є на складі (Стейк лосося, в наявності 15)...");
            _service.ScanProductToReceipt(activeReceiptId, 10, 50);
            _service.ScanProductToReceipt(activeReceiptId, 1, 3);
            _service.ScanProductToReceipt(activeReceiptId, productId: 28, countToBuy: 2);
            Console.WriteLine("");
            Logger.LogInfo("Спроба оплатити чек недостатньою сумою готівки...");
            _service.CloseReceipt(activeReceiptId, 100.00m);
            decimal change = _service.CloseReceipt(activeReceiptId, 300.00m);
            Console.WriteLine("");
            Logger.LogInfo("Спроба змінити або повторно оплатити вже ОПЛАЧЕНИЙ чек...");
            _service.ScanProductToReceipt(activeReceiptId, 1, 1);
            _service.CloseReceipt(activeReceiptId, 300.00m);
            _service.CancelReceipt(activeReceiptId);
        }

        Console.WriteLine("\n=== РОБОТА КАСИРА: ПОРОЖНІЙ ЧЕК ТА СКАСУВАННЯ З ПОВЕРНЕННЯМ ТОВАРІВ ===");
        int emptyReceiptId = _service.OpenNewReceipt("Олена", "Сільpo");
        Logger.LogInfo("Спроба закрити порожній чек...");
        _service.CloseReceipt(emptyReceiptId, 100.00m);
        _service.CancelReceipt(emptyReceiptId);
        Console.WriteLine("");
        Logger.LogInfo("Спроба взаємодії зі СКАСОВАНИМ чеком...");
        _service.ScanProductToReceipt(emptyReceiptId, 1, 1);
        _service.CloseReceipt(emptyReceiptId, 100.00m);
        _service.CancelReceipt(emptyReceiptId);
        int canceledWithItemsId = _service.OpenNewReceipt("Максим", "Новус");
        _service.ScanProductToReceipt(canceledWithItemsId, 10, 5);
        _service.PrintProductStock(10);
        Console.WriteLine("");
        Logger.LogInfo("Покупець передумав — скасовуємо чек з товарами...");
        _service.CancelReceipt(canceledWithItemsId);
        _service.PrintProductStock(10);

        Console.WriteLine("\n=== ТЕСТ БЕЗПЕКИ: КАСИР НАМАГАЄТЬСЯ ДОДАТИ ТОВАР ===");
        _service.AddNewProduct("Товар від касира", 1, 10, 50.00m, "-", 0, "-");
        SecurityManager.Logout();

        Console.WriteLine("\n=== ПЕРЕГЛЯД РЕЗУЛЬТАТІВ (АДМІНІСТРАТОР) ===");
        SecurityManager.Authenticate("admin", "admin123");
        _service.PrintAllReceiptsDetails();
        SecurityManager.Logout();
    }
}
