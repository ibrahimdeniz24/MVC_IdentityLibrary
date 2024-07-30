using _22_MVC_Identity.Models.Entites.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _22_MVC_Identity.Models.Infradstructre.ContextDB
{
    //role diye sınıf oluşturup ıdentity rose miras aldırıp onuda yazabilirdik.
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        //servicedeki connection string buradan ulaşıyor.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }


    }
}
