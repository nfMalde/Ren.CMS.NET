using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Repositories
{
    public class ContentAttachmentRoleRepository: Base.BaseRepository<Domain.ContentAttachmentRole>
    {
        public override void Add(Domain.ContentAttachmentRole newEntity)
        {
            base.Add(newEntity);
        }

        public override object AddAndGetId(Domain.ContentAttachmentRole newEntity)
        {
            return base.AddAndGetId(newEntity);
        }
        public override void Delete(Domain.ContentAttachmentRole entity)
        {
            base.Delete(entity);
        }

        public override IEnumerable<Domain.ContentAttachmentRole> GetMany(NHibernate.Criterion.ICriterion where = null, NHibernate.Criterion.IProjection orderBy = null, bool Asc = true)
        {
            return base.GetMany(where, orderBy, Asc);
        }

        public override Domain.ContentAttachmentRole GetOne(NHibernate.Criterion.ICriterion expression)
        {
            return base.GetOne(expression);
        }

        public override void Update(Domain.ContentAttachmentRole entity)
        {
            base.Update(entity);
        }
    }
}
