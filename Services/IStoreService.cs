namespace StoreAutomatization.Services;

public interface IStoreService
{
    void PrintFullProductInfo();
    void PrintAllReceiptsDetails();
    void PrintProductStock(int productId);
    int OpenNewReceipt(int customerId, int shopId);
    void ScanProductToReceipt(int receiptId, int productId, int countToBuy);
    decimal CloseReceipt(int receiptId, decimal cashTendered);
    void CancelReceipt(int receiptId);
    void AddNewCategory(string name);
    void AddNewProduct(string name, int categoryId, int count, decimal price, string composition, int calories, string storageConditions);
}
