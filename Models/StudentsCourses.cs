using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Lab_4.Models
{
    public class StudentsCourses
    {
        [ForeignKey ("Course")]
        public int Crs_Id{ get; set; }


        [ForeignKey ("Student")]
        public int Std_Id { get; set; }



        public int Degree { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
