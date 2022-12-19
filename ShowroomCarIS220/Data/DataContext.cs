using Microsoft.EntityFrameworkCore;
using ShowroomCarIS220.DTO.User;
using ShowroomCarIS220.Models;
using System.Reflection.Metadata;

namespace ShowroomCarIS220.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
                
        }

        public DbSet<CTHD> CTHD { get; set; }

        public DbSet<Customer> Customer { get; set; }
        //public DbSet<Employee> Employee { get; set; }

        public DbSet<Car> Car { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Token> Token { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTHD>().HasKey(r => new { r.macar, r.mahd });
            modelBuilder.Entity<HoaDon>().HasKey(r => new { r.mahd, r.makh, r.manv });
            modelBuilder.Entity<User>(entity => {
                entity.HasIndex(e => e.email).IsUnique();
            });
        }
    }
}
