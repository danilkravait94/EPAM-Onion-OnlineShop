using Onion.Domain.Core;
using Onion.Domain.Core.Roles;
using Onion.Domain.Interfaces;
using Onion.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onion.Infrastructure.Business
{
    public class ProductService : IProductRepository
    {
        IWork DB { get; set; }
        public ProductService(IWork db)
        {
            DB = db;
        }
        /// <summary>
        /// Add to basket the product
        /// </summary>
        /// <param name="user">User whose order will be got</param>
        /// <param name="id">The id of product</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void AddToBasket(User user, int? id)
        {
            if (id is null)
            {
                throw new ValidateException("The id cannot be null");
            }
            if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            User u = DB.Users.Get(user.Id);
            if (u is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            Product product = DB.Products.Get(id.Value);
            if (product is null)
            {
                throw new ValidateException("The product cannot be null");
            }
            u.Basket.Add(new Product() { Id = user.Basket.Count + 1, Category = product.Category, Description = product.Description, Name = product.Name, Price = product.Price });
        }

        /// <summary>
        /// Delete a product from basket
        /// </summary>
        /// <param name="user">User whose order will be got</param>
        /// <param name="id">The id of product</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void DeleteFromBasket(User user, int? id)
        {
            if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            if (id is null)
            {
                throw new ValidateException("The id cannot be null");
            }
            User u = DB.Users.Get(user.Id);
            if (u is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            Product product1 = u.Basket.Find(f => f.Id == id);
            if (product1 is null)
            {
                throw new ValidateException("The product cannot be null");
            }
            u.Basket.Remove(product1);
        }

        /// <summary>
        /// Creates a product and adds 
        /// to the list of product
        /// </summary>
        /// <param name="product">The product that will be added</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void CreateProduct(Product product)
        {
            if (product is null)
            {
                throw new ValidateException("The product cannot be null");
            }
            DB.Products.Create(new Product() { Id = DB.Products.GetAll().Count() + 1, Category = product.Category, Description = product.Description, Name = product.Name, Price = product.Price });
        }

        /// <summary>
        /// Delete a product from the list 
        /// </summary>
        /// <param name="product">the product with this pruduct`s id will be deleted</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void DeleteProduct(Product product)
        {
            if (product is null)
            {
                throw new ValidateException("The product cannot be null");
            }
            DB.Products.Delete(product.Id);
        }

        /// <summary>
        /// Delete a product from the list by id
        /// </summary>
        /// <param name="id">the product with this id will be deleted</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void DeleteProduct(int? id)
        {
            if (id is null)
            {
                throw new ValidateException("The id cannot be null");
            }
            DB.Products.Delete(id.Value);

        }

        /// <summary>
        /// Edit product`s name
        /// </summary>
        /// <param name="id">The id of product</param>
        /// <param name="name">New name</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void EditProduct(int? id, string name)
        {
            if (id is null)
            {
                throw new ValidateException("The id cannot be null");
            }
            Product pro = DB.Products.Get(id.Value);
            if (pro is null)
            {
                throw new ValidateException("The product cannot be null");
            }
            pro.Name = name;
        }

        /// <summary>
        /// Edit product`s price
        /// </summary>
        /// <param name="id">The id of product</param>
        /// <param name="price">New price</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void EditProduct(int? id, decimal price)
        {
            if (id is null)
            {
                throw new ValidateException("The id cannot be null");
            }
            Product pro = DB.Products.Get(id.Value);
            if (pro is null)
            {
                throw new ValidateException("The product cannot be null");
            }
            pro.Price = price;
        }

        /// <summary>
        /// Change a product with id to new product
        /// </summary>
        /// <param name="id">The id of product</param>
        /// <param name="newPro">The new product</param>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public void EditProduct(int? id, Product newPro)
        {
            if (id is null)
            {
                throw new ValidateException("The id cannot be null");
            }
            if (newPro is null)
            {
                throw new ValidateException("The product cannot be null");
            }
            Product pro = DB.Products.Get(id.Value);
            pro = newPro;
        }

        /// <summary>
        /// Get a product by id
        /// </summary>
        /// <param name="id">Id of searching product</param>
        /// <returns>the product with the id</returns>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public Product GetProduct(int? id)
        {
            if (id is null)
            {
                throw new ValidateException("The id cannot be null");
            }
            Product pro = DB.Products.Get(id.Value);
            if (pro is null)
            {
                throw new ValidateException("The product cannot be null");
            }
            return pro;
        }

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns>IEnumerable of products</returns>>
        public IEnumerable<Product> GetProducts() => DB.Products.GetAll();

        /// <summary>
        /// Returns the products which are in the user`s basket
        /// </summary>
        /// <param name="user">User whose basket will be got</param>
        /// <returns>IEnumerable of products</returns>
        /// <exception cref="ValidateException">Thrown if parameter was null</exception>
        public IEnumerable<Product> GetBasket(User user)
        {
            if (user is null)
            {
                throw new ValidateException("The user cannot be null");
            }
            User u = DB.Users.Get(user.Id);
            return u.Basket;
        }
    }
}
