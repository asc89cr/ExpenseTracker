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

            // Configure partition key for IdentityUser and IdentityRole
            builder.Entity<IdentityUser>()
                .HasPartitionKey(u => u.Id);  // Assuming Id is the partition key for users

            builder.Entity<IdentityRole>()
                .HasPartitionKey(r => r.Id);  // Assuming Id is the partition key for roles

            // Remove ConcurrencyStamp as a concurrency token for IdentityRole
            builder.Entity<IdentityUser>()
                .Property(r => r.ConcurrencyStamp)
                .IsConcurrencyToken(false);  // Disable concurrency token on ConcurrencyStamp


            builder.Entity<IdentityUser>()
            .Property<string>("_etag")  // Cosmos DB's ETag property
            .IsETagConcurrency();  // Use ETag as the concurrency token

            // Remove ConcurrencyStamp as a concurrency token for IdentityRole
            builder.Entity<IdentityRole>()
                .Property(r => r.ConcurrencyStamp)
                .IsConcurrencyToken(false);  // Disable concurrency token on ConcurrencyStamp


            builder.Entity<IdentityRole>()
            .Property<string>("_etag")  // Cosmos DB's ETag property
            .IsETagConcurrency();  // Use ETag as the concurrency token

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
