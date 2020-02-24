using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Odh.BooksDemo.Entities
{
    public static class EnumDescriptionExtension
    {
        public static string GetEnumDescription(Type enumType, string enumValue)
        {

            var memInfo = enumType.GetMember(enumValue);

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumValue;
        }


        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetDescription(this Enum enumValue)
        {
            string output = null;
            Type type = enumValue.GetType();
            FieldInfo fi = type.GetField(enumValue.ToString());
            if (fi == null) return string.Empty;
            var attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            output = (attrs != null && attrs.Length > 0) ? attrs[0].Description : enumValue.ToString();
            return output;
        }


        public static IDictionary<int, string> GetEnumValuesWithDescription<T>(this Type type) where T : struct, IConvertible
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return type.GetEnumValues()
                    .OfType<T>()
                    .ToDictionary(
                        key => Convert.ToInt32(key as Enum),
                        val => (val as Enum).GetDescription()
                    );
        }


        public static IDictionary<T, string> GetEnumsWithDescription<T>(this Type type) where T : struct, IConvertible
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return type.GetEnumValues()
                    .OfType<T>()
                    .ToDictionary(
                        key => key,
                        val => (val as Enum).GetDescription()
                    );
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute == null)
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }
            throw new ArgumentException("Not found.", "description");
        }
    }
}
