namespace Ren.CMS.CORE.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Web;

    using Ren.CMS.CORE.Permissions;

    public class GlobalSettings : SettingsBase
    {
        #region Fields

        private SqlHelper.SqlHelper myHelper = new SqlHelper.SqlHelper();
        private string sqlpref = "";

        #endregion Fields

        #region Constructors

        public GlobalSettings()
        {
            //this.thisUser = new MemberShip.nProvider.CurrentUser().nUser;
            this.sqlpref = new ThisApplication.ThisApplication().getSqlPrefix;
        }

        #endregion Constructors

        #region Methods

        public override bool AddSetting(nSetting Setting)
        {
            //We need 4 Queries here: 1. Add the Model, 2. Add The Values for all User, 3. If Array adds the store, 4. Connect Permissions to the Setting
            //Check Setting exists
            if (this.settingExists(Setting)) return false;
            //Check Category exists, if not create it.

               // if (String.IsNullOrEmpty(Setting.DescriptionLanguageLine) && String.IsNullOrEmpty(Setting.Description)) return false;
            if (String.IsNullOrEmpty(Setting.LabelLanguageLine) && String.IsNullOrEmpty(Setting.Label)) return false;
            int cid = 0;

            bool ok = true;
            if (!this.categoryExists(Setting.CategoryID, out cid))
            {

                if (!this.categoryExists(0, out cid, Setting.CategoryName))
                {

                    cid = this.createCategory(Setting.CategoryName);

                }

            }
            if (cid != 0) Setting.CategoryID = cid;

            int modelid = this.insertModel(Setting);
            if (modelid != 0)
            {

                Setting.ID = modelid;

                try
                {

                    this.setValue(Setting);
                }

                catch
                {

                    ok = false;
                }

                if (!nPermissions.permissionKeyExists(Setting.PermissionFrontend)) ok = false;
                if (!nPermissions.permissionKeyExists(Setting.PermissionBackend)) ok = false;
                if (ok != false)
                {
                    try
                    {
                        this.permissions2newSetting(Setting);
                    }
                    catch
                    {

                        ok = false;
                    }
                }
            }
            else
            {

                ok = false;

            }

            if (modelid != 0 && ok == false)
            {
                //Something went wrong, we have to remove our changes
                this.RemoveSetting(modelid);

            }
            return ok;
        }

        public override nSetting getSetting(string name)
        {
            string query = "SELECT TOP 1 id FROM " + sqlpref + "SettingModels WHERE SettingName=@name";
            SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
            Pcol.Add("@name", name);
            myHelper.SysConnect();
            int id = 0;
            SqlDataReader R = myHelper.SysReader(query, Pcol);
            if (R.HasRows)
            {
                R.Read();
                id = (int)R["id"];
            }
            R.Close();
            myHelper.SysDisconnect();
            if (id == 0) return new nSetting();
            return this._getSetting(id);
        }

        public override nSetting getSetting(int id)
        {
            return this._getSetting(id);
        }

        /// <summary>
        /// Returns a List of User Settings orderd by Category
        /// </summary>
        /// <returns>List of Settings</returns>
        public override List<nSetting> listSettings(bool ignorePermissions = false)
        {
            return this._listSettings(ignorePermissions);
        }

        /// <summary>
        /// Lists all User Settings for the internal Category Name  "categoryname"
        /// </summary>
        /// <param name="categoryname">Internal Category Name</param>
        /// <returns>List type if nSetting</returns>
        public override List<nSetting> listSettings(string categoryname, bool ignorePermissions = false)
        {
            int id = 0;

            if (categoryExists(0, out id, categoryname))
            {

                return this._listSettingForCat(id, ignorePermissions);

            }
            else
            {

                return new List<nSetting>();
            }
        }

        /// <summary>
        /// Lists all User Settings for the CategoryID  "categoryid"
        /// </summary>
        /// <param name="categoryid">Category ID</param>
        /// <returns>List type if nSetting</returns>
        public override List<nSetting> listSettings(int categoryid, bool ignorePermissions = false)
        {
            return this._listSettingForCat(categoryid, ignorePermissions);
        }

        public override void RemoveSetting(int settingid)
        {
            //Remove Model
            this.removeModel(settingid);

            //Remove Values
            this.removeValues(settingid);

            //Remove Permission Connections

            this.removePermissionConnections(settingid);
        }

        public override bool setDefaultValue(nSetting Setting)
        {
            bool ret = true;

            try
            {

                myHelper.SysConnect();

                if (Setting.ValueType ==  nValueType.ValueArray)
                {

                    if (Setting.DefaultValue.GetType() == typeof(string[]))
                    {
                        string arraybuilder = "a:{ ";

                        foreach (string str in (string[])Setting.DefaultValue)
                        {

                            arraybuilder += "\"" + HttpUtility.HtmlEncode(str) + "\" ";

                        }
                        Setting.DefaultValue = arraybuilder;

                    }

                }
                string query = "UPDATE " + sqlpref + "SettingModels SET SettingDefaultValue=@dv WHERE id=@id";
                SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
                Pcol.Add("@dv", Setting.DefaultValue);
                Pcol.Add("@id", Setting.ID);
                myHelper.SysConnect();
                myHelper.SysNonQuery(query, Pcol);
                myHelper.SysDisconnect();

            }
            catch
            {

                ret = false;
            }
            return ret;
        }

        public override bool setValue(nSetting Setting)
        {
            try
            {
                return this._setValue(Setting);
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public override bool setValue(int settingID, object val)
        {
            nSetting Setting = this._getSetting(settingID);

            try
            {
                Setting.Value = val;
                return this._setValue(Setting);

            }
            catch (Exception e)
            {

                return false;

            }
        }

        private bool categoryExists(int iid, out int foundid, string name = "")
        {
            string query1 = "SELECT id FROM " + sqlpref + "SettingCategories WHERE id=@id AND CatRel=@rel";
            string query2 = "SELECT id FROM " + sqlpref + "SettingCategories WHERE Name=@Name AND CatRel=@rel";
            SqlHelper.nSqlParameterCollection COL1 = new SqlHelper.nSqlParameterCollection();
            SqlHelper.nSqlParameterCollection COL2 = new SqlHelper.nSqlParameterCollection();
            int id = iid;
            COL1.Add("@id", id);
            COL1.Add("@rel", "GLOBAL_SETTINGS");
            COL2.Add("@Name", name);
            COL2.Add("@rel", "GLOBAL_SETTINGS");
            myHelper.SysConnect();
            bool exists = false;
            if (String.IsNullOrEmpty(name))
            {

                SqlDataReader R = myHelper.SysReader(query1, COL1);
                if (R.HasRows)
                {
                    R.Read();

                    id = ((int)R["id"]);
                    exists = true;

                }
                R.Close();

            }
            else
            {

                SqlDataReader R = myHelper.SysReader(query2, COL2);
                if (R.HasRows)
                {

                    R.Read();

                    id = ((int)R["id"]);
                    exists = true;

                }
                R.Close();

            }

            myHelper.SysDisconnect();
            foundid = id;
            return exists;
        }

        private int createCategory(string Name)
        {
            int id = 0;
            myHelper.SysConnect();

            string cmd = "INSERT INTO " + sqlpref + "SettingCategories (Name,CatRel) VALUES (@Name,@Rel)";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

            PCOL.Add("@Name", Name);
            PCOL.Add("@Rel", "GLOBAL_SETTINGS");
            myHelper.SysNonQuery(cmd, PCOL);

            id = myHelper.getLastId(sqlpref + "SettingCategories");
            myHelper.SysDisconnect();
            return id;
        }

        private int insertModel(nSetting Setting)
        {
            int id = 0;

            myHelper.SysConnect();
            if (String.IsNullOrEmpty(Setting.LabelLanguageLine) || String.IsNullOrEmpty(Setting.DescriptionLanguageLine))
            {
                string languagefix = "SELECT code FROM " + sqlpref + "Lang_Codes";
                SqlDataReader L = myHelper.SysReader(languagefix);
                string lng1 = "LANG_GLB_SETTING_ID" + id + "_" + Setting.Name.ToUpper() + "_LABEL_GENERATED";
                string lng2 = "LANG_GLB_SETTING_ID" + id + "_" + Setting.Name.ToUpper() + "_DESCR_GENERATED";
                Setting.LabelLanguageLine = lng1;
                Setting.DescriptionLanguageLine = lng2;
                if (L.HasRows)
                {
                    while (L.Read())
                    {
                        Ren.CMS.CORE.Language.Language LNG = new CORE.Language.Language(L["code"].ToString(), "GLOBAL_SETTINGS");

                        if (String.IsNullOrEmpty(Setting.LabelLanguageLine) && LNG.getLine(lng1) == "")
                        {

                            LNG.InsertLine(lng1, Setting.Label);

                        }

                        if (String.IsNullOrEmpty(Setting.DescriptionLanguageLine) && LNG.getLine(lng2) == "")
                        {

                            LNG.InsertLine(lng2, Setting.Description);

                        }
                    }
                }

                L.Close();
            }
            myHelper.SysDisconnect();
            if (String.IsNullOrEmpty(Setting.LabelLanguageLine) || String.IsNullOrEmpty(Setting.DescriptionLanguageLine)) return 0;
            //Insert the Model
            string model = "INSERT INTO " + sqlpref + "SettingModels (SettingName,SettingDefaultValue,SettingLangLineLabel,SettingLangLineDescr,SettingRelation,ValueType,CID,SettingType)" +
                            " VALUES (@SettingName,@DefaultValue,@SettingLangLineLabel,@SettingLangLineDescr,@SettingRelation,@ValueType,@CID,@SettingType)";
            //Parameters for Model
            SqlHelper.nSqlParameterCollection ModelParameters = new SqlHelper.nSqlParameterCollection();
            ModelParameters.Add("@SettingName", Setting.Name);
            ModelParameters.Add("@SettingType", Setting.SettingType);
            ModelParameters.Add("@SettingLangLineLabel", Setting.LabelLanguageLine);
            ModelParameters.Add("@SettingLangLineDescr", Setting.DescriptionLanguageLine);
            ModelParameters.Add("@SettingRelation", "GLOBAL_SETTINGS");
            ModelParameters.Add("@CID", Setting.CategoryID);

            string dval = "";
            if (Setting.ValueType == nValueType.ValueArray)
            {

                string arraybuilderD = "a:{ ";
                if (Setting.DefaultValue.GetType() == typeof(string[]))
                {

                    foreach (string str in (string[])Setting.DefaultValue)
                    {

                        arraybuilderD += "\"" + HttpUtility.HtmlEncode(str) + "\" ";

                    }

                }
                arraybuilderD += "}";

                dval = arraybuilderD;
            }
            else
            {

                dval = Setting.DefaultValue.ToString();

            }
            try
            {
                ModelParameters.Add("@DefaultValue", dval);
                ModelParameters.Add("@ValueType", Setting.ValueType);
                myHelper.SysConnect();
                myHelper.SysNonQuery(model, ModelParameters);
                id = myHelper.getLastId(sqlpref + "SettingModels");
                myHelper.SysDisconnect();
            }
            catch (SqlException e)
            {
                myHelper.SysDisconnect();
                throw e;

            }

            return id;
        }

        private void insertValue(int id, string val)
        {
            string query = "INSERT INTO " + sqlpref + "SettingValues (SettingID,SettingValue) VALUES(@id,@val)";
            SqlHelper.nSqlParameterCollection Pcol1 = new SqlHelper.nSqlParameterCollection();
            Pcol1.Add("@id", id);
            Pcol1.Add("@val", val);

            myHelper.SysConnect();
            myHelper.SysNonQuery(query, Pcol1);
            myHelper.SysDisconnect();
        }

        private void permissions2newSetting(nSetting Setting)
        {
            string query = "INSERT INTO " + sqlpref + "Settings2Permissions (sid,BackEndPM,FrontEndPM) VALUES(@id,@backend,@frontend)";

            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

            PCOL.Add("@id", Setting.ID);
            PCOL.Add("@backend", Setting.PermissionBackend);
            PCOL.Add("@frontend", Setting.PermissionFrontend);

            myHelper.SysConnect();

            myHelper.SysNonQuery(query, PCOL);

            myHelper.SysDisconnect();
        }

        private void removeModel(int settingid)
        {
            string query = "DELETE " + sqlpref + "SettingModels WHERE id=@id";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", settingid);

            myHelper.SysConnect();
            myHelper.SysNonQuery(query, PCOL);
            myHelper.SysDisconnect();
        }

        private void removePermissionConnections(int settingid)
        {
            string query = "DELETE " + sqlpref + "Settings2Permissions WHERE sid=@id";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", settingid);

            myHelper.SysConnect();
            myHelper.SysNonQuery(query, PCOL);
            myHelper.SysDisconnect();
        }

        private void removeValues(int settingid)
        {
            string query = "DELETE " + sqlpref + "SettingValues WHERE SettingID=@id";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", settingid);

            myHelper.SysConnect();
            myHelper.SysNonQuery(query, PCOL);
            myHelper.SysDisconnect();
        }

        private bool settingExists(nSetting Setting)
        {
            bool exists = false;

            //Check if setting name exists
            string check = "SELECT COUNT(*) as c FROM " + sqlpref + "SettingModels WHERE SettingName LIKE @name";
            //Parameter for check
            SqlHelper.nSqlParameterCollection CheckParameter = new SqlHelper.nSqlParameterCollection();
            CheckParameter.Add("@name", Setting.Name);

            myHelper.SysConnect();

            SqlDataReader R = myHelper.SysReader(check, CheckParameter);

            if (R.HasRows)
            {

                R.Read();

                if ((int)R["c"] > 0) exists = true;

            }
            R.Close();
            myHelper.SysDisconnect();
            return exists;
        }

        private void updateValue(int id, string val, int vid)
        {
            string query = "UPDATE " + sqlpref + "SettingValues SET SettingValue=@val WHERE SettingID=@id";

            SqlHelper.nSqlParameterCollection Pcol1 = new SqlHelper.nSqlParameterCollection();
            Pcol1.Add("@id", id);
            Pcol1.Add("@val", val);

            myHelper.SysConnect();
            myHelper.SysNonQuery(query, Pcol1);

            myHelper.SysDisconnect();
        }

        private nSetting _getSetting(int id)
        {
            string query = "SELECT s.id as id, ISNULL(c.id,-1) as CatID, ISNULL(c.Name,'') as CatName, ISNULL(sp.FrontEndPM,'USR_CAN_VIEW_ACCOUNT_SETTINGS') as FE_P, ISNULL(sp.BackEndPM,'USR_IS_ADMIN') as BE_P,s.SettingName as SettingName,s.SettingLangLineLabel as SettingLangLineLabel,s.SettingLangLineDescr as SettingLangLineDescr, s.SettingDefaultValue as SettingDefaultValue, s.SettingRelation as SettingRelation, s.SettingType as SettingType,s.ValueType as ValueType,  (" +
              "CASE " +

             "WHEN CAST(v.SettingValue as varchar) != '' THEN v.SettingValue" +
            " WHEN ISNULL(CAST(v.SettingValue as varchar),'') = '' THEN s.SettingDefaultValue" +

            " ELSE ISNULL(v.SettingValue,'')" +
            "  END " +
            ") AS sValue, s.SettingDefaultValue as DefaultValue FROM " +
                             sqlpref +
                             "SettingModels s LEFT OUTER JOIN " + sqlpref + "SettingValues v ON(s.id = v.SettingID) LEFT OUTER JOIN " + sqlpref + "SettingCategories c ON(s.CID = c.id) LEFT OUTER JOIN " +
                             sqlpref + "Settings2Permissions sp ON(s.id = sp.sid)  WHERE s.SettingRelation = 'GLOBAL_SETTINGS' AND s.id=@id ORDER BY c.id";
            SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
            Pcol.Add("@id", id);

            myHelper.SysConnect();
            //Permissions.nPermissions PM = new Permissions.nPermissions();

            nValueType VT = new nValueType();
            SqlDataReader Sett = myHelper.SysReader(query, Pcol);
            nSetting S = new nSetting();
            if (Sett.HasRows)
            {
                Sett.Read();

                S.CategoryID = (int)Sett["CatID"];
                S.CategoryName = Sett["CatName"].ToString();
                S.ID = (int)Sett["id"];
                S.LabelLanguageLine = Sett["SettingLangLineLabel"].ToString();
                S.SettingRelation = "GLOBAL_SETTINGS";
                S.DescriptionLanguageLine = Sett["SettingLangLineDescr"].ToString();

                S.Name = Sett["SettingName"].ToString();
                S.PermissionBackend = Sett["BE_P"].ToString();
                S.PermissionFrontend = Sett["FE_P"].ToString();
                S.SettingType = Sett["SettingType"].ToString();

                string tempval = Sett["sValue"].ToString();
                string vtype = Sett["ValueType"].ToString();
                S.ValueType = vtype;
                if (vtype == nValueType.ValueArray)
                {

                    //Regex a:{"..." "..."}

                    string pattern = @"a:{(.+?)\}";
                    System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tempval);
                    string i = "";
                    foreach (Match AP in API)
                    {

                        string str = AP.Value;
                        i = Regex.Replace(str, pattern, "$1");

                    }

                    string pattern2 = "\"(.+?)\"";
                    System.Text.RegularExpressions.MatchCollection API2 = new System.Text.RegularExpressions.Regex(pattern2).Matches(i);
                    string[] myvals = new string[API2.Count];
                    int x = 0;
                    foreach (Match V in API2)
                    {

                        myvals[x] = HttpUtility.HtmlDecode(Regex.Replace(V.Value, pattern2, "$1"));
                        x++;

                    }
                    S.Value = myvals;
                    System.Text.RegularExpressions.MatchCollection DVs = new System.Text.RegularExpressions.Regex(pattern).Matches(Sett["DefaultValue"].ToString());
                    string dv1 = "";
                    foreach (Match DV in DVs)
                    {

                        dv1 = Regex.Replace(DV.Value, pattern, "$1");

                    }
                    MatchCollection DVx = new Regex(pattern2).Matches(dv1);
                    string[] defaultval = new string[DVx.Count];
                    int y = 0;
                    foreach (Match DVxx in DVx)
                    {

                        defaultval[y] = HttpUtility.HtmlDecode(Regex.Replace(DVxx.Value, pattern2, "$1"));
                        y++;
                    }

                    S.DefaultValue = defaultval;

                }
                else if (vtype == nValueType.ValueString)
                {

                    S.DefaultValue = Sett["DefaultValue"].ToString();
                    S.Value = tempval;

                }

            }
            Sett.Close();
            myHelper.SysDisconnect();
            if (S.ID == 0)
                return null;
            return S;
        }

        private List<nSetting> _listSettingForCat(int iid, bool ignorePermissions)
        {
            myHelper.SysConnect();
            string query = "SELECT s.id as id, c.id as CatID, c.Name as CatName, ISNULL(sp.FrontEndPM,'USR_CAN_VIEW_ACCOUNT_SETTINGS') as FE_P, ISNULL(sp.BackEndPM,'USR_IS_ADMIN') as BE_P,s.SettingName as SettingName,s.SettingLangLineLabel as SettingLangLineLabel,s.SettingLangLineDescr as SettingLangLineDescr, s.SettingDefaultValue as SettingDefaultValue, s.SettingRelation as SettingRelation, s.SettingType as SettingType,s.ValueType as ValueType, ISNULL(v.SettingValue,s.SettingDefaultValue) as sValue FROM " +
                           sqlpref +
                           "SettingModels s LEFT OUTER JOIN " + sqlpref + "SettingValues v ON(s.id = v.SettingID) LEFT OUTER JOIN " + sqlpref + "SettingCategories c ON(s.CID = c.id) LEFT OUTER JOIN " +
                           sqlpref + "Settings2Permissions sp ON(s.id = sp.sid) WHERE s.SettingRelation = 'GLOBAL_SETTINGS' AND c.id=@id AND c.CatRel = 'GLOBAL_SETTINGS'";
            SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();
            Pcol.Add("@id", iid);

            SqlDataReader Sett = myHelper.SysReader(query, Pcol);

            List<nSetting> temp = new List<nSetting>();

            nValueType VT = new nValueType();
            while (Sett.Read())
            {

                if (nPermissions.hasPermission(Sett["FE_P"].ToString()) || nPermissions.hasPermission(Sett["BE_P"].ToString()) || ignorePermissions)
                {

                    nSetting S = new nSetting();
                    S.CategoryID = iid;
                    S.CategoryName = Sett["CatName"].ToString();
                    S.ID = (int)Sett["id"];
                    S.LabelLanguageLine = Sett["SettingLangLineLabel"].ToString();
                    S.DescriptionLanguageLine = Sett["SettingLangLineDescr"].ToString();
                    S.SettingRelation = "GLOBAL_SETTINGS";
                    S.Name = Sett["SettingName"].ToString();
                    S.PermissionBackend = Sett["BE_P"].ToString();
                    S.PermissionFrontend = Sett["FE_P"].ToString();
                    S.SettingType = Sett["SettingType"].ToString();
                    string tempval = Sett["sValue"].ToString();
                    string vtype = Sett["ValueType"].ToString();
                    if (vtype == nValueType.ValueArray)
                    {

                        //Regex a:{"..." "..."}

                        string pattern = @"a:{(.+?)\}";
                        System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tempval);
                        string i = "";
                        foreach (Match AP in API)
                        {

                            string str = AP.Value;
                            i = Regex.Replace(str, pattern, "$1");

                        }

                        string pattern2 = "\"(.+?)\"";
                        System.Text.RegularExpressions.MatchCollection API2 = new System.Text.RegularExpressions.Regex(pattern2).Matches(i);
                        string[] myvals = new string[API2.Count];
                        int x = 0;
                        foreach (Match V in API2)
                        {

                            myvals[x] = HttpUtility.HtmlDecode(Regex.Replace(V.Value, pattern2, "$1"));

                        }
                        S.Value = myvals;
                        System.Text.RegularExpressions.MatchCollection DVs = new System.Text.RegularExpressions.Regex(pattern).Matches(Sett["DefaultValue"].ToString());
                        string dv1 = "";
                        foreach (Match DV in DVs)
                        {

                            dv1 = Regex.Replace(DV.Value, pattern, "$1");

                        }
                        MatchCollection DVx = new Regex(pattern2).Matches(dv1);
                        string[] defaultval = new string[DVx.Count];
                        int y = 0;
                        foreach (Match DVxx in DVx)
                        {

                            defaultval[y] = HttpUtility.HtmlDecode(Regex.Replace(DVxx.Value, pattern2, "$1"));
                            y++;
                        }

                        S.DefaultValue = defaultval;

                    }
                    else if (vtype == nValueType.ValueString)
                    {

                        S.DefaultValue = Sett["SettingDefaultValue"].ToString();
                        S.Value = tempval;

                    }
                    temp.Add(S);

                }

            }
            Sett.Close();
            myHelper.SysDisconnect();

            return temp;
        }

        private List<nSetting> _listSettings(bool ignorePermissions)
        {
            myHelper.SysConnect();
            string query = "SELECT s.id as id, ISNULL(c.id,-1) as CatID, ISNULL(c.Name,'') as CatName, ISNULL(sp.FrontEndPM,'USR_CAN_VIEW_ACCOUNT_SETTINGS') as FE_P, ISNULL(sp.BackEndPM,'USR_IS_ADMIN') as BE_P,s.SettingName as SettingName,s.SettingLangLineLabel as SettingLangLineLabel,s.SettingLangLineDescr as SettingLangLineDescr, s.SettingDefaultValue as SettingDefaultValue, s.SettingRelation as SettingRelation, s.SettingType as SettingType,s.ValueType as ValueType, ISNULL(v.SettingValue,s.SettingDefaultValue) as sValue,s.SettingDefaultValue as DefaultValue FROM " +
                           sqlpref +
                           "SettingModels s LEFT OUTER JOIN " + sqlpref + "SettingCategories c ON(s.CID = c.id) LEFT OUTER JOIN " +
                           sqlpref + "Settings2Permissions sp ON(s.id = sp.sid) LEFT OUTER JOIN " + sqlpref + "SettingValues v ON(s.id = v.SettingID) WHERE s.SettingRelation = 'GLOBAL_SETTINGS' ORDER BY c.id";
            SqlHelper.nSqlParameterCollection Pcol = new SqlHelper.nSqlParameterCollection();

            SqlDataReader Sett = myHelper.SysReader(query, Pcol);

            List<nSetting> temp = new List<nSetting>();

            nValueType VT = new nValueType();

            while (Sett.Read())
            {

                if (nPermissions.hasPermission(Sett["FE_P"].ToString()) || nPermissions.hasPermission(Sett["BE_P"].ToString()) || ignorePermissions)
                {

                    nSetting S = new nSetting();
                    S.CategoryID = (int)Sett["CatID"];

                    S.CategoryName = Sett["CatName"].ToString();
                    S.ID = (int)Sett["id"];
                    S.LabelLanguageLine = Sett["SettingLangLineLabel"].ToString();
                    S.DescriptionLanguageLine = Sett["SettingLangLineDescr"].ToString();
                    S.SettingRelation = "GLOBAL_SETTINGS";
                    S.Name = Sett["SettingName"].ToString();
                    S.PermissionBackend = Sett["BE_P"].ToString();
                    S.PermissionFrontend = Sett["FE_P"].ToString();
                    S.SettingType = Sett["SettingType"].ToString();

                    string tempval = Sett["sValue"].ToString();
                    string vtype = Sett["ValueType"].ToString();
                    if (vtype == nValueType.ValueArray)
                    {

                        //Regex a:{"..." "..."}

                        string pattern = @"a:{(.+?)\}";
                        System.Text.RegularExpressions.MatchCollection API = new System.Text.RegularExpressions.Regex(pattern).Matches(tempval);
                        string i = "";
                        foreach (Match AP in API)
                        {

                            string str = AP.Value;
                            i = Regex.Replace(str, pattern, "$1");

                        }

                        string pattern2 = "\"(.+?)\"";
                        System.Text.RegularExpressions.MatchCollection API2 = new System.Text.RegularExpressions.Regex(pattern2).Matches(i);
                        string[] myvals = new string[API2.Count];
                        int x = 0;
                        foreach (Match V in API2)
                        {

                            myvals[x] = HttpUtility.HtmlDecode(Regex.Replace(V.Value, pattern2, "$1"));

                        }
                        S.Value = myvals;
                        System.Text.RegularExpressions.MatchCollection DVs = new System.Text.RegularExpressions.Regex(pattern).Matches(Sett["DefaultValue"].ToString());
                        string dv1 = "";
                        foreach (Match DV in DVs)
                        {

                            dv1 = Regex.Replace(DV.Value, pattern, "$1");

                        }
                        MatchCollection DVx = new Regex(pattern2).Matches(dv1);
                        string[] defaultval = new string[DVx.Count];
                        int y = 0;
                        foreach (Match DVxx in DVx)
                        {

                            defaultval[y] = HttpUtility.HtmlDecode(Regex.Replace(DVxx.Value, pattern2, "$1"));
                            y++;
                        }

                        S.DefaultValue = defaultval;

                    }
                    else if (vtype == nValueType.ValueString)
                    {

                        S.Value = tempval;
                        S.DefaultValue = Sett["DefaultValue"].ToString();

                    }
                    temp.Add(S);

                }
            }
            Sett.Close();
            myHelper.SysDisconnect();

            return temp;
        }

        private bool _setValue(nSetting Setting)
        {
            string querycheck = "SELECT id as vid FROM  " + sqlpref + "SettingValues WHERE SettingID=@sid";
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();

            PCOL.Add("@sid", Setting.ID);

            string val = "";

            nValueType VT = new nValueType();

            if (Setting.ValueType == nValueType.ValueArray)
            {

                if (Setting.Value.GetType() == typeof(string[]))
                {

                    string arraybuilder = "a:{ ";

                    foreach (string str in (string[])Setting.Value)
                    {

                        arraybuilder += "\"" + HttpUtility.HtmlEncode(str) + "\" ";

                    }
                    arraybuilder += "}";
                    val = arraybuilder;
                }

            }
            else
            {
                if (Setting.Value == null) Setting.Value = Setting.DefaultValue;
                val = Setting.Value.ToString();
            }
            myHelper.SysConnect();
            SqlDataReader R = myHelper.SysReader(querycheck, PCOL);
            if (R.HasRows)
            {

                int vid = (int)R["vid"];
                R.Close();
                this.updateValue(Setting.ID, val, vid);

            }
            else
            {
                R.Close();
                this.insertValue(Setting.ID, val);

            }

            if (!R.IsClosed) R.Close();
            myHelper.SysDisconnect();
            return true;
        }

        #endregion Methods
    }
}