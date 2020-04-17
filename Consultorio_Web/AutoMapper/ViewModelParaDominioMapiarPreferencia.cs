using AutoMapper;
using Consultorio.Dominio.Consultorios;
using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;
using Consultorio_Web.ViewsModels;
using Consultorio_Web.ViewsModels.Agendamentos;
namespace Consultorio_Web.AutoMapper
{
    public class ViewModelParaDominioMapiarPreferencia : Profile
    {
        public override string ProfileName => typeof(ViewModelParaDominioMapiarPreferencia).Name;

        protected override void Configure()
        {
            Mapper.CreateMap<Clinica, ClinicaViewModel>();
            Mapper.CreateMap<Endereco, EnderecoViewModel>();
            Mapper.CreateMap<Agendamento, AgendamentoViewModel>();
            Mapper.CreateMap<FiltrosAgendamentos, FiltrosAgendamentosViewModel>();
        }
    }
}