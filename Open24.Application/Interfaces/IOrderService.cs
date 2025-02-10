using Open24.Shared.DTOs.Requests;
using Open24.Shared.DTOs.Responses;

namespace Open24.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CreateAsync(OrderRequest entity);
    Task<OrderResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<OrderResponse>> GetAllAsync(int count, int skip, string search);
    Task<OrderResponse> UpdateAsync(OrderRequest productRequest);
    Task DeleteAsync(Guid id);
}
