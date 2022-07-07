using ImageSearcher.Services;

namespace ImageSearcher
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !DEBUG
            StartupService.AddToSturtup();
#endif
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}