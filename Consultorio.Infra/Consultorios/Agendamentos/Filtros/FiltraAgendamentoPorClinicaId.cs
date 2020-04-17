using System;
using System.Linq.Expressions;
using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;

namespace Consultorio.Infra.Consultorios.Agendamentos.Filtros
{
    public class FiltraAgendamentoPorClinicaId : IFiltraAgendamentoPorClinicaId
    {
        public int ClinicaId { get; set; }

        public Expression<Func<Agendamento, bool>> ObterExpressaoLambdaParaFiltro()
        {
            return x => x.ClinicaId == ClinicaId;
        }
    }
}
