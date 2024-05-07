using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Sharpnado.CollectionView.Droid;

namespace PfeShell.Droid
{
    [Activity(Label = "OMRANE_ESTATE", Icon = "@drawable/LogoNew", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjMyMTc5QDMyMzAyZTMxMmUzMGczS2tSN3h6ME1Xcm1EZWRjSzI4cjFJUVFQWW81cE9iRm00dWIwcnYxUDA9");
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Initializer.Initialize();
            Rg.Plugins.Popup.Popup.Init(this);
            LoadApplication(new App());

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}