using System.Security.Cryptography;
using System.Text;

namespace StPierre.manager
{
    /// <summary>
    /// Allow to manage a view which is doing modification to a user
    /// </summary>
    class UserManager
    {
        /// <summary>
        /// Hash a string with Sha1
        /// </summary>
        /// <param name="input"></param>
        /// <returns>The hashed input</returns>
        public string Hash(string input)
        {
            SHA1Managed sha1 = new SHA1Managed();
            StringBuilder sb = null;

            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));

            sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                sb.AppendFormat("{0:x2}", b);
            }

            return sb.ToString();
        }
    }
}
