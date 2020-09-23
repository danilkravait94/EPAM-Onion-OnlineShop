using Onion.Domain.Core;
using Onion.Domain.Core.Enums;
using Onion.Domain.Core.Roles;
using Onion.Infrastructure.Business;
using Onion.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEShop
{
    class Program
    {
        static Guest guest;
        static User user;
        static OrderService orderService;
        static ProductService productService;
        static UserService userService;
        static Dictionary<ConsoleKey, Action> ActionsKey = new Dictionary<ConsoleKey, Action>();
        static Order CurrentOrder;
        static Product CurrentProduct;
        static User CurrentUser;
        static void Main(string[] args)
        {
            guest = new Guest();
            SetDictionary();
            SetDB();
            Paint();
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (key.Key != ConsoleKey.Escape)
            {
                Paint();
                key = Console.ReadKey();
                if (ActionsKey.ContainsKey(key.Key))
                {
                    Clear();
                    ActionsKey[key.Key]();
                }
                else
                {
                    Console.WriteLine("Cannot find a action for this key");
                }
            }
        }
        static void Clear()
        {
            for (int i = 0; i < 80; i++)
            {
                for (int j = 1; j < productService.GetProducts().Count() + 4; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(" ");
                }
            }
            Console.SetCursorPosition(0, 1);
        }
        private static void Paint()
        {

            Console.SetCursorPosition(0, 0);
            Console.Write("/ List of Products(L) / Basket(B) / Orders(O) / Find(F) /");
            if (user is null)
            {
                Console.SetCursorPosition(70, 0);
                Console.Write("Sign in(I)/Sign up(U)\n");
            }
            else
            {
                Console.SetCursorPosition(70, 0);
                Console.Write($"{user.FirstName}/Exit(E)\n");
            }
        }
        static void SetDictionary()
        {
            ActionsKey.Add(ConsoleKey.L, ShowProducts);
            ActionsKey.Add(ConsoleKey.O, ShowOrders);
            ActionsKey.Add(ConsoleKey.F, FindProduct);
            ActionsKey.Add(ConsoleKey.B, ShowBasket);
            ActionsKey.Add(ConsoleKey.I, SignIn);
            ActionsKey.Add(ConsoleKey.U, SignUp);
            ActionsKey.Add(ConsoleKey.E, Exit);
        }
        #region Products
        static void ShowProducts()
        {
            foreach (var item in productService.GetProducts())
            {
                Console.WriteLine(item);
            }
        }
        static void FindProduct()
        {
            try
            {
                Console.Write("Id of product: ");
                int id = Convert.ToInt32(Console.ReadLine());
                CurrentProduct = productService.GetProduct(id);
                Console.WriteLine(CurrentProduct);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void ShowBasket()
        {
            if (user is null)
            {
                Console.WriteLine("Access deny");
                return;
            }

            foreach (var item in productService.GetBasket(user))
            {
                Product product = new Product() { Category = item.Category, Description = item.Description, Id = item.Id, Name = item.Name, Price = item.Price };
                Console.WriteLine(product);
            }
        }
        static void CreateProduct()
        {
            if (user is RegisteredUser)
            {
                Console.WriteLine("Access deny");
                return;
            }
            try
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("\nCategory: ");
                string category = Console.ReadLine();
                Console.Write("\nDescription: ");
                string description = Console.ReadLine();
                Console.Write("\nPrice: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                Product product = new Product() { Category = category, Description = description, Name = name, Price = price };

                productService.CreateProduct(product);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void AddToBasket()
        {
            try
            {
                Console.Write("ProductId: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                while (productId < 0 || productId > productService.GetProducts().Count())
                {
                    Console.WriteLine("\nWrite a number one more time");
                    productId = Convert.ToInt32(Console.ReadLine());
                }
                productService.AddToBasket(user, productId);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void DeleteFromBasket()
        {
            try
            {
                ShowBasket();
                Console.Write("ProductId: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                while (productId < 0 || productId > productService.GetBasket(user).Count())
                {
                    Console.WriteLine("\nWrite a number one more time");
                    productId = Convert.ToInt32(Console.ReadLine());
                }
                productService.DeleteFromBasket(user, productId);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void DeleteProduct()
        {
            if (user is RegisteredUser)
            {
                Console.WriteLine("Access deny");
                return;
            }
            try
            {
                Console.Write("ProductId: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                while (productId < 0 || productId > productService.GetProducts().Count())
                {
                    Console.WriteLine("\nWrite a number one more time");
                    productId = Convert.ToInt32(Console.ReadLine());
                }
                productService.DeleteProduct(productId);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region Orders
        static void ShowOrders()
        {
            if (user is null)
            {
                Console.WriteLine("Access deny");
                return;
            }
            foreach (var item in orderService.GetOrders(user))
            {
                Console.WriteLine(item);
            }
        }
        static void FindOrder()
        {
            try
            {
                Console.Write("Id of order: ");
                int id = Convert.ToInt32(Console.ReadLine());
                CurrentOrder = orderService.GetOrder(user, id);
                Console.WriteLine(CurrentProduct);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void CreateOrder()
        {
            try
            {
                Console.Write("ProductId: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                while (productId < 0 || productId > productService.GetBasket(user).Count())
                {
                    Console.WriteLine("\nWrite a number one more time");
                    productId = Convert.ToInt32(Console.ReadLine());
                }
                Console.Write("\nCountry: ");
                string country = Console.ReadLine();
                Console.Write("\nNumber of post: ");
                int number = Convert.ToInt32(Console.ReadLine());

                Order order = new Order() { Country = country, NumberOfPost = number, ProductId = productId, Status = Statuses.New };

                orderService.MakeOrder(user, order);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void SetGetToOrder()
        {
            try
            {
                ShowOrders();
                Console.Write("Id of order:");
                int number = Convert.ToInt32(Console.ReadLine());
                while (number < 0 || number > orderService.GetOrders(user).Count())
                {
                    Console.Write("\nWrite an id one more time:");
                    number = Convert.ToInt32(Console.ReadLine());
                }
                orderService.SetGetToOrder(user, number);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void CancelOrder()
        {
            try
            {
                ShowOrders();
                Console.Write("Id of order:");
                int number = Convert.ToInt32(Console.ReadLine());
                while (number < 0 || number > orderService.GetOrders(user).Count())
                {
                    Console.Write("\nWrite an id one more time:");
                    number = Convert.ToInt32(Console.ReadLine());
                }
                orderService.CancelOrder(user, number);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void ChangeStatus()
        {
            try
            {
                ShowOrders();
                Console.Write("Id of order:");
                int number = Convert.ToInt32(Console.ReadLine());
                while (number < 0 || number > orderService.GetOrders(user).Count())
                {
                    Console.Write("\nWrite an id one more time:");
                    number = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("\nSelect a status:");
                Console.WriteLine("1.CanceledByAdmin");
                Console.WriteLine("2.Ended");
                Console.WriteLine("3.GetMoney");
                Console.WriteLine("4.Recieved");
                Console.WriteLine("5.Sent");
                int nstatus = Convert.ToInt32(Console.ReadLine());
                while (nstatus <= 0 || nstatus > 5)
                {
                    Console.Write("\nWrite a number of status one more time:");
                    nstatus = Convert.ToInt32(Console.ReadLine());
                }
                Statuses status;
                switch (nstatus)
                {
                    case 1: status = Statuses.CanceledByAdmin; break;
                    case 2: status = Statuses.Ended; break;
                    case 3: status = Statuses.GetMoney; break;
                    case 4: status = Statuses.Recieved; break;
                    default: status = Statuses.Sent; break;
                }
                orderService.ChangeStatus(user, number, status);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region Users
        static void SignUp()
        {
            if (user != null) return;
            try
            {
                long card;
                Console.Write("First Name: ");
                string firstname = Console.ReadLine();
                Console.Write("\nLast Name: ");
                string lastname = Console.ReadLine();
                Console.Write("\nEmail: ");
                string email = Console.ReadLine();
                if (userService.IsUsed(email))
                {
                    Console.WriteLine("The email is used");
                    return;
                }
                string passwordfirst, passwordsecond;
                do
                {
                    Console.WriteLine("Write your password");
                    passwordfirst = Console.ReadLine();
                    Console.WriteLine("Repeat the password");
                    passwordsecond = Console.ReadLine();
                }
                while (passwordfirst != passwordsecond);
                string password = passwordfirst;
                Console.WriteLine("Do you want to add a card?\n" +
                    "Press 'Y' to Yes\n" +
                    "Press 'N' to No");
                guest = null;
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.WriteLine("Write a number of your card");
                    card = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine("The registration was successful");
                    user = userService.SetUp(firstname, lastname, email, password, card);
                    return;
                }
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.WriteLine("The registration was successful");
                user = userService.SetUp(firstname, lastname, email, password);
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void SignIn()
        {
            if (user != null) return;
            try
            {

                string email, password;
                Console.Write("Email: ");
                email = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();
                if (userService.IsUsed(email))
                {
                    User lUser = userService.GetByEmail(email);
                    if (lUser.Pasword == password)
                    {
                        guest = null;
                        user = userService.SetIn(email, password);
                        return;
                    }
                    else
                    {
                        throw new AccessException("Incorect email");
                    }
                }
                else
                {
                    throw new AccessException("Incorect email");
                }
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void AddUser()
        {
            if (user is RegisteredUser) return;
            try
            {
                Console.Write("First Name: ");
                string firstname = Console.ReadLine();
                Console.Write("\nLast Name: ");
                string lastname = Console.ReadLine();
                Console.Write("\nEmail: ");
                string email = Console.ReadLine();
                if (userService.IsUsed(email))
                {
                    //throw
                }
                string password;
                Console.WriteLine("Write your password");
                password = Console.ReadLine();
                userService.AddUser(new User() { FirstName = firstname, LastName = lastname, Email = email, Pasword = password });
            }
            catch (ValidateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void ShowUsers()
        {
            if (user is RegisteredUser) return;
            foreach (var item in userService.GetUsers())
            {
                Console.WriteLine(item);
            }
        }
        static void Exit()
        {
            guest = new Guest();
            user = null;
        }
        #endregion
        static void SetDB()
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
            DBWork dBWork = new DBWork(listofProduct, listofUsers);
            productService = new ProductService(dBWork);
            orderService = new OrderService(dBWork);
            userService = new UserService(dBWork);
        }
    }
}