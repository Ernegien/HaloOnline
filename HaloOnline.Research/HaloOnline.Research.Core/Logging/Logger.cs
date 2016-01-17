using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using NLog;

namespace HaloOnline.Research.Core.Logging
{
    /// <summary>
    /// Generic NLog wrapper.
    /// </summary>
    public sealed class Logger
    {
        /// <summary>
        /// Returns the singleton instance of the <see cref="Logger"/> class.
        /// </summary>
        public static Logger Instance { get; } = new Logger();

        /// <summary>
        /// Gets or sets the enabled status of the logger.
        /// </summary>
        public bool IsEnabled
        {
            get { return LogManager.IsLoggingEnabled(); }
            set
            {
                if (value)
                {
                    while (!IsEnabled) LogManager.EnableLogging();
                }
                else
                {
                    while (IsEnabled) LogManager.DisableLogging();
                }
            }
        }

        /// <summary>
        /// Disallows construction of the <see cref="Logger"/> class. Must be accessed by <see cref="Instance"/> instead.
        /// </summary>
        private Logger()
        {

        }

        /// <summary>
        /// Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="callerPath"></param>
        /// <param name="callerMember"></param>
        /// <param name="callerLine"></param>
        public void Trace(string message, Exception exception = null,
            [CallerFilePath] string callerPath = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0)
        {
            Log(LogLevel.Trace, message, exception, callerPath, callerMember, callerLine);
        }

        /// <summary>
        /// Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="callerPath"></param>
        /// <param name="callerMember"></param>
        /// <param name="callerLine"></param>
        public void Debug(string message, Exception exception = null,
            [CallerFilePath] string callerPath = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0)
        {
            Log(LogLevel.Debug, message, exception, callerPath, callerMember, callerLine);
        }

        /// <summary>
        /// Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="callerPath"></param>
        /// <param name="callerMember"></param>
        /// <param name="callerLine"></param>
        public void Info(string message, Exception exception = null,
            [CallerFilePath] string callerPath = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0)
        {
            Log(LogLevel.Info, message, exception, callerPath, callerMember, callerLine);
        }

        /// <summary>
        /// Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="callerPath"></param>
        /// <param name="callerMember"></param>
        /// <param name="callerLine"></param>
        public void Warn(string message, Exception exception = null,
            [CallerFilePath] string callerPath = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0)
        {
            Log(LogLevel.Warn, message, exception, callerPath, callerMember, callerLine);
        }

        /// <summary>
        /// Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="callerPath"></param>
        /// <param name="callerMember"></param>
        /// <param name="callerLine"></param>
        public void Error(string message, Exception exception = null,
            [CallerFilePath] string callerPath = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0)
        {
            Log(LogLevel.Error, message, exception, callerPath, callerMember, callerLine);
        }

        /// <summary>
        /// Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="callerPath"></param>
        /// <param name="callerMember"></param>
        /// <param name="callerLine"></param>
        public void Fatal(string message, Exception exception = null,
            [CallerFilePath] string callerPath = "",
            [CallerMemberName] string callerMember = "",
            [CallerLineNumber] int callerLine = 0)
        {
            Log(LogLevel.Fatal, message, exception, callerPath, callerMember, callerLine);
        }

        /// <summary>
        /// Writes the specified diagnostic message.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="callerPath"></param>
        /// <param name="callerMember"></param>
        /// <param name="callerLine"></param>
        private static void Log(LogLevel level, string message, Exception exception = null, string callerPath = "", string callerMember = "", int callerLine = 0)
        {
            try
            {
                // get the source-file-specific logger
                var logger = LogManager.GetLogger(callerPath);

                // quit processing any further if not enabled for the requested logging level
                if (!logger.IsEnabled(level)) return;

                // log the event with caller information bound to it
                var logEvent = new LogEventInfo(level, callerPath, message) { Exception = exception };
                logEvent.Properties.Add("callerpath", callerPath);
                logEvent.Properties.Add("callermember", callerMember);
                logEvent.Properties.Add("callerline", callerLine);
                logger.Log(logEvent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
