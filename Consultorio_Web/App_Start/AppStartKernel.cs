using Consultorio.Infra.EstruturaBancoDeDados;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Consultorio_Web.App_Start
{
    public class AppStartKernel : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public AppStartKernel()
        {
            ninjectKernel = new StandardKernel(new ModuloDoBancoDeDadosUsandoEntity(),
                new ModuloWebConsultorio());
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }
    }
}