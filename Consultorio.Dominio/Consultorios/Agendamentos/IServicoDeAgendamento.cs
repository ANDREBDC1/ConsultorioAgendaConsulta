using Consultorio.Dominio.Comum.Enumeradores;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;
using System;
using System.Collections.Generic;

namespace Consultorio.Dominio.Consultorios.Agendamentos
{
    public interface IServicoDeAgendamento
    {
        void Salvar(Agendamento agendamento);
        void AtualizarStatus(int agendamentoId, StatusAgendamentoEnum statusAgendamentoEnum);

        List<BaseParaEnumerador<StatusAgendamentoEnum>> ObterTodosOsStatusAgendamento();

        List<Agendamento> ObterPor(FiltrosAgendamentos filtroAgendamento);

        Agendamento ObterPor(int agendamentoId);
        int ObterTotalDeVagas(DateTime dataConsulta, int clinicaId);
    }
}
