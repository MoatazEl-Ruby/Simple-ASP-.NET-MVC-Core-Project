using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace ASP.NET_Lab_4.Models
{
    public class User
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [Range(10,50,ErrorMessage ="Age Must be between 10 ~ 50 Years")]
        public int Age { get; set; }


        [Required(ErrorMessage = "*")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Length Must Be Between 3 ~ 20 Characters")]
        public string Name { get; set; }

        [Required]
        [Remote("CheckUserName", "Student", AdditionalFields = "Id", HttpMethod = "POST")]
        public string UserName { get; set; }




        [RegularExpression(@"[a-zA-Z0-9_]+@[A-Za-z]+.[A-Za-z]{2,4}", ErrorMessage = "Email must be in this formart `Example@gmail.com`")] // b7ded el structure tb3 el Email be Regex 
        public string Email { get; set; }



        [DataType(DataType.Password)]
        public string Password { get; set; }



        [NotMapped] // msh ht3ml column le el "CPassword" fe el Database bs htzhr fe el View
        [Compare("Password", ErrorMessage = "Password Doesnt Match")] // by3ml check lwo 3la el password
        [DataType(DataType.Password)]
        public string CPassword { get; set; }

        public void AddRoleToUser(Role role)
        {
            Roles.Add(role);
        }

        public User()
        {
            Roles = new HashSet<Role>();
        }


        public ICollection<Role> Roles { get; set; }


    }
}
