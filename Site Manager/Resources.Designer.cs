﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Site_Manager {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Site_Manager.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Site Manager.
        /// </summary>
        public static string AppTitle {
            get {
                return ResourceManager.GetString("AppTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data Source=localhost\SQLEXPRESS;Initial Catalog=Site_Management;Integrated Security=True.
        /// </summary>
        public static string SQLConnString {
            get {
                return ResourceManager.GetString("SQLConnString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (Short_Name, Full_Name, Customer_Name, Address).
        /// </summary>
        public static string tblSiteFields {
            get {
                return ResourceManager.GetString("tblSiteFields", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dbo.Site_Info.
        /// </summary>
        public static string tblSiteInfo {
            get {
                return ResourceManager.GetString("tblSiteInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VALUES (@short_name, @full_name, @customer_name, @address).
        /// </summary>
        public static string tblSiteInsert {
            get {
                return ResourceManager.GetString("tblSiteInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dbo.Site_Users.
        /// </summary>
        public static string tblSiteUsers {
            get {
                return ResourceManager.GetString("tblSiteUsers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dbo.User_Info.
        /// </summary>
        public static string tblUserInfo {
            get {
                return ResourceManager.GetString("tblUserInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] tbUser {
            get {
                object obj = ResourceManager.GetObject("tbUser", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
