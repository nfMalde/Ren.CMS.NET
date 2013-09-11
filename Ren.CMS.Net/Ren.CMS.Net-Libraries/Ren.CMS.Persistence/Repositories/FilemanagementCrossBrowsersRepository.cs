namespace Ren.CMS.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using NHibernate;

    using Ren.CMS.Persistence;
    using Ren.CMS.Persistence.Base;
    using Ren.CMS.Persistence.Domain;

    public class FilemanagementCrossBrowsersRepository : BaseRepository<FilemanagementCrossBrowsers>
    {
        #region Fields

        private string filetype = "video";

        #endregion Fields

        #region Constructors

        public FilemanagementCrossBrowsersRepository(string filetype = "video")
        {
            this.filetype = filetype;
        }

        #endregion Constructors

        #region Methods

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

        public FilemanagementCrossBrowsers GetForCurrentBrowser()
        {
            return  this.GetOne(NHibernate.Criterion.Expression.Where<FilemanagementCrossBrowsers>(
                w => w.browserID == HttpContext.Current.Request.Browser.Browser.ToLower() ||w.browserID == "default"));
        }

        #endregion Methods
    }
}