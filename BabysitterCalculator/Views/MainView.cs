using System;
using Xamarin.Forms;

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
            Content = new StackLayout
            {
                Children =
                {
                    txtStartToBedRate,
                    txtBedToMidnightRate,
                    txtMidnightToEndRate
                }
            };
        }
    }
}
