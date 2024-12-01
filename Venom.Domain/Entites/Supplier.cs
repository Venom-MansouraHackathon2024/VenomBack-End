using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Domain.Entites
{
    public class Supplier
    {
        public int Id { get; set; }
       // public int PrandId { get; set; }
       // public List<Prand> Prand {  get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product>? Products { get; set; }
    }
}
