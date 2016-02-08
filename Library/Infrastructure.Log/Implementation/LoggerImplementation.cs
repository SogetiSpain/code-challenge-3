using System;
using System.Collections.Generic;
using Infrastructure.Log.Interface;

namespace Infrastructure.Log.Implementation
{
    using log4net;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// NLog implementation for ILogger.
    /// </summary>
    public sealed class LoggerImplementation : ILoggerImplementation
    {
        private ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerImplementation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LoggerImplementation(string name)
        {
            logger = LogManager.GetLogger(name);
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        internal ILog Logger
        {
            get { return logger; }
            set { logger = value; }
        }

        /// <summary>
        /// Write the specified exeption at debug priority.
        /// </summary>
        /// <param name="ex">The exeption.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Debug(Exception ex, string message = null)
        {
            logger.Debug(message, ex);
        }

        /// <summary>
        /// Write the specified exeption at warn priority.
        /// </summary>
        /// <param name="ex">The exeption.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Warn(Exception ex, string message = null)
        {
            logger.Warn(message, ex);
        }

        /// <summary>
        /// Write the specified exeption at error priority.
        /// </summary>
        /// <param name="ex">The exeption.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Error(Exception ex, string message = null)
        {
            logger.Error(message, ex);
        }

        /// <summary>
        /// Write the specified exeption at fatal priority.
        /// </summary>
        /// <param name="ex">The exeption.</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Fatal(Exception ex, string message = null)
        {
            logger.Fatal(message, ex);
        }

        /// <summary>
        /// Traces the method calls.
        /// </summary>
        /// <param name="info">The runtime information.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void TraceMethodCall(MethodTraceInfo info)
        {
            logger.Info(info.ToMessageTrace());
        }

        /// <summary>
        /// Traces the method calls.
        /// </summary>
        /// <param name="info">The runtime information.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void TraceEndMethodCall(MethodTraceInfo info)
        {
            logger.Info(info.ToEndMessageTrace());
        }

        /// <summary>
        /// Traces the service calls.
        /// </summary>
        /// <param name="info">The runtime information.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void TraceServiceCall(ServiceTraceInfo info)
        {
            logger.Info(info.ToMessageTrace());
        }

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Trace(string message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Determines whether [is trace enabled].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if trace is enabled.
        /// </returns>
        public bool IsTraceEnabled()
        {
            return this.logger.IsInfoEnabled;
        }
    }
}
