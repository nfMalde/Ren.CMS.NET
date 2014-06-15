using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Repositories
{
    public class ContentAttachmentTypeRepository: Base.BaseRepository<Domain.ContentAttachmenttypes>
    {
        public override Domain.ContentAttachmenttypes GetOne(NHibernate.Criterion.ICriterion expression)
        {
            var entry = base.GetOne(expression);
            if(entry != null)
            {
                Base.BaseRepository<Domain.ContentAttachmenttypesExtensionsettings> Extensions = new Base.BaseRepository<Domain.ContentAttachmenttypesExtensionsettings>();

                var list = Extensions.GetMany(NHibernate.Criterion.Expression.Where<Domain.ContentAttachmenttypesExtensionsettings>(e => e.Attachmenttypeid == entry.Id));

                entry.AttachmentExtensions = list.ToList();
                return entry;
            }
            return null;
        }

        public override IEnumerable<Domain.ContentAttachmenttypes> GetMany(NHibernate.Criterion.ICriterion where = null, NHibernate.Criterion.IProjection orderBy = null, bool Asc = true)
        {
            var entries =  base.GetMany(where, orderBy, Asc);
            List<Domain.ContentAttachmenttypes> list = new List<Domain.ContentAttachmenttypes>();

            foreach(var entry in entries)
            {
                list.Add(this.GetOne(NHibernate.Criterion.Expression.Where<Domain.ContentAttachmenttypes>(e => e.Id == entry.Id)));
            }

            return list;           
        }

        public override void Add(Domain.ContentAttachmenttypes newEntity)
        {
            this.AddAndGetId(newEntity);
        }

        public override object AddAndGetId(Domain.ContentAttachmenttypes newEntity)
        {
            int id =  (int) base.AddAndGetId(newEntity);
            newEntity.Id = id;
            Base.BaseRepository<Domain.ContentAttachmenttypesExtensionsettings> ExtRepo = new Base.BaseRepository<Domain.ContentAttachmenttypesExtensionsettings>();
            
           foreach(var ext in newEntity.AttachmentExtensions)
           {
              ext.Attachmenttypeid = id;
              if(ExtRepo.GetOne(NHibernate.Criterion.Expression.Where<Domain.ContentAttachmenttypesExtensionsettings>(e => e.Id == ext.Id)) != null)
              {  
                  ExtRepo.Update(ext);

              }
              else
              {
                  ExtRepo.Add(ext);
              }

           }

           return id;

        }

        public override void Update(Domain.ContentAttachmenttypes entity)
        {
            Base.BaseRepository<Domain.ContentAttachmenttypesExtensionsettings> ExtRepo = new Base.BaseRepository<Domain.ContentAttachmenttypesExtensionsettings>();

            base.Update(entity);
            foreach (var ext in entity.AttachmentExtensions)
            {
                ext.Attachmenttypeid = entity.Id;
                if (ExtRepo.GetOne(NHibernate.Criterion.Expression.Where<Domain.ContentAttachmenttypesExtensionsettings>(e => e.Id == ext.Id)) != null)
                {
                    ExtRepo.Update(ext);

                }
                else
                {
                    ExtRepo.Add(ext);
                }

            }
        }

        public override void Delete(Domain.ContentAttachmenttypes entity)
        {
           
            Base.BaseRepository<Domain.ContentAttachmenttypesExtensionsettings> ExtRepo = new Base.BaseRepository<Domain.ContentAttachmenttypesExtensionsettings>();

      
            foreach (var ext in entity.AttachmentExtensions)
            {

                ExtRepo.Delete(ext);
            }
            base.Delete(entity);
        }
  
    }
}
