namespace Ren.Config.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Ren.CMS.CORE.Settings;

    public static class RenConfigHelper
    {
        #region Nested Types

        public static partial class Settings<T>
            where T : Ren.CMS.CORE.Settings.SettingsBase, new()
        {
            #region Fields

            private static T SettingB = new T();

            #endregion Fields

            #region Methods

            public static void Add(nSetting Model)
            {
                SettingB.AddSetting(Model);
            }

            public static bool Exists(string name)
            {
                return SettingB.Exists(name);
            }

            public static nSetting Get(string Name)
            {
                return SettingB.getSetting(Name);
            }

            public static nSetting Get(int id)
            {
                return SettingB.getSetting(id);
            }

            public static void Remove(int id)
            {
                SettingB.RemoveSetting(id);
            }

            public static List<nSetting> SettingList(bool IgnorePermissions = false)
            {
                return SettingB.listSettings(IgnorePermissions);
            }

            public static List<nSetting> SettingList(string CategoryName, bool IgnorePermissions = false)
            {
                return SettingB.listSettings(CategoryName, IgnorePermissions);
            }

            public static List<nSetting> SettingList(int CategoryID, bool IgnorePermissions = false)
            {
                return SettingB.listSettings(CategoryID, IgnorePermissions);
            }

            #endregion Methods
        }

        #endregion Nested Types
    }
}