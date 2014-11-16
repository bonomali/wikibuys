$(window).load(function(){

	"use strict";

	// Button Heights and Widths
	
	$('.btn-wrapper').each(function(){
	
		var btnHeight = $(this).children('.btn').outerHeight() + 4;
		var btnWidth = $(this).children('.btn').outerWidth() + 4;
		
		$(this).css('height', btnHeight);
		$(this).css('width', btnWidth);
	
	});
	
	
	setTimeout(function(){$('#loader').addClass('hide-loader');},1000);
	$('#progress-bar').width('100%');


});

$(document).ready(function(){

	"use strict";

	// Smooth Scrolling
	
	$('.scroll').smoothScroll({
        offset: -80,
        speed: 800
    });
    
    // Slider backgrounds
    
    $('#home-slider .slides li').each(function(){
    
    	var imgSrc = $('.slider-bg').attr('src');
    	
    	$(this).children('.slider-bg').remove();
    	
    	$(this).css('background-image', 'url("'+imgSrc+'")');
    	
    	
    
    });
    
    // Mobile Menu Toggle
    
    $('.open-menu').click(function(){
    
    	if($('#nav').hasClass('open-nav')){
    		$('#nav').removeClass('open-nav');
    	}else{
    		$('#nav').addClass('open-nav');
    	}
    
    });

	// Initialize Sliders

	$('#home-slider').flexslider({ controlNav: false });

	$('#highlights-slider').flexslider({
		directionNav: false,
		slideshow: false,
		animation: "slide",
		animationLoop: true,
		controlNav: false,
		itemWidth: 243,
		itemMargin: 5,
		minItems: 1,
		maxItems: 4,
		move: 1
	});
	
	// Service Clicks
	
	$('.service').click(function(){
	
		if($(this).hasClass('open-service')){
			$(this).removeClass('open-service');
		}else{
			$(this).addClass('open-service');
		}
	
	});
	
	// Open & Close Projects
	
	$('.work-overlay').click(function(){
	
		$('.project').removeClass('open-project');
	
		var projectID = '#' + $(this).parent().parent().attr('data-project-id');
		var slideShowID = projectID + '-slideshow';
		
		if(!$(this).parent().parent().hasClass('open-project')){
			$(projectID).addClass('open-project');
			$(slideShowID).flexslider({ controlNav: false });
			
		}
		
		
		
		$('html,body').animate({
		scrollTop: $(projectID).offset().top
		}, 800);
		
	
	});
	
	$('.close-project').click(function(){
	
		$(this).closest('.project').removeClass('open-project');
		
		$('html,body').animate({
		scrollTop: $('#work-nav').offset().top - 90
		}, 800);
	
	});
	
	// Map Toggle
	
	$('#map-toggle').click(function(){
	
		if($('#contact-holder').hasClass('contact-fade')){
			$('#contact-holder').removeClass('contact-fade');
		}else{
			$('#contact-holder').addClass('contact-fade');
		}
	
	});
	
	
	// Contact Form Code
	
	$('#contact-form .btn-wrapper').click(function(){
	
		var name = $('#form-name').val();
		var email = $('#form-email').val();
		// var message = $('#form-message').val();
		var error = 0;
		
		if(name === '' || email === ''/* || message === ''*/){
			error = 1;
			$('#details-error').fadeIn(200);
		}else{
			$('#details-error').fadeOut(200);
		}
		
		if (!(/(.+)@(.+){2,}\.(.+){2,}/.test(email))) {
			$('#details-error').fadeIn(200);
			error = 1;
		}
		
		 var dataString = 'name=' + name + '&email=' + email; // + '&text=' + message;

            if (error === 0) {
                $.ajax({
                    type: "POST",
                    url: "mail.php",
                    data: dataString,
                    success: function () {
                        $('#details-error').fadeOut(1000);
                        $('#form-sent').fadeIn(1000);
                    }
                });
                return false;
            }
	
	});

	// open video link on modal open
	$('#myModal').on('open',function() {
		$('#video_frame').attr('src', video_link);
	});


	// highlight slider
	var currentSlide = 0;
	var count = $("#highlights-slider .slides li").length;

	function nextSlide() {
		var slider = $("#highlights-slider").data("flexslider");
		currentSlide++;
		if(currentSlide == count) {
			// fix bug with slide thru edge
			if(slider.animating) {
				currentSlide--;
				return false;
			}
			currentSlide = 0;
		}
		var $li = $("#highlights-slider .slides li").eq(currentSlide);
		$li.find("a").click();
		// console.log(Math.max(0,currentSlide - slider.visible + 1));
		if(currentSlide < slider.currentSlide || currentSlide > slider.currentSlide + slider.visible-1) {
			slider.flexAnimate(Math.max(0, currentSlide - slider.visible + 1));
			setTimeout();
		}

		/*
		if(slide) {
			clearTimeout(slide);
		}
		slide = setTimeout(nextSlide, timeout);
		*/
		return currentSlide;
	}

	function prevSlide() {
		var slider = $("#highlights-slider").data("flexslider");
		currentSlide--;
		if(currentSlide < 0) {
			// fix bug with slide thru edge
			if(slider.animating) {
				currentSlide++;
				return false;
			}
			currentSlide = count-1;
		}
		var $li = $("#highlights-slider .slides li").eq(currentSlide);
		$li.find("a").click();
		// console.log(Math.min(slider.pagingCount-1, currentSlide));
		if(currentSlide < slider.currentSlide || currentSlide > slider.currentSlide + slider.visible-1) {
			slider.flexAnimate(Math.min(slider.pagingCount-1, currentSlide));
		}

		/*
		if(slide) {
			clearTimeout(slide);
		}
		slide = setTimeout(nextSlide, timeout);
		*/
		return currentSlide;
	}

	// highlights
	$("#highlights .highlights-link").click( function(e) {
		e.preventDefault();
		var $mself = $(this);
		// TODO set currentSlide
		currentSlide = $("#highlights .highlights-link").index($(this));
		var id = $(this).attr("href");
		$("#highlights-text").html($(id).html());
		$(".selectbar").removeClass("active");
		$(this).siblings(".selectbar").addClass("active");
		if(slide) {
			clearTimeout(slide);
		}
		slide = setTimeout(nextSlide, timeout);
	});

	$(".my-prev").click(function(e) {
		e.preventDefault();
		prevSlide();
	});

	$(".my-next").click(function(e) {
		e.preventDefault();
		nextSlide();
	});

	/* scrolling for nav menu */
	// state: scrolled?
	var sc_state = 0;
	$(window).scroll( function() {
		var sc_limit = 390;
		var sc = $(window).scrollTop();
		if(sc >= sc_limit && sc_state == 0) {
			$("body").addClass("scrolled");
			sc_state = 1;
		} else if(sc < sc_limit && sc_state == 1) {
			$("body").removeClass("scrolled");
			sc_state = 0;
		}
	});
});


var timeout = 6000;
var slide = null;
// slide = setTimeout(nextSlide, timeout);