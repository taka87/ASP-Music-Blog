using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        //        public DbSet<Question> Questions { get; set; }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<Answer> Answers { get; set; } 


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Blog.Models.Russian> Russians { get; set; }



        public System.Data.Entity.DbSet<Blog.Models.S1> S1 { get; set; }

        public System.Data.Entity.DbSet<Blog.Models.House> Houses { get; set; }

        public System.Data.Entity.DbSet<Blog.Models.LatinPage> LatinPages { get; set; }


    }
}