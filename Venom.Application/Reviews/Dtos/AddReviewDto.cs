using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entites;

namespace Venom.Application.Reviews.Dtos
{
    public class AddReviewDto
    {

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int ProductId { get; set; }
        public string UserId { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
