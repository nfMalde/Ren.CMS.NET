
((function ($) {

    $.fn.dataTableExt.oApi.fnGetVisibleColumns = function (oSettings, iColumn, bUnique, bFiltered, bIgnoreEmpty) {

        //Getting all visible fields

        var colums = oSettings.aoColumns;
        var visibles = new Array();
        for (var i = 0; i < colums.length; i++) {

            if (colums[i].bVisible)
                visibles.push(colums[i]);
        }



        return visibles;

    }

    $.fn.dataTableExt.oApi.fnGetColumnData = function (oSettings, iColumn, bUnique, bFiltered, bIgnoreEmpty) {
        // check that we have a column id
        if (typeof iColumn == "undefined") return new Array();

        // by default we only wany unique data
        if (typeof bUnique == "undefined") bUnique = true;

        // by default we do want to only look at filtered data
        if (typeof bFiltered == "undefined") bFiltered = true;

        // by default we do not wany to include empty values
        if (typeof bIgnoreEmpty == "undefined") bIgnoreEmpty = true;

        // list of rows which we're going to loop through
        var aiRows;

        // use only filtered rows
        if (bFiltered == true) aiRows = oSettings.aiDisplay;
            // use all rows
        else aiRows = oSettings.aiDisplayMaster; // all row numbers

        // set up data array	
        var asResultData = new Array();

        for (var i = 0, c = aiRows.length; i < c; i++) {
            iRow = aiRows[i];
            var aData = this.fnGetData(iRow);
            var sValue = aData[iColumn];

            // ignore empty values?
            if (bIgnoreEmpty == true && sValue.length == 0) continue;

                // ignore unique values?
            else if (bUnique == true && jQuery.inArray(sValue, asResultData) > -1) continue;

                // else push the value onto the result data array
            else asResultData.push(sValue);
        }

        return asResultData;
    }

    $.fn.dtAddColumnFilter = function ()
    {
        var dt = $(this).dataTable();
        if ($(this).hasClass("filter-modal-added")) return;
        
        $(this).addClass("filter-modal-added");

        //Creating Modal
        var $modal = $(new $.parseHTML("<div/>"));
        $modal.addClass("modal");
        $modal.addClass("fade");
        
        var $mDialog = $(new $.parseHTML("<div/>"));
        $mDialog.addClass("modal-dialog");

        var $mContent = $(new $.parseHTML("<div/>"));
        $mContent.addClass("modal-content");
       
        var $mHeader = $(new $.parseHTML("<div/>"));
        $mHeader.addClass("modal-header");
        
        var $mHeaderCloser = $(new $.parseHTML("<button/>"));
        $mHeaderCloser.attr("type", "button");
        $mHeaderCloser.attr("data-dismiss", "modal");
        $mHeaderCloser.attr("aria-hidden", "true");
        $mHeaderCloser.html("&times;");
        $mHeaderCloser.addClass("close");
        var $mTitle = $(new $.parseHTML("<h4/>"));
        $mTitle.addClass("modal-title");
        $mTitle.text("Filter");

        var $mBody = $(new $.parseHTML("<div/>"));
        $mBody.addClass("modal-body");
 
        var $filterForm = $(new $.parseHTML("<form/>"));

        $filterForm.attr("role","form");

        $mBody.append($filterForm);

        var $mFooter = $(new $.parseHTML("<div/>"));
        var $mFcloser = $(new $.parseHTML("<button/>"));
        $mFcloser.addClass("btn btn-default");
        $mFcloser.attr("data-dismiss", "modal");
        $mFcloser.attr("type", "button");
        $mFcloser.text("Schließen");
        
        $mFooter.append($mFcloser);
        $mFooter.addClass("modal-footer");

        //Bulder Modal
        $mHeader.append($mHeaderCloser);
        $mHeader.append($mTitle);
        $mContent.append($mHeader);
        $mBody.append($filterForm);
        $mContent.append($mBody);
        $mContent.append($mFooter);
        $mDialog.append($mContent);
        $modal.append($mDialog);
        $modal.attr("id", "filterModal-"+ $(this).attr("id"));
        $('body').append($modal);
        $($modal).modal({ show: false });
        

       
        function fnCreateSelect(aData) {
            var r = '<select multiple="multiple"><option value=""></option>', i, iLen = aData.length;
            for (i = 0 ; i < iLen ; i++) {
                r += '<option value="' + aData[i] + '">' + aData[i] + '</option>';
            }
            return r + '</select>';
        }

 

      


       
            /* Initialise the DataTable */
        var oTable =dt
            
            var head = $(oTable).find("thead");
            var $filterrow = $($.parseHTML("<tr></tr>"));
            var visibles = oTable.fnGetVisibleColumns();
            for (var i = 0; i < visibles.length; i++)
            {
                var headerText = visibles[i].sTitle;
                var dataIndex = visibles[i].mData;

                var elementID = $(oTable).attr("id") + "-filter-" + i;

                var $div = $(new $.parseHTML("<div/>"));
                var $label = $(new $.parseHTML("<label/>"));
                $label.text(headerText);
                $label.attr("for", elementID);

                $div.html($label);

                $div.append(fnCreateSelect(oTable.fnGetColumnData(dataIndex)));
                $div.addClass("form-group");
                $div.find("select").attr("id", elementID);
                $div.find("select").addClass("filter-make-select");
                $div.css("margin-top", "10px");

                $($div).find("select").change(function () {
                    oTable.fnFilter($(this).val(), dataIndex);
                });

                $filterForm.append($div);
                $div.find("select").chosen();
                $div.find(".chzn-container").css("width", "100%");//
                $div.find(".chzn-container ul").addClass("form-control");
                $div.find(".chzn-container").find("input").addClass("form-control");

                $div.find(".chzn-drop").css("width", "100%");
                $div.find(".chzn-drop").find(".chzn-search").find("input").css("width", "100%");
                $div.css("width", "100%");

            }

            return;
            head.first("tr").find("th").each(function (i) {


               var p =  oTable.fnGetPosition($(this)[0]);
               conso.log(p);


                return;
               
                
            });
          
     
    
        
            /*
            
            var headerRow = head.find("tr");
            headerRow.addClass("filterrow");

            headerRow.find("th").each(function (i) {
                if ((function (c) {
                    var z = false;
                    $(c).find(".filtercontainer-nf").each(function () {
                        z = true;
                    });
                    return z;
                })(this))
                    return;


                var $p = $(new $.parseHTML("<p></p>"));
                $p.addClass("filtercontainer-nf");
                $p.hide();

                $p.html(fnCreateSelect(oTable.fnGetColumnData(i)));
                $($p).find("select").change(function () {
                    oTable.fnFilter($(this).val(), i);
                });

                $(this).append($p);

                $(this).find("select").chosen();
                $(this).find(".chzn-container").css("width", "100%");//
                $(this).find(".chzn-drop").css("width", "100%");
                $(this).find(".chzn-drop").find(".chzn-search").find("input").css("width", "100%");


            });
            */

         


    }

    $.fn.dtShowColumnFilter = function ()
    {
 

      //  $(this).find("thead").find("tr.filterrow").find(".filtercontainer-nf").fadeIn();
        var elid = "filterModal-" + $(this).attr("id");
        $('#' + elid).modal("show");


    }

    $.fn.dtHideColumnFilter = function () {
        //  $(this).find("thead").find("tr.filterrow").find(".filtercontainer-nf").fadeIn();
        var elid = "filterModal-" + $(this).attr("id");
        $('#' + elid).modal("close");

    }



})(jQuery));


