$(function () {

    //Config
    var pageSize = 8;
    var minWidth = 128;
    var minHeight = 72;
    var navigationElement = undefined;


    $div =  function () {

        var div = $.parseHTML("<div></div>");

        return div;


    };


    $a =  function () {

        var a = $.parseHTML("<a></a>");
        return a;

    };

    var $img = function () {

        var img = $.parseHTML("<img>");
        return img;
    };
    $('#g-nav a').click(function (e) {

        if (!$(this).hasClass("disabled") && navigationElement)
        {

            switch ($(this).attr("data-slide")) {

                case "prev":
                    $(navigationElement).carousel("prev");
                    break;

                case "next":

                    $(navigationElement).carousel("next");
                    break;



            }

        }
             

    });


    $(document).ready(function (e) {

        
        var el2add = $('.media-navbar-contents')
        var self = el2add;
        if ($(self).attr("data-id") && $(self).attr("data-page") && $(self).attr("data-type"))
        {
            $(this).button('loading');
       
            $.post("/Gallery/GetGalleryNavigation",
                {
                    Type: $(self).attr("data-type"),
                    Page: $(self).attr("data-page"),
                    ContentID: $(self).attr("data-id")



                },
                function (data) {
                    if (data.success == true) {
                        var list = $div();
                        $(list).attr("id", "nav-" + $(self).attr("data-id"));
                        $(list).addClass("carousel slide");

                        var innerDiv = new $div();
                        $(innerDiv).addClass("carousel-inner");
                        

                        var index = $(self).attr("data-page");

                        var ind = 5; //5 Thumnails per slide
                        var lastItem = new $div();

                        var items = 0;
                        var left = 0;



                        for (var x = 0; x < data.items.length; x++)
                        {

                            if (!$(lastItem).hasClass("item"))
                            {

                                $(lastItem).addClass("item");

                            }
                            var image = new $img();

                            $(image).attr("src", data.items[x].ThumpnailPath + "-" + minWidth + "-" + minHeight + ".jpg");
                            $(image).fadeTo(1, 0.5);
                            $(image).hover(function () {

                                $(this).fadeTo("fast", 1.0);

                            });

                            $(image).mouseout(function () {
                                if ($(this).hasClass("active-g")) {

                                    $(this).fadeTo("fast", 0.8);

                                }
                                else
                                $(this).fadeTo("fast", 0.5);

                            });

                            $(image).attr("title", data.items[x].Title);
                            $(image).addClass("gallery-nav-img");

                            var ia = $(new $a()).html(image);
                            $(ia).attr("href", "/Gallery/" + $(self).attr("data-ct") + "/" + $(self).attr("data-id") + "/" + $(self).attr("data-type") + "/" + (x + 1));
                            $(ia).attr("id", "item-" + (x + 1));
                            $(ia).addClass("nav-item-g");
                           

                            $(ia).click(function (e) {

                                e.preventDefault();
                                var self = this;
                                $.get($(this).attr("href"), function (data)
                                {
                                    data = $.parseHTML(data);
                                    var title = $(data).find("title").html();
                                    var header = $(data).find(".gheader").html();
                                    var img = $(data).find(".gmain-img").attr("src");
                                    var prev = $(data).find(".gprev").html();
                                    var next = $(data).find(".gnext").html();
                                    var remarks = $(data).find(".remarks").html();
                                    $("title").text(title);
                                    $(".remarks").html(remarks);
                                    $(".gheader").html(header);
                                   //TODO: Video Support.
                                    if ($(self).attr("data-type") == "video") {
                                        $('.blog-gallery-content').html($(data).find(".blog-gallery-content").html());
                                    }
                                    else {
                                        $(".gmain-img").fadeTo('slow', 0.1).attr("src", img).fadeTo("slow", 1.0);
                                    }
                                    $(".gprev").html(prev);
                                    $(".gnext").html(next);
                                    $(innerDiv).find(".active-g").removeClass("active-g").fadeTo(1, 0.5);
                                    $(self).find("img").addClass("active-g").fadeTo(1, 0.8);;
                                    window.location.hash = "#!" + $(self).attr("id");


                                });

                            });

                            var content = $(new $div()).html(ia);
                            $(content).addClass("gallery-nav-img");
                             $(lastItem).append(content);

                            if (index == x)
                            {
                                $(lastItem).addClass("active");
                                $(ia).find("img").fadeTo(1, 0.8);
                                $(ia).find("img").addClass("active-g");
                            }
                            if (window.location.hash)
                            {
                                var h = $(lastItem).find("a").each(function () {
                                    if ("#!" + $(this).attr("id") == window.location.hash) {
                                        $(innerDiv).find(".active").removeClass("active");
                                        $(lastItem).addClass("active");
                                        $(this).find("img").fadeTo(1, 0.8);
                                        $(innerDiv).find(".active-g").removeClass("active-g").fadeTo(1,0.5);
                                        $(ia).find("img").addClass("active-g").fadeTo(1, 0.8);;
                                    }
                                });

                            }

                            items++;
                            if (items == pageSize)
                            {
                                $(lastItem).attr("style", "height: " + ((pageSize/2) * (minHeight + 20)) + "px");
                                $(innerDiv).append(lastItem);
                                lastItem = new $div();
                                items = 0;

                            }

                            


                        }

                        if ($(lastItem).hasClass("item")) {
                            $(innerDiv).append(lastItem);
                            $(lastItem).attr("style", "height: " + ((items/2) * (minHeight + 20)) + "px");
                        }


                        $(list).html(innerDiv);

                        $(el2add).html(list);
                        $(el2add).find(list).carousel({ inetval: -1 }).carousel('pause'); 

                        $(el2add).fadeIn();
                        navigationElement = $(el2add).find(list);
                        if (data.items.length > pageSize)
                        {
                            $('#g-nav a').removeClass("disabled");

                        }

                        if (window.location.hash)
                        {

                            $(innerDiv).find(".nav-item-g").each(function () {


                                if ("#!" + $(this).attr("id") == window.location.hash)
                                    $(this).click();

                            });


                        }

                        $('.nav-item-g img').tooltip({ container: 'body', placement: 'bottom' });


                    }
                      

                });


        }

    });
    

});