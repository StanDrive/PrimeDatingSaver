using System;
using System.IO;
using NLog;
using NLog.Config;

namespace PrimeDating.BusinessLayer
{
    public class Nlogger : Interfaces.ILogger
    {
        private static readonly object Locker;

        private static readonly Logger Logger;

        private const string DefaultContext = "General";

        /// <summary>
        /// Initializes the <see cref="Nlogger"/> class.
        /// </summary>
        static Nlogger()
        {
            Locker = new object();

            var baseDir = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);

            LogManager.Configuration = new XmlLoggingConfiguration(Path.Combine(baseDir, "Nlog.config"));

            Logger = LogManager.GetLogger("*");
        }

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        public void Trace(string message, string context = DefaultContext)
        {
            Log(LogLevel.Trace, message, null, context);
        }

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        public void TraceException(string message, Exception exception, string context = DefaultContext)
        {
            Log(LogLevel.Trace, message, exception, context);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        public void Debug(string message, string context = DefaultContext)
        {
            Log(LogLevel.Debug, message, null, context);
        }

        /// <summary>
        /// Debugs the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        public void DebugException(string message, Exception exception, string context = DefaultContext)
        {
            Log(LogLevel.Debug, message, exception, context);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        public void Info(string message, string context = DefaultContext)
        {
            Log(LogLevel.Info, message, null, context);
        }

        /// <summary>
        /// Informations the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        public void InfoException(string message, Exception exception, string context = DefaultContext)
        {
            Log(LogLevel.Info, message, exception, context);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        public void Warning(string message, string context = DefaultContext)
        {
            Log(LogLevel.Warn, message, null, context);
        }

        /// <summary>
        /// Warnings the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        public void WarningException(string message, Exception exception, string context = DefaultContext)
        {
            Log(LogLevel.Warn, message, exception, context);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        public void Error(string message, string context = DefaultContext)
        {
            Log(LogLevel.Error, message, null, context);
        }

        /// <summary>
        /// Errors the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        public void ErrorException(string message, Exception exception, string context = DefaultContext)
        {
            Log(LogLevel.Error, message, exception, context);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        public void Fatal(string message, string context = DefaultContext)
        {
            Log(LogLevel.Fatal, message, null, context);
        }

        /// <summary>
        /// Fatals the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        public void FatalException(string message, Exception exception, string context = DefaultContext)
        {
            Log(LogLevel.Fatal, message, exception, context);
        }

        #region private

        private static void Log(LogLevel level, string message, Exception ex, string context)
        {
            try
            {
                var logMessage = ex != null ? $"{message}. {GetErrorMessage(ex)}" : message;

                var eventInfo =
                    new LogEventInfo(level,
                        Logger.Name,
                        logMessage)
                    {
                        Exception = ex
                    };

                eventInfo.Properties["Context"] = string.IsNullOrWhiteSpace(context) ? DefaultContext : context;

                Logger.Log(eventInfo);
            }
            catch (Exception e)
            {
                lock (Locker)
                {
                    try
                    {
                        File.AppendAllText("C:\\Logs\\PrimeDatingSaver\\NLog error report.txt",
                            $@"{DateTime.Now}|||Incoming message: {message}, {ex?.Message}|||Shutdown message: {e.Message}");
                    }
                    catch{ }
                }
            }
        }

        private static string GetErrorMessage(Exception ex)
        {
            var message = ex.Message;

            if (!string.IsNullOrWhiteSpace(ex.InnerException?.Message))
            {
                message += $". {GetErrorMessage(ex.InnerException)}";
            }

            return message;
        }
        #endregion
    }
}