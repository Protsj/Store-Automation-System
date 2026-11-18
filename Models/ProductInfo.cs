namespace StoreAutomatization.Models;

public class ProductInfo : BaseEntity
{
    public int product_id { get; set; }
    public string compositions { get; set; } = string.Empty;
    public int calories { get; set; }
	public string storage_conditions { get; set; } = string.Empty;
}
