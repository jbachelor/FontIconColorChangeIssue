using System;
using System.Threading.Tasks;
using Android.Content.Res;
using Android.OS;
using FontIconColorChanges.Droid.Services;
using FontIconColorChanges.Services;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceThemeService_Android))]
namespace FontIconColorChanges.Droid.Services
{
    public class DeviceThemeService_Android : IDeviceThemeService
    {
        //This code is largely based on https://codetraveler.io/2019/09/11/check-for-dark-mode-in-xamarin-forms/

        private BuildVersionCodes minimumAndroidVersion = BuildVersionCodes.Froyo;

        public DeviceThemeService_Android()
        {
            System.Diagnostics.Debug.WriteLine($"**** {this.GetType().Name}  ctor");
        }

        public Task<VisualTheme> GetOperatingSystemThemeAsync()
        {
            VisualTheme visualThemeToReturn;

            if (Build.VERSION.SdkInt < minimumAndroidVersion)
            {
                System.Diagnostics.Debug.WriteLine($"**** {this.GetType().Name}.{nameof(GetOperatingSystemThemeAsync)}:  Dark mode is not supported on Android {Build.VERSION.SdkInt}. Returning light mode as a default.");
                return Task.FromResult(VisualTheme.Light);
            }

            var uiModeFlags = CrossCurrentActivity.Current.AppContext.Resources.Configuration.UiMode & UiMode.NightMask;

            switch (uiModeFlags)
            {
                case UiMode.NightYes:
                    visualThemeToReturn = VisualTheme.Dark;
                    break;
                case UiMode.NightNo:
                    visualThemeToReturn = VisualTheme.Light;
                    break;
                default:
                    visualThemeToReturn = VisualTheme.Light;
                    System.Diagnostics.Debug.WriteLine($"!!!! {this.GetType().Name}.{nameof(GetOperatingSystemThemeAsync)}:  Unexpected UiModeFlags: [{uiModeFlags}].");
                    break;
            }

            System.Diagnostics.Debug.WriteLine($"**** {this.GetType().Name}.{nameof(GetOperatingSystemThemeAsync)} returning [{visualThemeToReturn}]");
            return Task.FromResult(visualThemeToReturn);
        }
    }
}
