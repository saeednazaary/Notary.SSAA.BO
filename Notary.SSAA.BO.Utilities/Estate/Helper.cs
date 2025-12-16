using Notary.SSAA.BO.Utilities.Extensions;
using System.Collections;

namespace Notary.SSAA.BO.Utilities.Estate
{
    public class Helper
    {
        public static void NormalizeStringValues(object entity, bool persionToArabic = true)
        {
            Type type = entity.GetType();
            System.Reflection.PropertyInfo[] propertiesList = type.GetProperties();
            foreach (System.Reflection.PropertyInfo property in propertiesList)
            {
                object value = property.GetValue(entity);
                if (value != null && property.PropertyType == typeof(string))
                {
                    string normalizeValue = value.ToString().NormalizeTextChars(persionToArabic).Trim();
                    property.SetValue(entity, normalizeValue);
                }
            }
        }

        public static void NormalizeStringValuesDeeply(object entity, bool persianToArabic = true)
        {
            Type type = entity.GetType();
            System.Reflection.PropertyInfo[] propertiesList = type.GetProperties();
            foreach (System.Reflection.PropertyInfo property in propertiesList)
            {
                object value = property.GetValue(entity);
                if (value != null && property.PropertyType == typeof(string))
                {
                    string normalizeValue = value.ToString().NormalizeTextChars(persianToArabic).Trim();
                    property.SetValue(entity, normalizeValue);
                }
                else
                {
                    if (value is IEnumerable)
                    {
                        IEnumerable List = GetList((IEnumerable)value);
                        foreach (object item in List)
                        {
                            NormalizeStringValuesDeeply(item, persianToArabic);
                        }
                    }
                }
            }

        }
        private static IEnumerable GetList(IEnumerable entityList)
        {
            if (!IsNullOrEmptyList(entityList))
            {
                Type elementType = GetListElementsType(entityList);

                if (elementType == null)
                {
                    return entityList;
                }

                Type genericListType = typeof(List<>).MakeGenericType(elementType);
                IList listToBeSorted = (IList)Activator.CreateInstance(genericListType);

                foreach (object element in entityList)
                {
                    listToBeSorted.Add(element);
                }
                return listToBeSorted;
            }
            else
            {
                return entityList;
            }
        }
        private static bool IsNullOrEmptyList(IEnumerable list)
        {
            if (list == null)
            {
                return true;
            }

            foreach (object element in list)
            {
                return false;
            }
            return true;
        }
        private static Type GetListElementsType(IEnumerable list)
        {
            if (list == null)
            {
                return null;
            }

            foreach (object element in list)
            {
                return element.GetType();
            }
            return null;


        }

        public static decimal[] Sum(decimal s1, decimal s2, decimal m1, decimal m2)
        {
            decimal[] arr = new decimal[2];

            if (m1 == 0 && s1 == 0)
            {
                arr[0] = s2;
                arr[1] = m2;
                return arr;
            }

            if (m2 == 0 && s2 == 0)
            {
                arr[0] = s1;
                arr[1] = m1;
                return arr;
            }

            decimal bmm = 0, mm = 0,
                   leastM, greatestM, i;

            greatestM = m1 > m2 ? m1 : m2;
            leastM = m1 > m2 ? m2 : m1;

            if (greatestM % leastM == 0)
            {
                mm = greatestM;
                s1 *= mm / m1;
                s2 *= mm / m2;
            }
            else
            {
                for (i = leastM / 2; i > 0; i--)
                {
                    if (greatestM % i == 0 && leastM % i == 0)
                    {
                        bmm = i;
                        mm = m1 * m2 / bmm;
                        s1 *= mm / m1;
                        s2 *= mm / m2;
                        break;
                    }
                }
            }


            arr[0] = s1 + s2;
            arr[1] = mm;
            return arr;

        }

        public static List<string> RemoveDuplicateMessages(List<string> messages)
        {
            List<string> result = [];
            List<string> tmpLst = [];
            System.Text.RegularExpressions.Regex regularExpr = new(@"\s");
            foreach (string message in messages)
            {
                string str = regularExpr.Replace(message.NormalizeTextChars(), "");
                if (!tmpLst.Contains(str))
                {
                    tmpLst.Add(str);
                    result.Add(message);
                }
            }
            return result;
        }

    }
    public class RationalNumber
    {

        public RationalNumber()
        {
        }

        public RationalNumber(decimal S, decimal M)
        {
            this.M = M;
            this.S = S;
        }

        public decimal S { get; set; }
        public decimal M { get; set; }

    }
}
