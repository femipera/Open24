using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Open24.Application.Interfaces;
using Open24.Application.Mappings;
using Open24.Application.Services;
using Open24.Domain.Interfaces;
using Open24.Infra.Data.Context;
using Open24.Infra.Data.Repositories;

namespace Open24.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Open24DbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(Open24DbContext).Assembly.FullName)));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<Open24DbContext>();
        services.AddHttpClient();

        #region Repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        #endregion

        #region Services
        services.AddScoped<IProductService, ProductService>();

        #endregion

        #region Validators

        #endregion

        #region AutoMapper
        services.AddAutoMapper(typeof(ProductProfile));
        #endregion

        return services;
    }
}

