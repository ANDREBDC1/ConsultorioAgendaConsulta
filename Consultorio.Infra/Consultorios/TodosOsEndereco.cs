using Consultorio.Dominio.Consultorios;
using Consultorio.Infra.EstruturaBancoDeDados;
using System.Linq;

namespace Consultorio.Infra.Consultorios
{
    public class TodosOsEndereco : Repositorio<Endereco>, ITodosOsEndereco
    {
        public void Adicinar(Endereco endereco)
        {
            Add(endereco);
            SaveAll();
        }

        public void Atualizar(Endereco endereco)
        {
            Update(endereco);
            SaveAll();
        }

        public Endereco OberPor(int clinicaId)
        {
            return GetBy(c => c.ClinicaId == clinicaId).FirstOrDefault();
        }

        public void RemoverPor(int clinicaId)
        {
            DeleteBy(c => c.ClinicaId == clinicaId);
            SaveAll();
        }
    }
}
