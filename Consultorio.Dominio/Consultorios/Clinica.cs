using System.Collections.Generic;

namespace Consultorio.Dominio.Consultorios
{
    public class Clinica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string  Telefone { get; set; }
        public virtual Endereco Endereco { get; set; }

    }
}
