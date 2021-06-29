using BuildingCompany.Models.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingCompany.Models
{
    public class DB_Context : DbContext
    {
        public virtual DbSet<Deals> Deals { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Equipments> Equipments { get; set; }
        public virtual DbSet<Equipments_works> Equipments_Works { get; set; }
        public virtual DbSet<Facility> Facility { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        public virtual DbSet<Works> Works { get; set; }

        public DB_Context(DbContextOptions<DB_Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Authorships>()
            .HasKey(o => new { o.Member_id, o.Lecture_id });
            modelBuilder.Entity<MRLink>()
            .HasKey(o => new { o.Role_id, o.Member_id});
            modelBuilder.Entity<Shedule>()
            .HasKey(o => new { o.Lecture_id, o.Seminar_id });*/
        }
    }

    public class DB_ContextFactory : IDesignTimeDbContextFactory<DB_Context>
    {
        public string CreateConnectionString(string[] args)
        {
            return "Server = 127.0.0.1; User Id = " + args[0] + "; Password = " + args[1] + "; Port = 5432; Database = BuildindCompany; ";//"Server = 127.0.0.1; User Id = " + arg[0] + "; Password = " + arg[1] + "; Port = 5432; Database = Lunapark; "; //
        }

        public DB_Context CreateDbContext(string[] args)
        {
            string connectionString;
            var optionBuilder = new DbContextOptionsBuilder<DB_Context>();
            connectionString = CreateConnectionString(args);
            optionBuilder.UseNpgsql(connectionString);
            return new DB_Context(optionBuilder.Options);
        }
    }
}
