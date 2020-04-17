using Consultorio.Dominio.Consultorios;
using System.Data.Entity.ModelConfiguration;

namespace Consultorio.Infra.EntityConfiguracao.Consultorios
{
    public class EnderecoConfiguracao : EntityTypeConfiguration<Endereco>
    {
        public EnderecoConfiguracao()
        {
            HasKey(c => c.Id);
            Property(c => c.Cep).HasMaxLength(20);
            Property(c => c.Rua).HasMaxLength(120);
            Property(c => c.Cidade).HasMaxLength(120);
            Property(c => c.Bairro).HasMaxLength(120);
            Property(c => c.Numero).HasMaxLength(100);
            Property(c => c.Uf).HasMaxLength(2);
        }
    }
}
