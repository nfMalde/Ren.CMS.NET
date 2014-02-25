using Ren.CMS.Models.Core;
using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ren.CMS.CORE.Helper;
using Ren.CMS.CORE.Helper.FormEncoding;
using Foolproof;
using System.ComponentModel.DataAnnotations;

namespace Ren.CMS.CORE.ModelBinders
{
    public class nContentPostMdlBinder : IModelBinder
    {
           


        public object BindModel(ControllerContext controllerContext,
                          ModelBindingContext bindingContext)
        {
            

            HttpRequestBase request = controllerContext.HttpContext.Request;
            List<nContentPostModelText> Texts = new List<nContentPostModelText>();

            int ID = 0;
            var ids = request.FormGatherer().GetValues<int>("ID");
            if (ids.Length > 0)
                ID = ids.First();
            //Get Texts 
            bool[] Actives = request.FormGatherer().GetValues<bool>("Active");
            int[] TextIDs = request.FormGatherer().GetValues<int>("TextID");
           

            string[] LangCodes = request.Form.GetValues("LangCode");
            string[] LongTexts = request.Form.GetValues("LongText");
            string[] MetaDescriptions = request.Form.GetValues("MetaDescription");
            string[] MetaKeyWords = request.Form.GetValues("MetaKeyWords");
            string[] PreviewTexts = request.Form.GetValues("PreviewText");
            string[] SEONames = request.Form.GetValues("SEOName");
    
            string[] Titles = request.Form.GetValues("Title");
          
            
             

            var refs = request.FormGatherer().GetValues<int>("ReferenceID");
            int refid = 0;
            if(refs != null && refs.Count() > 0)
                refid = refs.FirstOrDefault();

                //Put Texts to PostModel
                string creatorPKID =  request.Form.Get("CreatorPKID");

                if(String.IsNullOrEmpty(creatorPKID) || String.IsNullOrWhiteSpace(creatorPKID))
                {
                    creatorPKID = new Ren.CMS.MemberShip.nProvider.CurrentUser().nUser.ProviderUserKey.ToString();
                }

                nContentPostModel Model = new nContentPostModel()
                {
                    ID = ID,
                    CategoryID = request.Form.Get("CategoryID"),
                    ContentType = request.Form.Get("ContentType"),
                    CreationDate = request.FormGatherer().GetValues<DateTime>("CreationDate").First(),
                    CreatorName = request.Form.Get("CreatorName"),
                    CreatorPKID = creatorPKID,
                    CreatorSpecialName = request.Form.Get("CreatorSpecialName"),
                    Locked = request.FormGatherer().GetValues<bool>("Locked").First(),
                    ReferenceID = refid,
                    Tags = request.FormGatherer().GetValues<int>("Tags")
                };

                //Check Arrays

                List<IEnumerable<object>> Check = new List<IEnumerable<object>>();
                
                Check.Add(LangCodes);
                Check.Add(LongTexts);
                Check.Add(MetaDescriptions);
                Check.Add(MetaKeyWords);
                Check.Add(PreviewTexts);
                Check.Add(SEONames);
              

                Check.OrderBy(e => e.Count());

                int highest = Check.First().Count();
                int lowest = Check.Last().Count();

                if (highest != lowest || (highest != Actives.Count() || lowest != Actives.Count()) ||  (highest != TextIDs.Count() || lowest != TextIDs.Count()))
                {

                    bindingContext.ModelState.AddModelError("_FORM_", new Exception("Invalid TextModel"));


                }

                else
                {

                    for (int i = 0; i < highest; i++)
                    {
                        if (Actives[i])
                        {
                            var __t = new nContentPostModelText()
                            {
                                Active = Actives[i],
                                LangCode = LangCodes[i],
                                LongText = LongTexts[i],
                                MetaDescription = MetaDescriptions[i],
                                MetaKeyWords = MetaKeyWords[i],
                                PreviewText = PreviewTexts[i],
                                SEOName = PreviewTexts[i],
                                TextID = TextIDs[i],
                                Title = Titles[i]
                            };

                           

                            Texts.Add(__t.DecodeStringsURLandHTML(false));
                        }
                    }

                    Model.Texts = Texts;
                    Model = Model.DecodeStringsURLandHTML(false);

                    if(Texts.Count() == 0 || !Texts.Any(e => e.Active))
                        bindingContext.ModelState.AddModelError("_FORM_", new Exception("At least one Text Model should be active"));
                    

                    List<ValidationResult> ValResults = new List<ValidationResult>();

                    Model.Texts.ToList().ForEach(t =>
                    {
                        if (t.Active == true)
                        {
                            Validator.TryValidateObject(t, new ValidationContext(t, serviceProvider: null, items: null), ValResults, true);

                        }
                    }
                   );


                 

                    Validator.TryValidateObject(Model, new ValidationContext(Model, serviceProvider: null, items: null), ValResults, true);

                    if (ValResults.Count > 0)
                        ValResults.ForEach(v => bindingContext.ModelState.AddModelError(v.MemberNames.FirstOrDefault(), v.ErrorMessage));

                    bindingContext.ModelMetadata.Model = Model;
                }





            


            return bindingContext.Model;

        }
    }
}
