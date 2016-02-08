using Infrastructure.Log.Implementation;

namespace Infrastructure.Log.Interface
{
    using System;

    /// <summary>
    /// Represent the logger functionality of Infrastructure
    /// </summary>
    public interface ILoggerImplementation
    {
        /// <summary>
        /// Write the specified exeption at debug priority.
        /// </summary>
        /// <param name="ex">The exeption.</param>
        /// <param name="message">The message.</param>
        void Debug(Exception ex, string message = null);

        /// <summary>
        /// Write the specified exeption at warn priority.
        /// </summary>
        /// <param name="ex">The exeption.</param>
        /// <param name="message">The message.</param>
        void Warn(Exception ex, string message = null);

        /// <summary>
        /// Write the specified exeption at error priority.
        /// </summary>
        /// <param name="ex">The exeption.</param>
        /// <param name="message">The message.</param>
        void Error(Exception ex, string message = null);

        /// <summary>
        /// Write the specified exeption at fatal priority.
        /// </summary>
        /// <param name="ex">The exeption.</param>
        /// <param name="message">The message.</param>
        void Fatal(Exception ex, string message = null);

        /// <summary>
        /// Traces the method calls.
        /// </summary>
        /// <param name="info">The runtime information.</param>
        void TraceMethodCall(MethodTraceInfo info);

        /// <summary>
        /// Traces the end method call.
        /// </summary>
        /// <param name="info">The information.</param>
        void TraceEndMethodCall(MethodTraceInfo info);

        /// <summary>
        /// Traces the service calls.
        /// </summary>
        /// <param name="info">The runtime information.</param>
        void TraceServiceCall(ServiceTraceInfo info);

        /// <summary>
        /// Traces the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Trace(string message);

        /// <summary>
        /// Determines whether [is trace enabled].
        /// </summary>
        /// <returns><c>true</c> if trace is enabled.</returns>
        bool IsTraceEnabled();
    }

}
