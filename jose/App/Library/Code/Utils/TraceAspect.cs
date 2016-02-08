namespace Library.App.Program.Code.Utils
{
    using Castle.Core.Logging;
    using Castle.DynamicProxy;
    using Infrastructure.Log.Implementation;
    using Infrastructure.Log.Interface;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Xml.Serialization;

    /// <summary>
    /// Trace methods calls for AOP
    /// </summary>
    [DebuggerStepThrough]
    public sealed class TraceAspect : IInterceptor
    {
        private readonly Dictionary<Type, ILoggerImplementation> loggers;
        private readonly Infrastructure.Log.Interface.ILoggerFactory factory;
        private static readonly object Mutex = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceAspect" /> class.
        /// </summary>
        /// <param name="factory">The log factory.</param>
        public TraceAspect(Infrastructure.Log.Interface.ILoggerFactory factory)
        {
            this.factory = factory;
            this.loggers = new Dictionary<Type, ILoggerImplementation>();
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The method invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            if (!this.loggers.ContainsKey(invocation.TargetType))
            {
                lock (Mutex)
                {
                    if (!this.loggers.ContainsKey(invocation.TargetType))
                    {
                        this.loggers.Add(invocation.TargetType, factory.Create(invocation.TargetType.ToString()));
                    }
                }
            }
            ILoggerImplementation logger = this.loggers[invocation.TargetType];
            var info = new MethodTraceInfo()
            {
                MachineName = Environment.MachineName,
                AppDomainName = AppDomain.CurrentDomain.FriendlyName,
                ThreadIdentityName = Thread.CurrentPrincipal.Identity.Name,
                WindowsIdentityName = WindowsIdentity.GetCurrent().Name,
                ClassName = invocation.TargetType.Name,
                MethodName = invocation.Method.Name,
                Dump = CreateInvocationLogInfo(invocation).ToString()
            };
            logger.TraceMethodCall(info);
            invocation.Proceed();
            logger.TraceEndMethodCall(info);
        }

        private static String CreateInvocationLogInfo(IInvocation invocation)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object argument in invocation.Arguments)
            {
                String argumentDescription = argument == null ? "null" : DumpObject(argument);
                sb.Append(argumentDescription).Append(",");
            }
            if (invocation.Arguments.Count() > 0) { sb.Length--; }
            return sb.ToString();
        }

        private static string DumpObject(object argument)
        {
            Type objtype = argument.GetType();
            if (objtype == typeof(String) || objtype.IsPrimitive || !objtype.IsClass) { return objtype.ToString() + ": " + argument.ToString(); }
            if (IsSerializable(argument))
            { return Serialize(argument, objtype); }
            else
            { return "Unserializable"; }

        }

        private static string Serialize(object argument, Type objtype)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var reader = new StreamReader(memoryStream);
                DataContractSerializer serializer = new DataContractSerializer(objtype);
                serializer.WriteObject(memoryStream, argument);
                memoryStream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        private static bool IsSerializable(object obj)
        {
            Type t = obj.GetType();
            return Attribute.IsDefined(t, typeof(DataContractAttribute)) || t.IsSerializable || (obj is IXmlSerializable);
        }
    }
}
