using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace inventory
{
    public static class Db
    {
        public static string ConnString =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            Path.Combine(Application.StartupPath, "userdb.accdb");

        // Hindi sine-save ang password nang plain text
        public static string Hash(string text)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}