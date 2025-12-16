using System.Text;

namespace Notary.SSAA.BO.Utilities.Extensions
{
    public class RandomString
    {
        public static int RandomNumber(int digits)
        {
            Random rnd = new();
            string length = "";
            for (int i = 0; i < digits; i++)
            {
                length += "9";
            }
            return rnd.Next(1, int.Parse(length));
        }
        public static string RandomStr(int size, bool lowerCase)
        {
            StringBuilder builder = new();
            Random random = new();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string CreatePassword( int totalLen )
        {
            const string valid = "abcdefghijklmnpqrstuvwxyz";
            const string signValid = "?!@$";
            const string capitalAlphabet = "ABCDEFGHIJKLMNPQRSTUVWXYZ";
            const string number = "123456789";
            int Numberlength = totalLen - 4;
            int Alphalength = 2;
            StringBuilder res1 = new StringBuilder();
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            res1.Append(capitalAlphabet[rnd.Next(capitalAlphabet.Length)]);
            res1.Append(signValid[rnd.Next(signValid.Length)]);
          
            while (0 < Numberlength--)
            {
                res.Append(number[rnd.Next(number.Length)]);
                if (Alphalength>0)
                {
                    res.Append(valid[rnd.Next(valid.Length)]);
                    Alphalength--;
                }
            }

           string  shuffled = new string(
                               res.ToString()
                                   .OrderBy(character => Guid.NewGuid())
                                   .ToArray()
                               );

            return (res1.ToString() + shuffled);
        }
    }
}
