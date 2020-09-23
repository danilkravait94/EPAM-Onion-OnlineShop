using Onion.Domain.Core;
using Onion.Domain.Core.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Domain.Interfaces
{
    public interface IWork
    {
        IRepository<Product> Products { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
