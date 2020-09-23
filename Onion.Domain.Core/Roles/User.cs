using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Domain.Core.Roles
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pasword { get; set; }
        public long NumberofCard { get; set; }
        public decimal Money { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Product> Basket { get; set; } = new List<Product>();
        public override string ToString()
        {
            return $"{FirstName} {LastName} {Email} {Money}$";
        }
    }
}
