using Ren.CMS.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ren.CMS.Content
{
    public class ContentValidator
    {

        private bool multipleEmpty(string[] str)
        {
            foreach (string st in str)
            {

                if (String.IsNullOrEmpty(st)) return true;

                if (String.IsNullOrWhiteSpace(st)) return true;


            }

            return false;
        }

        /// <summary>
        /// Before Inserting, you can check all Required Field for valid values
        /// </summary>
        /// <param name="Postmodel">Posted Model</param>
        /// <returns>boolean</returns>
        public bool isValidPostModelForInsert(Ren.CMS.Models.Core.nContentPostModel Postmodel)
        {
            if (!Postmodel.Texts.Any(e => e.Active))
                return false;
            bool isEmpty = this.multipleEmpty(new string[] {    Postmodel.Texts.Where(e => e.Active).FirstOrDefault().Title, 
                                                                Postmodel.Texts.Where(e => e.Active).FirstOrDefault().PreviewText, 
                                                                Postmodel.Texts.Where(e => e.Active).FirstOrDefault().LongText, 
                                                                Postmodel.CreatorPKID, 
                                                                Postmodel.CategoryID, 
                                                                Postmodel.ContentType 
                                                            }

                                                            );



            if (isEmpty) return false;
            return true;
        }


        /// <summary>
        /// Converts a String to a valid URL CONFORM and SEO String for URLs and Filenames
        /// </summary>
        /// <param name="title">The String to Convert</param>
        /// <returns>Converted String</returns>
        public string makeTitleSEOConform(string title)
        {

            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                title = title.Replace(c, '_');
            }

            title = title.Replace(' ', '_');


            return title;
        }


    }
}