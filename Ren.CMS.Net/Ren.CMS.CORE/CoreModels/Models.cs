﻿using Ren.CMS.CORE.Links;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ren.CMS.Models.Core
{
    public class SendMailModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public int MailID { get; set; }
        public string Receiptient { get; set; }



    }


    public class nContentPostModelText
    {

        public bool Active { get; set; }
        public int TextID { get; set; }
        public string LangCode { get; set; }


        /// <summary>
        /// Returns the Title of the Content
        /// </summary>
        /// 
        [Required]
        public string Title
        {

            get;
            set;

        }

        public string SEOName { get; set; }


        /// <summary>
        /// Returns the COMMA SEPERATED Meta-Keywords
        /// </summary>
        /// 

        public string MetaKeyWords
        {

            get;
            set;


        }
        /// <summary>
        /// Returns the MetaDescription
        /// </summary>
        /// 
        [MaxLength(250)]

        public string MetaDescription
        {

            get;
            set;


        }
        /// <summary>
        /// Returns the PreviewText
        /// </summary>
        /// 
        [Required]

        [MaxLength(400)]
        public string PreviewText
        {

            get;
            set;


        }
        /// <summary>
        /// Returns the Content Long Text
        /// </summary>
        ///
        [Required]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string LongText
        {

            get;
            set;


        }
    
    }

    public class nContentTextBinder
    {
        public List<string> Title { get; set; }
        public List<string> PreviewText { get; set; }
        public List<string> LongText { get; set; }
        public List<string> SEOName { get; set; }
        public List<string> MetaDescription { get; set; }
        public List<string> MetaKeyWords { get; set; }
        public List<int> TextID { get; set; }
        public List<bool> Active { get; set; }
        public List<string> LangCode { get; set; }

        private bool IsN()
        {

            if (Title == null) return true;
            if (PreviewText == null) return true;
            if (LongText == null) return true;
            if (SEOName == null) return true;
            if (MetaDescription == null) return true;
            if (MetaKeyWords == null) return true;
            if (TextID == null) return true;
            if (Active == null) return true;
            if (LangCode == null) return true;

            return false;
        }
        public List<nContentPostModelText> Bind()
        {
            List<nContentPostModelText> B = new List<nContentPostModelText>();
            if (IsN()) return B;
            for (int x = 0; x < Active.Count; x++)
            {
                nContentPostModelText Bb = new nContentPostModelText();
                Bb.Active = Active[x];

                if (Title.Count > x)
                {
                    Bb.Title = Title[x];
                
                }
                if (PreviewText.Count > x)
                {
                    Bb.PreviewText = PreviewText[x];

                }
                if (LongText.Count > x)
                {
                    Bb.LongText = LongText[x];

                }
                if (SEOName.Count > x)
                {
                    Bb.SEOName = SEOName[x];

                }
                if (MetaDescription.Count > x)
                {
                    Bb.MetaDescription = MetaDescription[x];

                }
                if (MetaKeyWords.Count > x)
                {
                    Bb.MetaKeyWords = MetaKeyWords[x];

                }
                if (TextID.Count > x)
                {
                    Bb.TextID = TextID[x];

                }
                if (LangCode.Count > x)
                {
                    Bb.LangCode = LangCode[x];

                }

                B.Add(Bb);
            
            }


            return B;
        
        
        }
    
    }


    public class nContentPostModel
    {

        public int ReferenceID
        {
            get;
            set;


        }

        //Eigenschaften
        /// <summary>
        /// Returns the ID of the Content
        /// </summary>
        /// 
        public int ID
        {
            get;
            set;
        }

        public IEnumerable<nContentPostModelText> Texts { get; set; }

        /// <summary>
        /// Returns the Content Type of the Content
        /// </summary>
        /// 
        [Required]
        public string ContentType
        {

            get;
            set;

        }
        /// <summary>
        /// Returns the Category object
        /// </summary>
        /// 
        [Required]
        public string CategoryID
        {
            get;
            set;

        }



        /// <summary>
        /// Returns the Creators PK ID
        /// </summary>
        ///
        [Required]
        public string CreatorPKID
        {


            get;
            set;


        }

        /// <summary>
        /// Returns the Username of the Creator
        /// </summary>
        public string CreatorName
        {

            get;
            set;


        }




        public DateTime CreationDate
        {

            get;
            set;

        }

        public string CreatorSpecialName { get; set; }
        /// <summary>
        /// Returns Boolean of the Locked Status of the Content
        /// </summary>
        ///  

        public bool Locked
        {

            get;
            set;

        }



        public int[] Tags { get; set; }

    }

    public class FlexyGridPostParameters
    {
        [Required]
        public int page { get; set; }
        [Required]
        public int rp { get; set; }
        [Required]
        public string sortname { get; set; }
        [Required]
        public string sortorder { get; set; }

        public string query { get; set; }

        public string qtype { get; set; }


    }


    public class BackendShortcut
    {
        [Required]
        public int id { get; set; }

        public string IconUrl { get; set; }

        public string ShortCutText { get; set; }

        [Required]
        public string PosX { get; set; }
        [Required]
        public string PosY { get; set; }

        public string action { get; set; }

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
            else
            {

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

            else
            {


                if (currentA == Lnk.TargetAction.ToLower())
                {


                    isok = true;


                }
                else
                {


                    isok = false;

                }


            }





            return isok;
        }


    }
}
