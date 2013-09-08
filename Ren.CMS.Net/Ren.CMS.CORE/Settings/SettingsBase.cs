namespace Ren.CMS.CORE.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SettingsBase
    {
        #region Methods

        public virtual bool AddSetting(nSetting Setting)
        {
            return false;
        }

        public virtual bool Exists(string name)
        {
            if (this.getSetting(name).ID > 0)
                return true;

            return false;
        }

        public virtual nSetting getSetting(string name)
        {
            return null;
        }

        public virtual nSetting getSetting(int id)
        {
            return null;
        }

        public virtual List<nSetting> listSettings(bool ignorePermissions = false)
        {
            return null;
        }

        public virtual List<nSetting> listSettings(string categoryname, bool ignorePermissions = false)
        {
            return null;
        }

        public virtual List<nSetting> listSettings(int categoryid, bool ignorePermissions = false)
        {
            return null;
        }

        public virtual void RemoveSetting(int settingid)
        {
        }

        public virtual bool setDefaultValue(nSetting Setting)
        {
            return false;
        }

        public virtual bool setValue(nSetting Setting)
        {
            return false;
        }

        public virtual bool setValue(int settingID, object val)
        {
            return false;
        }

        #endregion Methods
    }
}