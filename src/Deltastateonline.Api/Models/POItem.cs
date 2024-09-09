namespace Deltastateonline.Models
{
    public class POItem
    {
    public int OrderId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public POItem(int orderId, string productName, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public override string ToString()
    {
        return $"OrderId: {OrderId}, ProductName: {ProductName}, Quantity: {Quantity}, Price: {Price:C}";
    } 
    }
}

