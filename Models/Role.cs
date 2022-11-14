namespace ASP.NET_Lab_4.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName  { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }

        public ICollection<User> Users { get; set; }

    }
}
