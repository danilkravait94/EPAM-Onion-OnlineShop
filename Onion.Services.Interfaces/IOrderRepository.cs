using Onion.Domain.Core;
using Onion.Domain.Core.Enums;
using Onion.Domain.Core.Roles;
using System;
using System.Collections.Generic;

namespace Onion.Services.Interfaces
{
    public interface IOrderRepository
    {
        void MakeOrder(User user, Order order);
        Order GetOrder(User user, int? id);
        IEnumerable<Order> GetOrders(User user);
        void SetGetToOrder(User user, int? id);
        void EditOrder(User user, Order order, string country);
        void EditOrder(User user, Order order, int number);
        void EditOrder(User user, Order order, Order newOrder);
        void CancelOrder(User user, int? id);
        void ChangeStatus(User user, int? id, Statuses status);

    }


}