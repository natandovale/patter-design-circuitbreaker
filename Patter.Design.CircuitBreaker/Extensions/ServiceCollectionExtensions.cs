using Microsoft.Extensions.DependencyInjection;

namespace Patter.Design.CircuitBreaker.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCircuitBreaker<ICircuitBreaker, TCircuitBreakerImplementation>(this IServiceCollection services)
        where ICircuitBreaker : class
        where TCircuitBreakerImplementation : class, ICircuitBreaker
    {
        services.AddSingleton<ICircuitBreaker, TCircuitBreakerImplementation>();
        return services;
    }
}
