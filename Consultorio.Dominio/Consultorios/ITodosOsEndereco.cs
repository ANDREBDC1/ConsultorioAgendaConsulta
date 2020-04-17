using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consultorio.Dominio.Consultorios
{
    public interface ITodosOsEndereco
    {
        void Adicinar(Endereco endereco);
        void Atualizar(Endereco endereco);
        void RemoverPor(int clinicaId);
        Endereco OberPor(int clinicaId);
    }
}
