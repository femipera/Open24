using Open24.Shared.DTOs.Requests;
using Open24.Shared.DTOs.Responses;

namespace Open24.Application.Interfaces;

public interface IProductService
{
    Task<ProductResponse> CreateAsync(ProductRequest entity);
    Task<ProductResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductResponse>> GetAllAsync(int count, int skip, string search);
    Task<ProductResponse> UpdateAsync(ProductRequest productRequest);
    Task DeleteAsync(Guid id);
}