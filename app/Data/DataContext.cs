using app.Helpers;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public class DataContext :  DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        public DbSet<User> Users { get; set; }
    }
}