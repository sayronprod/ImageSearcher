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
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, StartupService.AppName, out createdNew))
            {
                if (createdNew)
                {
                    StartupService.AddToSturtup();
                    ApplicationConfiguration.Initialize();
                    Application.Run(new MainForm());
                }
            }
        }
    }
}