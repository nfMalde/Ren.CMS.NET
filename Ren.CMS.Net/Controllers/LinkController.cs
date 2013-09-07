using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nfCMS_NET.Models;
namespace nfCMS_NET.Controllers
{
    public class LinkController : Controller
    {
        //
        // GET: /Link/

        public ActionResult Index()
        {

            return RedirectToAction("Index", "Home");
        }



        public ActionResult Forward(string id) {

             id = HttpUtility.UrlDecode(id);

             ForwardModel MDL = new ForwardModel();

            MDL.url = id;







            return View(MDL);
        }



        public ActionResult UniqueLink(int id) {

            nfCMS_NET.Content.ContentManagement.GetContent GCo = new Content.ContentManagement.GetContent(id);

            List<nfCMS_NET.Content.nContent> COLIST = GCo.getList();
            if (COLIST.Count > 0)
            {
                nfCMS_NET.Content.nContent Co = COLIST[0];
                Co.GenerateLink();

                string baseURL = new nfCMS_NET.CORE.ThisApplication.ThisApplication().BaseUrl + "/" + Co.FullLink;
                return RedirectPermanent(baseURL);
            }

            else return RedirectToAction("Index", "Home");
        
        
        
        }

    }
}
