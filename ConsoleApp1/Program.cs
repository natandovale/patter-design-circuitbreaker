// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using Patter.Design.CircuitBreaker.Exceptions;

var externalService = new ExternalService();

var circuitBreaker = new AnyCircuitBreakerTest()
                  .Init()
                  .WithFailureThreshold(1)
                  .WithSuccessThreshold(1)
                  .WithFailureThresholdingPeriod(1);
string er = "";

try
{
    circuitBreaker.Run(() => Console.WriteLine("Request 1 to externalService!-Success"));

    circuitBreaker.Run(() => Console.WriteLine("Request 2 to externalService!-Success"));

    circuitBreaker.Run(() => er = externalService.ReturnSomething());

    circuitBreaker.Run(() => externalService.ReturnUnexpectedException());
}
catch (CircuitBreakerOpenException e)
{
    
}

try
{
    circuitBreaker.Run(() => Console.WriteLine("Request 5 to externalService!- failed"));
}
catch (Exception)
{
    Console.WriteLine("Is Open!");
}

try
{
    circuitBreaker.Run(() => Console.WriteLine("Request 6 to externalService!"));
}
catch (Exception)
{
    Console.WriteLine("Is Open!");
}


try
{
    circuitBreaker.Run(() => Console.WriteLine("Is Half-Open!"));
}
catch (CircuitBreakerOpenException e)
{
    Console.WriteLine("Is Open!");
}

circuitBreaker.Run(() => Console.WriteLine("Request 8 to externalService!-Success"));
circuitBreaker.Run(() => Console.WriteLine("Request 9 to externalService!-Success"));
circuitBreaker.Run(() => Console.WriteLine("Request 10 to externalService!-Success"));

Console.WriteLine(er);
