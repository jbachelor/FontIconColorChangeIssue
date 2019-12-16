using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FontIconColorChanges.iOS.Services;
using FontIconColorChanges.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceThemeService_iOS))]
namespace FontIconColorChanges.iOS.Services
{
    public class DeviceThemeService_iOS : IDeviceThemeService
    {
        //This code is largely based on https://codetraveler.io/2019/09/11/check-for-dark-mode-in-xamarin-forms/

        private int _minimumIosMajorVersion = 12;
        private int _minimumIosMinorVersion = 0;

        public DeviceThemeService_iOS()
        {
            Debug.WriteLine($"**** {this.GetType().Name}:  ctor");
        }

        public async Task<VisualTheme> GetOperatingSystemThemeAsync()
        {
            if (CurrentOSSupportsLightAndDarkMode() == false)
            {
                return VisualTheme.Light;
            }

            var currentUIViewController = await GetVisibleViewController();
            var userInterfaceStyle = currentUIViewController.TraitCollection.UserInterfaceStyle;
            VisualTheme visualThemeToReturn;

            switch (userInterfaceStyle)
            {
                case UIUserInterfaceStyle.Light:
                    visualThemeToReturn = VisualTheme.Light;
                    break;
                case UIUserInterfaceStyle.Dark:
                    visualThemeToReturn = VisualTheme.Dark;
                    break;
                default:
                    visualThemeToReturn = VisualTheme.Light;
                    Debug.WriteLine($"!!!! {this.GetType().Name}.{nameof(GetOperatingSystemThemeAsync)}:  Unexpected UIUserInterfaceStyle: [{userInterfaceStyle}].");
                    break;
            }

            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(GetOperatingSystemThemeAsync)} returning [{visualThemeToReturn}]");
            return visualThemeToReturn;
        }

        private Task<UIViewController> GetVisibleViewController()
        {
            return Device.InvokeOnMainThreadAsync<UIViewController>(() =>
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                UIViewController visibleViewController = null;
                switch (rootController.PresentedViewController)
                {
                    case UINavigationController navigationController:
                        visibleViewController = navigationController.TopViewController;
                        break;
                    case UITabBarController tabBarController:
                        visibleViewController = tabBarController.SelectedViewController;
                        break;
                    case null:
                        visibleViewController = rootController;
                        break;
                    default:
                        visibleViewController = rootController.PresentedViewController;
                        break;
                }
                return visibleViewController;
            });
        }

        private bool CurrentOSSupportsLightAndDarkMode()
        {
            return UIDevice.CurrentDevice.CheckSystemVersion(_minimumIosMajorVersion, _minimumIosMinorVersion);
        }
    }
}
