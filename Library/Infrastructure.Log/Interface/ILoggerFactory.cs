
namespace Infrastructure.Log.Interface
{
    using System;

    /// <summary>
    /// Logger factory
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Creates the specified object type logger.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>A <see cref="ILogger"/> instance for the type.</returns>
        ILoggerImplementation Create(Type objectType);

        /// <summary>
        /// Creates the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object logger.</param>
        /// <returns>A <see cref="ILogger"/> instance for the type.</returns>
        ILoggerImplementation Create(string objectType);
    }
}
