using Crud.Example.Domain.Entities;
using Crud.Example.Infrastructure.Data.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Crud.Example.Infrastructure.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly ModelBuilderSeed? _modelBuilderSeed;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //Check if Database not exist
            if (!Database.CanConnect())
            {
                //Create the DataBase
                Database.EnsureCreated();

                //Create the Data Seed
                _modelBuilderSeed = new ModelBuilderSeed(this);
                _modelBuilderSeed.Seed();
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FoodOrder>()
                    .HasKey(bc => new { bc.FoodId, bc.OrderId });

            builder.Entity<Shop>()
                .HasMany(D => D.Dealers)
                .WithOne(bc => bc.Shop);

            builder.Entity<Dealer>()
                .HasOne(s => s.Shop)
                .WithMany(bc => bc.Dealers);

            builder.Entity<Dealer>()
                .HasMany(D => D.Orders)
                .WithOne(bc => bc.Dealer);

            builder.Entity<Order>()
                .HasOne(s => s.Dealer)
                .WithMany(bc => bc.Orders);
        }

        public DbSet<Dealer>? Dealer { get; set; }
        public DbSet<Shop>? Shop { get; set; }
        public DbSet<Food>? Foods { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<FoodOrder>? FoodOrders { get; set; }
    }
}
