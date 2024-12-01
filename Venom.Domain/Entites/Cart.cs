using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Venom.Domain.Entites
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } 
        [JsonIgnore]
        public virtual ApplicationUser User { get; set; } 
        public virtual ICollection<CartItem>? CartItems { get; set; }
    }
}
