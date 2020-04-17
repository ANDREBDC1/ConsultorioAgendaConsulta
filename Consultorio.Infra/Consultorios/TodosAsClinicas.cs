using System.Collections.Generic;
using System.Linq;
using Consultorio.Dominio.Consultorios;
using Consultorio.Infra.EstruturaBancoDeDados;

namespace Consultorio.Infra.Consultorios
{
    public class TodosAsClinicas : Repositorio<Clinica>, ITodosAsClinicas
    {
       
        public int Adicionar(Clinica clinica)
        {
            Add(clinica);
            return SaveAndGetId();
        }

        public void Atualizar(Clinica clinica)
        {
            Update(clinica);
            SaveAll();
        }

        public List<Clinica> ObterPor(List<int> clinicaIds)
        {
            return GetBy(c => clinicaIds.Contains(c.Id)).ToList();
        }

        public Clinica ObterPor(int id)
        {
            return GetBy(c => c.Id == id).FirstOrDefault();
        }

        public List<Clinica> ObterTodas()
        {
            return GetAll().ToList();
        }

        public void Remover(List<int> clinicaIds)
        {
            DeleteBy(c => clinicaIds.Contains(c.Id));
            SaveAll();
        }

        public void Remover(int id)
        {
            DeleteBy(c => c.Id == id);
            SaveAll();
        }
    }
}
