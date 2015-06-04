using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebAuto.Common
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            BinaryExpression and = Expression.And(left, right);
            ParameterExpression parameter = Expression.Parameter(typeof(T));
    
            return Expression.Lambda<Func<T, bool>>(and, parameter);
        }
    }
}
