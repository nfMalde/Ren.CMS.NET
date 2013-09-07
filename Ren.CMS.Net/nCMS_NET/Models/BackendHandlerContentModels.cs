using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Ren.CMS.CORE;
using Ren.CMS.MemberShip;
using Ren.CMS.CORE.Settings;
using Ren.CMS.CORE.SqlHelper;
using Ren.CMS.CORE.ThisApplication;
using System.Data.SqlClient;
namespace Ren.CMS.Models.Backend.Content
{
    public class ContentTagFormDialog
    {

        public string elID { get; set; }
        public string url { get; set; }
        public string method { get; set; }
        public string title { get; set; }
        public string gridID { get; set; }
         
       
    
    }

    public class MngContentTag
    {

        public int id { get; set; }
        [Required]
        public string contentType { get; set; }
        [Required]
        public string tagName { get; set; }
        
        public int enableBrowsing { get; set; }

    
    }

    public class jsTreeCategoryModel
    {
        
        public string node_id { get; set; }
        [Required]
        public string operation { get; set; }
        [Required]
        public string ContentType { get; set; }
    
    }

    public class TreeViewCatMover
    {
        [Required]
        public string id { get; set; }
        
        public string parent { get; set; }

    }





    public class TreeViewCategory
    {
        public string Selector { get; set; }

        public string ct { get; set; }

        public string excludePKID { get; set; }

        public string node_id { get; set; }
        [Required]
        public string operation { get; set; }
        [Required]
        public string ContentType { get; set; }

    }

    public class EditAttachment
    {

        public string id { get; set; }

        public string title { get; set; }

        public string role { get; set; }

        public string remark { get; set; }

       
    
    
    }

    public class ValidateSEOModel {
        [Required]
        public string title { get; set; }
    
    }

    public class CategoryModel {
     
        public object ID
        {

            get;
            set;

        }
        [Required]
        public string longName { get; set; }
        [Required]
        public string shortName { get; set; }
        [Required]
        public string contentType { get; set; }
        public string subFrom { get; set; }





        public List<object> CategoryList()
        {

            SqlHelper SQL = new SqlHelper();
           nSqlParameterCollection Col =  new nSqlParameterCollection();

            string query = "SELECT * FROM " + new ThisApplication().getSqlPrefix + "Categories";
            if(!String.IsNullOrEmpty(this.contentType))
            {
                
                query = "SELECT * FROM " + new ThisApplication().getSqlPrefix + "Categories WHERE contentType=@ct";
                Col.Add("@ct", this.contentType);
            }
            List<object> _list = new List<object>();
            SQL.SysConnect();
            SqlDataReader R = SQL.SysReader(query, Col );
            if (R.HasRows)
            {
                _list.Add(new { id ="", shortName = "Keine Kategorie", longName = "", subFrom = "", contentType = "" });
                
                while (R.Read())
                {

                    _list.Add(new { id = (object)R["PKID"], shortName = (string)R["shortName"], longName = (string)R["longName"], subFrom = (R["subFrom"] != DBNull.Value ? (string)R["subFrom"] : "") , contentType = (string)R["contentType"]});
                
                
                }
            
            
            }
            R.Close();
            SQL.SysDisconnect();


            return _list;
           
            
        
        
        }



    
    
    
    }
    public class ContentTypes
    {
        private List<object> ctList = new List<object>();


        public ContentTypes() {
            Ren.CMS.CORE.Language.Language Lang = new CORE.Language.Language("__USER__", "CONTENT_TYPES");

            SqlHelper SQL = new SqlHelper();
            ThisApplication TA = new ThisApplication();
            string prefix = TA.getSqlPrefix;

            string query = "SELECT * FROM " + prefix + "Content_Types";


            SQL.SysConnect();

            SqlDataReader R = SQL.SysReader(query, new nSqlParameterCollection());

            if (R.HasRows)
            {
                while (R.Read())
                {
                    string ctLangLine = Lang.getLine("LANG_CTYPE_"+ ((string)R["name"]).ToUpper());

                    if(String.IsNullOrEmpty(ctLangLine)) ctLangLine = (string)R["name"];
                    this.ctList.Add(new
                    {
                        ctype= (string)R["name"],
                        name = ctLangLine,
                        controller = (string)R["controller"],
                        actionpath = (string)R["actionpath"]


                    });
                }
            
            
            }

            R.Close();



            SQL.SysDisconnect();
        }

        public List<object> ObjectList()
        {

            return this.ctList;
        
        }

     
    
    
    }
}