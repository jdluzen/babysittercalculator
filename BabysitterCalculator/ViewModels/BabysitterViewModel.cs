using System;

namespace BabysitterCalculator
{
    public class BabysitterViewModel : ViewModelModel<ICalculator>
    {
        public BabysitterViewModel()
            : base(new StandardCalculator(12, 8, 16))
        {
        }

        public decimal StartToBedRate
        {
            get
            {
                return GetProperty(() => Model.StartToBedRate);
            }
            set
            {
                SetProperty(() => Model.StartToBedRate, value);
            }
        }

        public decimal BedToMidnightRate
        {
            get
            {
                return GetProperty(() => Model.BedToMidnightRate);
            }
            set
            {
                SetProperty(() => Model.BedToMidnightRate, value);
            }
        }

        public decimal MidnightToEndRate
        {
            get
            {
                return GetProperty(() => Model.MidnightToEndRate);
            }
            set
            {
                SetProperty(() => Model.MidnightToEndRate, value);
            }
        }
    }
}
