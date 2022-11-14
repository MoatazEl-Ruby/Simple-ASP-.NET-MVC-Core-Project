namespace ASP.NET_Lab_4.Models
{
    public class Course
    {

        public Course()
        {
            Departments = new HashSet<Department>();
        }
        public int Crs_Id { get; set; }
        public string Crs_Name { get; set; }
        public int Crs_Hours{ get; set; }

        public ICollection<Department> Departments { get; set; }
        public ICollection<StudentsCourses> StudentsCourses { get; set; }


    }
}
