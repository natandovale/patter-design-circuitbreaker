namespace ConsoleApp1;

public class ExternalService
{
    public Exception ReturnUnexpectedException()
    {
        throw new Exception();
    }

    public string ReturnSomething()
    {
        return "something";
    }
}
