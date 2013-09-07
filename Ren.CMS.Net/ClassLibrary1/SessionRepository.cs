using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ren.CMS.SessionRepository.Types;
using System.Web;
namespace Ren.CMS.SessionRepository
{
    class SessionRepository
    {
        private HttpContext context = HttpContext.Current;

        public T GetRepository<T>(string Name)  where  T : SessionRepositoryType, new()
        {

           T ret =  new T();
           if (context.Session[Name] != null)
           {

               ret.Data = context.Session[Name];
               ret.Name = Name;
               if(context.Session[Name + "_HEADDATA"] != null)
                   ret.ValidTime = (TimeSpan)context.Session[Name + "_HEADDATA"];
               return ret;
           }
           return null;
        }


        public void SetRepository<T>(T RepositoryTypeModel) where T : SessionRepositoryType
        {
           
            context.Session.Remove(RepositoryTypeModel.Name);
            context.Session.Timeout = Convert.ToInt32(RepositoryTypeModel.ValidTime.TotalMinutes);
            context.Session[RepositoryTypeModel.Name + "_HEADDATA"] = RepositoryTypeModel.ValidTime; 
            context.Session.Add(RepositoryTypeModel.Name, RepositoryTypeModel.Data);
            context.Session.Timeout = Convert.ToInt32(RepositoryTypeModel.ValidTime.TotalMinutes);
             
        }


        public void Delete(string Name)
        {

            if (context.Session[Name] != null)
                context.Session.Remove(Name);
        
        }

    }
}
