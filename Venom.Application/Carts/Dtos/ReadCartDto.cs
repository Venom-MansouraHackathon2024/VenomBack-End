using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Application.CartItems.Dtos;

namespace Venom.Application.Carts.Dtos
{
    public class ReadCartDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<ReadCartItemDto>? CartItems { get; set; }
    }
}
