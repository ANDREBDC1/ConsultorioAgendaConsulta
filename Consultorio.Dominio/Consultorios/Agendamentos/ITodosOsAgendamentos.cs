using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;
using System.Collections.Generic;

namespace Consultorio.Dominio.Consultorios.Agendamentos
{
    public interface ITodosOsAgendamentos
    {
        void Adicionar(Agendamento agendamento);
        void Atualizar(Agendamento agendamento);
        void DeletarPor(int id);
        Agendamento ObterPor(int id);

        List<Agendamento> ObterPor(IFiltrosAgendamento [] filtros);
    }
}
