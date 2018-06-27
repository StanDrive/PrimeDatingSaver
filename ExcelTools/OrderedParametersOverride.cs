using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Builder;
using Unity.Injection;
using Unity.Policy;
using Unity.Resolution;

namespace ExcelTools
{
    public class OrderedParametersOverride : ResolverOverride
    {
        private static readonly DefaultDependencyResolverPolicy DefaultPolicy = new DefaultDependencyResolverPolicy();
        private readonly Queue<InjectionParameterValue> _parameterValues;

        public OrderedParametersOverride(params object[] parameterValues)
        {
            _parameterValues = new Queue<InjectionParameterValue>(parameterValues.Select(InjectionParameterValue.ToParameter));
        }

        public override IResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
        {
            if (!_parameterValues.Any())
            {
                return null;
            }

            return _parameterValues.Dequeue().GetResolverPolicy(dependencyType);
        }
    }
}
