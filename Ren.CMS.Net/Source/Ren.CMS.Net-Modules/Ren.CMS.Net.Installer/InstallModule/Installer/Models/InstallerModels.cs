namespace InstallModule.Installer.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Xml;

    using NHibernate.Tool.hbm2ddl;

    using Ren.CMS.Persistence;

    #region Enumerations
    
   
    public enum InstallTypeEnum
    {
        full =1,
        update = 2
    }

    #endregion Enumerations

    public class LicenseModel
    {
        public bool IAccept { get; set; }
    }


    public class InstallType
    {
        public InstallTypeEnum Type { get; set; }
    }

    public class Languages
    {
        public string FileName { get; set; }
        public string LanguageName { get; set; }
    }

    public class InstallModel
    {
        public bool FullInstall { get; set; }

        public List<Languages> Languages { get; set; }
    }

    public class DatabaseConnection
    {
        public string ConnectionString { get; set; }
        public string TablePrefix { get; set; }
    }

    public class DatabaseMSIPackage
    {
        #region Properties

        public Version DBVersion
        {
            get {
                return
                    new Version("0.0.1.0");
            }
        }

        #endregion Properties

        #region Methods

        public void SetupTables()
        {
            var config = NHibernateHelper.GetConfiguration();

            new SchemaExport(config).Execute(true, true, false);
        }

        public void UpdateTables()
        {
            throw new NotImplementedException("Update at the moment not possible! Please select fresh install!");
        }

        #endregion Methods
    }

    public class InstallerConfig
    {
        #region Fields

        private LanguageMSIPackage MSILanguage = new LanguageMSIPackage();
        private decimal _dbversion = new Decimal(0.00);
        private decimal _dbversionInstalled = new Decimal(0.00);
        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> _languageContents = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        private decimal _version = new Decimal(0.00);

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Methods

        public void SetLanguageMSIPackage(string name)
        {
            if (!MSILanguage.Files.Any(e => e.Key == name))
                throw new Exception("MSILanguage Package " + name + " does not exists!");

            if (_languageContents.Count > 0)
                _languageContents = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            //TODO Build Array
        }

        #endregion Methods
    }

    public class LanguageMSIPackage
    {
        #region Fields

        private Dictionary<string, XmlDocument> _xmlFiles = new Dictionary<string, XmlDocument>();

        #endregion Fields

        #region Constructors

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

        #endregion Constructors

        #region Properties

        public Dictionary<string, XmlDocument> Files
        {
            get
            {
                return _xmlFiles;
            }
        }

        #endregion Properties
    }
}