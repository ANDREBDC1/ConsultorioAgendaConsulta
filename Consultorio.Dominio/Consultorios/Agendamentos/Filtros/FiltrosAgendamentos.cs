using System;
using System.Collections.Generic;

namespace Consultorio.Dominio.Consultorios.Agendamentos.Filtros
{
    public class FiltrosAgendamentos
    {
        public FiltrosAgendamentos()
        {
            AgendamentoIds = new List<int>();
        }
        public int ClinicaId { get; set; }
        public string CpfPaciente { get; set; }
        public List<int> AgendamentoIds { get; set; }
        public int StatusAgendamentoId { get; set; }
        public DateTime DataAgendamentoDe { get; set; }
        public DateTime DataAgendamentoAte { get; set; }
    }
}
