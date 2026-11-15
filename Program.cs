using StoreAutomatization.Infrastructure.Logging;
using StoreAutomatization.Data;
using StoreAutomatization.Services;
using StoreAutomatization.Repositories;
using StoreAutomatization.UI;

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

            StoreConsoleUI storeConsoleUI = new StoreConsoleUI(service);
            storeConsoleUI.RunDemoScenarios();
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