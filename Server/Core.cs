using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServTK.Diagnostics;
using ServTK.Eventing;
using ServTK.Helpers;
using ServTK.Networking;

namespace ServTK
{
    public class Core
    {
        private static bool _debug = false;

        private static AuthServer _authServer;

        public static bool Debug
        {
            get { return _debug; }
        }

        public static string Arguments
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (Debug)
                {
                    Utility.Separate(sb, "-debug", " ");
                }

                return sb.ToString();
            }
        }

        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;

            foreach (string arg in args)
            {
                if (StringCompare.Equals(arg, "-debug"))
                    _debug = true;
            }

            Logger.Seperator();
            Logger.Info("ServTK - [http://servtk.com]");

            string arguments = Arguments;

            if (arguments.Length > 0)
            {
                Logger.Info("Running with: {0}", arguments);
            }

            Logger.Seperator();

            Logger.Info("Loading Configuration Files");
            Logger.Log("Auth server IP = " + Config.Settings.Auth.Ip);
            Logger.Info("Finished Loading Configuration Files");

            Logger.Info("Acquiring Authentication Server");
            _authServer = (AuthServer) Server.Acquire(typeof (AuthServer));

            Logger.Info("Initializing Authentication Server");
            _authServer.Initialize();

            Logger.Info("Starting Authentication Server");
            _authServer.Thread.Start();

            Console.ReadLine();
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.IsTerminating)
                Logger.Error(e.ExceptionObject.ToString());
            else
                Logger.Warning(e.ExceptionObject.ToString());

            if (e.IsTerminating)
            {
                try
                {
                    CrashedEventArgs args = new CrashedEventArgs(e.ExceptionObject as Exception);

                    EventSink.InvokeCrashed(args);
                }
                catch
                {
                }

                Logger.Error("This exception is fatal, press return to exit");
                Console.ReadLine();

                Kill();
            }
        }

        private static void ProcessExitHandler(object sender, EventArgs e)
        {
            HandleClosed();
        }

        public static void Kill()
        {
            HandleClosed();

            Process.GetCurrentProcess().Kill();
        }

        private static void HandleClosed()
        {
            Logger.Log("Calling HandleClosed, although it doens't do anything at this point.");
        }
    }
}
