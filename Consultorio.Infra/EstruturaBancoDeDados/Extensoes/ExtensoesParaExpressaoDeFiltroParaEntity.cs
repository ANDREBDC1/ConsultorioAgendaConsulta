using System;
using System.Linq.Expressions;

namespace Consultorio.Infra.EstruturaBancoDeDados.Extensoes
{
    internal sealed class ExpressionDelegateVisitor : ExpressionVisitor
    {

        private readonly Func<Expression, Expression> _mVisitor;
        private readonly bool _mRecursive;

        public static Expression Visit(Expression exp, Func<Expression, Expression> visitor, bool recursive)
        {
            return new ExpressionDelegateVisitor(visitor, recursive).Visit(exp);
        }

        private ExpressionDelegateVisitor(Func<Expression, Expression> visitor, bool recursive)
        {
            if (visitor == null) throw new ArgumentNullException(nameof(visitor));
            _mVisitor = visitor;
            _mRecursive = recursive;
        }

        public override Expression Visit(Expression node)
        {
            if (_mRecursive)
            {
                return base.Visit(_mVisitor(node));
            }
            else
            {
                var visited = _mVisitor(node);
                if (visited == node) return base.Visit(visited);
                return visited;
            }
        }

    }

    public static class SystemLinqExpressionsExpressionExtensions
    {

        public static Expression Visit(this Expression self, Func<Expression, Expression> visitor, bool recursive = false)
        {
            return ExpressionDelegateVisitor.Visit(self, visitor, recursive);
        }

        public static Expression Replace(this Expression self, Expression source, Expression target)
        {
            return self.Visit(x => x == source ? target : x);
        }

        public static Expression<Func<T, bool>> CombinarComAClausulaAnd<T>(this Expression<Func<T, bool>> self, Expression<Func<T, bool>> other)
        {

            var parameter = Expression.Parameter(typeof(T), "a");
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    self.Body.Replace(self.Parameters[0], parameter),
                    other.Body.Replace(other.Parameters[0], parameter)
                ),
                parameter
            );
        }

        public static Expression<Func<T, bool>> CombinarComAClausulaOr<T>(this Expression<Func<T, bool>> self, Expression<Func<T, bool>> other)
        {

            var parameter = Expression.Parameter(typeof(T), "a");
            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    self.Body.Replace(self.Parameters[0], parameter),
                    other.Body.Replace(other.Parameters[0], parameter)
                ),
                parameter
            );
        }

    }
}
