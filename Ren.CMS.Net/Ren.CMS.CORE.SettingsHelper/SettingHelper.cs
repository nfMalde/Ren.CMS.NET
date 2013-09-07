using Ren.CMS.CORE.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Ren.CMS.CORE.SettingsHelper
{
  

        public class GlobalSettingsHelper
        {
            public bool SomethingIsEmpty(object[] mixed)
            {

                bool _ret = false;

                foreach (object obj in mixed)
                {

                    string test = obj.ToString();

                    if (String.IsNullOrEmpty(test) || String.IsNullOrWhiteSpace(test)) _ret = true;


                }

                return _ret;

            }
            public bool SomethingIsEmpty(object[] mixed, out string emptyobjects)
            {

                bool _ret = false;
                string invalid = "";
                foreach (object obj in mixed)
                {

                    string test = obj.ToString();

                    if (String.IsNullOrEmpty(test) || String.IsNullOrWhiteSpace(test))
                    {
                        _ret = true;

                        invalid += (String.IsNullOrEmpty(invalid) ? obj.GetType().Name : "," + obj.GetType().Name);

                    }

                }
                emptyobjects = invalid;
                return _ret;

            }
            public GlobalSettingsHelper() { }


            public bool empty(object obj)
            {
                bool _return = false;
                string ob = obj.ToString();

                if (String.IsNullOrEmpty(ob)) _return = true;
                if (String.IsNullOrWhiteSpace(ob)) _return = true;

                return _return;

            }
            private string pref = new ThisApplication.ThisApplication().getSqlPrefix;

            public string Read(string name)
            {

               
                string _return = "";

                Settings.nSetting x = new Settings.GlobalSettings().getSetting(name);
                if (x.Value == null) return "";
                if (x.ValueType == nValueType.ValueString) _return = x.Value.ToString();

                return _return;




            }

            /// <summary>
            /// Writes an global setting into the database.
            /// </summary>
            /// <param name="name">Name of the Setting</param>
            /// <param name="svalue">Value of the Setting</param>
            /// <param name="newName">If changing the name type here the new name</param>
            public void Write(string name, string svalue, string newName = "")
            {


                //if (!empty(name) && !empty(svalue)) {
                //    Settings.GlobalSettings GS = new Settings.GlobalSettings();

                //    Settings.nSetting Temp = new Settings.nSetting();
                //    Settings.nSetting n = GS.getSetting(name);
                //    if (!string.IsNullOrEmpty(newName))
                //    {



                //        Temp.CategoryName = n.CategoryName;
                //        Temp.CategoryID = n.CategoryID;
                //        Temp.PermissionFrontend = n.PermissionFrontend;
                //        Temp.PermissionBackend = n.PermissionBackend;



                //    }
                //    else {

                //        Temp.ID = 0;
                //        Temp.CategoryID = 0;
                //        Temp.CategoryName = "MISC";
                //        n.ID = 0;
                //    }
                //    Temp.Value = svalue;
                //    Temp.ValueType = new Settings.nValueType().ValueString;
                //    Temp.SettingType = new Settings.nSettingType().SettingString;
                //    if (Temp.ValueType == n.ValueType)
                //    {

                //        GS.RemoveSetting(n.ID);


                //        Temp.SettingType = n.SettingType;
                //        GS.AddSetting(Temp);

                //    }
                //    else {

                //        if (n.ID == 0) GS.AddSetting(Temp);

                //    }





                //  }


            }


        }
        public class UserSettingsHelper
        {
            public bool SomethingIsEmpty(object[] mixed)
            {

                bool _ret = false;

                foreach (object obj in mixed)
                {

                    string test = obj.ToString();

                    if (String.IsNullOrEmpty(test) || String.IsNullOrWhiteSpace(test)) _ret = true;


                }

                return _ret;

            }
            public bool SomethingIsEmpty(object[] mixed, out string emptyobjects)
            {

                bool _ret = false;
                string invalid = "";
                foreach (object obj in mixed)
                {

                    string test = obj.ToString();

                    if (String.IsNullOrEmpty(test) || String.IsNullOrWhiteSpace(test))
                    {
                        _ret = true;

                        invalid += (String.IsNullOrEmpty(invalid) ? obj.GetType().Name : "," + obj.GetType().Name);

                    }

                }
                emptyobjects = invalid;
                return _ret;

            }
            public UserSettingsHelper() { }


            public bool empty(object obj)
            {
                bool _return = false;
                string ob = obj.ToString();

                if (String.IsNullOrEmpty(ob)) _return = true;
                if (String.IsNullOrWhiteSpace(ob)) _return = true;

                return _return;

            }
            private string pref = new ThisApplication.ThisApplication().getSqlPrefix;

            public string Read(MembershipUser User, string name)
            {


                string _return = "";

                Settings.nSetting x = new Settings.UserSettings(User).getSetting(name);
                if (x.Value == null) return "";
                if (x.ValueType == nValueType.ValueString) _return = x.Value.ToString();

                return _return;




            }

            /// <summary>
            /// Writes an global setting into the database.
            /// </summary>
            /// <param name="name">Name of the Setting</param>
            /// <param name="svalue">Value of the Setting</param>
            /// <param name="newName">If changing the name type here the new name</param>
            public void Write(MembershipUser User, string name, string svalue, string newName = "")
            {


                //if (!empty(name) && !empty(svalue)) {
                //    Settings.GlobalSettings GS = new Settings.GlobalSettings();

                //    Settings.nSetting Temp = new Settings.nSetting();
                //    Settings.nSetting n = GS.getSetting(name);
                //    if (!string.IsNullOrEmpty(newName))
                //    {



                //        Temp.CategoryName = n.CategoryName;
                //        Temp.CategoryID = n.CategoryID;
                //        Temp.PermissionFrontend = n.PermissionFrontend;
                //        Temp.PermissionBackend = n.PermissionBackend;



                //    }
                //    else {

                //        Temp.ID = 0;
                //        Temp.CategoryID = 0;
                //        Temp.CategoryName = "MISC";
                //        n.ID = 0;
                //    }
                //    Temp.Value = svalue;
                //    Temp.ValueType = new Settings.nValueType().ValueString;
                //    Temp.SettingType = new Settings.nSettingType().SettingString;
                //    if (Temp.ValueType == n.ValueType)
                //    {

                //        GS.RemoveSetting(n.ID);


                //        Temp.SettingType = n.SettingType;
                //        GS.AddSetting(Temp);

                //    }
                //    else {

                //        if (n.ID == 0) GS.AddSetting(Temp);

                //    }





                //  }


            }


        }



    }
 
