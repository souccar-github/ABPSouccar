﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Project.Souccar.Core.CustomAttribute;

namespace Project.Souccar.Core.Extensions
{
    /// <summary>
    /// Provides a static utility object of methods and properties to interact
    /// with enumerated types.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the <see cref="DescriptionAttribute" /> of an <see cref="Enum" />
        /// type value.
        /// </summary>
        /// <param name="value">The <see cref="Enum" /> type value.</param>
        /// <returns>A string containing the text of the
        /// <see cref="DescriptionAttribute"/>.</returns>
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            if (fieldInfo != null)
            {
                var attributes =
                    (LocalizationDisplayNameAttribute[])
                    fieldInfo.GetCustomAttributes(typeof (LocalizationDisplayNameAttribute), false);

                if (attributes.Length > 0)
                {
                    description = attributes[0].DisplayName;
                }
            }
            return description;
        }

        /// <summary>
        /// Converts the <see cref="Enum" /> type to an <see cref="IList" /> 
        /// compatible object.
        /// </summary>
        /// <param name="type">The <see cref="Enum"/> type.</param>
        /// <returns>An <see cref="IList"/> containing the enumerated
        /// type value and description.</returns>
        public static IList ToList(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }
    }
}
