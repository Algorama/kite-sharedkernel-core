using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SharedKernel.Domain.Dtos;

namespace SharedKernel.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(string enumValue)
        {
            var enumType = typeof(T);
            if (!enumType.IsEnum) throw new InvalidOperationException();

            var descriptionAttribute = enumType
                .GetField(enumValue)
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute != null ? descriptionAttribute.Description : enumValue;
        }

        public static IList<EnumItem> GetList<T>()
        {
            var type = typeof(T);
            var data = Enum.GetNames(type).Select(name => new EnumItem
                {
                    Id = (int)Enum.Parse(type, name),
                    Name = name,
                    Description = GetDescription<T>(name)
                }
            ).ToList();
            return data;
        }
    }
}
