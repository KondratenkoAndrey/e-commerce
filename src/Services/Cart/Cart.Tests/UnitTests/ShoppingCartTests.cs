using Cart.API.Entities;
using FluentAssertions;

namespace Cart.Tests.UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TotalPriceIsZeroIfEmptyCart()
    {
        var cart = new ShoppingCart("SomeUser");
        cart.TotalPrice.Should().Be(0M);
    }
    
    [Test]
    public void TotalPriceIsCorrect()
    {
        var cart = new ShoppingCart("SomeUser")
        {
            Items = new List<ShoppingCartItem>
            {
                new() { Quantity = 1, Price = 1M },
                new() { Quantity = 2, Price = 2M },
                new() { Quantity = 3, Price = 3.3M }
            }
        };
        cart.TotalPrice.Should().Be(14.9M);
    }
}