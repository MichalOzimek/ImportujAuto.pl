/*!
    * Start Bootstrap - SB UI Kit Pro v2.0.5 (https://shop.startbootstrap.com/product/sb-ui-kit-pro)
    * Copyright 2013-2023 Start Bootstrap
    * Licensed under SEE_LICENSE (https://github.com/BlackrockDigital/sb-ui-kit-pro/blob/master/LICENSE)
    */
window.addEventListener('DOMContentLoaded', event => {
    // Activate feather
    feather.replace();

    // Enable tooltips globally
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Enable popovers globally
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });

    // Activate Bootstrap scrollspy for the sticky nav component
    const navStick = document.body.querySelector('#navStick');
    if (navStick) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#navStick',
            offset: 82,
        });
    }

    // Collapse Navbar
    // Add styling fallback for when a transparent background .navbar-marketing is scrolled
    /*    var navbarCollapse = function () {
            const navbarMarketingTransparentFixed = document.body.querySelector('.navbar-marketing.bg-transparent.fixed-top');
            if (!navbarMarketingTransparentFixed) {
                return;
            }
            if (window.scrollY === 0) {
                navbarMarketingTransparentFixed.classList.remove('navbar-scrolled')
            } else {
                navbarMarketingTransparentFixed.classList.add('navbar-scrolled')
            }
    
        };*/
    // Collapse now if page is not at top
    navbarCollapse();
    // Collapse the navbar when page is scrolled
    document.addEventListener('scroll', navbarCollapse);

});

$(document).ready(function () {

    $('.counter').each(function () {
        $(this).prop('Counter', 0).animate({
            Counter: $(this).text()
        }, {
            duration: 4000,
            easing: 'swing',
            step: function (now) {
                $(this).text(Math.ceil(now));
            }
        });
    });

});

const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
        console.log(entry)
        if (entry.isIntersecting) {
            entry.target.classList.add('show');
        } else {
            entry.target.classList.remove('show');
        }
    });
});

const hiddenElements = document.querySelectorAll('.hidden');
hiddenElements.forEach((el) => observer.observe(el));

//ImportSteps

//next prev buttons
// Functionality for clicking on arrows
$(".carousel-control-prev").click(function () {
    var currentStep = $(".step.active");
    if (currentStep.prev(".step").length > 0) {
        currentStep.removeClass("active");
        currentStep.prev(".step").click();
    }
});

$(".carousel-control-next").click(function () {
    var currentStep = $(".step.active");
    if (currentStep.next(".step").length > 0) {
        currentStep.removeClass("active");
        currentStep.next(".step").click();
    }
});

// Steps progress
$(".step").click(function () {
    $(this).addClass("active").prevAll().addClass("active");
    $(this).nextAll().removeClass("active");
});

$(".step01").click(function () {
    $("#line-progress").css("width", "3%");
    $(".discovery").addClass("active").siblings().removeClass("active");
});

$(".step02").click(function () {
    $("#line-progress").css("width", "25%");
    $(".strategy").addClass("active").siblings().removeClass("active");
});

$(".step03").click(function () {
    $("#line-progress").css("width", "50%");
    $(".creative").addClass("active").siblings().removeClass("active");
});

$(".step04").click(function () {
    $("#line-progress").css("width", "75%");
    $(".production").addClass("active").siblings().removeClass("active");
});

$(".step05").click(function () {
    $("#line-progress").css("width", "100%");
    $(".analysis").addClass("active").siblings().removeClass("active");
});

