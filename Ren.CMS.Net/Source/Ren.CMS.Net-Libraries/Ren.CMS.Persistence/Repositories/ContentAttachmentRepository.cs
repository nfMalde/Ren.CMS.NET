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
                 if (entry == null)
                     return null;

                //Call Extending Repository
                ContentAttachmentTypeRepository TypeRepo = new ContentAttachmentTypeRepository();
                ContentAttachmentRoleRepository RoleRepo = new ContentAttachmentRoleRepository(); 
                entry.AttachmentType = TypeRepo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmenttypes>(e => e.Id == entry.AttachmentTypeId));
                //entry.Role = RoleRepo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(e => e.Id == entry.Usage));

                return entry;
             }
        }

        public override void Add(ContentAttachment newEntity)
        {
            base.Add(newEntity);
        }

        public override void Delete(ContentAttachment entity)
        {
            base.Delete(entity);
        }

        public override IEnumerable<ContentAttachment> GetMany(ICriterion where = null, IProjection orderBy = null, bool Asc = true)
        {
            List<ContentAttachment> _LIST = new List<ContentAttachment>();
            var entries =  base.GetMany(where, orderBy, Asc);

            entries.ToList().ForEach(f => _LIST.Add(this.GetByPKid(f.Pkid)));

            return entries;
        }

        public override ContentAttachment GetOne(ICriterion expression)
        {
            var entry =  base.GetOne(expression);

            if (entry == null)
                return null;

            return this.GetByPKid(entry.Pkid);
        }

        public override void Update(ContentAttachment entity)
        {
            base.Update(entity);
        }


        public override object AddAndGetId(ContentAttachment newEntity)
        {
            return base.AddAndGetId(newEntity);
        }

        #endregion Methods
    }
}