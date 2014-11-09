namespace GalleryModule.Gallery.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using GalleryModule.Gallery.Models;

    using Ren.CMS.Content;

    public class GalleryController : Controller
    {
        #region Methods

        [HttpGet]
        public ActionResult Gallery(string reference, int id, string type, int page)
        {
            GalleryView GalleryView = new GalleryView(reference, id, type, page);
            if (!GalleryView.IsValid())
                return GalleryView.RedirectAction();

            return View(GalleryView);
        }

        [HttpPost]
        public JsonResult GetGalleryNavigation(GalleryNavigation Model)
        {
            if (ModelState.IsValid)
            {
                ContentManagement.GetContent NavGet = new ContentManagement.GetContent(id: Model.ContentID);

                var contentList = NavGet.getList();

                if (contentList.Count() > 0)
                {
                    var attachments = contentList.First().Attachments.GetAttachments(null, "gallery");

                    if (attachments.Count > 0)
                    {

                        return Json(new { success = true, items = attachments });

                    }

                }
            }

            return Json(new { success = false, items = new List<nContentAttachment>() });
        }

        //
        // GET: /Gallery/
        public ActionResult Index()
        {
            return View();
        }

        #endregion Methods
    }
}