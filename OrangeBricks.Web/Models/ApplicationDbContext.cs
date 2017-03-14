using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OrangeBricks.Web.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IOrangeBricksContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().
                HasMany(x => x.Offers)
                .WithRequired()
                .HasForeignKey(x => x.UserId);
        }

        public IDbSet<Property> Properties { get; set; }
        public IDbSet<Offer> Offers { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }

    public interface IOrangeBricksContext
    {
        IDbSet<Property> Properties { get; set; }
        IDbSet<Offer> Offers { get; set; }

        void SaveChanges();
    }
}