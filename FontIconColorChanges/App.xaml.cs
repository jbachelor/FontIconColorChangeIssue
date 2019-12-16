using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FontIconColorChanges.Services;
using FontIconColorChanges.Styles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FontIconColorChanges
{
    public partial class App : Application
    {
        private IDeviceThemeService _deviceThemeService;

        public static VisualTheme CurrentAppTheme { get; set; }

        public App()
        {
            Debug.WriteLine($"**** {this.GetType().Name}:  ctor");
            InitializeComponent();

            _deviceThemeService = DependencyService.Get<IDeviceThemeService>();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnStart)}");
            base.OnStart();
            await SetAppThemeAsync();
        }

        protected override void OnSleep()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnSleep)}");
            base.OnSleep();
        }

        protected override async void OnResume()
        {
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(OnResume)}");
            base.OnResume();
            await SetAppThemeAsync();
        }

        private async Task SetAppThemeAsync()
        {
            var theme = await _deviceThemeService.GetOperatingSystemThemeAsync();
            Debug.WriteLine($"**** {this.GetType().Name}.{nameof(SetAppThemeAsync)}:  Switching theme from {CurrentAppTheme} to {theme}");

            CurrentAppTheme = theme;

            switch (theme)
            {
                case VisualTheme.Light:
                    App.Current.Resources = new LightTheme();
                    break;
                case VisualTheme.Dark:
                    App.Current.Resources = new DarkTheme();
                    break;
                default:
                    App.Current.Resources = new LightTheme();
                    Debug.WriteLine($"!!!! {this.GetType().Name}.{nameof(SetAppThemeAsync)}:  Unsupported argument: [{theme}]. Falling back on light theme.");
                    break;
            }
        }
    }
}
