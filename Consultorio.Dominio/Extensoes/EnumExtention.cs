using System;
using System.ComponentModel;

namespace Consultorio.Dominio.Extensoes
{
    public static class EnumExtention
    {
        public static T ParseEnum<T>(this string pValue, string pTag, string pTableName, string pField)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), pValue);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"O Valor [{pValue}] não é valido para a tag [{pTag}] do campo [{pField}].", ex);
            }
        }

        public static T ParseEnumItem<T>(this object pValue, string pTag, string pTableName, string pField)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), $"Item{pValue}");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"O Valor [{pValue}] não é valido para a tag [{pTag}] do campo [[{pTableName}].[{pField}]].", ex);
            }
        }

        public static T ParseEnum<T>(this string pValue, string pTag)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), pValue);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"O Valor [{pValue}] não é valido para a tag [{pTag}].", ex);
            }
        }

        //public static T ParseEnumItem<T>(this short? pValue, string pTag, string format = null)
        //{
        //    return pValue.HasValue
        //        ? pValue.Value.ParseToString(format).ParseEnumItem<T>(pTag)
        //        : default;
        //}

        public static T ParseEnumItem<T>(this object pValue, string pTag)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), $"Item{pValue}");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"O Valor [{pValue}] não é valido para a tag [{pTag}].", ex);
            }
        }

        public static string ParseEnumToString(this Enum pValue)
        {
            return pValue.ToString().Replace("Item", "");
        }


        public static T ParseEnumTo<T>(this Enum pValue)
        {
            return pValue.ToString().Replace("Item", "").To<T>();
        }

        public static string GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
