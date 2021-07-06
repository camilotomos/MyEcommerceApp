using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Plugin.CurrentActivity;
using EcommerceApp.PlatformSpecifics;

[assembly: Dependency(typeof(EcommerceApp.Droid.PlatformSpecific.SetupTheme))]

namespace EcommerceApp.Droid.PlatformSpecific
{
    public class SetupTheme : ISetupTheme
    {
        public void SetStatusBarColor(Color color) 
        {
            var androidColor = color.ToAndroid();
            CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(androidColor);

        }
    }
}