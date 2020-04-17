using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Consultorio_Web.ViewsModels
{
    public class EnderecoViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha campo Uf")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Preencha campo Rua")]
        [MaxLength(120, ErrorMessage = "Tamanho máximo 120 caracteres")]
        [MinLength(2, ErrorMessage = "Tamanho mínimo 2 caracteres")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Preencha campo Bairro")]
        [MaxLength(120, ErrorMessage = "Tamanho máximo 120 caracteres")]
        [MinLength(2, ErrorMessage = "Tamanho mínimo 2 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Preencha campo Numero")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo 100 caracteres")]
        [MinLength(1, ErrorMessage = "Tamanho mínimo 1 caracteres")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Preencha campo Cidade")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo 100 caracteres")]
        [MinLength(1, ErrorMessage = "Tamanho mínimo 1 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Preencha campo UF")]
        [MaxLength(2, ErrorMessage = "Tamanho máximo 2 caracteres")]
        [MinLength(2, ErrorMessage = "Tamanho mínimo 2 caracteres")]
        [DisplayName("UF")]
        public string Uf { get; set; }

        [ScaffoldColumn(false)]
        public int ClinicaId { get; set; }
    }
}