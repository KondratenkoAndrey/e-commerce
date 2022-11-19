using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Discount.API.Data;
using Discount.API.Entities;
using Discount.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly DbConnection _connection;

    public DiscountRepository(DiscountContext context)
    {
        _connection = context.Database.GetDbConnection();
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        return await _connection.QueryFirstOrDefaultAsync<Coupon>(
            "select * from coupons where product_name=@ProductName",
            new { ProductName = productName });
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        var affected = await _connection.ExecuteAsync(
            "insert into coupons (product_name, description, amount) values (@ProductName, @Description, @Amount)",
            new { coupon.ProductName, coupon.Description, coupon.Amount });

        return affected > 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        var affected = await _connection.ExecuteAsync(
            "update coupons set product_name=@productName, description=@Description, amount=@Amount where id = @Id",
            new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });

        return affected > 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        var affected = await _connection.ExecuteAsync(
            "delete from coupons where product_name=@ProductName",
            new { ProductName = productName });

        return affected > 0;
    }
}