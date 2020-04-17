namespace Consultorio.Dominio.Consultorios.Agendamentos.Filtros
{
    public interface IFiltraAgendamentoPorStatusId : IFiltrosAgendamento
    {
        int StatusId { get; set; }
    }
}
