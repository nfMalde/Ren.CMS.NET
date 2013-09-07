﻿using Ren.CMS.CORE.SqlHelper;
using Ren.CMS.CORE.ThisApplication;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.Content
{
    public class nContentClickCounter
    {
        private nContent cont = null;

        public nContentClickCounter(nContent _Content)
        {

            this.cont = _Content;
        }


        private bool ipallreadyClicked()
        {
            string IP = HttpContext.Current.Request.UserHostAddress;
            Ren.CMS.CORE.Security.CryptoServices Crypto = new CORE.Security.CryptoServices();
            IP = Crypto.ConvertToSHA1(IP);

            if (String.IsNullOrEmpty(IP))
            {
                return true;
            }

            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            string query = "SELECT * FROM " + (new ThisApplication().getSqlPrefix) + "Content_ClickCounter WHERE IP=@ip AND cid=@id";
            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            PCOL.Add("@ip", IP);
            PCOL.Add("@id", this.cont.ID);
            SqlDataReader R = SQL.SysReader(query, PCOL);
            bool ret = R.HasRows;
            R.Close();
            SQL.SysDisconnect();

            return ret;

        }

        public void enableClickCounter(bool iprestrict = true)
        {
            string IP = HttpContext.Current.Request.UserHostAddress;
            Ren.CMS.CORE.Security.CryptoServices Crypto = new CORE.Security.CryptoServices();
            IP = Crypto.ConvertToSHA1(IP);
            if (this.cont.ID > 0)
            {
                if (!iprestrict || !this.ipallreadyClicked())
                {
                    nSqlParameterCollection PCOL = new nSqlParameterCollection();
                    SqlHelper SQL = new SqlHelper();
                    SQL.SysConnect();
                    string query = "INSERT INTO " + (new ThisApplication().getSqlPrefix) + "Content_ClickCounter(IP,cid) VALUES(@ip,@id)";
                    PCOL.Add("@ip", IP);
                    PCOL.Add("@id", this.cont.ID);
                    SQL.SysNonQuery(query, PCOL);
                    SQL.SysDisconnect();


                }
            }
        }

    }
}
