using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using nfCMS_NET.CORE.Language;
using nfCMS_NET.CORE.Links;
namespace nfCMS_NET.Models.Core
{

    public class BackendShortcut
    {
        public string IconUrl { get; set; }
        public string ShortCutText { get; set; }
        
    
    }

    public class nSliderModel {

        public List<nfCMS_NET.Content.nContent> ContentCollection { get; set; }
        public int SliderSize { get; set; }
    

    
    
    
    }
    public class SocialSharing {
        private int _width = 0;
        private int _height = 0;

        public string[] AddThisButtons { get; set; }
        public int width {
            get {

                return this._width;

            
            }
            set {

                this._width = value;
            
            }
        }
        public int height
        {
            get
            {

                return this._height;


            }
            set
            {

                this._height = value;

            }
        }
        public string ContainerClassesCSS { get; set; }
        public string ContainerStyle { get; set; }

    }
    public class nContentIncludedModel {


        public nfCMS_NET.Content.nContent Entry
        {

            get;
            set;

        }
    
    
    
    }


    public class MenuModel
    {
       [Required]
        public LinkCollection Links
        {

            get;
            set;


        }



       public string getCorrectCSSClass(ControllerContext CTT, Link Lnk)
       {


            if (isLinkAcitve(CTT, Lnk))
            {

                return Lnk.HoverStateClass;

            }
            else {

                return Lnk.NormalStateClass;
            
            }
        
        
        }

        public bool isLinkAcitve(ControllerContext CTT, Link Lnk)
        {
           ControllerContext CT = CTT;
          
           string currentC = CT.RouteData.GetRequiredString("controller").ToLower();
           string currentA = CT.RouteData.GetRequiredString("action").ToLower();
           bool isok = false;             
           //Stripping Action
           if (currentC == Lnk.TargetController.ToLower()) isok = true;
           else isok = false;

           if (!isok) return isok;
           if (String.IsNullOrEmpty(Lnk.TargetAction) || String.IsNullOrWhiteSpace(Lnk.TargetAction) || Lnk.TargetAction == "*")
           {

               isok = true;

           }

           else {


               if (currentA == Lnk.TargetAction.ToLower())
               {


                   isok = true;


               }
               else {


                   isok = false;
               
               }
           
           
           }



    

           return isok;
       }
       
        
    }
   
 
}
