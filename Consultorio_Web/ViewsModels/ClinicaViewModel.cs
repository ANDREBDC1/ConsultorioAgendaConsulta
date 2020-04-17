using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Consultorio_Web.ViewsModels
{
    public class ClinicaViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Preencha campo nome da clinica")]
        [MaxLength(120, ErrorMessage = "Tamanho máximo 120 caracteres")]
        [MinLength(2, ErrorMessage = "Minimo 2 caracteres")]
        [DisplayName("Nome da Clinica")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha campo Cnpj")]
        [DisplayName("CNPJ")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Preencha campo Telefone")]
        public string Telefone { get; set; }

        
        public virtual EnderecoViewModel EnderecoViewModel { get; set; }
    }
}