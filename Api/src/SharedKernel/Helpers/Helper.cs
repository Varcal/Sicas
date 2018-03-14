using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SharedKernel.Helpers
{
    public static class Helper
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static string GetDescription(int value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static Dictionary<T, string> EnumToDictionary<T>()
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
                throw new ArgumentException("T must be of type System.Enum");

            return Enum.GetValues(enumType).Cast<T>().ToDictionary(k => k, v => (v as Enum).GetDescription());
        }

        


        public static int CalcularDiasEmAberto(DateTime dataInicio, DateTime dataFim)
        {
            return (dataFim - dataInicio).Days;
        }
    }
}
