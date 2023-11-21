using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Encryption
{
    public static class MD5Operations
    {
        public static async Task<string> CreateMD5(string content)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(content);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static async Task<bool> VerifyMD5(string content, string expectedHash)
        {
            string computedHash = await CreateMD5(content);
            return string.Equals(computedHash, expectedHash);
        }
    }
}
