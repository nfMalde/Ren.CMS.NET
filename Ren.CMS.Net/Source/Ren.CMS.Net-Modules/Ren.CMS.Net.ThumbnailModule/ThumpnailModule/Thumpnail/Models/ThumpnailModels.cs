namespace ThumpnailModule.Thumpnail.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

  

    using Ren.CMS.Content;
    using Ren.CMS.CORE.FileManagement;
    using Ren.CMS.CORE.Settings;
    using Ren.CMS.Persistence.Base;
    using Ren.CMS.Persistence.Domain;
    using Ren.CMS.Persistence.Mapping;
    using Ren.CMS.Persistence.Repositories;
    using Ren.Config.Helper;

    using ThumpnailModule.Thumpnail.Domain;
    using ThumpnailModule.Thumpnail.Models;
    using ThumpnailModule.Thumpnail.Repositories;

    public class ThumpnailModel
    {
        #region Fields

        private string attachID;
        private int contentID;
        private string FileName;
        private int Height;
        private int Width;

        #endregion Fields

        #region Constructors

        public ThumpnailModel(int contentID, Guid attachID, string FileName = "", int Width = 64, int Height = 64)
        {
            this.Init(contentID, attachID, FileName, Width, Height);
        }

        #endregion Constructors

        #region Properties

        public TBThumpnailsModule Entity
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        private void Init(int contentID, Guid attachID, string FileName, int Width, int Height)
        {
            // ContentManagement.GetContent G = new ContentManagement.GetContent(id: contentID);
            // var cL = G.getList();

            if (0 == 0)
            {
                ContentAttachmentRepository Attach = new ContentAttachmentRepository();

                var img = Attach.GetByPKid(attachID);
                string fileName = Path.GetFileName(img.FPath);
                string filePath = img.FPath;
                if (img != null)
                {

                    //TODO: Resize of image and save
                    ThumpnailRepository Repo = new ThumpnailRepository();

                    var thump = Repo.GetOne(NHibernate.Criterion.Expression.Where<TBThumpnailsModule>(e => e.AtID == attachID && e.Height == Height && e.Width == Width));
                    if (thump != null)
                    {
                        try
                        {
                            string path2file = HttpContext.Current.Server.MapPath(thump.Path);

                            if (!System.IO.File.Exists(path2file))
                            {
                                Repo.Delete(thump);

                                Init(contentID, attachID, FileName, Width, Height);

                            }

                            else
                            {

                                if (thump.LastModification != System.IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath(thump.Path)))
                                {
                                    thump.LastModification = System.IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath(thump.Path));
                                    Repo.Update(thump);

                                }

                                Entity = thump;
                            }
                        }
                        catch (Exception)
                        {
                            Repo.Delete(thump);
                            try
                            {

                                System.IO.File.Delete(thump.Path);
                            }
                            catch
                            {

                                System.IO.File.Delete(HttpContext.Current.Server.MapPath(thump.Path));
                            }
                            Init(contentID, attachID, FileName, Width, Height);
                        }

                    }

                    else
                    {
                        if (img.AttachmentType == "video")
                        {
                            //new VideoThumpnailGenerator(contentID, attachID, FileName, Width, Height);

                            #region GLB_CONVERT_MOVIE_FFMEG_PATH
                            if (!RenConfigHelper.Settings<GlobalSettings>.Exists("GLB_CONVERT_MOVIE_FFMEG_PATH"))
                            {
                                RenConfigHelper.Settings<GlobalSettings>.Add(new nSetting()
                                {
                                    Name = "GLB_CONVERT_MOVIE_FFMEG_PATH",
                                    CategoryName = "CONTENT_SETTINGS",
                                    DefaultValue = HttpContext.Current.Server.MapPath("~/Binaries/Converter/FFMPEG.exe"),
                                    Description = new Ren.CMS.CORE.Language.LanguageDefaults.LanguageDefaultValues("LANG_GLB_CONVERT_MOVIE_FFMEG_PATH", "GLOBAL_SETTINGS"){
                                {"de-DE", "Geben Sie hier den kompletten physikalischen Pfad zur FFMPEG.exe an."},
                                {"en-US", "Enter here the complete physical path to FFMPEG.exe"}
                            }.ReturnLangLine(),
                                    DescriptionLanguageLine = "LANG_GLB_CONVERT_MOVIE_FFMEG_PATH",
                                    Label = new Ren.CMS.CORE.Language.LanguageDefaults.LanguageDefaultValues("LANG_GLB_CONVERT_MOVIE_FFMEG_PATH_LABEL", "GLOBAL_SETTINGS"){
                                {"de-DE", "Pfad zur FFMEG.exe"},
                                {"en-US", "Path to FFMEG.exe"}
                            }.ReturnLangLine(),
                                    LabelLanguageLine = "LANG_GLB_CONVERT_MOVIE_FFMEG_PATH_LABEL",
                                    PermissionBackend = "CAN_CONFIGURE_CONTENTTYPES",
                                    PermissionFrontend = "CAN_CONFIGURE_CONTENTTYPES",
                                    SettingRelation = "GLOBAL_SETTINGS",
                                    SettingType = nSettingType.SettingString,
                                    Value = HttpContext.Current.Server.MapPath("~/Binaries/Converter/FFMEG.exe"),
                                    ValueType = nValueType.ValueString

                                });
                            }
                            #endregion

                            string ffmegPath = RenConfigHelper.Settings<GlobalSettings>.Get("GLB_CONVERT_MOVIE_FFMEG_PATH").Value.ToString();

                            //Hack
                            string pathForThumpnail = new FileManagement().getVideoThumpnailRawImage(attachID, ffmegPath);

                            System.Drawing.Image thumpN = System.Drawing.Image.FromFile(pathForThumpnail).GetThumbnailImage(Width, Height, () => false, IntPtr.Zero);

                            new ThumpnailRepository().Add(
                            new TBThumpnailsModule()
                            {
                                AtID = attachID,
                                Height = Height,
                                Width = Width,
                               LastModification = DateTime.Now

                            }

                                );

                            TBThumpnailsModule Thumpthump = new ThumpnailRepository().GetOne(NHibernate.Criterion.Expression.Where<TBThumpnailsModule>(e => e.AtID == attachID && e.Height == Height && e.Width == Width));

                            //Success we got it
                            string[] neededPaths = new string[]{
                            "~/Thumpnails",
                            "~/Thumpnails/Storage",
                            "~/Thumpnails/Storage/"+ Thumpthump.Id,
                            "~/Thumpnails/Storage/"+ Thumpthump.Id +"/" + Width +"x"+ Height

                        };

                            foreach (string p in neededPaths)
                            {
                                if (!Directory.Exists(HttpContext.Current.Server.MapPath(p)))
                                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(p));

                            };

                            thumpN.Save(
                                filename: HttpContext.Current.Server.MapPath(neededPaths.Last() + "/" + Thumpthump.AtID.ToString() + ".thump.jpeg"),
                                format: ImageFormat.Jpeg);
                            Thumpthump.Path = neededPaths.Last() + "/" + Thumpthump.AtID.ToString() + ".thump.jpeg";
                            Thumpthump.LastModification = new FileInfo(HttpContext.Current.Server.MapPath(neededPaths.Last() + "/" + Thumpthump.AtID.ToString() + ".thump.jpeg")).LastWriteTime;

                            new ThumpnailRepository().Update(Thumpthump);

                            this.Init(contentID, attachID, FileName, Width, Height);

                        }
                        else
                        {
                            string tempfile = "";
                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Thumpnails/temp.down")))
                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Thumpnails/temp.down"));
                            //Save File from url
                            using (WebClient client = new WebClient())
                            {
                                string fullPath = img.FPath + "/" + img.FName;

                                Uri downloadUri = null;

                                tempfile = img.Pkid + ".thump." + System.IO.Path.GetExtension(fullPath);
                                if (img.AttachmentType == "video")
                                {
                                    tempfile = tempfile + ".jpg";

                                    System.IO.File.Copy(
                                        HttpContext.Current.Server.MapPath("~/VideoImageData/" + img.Pkid + ".jpg"),
                                         HttpContext.Current.Server.MapPath("~/Thumpnails/temp.down/" + tempfile)
                                         );

                                }
                                else
                                {
                                    try
                                    {
                                        client.DownloadFile(img.FPath + "/" + img.FName, HttpContext.Current.Server.MapPath("~/Thumpnails/temp.down/" + tempfile));
                                    }
                                    catch
                                    {
                                        string RequestUrl = HttpContext.Current.Request.Url.ToString();
                                        string px = img.FPath;
                                        if (img.FPath.StartsWith("/"))
                                        {
                                            px = px.Substring(1);
                                        }

                                        RequestUrl = RequestUrl.Remove(RequestUrl.LastIndexOf("/Thumpnail")) + "/" + px + "/" + img.FName;

                                        downloadUri = new Uri(RequestUrl);

                                        client.DownloadFile(downloadUri, HttpContext.Current.Server.MapPath("~/Thumpnails/temp.down/" + tempfile));

                                    }

                                }
                            }
                            //Resize
                            TBThumpnailsModule ThumpNew = new TBThumpnailsModule();
                            ThumpNew.AtID = attachID;
                            ThumpNew.Height = Height;
                            ThumpNew.Width = Width;
                            ThumpNew.LastModification = DateTime.Now;

                            System.Drawing.Image Temp = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/Thumpnails/temp.down/" + tempfile));
                            System.Drawing.Image thumpN = Temp.GetThumbnailImage(Width, Height, () => false, IntPtr.Zero);
                            Repo.Add(ThumpNew);
                            thump = Repo.GetOne(NHibernate.Criterion.Expression.Where<TBThumpnailsModule>(e => e.AtID == attachID && e.Height == Height && e.Width == Width));

                            //Success we got it
                            string[] neededPaths = new string[]{
                            "~/Thumpnails",
                            "~/Thumpnails/Storage",
                            "~/Thumpnails/Storage/"+ thump.Id,
                            "~/Thumpnails/Storage/"+ thump.Id +"/" + Width +"x"+ Height

                        };

                            foreach (string p in neededPaths)
                            {
                                if (!Directory.Exists(HttpContext.Current.Server.MapPath(p)))
                                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(p));

                            };

                            thumpN.Save(
                                filename: HttpContext.Current.Server.MapPath(neededPaths.Last() + "/" + thump.AtID.ToString() + ".thump.jpeg"),
                                format: ImageFormat.Jpeg);

                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(neededPaths.Last() + "/" + thump.AtID.ToString() + ".thump.jpeg")))
                            {

                                thump.LastModification = System.IO.File.GetLastWriteTime(HttpContext.Current.Server.MapPath(neededPaths.Last() + "/" + thump.AtID.ToString() + ".thump.jpeg"));
                                thump.Path = (neededPaths.Last().Substring(1) + "/" + thump.AtID.ToString() + ".thump.jpeg");
                                Entity = thump;
                                Repo.Update(thump);
                            }
                            else
                            {

                                Repo.Delete(entity: thump);
                            }

                        }

                    }

                }

            }
        }

        #endregion Methods
    }

    public class VideoThumpnailGenerator
    {
        #region Constructors

        public VideoThumpnailGenerator(int contentID, Guid attachID, string FileName = "", int Width = 64, int Height = 64)
        {
            ContentManagement.GetContent CM = new ContentManagement.GetContent(id: contentID);

            nContent C = CM.getList().First();

            if(C!= null)
            {
                ContentAttachmentRepository Repo = new ContentAttachmentRepository();

                var att = Repo.GetByPKid(attachID);

                if (att.AttachmentType == "video")
                {

                    #region GLB_CONVERT_MOVIE_FFMEG_PATH
                    if (!RenConfigHelper.Settings<GlobalSettings>.Exists("GLB_CONVERT_MOVIE_FFMEG_PATH"))
                    {
                        RenConfigHelper.Settings<GlobalSettings>.Add(new nSetting()
                        {
                            Name = "GLB_CONVERT_MOVIE_FFMEG_PATH",
                            CategoryName = "CONTENT_SETTINGS",
                            DefaultValue = HttpContext.Current.Server.MapPath("~/Binaries/Converter/FFMPEG.exe"),
                            Description = new Ren.CMS.CORE.Language.LanguageDefaults.LanguageDefaultValues("LANG_GLB_CONVERT_MOVIE_FFMEG_PATH", "GLOBAL_SETTINGS"){
                                {"de-DE", "Geben Sie hier den kompletten physikalischen Pfad zur FFMPEG.exe an."},
                                {"en-US", "Enter here the complete physical path to FFMPEG.exe"}
                            }.ReturnLangLine(),
                            DescriptionLanguageLine = "LANG_GLB_CONVERT_MOVIE_FFMEG_PATH",
                            Label = new Ren.CMS.CORE.Language.LanguageDefaults.LanguageDefaultValues("LANG_GLB_CONVERT_MOVIE_FFMEG_PATH_LABEL", "GLOBAL_SETTINGS"){
                                {"de-DE", "Pfad zur FFMEG.exe"},
                                {"en-US", "Path to FFMEG.exe"}
                            }.ReturnLangLine(),
                            LabelLanguageLine = "LANG_GLB_CONVERT_MOVIE_FFMEG_PATH_LABEL",
                            PermissionBackend = "CAN_CONFIGURE_CONTENTTYPES",
                            PermissionFrontend = "CAN_CONFIGURE_CONTENTTYPES",
                            SettingRelation = "GLOBAL_SETTINGS",
                            SettingType = nSettingType.SettingString,
                            Value = HttpContext.Current.Server.MapPath("~/Binaries/Converter/FFMEG.exe"),
                            ValueType = nValueType.ValueString

                        });
                    }
                    #endregion

                    string ffmegPath = RenConfigHelper.Settings<GlobalSettings>.Get("GLB_CONVERT_MOVIE_FFMEG_PATH").Value.ToString();

                    if (!String.IsNullOrEmpty(ffmegPath))
                    {

                        FileManagement FMx = new FileManagement();
                        try
                        {
                            var f = FMx.getFile(att.FName);
                            string target = Guid.NewGuid() + ".jpg";
                            string sourceFile = "";
                            if (f.id > 0)
                            {
                                sourceFile = f.filepath;
                                sourceFile = HttpContext.Current.Server.MapPath(sourceFile);

                            }

                            else
                            {
                                //Maybe downloadable file
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.DownloadFile(att.FPath + "/" + att.FName, HttpContext.Current.Server.MapPath("~/Thumpnails/temp.down/" + target));
                                    sourceFile = HttpContext.Current.Server.MapPath("~/Thumpnails/temp.down/" + target);
                                }
                                catch
                                {
                                    //Maybe direct path

                                    sourceFile = HttpContext.Current.Server.MapPath(sourceFile);

                                }

                            }

                            //Try generate
                            //Check
                            string[] thmppath = {
                                                    "~/Binaries",
                                                    "~/Binaries/Converter",
                                                    "~/Binaries/Converter/Videothumpnails"
                                                    , "~/Binaries/Converter/Videothumpnails/"+ att.Pkid
                                                };

                            foreach(string d in thmppath)
                                if(!Directory.Exists(HttpContext.Current.Server.MapPath(d)))
                                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(d));

                            string WorkingDirectory = thmppath.Last();
                            WorkingDirectory = HttpContext.Current.Server.MapPath(WorkingDirectory);

                            string output = WorkingDirectory +"\thump.%03D.jpg";
                            string ffmpegParameter = "-i \"{0}\" -f image2 -vf fps=fps=10/600 \"{1}\"";
                            ffmpegParameter = String.Format(ffmpegParameter,
                                sourceFile,
                                output);
                            FMx.RunProcess(ffmegPath, ffmpegParameter);

                            //Images Created choose last:
                            var list = Directory.GetFiles(WorkingDirectory);
                            string selectedThumpnail = list.Last();
                            //Ok got thumpnail, lets copy:
                            System.IO.File.Copy(selectedThumpnail, HttpContext.Current.Server.MapPath(
                                "~/VideoImageData/"+ att.Pkid +".jpg"));

                            //Clear WOrking Directory
                            //foreach (string file in list)
                            //{

                            //    System.IO.File.Delete(file);
                            //}

                            string img = HttpContext.Current.Server.MapPath(
                                "~/VideoImageData/" + att.Pkid + ".jpg");

                            System.Drawing.Image thumpN = System.Drawing.Image.FromFile(img).GetThumbnailImage(Width, Height, () => false, IntPtr.Zero);

                            new ThumpnailRepository().Add(
                            new TBThumpnailsModule()
                            {
                                AtID = att.Pkid,
                                Height = Height,
                                Width = Width,

                            }

                                );

                            TBThumpnailsModule Thumpthump = new ThumpnailRepository().GetOne(NHibernate.Criterion.Expression.Where<TBThumpnailsModule>(e => e.AtID == attachID && e.Height == Height && e.Width == Width));

                            //Success we got it
                            string[] neededPaths = new string[]{
                            "~/Thumpnails",
                            "~/Thumpnails/Storage",
                            "~/Thumpnails/Storage/"+ Thumpthump.Id,
                            "~/Thumpnails/Storage/"+ Thumpthump.Id +"/" + Width +"x"+ Height

                        };

                            foreach (string p in neededPaths)
                            {
                                if (!Directory.Exists(HttpContext.Current.Server.MapPath(p)))
                                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(p));

                            };

                            thumpN.Save(
                                filename: HttpContext.Current.Server.MapPath(neededPaths.Last() + "/" + Thumpthump.AtID.ToString() + ".thump.jpeg"),
                                format: ImageFormat.Jpeg);
                            Thumpthump.Path =  HttpContext.Current.Server.MapPath(neededPaths.Last() + "/" + Thumpthump.AtID.ToString() + ".thump.jpeg");
                            Thumpthump.LastModification = new FileInfo( HttpContext.Current.Server.MapPath(neededPaths.Last() + "/" + Thumpthump.AtID.ToString() + ".thump.jpeg")).LastWriteTime;

                            new ThumpnailRepository().Update(Thumpthump);
                        }
                        catch (Exception)
                        { }
                    }

                }

            }
        }

        #endregion Constructors
    }
}