namespace Patter.Design.CircuitBreaker;

public class CircuitBreaker : CircuitBreakerConfig
{
	public CircuitBreaker() : base()
	{
		State = CircuitBreakerState.Close;
	}

	public CircuitBreaker WithFailureThreshold(int failureThreshold)
	{
		FailureThreshold = failureThreshold;
		return this;
	}

    public CircuitBreaker WithSuccessThreshold(int successThreshold)
    {
        SuccessThreshold = successThreshold;
        return this;
    }

	public CircuitBreaker WithFailureThresholdingPeriod(int failureThresholdingPeriod) 
	{
        FailureThresholdingPeriod = TimeSpan.FromMinutes(failureThresholdingPeriod);
		return this;
    }
}
