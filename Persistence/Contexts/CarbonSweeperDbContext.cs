using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Persistence.Contexts
{
    public class CarbonSweeperDbContext : DbContext
    {
        public CarbonSweeperDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<GeneralConsumption> GeneralConsumptions { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<CalculationParameter> CalculationParameters { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>()
            //.HasOne(
            //
            //u => u.HouseId)             // User has one House
            //.WithOne(h => h.UserId)           // House has many Users
            //.HasForeignKey(u => u.HouseId);   // Foreign key property in User class

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserRole)          // User has one UserRole
                .WithMany(ur => ur.Users)         // UserRole has many Users
                .HasForeignKey(u => u.UserRoleId); 
        }
    }
}
