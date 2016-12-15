using Lorenzo.WorkWatcher.Core.Common;
using Lorenzo.WorkWatcher.Views;
using SimpleInjector;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Lorenzo.WorkWatcher
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // kriticke chyby - globalni pad aplikace
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            // inicializace kontajneru
            var container = SimpleInjectorContainer.Build();
            using (container.BeginLifetimeScope())
            {
                var shellView = container.GetInstance<IShellView>();
                var log = container.GetInstance<ILogger>();

                log.Trace("Run app");
                // beh hlavniho okna
                Application.Run(shellView as Form);
                log.Trace("close app");
            }
        }


        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Fatal(e.Exception, "Unhandled thread exception");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Fatal("Unhandled domain exception {0}", e.ExceptionObject);
        }
    }
}
