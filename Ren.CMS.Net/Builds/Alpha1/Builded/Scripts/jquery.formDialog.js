//Jquery Plugin for dialog Form
//Made by Malte Peters @ www.networkfreaks.de

//License: Open Source
//Please feel free to share, but this message must stay in.
//This Plugin is Free!


(function ($) {



    $.fn.formDialog = function (config,elName,value) {
        var callBacks_ = [];
        var elements_ = [

            { name: 'textbox', template: '<input type=\"text\" name=\"$1\" value=\"$2\" id=\"$3\" />' },
            { name: 'richtextbox', template: '<textarea style=\"width:100%; height:50px\"></textarea>' },

            { name: 'hidden', template: '<input type=\"hidden\" name=\"$1\" value=\"$2\" id=\"$3\" />' },
            { name: 'password', template: '<input type=\"password\" name=\"$1\" value=\"$2\" id=\"$3\" />' },
            { name: 'combobox', template: '<select style=\"width: 165px;\" name=\"$1\" id=\"$3\"> </select>', valueTemplate: '<option  value=\"$1\">$2</option>', marker: 'selected=\"selected\"' },
            {
                name: 'checkbox', template: '<input type=\"checkbox\" name=\"$1\" value=\"$2\" id=\"$3\" />', marker: 'checked=\"checked\"'
            },
            {
                name: 'radiobutton', template: '<div><div></div></div>', callBack: function (ElementConfig) {

                    
                    if (!ElementConfig.id) ElementConfig.id = ElementConfig.name;
                    if (!ElementConfig.name) console.log("ERROR! Form element Radio Buttonset has no name");

 //                   $(document.getElementById(ElementConfig.id)).buttonset();
 



                }, renderer: function (DOMElement, ElementConfig) {
                    var elStore = ElementConfig.dataStore;
                    if (elStore)
                    {
                        for (var x = 0; x < elStore.length; x++)
                        {
                            if (!ElementConfig.id) ElementConfig.id = ElementConfig.name;
                            if (!ElementConfig.name) console.log("ERROR! Form element Radio Buttonset has no name");
                            var checked = "";

                            if (ElementConfig.value == elStore[x].value)
                            {
                                checked = " checked=\"checked\" ";
                            }
                            $(DOMElement).find("div").append('<input value="'+ elStore[x].value+'" '+checked+' type="radio" id="' + ElementConfig.id + '_' + x + '" name="' + ElementConfig.name + '" /><label for="' + ElementConfig.id + '_' + x + '">'+ elStore[x].label+'</label>');
                          

                        }
                        $(DOMElement).trigger("create");
                        
                        return DOMElement;
                    }

                }
            }

        ];

        var elements = [];
        if (typeof config == 'string') {
            //Open Handler
            

            if (config == "open" || config == "show" || config == "hide" || config == "close" || config == "destroy")
                $(this).dialog();
            else {
               
                if (elName)
                {
                    var dia = this;
                    $(this).find("form :input").each(function () {
                        console.log(this);
                   
                            if (( elName == $(this).attr("name") || elName == $(this).attr("id"))) {
                                switch (config) {
                                    case 'open':
                                        $(dia).dialog("open");
                                        break;
                                    case 'close':
                                        $(dia).dialog("close");
                                        break;
                                    case 'hide':
                                        $(dia).dialog("hide");
                                        break;

                                    case 'getValue':
                                         return $(this).val();
                                        break;
                                    case 'setValue':
                                        var el = this;
                                        if ($(el).attr("type") == "radio" || $(el).attr("type") == "checkbox") {

                                            $(dia).find("input[name='" + elName + "']").each(function () {

                                                if ($(this).val() == value) $(this).attr("checked", "checked");

                                            });
                                        }
                                        else {

                                            $(this).val(value);
                                        }
                                        break;
                                }
                                 return false;

                            }
                        

                    });
                }

            }
        }
        else {
            if (config.customTemplates)
            {
                if (typeof config.customElTemplates == "array")
                {

                    for (var ct = 0; ct < config.customElTemplates.length; ct++)
                    {

                        elements_.push(config.customElTemplates[ct]);

                    }

                }


            }
            var htmlTemplate = $.parseHTML("<form><div class=\"returns\"></div><table class=\"formDialog\"></table></form>");

            if (config.elements) {

                for (var x = 0; x < config.elements.length; x++) {

                    var thisElement = config.elements[x];


                    for (var y = 0; y < elements_.length; y++) {

                        if (elements_[y].name == thisElement.type) {

                            if (elements_[y].callBack)
                            {

                                callBacks_.push({ cb: elements_[y].callBack, CFG: thisElement });

                            }

                            //Element Template found

                            var tpl = (thisElement.customTemplate ? thisElement.customTemplate : elements_[y].template);
                            tpl = $.parseHTML(tpl);
                            if (elements_[y].renderer)
                            {

                                tpl = elements_[y].renderer(tpl, thisElement);

                            }
                            var name = thisElement.name;

                            var value = thisElement.value;

                            var store = thisElement.dataStore; //value, label
                            if (!store) store = thisElement.store;
                            var innerStr = "";

                            if (elements_[y].valueTemplate) {
                                for (var s = 0; s < store.length; s++) {
                                    var htmlVtpl = $.parseHTML(elements_[y].valueTemplate);
                                    htmlVtpl = $(htmlVtpl);
                                    htmlVtpl.attr("value", store[s].value);
                                    htmlVtpl.text(store[s].label);


                                    $(tpl).append(htmlVtpl);

                                    //Create new row



                                }
                            }

                            
                            

                            var tr = "<tr></tr>";
                            tr = $.parseHTML(tr);
                            tr = $(tr);

                            var td1 = "<td></td>";
                            td1 = $.parseHTML(td1);
                            td1 = $(td1);
                            var td2 = "<td></td>";
                            td2 = $.parseHTML(td2);
                            td2 = $(td2);

                            if (thisElement.required == true) thisElement.label+= " <em>*</em>";
                            td1.html(thisElement.label);
                            td1.find("em").css({

                            "color": "red",
                            "font-weight": "bold",
                            "text-align": "right",
                            "font-size" : "10px"

                            });
                            $(tr).append(td1);
                            $(tpl).attr("name", name);
                            $(tpl).val(value);
                            $(tpl).attr("id", (thisElement.id ? thisElement.id : thisElement.name));

                            if (thisElement.labelBefore)
                                $(tpl).before(thisElement.before)

                            if (thisElement.checked) {

                                $(tpl).attr("checked", "checked");
                            }
                            if (!thisElement.required) thisElement.required = false;

                            //Adding Jquery Validator
                            if (thisElement.validators) {

                                var validators = thisElement.validators;

                                for (var i = 0; i < validators.length; i++)
                                {
                                    $(tpl).addClass(validators[i]);

                                }
                            }
                            if (thisElement.required == true) {
                                $(tpl).addClass("required");
                            }
                            elements.push({ id: thisElement.name, element: thisElement, required: thisElement.required });
                            if (thisElement.customRenderer)
                            {
                                //Parameters: dom / thisElement
                                tpl = thisElement.customRenderer(tpl, thisElement);

                            }
                            td2.html(tpl);
                            console.log("inserting first td->Label");
                            console.log(td1);
                            $(tr).append(td1);

                            console.log("inserting second td->Input");
                            console.log(td2);

                            $(tr).append(td2);


                            $(htmlTemplate).find("table").append(tr);
                            

                            console.log("Adding Row");
                            console.log(tr);
                            console.log("New Table Content");

                            console.log(htmlTemplate);
                        }

                    }
                }


            }

            //TPL is ready now lets generate the div
            var dautoOpen = false;
            var dwidth = 400;
            var dheight = 400;
            var dtitle = "";
            var saveText = "Save";
            var abortText = "Abort";
            var targetUrl = "";
            var targetMethod = "";
            var modal = false;
            if (config.modal != false) modal = config.modal;
            //Actions
            var self = this;
            this.onSuccess = function (data) {
                if (data.success == true) {

                    $(self).dialog("close");

                }
                else {

                    if (!data.message)
                        data.message = ("<p>Error encountered in ajax request.</p>");


                    if (data.messages) {
                        $(self).find("form").find("div[class='returns']").html("<div class=\"ui-widget\"><div class=\"ui-state-error ui-corner-all\" style=\"padding: 0 .7em;\"><ul></ul></div></div>");
                        var ul = $(self).find("form").find(".returns div div").find("ul");
                        for (var x = 0; x < data.messages.length ; x++) {
                            var li = $.parseHTML("<li></li>");
                            $(li).html("<span class=\"ui-icon ui-icon-alert\"></span> " + data.messages[x] + "</span>");
                            $(ul).append(li);
                        };
                        return;
                    }
                    $(self).find("form").find("div[class='returns']").html("<div class=\"ui-widget\"><div class=\"ui-state-error ui-corner-all\" style=\"padding: 0 .7em;\"><p><span class='ui-icon ui-icon-alert'></span>" + data.message + "</span></p></div></div>");

                }
            };

            this.onFailure = function (data) {

                if (!data.message)
                    data.message = ("<p>Error encountered in ajax request.</p>");


                if (data.messages)
                {
                    $(self).find("form").find("div[class='returns']").html("<div class=\"ui-widget\"><div class=\"ui-state-error ui-corner-all\" style=\"padding: 0 .7em;\"><ul></ul></div></div>");
                    var ul = $(self).find("form").find(".returns div div").find("ul");
                    for (var x = 0; x < data.messages.length ; x++) {
                        var li = $.parseHTML("<li></li>");
                        $(li).html("<span class=\"ui-icon ui-icon-alert\"></span> " + data.messages[x] + "</span>");
                        $(ul).append(li);
                    };
                    return;
                }
                $(self).find("form").find("div[class='returns']").html("<div class=\"ui-widget\"><div class=\"ui-state-error ui-corner-all\" style=\"padding: 0 .7em;\"><p><span class='ui-icon ui-icon-alert'></span>" + data.message + "</span></p></div></div>");
         
                 


            };

            if (config.success)
            {

                this.onSuccess = config.success;
            }
            if (config.failure)
            {

                this.onFailure = config.failure;
            }

            if (config.autoOpen) dautoOpen = config.autoOpen;
            if (config.width) dwidth = config.width;
            if (config.height) dheight = config.height;
            if (config.title) dtitle = config.title;
            if (config.saveText) saveText = config.saveText;
            if (config.abortText) abortText = config.abortText;

            if (config.url) targetUrl = config.url;
            if (config.method) targetMethod = config.method;


            $(htmlTemplate).find("form").attr("action", targetUrl);
            $(htmlTemplate).find("form").attr("method", targetMethod);
            
            $(this).append(htmlTemplate);
            console.log("Calling Callbacks");
            for (var c = 0; c < callBacks_.length; c++)
            {
                var callBack = callBacks_[c];
                $(document).ready(function () {
                    callBack.cb(callBack.CFG);
                });
            }

            self = this;
            $(self).find("form").find("div[class='returns']").html("");

            $(this).find("form").validate({
                submitHandler: function (form) {
                  
                    var data = $(form).find(":input").serialize();
                    $.ajax({
                        type: targetMethod,
                        url: targetUrl,
                        data: data
                    }).done(self.onSuccess)
                    .error(self.onFailure);
                },
                errorPlacement: function (err, el) {
                    $(el).attr("title", $(err).html());
                    
                    $(el).css({ border: "1px dotted red" });
                    $(el).tooltip({
                        using: function (p, f)
                        {
                            $(this).addClass("ui-state-error");
                            $(this).addClass("ui-corner-all");
                        }
                    });
                }
            });

            $(this).dialog(
                {
                    autoOpen: dautoOpen,
                    width: dwidth,
                    height: dheight,
                    title: dtitle,
                    modal: modal,
                    open: function () {

                        $(self).find("form").find("div[class='returns']").html("");

                    },
                    buttons: {

                        saveButton: function () {

                           
                            $(this).find("form").submit();
                          
                        },
                        abortButton: function () {

                            $(this).dialog("close");

                        }

                    }



                });


            console.log("Load Buttons");
            var test = $(this).find("~ .ui-dialog-buttonpane button").each(function () {

                var bt = $(this).text();
                var span = $(this).find("span");
                if (bt == "saveButton") $(span).text(saveText);
                if (bt == "abortButton") $(span).text(abortText);

            });
             


           

        }
    };

})(jQuery);