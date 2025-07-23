using Microsoft.EntityFrameworkCore;

namespace FrostedCornerWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<FranchiseItem> FranchiseItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<SuppliesOrder> SuppliesOrders { get; set; }
        public DbSet<SuppliesItem> SuppliesItems { get; set; }

        public DbSet<DietaryRestriction> DietaryRestrictions { get; set; }

        // Configure relationships (Order has OrderItem)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FranchiseItem>()
                .HasOne(f => f.Franchise)
                .WithMany(fi => fi.FranchiseItems)
                .HasForeignKey(id => id.FranchiseId);

            modelBuilder.Entity<DietaryRestriction>().HasData(
                new DietaryRestriction { DietaryRestrictionId = 1, Name = "Vegan" },
                new DietaryRestriction { DietaryRestrictionId = 2, Name = "Vegetarian" },
                new DietaryRestriction { DietaryRestrictionId = 3, Name = "Gluten-Free" },
                new DietaryRestriction { DietaryRestrictionId = 4, Name = "Nut-Free" },
                new DietaryRestriction { DietaryRestrictionId = 5, Name = "Dairy-Free" }
            );
        }

    }
}
