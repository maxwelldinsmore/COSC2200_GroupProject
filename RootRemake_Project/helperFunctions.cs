using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootRemake_Project
{
    public static class helperFunctions
    {
        /// <summary>
        /// Gets string of 10 random characters, used for adding in images and labels
        /// to the game board
        /// </summary>
        public static string GetRandomString()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, 10)
                              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
