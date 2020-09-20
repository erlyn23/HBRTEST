using System;
using HBRTEST.ErrorHandling;

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
                    byte[] passwordBytes = System.Text.Encoding.ASCII.GetBytes(Password);
                    passwordBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordBytes);
                    string passwordBytesEncryptedString = System.Text.Encoding.ASCII.GetString(passwordBytes);
                    return passwordBytesEncryptedString;
                }
                else
                {
                    throw new PersonalizedException("La constraseña no debe estar vacía");
                }
            }
            catch(Exception exception)
            {
                throw new PersonalizedException(exception.Message);
            }
        }
    }
}
