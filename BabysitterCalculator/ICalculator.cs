using System;

namespace BabysitterCalculator
{
    public interface ICalculator
    {
        decimal Calculate(DateTime start, DateTime end, DateTime bedTime);
    }
}
