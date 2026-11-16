namespace StoreAutomatization.Repositories;

using StoreAutomatization.Models;

public interface IStoreRepository
{
    List<Category> GetAllCategories();
    Category? GetCategoryById(int id);
    void AddCategory(Category category);

    List<Product> GetAllProducts();
    Product? GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);

    List<Receipt> GetAllReceipts();
    Receipt? GetReceiptById(int id);
    void AddReceipt(Receipt receipt);
    void UpdateReceipt(Receipt receipt);
}
