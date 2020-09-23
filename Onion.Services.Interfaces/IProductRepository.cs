using Onion.Domain.Core;
using Onion.Domain.Core.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Services.Interfaces
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product GetProduct(int? id);
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetBasket(User user);
        void AddToBasket(User user, int? id);
        void DeleteFromBasket(User user, int? id);
        void EditProduct(int? id, string name);
        void EditProduct(int? id, decimal price);
        void EditProduct(int? id, Product newPro);
        void DeleteProduct(Product product);
        void DeleteProduct(int? id);
    }
}
