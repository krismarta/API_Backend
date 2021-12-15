using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profilling> Profillings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to one Employe to Account
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.account)
                .WithOne(b => b.employee)
                .HasForeignKey<Account>(b => b.Nik);

            //one to one Account to Profiling
            modelBuilder.Entity<Account>()
                .HasOne(a => a.profilling)
                .WithOne(b => b.account)
                .HasForeignKey<Profilling>(b => b.Nik);

            //many to one Profilling to Education
            modelBuilder.Entity<Profilling>()
                .HasOne(c => c.education)
                .WithMany(e => e.profilling);

            // many to one Education to University
            modelBuilder.Entity<University>()
                .HasMany(c => c.education)
                .WithOne(e => e.university);


            //one to many account to accountrole
            modelBuilder.Entity<Account>()
                .HasMany(c => c.accountrole)
                .WithOne(e => e.account);


            //one to many role to accountrole
            modelBuilder.Entity<Role>()
                .HasMany(c => c.accountrole)
                .WithOne(e => e.role);
                

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
