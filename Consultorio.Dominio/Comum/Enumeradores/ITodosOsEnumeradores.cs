using System.Collections.Generic;

namespace Consultorio.Dominio.Comum.Enumeradores
{
    public interface ITodosOsEnumeradores
    {
        List<BaseParaEnumerador<T>> ObterTodos<T>() where T : new();
    }
}