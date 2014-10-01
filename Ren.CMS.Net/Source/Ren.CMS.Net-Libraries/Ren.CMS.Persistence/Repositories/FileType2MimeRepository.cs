using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Repositories
{
    public class FileType2MimeRepository : Base.BaseRepository<Domain.TbFiletype2MIME>
    {
        private RegisteredMimeRepository MimeRepo = null;

        public FileType2MimeRepository()
        {
            MimeRepo = new RegisteredMimeRepository();
        }

        private int InserOrUpdate(Domain.RegisteredMIMEType mime)
        {

            Domain.RegisteredMIMEType c = this.MimeRepo.GetOne(NHibernate.Criterion.Expression.Where<Domain.RegisteredMIMEType>(e => e.Id == mime.Id));
            if (c == null)
                return (int)this.MimeRepo.AddAndGetId(mime);

            this.MimeRepo.Update(mime);
            return mime.Id;            
        }


        public override void Add(Domain.TbFiletype2MIME newEntity)
        {

            this.AddAndGetId(newEntity);
        }

        public override object AddAndGetId(Domain.TbFiletype2MIME newEntity)
        {
            newEntity.MimeId = this.InserOrUpdate(newEntity.Mime);
            return base.AddAndGetId(newEntity);
        }

        public override void Delete(Domain.TbFiletype2MIME entity)
        {
            base.Delete(entity);
        }

        public override IEnumerable<Domain.TbFiletype2MIME> GetMany(NHibernate.Criterion.ICriterion where = null, NHibernate.Criterion.IProjection orderBy = null, bool Asc = true)
        {
            var list = base.GetMany(where, orderBy, Asc);
            List<Domain.TbFiletype2MIME> _list = new List<Domain.TbFiletype2MIME>();
            foreach(var entry in list)
            {
                entry.Mime = this.MimeRepo.GetOne(NHibernate.Criterion.Expression.Where<Domain.RegisteredMIMEType>(e => e.Id == entry.MimeId));
                _list.Add(entry);
            }

            return _list;
        }

        public override Domain.TbFiletype2MIME GetOne(NHibernate.Criterion.ICriterion expression)
        {
            var one =  base.GetOne(expression);
            one.Mime = this.MimeRepo.GetOne(NHibernate.Criterion.Expression.Where<Domain.RegisteredMIMEType>(e => e.Id == one.MimeId));

            return one;
        }

        public override void Update(Domain.TbFiletype2MIME entity)
        {
            this.InserOrUpdate(entity.Mime);
            base.Update(entity);
        }

        
    }
}
