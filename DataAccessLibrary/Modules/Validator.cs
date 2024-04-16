using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Modules
{
    public static class Validator
    {
        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            bool hasNumber = false;
            bool hasLetter = false;

            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    hasNumber = true;
                }
                else if (char.IsLetter(c))
                {
                    hasLetter = true;
                }

                if (hasNumber && hasLetter)
                {
                    break;
                }
            }

            return hasNumber && hasLetter && password.Length >= 6;
        }
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static bool ValidateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return false;
            }

            // Usernames must be at least 3 characters long
            if (username.Length < 3)
            {
                return false;
            }

            // Usernames can only contain letters, numbers, underscores, and hyphens
            foreach (char c in username)
            {
                if (!char.IsLetterOrDigit(c) && c != '_' && c != '-')
                {
                    return false;
                }
            }

            // Usernames cannot start or end with a hyphen
            if (username[0] == '-' || username[username.Length - 1] == '-')
            {
                return false;
            }

            // Usernames cannot contain two consecutive hyphens
            for (int i = 0; i < username.Length - 1; i++)
            {
                if (username[i] == '-' && username[i + 1] == '-')
                {
                    return false;
                }
            }

            return true;
        }
        public static bool ComparePasswords(string password, string confirmpassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmpassword))
            {
                return false;
            }

            return password.Equals(confirmpassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
