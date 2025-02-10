namespace Open24.Shared.DTOs.Requests;

public class ProductRequest
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
}
