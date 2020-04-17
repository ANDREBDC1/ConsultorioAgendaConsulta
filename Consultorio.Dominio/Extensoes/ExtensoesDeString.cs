using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Consultorio.Dominio.Extensoes
{
    public static class ExtensoesDeString
    {

        public static List<string> ListaDeStringsPorGroupDe(this string value, int length)
        {
            if (string.IsNullOrEmpty(value))
                return new List<string>();

            var agrupamentos = new List<string>();
            var ultimoValor = value;
            var ehPossivelAgrupar = value.Length % length == 0;

            if (!ehPossivelAgrupar)
                throw new Exception("Não é possível realizar agrupamentos da string");

            while (!string.IsNullOrWhiteSpace(ultimoValor))
            {
                var novoAgrupamento = ultimoValor.Substring(0, length);
                agrupamentos.Add(novoAgrupamento);
                ultimoValor = ultimoValor.Remove(0, length);
            }

            return agrupamentos;
        }

        public static string SubStringSeguro(this string value, int startIndex, int length)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Length <= startIndex + length
                ? value.Substring(startIndex, length)
                : value.Substring(startIndex);
        }

        public static string SubstituaCaracteres(this string pValue, string pCharacters, string pNewCharacter)
        {
            if (string.IsNullOrEmpty(pValue))
                return pValue;

            return pCharacters.ToCharArray().Aggregate(pValue, (current, item)
                => current.Replace(item.ToString(), pNewCharacter));
        }

        public static string RemovaAcentos(this string pValue)
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
        public static bool IsNumeric(this string stringValue)
        {
            const string STR_REGEX = @"^[0-9]*[,][0-9]*$";
            return Regex.IsMatch(stringValue, STR_REGEX);
        }
        public static bool EhSomenteNumeros(this string stringValue)
        {
            const string STR_REGEX = @"^[0-9]*$";
            return stringValue != null && Regex.IsMatch(stringValue, STR_REGEX);
        }

        public static string RemovaSimbolos(this string stringValue)
        {
            return stringValue.SubstituaCaracteres(@"-.,/\(){}[]”-%$&-.;:", "");
        }

        public static string RemovaEspacos(this string stringValue)
        {
            return stringValue.SubstituaCaracteres(@" ", "");
        }

        public static string EmMaisculo(this string stringValue)
        {
            return string.IsNullOrWhiteSpace(stringValue) ? stringValue : stringValue.ToUpper();
        }

        public static string ComoCnpj(this string stringCnpj)
        {
            var vLongCnpj = stringCnpj?.To<long>();
            return $@"{vLongCnpj:00\.000\.000\/0000\-00}";
        }

        public static string ComoCpf(this string stringCpf)
        {
            var vLongCpf = stringCpf?.To<long>();
            return $@"{vLongCpf:000\.000\.000\-00}";
        }

        //public static decimal ConvertTo2Decimal(this string value)
        //{
        //    if (string.IsNullOrEmpty(value?.Trim()))
        //        return decimal.Zero;

        //    var conversorDeDecimal = new Convert2Decimal().StringToField(value);
        //    return Convert.ToDecimal(conversorDeDecimal);
        //}




        //public static decimal ConvertTo4Decimal(this string value)
        //{
        //    if (string.IsNullOrEmpty(value?.Trim()))
        //        return decimal.Zero;
        //    var conversorDeDecimal = new Convert4Decimal().StringToField(value);
        //    return Convert.ToDecimal(conversorDeDecimal);
        //}
        public static DateTime ConverteStringParaData(this string dataString)
        {
            return string.IsNullOrWhiteSpace(dataString) || dataString.Equals("00000000")
                ? DateTime.MinValue
                : DateTime.ParseExact(dataString, "ddMMyyyy", CultureInfo.CurrentUICulture);
        }
        public static DateTime ConverteStringParaData(this string dataString, string format)
        {
            return string.IsNullOrWhiteSpace(dataString) || dataString.Equals("00000000") || dataString.Equals("000000")
                ? DateTime.MinValue
                : DateTime.ParseExact(dataString, format, CultureInfo.CurrentUICulture);
        }
        public static decimal ArredondaPara2Decimal(this decimal value)
        {
            return decimal.Round(value, 2);
        }
        public static string SomenteNumeros(this string value)
        {
            return Regex.Match(value, "\\d+").Value;
        }

        public static string MaxLength(this string text, int maxLength)
        {
            var newText = text.Length > maxLength ? text.Substring(0, maxLength) : text;

            return newText;
        }

        public static string AddZerosTheLeft(this string text, int length)
        {
            text = text ?? string.Empty;

            var zeros = string.Empty;
            var tamanho = length - text.Length;

            for (var i = 0; i < tamanho; i++)
            {
                zeros += "0";
            }

            return zeros + text;
        }

        public static string AddSpaceWhite(this string text, int length)
        {
            text = text ?? string.Empty;

            var spaceWhite = string.Empty;
            var tamanho = length - text.Length;

            for (var i = 0; i < tamanho; i++)
            {
                spaceWhite += " ";
            }

            return text + spaceWhite;
        }

        /// <summary>
        /// Insere um valor na posição da string de forma destrutiva, ou seja irá sobrepor o valor
        /// </summary>
        /// <param name="texto">Linha</param>
        /// <param name="posStart">Posição Inicial</param>
        /// <param name="posEnd">Posição Final</param>
        /// <param name="value">Valor</param>
        /// <returns>A string Alterada</returns>
        public static string InsertWithRemove(this string texto, int posStart, int posEnd, string value)
        {
            var len = posEnd - posStart + 1;
            posStart--;

            if (posStart < 0)
                posStart = 0;

            if (posStart + len > texto.Length)
                throw new ArgumentException(@"Posição final da alteração maior que o tamanho da string", nameof(posEnd));

            if (len != value.Length)
                throw new ArgumentException(@"Tamanho do novo valor, não corresponde ao tamanho do informado nas posições", nameof(posEnd));

            if (texto == null)
                throw new ArgumentException(@"Linha não pode ser nula", nameof(texto));

            if (value == null)
                throw new ArgumentException(@"Novo valor não pode ser nula", nameof(value));

#if DEBUG
            System.Diagnostics.Debug.Print("De   : {0} \nPara : {1}", texto.Substring(posStart, len), value);
#endif

            return texto.Insert(posStart, value).Remove(posStart + len, len);
        }



    }
}
