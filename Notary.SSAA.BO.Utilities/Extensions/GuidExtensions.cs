using System;


namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class GuidExtensions
    {
        public static byte[] ToOracleRaw16(this Guid guid)
        {
            return new Guid(BitConverter.ToString(guid.ToByteArray()).Replace("-", "")).ToByteArray();
        }
        public static Guid IsValidId(this Guid value, string exMessage)
        {
            if (value == Guid.Empty)
                throw new ArgumentNullException(exMessage);
            return value;
        }
        public static Guid FlipEndian(this Guid guid)
        {
            var newBytes = new byte[16];
            var oldBytes = guid.ToByteArray();

            for (var i = 8; i < 16; i++)
                newBytes[i] = oldBytes[i];

            newBytes[3] = oldBytes[0];
            newBytes[2] = oldBytes[1];
            newBytes[1] = oldBytes[2];
            newBytes[0] = oldBytes[3];
            newBytes[5] = oldBytes[4];
            newBytes[4] = oldBytes[5];
            newBytes[6] = oldBytes[7];
            newBytes[7] = oldBytes[6];

            return new Guid(newBytes);
        }

        public static List<string> MapToStringArray(this Guid? id)
        {
            if (id==null)
            {
                return new List<string>();
            }
            else
            {
                return new List<string> { id.ToString() };
            }

        }
        public static List<string> MapToStringArray ( this Guid id )
        {
            {
                return new List<string> { id.ToString () };
            }

        }
        public static List<string> MapToStringArray ( this string id )
        {
            {
                return new List<string> { id };
            }

        }
    }
}