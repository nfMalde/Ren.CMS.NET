using System;
using Ren.CMS.Filemanagement;
using Ren.CMS.Persistence;
using NUnit.Framework;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;
using NHibernate.Cfg;
using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using System.Linq;
namespace Test
{
     
    public class Integration
    {
         
        
        
        public void SetupDB()
        {


            SchemaExport Export = new SchemaExport(NHibernateHelper.GetConfiguration());
            Export.Execute(true,true, false);
        }
    
    }
}
