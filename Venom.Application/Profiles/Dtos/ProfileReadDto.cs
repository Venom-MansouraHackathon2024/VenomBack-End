using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Application.Profiles.Dtos
{
    public class ProfileReadDto
    {
        public string Id { get; set; }
        public string ProfilePicture { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
