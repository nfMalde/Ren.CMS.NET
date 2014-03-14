using Mvc.JQuery.Datatables;
using Ren.CMS.CORE.DataTables.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace Ren.CMS.CORE.Helper.DataTables
{

   

    public static class DataTable
    {
        //Generate Script for  DataTable
        public static MvcHtmlString JqeryDataTable<TController, TResult>(this HtmlHelper html, string id, Expression<Func<TController, DataTablesResult<TResult>>> exp)
        {
            var vm = html.DataTableVm<TController,TResult>(id,exp);
            vm = GetColDef<TResult>(vm);

            return JqueryDataTable(html, vm);
        }

        public static MvcHtmlString JqueryDataTable(this HtmlHelper html, DataTableConfigVm vm)
        {
            return html.Partial("~/Views/Backend/Shared/DataTable.cshtml", vm);
        }

       public static MvcHtmlString JqueryDataTable<TResult>(this HtmlHelper html, DataTableConfigVm vm)
       {
           vm = GetColDef<TResult>(vm);

          return JqueryDataTable(html, vm);
       }

       private static DataTableConfigVm GetColDef<TResult>(DataTableConfigVm vm)
       {
           var props = typeof(TResult).GetProperties().ToList();
           List<ColDef> defs = new List<ColDef>();

           foreach(var prop in props)
           {
               bool visible = true;
               bool sortable = true;
               SortDirection dir = SortDirection.None;
               string renderfunc = null;
               string p = prop.Name;
              var attr =  prop.GetCustomAttributes(typeof(RenDataTablesAttribute),false);
              var attr2 =  prop.GetCustomAttributes(typeof(DataTablesAttribute),false);
              if (attr2.Count() > 0)
              {
                  var dt = (DataTablesAttribute)attr2.First();
                  visible = dt.Visible;
                  sortable = dt.Sortable;
                  dir = dt.SortDirection;
                  renderfunc = dt.MRenderFunction;

              }


              if(attr.Count() > 0)
              {
               
              


                 var attributeRen =  (RenDataTablesAttribute) attr.First();
                 p = attributeRen.DisplayName;


              }


              defs.Add(
                  ColDef.Create(
                  prop.Name,
                  p,
                  prop.GetType(),
                  visible,
                  sortable,
                  dir,
                  renderfunc)
                  );



           }

           DataTableConfigVm nvm = new DataTableConfigVm(vm.Id,vm.AjaxUrl,defs);
           nvm.AutoWidth = vm.AutoWidth;
           nvm.ColumnFilter = vm.ColumnFilter;
           nvm.ColumnFilterVm = vm.ColumnFilterVm;
           nvm.DrawCallback = vm.DrawCallback;
           nvm.HideHeaders = vm.HideHeaders;
           nvm.JsOptions.Clear();
           foreach(var o in vm.JsOptions)
            nvm.JsOptions.Add(o);
           nvm.Language = vm.Language;
           nvm.ShowPageSizes = vm.ShowPageSizes;
           nvm.ShowSearch = vm.ShowSearch;
           nvm.StateSave = vm.StateSave;
           nvm.TableClass = vm.TableClass;
           nvm.TableTools = vm.TableTools;
           

           return nvm;

       }
    }

}
