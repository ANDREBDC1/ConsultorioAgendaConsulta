using System;
using System.Linq.Expressions;

namespace Consultorio.Infra.EstruturaBancoDeDados.Extensoes
{
    public static class ExtencoesParaLambdaDinamica
    {
        public static Expression<Func<T, bool>> ObterExpressaoDinamicaQueContenha<T>(string propriedade, object valor)
        {
            var parametros = Expression.Parameter(typeof(T), "t");
            var propriedadeDaExpressao = Expression.Property(parametros, propriedade);
            object valorConvertido;

            if (propriedadeDaExpressao.Type.Name.Equals("Boolean"))
            {
                return ObterExpressaoDinamicaDeIgualdade<T>(propriedade, valor);
            }

            try
            {
                valorConvertido = Convert.ChangeType(valor, propriedadeDaExpressao.Type);
            }
            catch (Exception)
            {
                valorConvertido = Convert.ChangeType(GetDefaultValue(propriedadeDaExpressao.Type), propriedadeDaExpressao.Type);
            }

            var constanteDaExpressao = Expression.Constant(valorConvertido?.ToString());

            var metodoInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            Expression callExpressao;
            if (!propriedadeDaExpressao.Type.Name.Equals("String"))
            {
                Expression propriedadeToString = Expression.Call(propriedadeDaExpressao, "ToString", Type.EmptyTypes);
                callExpressao = Expression.Call(propriedadeToString, metodoInfo, constanteDaExpressao);
            }
            else
            {
                callExpressao = Expression.Call(propriedadeDaExpressao, metodoInfo, constanteDaExpressao);
            }

            var expressao = Expression.Lambda<Func<T, bool>>(callExpressao, parametros);

            return expressao;
        }

        public static Expression<Func<T, bool>> ObterExpressaoDinamicaDeIgualdade<T>(string propriedade, object valor)
        {
            var parametros = Expression.Parameter(typeof(T), "t");
            var propriedadeDaExpressao = Expression.Property(parametros, propriedade);
            object valorConvertido;

            try
            {
                valorConvertido = Convert.ChangeType(valor, propriedadeDaExpressao.Type);
            }
            catch (Exception)
            {
                valorConvertido = Convert.ChangeType(GetDefaultValue(propriedadeDaExpressao.Type), propriedadeDaExpressao.Type);
            }

            var constanteDaExpressao = Expression.Constant(valorConvertido);
            var expressao = Expression.Equal(propriedadeDaExpressao, constanteDaExpressao);

            var lambda = Expression.Lambda<Func<T, bool>>(expressao, parametros);

            return lambda;
        }

        public static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
