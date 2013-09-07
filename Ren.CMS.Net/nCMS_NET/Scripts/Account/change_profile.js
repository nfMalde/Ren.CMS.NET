$(document).ready(function () {
    $('.progressbar').progressbar({ value: 0 });

    $('#file_upload').fileupload({
        dataType: 'json',
        url: '/Account/SaveAvatar/Ajax',
        progressall: function (e, data) {
            $(this).find('.progressbar').progressbar({ value: parseInt(data.loaded / data.total * 100, 10) });
        },
        done: function (e, data) {
            $('#file_name').html(data.result.name);
            
            $('#show_image').html('<img src="/home/image/' + data.result.name + '" />');
            $(this).find('.progressbar').progressbar({ value: 100 });
        }
    });
});