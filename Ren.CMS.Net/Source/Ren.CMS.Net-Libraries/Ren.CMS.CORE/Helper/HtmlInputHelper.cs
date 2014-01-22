using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.HtmlControls;
using HtmlAgilityPack;
using System.IO;


namespace Ren.CMS.CORE.Helper
{
    public static class HtmlInputHelper
    {

        private static int ElementCount = 0;


        private static MvcHtmlString ChangeNameAttribute(MvcHtmlString html)
        {

            HtmlDocument Doc = new HtmlDocument();
            Doc.LoadHtml(html.ToHtmlString());

            var element = Doc.DocumentNode.SelectSingleNode("//input");

            var hAttribute = element.Attributes.Where(e => e.Name == "name").First();
            var idAttribute = element.Attributes.Where(e => e.Name == "id").First();

            element.Attributes.Remove("name");

            element.Attributes.Remove("id");



            hAttribute = Doc.CreateAttribute("name", hAttribute.Value.Replace("[]", "") + "[]");
            idAttribute = Doc.CreateAttribute("id", idAttribute.Value + ElementCount);

            element.Attributes.Add(hAttribute);
            element.Attributes.Add(idAttribute);

            ElementCount = ElementCount + 1;

            Doc.DocumentNode.RemoveAllChildren();
            Doc.DocumentNode.ChildNodes.Add(element);
            string htmlstr = Doc.DocumentNode.OuterHtml;
            return new MvcHtmlString(htmlstr);

        }

        public static MvcHtmlString RadioButtonForAsArray<TModel,TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value)
        {

            var box = htmlHelper.RadioButtonFor(expression, value);

            return ChangeNameAttribute(box);
        
        }
    }
}
