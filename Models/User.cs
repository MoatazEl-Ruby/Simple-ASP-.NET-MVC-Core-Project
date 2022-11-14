using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Lab_4.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Range(10,50)]
        public int Age { get; set; }
        
        public string Name { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public User()
        {
            Roles=new HashSet<Role>();
        }

        public ICollection<Role> Roles { get; set; }
    }
}
