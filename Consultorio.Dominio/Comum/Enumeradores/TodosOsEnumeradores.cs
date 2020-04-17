using System;
using System.Collections.Generic;
using Consultorio.Dominio.Extensoes;

namespace Consultorio.Dominio.Comum.Enumeradores
{
    public class TodosOsEnumeradores : ITodosOsEnumeradores
    {
        public List<BaseParaEnumerador<T>> ObterTodos<T>() where T : new()
        {
            var valures = new List<BaseParaEnumerador<T>>();

            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var novoValor = new BaseParaEnumerador<T>
                {
                    ID = (T)value,
                    Description = ((Enum)value).GetDescription()
                };

                valures.Add(novoValor);
            }

            return valures;
        }
    }
}