using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ren.CMS.CORE.FileManagement;
using Ren.CMS.Models;
using System.Drawing;
using System.IO; 
namespace Ren.CMS.Controllers
{
    public class UserAvatarController : Controller
    {
        //
        // GET: /FileManagement/

        public ActionResult Index(string name = "404.jpg")
        {
            //Accepting two types Image and 




            FileManagement.FilemanagementControllers CNT = new FileManagement.FilemanagementControllers("UserAvatar");
            if (!CNT.ControllerExists("UserAvatar"))
            {
                //Setting Up Defaults


                FileManagement.nFileProfiles Profile = new FileManagement.nFileProfiles("UserAvatar");
 
                

                FileManagement.nFileProfileManagement ProfMGM = new FileManagement.nFileProfileManagement();

                List<FileSettingModel> DefaultSettings = new List<FileSettingModel>();
                //Default Settings
                FileSettingModel USE_WATERMARK_IMAGE = new FileSettingModel();
                USE_WATERMARK_IMAGE.ID = 0;
                USE_WATERMARK_IMAGE.Name = "USE_WATERMARK_IMAGE";
                USE_WATERMARK_IMAGE.Value = "FALSE";


                FileSettingModel USE_WATERMARK_TEXT = new FileSettingModel();
                USE_WATERMARK_TEXT.ID = 0;
                USE_WATERMARK_TEXT.Name = "USE_WATERMARK_TEXT";
                USE_WATERMARK_TEXT.Value = "FALSE";


                FileSettingModel WATERMARK_IMAGE_PERC_SIZE = new FileSettingModel();
                WATERMARK_IMAGE_PERC_SIZE.ID = 0;
                WATERMARK_IMAGE_PERC_SIZE.Name = "WATERMARK_IMAGE_PERC_SIZE";
                WATERMARK_IMAGE_PERC_SIZE.Value = "0.19";


                FileSettingModel WATERMARK_IMAGE_MARGIN = new FileSettingModel();
                WATERMARK_IMAGE_MARGIN.ID = 0;
                WATERMARK_IMAGE_MARGIN.Name = "WATERMARK_IMAGE_MARGIN";
                WATERMARK_IMAGE_MARGIN.Value = "2";


                FileSettingModel WATERMARK_IMAGE_OPACITY = new FileSettingModel();
                WATERMARK_IMAGE_OPACITY.ID = 0;
                WATERMARK_IMAGE_OPACITY.Name = "WATERMARK_IMAGE_OPACITY";
                WATERMARK_IMAGE_OPACITY.Value = "0.60";



                FileSettingModel WATERMARK_IMAGE_LOCATION = new FileSettingModel();
                WATERMARK_IMAGE_LOCATION.ID = 0;
                WATERMARK_IMAGE_LOCATION.Name = "WATERMARK_IMAGE_LOCATION";
                WATERMARK_IMAGE_LOCATION.Value = "LEFT_BOTTOM";


                FileSettingModel WATERMARK_TEXT_FONTNAME = new FileSettingModel();
                WATERMARK_TEXT_FONTNAME.ID = 0;
                WATERMARK_TEXT_FONTNAME.Name = "WATERMARK_TEXT_FONTNAME";
                WATERMARK_TEXT_FONTNAME.Value = "Verdana";

                FileSettingModel WATERMARK_TEXT_LOCATION = new FileSettingModel();
                WATERMARK_TEXT_LOCATION.ID = 0;
                WATERMARK_TEXT_LOCATION.Name = "WATERMARK_TEXT_LOCATION";
                WATERMARK_TEXT_LOCATION.Value = "RIGHT_BOTTOM";


                FileSettingModel WATERMARK_TEXT_COLOR_RED = new FileSettingModel();
                WATERMARK_TEXT_COLOR_RED.ID = 0;
                WATERMARK_TEXT_COLOR_RED.Name = "WATERMARK_TEXT_COLOR_RED";
                WATERMARK_TEXT_COLOR_RED.Value = "255";


                FileSettingModel WATERMARK_TEXT_COLOR_GREEN = new FileSettingModel();
                WATERMARK_TEXT_COLOR_GREEN.ID = 0;
                WATERMARK_TEXT_COLOR_GREEN.Name = "WATERMARK_TEXT_COLOR_GREEN";
                WATERMARK_TEXT_COLOR_GREEN.Value = "255";


                FileSettingModel WATERMARK_TEXT_COLOR_BLUE = new FileSettingModel();
                WATERMARK_TEXT_COLOR_BLUE.ID = 0;
                WATERMARK_TEXT_COLOR_BLUE.Name = "WATERMARK_TEXT_COLOR_BLUE";
                WATERMARK_TEXT_COLOR_BLUE.Value = "255";

                FileSettingModel WATERMARK_TEXT_OPACITY = new FileSettingModel();
                WATERMARK_TEXT_OPACITY.ID = 0;
                WATERMARK_TEXT_OPACITY.Name = "WATERMARK_TEXT_OPACITY";
                WATERMARK_TEXT_OPACITY.Value = "55";

                FileSettingModel WATERMARK_TEXT_TEXT = new FileSettingModel();
                WATERMARK_TEXT_TEXT.ID = 0;
                WATERMARK_TEXT_TEXT.Name = "WATERMARK_TEXT_TEXT";
                WATERMARK_TEXT_TEXT.Value = "Seen@YouSite.info";

                //add to list
                DefaultSettings.Add(USE_WATERMARK_IMAGE);
                DefaultSettings.Add(USE_WATERMARK_TEXT);
                DefaultSettings.Add(WATERMARK_IMAGE_MARGIN);
                DefaultSettings.Add(WATERMARK_IMAGE_PERC_SIZE);
                DefaultSettings.Add(WATERMARK_IMAGE_OPACITY);
                DefaultSettings.Add(WATERMARK_IMAGE_LOCATION);
                DefaultSettings.Add(WATERMARK_TEXT_FONTNAME);
                DefaultSettings.Add(WATERMARK_TEXT_LOCATION);
                DefaultSettings.Add(WATERMARK_TEXT_COLOR_RED);
                DefaultSettings.Add(WATERMARK_TEXT_COLOR_GREEN);
                DefaultSettings.Add(WATERMARK_TEXT_COLOR_BLUE);
                DefaultSettings.Add(WATERMARK_TEXT_OPACITY);
                DefaultSettings.Add(WATERMARK_TEXT_TEXT); 


                if (Profile.ID < 1)
                {

                    ProfMGM.createProfile("UserAvatar", DefaultSettings);

                
                
                
                }

                

              int cid =  CNT.registerFilemanagementController("UserAvatar");
              CNT.addIfNotAcceptedProfile("UserAvatar", cid);
           

              CNT.addIfNotAcceptedMime("image/*", cid);

              CNT = new FileManagement.FilemanagementControllers("UserAvatar");
            
              
            }




            FileManagement FM = new FileManagement();
    

            nFile F = FM.getFile(name, true);



            if (String.IsNullOrEmpty(F.filepath) || String.IsNullOrEmpty(F.mimetype))

                F.ProfileID = -1;

            if (!CNT.FileIsAccepted(F))
            {
                if (F.mimetype.StartsWith("image") && System.IO.File.Exists(Server.MapPath("/Storage/Errors/replacement_default.jpg")))
                    return base.File(Server.MapPath("/Storage/Errors/replacement_default.jpg"), "image/jpeg");
                else if (F.mimetype.StartsWith("video") && System.IO.File.Exists(Server.MapPath("/Storage/Errors/replacement_default.flv")))
                    return base.File(Server.MapPath("/Storage/Errors/replacement_default.flv"), "video/x-flv");
                else
                    return Content("File-System Error");
            }

            if (F.mimetype.StartsWith("image") || F.mimetype.StartsWith("video"))
            {
                byte[] resp = new byte[0];
          

                if (System.IO.File.Exists(Server.MapPath(F.filepath)))
                {
                    //Later: WM Options
                    Image RAW = Image.FromFile(Server.MapPath(F.filepath));
                    //Load Watermark
                            MemoryStream MS2 = new MemoryStream();
                        RAW.Save(MS2, System.Drawing.Imaging.ImageFormat.Jpeg);

                        resp = MS2.ToArray();
                    FileManagement.nFileProfiles Prof = new FileManagement.nFileProfiles(F.ProfileID);

                    if (Prof.getProfileSetting("USE_WATERMARK_IMAGE").Value == "TRUE" || Prof.getProfileSetting("USE_WATERMARK_TEXT").Value == "TRUE")
                    {
                        if (System.IO.File.Exists(Server.MapPath("/Storage/Default/Watermark.png")))
                        {
                            Image WM = Image.FromFile(Server.MapPath("/Storage/Default/Watermark.png"));

                            double perc = 0.19;
                            double.TryParse(Prof.getProfileSetting("WATERMARK_IMAGE_PERC_SIZE").Value, out perc);
                            int margin = 2;
                            float opacity = 0.60f;
                            int.TryParse(Prof.getProfileSetting("WATERMARK_IMAGE_MARGIN").Value, out margin);
                            float.TryParse(Prof.getProfileSetting("WATERMARK_IMAGE_OPACITY").Value, out opacity);


                            WaterMark W = new WaterMark();
                            W.SetWaterMarkLocation(Prof.getProfileSetting("WATERMARK_IMAGE_LOCATION").Value);
                            W.SetWaterMarkPercentageSize(perc);
                            W.SetMargin(margin);
                            W.SetOpacity(opacity);
                            W.getOpacity();

                            MemoryStream MS = W.AddWaterMark(RAW, WM);
                            MS2 = MS;
                            resp = MS.ToArray();
                        }
                        if (Prof.getProfileSetting("USE_WATERMARK_TEXT").Value == "TRUE")
                        {


                            string fontname = Prof.getProfileSetting("WATERMARK_TEXT_FONTNAME").Value;
                            string location = Prof.getProfileSetting("WATERMARK_TEXT_LOCATION").Value;
                            string colorR = Prof.getProfileSetting("WATERMARK_TEXT_COLOR_RED").Value;
                            string colorG = Prof.getProfileSetting("WATERMARK_TEXT_COLOR_GREEN").Value;
                            string colorB = Prof.getProfileSetting("WATERMARK_TEXT_COLOR_BLUE").Value;
                            int red = 255;
                            int green = 255;
                            int blue = 255;
                            int.TryParse(colorR, out red);
                            int.TryParse(colorG, out green);
                            int.TryParse(colorB, out blue);
                            int opacityx = 78;
                            string stropacity = Prof.getProfileSetting("WATERMARK_TEXT_OPACITY").Value;
                            int.TryParse(stropacity, out opacityx);

                            string layerText = Prof.getProfileSetting("WATERMARK_TEXT_TEXT").Value;
                           WaterMarkText WTX = new WaterMarkText();
                           if (String.IsNullOrEmpty(layerText)) layerText = "This is a dummy text. Please change it in backend.";
                           WTX.SetText(layerText);
                           WTX.SetOpacity(opacityx);
                           WTX.SetWaterMarkLocation(location);
                           if(String.IsNullOrEmpty(fontname))WTX.SetFontName("verdana");
                           else WTX.SetFontName(fontname.ToLower());


                           WTX.SetColor(red,green,blue);


                           WTX.SetMargin(2);



                           WTX.AddWaterMarkText(Image.FromStream(MS2));
                           resp = MS2.ToArray();
                        }
                    
                    }else{

                      
                    }
                   

                }


                Response.SetDefaultImageHeaders();
               return base.File(resp, F.mimetype, F.aliasName);
            
            }

           //Empty Picture

            byte[] b = new byte[] { new byte() };
            return base.File(b, "image/png");
           
          
        }
       
    }
}
