using Consultorio.Dominio.Comum.Enumeradores;
using Consultorio.Dominio.Consultorios;
using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;
using Consultorio.Infra.Consultorios;
using Consultorio.Infra.Consultorios.Agendamentos;
using Consultorio.Infra.Consultorios.Agendamentos.Filtros;
using Ninject.Modules;

namespace Consultorio.Infra.EstruturaBancoDeDados
{
    public class ModuloDoBancoDeDadosUsandoEntity : NinjectModule
    {
        public override void Load()
        {
            BindsRepositirosBancoDeDados();
            BindsEnumeradores();
            BindsFiltros();
        }

        private void BindsFiltros()
        {
            Bind<IFiltraAgendamentoPorCpfParciente>().To<FiltraAgendamentoPorCpfParciente>();
            Bind<IFiltraAgendamentoPorData>().To<FiltraAgendamentoPorData>();
            Bind<IFiltraAgendamentoPorIds>().To<FiltraAgendamentoPorIds>();
            Bind<IFiltraAgendamentoPorStatusId>().To<FiltraAgendamentoPorStatusId>();
            Bind<IFiltraAgendamentoPorClinicaId>().To<FiltraAgendamentoPorClinicaId>();
        }

        private void BindsEnumeradores()
        {
            Bind<ITodosOsEnumeradores>().To<TodosOsEnumeradores>().InSingletonScope();
        }

        private void BindsRepositirosBancoDeDados()
        {
            Bind<ITodosAsClinicas>().To<TodosAsClinicas>();
            Bind<ITodosOsEndereco>().To<TodosOsEndereco>();
            Bind<ITodosOsAgendamentos>().To<TodosOsAgendamentos>();
        }
    }
}
