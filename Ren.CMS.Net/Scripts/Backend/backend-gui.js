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
        $(".ShortCut").draggable({
            containment: "#frame-desktop-board",
            scroll: false,
            revert: 'invalid',

            distance: 30,
            grid: [64, 64],
            snap: true,
            snapMode: 'outer'
            //  appendTo: '#frame-desktop-menubar'
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

        $('#logout').click(function () {


 
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
                    accept: '.ShortCut'

                });
                $('button').each(function () {

                    $(this).button();

                });

                $('#startmenu').menu();
                $('#time').tooltip();
    
                
                $(this).fadeIn('slow', function () {

                   // new BackendGui().MenuCount.set(11);
                    new BackendGui().refreshMenu();
               
                });
            
               
                new BackendGui().initIcons();
                new BackendGui().initLogoutButton();
            
            }
            );
            var dialogOK = new BackendGui().dialog('authentification_ok_1', 'Authentifiziert', 'Sie wurde erfolgreich angemeldet.', new BackendGui().dialogStates.info, 'ui-icon-info');
            dialogOK.create();
            new BackendGui().dialog('authentification_ok_1').hide();
            dialogOK.show();
            new BackendGui().updateMenuBarTime();
            window.setTimeout("new BackendGui().dialog('authentification_ok_1').hide();", 15000);
           
        }
    }

}