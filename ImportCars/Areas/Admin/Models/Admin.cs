namespace ImportCars.Areas.Admin.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Admin()
        {

        }
        public Admin(string password, string email)
        {
            Password = password;
            Email = email;
        }
    }
}
