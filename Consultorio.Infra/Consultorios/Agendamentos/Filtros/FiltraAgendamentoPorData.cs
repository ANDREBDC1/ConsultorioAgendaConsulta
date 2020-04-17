using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.Infra.Consultorios.Agendamentos.Filtros
{
    public class FiltraAgendamentoPorData : IFiltraAgendamentoPorData
    {
        public DateTime? DataDeInicio { get; set; }
        public DateTime? DataDeTermino { get; set; }

        public Expression<Func<Agendamento, bool>> ObterExpressaoLambdaParaFiltro()
        {
            if (DataDeInicio.HasValue && DataDeTermino.HasValue)
            {
                return m => m.DataConsulta >= DataDeInicio && m.DataConsulta <= DataDeTermino;
            }

            if (!DataDeInicio.HasValue && DataDeTermino.HasValue)
            {
                return m => m.DataConsulta <= DataDeTermino;
            }

            if (DataDeInicio.HasValue && !DataDeTermino.HasValue)
            {
                return m => m.DataConsulta >= DataDeInicio;
            }
            return null;
        }
    }
}
