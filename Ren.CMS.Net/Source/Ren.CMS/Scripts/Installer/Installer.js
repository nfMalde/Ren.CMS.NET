var wizard = null;
$(function () {
    var options = { showClose: false, backdrop: 'static', contentWidth: 1050, contentHeight: 800};
    window.wizard = $("#rencmswizard").wizard(options);
    wizard.show();
    $('#selectDatabases').click(function (e) {
        e.preventDefault();
        $('#dbselector').fadeOut();
        $('#dbselect_errors').text('');
        $('#dbselect_errors').fadeOut();
        $.post(window.urlGetDB, wizard.cards.card4.el.find(":input").serialize(), function (data) {
            if (data.success) {

                $('#dbselector').find("option").filter(function () {
                    return !$(this).hasClass("option-label");
                }).remove();

                for (var i = 0; i < data.dbs.length; i++) {
                    var $op = $("<option/>");
                    $op.attr("value", data.dbs[i].name);
                    $op.text(data.dbs[i].name);
                    $('#dbselector').append($op);
                            
                }

                $('#dbselector').fadeIn();
                         
            }
            else {
                $('#dbselect_errors').text(data.message);
                $('#dbselect_errors').fadeIn();
            }
        }, 'json');
    });

    $('#dbselector').change(function () {
        $('.server-database input').val($(this).val());
        $(this).fadeOut();
    });
    $('#recheck').click(function (e) {
        e.preventDefault();
               
        summaryCheck();
    });

    function writeSummaryStatus($code)
    {
        var sp = $('#summary-status').val().split('-');
        sp.push($code);
               
        $('#summary-status').val(sp.join('-'));
        if ($('#summary-status').val().indexOf('-') == 0)
            $('#summary-status').val($('#summary-status').val().substr(1));

    }

    function summaryCheck() {
        $.post(window.urlGetSummaryTree, function (data) {

            $('#summary-content').html(data);
            $('#summary-status').val('');
            //Hiding Buttons
            wizard.hideButtons();
            //Field Definition
            var licenseField = $('#summary-license');
            var installMethod = $('#summary-install-method');

            //Check License
            if ($('input[name="LicenseAccepted"]').is(":checked")) {


                licenseField.addClass("text-success");
                licenseField.css("font-weight", "bolder");
                licenseField.text("License: Accepted");
            }
            else {
                licenseField.addClass("text-danger");
                licenseField.css("font-weight", "bolder");
                licenseField.text("License: Not Accepted");
            }

            //Check Install Method
            var summaryTextInstallType = '';
            var summaryStatusInstallType = '';
            switch ($('input[name="InstallationType"]').val()) {
                case '1':
                    summaryTextInstallType = 'Full Installation';
                    summaryStatusInstallType = 'text-success';
                    break;
                case '2':
                    summaryTextInstallType = 'Update';
                    summaryStatusInstallType = 'text-success';
                    break;
                default:
                    summaryTextInstallType = 'Not selected';
                    summaryStatusInstallType = 'text-danger';
                    break;
            }

            installMethod.addClass(summaryStatusInstallType);
            installMethod.css("font-weight", 'bold');
            installMethod.text(summaryTextInstallType);

            var servername = $('#summary-servername');
            var serverAuth = $('#summary-auth');
            var serverDB = $('#summary-db');
            var serverUsername = $('#summary-username-server');
            var serverPassword = $('#summary-password-server');
            var serverPrefix = $('#summary-prefix');


            //Add Summary for Server Data
            var sInstance = $('input[name="ServerInstance"]').val()
            servername.text("Servername: " + sInstance);
            var sAuth = $('select[name="Auth"]').val();
            var sAuthText = '';

            var sPrefix = $('.server-prefix').find("input").val();
            serverPrefix.text('Table Prefix: ' + sPrefix);

            switch(sAuth)
            {
                case '1':
                    sAuthText = 'Windows Authentification';
                    $('#summary-username-server').hide();
                    $('#summary-password-server').hide();
                    break;
                case '2':
                    sAuthText = 'User Authentification';
                    $('#summary-username-server').show();
                    $('#summary-password-server').show();
                    $('#summary-username-server').text('Username: '+ $('input[name="ServerUserName"]').val());
                    $('#summary-password-server').text('Password: ' + $('input[name="ServerPassword"]').val());
                    break;
                default:

                    break;
            }
            serverAuth.text("Authentification-Method: " + sAuthText);
            $('#summary-db').text('Database: '+ $('input[name="Database"]').val())
            $('#connectionString').val("Generating please wait...");
                
             
            $.post(window.urlGetConnectionString, wizard.cards.card4.el.find(":input").serialize(), function (data) {

                if (data.success == true) {
                    $('#connectionString').removeClass('text-danger');
                    $('#connectionString').val(data.connectionString);
                }
                else{
                    $('#connectionString').addClass('text-danger');
                    $('#connectionString').val('Error generating Connection-String');
                    $('<p/>').text(data.error).insertAfter($('#connectionString'));

                }
                var summaryTest = $('#summary-connectiontest');
                summaryTest.text("Connectiontest: Pending...");
                var constr = { connectionString: data.connectionString };
                $.post(window.urlTestConnection, constr, function (data) {
                    var testClass = '';
                    var testText = '';
                    if (data.ok) {
                        summaryTest.text("Connectiontest: Success.");
                        summaryTest.addClass('text-success');
                        $.post(window.urlSystemRequirements, constr, function (data) {
                            var ul = $('#summary-system');
                            ul.find("li").remove();

                            for (var i = 0; i < data.data.length; i++) {
                                if (data.data[i].failure == true)
                                    writeSummaryStatus('E');
                                else if (data.data[i].cssClass == 'text-warning')
                                    writeSummaryStatus('W');
                                else
                                    writeSummaryStatus('S');

                                var li = $('<li/>');
                                $(li).append('<strong>' + data.data[i].title + "</strong>");
                                var nUl = $('<ul/>');
                                $(nUl).append("<li>Required: " + data.data[i].required + "</li>");
                                $(nUl).append("<li class=\"" + data.data[i].cssClass + "\">Actual: " + data.data[i].actual + "</li>");

                                $(li).append(nUl);
                                $(ul).append(li);
                            }
                            wizard.showButtons();

                        }, 'json');
                    }
                    else {
                        summaryTest.text("Connectiontest: Failure. ("+ data.error +")");
                        summaryTest.addClass('text-danger');
                        //Show Buttons
                        wizard.showButtons();

                    }


                },'json')



            }, 'json');








            //Add Summary for System Requirements
        });

    }

    wizard.cards.card5.on('selected', function () {
     
        summaryCheck();

    });

    wizard.cards.install.on('selected', function () {
        var codes = $('#summary-status').val().split('-');
        for (var i = 0 ; i < codes.length; i++)
            if (codes[i] == 'E') {
                wizard.setCard("card5");
                return;
            };

        //Disable Labels
        var list = $('li.wizard-nav-item.already-visited a');
        $('li.wizard-nav-item.already-visited').unbind("click");
        $('li.wizard-nav-item.already-visited').addClass("text-success");
        list.each(function () {
            $(this).removeClass("wizard-nav-link");
            $(this).addClass("text-success");
            $(this).unbind("click");
            var icon = $(this).find("span.glyphicon");
            $(icon).removeClass("glyphicon-chevron-right");
            $(icon).addClass("glyphicon-check pull-right");
        });
        //Hide Buttons
        wizard.hideButtons();

        //Install
        getInstallAction();
    });

    $('.install-new-lng').change(function () {

        var that = this;
        var preinstalled = $('.pre-installed-lng').filter(function () {
            return $(this).val() == $(that).attr("data-code");
        });

        if (preinstalled.length > 0) {
            $('#warning').modal("show");
        }
       
        $('.install-new-lng').filter(function () { return $(this).attr("id") != $(that).attr("id") && $(this).is(":checked") && $(this).attr("data-code") == $(that).attr("data-code");}).removeAttr("checked");

    });

    $('#warning').modal({ show: false, background: 'static' });
    $('.closewarning').click(function (e) {
        e.preventDefault();
        

        if ($(this).hasClass("no")) {
            var id = $('#decisionID').val();
            $('#' + id).removeAttr("checked");
        }
        $('#decisionID').val('');
        $(this).modal("hide");
    });

});

