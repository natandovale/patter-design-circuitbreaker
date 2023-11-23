using Patter.Design.CircuitBreaker.Exceptions;

namespace Patter.Design.CircuitBreaker;

public class CircuitBreakerConfig
{
    public CircuitBreakerState State { get; set; }
    public int FailureThreshold { get; set; }
    public int FailureThresoldingCount { get; set; }
    public TimeSpan FailureThresholdingPeriod { get; set; }
    public DateTime LastFailureThresholdingPeriod { get; set; }
    public int SuccessThreshold { get; set; }
    public int SuccessThresholdingCount { get; set; }

    public CircuitBreakerConfig()
    { }

    public void Run(Action run)
    {
        if (IsOpen())
        {
            if (LastFailureThresholdingPeriod.Add(FailureThresholdingPeriod) < DateTime.UtcNow)
            {
                try
                {
                    SetHalfOpen();
                    SuccessThresholdingCount = 0;
                    run();
                    SuccessThresholdingCount += 1;
                    return;
                }
                catch (Exception)
                {
                    LastFailureThresholdingPeriod = DateTime.UtcNow;
                    SetOpen();
                    throw new CircuitBreakerOpenException();
                }
            }

            throw new CircuitBreakerOpenException();
        }
        else if (IsHalfOpen())
        {
            try
            {
                run();
                SuccessThresholdingCount += 1;
                
                if (SuccessThresholdingCount >= SuccessThreshold)
                {
                    SuccessThresholdingCount = 0;
                    SetClosed();

                    return;
                }

                return;
            }
            catch (Exception)
            {
                throw new CircuitBreakerOpenException();
            }
        }
        else if (IsClosed())
        {
            try
            {
                run();
                FailureThresoldingCount = 0;

                return;
            }
            catch (Exception e)
            {
                TrackException(e);

                return;
            }
        }
    }

    private void TrackException(Exception ex)
    {
        FailureThresoldingCount += 1;

        if (FailureThresoldingCount == FailureThreshold)
        {
            LastFailureThresholdingPeriod = DateTime.UtcNow;
            SetOpen();

            return;
        }
    }

    private void SetOpen()
    {
        State = CircuitBreakerState.Open;
    }

    private void SetClosed()
    {
        State = CircuitBreakerState.Close;
    }

    private void SetHalfOpen()
    {
        State = CircuitBreakerState.HalfOpen;
    }

    private bool IsOpen() => State == CircuitBreakerState.Open;

    private bool IsClosed() => State == CircuitBreakerState.Close;
    
    private bool IsHalfOpen() => State == CircuitBreakerState.HalfOpen;
}
