using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entities;

namespace Venom.Domain.Entites
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfilePicture { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public int? Points { get; set; }
        public Address? Address { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