//Validation
function validateDB(card)
{

    card.el.find(".popover").remove();
   
    var connectionOK = false;
    var serverinstance = card.el.find(".server-instance").find("input").val();
    var authType = card.el.find(".server-auth").find("select").val();
    var username = card.el.find(".server-username").find("input").val();
    var password = card.el.find(".server-password").find("input").val();
    var prefix = card.el.find('.server-prefix').find("input").val();
    var db = card.el.find(".server-database").find("input").val();
    switch(authType)
    {
        case '1':
            if (serverinstance.trim() == '') {
                card.wizard.errorPopover(card.el.find(".server-instance").find("input"), "Server instance cannot be empty");
                return false;
            }
            if (prefix.trim() == '') {
                card.wizard.errorPopover(card.el.find(".server-prefix").find("input"), "Table prefix cannot be empty");
                return false;
            }

            if (db.trim() == '') {
                card.wizard.errorPopover(card.el.find(".server-database").find("input"), "Database cannot be empty");
                return false;
            }

            break;
        case '2':
            if (serverinstance.trim() == '') {
                card.wizard.errorPopover(card.el.find(".server-instance").find("input"), "Server instance cannot be empty");
                return false;
            }
            if (username.trim() == '') {
                card.wizard.errorPopover(card.el.find(".server-username").find("input"), "Username cannot be empty");
                return false;
            }
            if (password.trim() == '') {
                card.wizard.errorPopover(card.el.find(".server-password").find("input"), "Password cannot be empty");
                return false;
            }
            if (prefix.trim() == '') {
                card.wizard.errorPopover(card.el.find(".server-prefix").find("input"), "Table prefix cannot be empty");
                return false;
            }
            if (db.trim() == '') {
                card.wizard.errorPopover(card.el.find(".server-database").find("input"), "Database cannot be empty");
                return false;
            }
            break;
        default:
            card.wizard.errorPopover(card.el.find(".server-auth").find("select"), "Please select an authentification method.");
            return false;
            break;
    }


    return true;
}
function validateLicense(el) {

    var retValue = {};

    if (!el.is(":checked")) {
        retValue.status = false;
        retValue.msg = "You need to accept the license to continue";
    }
    else {
        retValue.status = true;
    }

    return retValue;
}

