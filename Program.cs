using StoreAutomatization.Data;
using StoreAutomatization.Services;
using StoreAutomatization.Repositories;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Logger.Initialize();

        try
        {
            Logger.LogInfo("Систему автоматизації магазину запущено.");

            StoreRepository repository = new StoreRepository();
            MockData.Initialize(repository);
            StoreService service = new StoreService(repository);

            Console.WriteLine("\n=== ДЕТАЛЬНИЙ КАТАЛОГ ТОВАРІВ (Доступно всім) ===");
            service.PrintFullProductInfo();

            Console.WriteLine("\n=== ТЕСТИ БЕЗПЕКИ ТА АВТОРИЗАЦІЇ ===");
            Logger.LogInfo("Спроба входу з невірним паролем...");
            SecurityManager.Authenticate("admin", "wrong_pass");
            Console.WriteLine("");
            Logger.LogInfo("Дії без авторизації (гість намагається додати товар та відкрити чек)...");
            service.AddNewProduct("Спроба злому", 1, 1, 100.00m, "-", 0, "-");
            service.OpenNewReceipt("Гість", "АТБ");

            Console.WriteLine("\n=== РОБОТА АДМІНІСТРАТОРА ===");
            SecurityManager.Authenticate("admin", "admin123");
            service.AddNewProduct("Енергетик Monster", 30, 50, 65.00m,"Вода, таурин, кофеїн", 45, "Від 0°C до +25°C");
            Logger.LogInfo("Перевірка RBAC: Адміністратор намагається відкрити чек як касир...");
            service.OpenNewReceipt("Тест", "АТБ");
            SecurityManager.Logout();

            Console.WriteLine("\n=== РОБОТА КАСИРА: УСПІШНИЙ ПРОДАЖ ТА ВАЛІДАЦІЇ ===");
            SecurityManager.Authenticate("cashier", "cashier123");

            int activeReceiptId = service.OpenNewReceipt("Іван", "АТБ");
            if (activeReceiptId != -1)
            {
                Logger.LogInfo("Спроба відсканувати неіснуючий товар та чек...");
                service.ScanProductToReceipt(activeReceiptId, 999, 1);
                service.ScanProductToReceipt(999, 1, 1);
                Console.WriteLine("");
                Logger.LogInfo("Спроба купити більше товару, ніж є на складі (Стейк лосося, в наявності 15)...");
                service.ScanProductToReceipt(activeReceiptId, 10, 50);
                service.ScanProductToReceipt(activeReceiptId, 1, 3);
                service.ScanProductToReceipt(activeReceiptId, productId: 28, countToBuy: 2);
                Console.WriteLine("");
                Logger.LogInfo("Спроба оплатити чек недостатньою сумою готівки...");
                service.CloseReceipt(activeReceiptId, 100.00m);
                decimal change = service.CloseReceipt(activeReceiptId, 300.00m);
                Console.WriteLine("");
                Logger.LogInfo("Спроба змінити або повторно оплатити вже ОПЛАЧЕНИЙ чек...");
                service.ScanProductToReceipt(activeReceiptId, 1, 1);
                service.CloseReceipt(activeReceiptId, 300.00m);
                service.CancelReceipt(activeReceiptId);
            }

            Console.WriteLine("\n=== РОБОТА КАСИРА: ПОРОЖНІЙ ЧЕК ТА СКАСУВАННЯ З ПОВЕРНЕННЯМ ТОВАРІВ ===");
            int emptyReceiptId = service.OpenNewReceipt("Олена", "Сільпо");
            Logger.LogInfo("Спроба закрити порожній чек...");
            service.CloseReceipt(emptyReceiptId, 100.00m);
            service.CancelReceipt(emptyReceiptId);
            Console.WriteLine("");
            Logger.LogInfo("Спроба взаємодії зі СКАСОВАНИМ чеком...");
            service.ScanProductToReceipt(emptyReceiptId, 1, 1);
            service.CloseReceipt(emptyReceiptId, 100.00m);
            service.CancelReceipt(emptyReceiptId);
            int canceledWithItemsId = service.OpenNewReceipt("Максим", "Новус");
            service.ScanProductToReceipt(canceledWithItemsId, 10, 5);
            service.PrintProductStock(10);
            Console.WriteLine("");
            Logger.LogInfo("Покупець передумав — скасовуємо чек з товарами...");
            service.CancelReceipt(canceledWithItemsId);
            service.PrintProductStock(10);

            Console.WriteLine("\n=== ТЕСТ БЕЗПЕКИ: КАСИР НАМАГАЄТЬСЯ ДОДАТИ ТОВАР ===");
            service.AddNewProduct("Товар від касира", 1, 10, 50.00m, "-", 0, "-");
            SecurityManager.Logout();

            Console.WriteLine("\n=== ПЕРЕГЛЯД РЕЗУЛЬТАТІВ (АДМІНІСТРАТОР) ===");
            SecurityManager.Authenticate("admin", "admin123");
            service.PrintAllReceiptsDetails();
            SecurityManager.Logout();
        }
        catch(Exception ex)
        {
            Serilog.Log.Fatal(ex, "Виникла критична помилка під час роботи системи.");
        }
        finally
        {
            Logger.Close();
        }
    }
}