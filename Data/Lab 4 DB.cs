using ASP.NET_Lab_4.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace ASP.NET_Lab_4.Data
{
    public class Lab_4_DB : DbContext
    {

        public DbSet<Student> Students { get; set; }    
        public DbSet<Department> Departments { get; set; }    
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentsCourses> StudentsCourses { get; set; }
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
            optionsBuilder.UseSqlServer("Server=db,1433;Database=alexv5;User Id=sa;Password=Dockerpassword123!");
            base.OnConfiguring(optionsBuilder); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasKey(x => x.Crs_Id);
            modelBuilder.Entity<Course>()
                .Property(x => x.Crs_Name)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<StudentsCourses>().HasKey(x => new { x.Crs_Id, x.Std_Id });
            modelBuilder.Entity<StudentsCourses>()
                .Property(x => x.Degree)
                .IsRequired();



            base.OnModelCreating(modelBuilder);

            var role1 = new Role
            {
                Id = 1,
                RoleName = "Admin",
            };


            var role2 = new Role
            {
                Id = 2,
                RoleName = "Instructor"
            };

            var role3 = new Role
            {
                Id = 3,
                RoleName = "Student"
            };


            modelBuilder.Entity<Role>().HasData(
                role1,
                role2,
                role3);



            var adminUser = new User
            {
                Id = 1,
                Name = "admin",
                UserName = "admin",
                Password = "123456",
                Age = 27
            };

            modelBuilder.Entity<User>().HasData(
                  adminUser);

            modelBuilder.Entity("RoleUser").HasData(
                new { RolesId = 1, UsersId = 1 });


        }

    }
}
