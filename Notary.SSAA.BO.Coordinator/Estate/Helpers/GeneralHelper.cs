using Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem;
using System.ComponentModel;
using System.Text.Json;

namespace Notary.SSAA.BO.Coordinator.Estate.Helpers
{
    public class GeneralHelper
    {
        public static T? GetValue<T>(EntityData data, string fieldName)
        {
            var fieldValue = data.FieldValues.Where(fv => fv.FieldName.Equals(fieldName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (fieldValue != null)
            {
                if (fieldValue.Value != null)
                {
                    if (fieldValue.Value is JsonElement)
                    {
                        var jsonElement = (JsonElement)fieldValue.Value;
                        if (typeof(T) == typeof(string))
                        {
                            if (jsonElement.ValueKind == JsonValueKind.Number)
                                return ChangeType<T>(jsonElement.GetDecimal());
                            return ChangeType<T>(jsonElement.GetString());
                        }
                        if (typeof(T) == typeof(int))
                            return ChangeType<T>(jsonElement.GetInt32());
                        if (typeof(T) == typeof(int?))
                        {
                            return ChangeType<T>(jsonElement.GetInt32());
                        }
                        if (typeof(T) == typeof(long))
                            return ChangeType<T>(jsonElement.GetInt64());
                        if (typeof(T) == typeof(long?))
                        {
                            return ChangeType<T>(jsonElement.GetInt64());
                        }
                        if (typeof(T) == typeof(decimal))
                            return ChangeType<T>(jsonElement.GetDecimal());
                        if (typeof(T) == typeof(decimal?))
                        {
                            return ChangeType<T>(jsonElement.GetDecimal());
                        }
                        if (typeof(T) == typeof(double))
                            return ChangeType<T>(jsonElement.GetDouble());
                        if (typeof(T) == typeof(double?))
                        {
                            return ChangeType<T>(jsonElement.GetDouble());
                        }
                        if (typeof(T) == typeof(byte[]))
                            return ChangeType<T>(jsonElement.GetBytesFromBase64());
                        if (typeof(T) == typeof(Guid))
                        {
                            return ChangeType<T>(jsonElement.GetGuid());
                        }
                        if (typeof(T) == typeof(Guid?))
                        {
                            return ChangeType<T>(jsonElement.GetGuid());
                        }

                    }
                    else
                        return ChangeType<T>(fieldValue.Value);
                }
                return default(T);
            }
            return default(T);
        }
        public static T? ChangeType<T>(object? value)
        {
            if (value == null) return  default(T);
            var t = typeof(T);
            if (t == null) t = typeof(object);
            try
            {
                if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    if (value == null)
                    {
                        return default(T);
                    }
                    t = Nullable.GetUnderlyingType(t);
                }
                return (T?)Convert.ChangeType(value, t != null ? t : typeof(object));
            }
            catch
            {
                if (t == typeof(byte[]) && value.GetType() == typeof(string))
                {
                    try
                    {
                        var bs64 = value.ToString();
                        if(string.IsNullOrEmpty(bs64))  return default(T);
                        var ba = Convert.FromBase64String(bs64);
                        T? val = (T?)TypeDescriptor.GetConverter(t).ConvertFrom(ba);
                        return val;
                    }
                    catch
                    {
                        return default(T);
                    }
                }
                else
                {
                    T? val = (T?)TypeDescriptor.GetConverter(t != null ? t : typeof(object)).ConvertFrom(value);
                    return val;
                }
            }
        }
        
        public static string GetDatePart(string persianDateTime)
        {
            if (!string.IsNullOrWhiteSpace(persianDateTime))
            {
                if (persianDateTime.Length >= 10)
                {
                    return persianDateTime.Substring(0, 10);

                }
                else
                    return persianDateTime;
            }
            else
                return "";
        }
        public static string GetTimePart(string persianDateTime)
        {
            if (!string.IsNullOrWhiteSpace(persianDateTime))
            {
                if (persianDateTime.Length >= 10)
                {

                    if (persianDateTime.Length > 11)
                        return persianDateTime.Substring(11);
                    else
                        return "00:00:00";
                }
                else
                    return "00:00:00";
            }
            else
                return "";
        }
    }
}
