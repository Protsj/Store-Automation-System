namespace StoreAutomatization.Repositories;

using StoreAutomatization.Models;

public class StoreRepository : IStoreRepository
{
	private List<Category> _categories = new List<Category>();
    private List<Product> _products = new List<Product>();
	private List<Receipt> _receipts = new List<Receipt>();
    private readonly List<Customer> _customers = new();
    private readonly List<Shop> _shops = new();

    public List<Category> GetAllCategories() => _categories;
	public List<Product> GetAllProducts() => _products;
	public List<Receipt> GetAllReceipts() => _receipts;
    public List<Customer> GetAllCustomers() => _customers;
    public List<Shop> GetAllShops() => _shops;

    public Category GetCategoryById(int id) { return _categories.FirstOrDefault(c => c.id == id); }
	public Product GetProductById(int id) { return _products.FirstOrDefault(p => p.id == id); }
	public Receipt GetReceiptById(int id) { return _receipts.FirstOrDefault(r => r.id == id); }
    public Customer? GetCustomerById(int id) => _customers.FirstOrDefault(c => c.id == id);
    public Shop? GetShopById(int id) => _shops.FirstOrDefault(s => s.id == id);

    public void AddCategory(Category category) { _categories.Add(category); }
    public void AddProduct(Product product) { _products.Add(product); }
	public void AddReceipt(Receipt receipt) { _receipts.Add(receipt); }
    public void AddCustomer(Customer customer) => _customers.Add(customer);

    public void DeleteCategory(int id) { _categories.RemoveAll(c => c.id == id); }
    public void DeleteProduct(int id) { _products.RemoveAll(p => p.id == id); }
    public void DeleteReceipt(int id) { _receipts.RemoveAll(r => r.id == id); }
    public void AddShop(Shop shop) => _shops.Add(shop);

    public void UpdateCategory(Category category)
    {
        int index = _categories.FindIndex(c => c.id == category.id);
        if (index != -1)
            _categories[index] = category;
    }

	public void UpdateProduct(Product product)
    {
        int index = _products.FindIndex(p => p.id == product.id);
        if (index != -1)
            _products[index] = product;
    }

    public void UpdateReceipt(Receipt receipt)
    {
        int index = _receipts.FindIndex(r => r.id == receipt.id);
        if (index != -1)
            _receipts[index] = receipt;
    }
    public void UpdateCustomer(Customer customer)
    {
        var existing = GetCustomerById(customer.id);
        if (existing != null)
        {
            existing.name = customer.name;
            existing.phone = customer.phone;
        }
    }
    public void UpdateShop(Shop shop)
    {
        var existing = GetShopById(shop.id);
        if (existing != null)
        {
            existing.name = shop.name;
            existing.address = shop.address;
        }
    }
}
