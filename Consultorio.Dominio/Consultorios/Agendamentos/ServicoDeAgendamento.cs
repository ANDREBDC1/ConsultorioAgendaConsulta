using System;
using System.Collections.Generic;
using System.Linq;
using Consultorio.Dominio.Comum.Enumeradores;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;

namespace Consultorio.Dominio.Consultorios.Agendamentos
{
    public class ServicoDeAgendamento : IServicoDeAgendamento
    {
        private readonly ITodosOsEnumeradores _todosOsEnumeradores;
        private readonly ITodosOsAgendamentos _todosOsAgendamentos;
        private readonly IFiltraAgendamentoPorCpfParciente _filtraAgendamentoPorCpfParciente;
        private readonly IFiltraAgendamentoPorIds _filtraAgendamentoPorIds;
        private readonly IFiltraAgendamentoPorClinicaId _filtraAgendamentoPorClinicaId;
        private readonly ITodosAsClinicas _todosAsClinicas;
        private readonly IFiltraAgendamentoPorStatusId _filtraAgendamentoPorStatusId;
        private readonly IFiltraAgendamentoPorData _filtraAgendamentoPorData;

        public ServicoDeAgendamento(ITodosOsEnumeradores todosOsEnumeradores,
            ITodosOsAgendamentos todosOsAgendamentos,
            IFiltraAgendamentoPorCpfParciente filtraAgendamentoPorCpfParciente,
            IFiltraAgendamentoPorData filtraAgendamentoPorData,
            IFiltraAgendamentoPorStatusId filtraAgendamentoPorStatusId,
            IFiltraAgendamentoPorIds filtraAgendamentoPorIds,
            IFiltraAgendamentoPorClinicaId filtraAgendamentoPorClinicaId,
            ITodosAsClinicas todosAsClinicas)
        {
            _todosOsEnumeradores = todosOsEnumeradores;
            _todosOsAgendamentos = todosOsAgendamentos;
            _filtraAgendamentoPorCpfParciente = filtraAgendamentoPorCpfParciente;
            _filtraAgendamentoPorIds = filtraAgendamentoPorIds;
            _filtraAgendamentoPorClinicaId = filtraAgendamentoPorClinicaId;
            _todosAsClinicas = todosAsClinicas;
            _filtraAgendamentoPorStatusId = filtraAgendamentoPorStatusId;
            _filtraAgendamentoPorData = filtraAgendamentoPorData;
        }
        public void AtualizarStatus(int agendamentoId, StatusAgendamentoEnum statusAgendamentoEnum)
        {
            var agendamentoBanco = _todosOsAgendamentos.ObterPor(agendamentoId);
            agendamentoBanco.status = statusAgendamentoEnum;
            agendamentoBanco.StatusDescricao = ObterStatusDescricao(agendamentoBanco);
            _todosOsAgendamentos.Atualizar(agendamentoBanco);
        }

        public List<BaseParaEnumerador<StatusAgendamentoEnum>> ObterTodosOsStatusAgendamento()
        {
            return _todosOsEnumeradores.ObterTodos<StatusAgendamentoEnum>();
        }

        public void Salvar(Agendamento agendamento)
        {
            agendamento.StatusDescricao = ObterStatusDescricao(agendamento);
            agendamento.ClinicaNome = ObterClinicaNome(agendamento);
            if (agendamento.Id == decimal.Zero)
                AdiconarAgendamento(agendamento);
            else
                AtualizarAgendamento(agendamento);
        }

        private string ObterClinicaNome(Agendamento agendamento)
        {
            return agendamento.ClinicaId == decimal.Zero 
                ? string.Empty
                :_todosAsClinicas.ObterPor(agendamento.ClinicaId).Nome;
        }

        private string ObterStatusDescricao(Agendamento agendamento)
        {
            return ObterTodosOsStatusAgendamento().FirstOrDefault(c => Convert.ToInt32(c.ID) == agendamento.StatusId).Description;
        }

        private void AtualizarAgendamento(Agendamento agendamento)
        {
            _todosOsAgendamentos.Atualizar(agendamento);
        }

