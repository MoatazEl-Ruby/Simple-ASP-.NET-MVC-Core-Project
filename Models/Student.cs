using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Lab_4.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")] // ErrorMessage 3shan azher el error le el client wa2t el submit
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Length Must Be Between 3 ~ 20 Characters")]

        public string Name { get; set; }

        [Range(10, 20, ErrorMessage = "Age Must Be Between 10 ~ 20 Years")] // b7ded el range bta3  el age 
        public int Age { get; set; }

        [RegularExpression(@"[a-zA-Z0-9_]+@[A-Za-z]+.[A-Za-z]{2,4}")] // b7ded el structure tb3 el Email be Regex 
        public string Email { get; set; }

        [Required]
        [Remote("CheckUserName", "Student", AdditionalFields = "Id", HttpMethod = "POST")]
        public string Username { get; set; }

        [DataType(DataType.Password)] // dah by3ml el password hidden , we btb2a 2bl kol password
        public string? Password { get; set; }

        [NotMapped] // msh ht3ml column le el "CPassword" fe el Database bs htzhr fe el View
        [Compare("Password", ErrorMessage = "Password Doesnt Match")] // by3ml check lwo 3la el password
        [DataType(DataType.Password)]
        public string? CPassword { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }

        public Department? Department { get; set; }
        public Student()
        {
            StudentsCourses = new HashSet<StudentsCourses>();
        }
        public ICollection<StudentsCourses>? StudentsCourses { get; set; }
    }
}
