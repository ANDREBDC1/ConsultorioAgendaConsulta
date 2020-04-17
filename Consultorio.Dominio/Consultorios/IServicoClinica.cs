using System.Collections.Generic;

namespace Consultorio.Dominio.Consultorios
{
    public interface IServicoClinica
    {
        void Salvar(Clinica clinica);

        List<Clinica> ObterTodas();

        Clinica ObterPor(int clinicaId);
        void RemoverPor(int clinicaId);

        Endereco ObterEnderecoPor(int clinicaId);
    }
}
