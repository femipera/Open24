namespace Open24.Domain.Entities;

public class Product : Base
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>(); 
}
