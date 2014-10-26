using Ren.CMS.Content.ContentAttachmentHandlers;
using Ren.CMS.Persistence.Domain;
using Ren.CMS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ren.CMS.Content
{
    public class nContentAttachment
    {
        #region private fields
       
        private int _ContentID = 0;
        private ContentAttachmentHandlerBase _Handler = null;
        private string _FilePath = null;
        private string _Title = null;
        private int _Usage = 0;
        private string _RoleName = null;
        private string _RoleNameLang = null;
        private string _Thumbnail = null;
        private Guid _PKDID = new Guid();
        private bool isNew = false;
        private string _AttachmentType = null;
        #endregion

        #region Construct
        public nContentAttachment(ContentAttachment attachmentEntity)
        :this(attachmentEntity.Contentid,attachmentEntity.Pkid, attachmentEntity.AttachmentType.HandlerNamespace, attachmentEntity.Title, attachmentEntity.Usage, attachmentEntity.Thumnailpath, attachmentEntity.AttachmentType.Typename)
        {

        }
        public nContentAttachment(int contentID, string handler , string title, int usage, string thumbnail, string attachmentType)
        :this(contentID, Guid.NewGuid(),handler, null, title,usage, thumbnail, attachmentType)
        {
            this.isNew = true;
        }
        public nContentAttachment (int contentID,Guid pkid, string handler, string filePath, string title, int usage, string thumbnail, string attachmentType)
        {
            //Load Handler
            Type HandlerType = Type.GetType(handler);
            if (!HandlerType.IsClass)
                throw new Exception("Invalid Handler Type: Expected Typeof Class got" + HandlerType.ToString());
           //check if handler is based on ContentAttachmentHandlerBase
           ContentAttachmentHandlers.ContentAttachmentHandlerBase Base =   Activator.CreateInstance(HandlerType, this) as ContentAttachmentHandlers.ContentAttachmentHandlerBase;
           if (Base == null)
               throw new Exception("Requested Handler: " + handler + " is not based on Ren.CMS.Content.ContentAttachmentHandlers.ContentAttachmentHandlerBase");

           this._Handler = Base;
           this._FilePath = filePath;
           this._Thumbnail = thumbnail;
           this._Title = title;
           this._Usage = usage;
           this._AttachmentType = attachmentType;
           //Check if Content Exists
           ContentManagement.GetContent GC = new ContentManagement.GetContent(id: contentID, locked: true);
           nContent Entry = (GC.getList().Count > 0  ? GC.getList().First() : null);
           if(Entry == null)
               throw new Exception("Content#ID:"+ contentID +" does not exists");
            this._ContentID = contentID;
            this._PKDID = pkid;

            ContentAttachmentRoleRepository RoleRepo = new ContentAttachmentRoleRepository();
            ContentAttachmentRole Role = RoleRepo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(e => e.Id == this._Usage));
            Ren.CMS.CORE.Language.Language Lang = new CORE.Language.Language("__USER__", Role.Rolelangpackage);
            this._RoleName = Role.Rolename;
            this._RoleNameLang = Lang.getLine(Role.Rolelangline);
        }
        #endregion

        #region Properties

        public ContentAttachmentHandlerBase Handler { get { return this._Handler; } }

        public string Title { get { return this._Title; } set { this._Title = value; } }


        public string FilePath { get { return this._FilePath; } set { this._FilePath = value; } }

        public string Thumbnail { get { return this._Thumbnail; } set { this._Thumbnail = value; } }

        public int Usage { get { return this._Usage; }}

        public string RoleName { get { return this._RoleName; }}

        public string RoleNameLocal { get { return this._RoleNameLang; } }

        public Guid PKID 
        { 
            get {  return this._PKDID; } 
        }
        public Uri URL { get { return this._Handler.GetUrl();  } }

        #endregion

        #region Methods

        public void Save(string FileFieldName = null)
        {
           

             if(this.isNew)
             {
                 //Load Settings for AttachmentType
                 ContentAttachmentTypeRepository Repo = new ContentAttachmentTypeRepository();
                 ContentAttachmenttypes setting = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmenttypes>(e => e.Typename == this._AttachmentType));
                 ContentAttachmentRoleRepository RoleRepo = new ContentAttachmentRoleRepository();
                 ContentAttachmentRole Role = RoleRepo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRole>(e => e.Id == this._Usage));
                 ContentAttachmentRepository AttachRepo = new ContentAttachmentRepository();

                 if (setting == null)
                     throw new Exception("Attachment Type does not exists!");
                 //Insert to DB to get ID
                 try
                 {
                     ContentAttachment NewEntry = new ContentAttachment();
                     NewEntry.Pkid = Guid.NewGuid();
                     NewEntry.Filepath = this._FilePath;
                     NewEntry.AttachmentTypeId = setting.Id;
                     NewEntry.AttachmentType = setting;
                     NewEntry.Contentid = this._ContentID;
                     NewEntry.Usage = this._Usage;
                     NewEntry.Thumnailpath = this._Thumbnail;
                     NewEntry.Role = Role;
                     NewEntry.Title = this._Title;

                     NewEntry.Pkid = (Guid)AttachRepo.AddAndGetId(NewEntry);
                     new Task(() =>
                     {
                         //Upload
                         if (FileFieldName == null)
                         {
                             //Handle all Files like that
                             var files = HttpContext.Current.Request.Files;

                             foreach (HttpPostedFileBase File in files)
                             {
                                 this._Handler.Upload(File);
                             }
                         }
                         else
                         {
                             //Handle only this file field
                             var files = HttpContext.Current.Request.Files.GetMultiple(FileFieldName);

                             foreach (HttpPostedFile File in files)
                             {
                                 this._Handler.Upload(File);
                             }

                         }
                     }).RunSynchronously();
                     //Convert

                     this.Handler.Convert();
                     //Start Task in Handler
                 }
                 catch
                 {

                 }

             }
             else
             {
                 //Update
             }
        }


        #endregion
    }
}

