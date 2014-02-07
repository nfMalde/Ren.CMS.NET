$(function () {
    $('#slider_frame ul li').hover(function () {

        $(this).find('.slider-blender').fadeIn(1000);
      

    });

 

    $('#slider_frame ul li').mouseleave(function () {

        $(this).find('.slider-blender').fadeOut(1000);


    });

    $('#slider_frame ul li').click(function () {

       var l =  $(this).find(".slider-overlay").find("a").attr("href");
       location.href = l;
    });

    //Init Slider
    $('#slider_frame ul').anythingSlider({

        //Config
        mode: 'horiz',
        expand: true,
        resizeContents: true,
        showMultiple: false,
        buildArrows: false,      // If true, builds the forwards and backwards buttons 
        buildNavigation: false,      // If true, builds a list of anchor links to link to each panel 
        buildStartStop: false,      // If true, builds the start/stop button 
        autoPlay: true,
        pauseOnHover: true,


        //Handler
        onSlideComplete: function (slider) {

            var sliderID = slider.$currentPage.find("img").attr("id");
      
           
           $('.slider-active').removeClass("slider-active");
           
           $('#slider-' + sliderID).addClass("slider-active");
        }

    });


    //Init Navigation

    $('.slider-menu-bar a').click(function (e) {

        e.preventDefault();
        var id = $(this).attr("href");
        id = id.replace("#", "");
        id = parseInt(id);
        $('#slider_frame ul').anythingSlider(id);

    });

});