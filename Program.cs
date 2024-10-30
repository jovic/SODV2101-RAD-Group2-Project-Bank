using DevExpress.XtraWaitForm;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Bank
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var splashScreen = new SplashScreen())
            {
                splashScreen.TopMost = true;
                splashScreen.Show();

                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += (s, e) =>
                {
                    System.Threading.Thread.Sleep(3000);
                };

                backgroundWorker.RunWorkerCompleted += (s, e) =>
                {
                    splashScreen.Close();
                };
                backgroundWorker.RunWorkerAsync();

                Application.Run(new LoginForm());
            }
        }
    }
}
