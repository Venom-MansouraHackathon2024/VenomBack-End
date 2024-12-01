using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entites;

namespace Venom.Application.Reviews.Dtos
{
    public class ReadProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public double AverageRating { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }    

    }
}
