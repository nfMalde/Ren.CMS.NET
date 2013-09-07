using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ren.CMS.CORE.nhibernate.Base;
using Ren.CMS.Persistence.Domain;
using System.Web;
using Ren.CMS.CORE.nhibernate;
using NHibernate;

namespace Ren.CMS.Persistence.Repositories
{
    

    public class FilemanagementCrossBrowsersRepository:BaseRepository<FilemanagementCrossBrowsers>
    {
        private string filetype = "video";
        
        public FilemanagementCrossBrowsersRepository(string filetype = "video")
        {
            this.filetype = filetype;
        }

        public FilemanagementCrossBrowsers GetForCurrentBrowser()
        {

           return  this.GetOne(NHibernate.Criterion.Expression.Where<FilemanagementCrossBrowsers>(
                w => w.browserID == HttpContext.Current.Request.Browser.Browser.ToLower() ||w.browserID == "default"));
        }



        public IEnumerable<FilemanagementCrossBrowsers> GetAll()
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                var list = session.QueryOver<FilemanagementCrossBrowsers>();
                list = list.Where(NHibernate.Criterion.Expression.Where<FilemanagementCrossBrowsers>(e => e.FileType == filetype));


                var LIST = list.List();

                return LIST;

            }

        }

        public FilemanagementCrossBrowsers GetByBrowserID(string browserID)
        {
            browserID = browserID.ToLower();

            return base.GetOne(NHibernate.Criterion.Expression.Where<FilemanagementCrossBrowsers>(e => e.browserID == browserID && e.FileType == this.filetype)) ?? null;
        
        }

        public FilemanagementCrossBrowsers GetDefault()
        {
            return this.GetByBrowserID("default") ?? new FilemanagementCrossBrowsers()
                {
                    Id = -1,
                    browserID = "default",
                    browserFullName = "Default Browser Format",
                    FileFormat = "m4v",
                    FileType = "video"
                };
        }

    }
}
