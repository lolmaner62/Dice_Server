using Microsoft.AspNetCore.Components.Forms;
using System.Text;

namespace Dice_Server
{
    static public class Rng
    {
        static StringBuilder sb;
        static string chars;
        static Random rnd;
        static Rng()
        {
            sb = new StringBuilder();
            chars = "abcefghijklmnopqrstuvwxyz0123456789";
            rnd = new Random();
            
        }
        public static async Task<string> GenerateSessionID()
        {
            
            return await Task<string>.Run(() =>
            {
                for (int i = 0; i < 6; i++)
                {
                    char c = chars[rnd.Next(0, chars.Length)];
                    sb.Append(c);
                }
                string output = sb.ToString();
                sb.Clear();
                return output;
            });
        }
    }
}
