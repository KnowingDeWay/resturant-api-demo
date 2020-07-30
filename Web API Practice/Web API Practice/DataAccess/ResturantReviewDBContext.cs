using Microsoft.EntityFrameworkCore;
using Web_API_Practice.Configuration;
using Web_API_Practice.Models;

namespace Web_API_Practice.DataAccess
{
    public class ResturantReviewDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Resturant> Resturants { get; set; }
        public DbSet<ResturantReview> ResturantReviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SystemResources.DATABASE_CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resturant>().OwnsOne(x => x.ResturantAddress);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Resturant>().ToTable("Resturants");
            modelBuilder.Entity<ResturantReview>().ToTable("ResturantReviews");
        }
    }
}
