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

    List<Customer> GetAllCustomers();
    Customer? GetCustomerById(int id);
    void AddCustomer(Customer customer);
    void UpdateCustomer(Customer customer);

    List<Shop> GetAllShops();
    Shop? GetShopById(int id);
    void AddShop(Shop shop);
    void UpdateShop(Shop shop);
}
