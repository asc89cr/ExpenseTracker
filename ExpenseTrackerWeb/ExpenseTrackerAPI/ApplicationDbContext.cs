using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // This is important for Cosmos DB, as it requires specifying containers
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToContainer("Users"); // Specify the container name for Cosmos DB
                entity.HasPartitionKey(user => user.Id); // Set the partition key
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToContainer("Roles"); // Specify the container name for roles
                entity.HasPartitionKey(role => role.Id); // Set the partition key
            });
        }
    }
}
