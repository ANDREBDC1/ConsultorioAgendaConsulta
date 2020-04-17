namespace Consultorio.Dominio.Consultorios.Agendamentos.Filtros
{
    public interface IFiltraAgendamentoPorCpfParciente : IFiltrosAgendamento
    {
        string CpfPaciente { get; set; }
    }
}
