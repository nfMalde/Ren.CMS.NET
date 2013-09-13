function addHandlersComment() {
    $('.blog-comment-answer-inside').click(function (e) {
        e.preventDefault();

        var target = $(this).attr("data-target");
        var ref = $(this).attr("data-ref");



        $.post("/Blog/" + window.contenttype + "/GetAnswerInfo", { id: ref }, function (data) {
            var val = $('#' + target).find('form fieldset div textarea[name="Comment"]').val();
            if (val != null && val != '') {

                val += "\n\r";

            }
            val += "@" + data.ID + "-" + data.CreatorName + ": ";
            if (!$('#' + target).is(":visible")) {

                $('#' + target).slideDown();


            }

            $('#' + target).find('form fieldset div textarea[name="Comment"]').focus();

            $('#' + target).find('form fieldset div textarea[name="Comment"]').val(val);




        }, 'json');


    });

    $('.CommentTreeAnswer').click(function (e) {

        var target = $(this).attr("data-target");
        if (target) {
            $(this).fadeOut(function () {
                $('#' + target).slideDown();
            });
        }

    });


}

function loadAllComments(ContentType, ID,ref, CB)
{




    $.get("/Blog/" + ContentType + "/Comments/" + ID, function (data) {



        var htmldata = $.parseHTML(data);

        var html = $(htmldata).find("#CommentTree").html();
        $('#CommentTree').html(html).effect("highlight", {




        }, 3000);

        addHandlersComment();
        //Clone Captcha

        $('.answer-captcha').each(function () {


            $(this).html($('#add-comment-captcha').clone(true, true));

        });

        if (ref) {


            if (!$('#' + ref).is(":visible"))
            {

                $('#' + ref).show();

            }

            $("html, body").delay(2000).animate({ scrollTop: $('#' + ref).offset().top }, 2000);


            
        }

        new CB(data);
    });

}


$(function () {


    $('.show-more-comments').click(function (e) {

        e.preventDefault();
        var ct = window.contenttype;
        var ref = $(this).attr("data-ref");
        var id = $(this).attr("data-eid");

        loadAllComments(ct, id, ref);

    });

    addHandlersComment();
    
});