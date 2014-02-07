(function ($) {



    $.fn.msgWidget = function (type, title, text, addingMethod) {

        var self = this;
        $(self).find("div[class^='message']").remove();
        if (!addingMethod) addingMethod = 'top';
        //Adding container
        if (addingMethod == 'top')
            $(self).prepend("<div class=\"message\" style=\"font-size:11px;\"></div>");
        else
            $(self).append("<div class=\"message\" style=\"font-size:11px;\"></div>");

       
        var state_class = "highlight";
        var icon = "info";
        switch(type)
        {
        
            case 'error':

                icon = "alert";
                state_class = "error";
                break;

            case 'info':
                icon = "info";
                state_class = "highlight";
                break;
            case 'active':
                icon = 'lightbulb';
                state_class = 'active'
                break;
            case 'none':
                icon ='';
                state_class = 'default';
                break;
            default:
                icon ='lightbulb';
                state_class = 'default';
                break;

        }

        var msgDiv =   $(self).find("div[class='message']");
        $(msgDiv).addClass("ui-widget");
        $(msgDiv).html("");
        $(msgDiv).css({display:'none', fontSize:'11px !important', marginTop: '9px', marginBottom: '1px;'});
        $(msgDiv).append("<div></div>");
        var msgWidget = $(msgDiv).find("div");
        $(msgWidget).addClass("ui-state-"+ state_class +" ui-corner-all");
        $(msgWidget).html("<p></p>");
        var p = $(msgWidget).find("p");
        $(p).append("<strong></strong>");
        var s = $(p).find("strong");
        $(s).append("<span class=\"ui-icon ui-icon-"+ icon +"\" style=\"float: left; margin-right: .3em;\"></span>");
        $(s).append(title);
        $(p).append(text);

        $(msgDiv).fadeIn('slow');
    };
})(jQuery);