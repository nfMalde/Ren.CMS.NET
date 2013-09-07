using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using Ren.CMS.CORE.SqlHelper;
using Ren.CMS.CORE.Settings;
using Ren.CMS.CORE.ThisApplication;
namespace Ren.CMS.Extensions.ArticleExt
{
    #region ::LoadTotalRating
    public class LoadTotalRating {
        private decimal totalRatingP = 0;
        private int cid = 0;

        #region Constructor
        public LoadTotalRating(int contentID) {
            this._calculate(contentID);
            this.cid = contentID;
        }
        #endregion

        #region Methods
        private void _calculate(int contentID) {


            GlobalSettings GS = new GlobalSettings();
            ThisApplication App = new ThisApplication();

            
             


            SqlHelper Sql = new SqlHelper();
            Sql.SysConnect();

            //Connected - Now lets calculate....

            //Get the Sum of all Topics...
            int sum = 0;
            try
            {
                nSqlParameterCollection SumPara = new nSqlParameterCollection();
                SumPara.Add("@refid", contentID);
                SqlDataReader Su = Sql.SysReader("SELECT COUNT(*) as topicSum FROM " + App.getSqlPrefix + "Internal_Rating WHERE refid=@refid", SumPara);
                if (Su.HasRows)
                {
                    Su.Read();

                    int.TryParse(Su["topicSum"].ToString(), out sum);



                }
                Su.Close();
                int starSum = 0;

                if (Su.IsClosed)
                {
                    //Ok we got the Sum...

                    if (sum > 0)
                    {
                        nSqlParameterCollection CalcPara = new nSqlParameterCollection();
                        CalcPara.Add("@refid", contentID);
                        //Sum is not 0 so we can go on

                        SqlDataReader Calc = Sql.SysReader("SELECT SUM(stars) as starSum FROM " + App.getSqlPrefix + "Internal_Rating WHERE refid=@refid", CalcPara);
                        if (Calc.HasRows)
                        {


                            //Ah good we got a starSum
                            Calc.Read();
                            int.TryParse(Calc["starSum"].ToString(), out starSum);


                        }
                        Calc.Close();


                    }





                }



                if (starSum > 0 && sum > 0)
                {

                    //Ok we want a decimal so lets convert them

                    decimal decSum = Convert.ToDecimal(sum);
                    decimal decStarSum = Convert.ToDecimal(starSum);

                    this.totalRatingP = decStarSum / decSum;




                }



            }

            catch (Exception e)
            {

                //We stay here...we cant calculate it now.


            }
            Sql.SysDisconnect();
        
        }
     


      
        public decimal toDecimal() {


            return this.totalRatingP;
        }
        public int toInt() {

           return Convert.ToInt32(Math.Round(this.totalRatingP));
        
        
        }
        public void reCalculate() {

            this._calculate(this.cid);
        
        
        
        
        }
        #endregion


    }
    #endregion

    public class ProContra {
        private ThisApplication App = new ThisApplication();
        public int contentID = 0;

        private void check() {

            GlobalSettings GS = new GlobalSettings();


            if (String.IsNullOrEmpty(this.Text)) throw new Exception("Text cannot be empty");
            if (String.IsNullOrEmpty(this.Type)) throw new Exception("Cannot save typeless item");
            if (this.contentID == 0) throw new Exception("Content ID cannot be empty");
            this.Type = this.Type.ToLower();

            if (this.Type != "pro" && this.Type != "contra") throw new Exception("Invalid type \"" + this.Type + "\"");
        
        }
        private int insert() {
            int retu = 0;


            this.check();
  
            SqlHelper Sql = new SqlHelper();
            Sql.SysConnect();


            string query = "INSERT INTO " + App.getSqlPrefix + "Internal_Pro_Contra (pType,pText,refid) VALUES(@type,@text,@refid)";

            nSqlParameterCollection Parameters = new nSqlParameterCollection();
            Parameters.Add("@type", this.Type);
            Parameters.Add("@text", this.Text);
            Parameters.Add("@refid", this.contentID);

            Sql.SysNonQuery(query, Parameters);

            string query2 = "SELECT TOP 1 id FROM " + App.getSqlPrefix + "Internal_Pro_Contra WHERE pType=@type AND pText=@text AND refid=@refid ORDER BY id DESC";
            SqlDataReader R = Sql.SysReader(query2, Parameters);

            if (R.HasRows)
            {
                R.Read();
                retu = (int)R["id"];
            }
            if (!R.IsClosed) R.Close();
            Sql.SysDisconnect();
            return retu;
        }

