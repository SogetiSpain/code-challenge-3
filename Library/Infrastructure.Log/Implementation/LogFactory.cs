namespace Infrastructure.Log.Implementation
{
    using System;
    using Infrastructure.Log.Interface;

    /// <summary>
    /// Log factory for NLog
    /// </summary>
    public class LogFactory : ILoggerFactory
    {
        /// <summary>
        /// Creates the specified object type logger.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// A <see cref="ILogger" /> instance for the type.
        /// </returns>
        public ILoggerImplementation Create(Type objectType)
        {
            return this.Create(objectType.ToString());
        }

        /// <summary>
        /// Creates the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object logger.</param>
        /// <returns>
        /// A <see cref="ILogger" /> instance for the type.
        /// </returns>
        public ILoggerImplementation Create(string objectType)
        {
            return new LoggerImplementation(objectType);
        }
    }
}
