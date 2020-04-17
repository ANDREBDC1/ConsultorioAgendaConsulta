using Consultorio.Dominio.Consultorios;
using Consultorio.Dominio.Consultorios.Agendamentos;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consultorio_Web
{
    public class ModuloWebConsultorio : NinjectModule
    {
        public override void Load()
        {
            BindsServicos();
        }

        private void BindsServicos()
        {
            Bind<IServicoClinica>().To<ServicoClinica>();
            Bind<IServicoDeAgendamento>().To<ServicoDeAgendamento>();
        }
    }
}