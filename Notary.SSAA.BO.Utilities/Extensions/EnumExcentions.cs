using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Notary.SSAA.BO.Utilities.Extensions;
using Stimulsoft.Data.Extensions;

namespace Notary.SSAA.BO.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }
        public static string GetEnumDescription(this Enum value)
        {
            var type = value.GetType();
            var memInfo = type.GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
                return ((DescriptionAttribute)attributes[0]).Description;
            else
                return value.ToString();
        }
        public static EnumViewModel ToEnumViewModel(this Enum value)
        {
            Assert.NotNull(value, nameof(value));
            return new EnumViewModel(Convert.ToInt32(value), value.ToDisplay());
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            foreach (var value in Enum.GetValues(input.GetType()))
                if ((input as Enum).HasFlag(value as Enum ?? throw new InvalidOperationException()))
                    yield return (T)value;
        }

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString())?.GetValue(attribute, null);
            return propValue?.ToString();
        }

        public static string ToAssignedValue(this Enum value)
        {
            return Convert.ToInt32(value).ToString();
        }


        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Cast<Enum>().ToDictionary(p => Convert.ToInt32(p), q => q.ToDisplay());
        }

        public static List<EnumViewModel> ToDictionary<Tenum>()
        {
            return Enum.GetValues(typeof(Tenum))
                .Cast<Enum>()
                .Select(p => new EnumViewModel(Convert.ToInt32(p), p.ToDisplay()))
                .ToList();
        }
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

    public class EnumViewModel : EnumViewModel<int>
    {
        public EnumViewModel(int id, string title)
            : base(id, title)
        {
        }
    }

    public class EnumViewModel<TID>
    {
        public EnumViewModel(TID id, string title)
        {
            Id = id;
            Title = title;
        }

        public TID Id { get; }
        public string Title { get; }
    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
