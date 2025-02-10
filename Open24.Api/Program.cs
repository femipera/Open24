using Microsoft.AspNetCore.Mvc.Authorization;
using Open24.Api.Configuration;
using Open24.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApiConfig();
builder.Services.AddIdentityConfig();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedConfig.SeedUserAsync(services);
}

app.UseApiConfig(app.Environment);

app.UseAuthentication();  
app.UseAuthorization();   

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();