using System;

namespace PrimeDating.BusinessLayer.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        void Trace(string message, string context = "");

        /// <summary>
        /// Traces the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        void TraceException(string message, Exception exception, string context = "");

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        void Debug(string message, string context = "");

        /// <summary>
        /// Debugs the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        void DebugException(string message, Exception exception, string context = "");

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        void Info(string message, string context = "");

        /// <summary>
        /// Informations the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        void InfoException(string message, Exception exception, string context = "");

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        void Warning(string message, string context = "");

        /// <summary>
        /// Warnings the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        void WarningException(string message, Exception exception, string context = "");

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        void Error(string message, string context = "");

        /// <summary>
        /// Errors the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        void ErrorException(string message, Exception exception, string context = "");

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="context">The context.</param>
        void Fatal(string message, string context = "");

        /// <summary>
        /// Fatals the exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="context">The context.</param>
        void FatalException(string message, Exception exception, string context = "");
    }
}
