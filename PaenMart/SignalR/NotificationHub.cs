using Data_Access.DataContext_Class;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace PaenMart.SignalR
{
    public class NotificationHub : Hub
    {
        private readonly DataContext _dataContext;
        public NotificationHub(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public async Task LivePendingOrders()
        {
            var findingPendingOrderCount = await _dataContext.Orders.Where(a => a.OrderStatus == "Pending").CountAsync();
            await Clients.All.SendAsync("PendingLiveOrders", findingPendingOrderCount);
        }

        public async Task LiveShippingPendingOrders()
        {
            var findingPendingOrderCount = await _dataContext.Orders.Where(a => a.OrderStatus == "Shipping pending").CountAsync();
            await Clients.All.SendAsync("ShippingPendingOrdersCountLive", findingPendingOrderCount);
        }
    }
}
