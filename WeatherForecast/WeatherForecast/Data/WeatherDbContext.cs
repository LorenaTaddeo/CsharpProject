using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherForecast.Models;

namespace WeatherApp.Data
{
    public class WeatherDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<WeatherFor> WeatherFor { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WeatherFor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.LocationName)
                        .IsRequired()
                        .HasMaxLength(255); 
                entity.Property(e => e.MinTemperature)
                    .IsRequired();
                entity.Property(e => e.MaxTemperature)
                    .IsRequired();
                entity.Property(e => e.DateTime)
                    .IsRequired();
                entity.Property(e => e.IconPhrase)
                    .IsRequired()
                    .HasMaxLength(100); 
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Nickname).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });
        }
    }
}
