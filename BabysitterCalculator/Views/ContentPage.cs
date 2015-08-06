using System;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace BabysitterCalculator
{
    public abstract class ContentPage<T> : ContentPage where T : ViewModel
    {
        public T ViewModel { get; set; }

        protected ContentPage(T vm)
        {
            BindingContext = ViewModel = vm;
        }
    }
}
