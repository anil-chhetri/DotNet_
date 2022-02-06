using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jwtTest
{
    public class appDbContext : IdentityDbContext<IdentityUser>
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options)
        {
            
        }
    }
}