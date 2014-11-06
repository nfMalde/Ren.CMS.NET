using Ren.CMS.Persistence.Base;
using Ren.CMS.Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ren.CMS.Content
{
    public static class nContentAttachmenTypeManager
    {
        private static BaseRepository<ContentAttachmenttypes> Repo = new BaseRepository<ContentAttachmenttypes>();

        public static nContentAttachmenType GetTypeByName(string name)
        {
            ContentAttachmenttypes AType = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmenttypes>(x => x.Typename == name));
            if(AType != null)
            {
                return new nContentAttachmenType(AType);
            }

            return null;
        }

        public static nContentAttachmenType GetTypeById(int id)
        {
            ContentAttachmenttypes AType = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmenttypes>(x => x.Id == id));
            if (AType != null)
            {
                return new nContentAttachmenType(AType);
            }

            return null;
        }


        public static nContentAttachmenType RegisterAttachmentType(nContentAttachmenType type)
        {
            if (GetTypeByName(type.Name) != null)
                throw new Exception("AttachmentType with name " + type.Name + " allready exists.");

            ContentAttachmenttypes NewEntity = new ContentAttachmenttypes();
            //Extract Type
            Type x = type.Handler.GetType();
            NewEntity.HandlerNamespace = x.ToString();
            if (type.Settings != null && type.Settings.Count > 0)
            {
                if (NewEntity.AttachmentExtensions == null)
                    NewEntity.AttachmentExtensions = new List<ContentAttachmenttypesExtensionsettings>();
                foreach(var setting in type.Settings)
                {
                    NewEntity.AttachmentExtensions.Add(new ContentAttachmenttypesExtensionsettings()
                    {
                        Convertfile = setting.Convertfile,
                        ConvertToExt = setting.TargetExtension,
                        Extension = setting.Extension,
                        Maxfilesize = setting.Maxfilesize
                    });
                }

            }

            NewEntity.Storagepath = type.StoragePath;
            NewEntity.Typename = type.Name;

            int id = (int)Repo.AddAndGetId(NewEntity);

            return GetTypeById(id);


        }

        public static bool UpdateAttachmentType(nContentAttachmenType type)
        {
            ContentAttachmenttypes t = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmenttypes>(e => e.Id == type.Id));
            if (t == null)
                throw new Exception("Attachment Type ID#" + type.Id + " does not exists");

            if(t.AttachmentExtensions.Count > 0)
            {
                List<ContentAttachmenttypesExtensionsettings> toDelete = new List<ContentAttachmenttypesExtensionsettings>();
                foreach(ContentAttachmenttypesExtensionsettings setting in t.AttachmentExtensions)
                {
                    if (!type.Settings.Any(e => e.Id == setting.Id))
                        toDelete.Add(setting);
                }

                foreach(ContentAttachmenttypesExtensionsettings settings2d in toDelete)
                {
                    t.AttachmentExtensions.Remove(settings2d);
                }



            }

            foreach(nContentAttachmenTypeExtensions ext in type.Settings)
            {
                if(ext.Id <= 0 ||!t.AttachmentExtensions.Any(e => e.Id == ext.Id))
                    if(!t.AttachmentExtensions.Any(e => e.Extension == ext.Extension))
                        t.AttachmentExtensions.Add(new ContentAttachmenttypesExtensionsettings() { Convertfile = ext.Convertfile, ConvertToExt = ext.TargetExtension, Extension = ext.Extension, Maxfilesize = ext.Maxfilesize });
            }


            t.HandlerNamespace = type.Handler.GetType().ToString();
            t.Storagepath = type.StoragePath;
            t.Typename = type.Name;

            Repo.Update(t);

            return true;
        }

        public static bool DeleteAttachmenType(int id)
        {
            var one = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmenttypes>(e => e.Id == id));
            if(one != null)
            {
                Repo.Delete(one);
                return true;
            }

            return false;
        }
    }
}
