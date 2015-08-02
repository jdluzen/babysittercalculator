using System;

namespace BabysitterCalculator
{
    public class StandardCalculator : ICalculator
    {
        public decimal StartToBedRate { get; protected set; }

        public decimal BedToMidnightRate { get; protected set; }

        public decimal MidnightToEndRate { get; protected set; }

        public StandardCalculator(decimal startToBedRate, decimal bedToMidnightRate, decimal midnightToEndRate)
        {
            StartToBedRate = startToBedRate;
            BedToMidnightRate = bedToMidnightRate;
            MidnightToEndRate = midnightToEndRate;
        }

        public virtual decimal Calculate(DateTime start, DateTime end, DateTime bedTime)
        {
            if (start.TimeOfDay.Hours < 17 && start.TimeOfDay.Hours > 4)
                throw new ArgumentException("start time must be after 5 PM, and before 4 AM", "start");
            if (end.TimeOfDay.Hours < 17 && end.TimeOfDay.Hours > 4)
                throw new ArgumentException("end time must be after 5 PM, and before 4 AM", "end");
            if (end < start)
                throw new ArgumentException("end time must be after start time");
            if (bedTime.TimeOfDay.Hours < 17 && bedTime.TimeOfDay.Hours > 4)
                throw new ArgumentException("bed time must be after 5 PM, and before 4 AM", "bedTime");
            if (bedTime.Minute != 0 || bedTime.Second != 0)
                throw new ArgumentException("bed time must be on the hour", "bedTime");
            
            return 0;
        }
    }
}
