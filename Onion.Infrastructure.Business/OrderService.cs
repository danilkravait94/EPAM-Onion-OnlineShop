using Onion.Domain.Core;
using Onion.Domain.Core.Enums;
using Onion.Domain.Core.Roles;
using Onion.Domain.Interfaces;
using Onion.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Infrastructure.Business
{
    public class OrderService : IOrderRepository
    {
        IWork DB { get; set; }
        public OrderService(IWork db)
        {
            DB = db;
        }

        /// <summary>
        /// Return the order by user with id
        /// </summary>
        /// <param name="user">User whose order will be got</param>
        /// <param name="id">Id of order</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public Order GetOrder(User user, int? id)
        {
            if (id is null)
            {
                throw new ValidateException("The Id cannot be null");
            }
            if (user is null)
            {
                throw new ValidateException("The user cannot be null");

            }
            Order order = DB.Users.Get(user.Id).Orders.Find(p => p.Id == id);
            if (order is null)
            {
                throw new ValidateException("The order cannot be null");
            }
            return order;

        }

        /// <summary>
        /// Returns all orders by user
        /// </summary>
        /// <param name="user">User whose order will be got</param>
        /// <returns>An IEnumerable of orders</returns>
        /// <exception cref="ValidateException">Thrown if user was null</exception>
        public IEnumerable<Order> GetOrders(User user)
        {
            if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            User u = DB.Users.Get(user.Id);
            return u.Orders;
        }

        /// <summary>
        /// Create an order 
        /// </summary>
        /// <param name="user">User whose order will be got</param>
        /// <param name="order">Orderf that will be added to the order list</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void MakeOrder(User user, Order order)
        {
            Product product = DB.Products.Get(order.ProductId);
            if (product is null)
            {
                throw new ValidateException("The product cannot be null");
            }
            else if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            else
            {
                Order newOrder = new Order()
                {
                    NumberOfPost = order.NumberOfPost,
                    Country = order.Country,
                    ProductId = product.Id,
                    Id = user.Orders.Count + 1,
                    Status = order.Status
                };
                User u = DB.Users.Get(user.Id);
                u.Orders.Add(newOrder);
            }
        }

        /// <summary>
        /// Sets status "Get" to the order
        /// </summary>
        /// <param name="user">User whose order will be got</param>
        /// <param name="id">THe id of order</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void SetGetToOrder(User user, int? id)
        {
            if (id is null)
            {
                throw new ValidateException("The Id cannot be null");
            }
            else if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            Order orderMain = DB.Users.Get(user.Id).Orders.Find(f => f.Id == id);
            if (orderMain is null)
            {
                throw new ValidateException("The order cannot be null");
            }
            orderMain.Status = Statuses.Recieved;
        }

        /// <summary>
        /// Edits the information of order
        /// Changes country
        /// </summary>
        /// <param name="user">User whose order will be got</param>
        /// <param name="order"></param>
        /// <param name="country"></param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void EditOrder(User user, Order order, string country)
        {
            if (order is null)
            {
                throw new ValidateException("The order cannot be null");
            }
            else if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            Order orderMain = DB.Users.Get(user.Id).Orders.Find(f => f.Id == order.Id);
            if (orderMain is null)
            {
                throw new ValidateException("The order cannot be null");
            }
            orderMain.Country = country;
        }

        /// <summary>
        /// Edits the information of order
        /// Changes country
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        /// <param name="number"></param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void EditOrder(User user, Order order, int number)
        {
            if (order is null)
            {
                throw new ValidateException("The order cannot be null");
            }
            else if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            Order orderMain = DB.Users.Get(user.Id).Orders.Find(f => f.Id == order.Id);
            if (orderMain is null)
            {
                throw new ValidateException("The odrer cannot be null");
            }
            orderMain.NumberOfPost = number;
        }

        /// <summary>
        /// Edits the information of order
        /// Changes country
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        /// <param name="newOrder"></param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void EditOrder(User user, Order order, Order newOrder)
        {
            if (order is null)
            {
                throw new ValidateException("The order cannot be null");
            }
            else if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            Order orderMain = DB.Users.Get(user.Id).Orders.Find(f => f.Id == order.Id);
            if (orderMain is null)
            {
                throw new ValidateException("The order cannot be null");
            }
            orderMain = newOrder;
        }

        /// <summary>
        /// Cancel the order
        /// </summary>
        /// <param name="user">User whose order will be got</param>
        /// <param name="id">The id of order</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void CancelOrder(User user, int? id)
        {
            if (id is null)
            {
                throw new ValidateException("The id cannot be null");
            }
            if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            Order order = DB.Users.Get(user.Id).Orders.Find(f => f.Id == id);
            if (order.Status != Statuses.CanceledByAdmin && order.Status != Statuses.Recieved)
            {
                order.Status = Statuses.CanceledByUser;
            }
            else
            {
                throw new ValidateException("The status cannot be changed");
            }
        }

        /// <summary>
        /// Change the status of order
        /// </summary>
        /// <param name="user">User whose order will be got</param>
        /// <param name="id">The id of order</param>
        /// <param name="status">The status the will be set to the order</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void ChangeStatus(User user, int? id, Statuses status)
        {
            if (id is null)
            {
                throw new ValidateException("The id cannot be null");
            }
            if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            Order orderMain = DB.Users.Get(user.Id).Orders.Find(f => f.Id == id);
            if (orderMain is null)
            {
                throw new ValidateException("The order cannot be null");
            }
            orderMain.Status = status;
        }
    }
}
