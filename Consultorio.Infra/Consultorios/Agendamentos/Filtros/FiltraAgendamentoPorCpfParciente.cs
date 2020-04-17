using System;
using System.Linq.Expressions;
using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;

namespace Consultorio.Infra.Consultorios.Agendamentos.Filtros
{
    public class FiltraAgendamentoPorCpfParciente : IFiltraAgendamentoPorCpfParciente
    {
        public string CpfPaciente { get; set; }

        public Expression<Func<Agendamento, bool>> ObterExpressaoLambdaParaFiltro()
        {
            return x => x.CpfPaciente.Contains(CpfPaciente);
        }
    }
}
