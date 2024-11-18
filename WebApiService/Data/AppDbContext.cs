using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ClientLastName> LastNames { get; set; }

        public DbSet<ClientFirstName> FirstNames { get; set; }

        public DbSet<ClientEmail> Emails { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<OrderOrderStatus> OrderStatuses { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Service> Services { get; set; }
    }
}
