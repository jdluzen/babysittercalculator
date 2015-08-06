using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using NativeCode.Mobile.AppCompat.FormsAppCompat;
using NativeCode.Mobile.AppCompat.Renderers;
using Acr.UserDialogs;

namespace BabysitterCalculator.Droid
{
    [Activity(Label = "Babysitter Calculator", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = CompatThemeLightDarkActionBar)]
    public class MainActivity : AppCompatFormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine(e.ExceptionObject);
            };
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsAppCompat.EnableAll();
            if (UserDialogs.Instance == null)
                UserDialogs.Init(() => this);
            LoadApplication(new App());
        }
    }
}
