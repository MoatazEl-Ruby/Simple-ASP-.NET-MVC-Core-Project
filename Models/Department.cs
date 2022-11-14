using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Lab_4.Models
{
    public class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Courses = new HashSet<Course>();
        }

        [Key]
        public int DeptId { get; set; }
        [StringLength(20 , MinimumLength =2)]
        public string DeptName { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Course> Courses { get; set; }


    }
}
