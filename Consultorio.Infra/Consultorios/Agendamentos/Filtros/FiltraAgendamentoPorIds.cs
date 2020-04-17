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
    public class FiltraAgendamentoPorIds : IFiltraAgendamentoPorIds
    {
        public List<int> AgendamentoIds { get; set; }

        public Expression<Func<Agendamento, bool>> ObterExpressaoLambdaParaFiltro()
        {
            return x => AgendamentoIds.Contains(x.Id);
        }
    }
}
