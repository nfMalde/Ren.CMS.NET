function BackendGui()
{
    //Login
    this.login = function (username, password)
    {
        var return_ = false;
       
        var paras = { uName: username, uPassword: password };
        var loginStore = this.dataStore;

        loginStore.isAsync(false);
        loginStore.setFormat('json');
        loginStore.setHttpVerb('POST');
        loginStore.setUrl("/BackendHandler/Account/Login");
        loginStore.setParameters(paras);
        loginStore.doRequest();

        var content = loginStore.getContent();

        if (content.loginData.loginSuccessfull == true) return_ = true;
        else return_ = false;

        return return_;
    };
    this.dialogStates = { info: 'ui-state-highlight', error: 'ui-state-error', success: 'ui-state-success'}; 
    //CheckPermission
    this.dialog = function(id, title, text, state,jqueryuiIcon) {
        
        if(!state) state =  this.dialogStates.info;
        var backendID = 'backend-notify-'+ id;
        var html = '<div style="display:none; margin-left:10px; width:250px; float:left" class="ui-widget ui-helper-clearfix" id="backend-notify-'+ id +'">'+
	                '<div class="' + state + ' ui-corner-all ui-helper-clearfix" style="margin-top: 5px; padding: 0 .7em;">' +
                    '<div style="float:left; width:210px">'+
		            '<p>';
        if(jqueryuiIcon) html +='<span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>';
        if(title) html+='<strong>'+ title+'</strong><br/>';
        if(text)  html+=text;
        
        html+='</p></div>'+
                '<div class="backendNotfClickable ui-state-default" id="close-' + backendID + '" style="float:right width:17px; cursor:pointer; height 17px;">' +
                '<span class="ui-icon ui-icon-circle-close"><input type="hidden" class="containerid" value="'+ id +'" /></span></div>' +
               
                '</div></div>';
        var closeID = 'close-' + backendID;


        return {
            id: id,
            domID: backendID,
            title: title,
            text: text,
            create: function () {

                $('#notifications').append(html);
                $('#'+ closeID).click(function () {
                    var bid = $(this).find('.containerid').val();
           
                    new BackendGui().dialog(bid).remove();

                });


            },
            show: function ()
            {

                $('#' + backendID).fadeIn('slow');
            },
            hide: function()
            {
                
                $('#' + backendID).fadeOut('slow');
             
            },
            remove: function ()
            {

                $('#' + backendID).remove();

            }


        }

    }

    this.dataStore = (function () {
        var content;
        var httpVerb = 'GET';
        var Url;
        var Format = 'html';
        var async = true;
        var Parameters = {};
        return {
            doRequest: function()
            {
                if (Url)
                $.ajax({
                    type: httpVerb,
                    url: Url,
                    data: Parameters,
                    dataType: Format,
                    async: async,
                    success: function (data) {
                        content = data;
                     
                    }
                });
            },
            setHttpVerb: function(httpverb)
            {
                httpVerb = httpverb;
            },
            setParameters: function(parameters)
            {

                Parameters = parameters;
            },
            setUrl: function(url)
            {
                Url = url;
            },
            isAsync: function(isasync)
            {

                async = isasync;
            },
            setFormat:function(format)
            {

                Format = format;
            },
            getContent: function () {
       
                
                if (content) return content;
                // else show some error that it isn't loaded yet;
            }
        };
    })();
   
    this.checkPermission = function () {

        var return_ = false;
        var data1 = { hasPermission: false };

 
        var myData = this.dataStore;
        myData.setUrl("/BackendHandler/Account/checkPermission");
        myData.setFormat("json");
        myData.setHttpVerb("POST");
        myData.isAsync(false);
        myData.doRequest();
            
            
        var data = myData.getContent();


        data1 = data.loginData;
      
        return data1.hasPermission;

    };

    //Prepares the Document to handle the GUI Events
    this.prepareDocument = function () {
        $(document).ready(function () {


        });
    };

    //Returns HTML Response from the handler. Cann be converted to other Formats. example: jsonparse();
    this.getContentFromHandler = function (handler, action) {
        var returndata = null;
        $.get("/BackendHandler/" + handler + "/" + action, function (data)
        {

            returndata = data;

        });

        return returndata;
    };
    
    this.fixShortCutPositions = function () {

        var posCache = new Array();
        var w = $(window).width();
        var h = $(window).height();

        //Get All Desktop Icons
        $('.ShortCut').each(function () {
            
       
            var offset = $(this).offset();
            var ar = { el: this, left: offset.left, top: offset.top };
            posCache.push(ar);
            console.log(ar);

        });
        console.log("Icons Position Cache loaded:");
        console.log(posCache);

        for (x = 0; x < posCache.length; x++) {
            console.log("Running...");
            var l = posCache[x];

            if (h < posCache[x].top || w < posCache[x].left) {
                console.log("Icons is out of the Window: ");
                console.log(posCache[x]);
              
                for (y = 0; y < posCache.length; y++) {

                    var e = posCache[y];
                     

                        if (w < posCache[x].left) {
                            var newLeft = w - 128;

                            if (
                                posCache[y].left == newLeft && posCache[x].top == posCache[y].top && l.el != e.el
                                ) {
                                posCache[x].left = posCache[y].left - 128;
                            }
                            else {
                                posCache[x].left = newLeft;
                                console.log("News Left val: " + newLeft);

                            }
                        }

                        if (h < posCache[x].top) {
                            var newTop = h - 128;

                            if (
                                posCache[y].top == newTop && posCache[x].left == posCache[y].left && l.el != e.el
                                ) {
                                posCache[x].top = posCache[y].top - 128;
                            }
                            else {
                                posCache[x].top = newTop;

                            }
                        }
                    }

                
            }
        }
        var relaunch = false;
        //Lets fix the positions
        for (z = 0; z < posCache.length; z++) {
            console.log("Changing Poistion:");
            if (posCache[z].top == 0 && posCache[z].left == 0) {
                console.log("Element is not ready yet");
                relaunch = true;
            } else {
                $(posCache[z].el).css('top', posCache[z].top + 'px');
                $(posCache[z].el).css('left', posCache[z].left + 'px');
            }

        }
        if (relaunch == true) {

            window.setTimeout("new BackendGui().fixShortCutPositions();", 1000);
        }


    }
    //Window Resize Listener
    this.addWindowListener = function ()
    {
         
            $(window).resize(function () {
                console.log("Window Listener called!");
                new BackendGui().fixShortCutPositions();
           

            });
        

    };
        
    //Get the Desktop HTML Element
    this.getDesktopElement = function () {

        var desktopElement = $('#frame-desktop-board');

        if (desktopElement) return desktopElement;
        else return false;
    };

    //Redirect

    this.redirect = function (uri) {

        document.location.href = uri;

    };




 

    this.initIcons = function () {
        $(".ShortCut").each(function () {
            $(this).draggable({
                containment: "#frame-desktop-board",
                scroll: false,
                revert: 'invalid',
                 
                distance: 30,
                //grid: [64, 64],
                snap: true,
                snapMode: 'outer'
                //  appendTo: '#frame-desktop-menubar'
            });
            new BackendGui().bindContextDetail(this);
            $(this).dblclick(function () {

                var action =($(this).find('input[name="action"]').val());
                new widgetAction(action);


            });

        });

        
    };

    this.bindDefaultContext = function ()
    {
        console.log("Init: DefaultContext");
        $('#mainframe').click(function (e) {

            console.log("Hiding detail Context");
            $('#detailContext').css({
                display: 'none'
            }).hide();
            $('#defaultContext').css({
                display: 'none'
            }).hide();

        });
        
        $('#mainframe').bind("contextmenu", function (e) {
            new BackendGui().defaultContext(e);


            return false;
        });

/*        $('#frame-desktop-board').bind("contextmenu", function (e) {

            new BackendGui().defaultContext();

            return false;
        });
  */
  $(document).bind("contextmenu", function (e) {

            new BackendGui().defaultContext(e);

            return false;
        });




    };
    this.defaultContext = function (e)
    {
        console.log("Hiding detail Context");
        $('#detailContext').css({
            display: 'none'
        }).hide();

        $('#defaultContext').menu();

        $('#defaultContext').css({
            top: e.pageY + 'px',
            left: e.pageX + 'px'
        }).show();


    };
    this.bindContextDetail = function (el)
    {

        
         $(el).bind("contextmenu", function (e) {
             $('.OpenDT').attr("href", 'javascript: new widgetAction(\'' + $(this).find("input[name='action']").val() + '\');');
             
             $('.DeleteDT').attr("href", '#' + $(this).attr('id').replace('ico-shortcut-', ''));
             $('.DeleteDT').attr("id", $(this).attr('id').replace('ico-shortcut-', ''));

             $('.DeleteDT').click(function () {
                 $("#deleteDialog").dialog('open');
                 var id = $(this).attr('id').replace('#', '');
                 $("#deleteDialog").on("dialogclose", function (event, ui) {
                     var answer = $(this).find('input[name="answer"]').val();
                     if (answer == "yes")
                     {
                     
                         var parameter = { id: id };

                         var ds = new BackendGui().dataStore;
                         ds.setUrl('/BackendHandler/Account/RemoveIcon');
                         ds.setHttpVerb('POST');
                         ds.isAsync(false);
                         ds.setFormat('json');
                         ds.setParameters(parameter);

                         ds.doRequest();
                         console.log(ds.getContent());
                         var js = ds.getContent();

                         if (js.success == true) {

                             var elID = '#ico-shortcut-' + id;
                             $(elID).fadeOut('slow', function () {

                                 $(this).remove();
                             });
                         }
                     }

                 });

                 return false;
                
                




             });
             $('#defaultContext').css({
                 top: e.pageY + 'px',
                 left: e.pageX + 'px'
             }).hide();
             $('#detailContext').menu();

              
             


            $('#detailContext').css({
                top: e.pageY+'px',
                left: e.pageX+'px'
            }).show();



            return false;
        });

    };

    this.showLoginError = function (title, text) {
        $('#loginFormular').fadeIn();
        var html = '<div id="loginError" class="ui-widget" style="display:none">' +
                             '<div class="ui-state-error ui-corner-all" style="padding: 0 .7em;">' +
                             '<p><span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>' +
                             '<strong>' + title + ':</strong>&nbsp;' + text + '</p>' +
                             '</div>' +
                             '</div>';
        $('#loginError').remove();
        $('#loginFormular').prepend(html);
        
        $('#loginFormular').fadeIn();
        $('#loginError').fadeIn();
    

    };
    this.showLoginInfo = function (title, text) {
        $('#loginFormular').fadeIn();
        var html = '<div id="loginInfo" class="ui-widget" style="display:none">' +
                             '<div class="ui-state-highlight ui-corner-all" style="padding: 0 .7em;">' +
                             '<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>' +
                             '<strong>' + title + ':</strong>&nbsp;' + text + '</p>' +
                             '</div>' +
                             '</div>';
        $('#loginInfo').remove();
        $('#loginFormular').prepend(html);

        $('#loginFormular').fadeIn();
        $('#loginInfo').fadeIn();


    };

    this.LogOut = function (errorMsg,errorTitle) {
       var loginFormStore = this.dataStore;

        loginFormStore.setUrl("/BackendHandler/Layout/LoginForm");
        loginFormStore.setFormat('html');
        loginFormStore.setHttpVerb('GET');
        loginFormStore.isAsync(false);

        loginFormStore.doRequest();

        var content = loginFormStore.getContent();
      

        $('#mainframe').fadeOut('slow', function () {


            $(this).html(content);
            var gi = new BackendGui();
          if(errorMsg || errorTitle) gi.showLoginError(errorTitle, errorMsg);
            //Enable Ajax Functions for Form
            $('input[type="submit"]').each(function () {

                $(this).button();

            });
         
            $(function(){
                $('#ajaxLogin').submit(function (event) {

                    event.preventDefault();
                    $('#loginFormular').fadeOut(function () {
                        $('#waiter').fadeIn(function () {
                           
                            var u = $('#username').val();
                            var p = $('#password').val();
                           
                            var gx = new BackendGui();
                       
                            var logSucc = gx.login(u, p);
                      
                            if (logSucc == true) {

                                //Login successfull
                                if (gx.checkPermission())
                                    gx.loadDesktop();
                                else {

                                    $('#waiter').fadeOut(function () {

 
                                        gx.showLoginError('Fehlende Rechte', 'Sie verfügen nicht über die benötigten Recht diesen Bereich zu sehen.');

                                    });


                                }

                            }
                            else {

                                $('#waiter').fadeOut(function () {


                           
                                    gx.showLoginError('Fehler', 'Die Logindaten stimmen nicht überein.');


                                });

                            }
                        });

                    });
                });

               
            });
            
           

            $('#loginFormular').fadeOut();
            $('#waiter').fadeIn();
            $('#waitertext').html("<p>Bitte warten. Sie werden ausgeloggt...</p>");
            $('#mainframe').fadeIn();

            var logoutStore = new BackendGui().dataStore;

            logoutStore.setFormat('json');

            logoutStore.setHttpVerb('POST');
            logoutStore.setUrl('/BackendHandler/Account/Logout');
            logoutStore.isAsync(false);
            logoutStore.doRequest();

            var data = logoutStore.getContent();
            $('#waiter').fadeOut(function () {

                $('#waitertext').html("<p>Bitte warten. Sie werden eingeloggt...</p>");
                $('#loginFormular').fadeIn();

            });
            $(this).fadeIn();
            new BackendGui().showLoginInfo('Logout', 'Sie wurden erfolgreich abgemeldet.');
            
        });


    };
    this.loadLoginForm = function (errorMsg, errorTitle)
    {
       

        var loginFormStore = this.dataStore;

        loginFormStore.setUrl("/BackendHandler/Layout/LoginForm");
        loginFormStore.setFormat('html');
        loginFormStore.setHttpVerb('GET');
        loginFormStore.isAsync(false);

        loginFormStore.doRequest();

        var content = loginFormStore.getContent();
      

        $('#mainframe').fadeOut('slow', function () {


            $(this).html(content);
            var gi = new BackendGui();
          if(errorMsg || errorTitle) gi.showLoginError(errorTitle, errorMsg);
            //Enable Ajax Functions for Form
            $('input[type="submit"]').each(function () {

                $(this).button();

            });
         
            $(function(){
                $('#ajaxLogin').submit(function (event) {

                    event.preventDefault();
                    $('#loginFormular').fadeOut(function () {
                        $('#waiter').fadeIn(function () {
                           
                            var u = $('#username').val();
                            var p = $('#password').val();
                           
                            var gx = new BackendGui();
                       
                            var logSucc = gx.login(u, p);
                      
                            if (logSucc == true) {

                                //Login successfull
                                if (gx.checkPermission())
                                    gx.loadDesktop();
                                else {

                                    $('#waiter').fadeOut(function () {

 
                                        gx.showLoginError('Fehlende Rechte', 'Sie verfügen nicht über die benötigten Recht diesen Bereich zu sehen.');

                                    });


                                }

                            }
                            else {

                                $('#waiter').fadeOut(function () {


                           
                                    gx.showLoginError('Fehler', 'Die Logindaten stimmen nicht überein.');


                                });

                            }
                        });

                    });
                });

               
            });
            
           

            $(this).fadeIn();
        });

       
    };
    this.isLoggedIn = function () {

        var userStore = this.dataStore;

        userStore.isAsync(false);
        userStore.setUrl("/BackendHandler/Account/LoggedIn");
        userStore.setFormat('json');
        userStore.setHttpVerb('POST');
        userStore.doRequest();


        var data = userStore.getContent();
        if (data.LoggedIn == true)
            return true;

        else return false;


    };



    this.updateMenuBarTime = function () {
        if (!document.getElementById('frame-desktop-menubar')) return false;
        $.post('/BackendHandler/Layout/TimeUpdate', function (data) {


            $('#date').attr('title', data.date);
            $('#time').html(data.time);

            window.setTimeout("new BackendGui().updateMenuBarTime();", 4000);


        });



    };

    this.MenuCount = (function () {



        return {

            get: function () {


                var v = $('#backen_menu_count').val();

                var i = parseInt(v);

                return i;

            }
            ,
            set: function (value)
            {


                $('#backen_menu_count').val(parseInt(value));
            }


        }

    })();
    //Setups Intervalll for Refreshing the menu if needed 
    this.refreshMenu = function () {
      
        if (!document.getElementById('frame-desktop-menubar')) return false;
        var countStore = this.dataStore;

        countStore.setFormat('json');

        countStore.setHttpVerb('POST');

        countStore.setUrl("/BackendHandler/Layout/MenuCount");

        countStore.isAsync(false);


        countStore.doRequest();


        var countContent = countStore.getContent();

        if (countContent.count !=  this.MenuCount.get())
        {
            //Update
            var idMenuUpdateNotf = "MenuUpdateNotf";


            var dialogMenuUpdate = this.dialog(idMenuUpdateNotf, 'Aktualisierung...', 'Ihr Menu wird aktualsiert, da neue Menueinträge vorhanden sind. Bitte warten Sie...', this.dialogStates.info, 'ui-icon-info');
            dialogMenuUpdate.remove();
            dialogMenuUpdate.create();
            dialogMenuUpdate.show();

            $('#startmenucontainer').fadeOut();

            var menuStore = this.dataStore;

            menuStore.setFormat('html');

            menuStore.setHttpVerb('GET');

            menuStore.setUrl('/BackendHandler/Layout/Menu');

            menuStore.isAsync(false);

            menuStore.doRequest();

            //Clearing
            $('#startmenucontainer').find('ul').each(function () {

                $(this).remove();
 
            });

            //Set new content 
            $('#startmenucontainer').html('<ul id="startmenu">'+ menuStore.getContent() +'</ul>');
         
            $('#startmenu').menu();
         
            $('#startmenucontainer').fadeIn();
  
            dialogMenuUpdate.hide();
            dialogMenuUpdate.remove();
            if(countContent.count>this.MenuCount.get())
                dialogMenuUpdate = this.dialog(idMenuUpdateNotf, 'Ihr Menü wurde aktualisiert', 'Es befinden sich nun '+ (countContent.count - this.MenuCount.get()) +' neue Einträge im Menü.', this.dialogStates.info, 'ui-icon-info');
            else
                dialogMenuUpdate = this.dialog(idMenuUpdateNotf, 'Ihr Menü wurde aktualisiert', 'Es wurded ' + (this.MenuCount.get() - countContent.count) + ' Menüeinträge entfernt.', this.dialogStates.info, 'ui-icon-info');

            dialogMenuUpdate.create();
            dialogMenuUpdate.show();


            this.MenuCount.set(countContent.count);
            this.initLogoutButton();
        }

        
        window.setTimeout('new BackendGui().refreshMenu();', 5000);
    };


    this.initLogoutButton = function ()
    {

        $('.aMenuLogoutButtonStartMenuTray').click(function () {


 
            new BackendGui().LogOut();
           


        });


    };

       //Load Desktop
    this.loadDesktop = function ()
    {

        if (this.checkPermission()) {
           
            var desktopview;

            var deskStore = this.dataStore;
            deskStore.setHttpVerb('GET');
            deskStore.setFormat('html');
            deskStore.isAsync(false);

            deskStore.setUrl("/BackendHandler/Layout/Desktop");
            deskStore.doRequest();
            desktopview = deskStore.getContent();
            
        

            $('#mainframe').fadeOut('slow',function(){
                //var desk = this.getDesktopElement();
                $(this).html(desktopview);
                $('#frame-desktop-board').droppable({
                    
                    drop: function (e, ui) {
                        try{
                        var classNames = $(ui.draggable).attr('class').split(' ');

                        var accepted = false;
                        for (c = 0; c < classNames.length; c++)
                        {
                            if (classNames[c] == 'ShortCut') accepted = true;
                            if (classNames[c] == 'ShortCut2') accepted = true;


                        }
                        if(accepted != true) return false;
                        var id = $(ui.draggable).attr("id");

                        if (id.substring(0, 4) == "app-") {
                            var i = id.substring(4);
                            var posX = $(ui.draggable).offset().left;
                            var posY = $(ui.draggable).offset().top;
                            var parameters = { id: i, PosX: posX, PosY: posY };

                            $.post('/BackendHandler/Account/AddIcon', parameters, function (data) {

                                var GUI = new BackendGui();
                                var Desk = GUI.getDesktopElement();
                                $(Desk).append(data);
                                GUI.initIcons();

                            });

                        }
                        else {
                            //ico-shortcut-1
                            var stringTocut = "ico-shortcut-";
                            var len = stringTocut.length;

                            var id = parseInt($(ui.draggable).attr("id").substring(len));
                            var posX = $(ui.draggable).offset().left;
                            var posY = $(ui.draggable).offset().top;
                            $.post("/BackendHandler/Account/UpdateIconPos", { id: id, PosX: posX, PosY: posY });

                        }
                        }
                        catch (e)
                        {

                            console.log("Error - We dont break here lets continue");
                            console.log(e);

                        }
                    }
                });
                $('button').each(function () {

                    $(this).button();

                });

                $('#startmenu').menu();
                try{
                    $('#time').tooltip();
                }
                catch (e)
                {

                    console.log("Time cannot be tooltip, continue...");
                }
                
                $(this).fadeIn('slow', function () {

                    // new BackendGui().MenuCount.set(11);
                    new BackendGui().refreshMenu();
               
                });
            
               
                new BackendGui().initIcons();
                new BackendGui().initLogoutButton();


                var Icons = new BackendGui().dataStore;

                Icons.setUrl("/BackendHandler/Layout/Icons");
                Icons.setHttpVerb("POST");
                Icons.setFormat("json");
                Icons.isAsync(false);

                Icons.doRequest();

                var iicos = Icons.getContent();
                var icos = iicos.icons;
                for (x = 0; x < iicos.count; x++)
                {
                    console.log(icos[x]);
                    //Render
                  
                    //TODO: Position anhand Desktopauflösung überprüfen.

                    var newICON = new BackendGui().dataStore;
                    newICON.setFormat('html');
                    newICON.setParameters({
                        IconUrl: icos[x].iconUrl,
                        id: icos[x].id,
                        PosX: icos[x].xPos,
                        PosY: icos[x].yPos,
                        action: icos[x].action,
                        ShortCutText: icos[x].text


                    });
                    newICON.setHttpVerb("POST");
                    newICON.setUrl("/BackendHandler/Layout/RenderIcon");
                    newICON.isAsync(false);
                    newICON.doRequest();
                  
                    $('#frame-desktop-board').append(newICON.getContent());
                    new BackendGui().initIcons();
                
                }
             
            });
            var dialogOK = new BackendGui().dialog('authentification_ok_1', 'Authentifiziert', 'Sie wurde erfolgreich angemeldet.', new BackendGui().dialogStates.info, 'ui-icon-info');
            dialogOK.create();
            new BackendGui().dialog('authentification_ok_1').hide();
            dialogOK.show();
            new BackendGui().updateMenuBarTime();
            window.setTimeout("new BackendGui().dialog('authentification_ok_1').hide();", 15000);
            new BackendGui().initIcons();
            new BackendGui().addWindowListener();
            new BackendGui().fixShortCutPositions();
            new BackendGui().bindDefaultContext();


        }
    }

}