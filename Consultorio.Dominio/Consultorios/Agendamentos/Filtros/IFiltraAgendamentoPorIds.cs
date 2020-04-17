using System.Collections.Generic;

namespace Consultorio.Dominio.Consultorios.Agendamentos.Filtros
{
    public interface IFiltraAgendamentoPorIds : IFiltrosAgendamento
    {
        List<int> AgendamentoIds { get; set; }
    }
}
