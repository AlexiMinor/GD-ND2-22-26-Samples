namespace ConsoleProjectWithDb.Db;

public class OrderItem
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }

    public int? OrderId { get; set; }
    public Order? Order { get; set; }

    public Guid ProductId { get; set; }
    public required Product Product { get; set; }
}