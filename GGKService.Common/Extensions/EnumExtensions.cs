using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace GGKService.Common.Extensions
{

	/// <summary>
	/// Расширения для Enum'ов
	/// </summary>
	public static class EnumExtensions{

		/// <summary>
		/// Получение аттрибута с Enum
		/// </summary>
		/// <typeparam name="TAttribute"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute{
			var type = value.GetType();
			var name = Enum.GetName(type, value);
			return type.GetField(name) // I prefer to get attributes this way
				.GetCustomAttributes(false)
				.OfType<TAttribute>()
				.SingleOrDefault();
		}

		public static string GetEnumDescription(Enum value){
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0)
				return attributes[0].Description;
			else
				return value.ToString();
		}
	}
}
