namespace Ren.CMS.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NHibernate;

    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Mapping;

    public class ContentTagRepository : Base.BaseRepository<ContentTag>
    {
        #region Constructors

        public ContentTagRepository()
            : base()
        {
        }

        #endregion Constructors

        #region Methods

        public ContentTag GetTagByName(string Tagname)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {

                var t = session.QueryOver<ContentTag>().Where(NHibernate.Criterion.Expression.Where<ContentTag>(e => e.TagName == Tagname)).SingleOrDefault();
                return t;

            }
        }

        #endregion Methods
    }
}