using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Application.Profiles.Dtos
{
    public class ProfileUpdateDto
    {
        public string? ProfilePicture { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
