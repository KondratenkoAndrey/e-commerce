using Discount.API.Data;
using Discount.API.Repositories;
using Discount.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Discount API", Version = "v1" });
});
builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetValue<string>("DbSettings:ConnectionString"));
    options.UseSnakeCaseNamingConvention();
});
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swaggerUiOptions =>
    {
        swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount API V1");
    });
}

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DiscountContext>();    
    context.Database.Migrate();
}

app.Run();