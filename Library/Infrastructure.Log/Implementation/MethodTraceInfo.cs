
namespace Infrastructure.Log.Implementation
{
    /// <summary>
    /// Runtime information for method
    /// </summary>
    public class MethodTraceInfo
    {
        /// <summary>
        /// Gets or sets the name of the machine where method is executed.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the name of the application domain where the method is called.
        /// </summary>
        public string AppDomainName { get; set; }

        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the name of the method executed.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the name of the thread identity.
        /// </summary>
        public string ThreadIdentityName { get; set; }

        /// <summary>
        /// Gets or sets the name of the windows identity.
        /// </summary>
        public string WindowsIdentityName { get; set; }

        /// <summary>
        /// Gets or sets the object dump.
        /// </summary>
        public string Dump { get; set; }

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
            if (typeof(MethodTraceInfo) != obj.GetType()) { return false; }
            var comp = obj as MethodTraceInfo;
            return
                comp.MachineName == this.MachineName &&
                comp.AppDomainName == this.AppDomainName &&
                comp.ClassName == this.ClassName &&
                comp.Dump == this.Dump &&
                comp.MachineName == this.MachineName &&
                comp.MethodName == this.MethodName &&
                comp.ThreadIdentityName == this.ThreadIdentityName &&
                comp.WindowsIdentityName == this.WindowsIdentityName;
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
            return hash;
        }

        /// <summary>
        /// Convert to the message trace format.
        /// </summary>
        /// <returns>A string with serialized info.</returns>
        internal virtual string ToMessageTrace()
        {
            return string.Format("Call in {0}-{1} with [Pricipal={2}, Windows={3}] Called {4}.{5}({6})", MachineName, AppDomainName, ThreadIdentityName, WindowsIdentityName, ClassName, MethodName, Dump);
        }

        /// <summary>
        /// Convert to the end of message trace format.
        /// </summary>
        /// <returns>A string with serialized info.</returns>
        internal virtual string ToEndMessageTrace()
        {
            return string.Format("Exit from call out {0}-{1} with [Pricipal={2}, Windows={3}] Called {4}.{5}({6})", MachineName, AppDomainName, ThreadIdentityName, WindowsIdentityName, ClassName, MethodName, Dump);
        }
    }
}