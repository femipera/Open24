namespace Open24.Domain.Entities;

public class Order : Base
{
    public Guid CustomerId { get; set; }
    public User? Customer { get; set; }
    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "Pending";
    public Payment? Payment { get; set; }
}