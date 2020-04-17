using System.Collections.Generic;

namespace Consultorio.Dominio.Consultorios
{
    public interface ITodosAsClinicas
    {
        int Adicionar(Clinica clinica);
        void Atualizar(Clinica clinica);
        void Remover(int id);
        List<Clinica> ObterPor(List<int> clinicaIds);
        List<Clinica> ObterTodas();
        Clinica ObterPor(int id);
    }
}
