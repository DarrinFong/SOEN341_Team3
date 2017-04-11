using System.Linq;
namespace SaveLoadExtension
{
    public class SaveLoadExtension
    {

        public bool ValidateUsername(string name)
        {
            return name.All(char.IsLetter);
        }

        public bool CheckPasswords(string password, string password2)
        {
            return password == password2 && password.Length > 5 && !password.Any(x => x == ' ');
        }

    }
}