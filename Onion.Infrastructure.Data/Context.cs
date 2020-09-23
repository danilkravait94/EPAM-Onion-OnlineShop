using Onion.Domain.Core;
using Onion.Domain.Core.Roles;
using System;
using System.Collections.Generic;

namespace Onion.Infrastructure.Data
{
    public class Context
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<User> Users { get; set; } = new List<User>();
        static Context()
        {
        }
        public Context(List<Product> products, List<User> users)
        {
            Products = products;
            Users = users;
        }
    }
}
