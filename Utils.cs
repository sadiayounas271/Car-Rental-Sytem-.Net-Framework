using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace RentARide
{
   public static class Utils
    {
        public static bool FormIsOpen(string name)
        {
            var openForms = Application.OpenForms.Cast<Form>();
            var isOpen= openForms.Any(x => x.Name == name);
            return isOpen;
        }

        public static string HashPassword(string password)
        {
            SHA256 sha = SHA256.Create();
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            //Create a Stringbuilder to collect the bytes
            //and create a string
            StringBuilder stringBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            var hashed_Password = stringBuilder.ToString();
            return hashed_Password;
        }
    }
}
