using ExpenseTracker.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure partition key for IdentityUser and IdentityRole
            builder.Entity<IdentityUser>()
                .HasPartitionKey(u => u.Id);

            builder.Entity<IdentityRole>()
                .HasPartitionKey(r => r.Id);

            // Remove ConcurrencyStamp as concurrency token
            builder.Entity<IdentityUser>()
                .Property(u => u.ConcurrencyStamp)
                .IsConcurrencyToken(false);

            builder.Entity<IdentityRole>()
                .Property(r => r.ConcurrencyStamp)
                .IsConcurrencyToken(false);

            // Use _etag as concurrency token
            builder.Entity<IdentityUser>()
                .Property<string>("_etag")
                .IsETagConcurrency();

            builder.Entity<IdentityRole>()
                .Property<string>("_etag")
                .IsETagConcurrency();

            // Map NormalizedUserName for IdentityUser
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToContainer("Users");
                entity.Property(u => u.NormalizedUserName).IsRequired(); // Keep NormalizedUserName
                entity.HasIndex(u => u.NormalizedUserName)  // Create a non-indexed virtual property
                      .HasDatabaseName("NoIndex");
            });

            // Map NormalizedName for IdentityRole without index
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToContainer("Roles");
                entity.Property(r => r.NormalizedName).IsRequired(); // Map NormalizedName
                entity.HasIndex(r => r.NormalizedName)  // Create a non-indexed virtual property
                      .HasDatabaseName("NoIndex");
            });

            // Configure other entities (Income, Expense, ExpenseCategory)
            builder.Entity<Income>(entity =>
            {
                entity.ToContainer("Incomes");
                entity.HasKey(i => i.Id);
                entity.HasPartitionKey(i => i.Id);
                entity.Property(i => i.Source).IsRequired();
                entity.Property(i => i.Date).IsRequired();
                entity.Property(i => i.Amount).IsRequired();
            });

            builder.Entity<Expense>(entity =>
            {
                entity.ToContainer("Expenses");
                entity.HasKey(e => e.Id);
                entity.HasPartitionKey(e => e.Id);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.HasOne(e => e.Category)
                      .WithMany()
                      .HasForeignKey("CategoryId");
            });

            builder.Entity<ExpenseCategory>(entity =>
            {
                entity.ToContainer("ExpenseCategories");
                entity.HasKey(c => c.Id);
                entity.HasPartitionKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();
            });
        }



    }
}
