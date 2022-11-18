using System.Threading.Tasks;
using Cart.API.Entities;

namespace Cart.API.Repository.Interfaces;

public interface ICartRepository
{
    Task<ShoppingCart> GetCart(string userName);
    Task<ShoppingCart> UpdateCart(ShoppingCart cart);
    Task DeleteCart(string userName);
}