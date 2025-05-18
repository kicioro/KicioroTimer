namespace KicioroTimer.Utils;

public static class PrintHelper
{
    public static string PrintTime(TimeSpan span)
    {
        return string.Format("{0}h{1}m", span.Days * 24 + span.Hours, span.Minutes);
    }

    public static string PrintLearningTime(TimeSpan span)
    {
        if(span.Hours != 0)
            return string.Format("{0}h{1}m", span.Hours, span.Minutes);
        
        return string.Format("{0}m", span.Minutes);
    }
    
    public static string PrintHours(TimeSpan span)
    {
        return string.Format("{0}h", span.Days * 24 + span.Hours);
    }
}