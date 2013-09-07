using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ren.CMS.Models.FlexiGrid;
using System.Web.Mvc;
namespace Ren.CMS.Helpers
{
    public static class FlexiGridHelper
    {
        public static IHtmlString GenerateFlexiGrid(this HtmlHelper helper, FlexiSettings GridSetup)
        {
            if (GridSetup.SortName == null)
            {
                if (GridSetup.ColumnModel[0] != null)
                    GridSetup.SortName = GridSetup.ColumnModel[0].Name;
                else
                    GridSetup.SortName = "";
 
            }
            if (GridSetup.SortOrder == null)
            {
                GridSetup.SortOrder = "DESC";

            
            }
            string jsCode = "$(function() { " +
                                "$('#" + GridSetup.ElementID + "').flexigrid({ " +
                                "url: '" + (GridSetup.Url) + "', " +
                                "dataType: '" + HttpUtility.HtmlDecode(GridSetup.DataType) + "', "+
                                "colModel: [ ";


            //Generating ColModels 

            foreach (FlexiColModel Model in GridSetup.ColumnModel)
            {
                if (String.IsNullOrEmpty(Model.Name) && String.IsNullOrEmpty(Model.Display))
                    continue;

                if (String.IsNullOrEmpty(Model.Name))
                    continue;

                if (String.IsNullOrEmpty(Model.Display))
                    Model.Display = Model.Name;

                jsCode += "{  display: '" + HttpUtility.HtmlDecode(Model.Display) + "', " +
                        "   name: '" + Model.Name + "', " +
                        "   width: " + (Model.Width > 0 ? Model.Width.ToString() : "'auto'") + ", " +
                        "   height: " + (Model.Height > 0 ? Model.Height.ToString() : "'auto'") + ", " +
                        "   sortable: " + Model.Sortable.ToString().ToLower() + ", " +
                        "   align: '" + Model.Align + "'" +
                        "  },";

            }

            if (jsCode.EndsWith(","))
                jsCode = jsCode.Remove(jsCode.LastIndexOf(','));


            jsCode += "], ";
            if (GridSetup.Buttons.Count > 0)
            {
                jsCode += "buttons: [ ";

                foreach (FlexiButtons Btn in GridSetup.Buttons)
                {
                    if(String.IsNullOrEmpty(Btn.Name))
                        continue;

                    jsCode += "{  " +
                            "   name: '" + HttpUtility.HtmlDecode(Btn.Name) + "', " +
                            "   bclass: '" + Btn.BGclass + "', " +
                            "   onpress: " + Btn.jsOnPress + ", " +
                            "  },";
                }

                if (jsCode.EndsWith(","))
                    jsCode = jsCode.Remove(jsCode.LastIndexOf(','));

                jsCode += "], ";

            }

            jsCode += "sortname: '" + GridSetup.SortName.ToString() + "', "+
                      "sortorder: '"+ (GridSetup.SortOrder.ToUpper() == "DESC" ||
                                            GridSetup.SortOrder.ToUpper() == "ASC" ? 
                                            GridSetup.SortOrder.ToUpper() : "ASC") +"' ,";

            jsCode += "usepager: "+ GridSetup.UsePager.ToString().ToLower() +", ";
            if (!String.IsNullOrEmpty(GridSetup.Title))
                jsCode += "title: '" + GridSetup.Title + "', ";
            int dummy = 0;
            jsCode += "width: " + (GridSetup.Width != null ?
                                    (int.TryParse(GridSetup.Width, out dummy) ? GridSetup.Width : "'" + GridSetup.Width + "'")
                                    : "'100%'") +", ";
            jsCode += "height: " + (GridSetup.Height != null ?
                                    (int.TryParse(GridSetup.Height, out dummy) ? GridSetup.Height : "'" + GridSetup.Height + "'")
                                    : "'auto'") + ", ";
            jsCode += "singleSelect: " + GridSetup.SingleSelect.ToString().ToLower();

            jsCode += "});";
            jsCode += "});";


            string html = "<table id=\"" + GridSetup.ElementID + "\">" +
                            "</table>" +
                            "<script type=\"text/javascript\">" +
                            jsCode
                            + "</script>";

            //Okay done! Lets return the HTML

            return new HtmlString(html);

         }
    }
}