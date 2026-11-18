namespace StoreAutomatization.Models;

public class ReceiptItem : BaseEntity
{
    public int receipt_id { get; set; }
    public int product_id { get; set; }
    public int count { get; set; }
    public decimal price { get; set; }
}
