using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Open24.Domain.Entities;

namespace Open24.Infra.Data.Context;

public class Open24DbContext : IdentityDbContext<IdentityUser>
{
    public Open24DbContext(DbContextOptions<Open24DbContext> options) : base(options) { }

    #region DbSet
    public DbSet<User> User { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    #endregion
}
