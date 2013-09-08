namespace Ren.CMS.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Ren.CMS.CORE.FileManagement;
    using Ren.CMS.Models.SharedElements;
    using Ren.CMS.Persistence.Repositories;

    public class SharedElementsController : Controller
    {
        #region Methods

        //
        // GET: /SharedElements/
        public ActionResult JPlayer(string filePath, string Title, int width = 640, int height = 264, string poster = "")
        {
            jplayer Player = new jplayer()
            {
                ID = Guid.NewGuid().ToString(),
                FilePath = filePath,
                Title = Title,
                Width = width,
                Height = height,
                Poster = poster
            };
            if (String.IsNullOrEmpty(Player.Poster))
            {
                try
                {

                }
                catch
                {

                }

            }

            return PartialView("Jplayer/jplayerEmbed", Player);
        }

        #endregion Methods
    }
}