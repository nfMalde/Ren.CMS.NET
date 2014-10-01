using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Repositories
{
    public class FileTypeRepository:Base.BaseRepository<Domain.TbFileType>
    {
        //TbFiletype2MIME FileManagementProfile
        private FileType2MimeRepository F2M = null;

        public FileTypeRepository() : base()
        {
            this.F2M = new FileType2MimeRepository();
        }

        private void InsertOrUpdate(Domain.TbFiletype2MIME Mime)
        {
            Domain.TbFiletype2MIME c = this.F2M.GetOne(NHibernate.Criterion.Expression.Where<Domain.TbFiletype2MIME>(e => e.MimeId == Mime.MimeId && e.FileTypeId == Mime.FileTypeId));
            if (c == null)
                this.F2M.Add(Mime);
            else
            {
                Mime.Id = c.Id;

                this.F2M.Update(Mime);
            }
        }

        public override void Add(Domain.TbFileType newEntity)
        {
            this.AddAndGetId(newEntity);
        }

        public override object AddAndGetId(Domain.TbFileType newEntity)
        {

            int id = (int)base.AddAndGetId(newEntity);
            foreach (Domain.TbFiletype2MIME m in newEntity.AllowedMIMETypes)
            {
                m.FileTypeId = id;
                this.InsertOrUpdate(m);
            }
            return id;
        }

        public override void Delete(Domain.TbFileType entity)
        {

            foreach (Domain.TbFiletype2MIME m in entity.AllowedMIMETypes)
            {
                this.F2M.Delete(m);
            }
            base.Delete(entity);
        }

        public override IEnumerable<Domain.TbFileType> GetMany(NHibernate.Criterion.ICriterion where = null, NHibernate.Criterion.IProjection orderBy = null, bool Asc = true)
        {
            List<Domain.TbFileType> _List = base.GetMany(where, orderBy, Asc).ToList();
            List<Domain.TbFileType> List = new List<Domain.TbFileType>();
            foreach(Domain.TbFileType FT in _List)
            {
                FT.AllowedMIMETypes = this.F2M.GetMany(NHibernate.Criterion.Expression.Where<Domain.TbFiletype2MIME>(e => e.FileTypeId == FT.Id)).ToList();
                List.Add(FT);
            }
            return List;
        }

        public override Domain.TbFileType GetOne(NHibernate.Criterion.ICriterion expression)
        {
            var FT =  base.GetOne(expression);
            FT.AllowedMIMETypes = this.F2M.GetMany(NHibernate.Criterion.Expression.Where<Domain.TbFiletype2MIME>(e => e.FileTypeId == FT.Id)).ToList();

            return FT;
        }

        public override void Update(Domain.TbFileType entity)
        {
            if(entity.AllowedMIMETypes.Count() > 0)
            { 
            var entitys = this.F2M.GetMany(NHibernate.Criterion.Expression.Not(NHibernate.Criterion.Expression.In("Id", entity.AllowedMIMETypes.Select(e => e.Id).ToArray())));
            foreach (var en in entitys)
                this.F2M.Delete(en);
            }
            base.Update(entity);
        }


    }
}
