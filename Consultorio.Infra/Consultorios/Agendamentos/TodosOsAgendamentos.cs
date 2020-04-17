using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;
using Consultorio.Infra.EstruturaBancoDeDados;
using Consultorio.Infra.EstruturaBancoDeDados.Extensoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Consultorio.Infra.Consultorios.Agendamentos
{
    public class TodosOsAgendamentos : Repositorio<Agendamento>, ITodosOsAgendamentos
    {
        public void Adicionar(Agendamento agendamento)
        {
            Add(agendamento);
            SaveAll();
        }

        public void Atualizar(Agendamento agendamento)
        {
            Update(agendamento);
            SaveAll();
        }

        public void DeletarPor(int id)
        {
            DeleteBy(c => c.Id == id);
        }

        public Agendamento ObterPor(int id)
        {
            return GetBy(c => c.Id == id).FirstOrDefault();
        }

        public List<Agendamento> ObterPor(IFiltrosAgendamento[] filtros)
        {
            var filtrosAgendamento = MontaExpressaoDeFiltroParaAgendamento(filtros);

            return filtrosAgendamento != null
                ? GetBy(filtrosAgendamento).ToList()
                : GetAll().ToList();
        }

        private Expression<Func<Agendamento, bool>> MontaExpressaoDeFiltroParaAgendamento(IFiltrosAgendamento[] filtros)
        {
            Expression<Func<Agendamento, bool>> todosOsFiltros = null;
            foreach (var filtroImportacao in filtros)
            {
                var expressaoDeFiltro = filtroImportacao.ObterExpressaoLambdaParaFiltro();
                if (expressaoDeFiltro != null)
                    todosOsFiltros = (todosOsFiltros != null) ? todosOsFiltros.CombinarComAClausulaAnd(expressaoDeFiltro) : expressaoDeFiltro;

            }
            return todosOsFiltros;
        }
    }
}
