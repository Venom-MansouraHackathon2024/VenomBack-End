using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venom.Domain.Entites;
using Venom.Domain.IRepositories;
using Venom.infrastructure.Persistance;

namespace Venom.infrastructure.Repositories
{
    public class ProfileRepo : IprofileRepo
    {
        private readonly VenomDbContext _context;
        public ProfileRepo(VenomDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteAsync(ApplicationUser user)
        {
            _context.Users.Remove(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            var user = await _context.Users
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return null;
            }
            return user;

        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
