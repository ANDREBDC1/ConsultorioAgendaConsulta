namespace Consultorio.Dominio.Consultorios.Agendamentos.Filtros
{
    public interface IFiltraAgendamentoPorClinicaId : IFiltrosAgendamento
    {
        int ClinicaId { get; set; }
    }
}
