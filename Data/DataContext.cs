using f00die_finder_be.Entities;
using Microsoft.EntityFrameworkCore;

namespace f00die_finder_be.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<AdditionalService> AdditionalServices { get; set; }
        public DbSet<BusinessHour> BusinessHours { get; set; }
        public DbSet<CuisineType> CuisineTypes { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ProvinceOrCity> ProvinceOrCities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantAdditionalService> RestaurantAdditionalServices { get; set; }
        public DbSet<RestaurantCuisineType> RestaurantCuisineTypes { get; set; }
        public DbSet<RestaurantImage> RestaurantImages { get; set; }
        public DbSet<RestaurantServingType> RestaurantServingTypes { get; set; }
        public DbSet<ReviewComment> ReviewComments { get; set; }
        public DbSet<ServingType> ServingTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WardOrCommune> WardOrCommunes { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<RestaurantCustomerType> RestaurantCustomerTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Reservation>()
                        .HasOne(c => c.Restaurant)
                        .WithMany(u => u.Reservations)
                        .HasForeignKey(c => c.RestaurantId)
                        .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Reservation>()
                        .HasOne(c => c.User)
                        .WithMany(u => u.Reservations)
                        .HasForeignKey(c => c.UserId)
                        .OnDelete(DeleteBehavior.NoAction);
            
            builder.Entity<ReviewComment>()
                .HasOne(c => c.Restaurant)
                        .WithMany(u => u.Reviews)
                        .HasForeignKey(c => c.RestaurantId)
                        .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ReviewComment>()
                .HasOne(u => u.User)
                        .WithMany(u => u.Reviews)
                        .HasForeignKey(c => c.UserId)
                        .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
