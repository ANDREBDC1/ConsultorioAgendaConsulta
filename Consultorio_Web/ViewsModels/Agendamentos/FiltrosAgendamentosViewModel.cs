using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Consultorio_Web.ViewsModels.Agendamentos
{
    public class FiltrosAgendamentosViewModel
    {
        [DisplayName("Status")]
        public int StatusAgendamentoId { get; set; }

        [DisplayName("Data Agendamento de")]
        [DataType(DataType.Date)]
        public DateTime DataAgendamentoDe { get; set; }

        
        [DisplayName("Até")]
        [DataType(DataType.Date)]
        public DateTime DataAgendamentoAte { get; set; }
    }
}