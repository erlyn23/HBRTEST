using System;

namespace HBRTEST.Utilities
{
    public class PasswordEncrypt
    {
        public static string Encrypt(string Password)
        {
            try
            {
                if (!string.IsNullOrEmpty(Password))
                {
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(Password);
                    data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                    string hash = System.Text.Encoding.ASCII.GetString(data);
                    return hash;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new ArgumentNullException();
            }
        }
    }
}