        private void AdiconarAgendamento(Agendamento agendamento)
        {
            ValidarAgendamentoAdicinar(agendamento);
            
            _todosOsAgendamentos.Adicionar(agendamento);
        }

        private void ValidarAgendamentoAdicinar(Agendamento agendamento)
        {
            var agendamentoBanco = ObterPor(new FiltrosAgendamentos
            {
                DataAgendamentoAte = agendamento.DataConsulta,
                DataAgendamentoDe = agendamento.DataConsulta,
                CpfPaciente = agendamento.CpfPaciente,
                ClinicaId = agendamento.ClinicaId,
                StatusAgendamentoId = (int) StatusAgendamentoEnum.Aguardando,
            });

            if (agendamentoBanco.Any())
                throw new Exception($"O paciente {agendamento.NomePaciente} CPF {agendamento.CpfPaciente} já está agendado para a clínica {agendamento.ClinicaNome}, com data {agendamento.DataConsulta.ToString("dd/MM/yyyy")}");

            if (agendamento.TotalVagas <= decimal.Zero)
                throw new Exception($"Não há vagas disponível para data {agendamento.DataConsulta.ToString("dd/MM/yyyy")}");

            if (string.IsNullOrWhiteSpace(agendamento.ClinicaNome))
                throw new Exception("Clínica não informada");
        }

        public List<Agendamento> ObterPor(FiltrosAgendamentos filtroAgendamento)
        {
            var filtros = ObterFitrosAgendamento(filtroAgendamento);
            return _todosOsAgendamentos.ObterPor(filtros);
        }

        private IFiltrosAgendamento[] ObterFitrosAgendamento(FiltrosAgendamentos filtroAgendamento)
        {
            var filtros = new List<IFiltrosAgendamento>();

            if (!string.IsNullOrWhiteSpace(filtroAgendamento.CpfPaciente)) 
            {
                _filtraAgendamentoPorCpfParciente.CpfPaciente = filtroAgendamento.CpfPaciente;
                filtros.Add(_filtraAgendamentoPorCpfParciente);
            }

            if(filtroAgendamento.StatusAgendamentoId != decimal.Zero)
            {
                _filtraAgendamentoPorStatusId.StatusId = filtroAgendamento.StatusAgendamentoId;
                filtros.Add(_filtraAgendamentoPorStatusId);
            }

            if (filtroAgendamento.DataAgendamentoDe != DateTime.MinValue)
            {
               _filtraAgendamentoPorData.DataDeInicio = filtroAgendamento.DataAgendamentoDe;
                
            }

            if (filtroAgendamento.DataAgendamentoAte != DateTime.MinValue)
            {
                _filtraAgendamentoPorData.DataDeTermino = filtroAgendamento.DataAgendamentoAte;

            }

            if (filtroAgendamento.DataAgendamentoAte != DateTime.MinValue
                || filtroAgendamento.DataAgendamentoDe != DateTime.MinValue)
                filtros.Add(_filtraAgendamentoPorData);

            if (filtroAgendamento.AgendamentoIds.Any())
            {
                _filtraAgendamentoPorIds.AgendamentoIds = filtroAgendamento.AgendamentoIds;
                filtros.Add(_filtraAgendamentoPorIds);
            }

            if(filtroAgendamento.ClinicaId != decimal.Zero)
            {
                _filtraAgendamentoPorClinicaId.ClinicaId = filtroAgendamento.ClinicaId;
                filtros.Add(_filtraAgendamentoPorClinicaId);
            }

            return filtros.ToArray();
        }

        public Agendamento ObterPor(int agendamentoId)
        {
            return _todosOsAgendamentos.ObterPor(agendamentoId);
        }

        public int ObterTotalDeVagas(DateTime dataConsulta, int clinicaId)
        {
            var totalDeVagaPorDia = 20;
            var totalVagasJaAgendas = ObterPor(new FiltrosAgendamentos
            {
                DataAgendamentoDe = dataConsulta,
                DataAgendamentoAte = dataConsulta,
                ClinicaId = clinicaId,
                StatusAgendamentoId = (int)StatusAgendamentoEnum.Aguardando
            }).Count;

            return totalDeVagaPorDia - totalVagasJaAgendas;
        }
    }
}
