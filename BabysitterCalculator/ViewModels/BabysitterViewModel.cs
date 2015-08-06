using System;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace BabysitterCalculator
{
    public class BabysitterViewModel : ViewModelModel<ICalculator>
    {
        public BabysitterViewModel()
            : base(new StandardCalculator(12, 8, 16))
        {
            ShiftDate = DateTime.Now;
            Start = new TimeSpan(17, 0, 0);
            End = new TimeSpan(4, 0, 0);
            BedTime = new TimeSpan(20, 0, 0);
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

        private DateTime shiftDate;

        public DateTime ShiftDate
        {
            get
            {
                return shiftDate;
            }
            set
            {
                SetProperty(ref shiftDate, value);
            }
        }

        private TimeSpan start;

        public TimeSpan Start
        {
            get
            {
                return start;
            }
            set
            {
                SetProperty(ref start, value);
            }
        }

        private TimeSpan end;

        public TimeSpan End
        {
            get
            {
                return end;
            }
            set
            {
                SetProperty(ref end, value);
            }
        }

        private TimeSpan bedTime;

        public TimeSpan BedTime
        {
            get
            {
                return bedTime;
            }
            set
            {
                SetProperty(ref bedTime, value);
            }
        }

        private string total;

        public string Total
        {
            get
            {
                return total;
            }
            set
            {
                SetProperty(ref total, value);
            }
        }

        private Command cmdCalc;

        public Command CmdCalc
        {
            get
            {
                return cmdCalc ?? (cmdCalc = new Command(() =>
                {
                    try
                    {
                        Total = Model.Calculate(ShiftDate.Date.Add(Start),
                            End.Hours <= 4 ? ShiftDate.Date.AddDays(1).Add(End) : ShiftDate.Date.Add(End),
                            bedTime.Hours <= 4 ? ShiftDate.Date.AddDays(1).Add(BedTime) : ShiftDate.Date.Add(BedTime)).ToString("C0");
                        App.RunOnUiThread(() => UserDialogs.Instance.ShowSuccess(Total));
                    }
                    catch (Exception ex)
                    {
                        App.RunOnUiThread(() => UserDialogs.Instance.ShowError(ex.Message));
                    }
                }));
            }
        }
    }
}
