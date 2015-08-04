using System;
using System.Collections.Generic;
using System.Linq;

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
            //no partial hours
            start = start.FloorHour();
            end = end.CeilHour();
            return GetRates(start, end, bedTime).Sum();
        }

        public virtual int GetTotalHours(DateTime start, DateTime end)
        {
            return (end - start).Hours;
        }

        private IEnumerable<decimal> GetRates(DateTime start, DateTime end, DateTime bedTime)
        {
            int totalHours = GetTotalHours(start, end);
            DateTime midnight = start.GetMidnightForShift();
            for (int hour = 0; hour < totalHours; hour++)
            {
                DateTime current = start.AddHours(hour);
                if (current < midnight)//before midnight
                {
                    if (current < bedTime)//before bed
                        yield return StartToBedRate;
                    else
                        yield return BedToMidnightRate;
                }
                else
                    yield return MidnightToEndRate;//this rate is likely to be higher than the rest no matter what
            }
        }
    }
}
