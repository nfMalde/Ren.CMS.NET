namespace Ren.CMS.CORE.nhibernate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NHibernate;
    using NHibernate.Criterion;

    using Ren.CMS.CORE.nhibernate;
    using Ren.CMS.CORE.nhibernate.Domain;
    using Ren.CMS.CORE.nhibernate.Mapping;

    public class ContentAttachmentRepository : Base.BaseRepository<ContentAttachment>
    {
        #region Constructors

        public ContentAttachmentRepository()
            : base()
        {
        }

        #endregion Constructors

        #region Methods

        public ContentAttachment GetByPKid(Guid pkid)
        {
            using (ISession session = NHibernateHelper.OpenSession())
             {
                 return base.GetOne(Expression.Where<ContentAttachment>(e => e.Pkid == pkid));
             }
        }

        #endregion Methods
    }
}