        private int update() {

            this.check();
           
             nSqlParameterCollection Parameters = new nSqlParameterCollection();
            Parameters.Add("@type", this.Type);
            Parameters.Add("@text", this.Text);
            Parameters.Add("@refid", this.contentID);
            Parameters.Add("@id", this.ID);


            string query = "UPDATE " + App.getSqlPrefix + "Internal_Pro_Contra SET pType=@type,pText=@text,refid=@refid WHERE id=@id";
            SqlHelper Sql = new SqlHelper();
            Sql.SysConnect();

            Sql.SysNonQuery(query, Parameters);


            Sql.SysDisconnect();
        return this.ID;
        }

        private int delete() {

            nSqlParameterCollection Parameters = new nSqlParameterCollection();
 
            Parameters.Add("@id", this.ID);
            string query = "DELETE " + App.getSqlPrefix + "Internal_Pro_Contra WHERE id=@id";
            SqlHelper Sql = new SqlHelper();

            Sql.SysConnect();


            Sql.SysNonQuery(query, Parameters);

            Sql.SysDisconnect();

            return this.ID;
        }
        public void Save(){
            if (this.ID == 0)
                this.insert();
            else this.update();

        }

        private void Save(out int newID) {


            if (this.ID == 0) newID = this.insert();
            else newID = this.update();        
        }
        public ProContra(string type, string text, int refID, int id = 0) {




            if (id == 0) this.Save(out id);
            this.Text = text;
            this.Type = type;
            this.ID = id;
            this.contentID = refID;
        }
        public string Type { get; set; }
        public string Text { get; set; }
        public int ID { get; set; }
     
    
    }
    public class initProContra {

        private List<ProContra> _list = new List<ProContra>();
        private ThisApplication App = new ThisApplication();
        
        
        public initProContra(int contentID, string type = "all"){
            
                string query = "";
                nSqlParameterCollection Parameters = new nSqlParameterCollection();


                switch (type.ToLower()) { 
                
                    case "pro":
                        Parameters.Add("@type", "pro");
                        Parameters.Add("@id", contentID);

                        
                        query = "SELECT * FROM " + App.getSqlPrefix + "Internal_Pro_Contra WHERE pType=@type AND refid=@id";

                        
                        break;
                    case "contra":
                        
                        Parameters.Add("@type", "contra");
                        Parameters.Add("@id", contentID);

                        query = "SELECT * FROM " + App.getSqlPrefix + "Internal_Pro_Contra WHERE pType=@type AND refid=@id";

                        break;
                
                    default:

                         Parameters.Add("@id", contentID);

                        query = "SELECT * FROM " + App.getSqlPrefix + "Internal_Pro_Contra WHERE refid=@id";

                        break;
                
                
                }

                //Query steht...


                SqlHelper Sql = new SqlHelper();
                Sql.SysConnect();

                SqlDataReader sList = Sql.SysReader(query, Parameters);
                if (sList.HasRows) {



                    while (sList.Read()) {


                        ProContra Model = new ProContra((string)sList["pType"], (string)sList["pText"], contentID, (int)sList["id"]);

                        _list.Add(Model);
                    
                    
                    }
                
                
                
                }
                Sql.SysDisconnect();
            
            
            }

        public List<ProContra> Rows {

            get {

                return this._list;
            
            }
        
        
        }
    
    }
    public class InternalRating
    {

        public int stars { set; get; }
        public string topic { set; get; }




    }

    public class initRatings {

        private List<InternalRating> x = new List<InternalRating>();
        public initRatings(int contentID) {

            GlobalSettings GS = new GlobalSettings();
            ThisApplication App = new ThisApplication();





            SqlHelper Sql = new SqlHelper();
            Sql.SysConnect();


            nSqlParameterCollection ListPara = new nSqlParameterCollection();
            ListPara.Add("@refid", contentID);

            SqlDataReader Li = Sql.SysReader("SELECT topic,stars FROM " + App.getSqlPrefix + "Internal_Rating WHERE refid=@refid", ListPara);

            while (Li.Read()) {

                InternalRating IR = new InternalRating();
                IR.stars = (int)Li["stars"];
                IR.topic = (string)Li["topic"];

                x.Add(IR);
            
            
            
            }

            Li.Close();
            Sql.SysDisconnect();
        }

        public List<InternalRating> getRatings() {


            return x;
        
        
        
        }

    
    }
}