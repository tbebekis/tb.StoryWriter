namespace StoryWriter
{
    internal static class Program
    {
        // ● private
        /// <summary>
        /// It is used by the InstanceManager to control application instancing
        /// </summary>                                                              
        const string AppUniqueId = "{F58979E6-6DFD-4F95-9250-638D289E8BFA}";

        /// <summary>
        /// this is of a little use because it actually is a notification about an exception, NOT a catcher. 
        /// <para>WARNING: An unhandled exception in ANY thread terminates the application. </para>
        /// </summary>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null && !e.IsTerminating)
            {
                try
                {
                    string Text = ex.GetErrorText();
                    LogBox.AppendLine(Text); 
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// Global exception catcher for unhandled exceptions thrown by the primary (UI) thread.
        /// </summary>
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.Exception;
                string Text = "Unknown error";

                if (ex != null)
                {
                    try
                    {
                        Text = ex.GetErrorText();
                        LogBox.AppendLine(Text);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        static bool IsSingleInstance()
        {
            bool Result = true;
            using (var im = new InstanceManager(AppUniqueId))
            {
                Result = im.IsSingleInstance;
            }

            return Result;
        }

        // ● construction 
        /// <summary>
        /// Static constructor. Setups unhandled exception event handlers.
        /// </summary>
        static Program()
        {
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if (!IsSingleInstance())
            {
                MessageBox.Show("This application is already running!", "Batch Orders", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            Application.Run(new MainForm());


        }
    }
}