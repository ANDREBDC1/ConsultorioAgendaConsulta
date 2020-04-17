using Consultorio.Dominio.Comum.Enumeradores;
using System.ComponentModel;

namespace Consultorio.Dominio.Consultorios.Agendamentos
{
    public class StatusAgendamento : BaseParaEnumerador<StatusAgendamentoEnum> { }
    public enum StatusAgendamentoEnum
    {
        [Description("Aguardando atendimento")]
        Aguardando = 1,

        [Description("Atendido")]
        Atendido = 2,

        [Description("Não Compareceu")]
        NaoCompareceu = 3,

        [Description("Cancelado pelo Usuário")]
        CanceladoUsuario = 4,

        [Description("Cancelado pela Clínica")]
        CanceladoClinica = 5,
    }
}
