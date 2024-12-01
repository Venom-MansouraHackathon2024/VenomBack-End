using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Domain.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } // Pending, Completed, etc.
        public DateTime CreatedDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
