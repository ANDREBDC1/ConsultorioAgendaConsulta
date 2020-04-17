using System;

namespace Consultorio.Dominio.Extensoes
{
    public static class DateTimeExtentions
    {
        /// <summary>
        /// Converte um data para Zero hora ( 01/01/2011 : 14:13 para 01/01/2011 00:00 )
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns>Data comnvertida</returns>
        public static DateTime FirstHour(this DateTime pDateTime)
        {
            return new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day, 0, 0, 0);
        }

        /// <summary>
        /// Converte uma data para a útlima hora do dia ( 01/01/2011 : 14:13 para 01/01/2011 24:59 )
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns>A Data comnvertida</returns>
        public static DateTime LastHour(this DateTime pDateTime)
        {
            return new DateTime(pDateTime.Year, pDateTime.Month, pDateTime.Day, 23, 59, 59);
        }

        /// <summary>
        /// Converte um data para Zero hora ( 01/01/2011 : 14:13 para 01/01/2011 00:00 )
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns>Data comnvertida</returns>
        public static DateTime FirstDayOfYear(this DateTime pDateTime)
        {
            return new DateTime(pDateTime.Year, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Converte uma data para a útlima hora do dia ( 01/01/2011 : 14:13 para 01/01/2011 24:59 )
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns>A Data comnvertida</returns>
        public static DateTime LastDayOfYear(this DateTime pDateTime)
        {
            return new DateTime(pDateTime.Year, 12, 31, 23, 59, 59);
        }

        /// <summary>
        /// Converte um data para Zero hora ( 01/01/2011 : 14:13 para 01/01/2011 00:00 )
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns>Data comnvertida</returns>
        public static DateTime FirstDayOfMonth(this DateTime pDateTime)
        {
            return new DateTime(pDateTime.Year, pDateTime.Month, 1);
        }

        /// <summary>
        /// Converte uma data para a útlima hora do dia ( 01/01/2011 : 14:13 para 01/01/2011 24:59 )
        /// </summary>
        /// <param name="pDateTime"></param>
        /// <returns>A Data comnvertida</returns>
        public static DateTime LastDayOfMonth(this DateTime pDateTime)
        {
            return new DateTime(pDateTime.Year, pDateTime.Month, 1)
                .AddMonths(1)
                .AddDays(-1)
                .LastHour();
        }
    }
}
