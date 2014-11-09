using Ren.CMS.Persistence.Base;
using Ren.CMS.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content
{
    public class RemarkTypeManager
    {
        private static BaseRepository<ContentAttachmentRemarkTypes> Repo = new BaseRepository<ContentAttachmentRemarkTypes>();


        public static nAttachmentRemarkType GetRemarkTypeByName(string name)
        {
            ContentAttachmentRemarkTypes entity = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRemarkTypes>(e => e.Remarkname == name));
            if (entity != null)
                return new nAttachmentRemarkType(entity);

            return null;
        }


        public static nAttachmentRemarkType GetRemarkTypeById(int Id)
        {
            ContentAttachmentRemarkTypes entity = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRemarkTypes>(e => e.Id == Id));
            if (entity != null)
                return new nAttachmentRemarkType(entity);

            return null;
        }


        public static nAttachmentRemarkType RegisterNewType(nAttachmentRemarkType t)
        {
            if (GetRemarkTypeByName(t.Name) != null)
                throw new Exception("Remark type with this name allready exists");
            ContentAttachmentRemarkTypes T = new ContentAttachmentRemarkTypes();
            T.Remarklocalline = t.Remarklocalline;
            T.Remarklocalpackage = t.Remarklocalpackage;
            T.Remarkname = t.Name;
            int id = (int) Repo.AddAndGetId(T);
            return GetRemarkTypeById(id);
        }

        public static bool UpdateType(nAttachmentRemarkType t)
        {
            ContentAttachmentRemarkTypes entity = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRemarkTypes>(e => e.Id == t.Id));
            if (entity != null)
            {
                var c = GetRemarkTypeByName(t.Name);
                if (c != null && c.Id != t.Id)
                    throw new Exception("Remark type with this name allready exists");

                entity.Remarkname = t.Name;
                entity.Remarklocalline = t.Remarklocalline;
                entity.Remarklocalpackage = t.Remarklocalpackage;
                Repo.Update(entity);
                return true;
            }


            return false;
        }

        public static bool DeleteType(int Id)
        {
             ContentAttachmentRemarkTypes entity = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRemarkTypes>(e => e.Id == Id));
             if (entity != null)
             {
                 Repo.Delete(entity);
                 return true;
             }

             return false;
        }

    }
}
