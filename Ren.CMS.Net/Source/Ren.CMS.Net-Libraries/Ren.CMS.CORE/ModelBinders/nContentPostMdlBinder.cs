using Ren.CMS.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ren.CMS.CORE.ModelBinders
{
    public class nContentPostMdlBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
                          ModelBindingContext bindingContext)
        {


            HttpRequestBase request = controllerContext.HttpContext.Request;

            //Get Texts 

            List<nContentPostModelText> Texts = new List<nContentPostModelText>();

            string[] sActives = request.Form.GetValues("Active");
            List<bool> Actives = new List<bool>();
            foreach (string active in sActives)
            {
                bool bActive = false;
                //TODO: Find out why parsing is wrong for bool
                if (bool.TryParse(active, out bActive))
                {
                    Actives.Add(bActive);

                }


            }

            string[] LangCodes = request.Form.GetValues("LangCode");
            string[] LongTexts = request.Form.GetValues("LongText");
            string[] MetaDescriptions = request.Form.GetValues("MetaDescription");
            string[] MetaKeyWords = request.Form.GetValues("MetaKeyWords");
            string[] PreviewTexts = request.Form.GetValues("PreviewText");
            string[] SEONames = request.Form.GetValues("SEOName");
            string[] txIds = request.Form.GetValues("TextID");
            string[] Titles = request.Form.GetValues("Title");

            //TODO: Make check working
            if (!txIds.Any(e => e.ToCharArray().ToList().Any(c => char.IsNumber(c) == false)))
            {
                bindingContext.ModelState.AddModelError("TextID", new Exception("TextID is not a number: " + txIds.First(e => e.ToCharArray().ToList().Any(c => char.IsNumber(c)) == false)));
            }
            else
            {
                List<int> TextIDs = new List<int>();

                txIds.ToList().ForEach(f => TextIDs.Add(Convert.ToInt32(f)));

                //Put Texts to PostModel

                nContentPostModel Model = (nContentPostModel) bindingContext.Model;

                //Check Arrays

                List<IEnumerable<object>> Check = new List<IEnumerable<object>>();
                
                Check.Add(LangCodes);
                Check.Add(LongTexts);
                Check.Add(MetaDescriptions);
                Check.Add(MetaKeyWords);
                Check.Add(PreviewTexts);
                Check.Add(SEONames);
                Check.Add(txIds);

                Check.OrderBy(e => e.Count());

                int highest = Check.First().Count();
                int lowest = Check.Last().Count();

                if (highest != lowest || (highest != Actives.Count() || lowest != Actives.Count()))
                {

                    bindingContext.ModelState.AddModelError("_FORM_", new Exception("Invalid TextModel"));


                }

                else
                {

                    for (int i = 0; i < highest; i++)
                    {
                        Texts.Add(new nContentPostModelText()
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
                        });

                    }

                    Model.Texts = Texts;


                    if(Texts.Count() == 0 || !Texts.Any(e => e.Active))
                        bindingContext.ModelState.AddModelError("_FORM_", new Exception("At least one Text Model should be active"));
                    

                    List<ValidationResult> ValResults = new List<ValidationResult>();

                    Model.Texts.ToList().ForEach(t =>

                        Validator.TryValidateObject(t, new ValidationContext(t, serviceProvider: null, items: null),ValResults, true)
                        
                        );


                    if (ValResults.Count > 0)
                        ValResults.ForEach(v => bindingContext.ModelState.AddModelError("__FORM__", v.ErrorMessage));

                    bindingContext.Model = Model;
                }





            }


            return bindingContext.Model;

        }
    }
}
