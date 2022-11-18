using System.Collections.Generic;
using System.Linq;

namespace Cart.API.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; }

    public decimal TotalPrice
    {
        get
        {
            if (Items == null)
            {
                return 0M;
            }
            return Items.Sum(item => item.Price * item.Quantity);
        }
    }

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
}