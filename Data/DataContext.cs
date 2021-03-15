using Microsoft.EntityFrameworkCore;
using Projekt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasIndex(c => new { c.Name, c.Address })
                .IsUnique(true);


        }
    }
}
