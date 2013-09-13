
function decodeEntities(input) {
    var y = document.createElement('textarea');
    y.innerHTML = input;
    return y.value;
}

       try{

           console.log("Ren.CMS (Renate) Backend 0.2 Console launched");
           console.log("You will see debug information in the console now.");

       }
       catch (e)
       {
           eval("var console = { log: function (str) { } };");

       }
    
       $(document).ready(function () {
           var gui = new BackendGui();
           // alert(gui.checkPermission());
           if (!gui.isLoggedIn()) {

               if (gui.checkPermission()) {
                   //   alert("Load Desktop called");
                   gui.loadDesktop();
               
                  

               } else {

                   gui.loadLoginForm('Fehlende Rechte', 'Sie verfügen nicht über die benötigten Recht diesen Bereich zu sehen.');
                   //gui.showLoginError('Fehlende Rechte', 'Sie verfügen nicht über die benötigten Recht diesen Bereich zu sehen.');


               }
           }
           else {

               gui.loadLoginForm();


           }

           $('#systemmessage').modal({

               show: false


           });

       });

       function systemMessage(title, message, type)
       {
           var $sys = $('#systemmessage');
           var $systitle = $sys.find(".modal-title");
           var $syscontainer = $sys.find(".sys-msg-cont");
           var isError = (type == "error");
           if ($sys.is(":visible"))
           {

               $sys.modal('hide')


           }


           $systitle.attr("class", "");
           $syscontainer.attr("class", "");
           $syscontainer.attr("class", "sys-msg-cont");

           $systitle.addClass("modal-title");
           var clsT = "";
           var clsB = "";


           switch (type)
           {
               case 'error':
                   
                   clsT = "text-danger";
                   clsB = "alert alert-error";
                   break;

               case 'success':


                   clsT = "text-success";
                   clsB = "alert alert-success";
                   break;
               case 'warning':

                   clsT = "text-warning";
                   clsB = "alert alert-warning";
                   break;


               default :

                   clsT = "text-info";
                   clsB = "alert alert-info";
                   

           }



           $systitle.addClass(clsT);
           $syscontainer.addClass(clsB);

           $systitle.text(title);
           $syscontainer.html("<p></p>").text(message);



           $sys.modal("show");


       }

        function backenForm(e, options)
       {
            

            var backendHandler = (options.backendHandler ? options.backendHandler : false);
            var backendAction = (options.backendAction ? options.backendAction : false);
            var backendLanguage = (options.language ? options.language :
                                    (window.defaultLanguage ? window.defaultLanguage : false));
            var systemerrors = (options.systemerrors ? options.systemerrors : false);



            if (!backendLanguage)
            {

                console.error("BackendCore Formhelper requires option 'language' in ISO Format e.g. 'de-DE', 'en-US'");

                return;
            }


                   if (!systemerrors) systemerrors = false;
                   if (!$(e).is("form")) {
                       console.error("Element " + e + " is no form element!");
                       return false;
                   }
                   
                   var eform = $(e);
                   var url = "";
               

                  

                   if (!backendAction && !backendHandler) {

                       url = eform.attr("action");

                   }
                   else {

                       if (!backendAction) backendAction = "";

                       if (!backendHandler) return;

                       url = window.base_url + backendLanguage +"/BackendHandler/" + backendHandler + "/" + backendAction;
                   }
                   alert(url);
                   eform.attr("action", url);
                   eform.attr("method", "post");
                   eform.attr("data-generated", "backenhandlerAttached");

                   eform.submit(function (e) {
                       e.preventDefault();
                        
                       $(this).find("input[type='submit']").each(function () {

                           $(this).attr("disabled", "disabled");




                       });

                       var pd = $(this).find(":input");

                       if (options.beforeSubmit && typeof options.beforeSubmit == 'function')
                       {

                           options.beforeSubmit(pd);
                          

                       }


                       var data = {};

                       if (options.preRenderPostData && typeof options.preRenderPostData == 'function') {

                           data = new options.preRenderPostData(pd);


                       }
                       else {

                           data = pd.serialize();


                       }
                       $.post(url, data , function (d) {





                           if (options.callBack) {
                               if (typeof callback == "function") {


                                   new options.callback(d);

                               }
                           }

                           var t = (d.success == true ? 'success' : 'error');
                           var title = (d.success == true ? window.jsLang.Root.LANG_SHARED_SUCCESS : window.jsLang.Root.LANG_SHARED_ERROR);

                           new systemMessage(title, d.message, t);





                       }, 'json');
                       



                   });
                   



               }

       
