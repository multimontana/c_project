using System;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Services
{
    public static class CalculatePasswordService
    {
        public static byte[] CalculatePassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            //Logger<>.Trace("AuthenticationHelper.CalculatePassword");

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordHash = new MD5CryptoServiceProvider().ComputeHash(passwordBytes);
            var guidBytes = Guid.NewGuid().ToByteArray();

            var retVal = new Byte[passwordHash.Length + guidBytes.Length];
            Array.Copy(guidBytes, 0, retVal, 0, 5);
            Array.Copy(passwordHash, 0, retVal, 5, passwordHash.Length);
            Array.Copy(guidBytes, 5, retVal, passwordHash.Length + 5, 11);

            return retVal;
        }

    }
}
