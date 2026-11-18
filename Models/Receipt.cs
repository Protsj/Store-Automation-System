namespace StoreAutomatization.Models;

public class Receipt : BaseEntity
{
	public int customer_id { get; set; }
	public int shop_id { get; set; }
    public ReceiptState state { get; private set; } = ReceiptState.Open;
    public DateTime time { get; set; }
    public List<ReceiptItem> items { get; set; } = new List<ReceiptItem>();
	public Customer? customer { get; set; }
	public Shop? shop { get; set; }

    public void AddItem(int productId, int count, decimal price)
	{
		if (state != ReceiptState.Open)
			throw new InvalidOperationException("Неможливо додати товар: чек вже закритий або скасований.");

		var newItem = new ReceiptItem
		{
			id = items.Count + 1,
			receipt_id = this.id,
			product_id = productId,
			count = count,
			price = price
		};
		items.Add(newItem);
	}

	public decimal GetTotal()
	{
		return items.Sum(item => item.price * item.count);
	}

	public void Pay()
	{
		if (state != ReceiptState.Open)
            throw new InvalidOperationException("Оплатити можна лише відкритий чек.");
		if (items.Count == 0)
            throw new InvalidOperationException("Неможливо оплатити порожній чек (потрібен хоча б 1 товар).");

        state = ReceiptState.Paid;
	}

	public void Cancel()
    {
        if (state != ReceiptState.Open)
            throw new InvalidOperationException("Скасувати можна лише відкритий чек.");
        state = ReceiptState.Canceled;
    }

    public void ClearItems() { items.Clear(); }
}
