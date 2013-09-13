namespace Ren.CMS.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.Models;

    public class LinkController : Controller
    {
        #region Methods

        public ActionResult Forward(string id)
        {
            id = HttpUtility.UrlDecode(id);

             ForwardModel MDL = new ForwardModel();

            MDL.url = id;

            return View(MDL);
        }

        //
        // GET: /Link/
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UniqueLink(int id)
        {
            Ren.CMS.Content.ContentManagement.GetContent GCo = new Content.ContentManagement.GetContent(id);

            List<Ren.CMS.Content.nContent> COLIST = GCo.getList();
            if (COLIST.Count > 0)
            {
                Ren.CMS.Content.nContent Co = COLIST[0];
                Co.GenerateLink();

                string baseURL = new Ren.CMS.CORE.ThisApplication.ThisApplication().BaseUrl + "/" + Co.FullLink;
                return RedirectPermanent(baseURL);
            }

            else return RedirectToAction("Index", "Home");
        }

        #endregion Methods
    }
}