using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using School.DAL.Entities;

namespace School.DAL.EF
{
    public class SchoolDbContext:DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Class> Classes { get; set; }

        private readonly IConfiguration _configuration;

        public SchoolDbContext()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(@"E:\Tasks\School\School.DAL\appsettings.json").Build();
        }
        
        public SchoolDbContext(DbContextOptions options) :base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString,
                    options => 
                        options.EnableRetryOnFailure())
                . ConfigureWarnings (warnings=>
                    warnings.Throw(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning) ) ;
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     // modelBuilder.Entity<Student>()
        //     //     .Property(s => s.FirstName)
        //     //     .IsRequired()
        //     //     .HasMaxLength(20);
        //     //
        //     // modelBuilder.Entity<Student>()
        //     //     .Property(s => s.LastName)
        //     //     .IsRequired()
        //     //     .HasMaxLength(20);
        //     //
        //     // modelBuilder.Entity<Student>()
        //     //     .Property(s => s.Gender)
        //     //     .IsRequired();
        //     //
        //     // modelBuilder.Entity<Student>()
        //     //     .Property(s => s.Timestamp)
        //     //     .IsRowVersion()
        //     //     .IsRequired();
        //     //
        //     // modelBuilder.Entity<Student>()
        //     //     .Ignore(s => s.FullName);
        //     //
        //     // // modelBuilder.Entity<Student>()
        //     // //     .Property(s => s.ClassId)
        //     // //     .HasField("Class");
        //     //
        //     //
        //     //
        //     //     modelBuilder.Entity<Student>()
        //     //     .HasOne(s => s.Class)
        //     //     .WithMany(c => c.Students)
        //     //     .HasForeignKey(s => s.ClassId);
        //     //
        //     // modelBuilder.Entity<Student>()
        //     //     .HasMany(s => s.Subjects)
        //     //     .WithMany(s => s.Students);
        //     //
        //     //
        //     // modelBuilder.Entity<Teacher>()
        //     //     .HasMany(t => t.Subjects)
        //     //     .WithMany(s => s.Teachers);
        //     //
        //     // modelBuilder.Entity<Teacher>()
        //     //     .HasKey(t => t.Id);
        //     //
        //     // modelBuilder.Entity<Teacher>()
        //     //     .Property(t => t.Timestamp)
        //     //     .IsRowVersion()
        //     //     .IsRequired();
        //     //
        //     // modelBuilder.Entity<Teacher>()
        //     //     .Property(t => t.FirstName)
        //     //     .IsRequired()
        //     //     .HasMaxLength(20);
        //     //
        //     // modelBuilder.Entity<Teacher>()
        //     //     .Property(t => t.LastName)
        //     //     .IsRequired()
        //     //     .HasMaxLength(20);
        //     //
        //     // // modelBuilder.Entity<Teacher>()
        //     // //     .Property(t => t.ClassId)
        //     // //     .HasField("Class");
        //     //
        //     // modelBuilder.Entity<Teacher>()
        //     //     .Property(t => t.Gender)
        //     //     .IsRequired();
        //     //
        //     // modelBuilder.Entity<Teacher>()
        //     //     .Ignore(t => t.FullName);
        //     //
        //     // modelBuilder.Entity<Teacher>()
        //     //     .HasOne(t => t.Class)
        //     //     .WithOne(c => c.Teacher)
        //     //     .HasForeignKey<Teacher>(t=>t.ClassId)
        //     //     .HasForeignKey<Class>(c=>c.TeacherId);
        //     //
        //     // modelBuilder.Entity<Class>()
        //     //     .HasKey(c => c.Id);
        //     //
        //     // modelBuilder.Entity<Class>()
        //     //     .Property(c => c.Name)
        //     //     .IsRequired()
        //     //     .HasMaxLength(4);
        //     //
        //     // modelBuilder.Entity<Class>()
        //     //     .Property(c => c.TeacherId)
        //     //     .HasField("Teacher");
        //     //
        //     // modelBuilder.Entity<Class>()
        //     //     .Property(c => c.Timestamp)
        //     //     .IsRequired()
        //     //     .IsRowVersion();
        //     //
        //     // modelBuilder.Entity<Subject>()
        //     //     .HasKey(s => s.Id);
        //     //
        //     // modelBuilder.Entity<Subject>()
        //     //     .Property(s => s.Timestamp)
        //     //     .IsRequired()
        //     //     .IsRowVersion();
        //     //
        //     // modelBuilder.Entity<Subject>()
        //     //     .Property(s => s.Name)
        //     //     .IsRequired()
        //     //     .HasMaxLength(30);
        // }
    }
}