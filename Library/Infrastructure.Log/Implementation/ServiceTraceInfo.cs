namespace Infrastructure.Log.Implementation
{

    /// <summary>
    /// Runtime information for service operation
    /// </summary>
    public class ServiceTraceInfo : MethodTraceInfo
    {
        /// <summary>
        /// Gets or sets the name of the service called.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation executed.
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }
            if (typeof(ServiceTraceInfo) != obj.GetType()) { return false; }
            var comp = obj as ServiceTraceInfo;
            return
                comp.MachineName == this.MachineName &&
                comp.AppDomainName == this.AppDomainName &&
                comp.ClassName == this.ClassName &&
                comp.Dump == this.Dump &&
                comp.MachineName == this.MachineName &&
                comp.MethodName == this.MethodName &&
                comp.ThreadIdentityName == this.ThreadIdentityName &&
                comp.WindowsIdentityName == this.WindowsIdentityName &&
                comp.ServiceName == this.ServiceName &&
                comp.OperationName == this.OperationName;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            int hash = 32;
            if (!string.IsNullOrEmpty(MachineName))
            {
                hash = 17 * hash + MachineName.GetHashCode();
            }
            if (!string.IsNullOrEmpty(AppDomainName))
            {
                hash = 17 * hash + AppDomainName.GetHashCode();
            }
            if (!string.IsNullOrEmpty(ClassName))
            {
                hash = 17 * hash + ClassName.GetHashCode();
            }
            if (!string.IsNullOrEmpty(MethodName))
            {
                hash = 17 * hash + MethodName.GetHashCode();
            }
            if (!string.IsNullOrEmpty(ThreadIdentityName))
            {
                hash = 17 * hash + ThreadIdentityName.GetHashCode();
            }
            if (!string.IsNullOrEmpty(WindowsIdentityName))
            {
                hash = 17 * hash + WindowsIdentityName.GetHashCode();
            }
            if (!string.IsNullOrEmpty(Dump))
            {
                hash = 17 * hash + Dump.GetHashCode();
            }
            if (!string.IsNullOrEmpty(ServiceName))
            {
                hash = 17 * hash + ServiceName.GetHashCode();
            }
            if (!string.IsNullOrEmpty(OperationName))
            {
                hash = 17 * hash + OperationName.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Convert to the message trace format.
        /// </summary>
        /// <returns>A string with serialized info.</returns>
        internal override string ToMessageTrace()
        {
            return string.Format("Call in {0}-{1} {2}/{3} with [Pricipal={4}, Windows={5}] /n Called {6}.{7}({8})", MachineName, AppDomainName, ServiceName, OperationName, ThreadIdentityName, WindowsIdentityName, ClassName, MethodName, Dump);
        }
    }

}
