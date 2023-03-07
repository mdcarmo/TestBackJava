using ExpenseManagement.Authentication.Api.Models;

namespace ExpenseManagement.Authentication.Api.Repositories
{
    public class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "EMPRESA XPTO", Password = "123", Role = "system" });
            users.Add(new User { Id = 1234, Username = "Marcelo", Password = "123", Role = "client" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}
