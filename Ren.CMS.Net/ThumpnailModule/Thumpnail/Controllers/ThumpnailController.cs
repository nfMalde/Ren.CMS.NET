namespace ThumpnailModule.Thumpnail.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using ThumpnailModule.Thumpnail.Domain;
    using ThumpnailModule.Thumpnail.Models;
    using ThumpnailModule.Thumpnail.Repositories;

    public class ThumpnailController : Controller
    {
        #region Fields

        private const string ThumpnailPath = "~/StorageThumpnails";

        #endregion Fields

        #region Methods

        //
        // GET: /Thumpnail/
        public ContentResult Index()
        {
            return Content("");
        }

        public FileResult _Get(int contentid, Guid PKID, string ext = "", string fileName = "default.png", int Width = 64, int Height = 64)
        {
            Thumpnail.Models.ThumpnailModel Model = new Models.ThumpnailModel(contentID: contentid,
                                                                            attachID: PKID,
                                                                            FileName: fileName,
                                                                            Width: Width,
                                                                            Height: Height);

            return File(Model.Entity.Path, "image/jpeg",fileName);
        }

        #endregion Methods
    }
}