using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Core.Entities;

namespace JobPortal.Infrastructure.Data
{

    public static class DbSeeder
    {
        public static void Seed(JobPortalDbContext context)
        {
            context.Database.EnsureCreated();

            // ✅ Seed Users
            if (!context.Users.Any())
            {
                var users = new List<User>
            {
                new User
                {
                    Name = "Admin User",
                    Email = "admin@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin"
                },
                new User
                {
                    Name = "Normal User",
                    Email = "user@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                    Role = "User"
                }
            };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            // ✅ Seed Jobs
            if (!context.Jobs.Any())
            {
                var jobs = new List<Job>
            {
                new Job
                {
                    Title = ".NET Developer",
                    Description = "Work on Web API & Microservices",
                    Company = "TCS",
                    Location = "Mumbai",
                    Salary = 800000,
                    CreatedAt = DateTime.UtcNow
                },
                new Job
                {
                    Title = "Java Developer",
                    Description = "Spring Boot + Kafka",
                    Company = "Infosys",
                    Location = "Bangalore",
                    Salary = 900000,
                    CreatedAt = DateTime.UtcNow
                }
            };

                context.Jobs.AddRange(jobs);
                context.SaveChanges();
            }

        }
    }
}
