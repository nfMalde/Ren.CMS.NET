namespace Ren.CMS.SessionRepository.Types
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class FileRepository : SessionRepositoryType
    {
        #region Constructors

        public FileRepository(HttpPostedFileBase fileBase)
        {
            object newData = fileBase;

            string dataName = fileBase.FileName;

            this.Data = newData;
            this.Name = dataName;
        }

        public FileRepository()
        {
        }

        #endregion Constructors

        #region Methods

        public HttpPostedFileBase GetFileBase()
        {
            return (HttpPostedFileBase)Data;
        }

        #endregion Methods
    }

    //Starting default types
    public class ObjectRespository : SessionRepositoryType
    {
        #region Constructors

        public ObjectRespository()
        {
        }

        public ObjectRespository(string name, object data)
        {
            this.Name = name;
            this.Data = data;
        }

        #endregion Constructors
    }

    public class SessionRepositoryType
    {
        #region Fields

        private object data = null;
        private string name = "NEWSESSIONREPOSITORY";
        private TimeSpan validTime = new TimeSpan(3,0,0,0);

        #endregion Fields

        #region Properties

        public object Data
        {
            get { return this.data; } set { this.data = value; }
        }

        public string Name
        {
            get { return this.name; } set { this.name = value; }
        }

        public TimeSpan ValidTime
        {
            get { return this.validTime; } set { this.validTime = value; }
        }

        #endregion Properties
    }
}