
/*!
    * Start Bootstrap - SB Admin v7.0.7 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2023 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
// 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});


//foto delete change tyle
$(document).ready(function () {
    $('.removePhoto').on('change', function () {
        var deleteIcon = $(this).siblings('label').find('.delete-photo-icon');
        if ($(this).is(':checked')) {
            $(this).closest('.photo').css('opacity', '0.3');
            deleteIcon.css({
                'background-color': '#dc3545',
                'color': '#fff',
            });
        } else {
            $(this).closest('.photo').css('opacity', '1');
            deleteIcon.css({
                'background-color': '',
                'color': '#dc3545',
            });
        }
    });
});


