﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrossCutting.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Display {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Display() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CrossCutting.Resources.Display", typeof(Display).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ¿Desea alquilar otro libro? (S)i o (N)o.
        /// </summary>
        public static string AskForAnotherBook {
            get {
                return ResourceManager.GetString("AskForAnotherBook", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ¿Quiere (R)egistrar un libro, hacer un (P)réstamo, una (D)evolución o (S)alir:.
        /// </summary>
        public static string AskUser {
            get {
                return ResourceManager.GetString("AskUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El libro {0} está disponible para ser prestado.
        /// </summary>
        public static string BookIsAvailable {
            get {
                return ResourceManager.GetString("BookIsAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Libros que tiene préstados: {0}.
        /// </summary>
        public static string BooksLent {
            get {
                return ResourceManager.GetString("BooksLent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Introduzca el libro:.
        /// </summary>
        public static string IntroduceBook {
            get {
                return ResourceManager.GetString("IntroduceBook", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Idioma del libro: Español &apos;es&apos; o Inglés &apos;en&apos;.
        /// </summary>
        public static string IntroduceBookLanguage {
            get {
                return ResourceManager.GetString("IntroduceBookLanguage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Introduzca el título del libro:.
        /// </summary>
        public static string IntroduceBookName {
            get {
                return ResourceManager.GetString("IntroduceBookName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Introduzca la fecha del préstamo:.
        /// </summary>
        public static string IntroduceDataLoan {
            get {
                return ResourceManager.GetString("IntroduceDataLoan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Introduzca el usuario:.
        /// </summary>
        public static string IntroduceUser {
            get {
                return ResourceManager.GetString("IntroduceUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Introduzca la fecha de la devolución:.
        /// </summary>
        public static string IntroduzceReturnDate {
            get {
                return ResourceManager.GetString("IntroduzceReturnDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Préstamo realizado. Fecha de devolución: {0}.
        /// </summary>
        public static string LoanSuccessful {
            get {
                return ResourceManager.GetString("LoanSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No tiene ningún libro en préstamo.
        /// </summary>
        public static string NotBooksToReturn {
            get {
                return ResourceManager.GetString("NotBooksToReturn", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La devolución se ha realizado tarde, por tanto debe pagar una multa antes de alquilar más libros. ¿Desea pagarla ahora? (S)i o (N)o.
        /// </summary>
        public static string NotReturnOnTime {
            get {
                return ResourceManager.GetString("NotReturnOnTime", resourceCulture);
            }
        }
    }
}
