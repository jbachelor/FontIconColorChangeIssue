using System;
using System.Threading.Tasks;

namespace FontIconColorChanges.Services
{
    public interface IDeviceThemeService
    {
        Task<VisualTheme> GetOperatingSystemThemeAsync();
    }
}
