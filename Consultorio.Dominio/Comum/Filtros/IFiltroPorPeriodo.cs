using System;

namespace Consultorio.Dominio.Comum.Filtros
{
    public interface IFiltroPorPeriodo
    {
        DateTime? DataDeInicio { get; set; }
        DateTime? DataDeTermino { get; set; }
    }
}
