using AutoMapper;
using Consultorio.Dominio.Consultorios;
using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;
using Consultorio_Web.ViewsModels;
using Consultorio_Web.ViewsModels.Agendamentos;

namespace Consultorio_Web.AutoMapper
{
    public class DominioParaViewModelMapiarPreferencia : Profile
    {
        public override string ProfileName => typeof(DominioParaViewModelMapiarPreferencia).Name;

        protected override void Configure()
        {
            Mapper.CreateMap<ClinicaViewModel, Clinica>();
            Mapper.CreateMap<EnderecoViewModel, Endereco>();
            Mapper.CreateMap<AgendamentoViewModel, Agendamento>();
            Mapper.CreateMap<FiltrosAgendamentosViewModel, FiltrosAgendamentos>();
        }
    }
}