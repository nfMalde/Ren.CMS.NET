namespace Ren.CMS.CORE.nhibernate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NHibernate;

    using Ren.CMS.CORE.nhibernate.Domain;
    using Ren.CMS.CORE.nhibernate.Mapping;

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