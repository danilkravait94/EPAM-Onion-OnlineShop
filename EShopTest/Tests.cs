using NUnit.Framework;
using Onion.Domain.Core;
using Onion.Domain.Core.Enums;
using Onion.Domain.Core.Roles;
using Onion.Infrastructure.Business;
using Onion.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace EShopTest
{
    public class Tests
    {
        static OrderService orderService;
        static ProductService productService;
        static UserService userService;
        [SetUp]
        public void Setup()
        {
            List<Product> listofProduct = new List<Product>();
            List<User> listofUsers = new List<User>();
            listofProduct.Add(new Product() { Id = 1, Name = "Apple", Category = "Food", Description = "Eatable", Price = 120 });
            listofProduct.Add(new Product() { Id = 2, Name = "Plun", Category = "Food", Description = "Eatable", Price = 30 });
            listofProduct.Add(new Product() { Id = 3, Name = "Potato", Category = "Food", Description = "Eatable", Price = 15 });
            listofProduct.Add(new Product() { Id = 4, Name = "Carrot", Category = "Food", Description = "Eatable", Price = 20 });
            listofProduct.Add(new Product() { Id = 5, Name = "Cabage", Category = "Food", Description = "Eatable", Price = 40 });
            listofProduct.Add(new Product() { Id = 6, Name = "Grapes", Category = "Food", Description = "Eatable", Price = 63 });
            listofProduct.Add(new Product() { Id = 7, Name = "Cinamon", Category = "Food", Description = "Eatable", Price = 21 });
            listofProduct.Add(new Product() { Id = 8, Name = "Tea", Category = "Drink", Description = "Eatable", Price = 31 });
            listofProduct.Add(new Product() { Id = 9, Name = "Soup", Category = "Food", Description = "Eatable", Price = 150 });
            listofProduct.Add(new Product() { Id = 10, Name = "Coffee", Category = "Drink", Description = "Eatable", Price = 46 });
            listofProduct.Add(new Product() { Id = 11, Name = "Apple", Category = "Food", Description = "Fruit", Price = 120 });
            listofProduct.Add(new Product() { Id = 12, Name = "Plum", Category = "Food", Description = "Fruit", Price = 230 });
            listofProduct.Add(new Product() { Id = 13, Name = "Grapes", Category = "Food", Description = "Fruit", Price = 80 });
            listofProduct.Add(new Product() { Id = 14, Name = "Potato", Category = "Food", Description = "Vegetables", Price = 53 });
            listofProduct.Add(new Product() { Id = 15, Name = "Tomato", Category = "Food", Description = "Fruit", Price = 63 });
            listofProduct.Add(new Product() { Id = 16, Name = "Strawberry", Category = "Food", Description = "Fruit", Price = 54 });
            listofProduct.Add(new Product() { Id = 17, Name = "Cinamon", Category = "Food", Description = "Fruit", Price = 75 });
            listofProduct.Add(new Product() { Id = 18, Name = "Parsley", Category = "Food", Description = "Fruit", Price = 25 });
            listofProduct.Add(new Product() { Id = 19, Name = "Dill", Category = "Food", Description = "Fruit", Price = 54 });
            listofProduct.Add(new Product() { Id = 20, Name = "Tendalier", Category = "Food", Description = "Fruit", Price = 45 });
            listofProduct.Add(new Product() { Id = 21, Name = "Orange", Category = "Food", Description = "Fruit", Price = 74 });
            listofProduct.Add(new Product() { Id = 22, Name = "Plum", Category = "Food", Description = "Fruit", Price = 96 });
            listofProduct.Add(new Product() { Id = 23, Name = "Cake", Category = "Food", Description = "Fruit", Price = 78 });
            listofProduct.Add(new Product() { Id = 24, Name = "Garlic", Category = "Food", Description = "Fruit", Price = 58 });
            listofProduct.Add(new Product() { Id = 25, Name = "Raspberry", Category = "Food", Description = "Fruit", Price = 98 });
            listofProduct.Add(new Product() { Id = 26, Name = "Watermelon", Category = "Food", Description = "Fruit", Price = 78 });
            listofProduct.Add(new Product() { Id = 27, Name = "Melon", Category = "Food", Description = "Fruit", Price = 96 });
            listofUsers.Add(new Admin() { Id = 1, Email = "danilkrava@ukr.net", FirstName = "Danil", LastName = "Krava", NumberofCard = 1234567890, Money = 1234, Pasword = "Krava2002" });
            listofUsers.Add(new RegisteredUser() { Id = 2, Email = "maksim2002@gmail.com", FirstName = "Makisikm", LastName = "Genevskiy", NumberofCard = 456789765, Money = 590, Pasword = "Maks1234" });
            listofUsers.Add(new RegisteredUser() { Id = 3, Email = "iliamak@gmail.com", FirstName = "Ilia", LastName = "Makarov", NumberofCard = 5432789754, Money = 720, Pasword = "Ilia2001" });
            DBWork dBWork = new DBWork(listofProduct,listofUsers);
            productService = new ProductService(dBWork);
            orderService = new OrderService(dBWork);
            userService = new UserService(dBWork);
        }

        [Test]
        public void AddUser_TestForAdmin()
        {
            User admin = new User() { Id = 12, Email = "qwerty@", FirstName = "Name", LastName = "LastName", NumberofCard = 1234567890, Pasword = "qwerty1234" };

            userService.AddUser(admin);

            Assert.AreEqual(admin.Id, userService.GetUsers().Last().Id);
        }

        [Test]
        public void AddUser_TestForRegisteredUser()
        {
            RegisteredUser user = new RegisteredUser() { Id = 12, Email = "qwerty@", FirstName = "Name", LastName = "LastName", NumberofCard = 1234567890, Pasword = "qwerty1234" };

            userService.AddUser(user);

            Assert.AreEqual(user.Id, userService.GetUsers().Last().Id);
        }

        [Test]
        public void AddToBasket_TestForRegisteredUser()
        {
            RegisteredUser user = new RegisteredUser() { Id = 12, Email = "qwerty@", FirstName = "Name", LastName = "LastName", NumberofCard = 1234567890, Pasword = "qwerty1234" };
            Product product = productService.GetProduct(3);

            userService.AddUser(user);
            productService.AddToBasket(user, product.Id);

            Assert.AreEqual(product.Name, productService.GetBasket(user).Last().Name);
        }

        [Test]
        public void AddToBasket_TestForAdmin()
        {
            Admin admin = new Admin() { Id = 12, Email = "qwerty@", FirstName = "Name", LastName = "LastName", NumberofCard = 1234567890, Pasword = "qwerty1234" };
            Product product = productService.GetProduct(3);

            userService.AddUser(admin);
            productService.AddToBasket(admin, product.Id);

            Assert.AreEqual(product.Name, productService.GetBasket(admin).Last().Name);
        }
        [Test]
        public void CreateOrder_TestForAdmin()
        {
            Admin admin = new Admin() { Id = 12, Email = "qwerty@", FirstName = "Name", LastName = "LastName", NumberofCard = 1234567890, Pasword = "qwerty1234" };
            Order order = new Order() { Id = 2, Country = "Ukraine", NumberOfPost = 12, ProductId = 3, Status = Statuses.New };

            userService.AddUser(admin);
            orderService.MakeOrder(admin, order);

            Assert.AreEqual(order.ProductId, orderService.GetOrders(admin).Last().ProductId);
        }
        [Test]
        public void SetUp_Test()
        {
            object obj = userService.SetUp("Name", "LastName", "qwerty@", "qwerty1234");

            Assert.That(obj is User);
        }
        [Test]
        public void SetIn_Test()
        {
            object obj = userService.SetIn("danilkrava@ukr.net", "Krava2002");

            Assert.That((obj as User).Email == "danilkrava@ukr.net");
        }
        [Test]
        public void CancelOrder_Test()
        {
            Admin admin = new Admin() { Id = 12, Email = "qwerty@", FirstName = "Name", LastName = "LastName", NumberofCard = 1234567890, Pasword = "qwerty1234" };
            Order order = new Order() { Id = 2, Country = "Ukraine", NumberOfPost = 12, ProductId = 3, Status = Statuses.New };

            userService.AddUser(admin);
            orderService.MakeOrder(admin, order);
            orderService.CancelOrder(admin, orderService.GetOrders(admin).Last().Id);

            Assert.That(orderService.GetOrders(admin).Last().Status == Statuses.CanceledByUser);
        }
        [Test]
        public void ChangeEmail_Test()
        {
            userService.ChangeUsersEmail(userService.GetUser(2), "qwerty@ukr.net");

            Assert.That(userService.GetUser(2).Email == "qwerty@ukr.net");
        }
        [Test]
        public void ChangePassword_Test()
        {
            userService.ChangeUsersPassword(userService.GetUser(2), "12345678");

            Assert.That(userService.GetUser(2).Pasword == "12345678");
        }
        [Test]
        public void ChangeFullName_Test()
        {
            userService.ChangeUsersInfo(userService.GetUser(2), "Mark", "Krava");

            Assert.That(userService.GetUser(2).FirstName == "Mark" && userService.GetUser(2).LastName == "Krava");
        }
        [Test]
        public void SetGetToOrder_Test()
        {
            Admin admin = new Admin() { Id = 12, Email = "qwerty@", FirstName = "Name", LastName = "LastName", NumberofCard = 1234567890, Pasword = "qwerty1234" };
            Order order = new Order() { Id = 2, Country = "Ukraine", NumberOfPost = 12, ProductId = 3, Status = Statuses.New };

            userService.AddUser(admin);
            orderService.MakeOrder(admin, order);
            orderService.SetGetToOrder(admin, orderService.GetOrders(admin).Last().Id);

            Assert.That(orderService.GetOrders(admin).Last().Status == Statuses.Recieved);
        }

        [TestCase(Statuses.CanceledByAdmin)]
        [TestCase(Statuses.Sent)]
        [TestCase(Statuses.Ended)]
        public void ChangeStatus_Test(Statuses status)
        {
            Admin admin = new Admin() { Id = 12, Email = "qwerty@", FirstName = "Name", LastName = "LastName", NumberofCard = 1234567890, Pasword = "qwerty1234" };
            Order order = new Order() { Id = 2, Country = "Ukraine", NumberOfPost = 12, ProductId = 3, Status = Statuses.New };

            userService.AddUser(admin);
            orderService.MakeOrder(admin, order);
            orderService.ChangeStatus(admin, orderService.GetOrders(admin).Last().Id, status);

            Assert.That(orderService.GetOrders(admin).Last().Status == status);
        }

        [Test]
        public void ChangeProduct_Test()
        {
            Admin admin = new Admin() { Id = 12, Email = "qwerty@", FirstName = "Name", LastName = "LastName", NumberofCard = 1234567890, Pasword = "qwerty1234" };
            Product product = productService.GetProduct(3);

            userService.AddUser(admin);
            productService.EditProduct(product.Id, 123);

            Assert.That(productService.GetProduct(3).Price == 123);
        }
    }
}