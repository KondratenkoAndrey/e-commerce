using System;
using System.Text.Json;
using System.Threading.Tasks;
using Cart.API.Entities;
using Cart.API.Repository.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Cart.API.Repository;

public class CartRepository : ICartRepository
{
    private readonly IDistributedCache _redisCache;

    public CartRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
    }

    public async Task<ShoppingCart> GetCart(string userName)
    {
        var cart = await _redisCache.GetStringAsync(userName);
        return string.IsNullOrWhiteSpace(cart) ? null : JsonSerializer.Deserialize<ShoppingCart>(cart);
    }

    public async Task<ShoppingCart> UpdateCart(ShoppingCart cart)
    {
        await _redisCache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));
        return await GetCart(cart.UserName);
    }

    public async Task DeleteCart(string userName)
    {
        await _redisCache.RemoveAsync(userName);
    }
}