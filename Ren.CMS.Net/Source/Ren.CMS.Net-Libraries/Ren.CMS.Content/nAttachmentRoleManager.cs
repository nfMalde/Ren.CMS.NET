using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ren.CMS.Persistence.Domain;
using Ren.CMS.Persistence.Base;
namespace Ren.CMS.Content
{
    public static class nAttachmentRoleManager
    {
        private static BaseRepository<ContentAttachmentRole> Repo = new BaseRepository<ContentAttachmentRole>();

        public static nAttachmentRole GetRoleByName(string roleName)
        {
            ContentAttachmentRole Role = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(x => x.Rolename == roleName));
            if(Role != null)
            {
                return new nAttachmentRole(Role);
                
            }

            return null;
        }

        public static nAttachmentRole GetRoleById(int id)
        {
            ContentAttachmentRole Role = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(x => x.Id == id));
            if (Role != null)
            {
                return new nAttachmentRole(Role);

            }

            return null;
        }

        public static nAttachmentRole RegisterNewRole(nAttachmentRole role)
        {
            ContentAttachmentRole Role = new ContentAttachmentRole();
            Role.Rolelangline = role.Rolelangline;
            Role.Rolelangpackage = role.Rolelangpackage;
            Role.Rolename = role.Rolename;
            if (Role.Arguments == null)
                Role.Arguments = new List<ContentAttachmentArgument>();
            if (role.Arguments.Count == 0)
                throw new Exception("Attachment Roles requries at least one Argument");

           foreach(nAttachmentArgument argument in role.Arguments)
           {
               Role.Arguments.Add(new ContentAttachmentArgument() {Role = Role, ArgumentName = argument.ArgumentName, Argumentlangline = argument.Argumentlangline, Argumentlangpackage = argument.Argumentlangpackage });
           }

           int id = (int)Repo.AddAndGetId(Role);
           return GetRoleById(id);
        }

        public static bool UpdateRole(nAttachmentRole role)
        {
             ContentAttachmentRole Role = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(x => x.Id == role.Id));
             ContentAttachmentRole RoleDpl = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(x => x.Id != role.Id && x.Rolename == role.Rolename));
             if (Role != null && RoleDpl == null)
             {


                 Role.Rolelangline = role.Rolelangline;
                 Role.Rolelangpackage = role.Rolelangpackage;


                 Role.Rolename = role.Rolename;
                 List<ContentAttachmentArgument> toDelete = new List<ContentAttachmentArgument>();

                 foreach (ContentAttachmentArgument arg in Role.Arguments)
                 {
                     if (!role.Arguments.Any(e => e.Id == arg.Id))
                         toDelete.Add(arg);
                 }

                 foreach (ContentAttachmentArgument arg in toDelete)
                     Role.Arguments.Remove(arg);

                 foreach (nAttachmentArgument arg in role.Arguments)
                 {
                     ContentAttachmentArgument nArg = null;
                     if(Role.Arguments.Any(e => e.Id == arg.Id))
                     {
                         nArg = Role.Arguments.First(e => e.Id == arg.Id);
                         Role.Arguments.Remove(nArg);
                                                 
                     }
                     else
                     {
                         nArg = new ContentAttachmentArgument();
                         nArg.Id = arg.Id;
                     }

                     nArg.Argumentlangline = arg.Argumentlangline;
                     nArg.Argumentlangpackage = arg.Argumentlangpackage;
                     nArg.ArgumentName = arg.ArgumentName;
                     nArg.RoleId = Role.Id;

                     Role.Arguments.Add(nArg);
                 }




                 Repo.Update(Role);
                 return true;

             }
             return false;
        }

        public static bool DeleteRole(int id)
        {
            ContentAttachmentRole Role = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(x => x.Id == id));
            if(Role != null)
            {
                Repo.Delete(Role);
                return true;
            }

            return false;
        }
    }
}
