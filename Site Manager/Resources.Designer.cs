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
        ///   Looks up a localized string similar to Jeremy Huff.
        /// </summary>
        public static string AppAuthor {
            get {
                return ResourceManager.GetString("AppAuthor", resourceCulture);
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
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] btnAdd_Users {
            get {
                object obj = ResourceManager.GetObject("btnAdd_Users", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] btnRemove_Users {
            get {
                object obj = ResourceManager.GetObject("btnRemove_Users", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] Close_Tab {
            get {
                object obj = ResourceManager.GetObject("Close_Tab", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Icons made by Smashicons from Flaticon..
        /// </summary>
        public static string ResourceProviders {
            get {
                return ResourceManager.GetString("ResourceProviders", resourceCulture);
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
        ///   Looks up a localized string similar to (Short_Name, Full_Name, Customer_Name, Address, Job_Number).
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
        ///   Looks up a localized string similar to VALUES (@short_name, @full_name, @customer_name, @address @job_number).
        /// </summary>
        public static string tblSiteInsert {
            get {
                return ResourceManager.GetString("tblSiteInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SET Short_Name = @short_name, Full_Name = @full_name, Customer_Name = @customer_name, Address = @address, Job_Number = @job_number WHERE Site_ID = .
        /// </summary>
        public static string tblSiteUpdate {
            get {
                return ResourceManager.GetString("tblSiteUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (Site_ID, User_ID, View_All_Resources, Add_Resources, Modify_Resources, Delete_Resources, View_All_Tickets, Add_Tickets).
        /// </summary>
        public static string tblSiteUserFields {
            get {
                return ResourceManager.GetString("tblSiteUserFields", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VALUES (@site_id, @user_id, @view_all_resources, @add_resources, @modify_resources, @delete_resources, @view_all_tickets, @add_tickets).
        /// </summary>
        public static string tblSiteUserInsert {
            get {
                return ResourceManager.GetString("tblSiteUserInsert", resourceCulture);
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
        ///   Looks up a localized string similar to SET Site_ID = @site_id, User_ID = @user_id, View_All_Resources = @view_all_resources, Add_Resources = @add_resources, Modify_Resources = @modify_resources, Delete_Resources = @delete_resources, View_All_Tickets = @view_all_tickets, Add_Tickets = @add_tickets WHERE Site_Users_ID = .
        /// </summary>
        public static string tblSiteUserUpdate {
            get {
                return ResourceManager.GetString("tblSiteUserUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dbo.Ticket_Tasks.
        /// </summary>
        public static string tblTasks {
            get {
                return ResourceManager.GetString("tblTasks", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (Site_ID, Ticket_ID, Creator_ID, Created_On, Task_Status, Task_ID, Priority, Task_Overview, Task_Details, Assigned_User_ID, Progress, Due_On, Started_TS, Completed_TS, Notes, Alarm1_Enabled, Alarm1, Alarm1_Ack_TS, Alarm2_Enabled, Alarm2, Alarm2_Ack_TS, Alarm3_Enabled, Alarm3, Alarm3_Ack_TS, Late_Alarm).
        /// </summary>
        public static string tblTasksFields {
            get {
                return ResourceManager.GetString("tblTasksFields", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VALUES (@site_id, @ticket_id, @ticket_task_id, @creator_id, @created_on, @task_status, @task_id, @priority, @task_overview, @task_details, @assigned_user_id, @progress, @due_on, @started_ts, @completed_ts, @notes, @alarm1, @alarm1_enabled, @alarm1_ack_ts, @alarm2, @alarm2_enabled, @alarm2_ack_ts, @alarm3, @alarm3_enabled, @alarm3_ack_ts, @late_alarm).
        /// </summary>
        public static string tblTasksInsert {
            get {
                return ResourceManager.GetString("tblTasksInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SET Site_ID =  @site_id, Ticket_ID = @ticket_id, Ticket_Task_ID = @ticket_task_id, Creator_ID = @creator_id, Created_On = @created_on, Task_Status = @task_status, Task_ID = @task_id, Priority = @priority, Task_Overview = @task_overview, Task_Details = @task_details, Assigned_User_ID = @assigned_user_id, Progress = @progress, Due_On = @due_on, Started_TS = @started_ts, Completed_TS = @completed_ts, Notes = @notes, Alarm1 = @alarm1, Alarm1_Enabled = @alarm1_enabled, Alarm1_Ack_TS = @alarm1_ack_ts, Alarm2 = @a [rest of string was truncated]&quot;;.
        /// </summary>
        public static string tblTasksUpdate {
            get {
                return ResourceManager.GetString("tblTasksUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dbo.Site_Tickets.
        /// </summary>
        public static string tblTickets {
            get {
                return ResourceManager.GetString("tblTickets", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (Site_ID, Creator_ID, Created_On, Due_On, Brief_Desc, Long_Desc, Status, Completed_TS, Notes, Total_Tasks, Completed_Tasks, Active_Tasks).
        /// </summary>
        public static string tblTicketsFields {
            get {
                return ResourceManager.GetString("tblTicketsFields", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VALUES (@site_id, @creator_id, @created_on, @due_on, @brief_desc, @long_desc, @status, @completed_ts, @notes, @total_tasks, @completed_tasks, @active_tasks).
        /// </summary>
        public static string tblTicketsInsert {
            get {
                return ResourceManager.GetString("tblTicketsInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SET Site_ID = @site_id,  Creator_ID = @creator_id, Created_On = @created_on, Due_On = @due_on, Brief_Desc = @brief_desc, Long_Desc = @long_desc, Status = @status, Completed_TS = @completed_ts, Notes = @notes, Total_Tasks = @total_tasks, Completed_Tasks = @completed_tasks, Active_Tasks = @active_tasks WHERE Ticket_ID = .
        /// </summary>
        public static string tblTicketsUpdate {
            get {
                return ResourceManager.GetString("tblTicketsUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (User_Name, Access_Level).
        /// </summary>
        public static string tblUserFields {
            get {
                return ResourceManager.GetString("tblUserFields", resourceCulture);
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
        ///   Looks up a localized string similar to VALUES (@user_name, @access_level).
        /// </summary>
        public static string tblUserInsert {
            get {
                return ResourceManager.GetString("tblUserInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SET User_Name = @user_name, Access_Level = @access_level WHERE User_ID =.
        /// </summary>
        public static string tblUserUpdate {
            get {
                return ResourceManager.GetString("tblUserUpdate", resourceCulture);
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
        
        /// <summary>
        ///   Looks up a localized string similar to Andrea Huff, Brayden Huff, Kiefer Huff.
        /// </summary>
        public static string Thanks {
            get {
                return ResourceManager.GetString("Thanks", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] user {
            get {
                object obj = ResourceManager.GetObject("user", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
