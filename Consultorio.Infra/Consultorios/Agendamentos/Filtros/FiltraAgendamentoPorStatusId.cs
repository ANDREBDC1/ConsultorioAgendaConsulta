using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;
using System;
using System.Linq.Expressions;

namespace Consultorio.Infra.Consultorios.Agendamentos.Filtros
{
    public class FiltraAgendamentoPorStatusId : IFiltraAgendamentoPorStatusId
    {
        public int StatusId { get; set; }

        public Expression<Func<Agendamento, bool>> ObterExpressaoLambdaParaFiltro()
        {
            return x => x.StatusId == StatusId; 
        }
    }
}
