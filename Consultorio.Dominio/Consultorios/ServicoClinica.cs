using System.Collections.Generic;
using System.Transactions;

namespace Consultorio.Dominio.Consultorios
{
    public class ServicoClinica : IServicoClinica
    {
        private readonly ITodosAsClinicas _todosAsClinicas;
        private readonly ITodosOsEndereco _todosOsEndereco;

        public ServicoClinica(ITodosAsClinicas todosAsClinicas,
            ITodosOsEndereco todosOsEndereco)
        {
            _todosAsClinicas = todosAsClinicas;
            _todosOsEndereco = todosOsEndereco;
        }
        public Clinica ObterPor(int clinicaId)
        {
            var clinica = _todosAsClinicas.ObterPor(clinicaId);
            if(clinica != null)
                clinica.Endereco = _todosOsEndereco.OberPor(clinicaId);

            return clinica;
        }

        public List<Clinica> ObterTodas()
        {
            return _todosAsClinicas.ObterTodas();
        }

        public void RemoverPor(int clinicaId)
        {
            using (var trans = new TransactionScope())
            {
                _todosAsClinicas.Remover(clinicaId);
                _todosOsEndereco.RemoverPor(clinicaId);
                trans.Complete();
            }
        }

        public void Salvar(Clinica clinica)
        {
            if (clinica.Id == decimal.Zero)
                AdicionarClinica(clinica);
            else
                AtualizarClinica(clinica);
        }

        private void AtualizarClinica(Clinica clinica)
        {
            using (var trans = new TransactionScope())
            {
                _todosAsClinicas.Atualizar(clinica);
                _todosOsEndereco.Atualizar(clinica.Endereco);
                trans.Complete();
            }
        }

        private void AdicionarClinica(Clinica clinica)
        {
            using (var trans = new TransactionScope())
            {
                var clinicaId = _todosAsClinicas.Adicionar(clinica);
                clinica.Endereco.ClinicaId = clinicaId;
                _todosOsEndereco.Adicinar(clinica.Endereco);
                trans.Complete();
            }
        }

        public Endereco ObterEnderecoPor(int clinicaId)
        {
            return _todosOsEndereco.OberPor(clinicaId);
        }
    }
}
