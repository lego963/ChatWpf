using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ChatWpf.Core.Expressions
{
    public static class ExpressionHelpers
    {
        public static T GetPropertyValue<T>(this Expression<Func<T>> lambda)
        {
            return lambda.Compile().Invoke();
        }

        public static T GetPropertyValue<TIn, T>(this Expression<Func<TIn, T>> lambda, TIn input)
        {
            return lambda.Compile().Invoke(input);
        }

        public static void SetPropertyValue<T>(this Expression<Func<T>> lambda, T value)
        {
            if (!(lambda.Body is MemberExpression expression)) return;
            var propertyInfo = (PropertyInfo)expression.Member;
            var target = Expression.Lambda(expression.Expression).Compile().DynamicInvoke();

            propertyInfo.SetValue(target, value);
        }


        public static void SetPropertyValue<TIn, T>(this Expression<Func<TIn, T>> lambda, T value, TIn input)
        {
            if (!(lambda.Body is MemberExpression expression)) return;
            var propertyInfo = (PropertyInfo)expression.Member;

            propertyInfo.SetValue(input, value);
        }
    }
}