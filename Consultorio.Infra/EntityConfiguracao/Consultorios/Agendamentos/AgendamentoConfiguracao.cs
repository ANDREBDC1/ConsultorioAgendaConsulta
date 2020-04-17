using Consultorio.Dominio.Consultorios.Agendamentos;
using System.Data.Entity.ModelConfiguration;

namespace Consultorio.Infra.EntityConfiguracao.Consultorios.Agendamentos
{
    public class AgendamentoConfiguracao : EntityTypeConfiguration<Agendamento>
    {
        public AgendamentoConfiguracao()
        {
            HasKey(c => c.Id);
            Property(c => c.NomePaciente).HasMaxLength(200);
            Property(c => c.CpfPaciente).HasMaxLength(30);
            Property(c => c.Email).HasMaxLength(100);
            Property(c => c.NomeConvenio).HasMaxLength(200);
            Ignore(c => c.status);
            Ignore(c => c.TotalVagas);
        }
    }
}
