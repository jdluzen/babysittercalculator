using System;
using Xamarin.Forms;
using System.ServiceModel;

namespace BabysitterCalculator
{
    public class MainView : ContentPage<BabysitterViewModel>
    {
        public MainView()
            : base(new BabysitterViewModel())
        {
            Title = "Babysitter Calculator";
            Entry txtStartToBedRate = new Entry { Keyboard = Keyboard.Numeric, Placeholder = "Start to bed rate" };
            Entry txtBedToMidnightRate = new Entry { Keyboard = Keyboard.Numeric, Placeholder = "Bed to midnight rate" };
            Entry txtMidnightToEndRate = new Entry { Keyboard = Keyboard.Numeric, Placeholder = "Midnight to end rate" };
            txtStartToBedRate.SetBinding<BabysitterViewModel>(Entry.TextProperty, vm => vm.StartToBedRate);
            txtBedToMidnightRate.SetBinding<BabysitterViewModel>(Entry.TextProperty, vm => vm.BedToMidnightRate);
            txtMidnightToEndRate.SetBinding<BabysitterViewModel>(Entry.TextProperty, vm => vm.MidnightToEndRate);
            DatePicker shiftDate = new DatePicker();
            TimePicker start = new TimePicker();
            TimePicker end = new TimePicker();
            TimePicker bedTime = new TimePicker();
            shiftDate.SetBinding<BabysitterViewModel>(DatePicker.DateProperty, vm => vm.ShiftDate);
            start.SetBinding<BabysitterViewModel>(TimePicker.TimeProperty, vm => vm.Start);
            end.SetBinding<BabysitterViewModel>(TimePicker.TimeProperty, vm => vm.End);
            bedTime.SetBinding<BabysitterViewModel>(TimePicker.TimeProperty, vm => vm.BedTime);
            Label lblTotal = new Label { FontSize = 24 };
            lblTotal.SetBinding<BabysitterViewModel>(Label.TextProperty, vm => vm.Total);
            Button btnCalc = new Button { Text = "Calculate" };
            btnCalc.SetBinding<BabysitterViewModel>(Button.CommandProperty, mv => mv.CmdCalc);
            Content = new StackLayout
            {
                Children =
                {
                    new ScrollView
                    {
                        Content = new StackLayout
                        {
                            Children =
                            {
                                new Label { Text = "Start to bed rate" },
                                txtStartToBedRate,
                                new Label { Text = "Bed to midnight rate" },
                                txtBedToMidnightRate,
                                new Label { Text = "Midnight to end rate" },
                                txtMidnightToEndRate,
                                new Label { Text = "Shift date" },
                                shiftDate,
                                new Label { Text = "Start time" },
                                start,
                                new Label { Text = "End time" },
                                end,
                                new Label { Text = "Bed time" },
                                bedTime,
                                new Label { Text = "Total" },
                                lblTotal,
                            }
                        }
                    },
                    btnCalc
                }
            };
        }
    }
}
