using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entites;

namespace Venom.Application.Reviews.Dtos
{
    public class UpdateReviewDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
