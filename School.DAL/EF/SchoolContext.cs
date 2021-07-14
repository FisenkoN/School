using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using School.Models;

namespace School.DAL.EF
{
    public class SchoolContext:DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Class> Classes { get; set; }
        
        public SchoolContext(){}
        
        public SchoolContext(DbContextOptions options) :base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            const string connectionString = @"data source=DESKTOP-8TNDF2C\SQLEXPRESS; initial catalog=SchoolDB_v1.1.1;integrated security=True; MultipleActiveResultSets=True;App=EntityFramework";
            optionsBuilder.UseSqlServer(connectionString,
                    options => 
                        options.EnableRetryOnFailure())
                . ConfigureWarnings (warnings=>
                    warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning) ) ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>()
                .HasMany<Teacher>(s => s.Teachers)
                .WithMany(t => t.Subjects);

            modelBuilder.Entity<Student>()
                .HasMany<Subject>(s => s.Subjects)
                .WithMany(s => s.Students);

            modelBuilder.Entity<Class>()
                .HasMany<Student>(c => c.Students)
                .WithOne(s => s.Class)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Class>()
                .HasOne(c => c.Teacher)
                .WithOne(t => t.Class);
        }

        public string GetTableName(Type type)
        {
            return Model.FindEntityType(type).GetTableName();
        }
    }
}