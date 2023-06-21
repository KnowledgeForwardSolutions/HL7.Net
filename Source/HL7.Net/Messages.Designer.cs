﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HL7.Net {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HL7.Net.Messages", typeof(Messages).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field separator character may not be carriage return (hex 0D).
        /// </summary>
        internal static string InvalidFieldSeparator {
            get {
                return ResourceManager.GetString("InvalidFieldSeparator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value must be an integer greater than zero (0) or minus one (-1) to indicate that the field allows unlimited length.
        /// </summary>
        internal static string InvalidMaxLength {
            get {
                return ResourceManager.GetString("InvalidMaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Allowed repetitions must be greater than one (1).
        /// </summary>
        internal static string InvalidNumberOfRepetitionsAllowed {
            get {
                return ResourceManager.GetString("InvalidNumberOfRepetitionsAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value is not &apos;N&apos;, &apos;Y&apos; or a integer greater than one (1).
        /// </summary>
        internal static string InvalidRepetitionSpecification {
            get {
                return ResourceManager.GetString("InvalidRepetitionSpecification", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field {0} has duplicate encoding character {1}.
        /// </summary>
        internal static string LogDuplicateEncodingCharacter {
            get {
                return ResourceManager.GetString("LogDuplicateEncodingCharacter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field {0} has invalid length.
        /// </summary>
        internal static string LogFieldInvalidLength {
            get {
                return ResourceManager.GetString("LogFieldInvalidLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field {0} is not present.
        /// </summary>
        internal static string LogFieldNotPresent {
            get {
                return ResourceManager.GetString("LogFieldNotPresent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field {0} is present.
        /// </summary>
        internal static string LogFieldPresent {
            get {
                return ResourceManager.GetString("LogFieldPresent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field {0} is present but null.
        /// </summary>
        internal static string LogFieldPresentButNull {
            get {
                return ResourceManager.GetString("LogFieldPresentButNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field {0} is present but was truncated to field max length of {1}.
        /// </summary>
        internal static string LogFieldPresentButTruncated {
            get {
                return ResourceManager.GetString("LogFieldPresentButTruncated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Required field {0} is not present.
        /// </summary>
        internal static string LogRequiredFieldNotPresent {
            get {
                return ResourceManager.GetString("LogRequiredFieldNotPresent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value is null, String.Empty or all whitespace characters.
        /// </summary>
        internal static string StringValueIsNullOrWhiteSpace {
            get {
                return ResourceManager.GetString("StringValueIsNullOrWhiteSpace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value is not a defined member of the {0} enumeration.
        /// </summary>
        internal static string UndefinedEnumValue {
            get {
                return ResourceManager.GetString("UndefinedEnumValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value is not a defined member of the Optionality enumeration..
        /// </summary>
        internal static string UndefinedOptionality {
            get {
                return ResourceManager.GetString("UndefinedOptionality", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value may not be less than {0}.
        /// </summary>
        internal static string ValueLessThan {
            get {
                return ResourceManager.GetString("ValueLessThan", resourceCulture);
            }
        }
    }
}
