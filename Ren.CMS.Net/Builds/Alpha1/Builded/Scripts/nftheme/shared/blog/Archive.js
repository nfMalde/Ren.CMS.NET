var running = false;

$(function () {

    //Func
    function getDocHeight() {
        var D = document;
        return Math.max(
            Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
            Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
            Math.max(D.body.clientHeight, D.documentElement.clientHeight)
        );
    }
    function ExpandArchive()
    {
        if (running) return;

        var page = function (p) {
           
            if (!$('#pager input[type="hidden"]')[0]) {
                $('#pager').append("<input type=\"hidden\" name=\"page\" value=\"1\"/>");


            }

            var i = $('#pager input[type="hidden"]');
            if ($(i).attr("name") == "page") {
                if (p > 0) {

                    $(i).val(p);

                }
                return parseInt($(i).val());

            }
            return 1;
        };
        running = true;
        $('#pager a').button('loading');
        $.post("/News/ArchiveAjax", { page: page() + 1 }, function (data) {
          

            if (data.Contents) {

                for (var x = 0; x < data.Contents.length; x++) {
                    var list = null;

                    var checkUL = function (el) {

                        if ($(el).attr("id") == data.Contents[x].Key && $(el).children().length <50) {

                            list = el;

                        }


                    };
                    $('.entrylist').find("ul").last();

                    var li = function (url, title, previewtext, creationDate) {

                        var _li = $.parseHTML("<li></li>");
                        var a = new function () {


                            return $.parseHTML("<a></a>");
                        };

                        $(a).attr("href", url);
                        $(a).html(creationDate + " <b>" + title + "</b> " + previewtext);
                        $(_li).html(a);
                        $(_li).hide();
                        $(_li).addClass("entry-hidden");
                        return _li;

                    };



                    if (list == null) {
                        //Create List
                        var ul = function () {


                            var _ul = $.parseHTML("<ul></ul>");
                            $(_ul).attr("id", data.Contents[x].Key);
                            $(_ul).addClass("nav nav-list");
                           
                            return _ul;

                        };


                        var header = function (datatime) {

                            var div = $.parseHTML("<div></div>");

                            $(div).html("<strong></strong>");
                            $(div).find("strong").text("Beiträge seit " + datatime);
                            $(div).addClass("well");
                            $(div).addClass("well-small");
                            $(div).addClass("entry-hidden");
                            return div;


                        };

                        $('.entrylist').append(new header(data.Contents[x].KeyText));
                        list = new ul();
                        $('.entrylist').append(list);

                     





                    }


                    for (var c = 0; c< data.Contents[x].List.length; c++) {
                   
                        var url =   data.Contents[x].List[c].Row.FullLink;
                        var title = data.Contents[x].List[c].Row.Title;
                        var preview = data.Contents[x].List[c].Row.PreviewText;
                        var creationdate = data.Contents[x].List[c].DateString;

                        $(list).append(new li(url, title, preview, creationdate));


                    };




                }
                
            }
            //Set pager:
            $('#pager a').attr("href", "/News/Archive/" + (page(0) + 1));
            if (data.TotalRows > 0) {
                var pcount = $('#pagecount');
                data.Rows = data.Rows + parseInt(pcount.text());
                pcount.text(parseInt(data.Rows));


                $('#countTotal').text(data.TotalRows);
                page(page(0) + 1);

                $('li.entry-hidden').each(function () {

                    $(this).fadeIn(function () {


                        $(this).effect("highlight",  {

                         


                        }, 3000);

                        $(this).removeClass("entry-hidden");
                    });


                });

            }
            
            running = false;
            $('#pager a').button('complete');
        }, 'json');
        
    }

  
    $(window).scroll(function () {
        var documentHeight = $(document).height();

        var scrollPosition = $(window).height() + $(window).scrollTop();

        var bool = ((documentHeight - scrollPosition) <=30);//Offset of 30px

        $('#pager a').click();
    });

    $('#pager a').click(function (e) {

        e.preventDefault();
        ExpandArchive();


    });



});