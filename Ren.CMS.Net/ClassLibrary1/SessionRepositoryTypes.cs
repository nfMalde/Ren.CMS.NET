using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.SessionRepository.Types
{
    public class SessionRepositoryType
    {
        private TimeSpan validTime = new TimeSpan(3,0,0,0);
        private string name = "NEWSESSIONREPOSITORY";
        private object data = null;

        public TimeSpan ValidTime { get { return this.validTime; } set { this.validTime = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public object Data { get { return this.data; } set { this.data = value; } }

    }

    //Starting default types

    public class ObjectRespository : SessionRepositoryType
    {

        public ObjectRespository()
        { 
        }

        public ObjectRespository(string name, object data)
        {

            this.Name = name;
            this.Data = data;
            
        
        }


    
    
    }



    public class FileRepository : SessionRepositoryType
    {
        public FileRepository(HttpPostedFileBase fileBase)
        {


            object newData = fileBase;

            string dataName = fileBase.FileName;

            this.Data = newData;
            this.Name = dataName;
           
        }
        public FileRepository() { }


        public HttpPostedFileBase GetFileBase()
        {
            return (HttpPostedFileBase)Data;
        }
    }


}
