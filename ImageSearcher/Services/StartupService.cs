using Microsoft.Win32;

namespace ImageSearcher.Services
{
    internal class StartupService
    {
        public static string AppName { get; } = "Screenshot Searcher";
        public static void AddToSturtup()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (!IsInStartup())
            {
                rkApp.SetValue(AppName, Application.ExecutablePath.ToString());
            }
        }

        public static void DeleteFromStartup()
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (IsInStartup())
            {
                rkApp.DeleteValue(AppName, false);
            }
        }

        public static bool IsInStartup()
        {
            bool result = false;
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp.GetValue(AppName) != null)
            {
                result = true;
            }
            return result;
        }
    }
}
