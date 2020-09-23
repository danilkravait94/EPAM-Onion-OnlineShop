using Onion.Domain.Core.Enums;
using System;

namespace Onion.Domain.Core
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Statuses Status { get; set; }
        public string Country { get; set; }
        public int NumberOfPost { get; set; }
        public override string ToString()
        {
            return $"{Id} {ProductId} {Country}";
        }
    }
}
