using Open24.Domain.Entities;
using Open24.Domain.Interfaces;
using Open24.Infra.Data.Context;

namespace Open24.Infra.Data.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(Open24DbContext context) : base(context) { }
}