
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

       function systemMessage(title, message, type, errors)
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
           var li = $syscontainer.html("<ul></ul>");
           if (errors) {
               li.addClass("text-danger");
               for (var i = 0; i < errors.length; i++)
                   li.append("<li></li>").text(errors[i].Value);


           }



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
                
                   eform.attr("action", url);
                   eform.attr("method", "post");
                   eform.attr("data-generated", "backenhandlerAttached");
                   eform.find(":input").each(function () {

                       if ($(this).attr("type") && $(this).attr("type").toString().toLowerCase() == "checkbox")
                       {

                           if ($(this).next().attr("type") &&
                               $(this).next().attr("type").toString().toLowerCase() == "hidden"
                               && $(this).next().attr("name") && $(this).next().attr("name") == $(this).attr("name")) {
                               var n = $(this).next();

                               if (!$(this).is(":checked")) {
                                   $(n).removeAttr("disabled");

                               }
                               else {

                                   $(n).attr("disabled", "disabled");
                               }

                               $(this).change(function () {

                                   if (!$(this).is(":checked")) {
                                       $(n).removeAttr("disabled");

                                   }
                                   else {

                                       $(n).attr("disabled", "disabled");
                                   }

                               });

                           }


                       }

                   });


                   eform.submit(function (e) {
                       e.preventDefault();
                        
                       $(this).find("input[type='submit']").each(function () {

                           $(this).attr("disabled", "disabled");




                       });

                       var pd = $(this).find(":input");
                       var filteredInputs = new Array();
                       var form = this;
                     /*
                       pd = pd.filter(function (i) {

                           if ($(this).attr("type") && $(this).attr("type").toString().toLowerCase() == "hidden") {

                               var c = $(form).find(":input[type='checkbox'][name='" + $(this).attr("name") + "']");
                               var check = null;
                               for (var x = 0; x < c.length; x++)
                               {
                                   if ($(c[x]).next() == $(this))
                                       check = $(c[x]);


                               }

                               if (check != null) {

                                   if ($(check).is(":checked") == true) {
                                       return false;
                                   }

                               }

                           }

                           return true;

                       });

                       */
                       //MVC adds hiddenfields to checkboxes we have to remove if checked....


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
                           var addErrors = function DisplayErrors(errors) {
                               for (var i = 0; i < errors.length; i++) {
                                   $("<label for='" + errors[i].Key + "' class='error'></label>")
                                   .html(errors[i].Value[0]).appendTo($(eform).find("input#" + errors[i].Key).parent());
                               }
                           };

                           if (d.errors && d.success == false)
                               addErrors(d.errors);



                           var t = (d.success == true ? 'success' : 'error');
                           var title = (d.success == true ? window.jsLang.Root.LANG_SHARED_SUCCESS : window.jsLang.Root.LANG_SHARED_ERROR);

                           new systemMessage(title, d.message, t);





                       }, 'json');
                       



                   });
                   



               }

       
