using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class EDbContext: DbContext
    {
        public virtual DbSet<Company> Companys { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }

        public EDbContext() { }

        public EDbContext(DbContextOptions<EDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //var serverVersion = ServerVersion.Parse("1.0");
            //options.UseMySql("server=192.168.10.100;port=3306;userid=root;password=123456;database=miaomiao;TreatTinyAsBoolean=false;Persist Security Info=True;", serverVersion);
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>().ToTable("company");
            modelBuilder.Entity<Manager>().ToTable("manager");
            modelBuilder.Entity<ClientUser>().ToTable("clientuser");

            modelBuilder.Entity<Company>().HasKey(p => p.id);
            modelBuilder.Entity<Company>().Property(p => p.id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Company>().HasMany(t => t.managers).WithOne(p => p.company);

            modelBuilder.Entity<Manager>().HasKey(p => p.id);
            modelBuilder.Entity<Manager>().Property(p => p.id).ValueGeneratedOnAdd();


        }
    }
}
