namespace Ren.CMS.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.Content;
    using Ren.CMS.CORE.Config;
    using Ren.CMS.Filemanagement;
    using Ren.CMS.CORE.Language;
    using Ren.CMS.CORE.Language.LanguageDefaults;
    using Ren.CMS.CORE.Permissions;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.CORE.SqlHelper;
    using Ren.CMS.CORE.ThisApplication;
    using Ren.CMS.Helpers;
    using Ren.CMS.Models.Backend.Content;
    using Ren.CMS.Models.Core;
    using Ren.CMS.Persistence.Base;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.CORE.Helper.ModelStateHelper;
    using Ren.CMS.Persistence.Repositories;
    using Ren.Config.Helper;
    using Mvc.JQuery.Datatables;
    using Ren.CMS.CORE.DataTables.BackendModels;
    using Ren.CMS.CORE.Taskmanagement;
    using Ren.CMS.Content.Tasks;

    public class BackendHandlerContentController : Controller
    {
        #region Methods

        [AcceptVerbs(HttpVerbs.Post)]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_UPLOAD_CONTENT_ATTACHMENTS")]
        public JsonResult AddAttachment(List<AddAttachmentModel> Model)
        {
            if (ModelState.IsValid)
            {
                List<object> _Files = new List<object>();
                FilemanagementCrossBrowsersRepository Repo = new FilemanagementCrossBrowsersRepository();
                ConvertVideoTask Task = new ConvertVideoTask();
                List<nContentAttachment> ToConvert = new List<nContentAttachment>();
                Task.TaskData.Add("_FORMATS", Repo.GetAll().ToList());
                foreach(AddAttachmentModel model in Model)
                {
                    Ren.CMS.Content.ContentManagement.GetContent GC = new Content.ContentManagement.GetContent(model.ContentId, true, 0);
                    var clist = GC.getList();
                    if(clist.Count > 0)
                    {
                        nContent c = clist.First();
                        nAttachmentRole Role  = nAttachmentRoleManager.GetRoleById(model.RoleId);
                        nAttachmentArgument Argument = null;
                        if(!Role.Arguments.Any(e => e.Id == model.ArgumentId))
                            Argument = Role.Arguments.First();
                        else
                            Argument = Role.Arguments.First(e => e.Id == model.ArgumentId);

                        nContentAttachmenType type = nContentAttachmenTypeManager.GetTypeById(model.TypeId);
                        if(type == null)
                         continue;
                        nContentAttachment attachment = null;
                        if (model.Physical)
                           attachment = c.Attachments.AddAttachment(model.File, type, Role, Argument);
                        else
                           attachment = c.Attachments.AddAttachment(model.Url, type, Role, Argument);
                        if(attachment != null)
                        {
                            _Files.Add(new
                            {
                                deleteType = "POST",
                                deleteUrl = "/BackendHandler/Content/DeleteAttachment/" + attachment.AttachmentID,
                                name = attachment.File.AliasName,
                                size =  attachment.File.FileSize,
                                thumbnailUrl = Url.Content("~/ContentFiles/Thumpnail/"+attachment.File.Id +"/"+ attachment.File.AliasName),
                                type = MimeMapping.GetMimeMapping(attachment.File.FilePath),
                                url = Url.Content("~/ContentFiles/Orig/" + attachment.File.Id + "/" + attachment.File.AliasName)

                            });

                            if(MimeMapping.GetMimeMapping(attachment.File.FilePath).ToLower().StartsWith("video") && 
                                attachment.AttachmentType.Settings.Any(e => e.Extension == System.IO.Path.GetExtension(attachment.File.FilePath ) 
                                    && e.Convertfile) && attachment.File.Physical)
                            {
                                ToConvert.Add(attachment);
                            }

                        }


                    }
                }

                Task.TaskData.Add("_ATTACHMENTS", ToConvert);
                
                int taskId = Taskmanagement.RegisterTask<ConvertVideoTask>(Task);

                return Json(new { files = _Files, TaskID = taskId, Errors = ModelState.Errors() });
            }

            return Json(new { Errors = ModelState.Errors() });
            


        }

        [nPermissionVal(NeededPermissionKeys="USR_CAN_ADD_CONTENT_CATEGORY")]
        public ActionResult AddCategory(Models.Backend.Content.CategoryModel MDL)
        {
            if (!nPermissions.hasPermission("USR_CAN_ENTER_BACKEND")) return Content("Error: No Permission");
            if (!ModelState.IsValid) return Content("Error: Required Fields not filled out");

            SqlHelper SQL = new SqlHelper();
            if(MDL.subFrom == null)MDL.subFrom= "";
            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            PCOL.Add("@PKID", Guid.NewGuid());
            PCOL.Add("@shortName", MDL.shortName);
            PCOL.Add("@longName", MDL.longName);
            PCOL.Add("@contentType", MDL.contentType);
            PCOL.Add("@subFrom", MDL.subFrom);
            ThisApplication TA = new ThisApplication();

            string prefix = TA.getSqlPrefix;

            string query = "INSERT INTO " + prefix + "Categories (PKID, shortName,longName,contentType, subFrom) VALUES(@PKID,@shortName, @longName, @contentType, @subFrom)";

            SQL.SysConnect();

            SQL.SysNonQuery(query, PCOL);

            SQL.SysDisconnect();

            return Content("<span style=\"color:green\">Kategorie wurde erstellt</span>");
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_CREATE_CONTENT")]
        public JsonResult Content(nContentPostModel MDL )
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = LanguageDefaultsMessages.LANG_SHARED_MESSAGE_FORM_NOT_VALID.ReturnLangLine(),
                    errors = ModelState.Errors()

                });

            }

           Ren.CMS.Content.ContentManagement CtM = new Content.ContentManagement();
           Ren.CMS.Content.nContent ContentModel = new Content.nContent(MDL);
           object newID = null;

           if (ContentModel.ID < 1)
           {
               bool ok = CtM.InsertContent(ref ContentModel);

               if (ok)
               {
                   newID = ContentModel.ID;

               }
               else
               {

                   return Json(new
                   {
                       success = false,
                       message = LanguageDefaultsMessages.LANG_SHARED_UNKNOWN_SUBMITERROR.ReturnLangLine(),
                       errors = ModelState.Errors()

                   });
               
               }
           }
           else
           {
               CtM.UpdateContent(ContentModel);
           }



            return Json(new { newID = newID,  success = true, message = LanguageDefaultsMessages.LANG_SHARED_MESSAGE_FORM_CONTENT_SAVED.ReturnLangLine() });
            //Ren.CMS.Content.ContentValidator Cval = new Content.ContentValidator();

            //if (!Cval.isValidPostModelForInsert(MDL))

            //    return Json(new
            //    {
            //        success = false,
            //        message = LanguageDefaultsMessages.LANG_SHARED_MESSAGE_FORM_NOT_VALID,
            //        modelStateKeys = ModelState.ToDictionary(e => e.Key),
            //        modelStateValues = ModelState.ToDictionary(e => e.Value)
            //    });

            //MDL.Texts.ToList().ForEach(e => e.LongText = HttpUtility.UrlDecode(e.LongText));
            //var Props = MDL.GetType().GetProperties().Where(e => e.PropertyType == typeof(string));
            //foreach (var prop in Props)
            //    prop.SetValue(MDL, HttpUtility.UrlDecode((prop.GetValue(MDL) ?? String.Empty).ToString()));

            //Ren.CMS.Content.ContentManagement CtM = new Content.ContentManagement();
            //Ren.CMS.Content.nContent ContentModel = new Content.nContent(MDL);
            //CtM.InsertContent(ContentModel);
            //if (MDL.Tags != null)
            //    CtM.bindTagsToContent(MDL.ID, MDL.Tags);

            //
        }

        //
        // GET: /BackendHandlerContent/
        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public JsonResult Catlist()
        {
            List<object> _list = new List<object>();
            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            CORE.ThisApplication.ThisApplication TA = new CORE.ThisApplication.ThisApplication();

            string prefix = TA.getSqlPrefix;

            string query = "SELECT c.PKID as PKID, c.shortName as shortName,c.longName as longName, ISNULL(sub.shortName,'') as subFrom, c.contentType as contentType FROM " + prefix + "Categories c LEFT OUTER JOIN " + prefix + "Categories sub ON(c.subFrom = CAST(sub.PKID as varchar(250)))";

            SqlDataReader R = SQL.SysReader(query, new nSqlParameterCollection());
            if (R.HasRows)
            {
                Ren.CMS.CORE.Language.Language Lang = new CORE.Language.Language("__USER__", "CONTENT_TYPES");
                while (R.Read())
                {
                    string ctype = (R["contentType"] != DBNull.Value ? (string)R["contentType"] : "");
                    string ctLangLine = Lang.getLine("LANG_CTYPE_" + ctype.ToUpper());
                    if (String.IsNullOrEmpty(ctLangLine)) ctLangLine = ctype;
                    if (String.IsNullOrEmpty(ctype)) ctLangLine = "";
                    _list.Add(

                        new
                        {
                            id = ((object)R["PKID"]).ToString(),
                            cell = new
                            {
                                shortName = ((string)R["shortName"]),
                                longName = ((string)R["longName"]),
                                subFrom = (R["subFrom"] != DBNull.Value ? (string)R["subFrom"] : ""),
                                contentType =ctLangLine

                            }
                        }

                        );

                }

            }
            R.Close();

            SQL.SysDisconnect();

            return Json(new {total=_list.Count, page = 1, rows = _list } );
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public JsonResult CatTree(TreeViewCategory MDL)
        {
            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            string query = "SELECT c.PKID as PKID, c.shortName as shortName,c.longName as longName, ISNULL(sub.shortName,'') as subFrom, c.contentType as contentType FROM " + new ThisApplication().getSqlPrefix + "Categories c LEFT OUTER JOIN " + new ThisApplication().getSqlPrefix + "Categories sub ON(c.subFrom = CAST(sub.PKID as varchar(250))) WHERE ISNULL(c.subFrom,'') =''";

            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            if (MDL.ct != null)
            {
                query += " AND c.contentType=@ct";
                PCOL.Add("@ct", MDL.ct);
             }

            if (MDL.excludePKID != null)
            {

                query += " AND c.PKID != @id";
                PCOL.Add("@id", MDL.excludePKID);

            }

            SqlDataReader R = SQL.SysReader(query, PCOL);
            List<object> obj = new List<object>();
            if (R.HasRows)
            {

                while (R.Read())
                {

                    obj.Add(new
                    {
                        classes = "folder",
                        text = "<a class=\"CategoryTreeItem\" href=\"#" + R["PKID"].ToString() + "\">" + (string)R["shortName"] + "</a>",
                        children = this.renderCatsSub(R["PKID"].ToString(), MDL)

                    });
                }
            }

            R.Close();

            SQL.SysDisconnect();

            return Json(obj);
        }

        //
        // GET: /BackendHandlerContent/
        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public DataTablesResult<ContentListView> ContentList(DataTablesParam param)
        {
            List<ContentListView> _list = new List<ContentListView>();
            

            Ren.CMS.Content.ContentManagement.GetContent GC = new Content.ContentManagement.GetContent(new string[] { "*" }, categoryname: null, locked:false, pageIndex:0, pageSize:0, contentRef: 0,countReferences: false, referenceContentTypes: null);

            List<Ren.CMS.Content.nContent> Contents = GC.getList();

            foreach (Ren.CMS.Content.nContent Con in Contents)
            {

                _list.Add(

                    new ContentListView
                    {
                            ID  = Con.ID,
                            Title = (Con.Texts.Any(e => e.LangCode == Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage) ? Con.Texts.Where(e => e.LangCode == Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage).FirstOrDefault().Title : Con.Texts.First().Title),
                            Creator = Con.CreatorName,
                            Category = Con.CategoryName,
                            cDate = Con.CreationDate,
                            ContentType = Con.ContentType

                        
                    }

                    );

            }

            return DataTablesResult.Create<ContentListView>(_list.AsQueryable(), param);
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ADD_CONTENT_TAGS")]
        public JsonResult CreateTag(MngContentTag MDL)
        {
            if (ModelState.IsValid)
            {
                SqlHelper SQL = new SqlHelper();
                ThisApplication TA = new ThisApplication();
                string sqlprefix = TA.getSqlPrefix;
                string check = "SELECT * FROM " + TA.getSqlPrefix + "Content_Tags WHERE contentType=@ct AND tagName=@name";
                SQL.SysConnect();

                nSqlParameterCollection PCheck = new nSqlParameterCollection();
                PCheck.Add("@ct", MDL.contentType);
                PCheck.Add("@name", MDL.tagName);

                SqlDataReader Check = SQL.SysReader(check, PCheck);

                if (!Check.HasRows)
                {
                    //Good to go lets create the tag

                    Check.Close();

                    string nonquery = "INSERT INTO " + sqlprefix + "Content_Tags (contentType,tagName, enableBrowsing, tagNameSEO) VALUES(@ct,@name,@browsing,@seo)";
                    Ren.CMS.Content.ContentValidator Cval = new Content.ContentValidator();
                    string seoName = Cval.makeTitleSEOConform(MDL.tagName);

                    nSqlParameterCollection PINS = new nSqlParameterCollection();
                    PINS.Add("@ct", MDL.contentType);
                    PINS.Add("@name", MDL.tagName);
                    PINS.Add("@browsing", MDL.enableBrowsing);
                    PINS.Add("@seo", seoName);
                    SQL.SysNonQuery(nonquery, PINS);

                    SQL.SysDisconnect();

                    return Json(new { success = true, message = "" });

                }
                Check.Close();
                SQL.SysDisconnect();
            }

            return Json(new { success = false, message = "ERROR: Required field not set" });
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_DELETE_CONTENT_ATTACHMENTS|USR_CAN_DELETE_CONTENTS")]
        public JsonResult DeleteAttachment(string id)
        {
            deleteAttachment(id);

            return Json(new { success = true });
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys = "USR_CAN_DELETE_CONTENTS")]
        public JsonResult DeleteContent(int id)
        {
            try
            {
                new ContentManagement().DeleteContent(id);

                return Json(new { success = true, message = "" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });

            }
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_DELETE_CONTENT_TAGS")]
        public JsonResult DeleteTag(int id)
        {
            if (id > 0)
            {
                SqlHelper SQL = new SqlHelper();
                SQL.SysConnect();
                ThisApplication TA = new ThisApplication();
                string prefix = TA.getSqlPrefix;
                string query = "DELETE " + prefix + "Content_Tags WHERE id=@id";
                Language Lng = new Language("__USER__", "CONTENT_TAGS");
                Language LngDE = new Language("de-DE", "CONTENT_TAGS");
                Language LngEN = new Language("en-US", "CONTENT_TAGS");
                if (Lng.getLine("LANG_CT_TAGS_NOT_EXISTING") == "")
                {
                    if (LngDE.getLine("LANG_CT_TAGS_NOT_EXISTING") == "")
                    {
                        try
                        {
                            LngDE.InsertLine("LANG_CT_TAGS_NOT_EXISTING", "Das angeforderte Tag existiert nicht. M&ouml;glicher Weise wurde es bereits gel&ouml;scht!");

                        }
                        catch
                        {

                        }
                    }

                    if (LngEN.getLine("LANG_CT_TAGS_NOT_EXISTING") == "")
                    {
                        try
                        {
                            LngEN.InsertLine("LANG_CT_TAGS_NOT_EXISTING", "The requested tag does not exists. Maybe it was deleted allready!");

                        }
                        catch
                        {

                        }
                    }
                }
                    nSqlParameterCollection PCOL = new nSqlParameterCollection() { { "@id", id } };
                    SQL.SysNonQuery(query, PCOL);
                    if (SQL.SysCount("Content_Tags", "id", id.ToString()) == 0)
                    {
                        SQL.SysDisconnect();

                        return Json(new { success = true, message = "" });
                    }
                    else
                    {
                        return Json(new { success = false, message = LngDE.getLine("LANG_CT_TAGS_NOT_EXISTING") });
                    }

            }

            return Json(new { success = false, message = "" });
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_EDIT_CONTENT_ATTACHMENTS")]
        public JsonResult EditAttachment(EditAttachment MDL)
        {
            if (!ModelState.IsValid) return Json(new { success = false });
            ContentAttachmentRepository Repo = new ContentAttachmentRepository();

            var A = Repo.GetByPKid(Guid.Parse(MDL.id));

            if(A == null)
                return Json(new { success = false, message ="entity not found" });
             
            //What the heck? Update?
            ContentManagement.GetContent GC = new ContentManagement.GetContent(id: A.Content.Id);
            var list = GC.getList();
            if(list.Count == 0)
                return Json(new { success = false, message = "entity not found" });

            var content = list.First();

            var attachs = content.Attachments.GetAttachments();
            if(attachs.Any(e => e.AttachmentID == Guid.Parse(MDL.id)))
            {
                nContentAttachment attachment = attachs.First(e => e.AttachmentID == Guid.Parse(MDL.id));

                //Remarks
                attachment.Remarks.Clear();
                foreach(var remark in MDL.Remarks)
                {
                   var type = RemarkTypeManager.GetRemarkTypeById(remark.RemarkTypeId);
                   if(type != null)
                   {
                       nAttachmentRemark Remark = null;
                       BaseRepository<ContentAttachmentRemarks> Repo2 = new BaseRepository<ContentAttachmentRemarks>();
                       var entity = Repo2.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachmentRemarks>(e => e.Id == remark.RemarkId));
                       if (entity != null)
                           Remark = new nAttachmentRemark(entity);
                       else
                           Remark = new nAttachmentRemark(remark.RemarkText, type);

                       Remark.Remarktext = remark.RemarkText;
                       attachment.Remarks.Add(Remark);
                   }
                }

                //Texts
                attachment.Texts.Clear();
                foreach(var text in MDL.Texts)
                {
                    attachment.Texts.Add(text);
                }


                var handler = attachment.AttachmentType.Handler;
                handler.SetSource(attachment);
                handler.Update(attachment);

                return Json(new { success = true });
            }
 

            return Json(new { success = false });
        }

        [nPermissionVal(NeededPermissionKeys="USR_CAN_EDIT_CONTENT_CATEGORY")]
        public ActionResult EditCategory(Models.Backend.Content.CategoryModel MDL)
        {
            if (!nPermissions.hasPermission("USR_CAN_ENTER_BACKEND")) return Content("Error: No Permission");
            if (!ModelState.IsValid) return Content("Error: Required Fields not filled out");
            if (MDL.ID == null) return Content("Error: Not ID Recieved");
            SqlHelper SQL = new SqlHelper();
            if (MDL.subFrom == null) MDL.subFrom = "";
            string ID = Request.Form["ID"].ToString();
            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            PCOL.Add("@PKID", ID);
            PCOL.Add("@shortName", MDL.shortName);
            PCOL.Add("@longName", MDL.longName);
            PCOL.Add("@contentType", MDL.contentType);
            PCOL.Add("@subFrom", MDL.subFrom);
            ThisApplication TA = new ThisApplication();

            string prefix = TA.getSqlPrefix;

            string query = "UPDATE " + prefix + "Categories SET shortName= @shortName, longName= @longName, contentType = @contentType, subFrom= @subFrom WHERE PKID=@PKID";

            SQL.SysConnect();

            SQL.SysNonQuery(query, PCOL);

            SQL.SysDisconnect();

            return Content("<span style=\"color:green\">Kategorie wurde editiert</span>");
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_EDIT_CONTENT")]
        public JsonResult EditContent(Models.Core.nContentPostModel MDL, nContentTextBinder Binder)
        {
           

            Ren.CMS.Content.ContentValidator Cval = new Content.ContentValidator();

            if (!Cval.isValidPostModelForInsert(MDL))

                return Json(new { success = false, message = LanguageDefaultsMessages.LANG_SHARED_MESSAGE_FORM_NOT_VALID,
                    modelStateKeys = ModelState.ToDictionary(e => e.Key), modelStateValues = ModelState.ToDictionary(e => e.Value)});

            MDL.Texts.ToList().ForEach(e => e.LongText = HttpUtility.UrlDecode(e.LongText));
            var Props = MDL.GetType().GetProperties().Where(e => e.PropertyType == typeof(string));
            foreach (var prop in Props)
                prop.SetValue(MDL, HttpUtility.UrlDecode((prop.GetValue(MDL) ?? String.Empty).ToString()));

            Ren.CMS.Content.ContentManagement CtM = new Content.ContentManagement();
            Ren.CMS.Content.nContent ContentModel = new Content.nContent(MDL);
               CtM.UpdateContent(ContentModel);
               if(MDL.Tags != null)
               CtM.bindTagsToContent(MDL.ID, MDL.Tags);

            return Json(new { success = true, message = LanguageDefaultsMessages.LANG_SHARED_MESSAGE_FORM_CONTENT_SAVED.ReturnLangLine() });
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_EDIT_CONTENT_TAGS")]
        public JsonResult EditTag(MngContentTag MDL)
        {
            if (ModelState.IsValid && MDL.id > 0)
            {
                SqlHelper SQL = new SqlHelper();
                ThisApplication TA = new ThisApplication();

                string sqlprefix = TA.getSqlPrefix;

                bool changed = false;
                SQL.SysConnect();
                string queryChanged = "SELECT * FROM " + TA.getSqlPrefix + "Content_Tags WHERE id=@id AND tagName=@name";
                nSqlParameterCollection PCOL1 = new nSqlParameterCollection();
                PCOL1.Add("@id", MDL.id);
                PCOL1.Add("@name", MDL.tagName);
                SqlDataReader Changed = SQL.SysReader(queryChanged, PCOL1);
                changed = (!Changed.HasRows ? true : false);
                Changed.Close();

                string check = "SELECT * FROM " + TA.getSqlPrefix + "Content_Tags WHERE contentType=@ct AND tagName=@name";

                nSqlParameterCollection PCheck = new nSqlParameterCollection();
                PCheck.Add("@ct", MDL.contentType);
                PCheck.Add("@name", MDL.tagName);

                SqlDataReader Check = SQL.SysReader(check, PCheck);

                if (!Check.HasRows || !changed)
                {
                    //Good to go lets create the tag

                    Check.Close();

                    string nonquery = "UPDATE " + sqlprefix + "Content_Tags SET contentType=@ct, tagName = @name , enableBrowsing = @browsing, tagNameSEO = @seo WHERE id = @id";
                    Ren.CMS.Content.ContentValidator Cval = new Content.ContentValidator();
                    string seoName = Cval.makeTitleSEOConform(MDL.tagName);

                    nSqlParameterCollection PINS = new nSqlParameterCollection();
                    PINS.Add("@ct", MDL.contentType);
                    PINS.Add("@name", MDL.tagName);
                    PINS.Add("@browsing", MDL.enableBrowsing);
                    PINS.Add("@seo", seoName);
                    PINS.Add("@id", MDL.id);

                    SQL.SysNonQuery(nonquery, PINS);

                    SQL.SysDisconnect();

                    return Json(new { success = true, message = "" });

                }
                else
                {

                    Check.Close();
                    SQL.SysDisconnect();
                    return Json(new { success = false, message = "ERROR: Tag does allready exists in this Content Type" });

                }

            }

            return Json(new { success = false, message = "ERROR: Required field not set" });
        }

        [HttpGet]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public PartialViewResult GetAttachmentInfo(string id)
        {
            ContentAttachmentRepository Repo = new ContentAttachmentRepository();

            var Attachment = Repo.GetOne(NHibernate.Criterion.Expression.Where<ContentAttachment>(e => e.Pkid == Guid.Parse(id)));
            nContentAttachment Attach = new nContentAttachment(Attachment);
            EditAttachment Model = new EditAttachment();
            Model.ArgumentId = Attachment.Argument.Id;
            Model.id = Attachment.Pkid.ToString();
            Model.RoleId = Attachment.Role.Id;
            Model.Remarks = new List<vAttachmentRemark>();
            foreach(var remark in Attach.Remarks)
            {
                Model.Remarks.Add(new vAttachmentRemark() { RemarkId = remark.Id, RemarkText = remark.Remarktext, RemarkTypeId = remark.Type.Id });
            }

            Model.Texts = Attach.Texts;

            return PartialView("~/Views/Backend/widgets/__SHARED/Content/Modals/EditFile.cshtml", Model);
            
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public DataTablesResult<ContentAttachmentListView> GetAttachments(int id, DataTablesParam param) 
        {
            List<ContentAttachmentListView> _list = new List<ContentAttachmentListView>();

            Ren.CMS.Content.ContentManagement.GetContent GC = new Content.ContentManagement.GetContent(id, true, 0);

            List<Ren.CMS.Content.nContent> C = GC.getList();

           

            Ren.CMS.Content.nContent Con = C[0];

            var listOfAttachments = Con.Attachments.GetAttachments();


            foreach (Ren.CMS.Content.nContentAttachment Att in listOfAttachments)
            {
                string shadowboxCommand = "shadowbox[Mixed]";
                string title = "";
                if (Att.Texts.Any(e => e.LangCode == Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage))
                    title = Att.Texts.First(e => e.LangCode == Ren.CMS.CORE.Helper.CurrentLanguageHelper.CurrentLanguage).Title;
                else if (Att.Texts.Any(e => e.LangCode == Ren.CMS.CORE.Helper.CurrentLanguageHelper.DefaultLanguage))
                    title = Att.Texts.First(e => e.LangCode == Ren.CMS.CORE.Helper.CurrentLanguageHelper.DefaultLanguage).Title;
                else if (Att.Texts.Count > 0)
                    title = Att.Texts.First().Title;
                else
                    title = Att.AttachmentID.ToString();
                string path = Att.File.FilePath;
                    if (Att.GetFileType(true) == "video")
                    {

                        path = "/SharedElements/JPlayer?filePath=" + Att.File.FilePath + "&Title=" + HttpUtility.UrlEncode(title) + "width=640&height=264";
                        shadowboxCommand += ";width=640;height=264";
                    }

                
                string op = "<a href=\"javascript: editAttachment('"+ Att.AttachmentID +"')\">Anpassen</a> | <a href=\"javascript: deleteAttachment('"+ Att.AttachmentID +"')\">Löschen</a>";
                string fname = "<a href=\"" + path + "\" rel=\""+ shadowboxCommand +"\">" + Att.File.AliasName + "</a>";
                string argument = "";
                if(Att.Argument != null)
                {
                    Language lang = new Language("__USER__", Att.Argument.Argumentlangpackage);
                    argument = lang.getLine(Att.Argument.Argumentlangline);
                }
                string role = "";

                 if(Att.Role != null)
                {
                    Language lang = new Language("__USER__", Att.Role.Rolelangpackage);
                    role = lang.getLine(Att.Role.Rolelangline);
                }

                string fullRole = role + (!String.IsNullOrEmpty(argument) ? " -> "+ argument : "");

                _list.Add(

                    new ContentAttachmentListView() { ID = Att.AttachmentID.ToString() , FileName = fname, Role = fullRole, FileType = Att.GetFileType() }

                        
                        );

            }

            return DataTablesResult.Create(_list.AsQueryable(), param);
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public JsonResult GetContentTypes()
        {
            //Content Combobox
            Ren.CMS.CORE.ThisApplication.ThisApplication TA = new Ren.CMS.CORE.ThisApplication.ThisApplication();
            string prefix = TA.getSqlPrefix;

            string query = "SELECT *  FROM " + prefix + "Content_Types";

            SqlHelper SQL = new SqlHelper();

            SQL.SysConnect();

            System.Data.SqlClient.SqlDataReader Row = SQL.SysReader(query, new nSqlParameterCollection());

            List<object> ContentTypes = new List<object>();

            Language Lang = new Language("__USER__", "CONTENT_TYPES");
            if (Row.HasRows)
            {

                while (Row.Read())
                {
                    string ctLangLine = Lang.getLine("LANG_CTYPE_" + ((string)Row["name"]).ToUpper());
                    ContentTypes.Add(new { Label = ctLangLine, Value = ((string)Row["name"]) });

                }

            }
            Row.Close();

            SQL.SysDisconnect();
            return Json(new { ContentTypes = ContentTypes });
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public JsonResult GetTagData(int id)
        {
            Dictionary<string, object> Cols = new Dictionary<string,object>();
            if (id > 0)
            {
                SqlHelper SQL = new SqlHelper();
                ThisApplication TA = new ThisApplication();

                string prefix = TA.getSqlPrefix;

                SQL.SysConnect();
                string query = "SELECT id, contentType, tagName, enableBrowsing FROM " + prefix + "Content_Tags WHERE id=@id";
                nSqlParameterCollection PCOL = new nSqlParameterCollection();
                PCOL.Add("@id", id);
                SqlDataReader Row = SQL.SysReader(query, PCOL);
                if (Row.HasRows)
                {
                    Row.Read();
                    for (int x = 0; x < Row.FieldCount; x++)
                    {
                        string colName = Row.GetName(x);

                        Cols.Add(colName, Row[colName].ToString());

                    }

                }
                Row.Close();

                SQL.SysDisconnect();

            }

            return Json(Cols);
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public JsonResult GetTags(Models.Core.FlexyGridPostParameters FlexyGridPost)
        {
            if (!ModelState.IsValid)
            {
                FlexyGridPost.page = 1;
                FlexyGridPost.sortname = "tagName";
                FlexyGridPost.sortorder = "ASC";
                FlexyGridPost.rp = 15;

            }

            FlexyGridPost.sortname = FlexyGridPost.sortname.Replace('-', '_').Replace("'", "");
            FlexyGridPost.sortorder = FlexyGridPost.sortorder.Replace('-', '_').Replace("'", "");

            SqlHelper SQL = new SqlHelper();

            ThisApplication TA = new ThisApplication();

            string prefix = TA.getSqlPrefix;

            string query = "     ;WITH pagination as (" +
                             "SELECT   dense_rank() over (order by "+ FlexyGridPost.sortname +",id "+ FlexyGridPost.sortorder +") as rowNo,id" +
                             ",contentType" +
                             ",tagName" +
                             ",enableBrowsing" +
                             ",tagNameSEO " +
                             "FROM "+prefix+"Content_Tags" +
                             ")" +
                             "SELECT *,(SELECT COUNT(*) from pagination) as TotalRows FROM pagination WHERE rowNo BETWEEN @i and @ii";

            int pageSize = FlexyGridPost.rp;
            int pageStart = (FlexyGridPost.page-1) * pageSize;
            int pageEnd = FlexyGridPost.page * pageSize;
            int totalRows = 0;
            nSqlParameterCollection PCOL = new nSqlParameterCollection();

            PCOL.Add("@i", pageStart);
            PCOL.Add("@ii", pageEnd);

            List<object> Rows = new List<object>();

            SQL.SysConnect();

            SqlDataReader Tags = SQL.SysReader(query, PCOL);

            if (Tags.HasRows)
            {

                while (Tags.Read())
                {

                    object row = new
                        {
                            id = ((int)Tags["id"]).ToString(),
                            cell = new
                            {
                                contentType = ((string)Tags["contentType"]),
                                tagName = ((string)Tags["tagName"]),
                                enableBrowsing = ((int)Tags["enableBrowsing"] == 1 ? "Y" : "N")

                            }
                        };

                    Rows.Add(row);

                    totalRows = (int)Tags["TotalRows"];

                }

            }

            Tags.Close();

            SQL.SysDisconnect();

            return Json(new { total =totalRows, page = FlexyGridPost.page, rows = Rows });
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public JsTreeJsonResult JSTREE_CATEGORIES(TreeViewCategory MDL)
        {
            JsTreeJsonResult JSRES = new JsTreeJsonResult();
            if (MDL.Selector != null)
            {

                if (MDL.Selector == "subSelector")
                {

                    if (MDL.node_id == "" || MDL.node_id == "0")
                    {

                        JSRES.addNode("NO", "main", "-/-");

                        return JSRES;
                    }
                    if (MDL.node_id == "NO")
                    {
                        MDL.node_id = "";

                    }

                }

            }

            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            string query = "SELECT c.PKID as PKID, c.shortName as shortName,c.longName as longName, ISNULL(sub.shortName,'') as subFrom, c.contentType as contentType FROM " + new ThisApplication().getSqlPrefix + "Categories c LEFT OUTER JOIN " + new ThisApplication().getSqlPrefix + "Categories sub ON(c.subFrom = CAST(sub.PKID as varchar(250))) WHERE ISNULL(c.subFrom,'') =@node_id";

            MDL.ct = MDL.ContentType;
            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            if (MDL.ct != null)
            {
                query += " AND c.contentType=@ct";
                PCOL.Add("@ct", MDL.ct);
            }

            if (MDL.excludePKID != null)
            {

                query += " AND c.PKID != @id";
                PCOL.Add("@id", MDL.excludePKID);

            }
            if (MDL.node_id == "0")
            {
                MDL.node_id = "";
            }

            PCOL.Add("@node_id", MDL.node_id);

            SqlDataReader R = SQL.SysReader(query, PCOL);
            List<object> obj = new List<object>();
            if (R.HasRows)
            {

                while (R.Read())
                {

                    JSRES.addNode(R["PKID"].ToString(), "drive", (string)R["shortName"]);

                }
            }

            R.Close();

            SQL.SysDisconnect();

            return JSRES;
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_EDIT_CONTENT_CATEGORY")]
        public JsonResult MoveCategory(TreeViewCatMover MDL)
        {
            if (!ModelState.IsValid)
            {
                return Json(new  { status = false, id = false });
            }
            if (MDL.parent == null) MDL.parent = "";
            SqlHelper SQL = new SqlHelper();

            string pref = new ThisApplication().getSqlPrefix;

            string query = "UPDATE " + pref + "Categories SET subFrom=@parent WHERE PKID=@id";

            nSqlParameterCollection PCOL = new nSqlParameterCollection();

            PCOL.Add("@parent", MDL.parent);
            PCOL.Add("@id", MDL.id);
            SQL.SysConnect();

            SQL.SysNonQuery(query, PCOL);

            SQL.SysDisconnect();

            return Json(new { status = true, id = MDL.id });
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_DELETE_CONTENT_CATEGORIES")]
        public ActionResult RemoveCat()
        {
            if (!nPermissions.hasPermission("USR_CAN_ENTER_BACKEND")) return Content("Error: No Permission");
            SqlHelper SQL = new SqlHelper();
            if (Request.Form["ID"] == null || Request.Form["ID"] == "")
            {

                return Content("Error: No ID recieved");
            }
            string ID = Request.Form["ID"].ToString();

            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            PCOL.Add("@ID", ID);
              ThisApplication TA = new ThisApplication();

            string prefix = TA.getSqlPrefix;
            string query = "DELETE " + prefix + "Categories WHERE PKID=@ID";

            //Reset Subfrom

            string update = "UPDATE " + prefix + "Categories SET subFrom='' WHERE subFrom=@ID";
            nSqlParameterCollection PCOL2 = new nSqlParameterCollection();
            PCOL2.Add("@ID", ID);

            SQL.SysConnect();

            SQL.SysNonQuery(query, PCOL);

            SQL.SysNonQuery(update, PCOL2);

            SQL.SysDisconnect();

            return Content("Kategorie wurde gelöscht");
        }

        [HttpPost]
        [nPermissionVal(NeededPermissionKeys="USR_CAN_ENTER_BACKEND")]
        public JsonResult ValidateSEOTitle(ValidateSEOModel Mdl)
        {
            if(!ModelState.IsValid)
                return Json( new{seoname=""});
            Ren.CMS.Content.ContentValidator Cval = new Content.ContentValidator();

            return Json(new { seoname = Cval.makeTitleSEOConform(Mdl.title)});
        }

        private void deleteAttachment(string id)
        {
            new ContentManagement().DeleteAttachment(id);
        }

        private List<object> renderCatsSub(string id, TreeViewCategory MDL)
        {
            SqlHelper SQL = new SqlHelper();
            SQL.SysConnect();
            string query = "SELECT c.PKID as PKID, c.shortName as shortName,c.longName as longName, ISNULL(sub.shortName,'') as subFrom, c.contentType as contentType FROM " +  new ThisApplication().getSqlPrefix + "Categories c LEFT OUTER JOIN " + new ThisApplication().getSqlPrefix + "Categories sub ON(c.subFrom = CAST(sub.PKID as varchar(250))) WHERE c.subFrom=@id ";
            nSqlParameterCollection PCOL = new nSqlParameterCollection();
            PCOL.Add("@id",id);
            if (MDL.ct != null)
            {

                query += " AND c.contentType = @ct";
                PCOL.Add("@ct", MDL.ct);

            }

            if (MDL.excludePKID != null)
            {

                query += " AND c.PKID != @pk";
                PCOL.Add("@pk", MDL.excludePKID);

            }

            SqlDataReader R = SQL.SysReader(query, PCOL);
            List<object> obj = new List<object>();
            if (R.HasRows)
            {

                while (R.Read())
                {

                    obj.Add(new {
                        classes = "folder",
                        text = "<a class=\"CategoryTreeItem\" href=\"#" + R["PKID"].ToString() + "\">" + (string)R["shortName"] + "</a>",
                        children = this.renderCatsSub(R["PKID"].ToString(), MDL)

                    });
                }
            }

            R.Close();

            SQL.SysDisconnect();

            return obj;
        }

        #endregion Methods
    }
}