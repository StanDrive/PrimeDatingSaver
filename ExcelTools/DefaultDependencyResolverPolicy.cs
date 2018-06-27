using Unity.Builder;
using Unity.Policy;

namespace ExcelTools
{
    class DefaultDependencyResolverPolicy : IResolverPolicy
    {
        public object Resolve(IBuilderContext context)
        {
            return context.Strategies.ExecuteBuildUp(context);
        }
    }
}