function fixActionColumn($tb){
    
    $tb.find("thead").find("th").each(function () {
            

        if ($(this).text() == "ActionColumn") {
            $(this).text("");
            $(this).css("width", "55px");
            $(this).css("max-width", "55px");

        }

    });
}
function renderActionColumn(a,b,c,d) {
   
    console.log(b);
    console.log(c);
    console.log(d);

    var obj = jQuery.parseJSON(a);
    $html = $($.parseHTML("<div/>"));


    if (obj.edit) {
        if (obj.edit.enabled && obj.edit.enabled == true) {
            var $a = $($.parseHTML("<a/>"));
            $a.addClass("edit");
            $a.addClass("btn btn-sm btn-default");
            $a.attr("onclick", (obj.edit.action) + "; return false;");
           

            $html.append($a);
        }
    }

    if (obj.delete) {
        if (obj.delete.enabled && obj.delete.enabled == true) {
            var $a = $($.parseHTML("<a/>"));
            $a.addClass("delete");
            $a.addClass("btn btn-sm btn-danger");
            $a.attr("onclick", (obj.delete.action) +"; return false;");
            $html.append($a);
        }
    }

    

    


  
    $html.find("a.edit").attr("href", "#");
    $html.find("a.delete").attr("href", "#");

    $html.find("a.edit").html("<i></i>");
    $html.find("a.edit").find("i").addClass("fa fa-pencil-square-o");

    $html.find("a.delete").html("<i></i>");
    $html.find("a.delete").find("i").addClass("fa fa-times-circle-o");

    


    return $html.html();
}


