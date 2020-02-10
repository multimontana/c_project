namespace InfraManager.WebApi.Auth.Services
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using InfraManager.WebApi.Auth.Model;

    public class UserManagementService : IUserManagementService
    {
        public bool IsValidUser(string login, byte[] password, AuthenticateModel requestModel)
        {
            if (!string.IsNullOrEmpty(login) && password != null)
            {
                // Check password
                if (this.IsValidPassword(password, requestModel.Password))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Compare passwords
        /// </summary>
        /// <param name="password"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool IsValidPassword(byte[] password, string source)
        {
            byte[] hash;

            // Get byte of password on hash value
            Split(password, 5, 12, out hash);

            // Create a new instance of the MD5CryptoServiceProvider object.
            var md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            var hashData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));

            if (hash.SequenceEqual(hashData))
            {
                return true; // "The hashes are the same.";
            }
            else
            {
                return false; // "The hashes are not same.";
            }

            return true;
        }

        /// <summary>
        /// Get original byte of password
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="firstIndex"></param>
        /// <param name="lastIndex"></param>
        /// <param name="first"></param>
        public static void Split<T>(T[] array, int firstIndex, int lastIndex, out T[] first)
        {
            first = array.Skip(firstIndex).ToArray();
            first = first.Take(first.Length - (lastIndex - 1)).ToArray();
        }

        /// <summary>
        /// Hash an string and return the hash as a 32 character hexadecimal string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}