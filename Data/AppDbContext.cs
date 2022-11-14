using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SixMinApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Command> Commands { get; set; }

        //Temporary fix for initial migration....
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Command>(x =>

            {
                x.Property<int>("Id");
                x.HasKey("Id");
                x.Property<string>("HowTo");
                x.Property<string>("Platform");
                x.Property<string>("CommandLine");

            });
        }

    }
}