using Patter.Design.CircuitBreaker;

namespace ConsoleApp1;

public class AnyCircuitBreakerTest : ICircuitBreaker
{
    public CircuitBreaker Init()
        => new();
}
