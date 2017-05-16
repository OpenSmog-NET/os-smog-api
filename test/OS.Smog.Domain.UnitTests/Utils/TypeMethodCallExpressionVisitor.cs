using System;
using System.Linq.Expressions;
using System.Reflection;

namespace OS.Smog.Domain.UnitTests.Utils
{
    internal sealed class TypeMethodCallExpressionVisitor : ExpressionVisitor
    {
        private readonly Type toFindCall;
        public MethodInfo Method;

        public TypeMethodCallExpressionVisitor(Type toFindCall)
        {
            this.toFindCall = toFindCall;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var parameters = node.Method.GetParameters();
            if (parameters.Length > 0)
                if (parameters.Length > 0 && parameters[0].ParameterType == toFindCall)
                    Method = node.Method;
            return base.VisitMethodCall(node);
        }
    }
}