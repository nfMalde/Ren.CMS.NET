using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ren.CMS.Content;
using Ren.CMS.CORE.Language;
using Ren.CMS.CORE.Language.LanguageDefaults;

namespace Ren.CMS.Models.HomeModels
{
    public class ContentBoxModel
    {
        private string headerTextLangLine = null;

        public LanguageDefaultValues LanguageDefaultText = null;
        public string CSSclass { get; set; }

        public IEnumerable<nContent> Contents { get; set; }

        public string ContentListAction { get; set; }
       
        public string ContentListController { get; set; }
        /// <summary>
        /// Language Package for the Header (Default: Root)
        /// </summary>
        public string LanguagePackage {get;set;}
        
        /// <summary>
        /// Language Line for the Header
        /// </summary>
        public string LanguageLine {get;set;}

        /// <summary>
        /// Header Text(READ ONLY)
        /// </summary>
        public string HeaderText
        {
            get 
            { 

                if(LanguageLine != null || LanguageDefaultText != null)
                {
                    if (LanguageDefaultText != null)
                    {
                        string Default = LanguageDefaultText.ReturnLangLine();
                        if(!String.IsNullOrEmpty(Default))
                        {
                            return Default;
                        
                        }
                    }
                    string Package = (LanguagePackage != null ? LanguagePackage : "Root");
                    Language Lang = new Language("__USER__", Package);

                    string Text =  Lang.getLine(LanguageLine);
                    if (String.IsNullOrEmpty(Text))
                        Text = LanguageLine;

                    return Text;
                }

                return "!HEADER_TEXT_MISSING!";
            }
        
        
        }
    }
}