function validateInstallType(card)
{
    var active = card.el.find('.installation-type.btn-primary');
    if (active.length > 0)
    {
        $(active).find("input").click();
        return true;
    }

    return false;
}


function validateLanguages(card) {
    var languagesCheckBox = card.el.find(":checkbox").filter(function () {
        return $(this).is(":checked");
    });
    var DefaultLanguage = card.el.find(":radio").filter(function () {
        return $(this).is(":checked");
    });
    if (languagesCheckBox.length == 0) {
        card.find(":checkbox").each(function () {

            card.wizard.errorPopover($(this), "Please select at least one language");
        });

        return false;
    }

    if (DefaultLanguage.length == 0) {
        card.el.find(":radio").each(function () {
            card.wizard.errorPopover($(this), "Please select at least one language");
        });

        return false;
    }
    return true;
}

function getInstallAction()
{
    $.post(window.urlGetInstallAction, $('body').find(":input").serialize(), function (data) {

        if (data.success == true) {
            executeInstallAction(data.actions, 0);
        }

    }, 'json');
}


function executeInstallAction(actions, index)
{
    if (index >= actions.length)
    {
        //Done
        wizard.setCard(7);
        var list = $('li.wizard-nav-item.already-visited a');
        $('li.wizard-nav-item.already-visited').unbind("click");
        $('li.wizard-nav-item.already-visited').addClass("text-success");
        list.each(function () {
            $(this).removeClass("wizard-nav-link");
            $(this).addClass("text-success");
            $(this).unbind("click");
            var icon = $(this).find("span.glyphicon");
            $(icon).removeClass("glyphicon-chevron-right");
            $(icon).addClass("glyphicon-check pull-right");
        });
    }
    else
    {
        var number = index + 1;
        var url = actions[index].url;
        var percentageBar = $('#installProgress');
        var data = $('body').find(":input").serialize();
        $('#installCurrentAction').text(actions[index].title);
        $.post(url, data, function (data) {

            if (data.success == true) {
                //Calc new percentage
                var percentage = number * 100 / actions.length;
            
                percentageBar.find('.progress-bar').animate({ width: percentage + '%' });
                percentageBar.find('.progress-bar').attr("aria-value", percentage);
                percentageBar.find('.sr-only').text(percentage + "% Complete");
                executeInstallAction(actions, index + 1);

            }
            else {

                percentageBar.find(".progress-bar").addClass("progress-bar-danger");
                $('#installCurrentAction').removeClass("text-info");
                $('#installCurrentAction').addClass("text-danger");
            }
             

        }, 'json')
    }


    

}

$(function () {

    $('.installation-type').click(function (e) {
       
        $('.installation-type.btn-primary').removeClass('btn-primary');
        $(this).addClass("btn-primary");
        
    });

    $('#serverbrowser').modal({ show: false });
    $('#findServer').click(function (e) {
        e.preventDefault();
        $(this).fadeOut(function () {
            $('#serverbrowser').modal("show");
        });

    });

    $('#serverbrowser').on("hidden.bs.modal", function () {
        $('#findServer').fadeIn();
    })
    $('#serverSelector').find("select").change(function () {
        var val = $(this).val();
        $('.server-instance').find("input").val(val);
        $('#serverSelector .close').click();
    });

    $('.serverbrowserclose').click(function () {
        $('#serverbrowser').modal("hide");
    });

    $('.server-auth select').change(function () {
        if ($(this).val() == '1') {
            $('.server-username').fadeOut();
            $('.server-password').fadeOut();
        }
        else {

            $('.server-username').fadeIn();
            $('.server-password').fadeIn();
        }
    });

});