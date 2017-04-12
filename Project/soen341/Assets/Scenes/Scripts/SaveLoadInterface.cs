using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace SaveLoadInterface
{
    public class SaveLoadInterface
    {
        public bool ValidateUsername(string name)
        {
            return (name.All(char.IsLetter)&&name.Length>0&& name != null);
        }

        public bool CheckPasswords(string password, string password2)
        {
            return password == password2 && password != null && password.Length > 5 && !password.Any(x => x == ' ');
        }

    }
}