using System.Security.Cryptography;
using System.Text;
using WorkTimePanelFull.Domain.Entities;

namespace WorkTimePanelFull.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (db.Users.Any()) return;

            string Hash(string input)
            {
                using var sha = SHA256.Create();
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(bytes);
            }

            db.Users.Add(new User
            {
                Username = "admin",
                PasswordHash = Hash("Admin@123"),
                Role = "RH"
            });
            db.SaveChanges();
        }
    }
}
