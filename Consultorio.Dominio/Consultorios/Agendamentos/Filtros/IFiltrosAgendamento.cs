using System;
using System.Linq.Expressions;

namespace Consultorio.Dominio.Consultorios.Agendamentos.Filtros
{
    public interface IFiltrosAgendamento
    {
        Expression<Func<Agendamento, bool>> ObterExpressaoLambdaParaFiltro();
    }
}
