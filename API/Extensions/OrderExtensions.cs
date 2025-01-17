using API.DTOs;
using API.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class OrderExtensions
{
    public static IQueryable<OrderDto> ProjectOrderToOrderDto(this IQueryable<Order> query)
    {
        return query
            .Select(order => new OrderDto
            {
                Id = order.Id,
                BuyerId = order.BuyerId,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                DeliveryFee = order.DeliveryFee,
                Subtotal = order.Subtotal,
                OrderStatus = order.OrderStatus.ToString(),
                Total = order.GetTotal(),
                OrderedItems = order.OrderItems.Select(item => new OrderedItemDto
                {
                    ProductId = item.OrderedItem.ProductId,
                    Name = item.OrderedItem.Name,
                    PictureUrl = item.OrderedItem.PictureUrl,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            }).AsNoTracking();
    }
}