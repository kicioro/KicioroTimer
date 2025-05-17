namespace KicioroTimer.Logic;

public class Progress
{
    public int Value { get; set; }
}

public class CountService
{
    public event EventHandler<Progress> Progress;
    
    private bool stop = false;

    public void Stop()
    {
        stop = true;
    }
    public async Task<double> Count(int sumLength)
    {
        for (int i = 0; i < sumLength; i++)
        {
            if(stop)
                return 0;
            
            Progress?.Invoke(this, new Progress() { Value = i });
            await Task.Delay(1000);
        }
        return 0;
    }
}