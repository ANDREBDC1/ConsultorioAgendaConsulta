using Consultorio.Dominio.Consultorios;
using System.Data.Entity.ModelConfiguration;

namespace Consultorio.Infra.EntityConfiguracao.Consultorios
{
    public class ClinicasConfiguracao : EntityTypeConfiguration<Clinica>
    {
        public ClinicasConfiguracao()
        {
            HasKey(c => c.Id);
            Property(c => c.Nome).HasMaxLength(100);
            Property(c => c.Cnpj).HasMaxLength(30);
            Property(c => c.Telefone).HasMaxLength(20);
            Ignore(c => c.Endereco);

        }
    }
}
