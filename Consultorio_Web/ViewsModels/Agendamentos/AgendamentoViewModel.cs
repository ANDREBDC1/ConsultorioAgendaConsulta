using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Consultorio_Web.ViewsModels.Agendamentos
{
    public class AgendamentoViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Preenchar o nome do paciente")]
        [DisplayName("Nome Paciente")]
        [MaxLength(120, ErrorMessage = "Tamanho máximo 120 caracteres")]
        public string NomePaciente { get; set; }

        [Required(ErrorMessage = "Preenchar o CPF do paciente")]
        [DisplayName("CPF Paciente")]
        public string CpfPaciente { get; set; }

        [Required(ErrorMessage = "Preenchar o CPF do paciente")]
        [EmailAddress(ErrorMessage = "Preenchar um e-mail valido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Posssuir Plano?")]
        public bool TemPlano { get; set; }

        [DisplayName("Número Convênio")]
        public string NumeroConvenio { get; set; }

        [DisplayName("Nome Convênio")]
        [MaxLength(120, ErrorMessage = "Tamanho máximo 120 caracteres")]
        public string NomeConvenio { get; set; }

        [Required(ErrorMessage = "Preenchar data da consulta")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data Consulta")]
        public DateTime DataConsulta { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("Clinica")]
        public int ClinicaId { get; set; }

        [DisplayName("Status")]
        [ScaffoldColumn(false)]
        public int StatusId { get; set; }

        [DisplayName("Total de Vagas")]
        [ScaffoldColumn(false)]
        public int TotalVagas { get; set; }

        [DisplayName("Status")]
        [ScaffoldColumn(false)]
        public string StatusDescricao { get; set; }

        [DisplayName("Clinica")]
        [ScaffoldColumn(false)]
        public string ClinicaNome { get; set; }
    }

}