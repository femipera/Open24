using Open24.Domain.Entities;
using Open24.Domain.Interfaces;
using Open24.Infra.Data.Context;

namespace Open24.Infra.Data.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(Open24DbContext context) : base(context) { }
}

