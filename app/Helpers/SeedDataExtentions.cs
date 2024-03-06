using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Helpers
{
    public static class SeedDataExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User{
                    Id = new Guid("2bc66317-fabc-4580-83ae-48d2ab60fa8b"),
                    UserName = "test",
                    Age = 9
                }
            );

        }
    }
}