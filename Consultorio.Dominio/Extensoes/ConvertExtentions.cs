using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Consultorio.Dominio.Extensoes
{
    /// <summary>
    /// Classe estática para tratamento de conversões
    /// </summary>
    public static class ConvertExtentions
    {
        //Comparison
        public static bool IsZeroOrNull(this object pObject)
        {
            return pObject == null || pObject == (object)0 || pObject.ToString() == "0.0";
        }

        /// <summary>
        /// Remove os carcteres 
        /// </summary>
        /// <param name="pValue">Valor</param>
        /// <param name="pCharacters">Carcteres a serem substituido</param>
        /// <param name="pNewCharacter">Novo caractere</param>
        /// <returns></returns>
        public static string RemoveCharacter(this string pValue, string pCharacters, string pNewCharacter)
        {
            return pCharacters.ToCharArray().Aggregate(pValue, (current, item)
                => current.Replace(item.ToString(), pNewCharacter));
        }


        /// <summary>
        /// Remover acentos da strins
        /// </summary>
        /// <param name="pValue">String com Acentos</param>
        /// <returns>Uma string sem accentos</returns>
        public static string NoAccents(this string pValue)
        {
            if (string.IsNullOrEmpty(pValue))
                return pValue;

            var normalizedString = pValue.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (var i = 0; i < normalizedString.Length; i++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);

                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(normalizedString[i]);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }



        /// <summary>
#pragma warning disable 1570
        /// Converter caracteres de simbols (&) para (&amp) < > " ' para códigos HMTLs
#pragma warning restore 1570
        /// </summary>
        /// <param name="pValue">String com Acentos</param>
        /// <param name="pKeepSpecialCharacters">Bool. Caso seja true retornar o próprio texto, sem alterações.</param>
        /// <returns>Uma string sem accentos</returns>
        public static string ToParserSimbol(this string pValue, bool pKeepSpecialCharacters = false)
        {
            if (string.IsNullOrEmpty(pValue))
                return pValue;

            if (pKeepSpecialCharacters)
                return pValue;

            var dic = new Dictionary<char, string>(){
                { '<' , "&alt;" },
                { '>' , "&gt;" },
                { '&' , "&amp;" },
                { '"' , "&quot;" },
                {  "'"[0] , "&#39" }
            };

            foreach (var item in dic)
                pValue = pValue.Replace(item.Key.ToString(), item.Value);

            return pValue;
        }



        /// <summary>
        /// Verifica se uma string é numérica
        /// </summary>
        /// <param name="pStr">String</param>
        /// <returns>True de for numérica, false se não for</returns>
        public static bool IsNumeric(this string pStr)
        {
            const string strRegex = @"^[0-9]*[,][0-9]*$";
            return System.Text.RegularExpressions.Regex.IsMatch(pStr, strRegex);
        }

        /// <summary>
        /// Converte um valor de forma segura
        /// </summary>
        /// <typeparam name="T">Converter para tipo</typeparam>
        /// <param name="value">valor para conversão</param>
        /// <returns></returns>
        public static T To<T>(this object value)
        {
            var conversionType = typeof(T);
            return (T)value.To(conversionType);
        }

        public static object To(this object value, Type conversionType)
        {
            if (conversionType == null)
                throw new ArgumentNullException("conversionType");

            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || Convert.IsDBNull(value))
                    return null;

                var nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            else if (conversionType == typeof(Guid))
            {
                return new Guid(value.ToString());

            }

            if ((value is string || value == null || value is DBNull) &&
                (conversionType == typeof(short) ||
                conversionType == typeof(int) ||
                conversionType == typeof(long) ||
                conversionType == typeof(double) ||
                conversionType == typeof(decimal) ||
                conversionType == typeof(float)))
            {
                if (!decimal.TryParse(value as string, out _))
                    value = "0";
            }

            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// Converte um dataTable para um dicionário
        /// </summary>
        /// <param name="pDataTable">Data Table</param>
        /// <param name="pKey">Nome do Campo de Chave</param>
        /// <param name="pValue">Nome do Campo de Valor</param>
        /// <returns></returns>
        public static Dictionary<object, string> ToDictionary(
            this DataTable pDataTable, string pKey, params string[] pValue)
        {
            var dic = new Dictionary<object, string>();

            foreach (DataRow row in pDataTable.Rows)
            {
                var value = string.Empty;

                foreach (var item in pValue)
                    value = string.Format("{0}{1}{2}", value,
                        string.IsNullOrEmpty(value) ? string.Empty : "-",
                        row.Field<string>(item));

                if (!dic.ContainsKey(row[pKey]))
                    dic.Add(row[pKey], value);
            }

            return dic;
        }

        /// <summary>
        /// Verifica se uma coleção está vazia.
        /// </summary>
        /// <typeparam name="T">O Tipo da coleção.</typeparam>
        /// <param name="source">A coleção.</param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null || source.Count() == 0)
                return true;

            return !source.Any();
        }

        /// <summary>
        /// Salva um objeto com um binário no disco
        /// </summary>
        /// <param name="path">Caminho do Arquivo</param>
        /// <param name="obj">Objeto</param>
        public static void SaveToBinary(this object obj, string path)
        {
            using (Stream textWriter = File.Open(path, FileMode.Create))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(textWriter, obj);
            }
        }



        /// <summary>
        /// Carrega um objeto através do arquivo
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="path">Caminho do Arquivo</param>
        /// <returns>Um bjeto preenchido</returns>
        public static T LoadFromBinary<T>(string path) where T : class
        {
            T serializableObject;
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter bFormatter = new BinaryFormatter();
                serializableObject = bFormatter.Deserialize(stream) as T;
            }
            return serializableObject;
        }

        //public static decimal ConvertTo2Decimal(this ulong value)
        //{
        //    if (string.IsNullOrEmpty(value.ToString().Trim()))
        //        return decimal.Zero;

        //    var conversorDeDecimal = new Convert2Decimal().StringToField(value.ToString().Trim());
        //    return Convert.ToDecimal(conversorDeDecimal);
        //}
        //public static decimal ConvertTo2Decimal(this long value)
        //{
        //    if (string.IsNullOrEmpty(value.ToString().Trim()))
        //        return decimal.Zero;

        //    var conversorDeDecimal = new Convert2Decimal().StringToField(value.ToString().Trim());
        //    return Convert.ToDecimal(conversorDeDecimal);
        //}
        //public static decimal ConvertTo2Decimal(this short value)
        //{
        //    if (string.IsNullOrEmpty(value.ToString().Trim()))
        //        return decimal.Zero;

        //    var conversorDeDecimal = new Convert2Decimal().StringToField(value.ToString().Trim());
        //    return Convert.ToDecimal(conversorDeDecimal);
        //}

    }
}
