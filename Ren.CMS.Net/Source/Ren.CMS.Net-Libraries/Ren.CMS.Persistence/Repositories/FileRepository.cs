using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Persistence.Repositories
{
    public class FileRepository:Base.BaseRepository<Domain.File>
    {
        private FileTypeRepository FTypeRepo = null;
       

        public FileRepository() : base()
        {
            FTypeRepo = new FileTypeRepository();
        }

        public int InsertOrUpdate(Domain.TbFileType ft)
        {
            Domain.TbFileType f = this.FTypeRepo.GetOne(NHibernate.Criterion.Expression.Where<Domain.TbFileType>(e => e.Id == ft.Id || e.TypeName == ft.TypeName));
            if (f == null)
                return (int)this.FTypeRepo.AddAndGetId(ft);

            ft.Id = f.Id;
            this.FTypeRepo.Update(ft);

            return ft.Id;
        }

        public override void Add(Domain.File newEntity)
        {
            this.AddAndGetId(newEntity);
        }

        public override object AddAndGetId(Domain.File newEntity)
        {
            newEntity.TypeID = this.InsertOrUpdate(newEntity.FileType);
 
            return base.AddAndGetId(newEntity);
        }

        public override void Delete(Domain.File entity)
        {
            base.Delete(entity);
        }

        public override IEnumerable<Domain.File> GetMany(NHibernate.Criterion.ICriterion where = null, NHibernate.Criterion.IProjection orderBy = null, bool Asc = true)
        {
            var _List = base.GetMany(where, orderBy, Asc);
            List<Domain.File> list = new List<Domain.File>();
            foreach(var l in _List)
            {
                l.FileType = this.FTypeRepo.GetOne(NHibernate.Criterion.Expression.Where<Domain.TbFileType>(e => e.Id == l.TypeID));
                list.Add(l);
            }

            return list;
        }


        public override Domain.File GetOne(NHibernate.Criterion.ICriterion expression)
        {
            
            
            var l = base.GetOne(expression);
            l.FileType = this.FTypeRepo.GetOne(NHibernate.Criterion.Expression.Where<Domain.TbFileType>(e => e.Id == l.TypeID));

            return l;
        }

        public override void Update(Domain.File entity)
        {
            entity.TypeID = this.InsertOrUpdate(entity.FileType);

            base.Update(entity);
        }
    }
}
