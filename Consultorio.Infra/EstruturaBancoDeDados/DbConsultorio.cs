using Consultorio.Dominio.Consultorios;
using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Infra.EntityConfiguracao.Consultorios;
using Consultorio.Infra.EntityConfiguracao.Consultorios.Agendamentos;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Consultorio.Infra.EstruturaBancoDeDados
{
    public class DbConsultorio : DbContext
    {
        public DbConsultorio()
            : base("DbConsultorioConnectString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ClinicasConfiguracao());
            modelBuilder.Configurations.Add(new EnderecoConfiguracao());
            modelBuilder.Configurations.Add(new AgendamentoConfiguracao());
            base.OnModelCreating(modelBuilder);


        }

        public DbSet<Agendamento>  Agendamentos { get; set; }
        public DbSet<Clinica> Clinicas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        
    }
}
