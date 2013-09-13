$(function () {

    $.fn.flexReturnSelected = function () { // returns IDs of selected TRs 

        var selected = [];
        this.each(function () {
            if (this.grid) {
                $('.trSelected', this).each(function () {
                    selected.push(this.id.toString());
                });
            }
        });

        return selected;

    };



});