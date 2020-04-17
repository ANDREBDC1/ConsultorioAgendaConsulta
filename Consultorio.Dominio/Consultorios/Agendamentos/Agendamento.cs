using System;

namespace Consultorio.Dominio.Consultorios.Agendamentos
{
    public class Agendamento
    {
        public int Id { get; set; }
        public string NomePaciente { get; set; }
        public string CpfPaciente { get; set; }
        public string Email { get; set; }
        public bool TemPlano { get; set; }
        public string NumeroConvenio { get; set; }
        public string NomeConvenio { get; set; }
        public DateTime DataConsulta { get; set; }
        public int ClinicaId { get; set; }

        public string ClinicaNome { get; set; }

        public int StatusId { get; set; }

        public string StatusDescricao { get; set; }
        public StatusAgendamentoEnum status 
        { 
            get => (StatusAgendamentoEnum) StatusId; 
            set => StatusId = (int) value; 
        }
        public int TotalVagas { get; set; }
    }
}
