using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.CORE.Settings
{
    public struct nSettingStoreItem
    {

        public int ID { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }


    }
    public class nSettingStore
    {

        private List<nSettingStoreItem> store = new List<nSettingStoreItem>();
        public nSettingStore(nSetting Setting)
        {

            string pref = new ThisApplication.ThisApplication().getSqlPrefix;
            string query = "SELECT s.id as id, ISNULL(s.val,'') as Val, ISNULL(l.langLine,'') as langLine FROM " + pref + "SettingStores s INNER JOIN " + pref + "SettingStores2Locales l ON(l.stid = s.id) WHERE s.sid=@id";
            string langcode = "";


            SqlHelper.SqlHelper Helper = new SqlHelper.SqlHelper();
            SqlHelper.nSqlParameterCollection PCOL = new SqlHelper.nSqlParameterCollection();
            PCOL.Add("@id", Setting.ID);

            Language.Language Lng = new Language.Language("__USER__", "SETTING_STORES");


            Helper.SysConnect();
            SqlDataReader R = Helper.SysReader(query, PCOL);
            while (R.Read())
            {

                nSettingStoreItem Item = new nSettingStoreItem();
                Item.ID = (int)R["id"];
                Item.Label = Lng.getLine((string)R["langLine"]);
                Item.Value = ((string)R["Val"]);
                this.store.Add(Item);
            }

            R.Close();
            Helper.SysDisconnect();
        }
        public List<nSettingStoreItem> getStore { get { return this.store; } }

       


    }



    public class nSetting
    {



        private string plabel = "";
        private string pdescr = "";


        public int ID
        {

            get;
            set;
        }

        public object DefaultValue { get; set; }

        /// <summary>
        /// This Permission is needed to see the Setting in Frontend and edit the Value for it. If NULL every one can see and edit it. (With Permission: USR_CAN_VIEW_ACCOUNT_SETTINGS)
        /// </summary>
        public string PermissionFrontend
        {

            get;
            set;

        }

        /// <summary>
        /// This Permission is needed to see and edit it in the Backend. If NULL every one with Permission USR_IS_ADMIN can see and edit it.
        /// </summary>
        public string PermissionBackend
        {


            get;
            set;


        }

        public string CategoryName
        {

            get;
            set;
        }

        public int CategoryID
        {


            get;
            set;

        }

        public string Name
        {


            get;
            set;

        }
        private string SettingRelationP = "SETTINGS";
        public string SettingRelation { get { return this.SettingRelationP; } set { this.SettingRelationP = value; } }
        public string Label
        {

            get
            {
                if (this.plabel == "" && this.LabelLanguageLine != null)
                {
                    Language.Language LNG = new Language.Language("__USER__", this.SettingRelationP);
                    return LNG.getLine(this.LabelLanguageLine);

                }
                else
                {


                    return this.plabel;

                }


            }
            set
            {


                this.plabel = value;
            }


        }

        public string LabelLanguageLine
        {

            get;
            set;


        }

        public string Description
        {


            get
            {
                if (this.pdescr == "" && this.DescriptionLanguageLine != null)
                {
                    Language.Language LNG = new Language.Language("__USER__", this.SettingRelationP);
                    return LNG.getLine(this.DescriptionLanguageLine);

                }
                else
                {


                    return this.pdescr;

                }


            }
            set
            {


                this.pdescr = value;
            }
        }
        public string DescriptionLanguageLine
        {

            get;
            set;

        }

        /// <summary>
        /// Checks if the requested value is set.
        /// </summary>
        /// <param name="val">The value to search</param>
        /// <returns></returns>
        public bool isValue(string val)
        {

            

            if (this.ValueType == nValueType.ValueArray)
            {


                string[] vals = (string[])this.Value;
                foreach (string v in vals)
                {

                    if (v == val) return true;

                }
                return false;
            }
            else
            {

                string v = (string)this.Value;
                if (v == val) return true;
                else return false;


            }


        }
        public string ValueType
        {

            get;
            set;

        }


        private object pValue = null;
        public bool ValueAsBoolean()
        {
            string val = Value.ToString();
            bool ret = false;

            bool.TryParse(val, out ret);



            return ret;
        }
        public object Value
        {


            get
            {

                if (this.pValue == null) return "";
                else return this.pValue;

            }
            set
            {

                this.pValue = value;


            }

        }

        public string SettingType
        {
            get;
            set;

        }


        public int toInt()
        {

            int ret = 0;
            if (this.Value == null || this.Value == "") return ret;
            if (int.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());




        }

        public bool toBoolean()
        {

            bool ret = false;

            if (bool.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());


        }

        public string[] toStringArray(bool showError = false)
        {


            try
            {

                string[] ret = (string[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new string[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new string[1].GetType());
            }

        }

        public double toDouble()
        {
            double ret = 0;
            if (this.Value == "") return 0;
            if (double.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());

        }

        public float toFloat()
        {

            float ret = 0;
            if (this.Value == "") return 0;
            if (float.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());



        }

        public long toLong()
        {

            long ret = 0;
            if (this.Value == "") return 0;
            if (long.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());



        }


        public decimal toDecimal()
        {


            decimal ret = 0;
            if (this.Value == "") return 0;
            if (decimal.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());



        }


        public int[] toIntArray(bool showError = false)
        {


            try
            {

                int[] ret = (int[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new int[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new int[1].GetType());
            }


        }

        public float[] toFloatArray(bool showError = false)
        {

            try
            {

                float[] ret = (float[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new float[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new float[1].GetType());
            }


        }

        public long[] toLongArray(bool showError = false)
        {


            try
            {

                long[] ret = (long[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new long[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new long[1].GetType());
            }


        }


        public decimal[] toDecimalArray(bool showError = false)
        {

            try
            {

                decimal[] ret = (decimal[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new decimal[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new decimal[1].GetType());
            }


        }



        public bool[] toBooleanArray(bool showError = false)
        {

            try
            {

                bool[] ret = (bool[])this.Value;
                return ret;
            }
            catch
            {

                if (!showError) return new bool[0];
                else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + new bool[1].GetType());
            }


        }

        public char toChar()
        {

            char ret = ' ';


            if (char.TryParse(this.Value.ToString(), out ret)) return ret;
            else throw new Exception("Cannot cast Setting Value of type " + this.Value.GetType() + " as " + ret.GetType());






        }

        public List<nSettingStoreItem> Store = new List<nSettingStoreItem>();
    }
}
