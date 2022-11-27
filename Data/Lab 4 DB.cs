using ASP.NET_Lab_4.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ASP.NET_Lab_4.Data
{
    public class Lab_4_DB : DbContext
    {

        public DbSet<Student> Students { get; set; }    
        public DbSet<Department> Departments { get; set; }    
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentsCourses> studentsCourses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }



        public Lab_4_DB()
        {

        }


        public Lab_4_DB(DbContextOptions options) :base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-69E9690;Database=alexv5;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasKey(x => x.Crs_Id);
            modelBuilder.Entity<Course>()
                .Property(x => x.Crs_Name)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<StudentsCourses>().HasKey(x =>new { x.Crs_Id, x.Std_Id});
            modelBuilder.Entity<StudentsCourses>()
                .Property(x => x.Degree)
                .IsRequired();



            base.OnModelCreating(modelBuilder);
        }

    }
}
