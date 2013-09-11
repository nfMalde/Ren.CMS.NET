using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;
using Ren.CMS.Persistence;
using NHibernate.Tool.hbm2ddl;



namespace InstallModule.Installer.Models
{
    public enum ReleaseStages
    { 
        
        alpha1,
        alpha2,
        beta,
        rc1
        , rc2
        , stable
    
    
    
    }

    public class InstallerConfig
    {
        private decimal _version = new Decimal(0.00);

        private decimal _dbversion = new Decimal(0.00);

        private decimal _dbversionInstalled = new Decimal(0.00);

        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> _languageContents = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

        private LanguageMSIPackage MSILanguage = new LanguageMSIPackage();

        public InstallerConfig()
        { 
            
            //Get CMS Version
            Assembly CMSDLL = Assembly.LoadFile(HttpContext.Current.Server.MapPath("~/Bin/Ren.CMS.dll"));

            var version = CMSDLL.GetName().Version;

            this._version = Decimal.Parse(version.Major +"."+ version.Minor +""+ version.Revision +"" +version.Build);

            //GET DB Version
             Version dbx = new DatabaseMSIPackage().DBVersion;
             this._dbversion =

                 Decimal.Parse(dbx.Major + "." + dbx.Minor + "" + dbx.Revision + "" + dbx.Build);
            


        
        
        }


        public void SetLanguageMSIPackage(string name)
        {
            if (!MSILanguage.Files.Any(e => e.Key == name))
                throw new Exception("MSILanguage Package " + name + " does not exists!");

            if (_languageContents.Count > 0)
                _languageContents = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();


            //TODO Build Array




        
        
        }
    }

    public class LanguageMSIPackage
    {

        private Dictionary<string, XmlDocument> _xmlFiles = new Dictionary<string, XmlDocument>();

        public LanguageMSIPackage()
        {

            string DIR = "~/LanguagePackagesInstall";
            DIR = HttpContext.Current.Server.MapPath(DIR);

            if (!Directory.Exists(DIR))
                throw new Exception("LanguagePackagesInstall Directory not found!");

            foreach(string f in Directory.GetFiles(DIR))
            {
                if(!Path.GetExtension(f).ToLower().EndsWith("xml"))
                    continue;

                 XmlDocument X = new XmlDocument();
                 X.Load(f);

                 _xmlFiles.Add(Path.GetFileNameWithoutExtension(f), X);
            }
        
        }


        public Dictionary<string, XmlDocument> Files {

            get
            {
                return _xmlFiles;
            }
        
        }
    
    
    }

    public class DatabaseMSIPackage
    {

        public Version DBVersion { 
            get {
                return 
                    new Version("0.0.1.0"); 
            }
        }


        public void SetupTables()
        {
            var config = NHibernateHelper.GetConfiguration();


            new SchemaExport(config).Execute(true, true, false);
          
        
        }


       


        public void UpdateTables()
        {

            throw new NotImplementedException("Update at the moment not possible! Please select fresh install!");
        
        }

    
    
    }
}