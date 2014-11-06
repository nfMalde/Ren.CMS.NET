namespace Ren.CMS.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NHibernate;
    using NHibernate.Criterion;

    using Ren.CMS.Persistence;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Mapping;

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
                 var entry =  base.GetOne(Expression.Where<ContentAttachment>(e => e.Pkid == pkid));
                 return entry;
             }
        }
         


        public override object AddAndGetId(ContentAttachment newEntity)
        {
            return base.AddAndGetId(newEntity);
        }

        #endregion Methods
    }
}