using System;

namespace BabysitterCalculator
{
    public interface ICalculator
    {
        decimal StartToBedRate { get; set; }

        decimal BedToMidnightRate { get; set; }

        decimal MidnightToEndRate { get; set; }

        decimal Calculate(DateTime start, DateTime end, DateTime bedTime);

        int GetTotalHours(DateTime start, DateTime end);
    }
}
