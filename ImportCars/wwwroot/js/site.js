// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.owl-carousel').owlCarousel({
    loop: true,
    margin: 32,
    stagePadding: 32,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1,
            nav: true,
        },
        750: {
            items: 2,
            nav: true,
            navText: [
                //'<button class="carousel-control-prev" type="button" style="left: -204px">' +
                //'<span class="carousel-control-prev-icon" aria-hidden="true"></span>' +
                //'<span class="visually-hidden">Previous</span>' +
                //'</button>',
                //'<button class="carousel-control-next" type="button"style="right: -204px">' +
                //'<span class="carousel-control-next-icon" aria-hidden="true"></span>' +
                //'<span class="visually-hidden">Next</span>' +
                //'</button>'
            ],
        },
        1200: {
            items: 3,
            nav: true,
            loop: true,
            navText: [
                //'<button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleControls2" data-bs-slide="prev" style="left: -204px">' +
                //'<span class="carousel-control-prev-icon" aria-hidden="true"></span>' +
                //'<span class="visually-hidden">Previous</span>' +
                //'</button>',
                //'<button class="carousel-control-next" type="button" data-bs-target="#carouselExampleControls2" data-bs-slide="next" style="right: -204px">' +
                //'<span class="carousel-control-next-icon" aria-hidden="true"></span>' +
                //'<span class="visually-hidden">Next</span>' +
                //'</button>'
            ],
        }
    }
})

// contact form validation
function validateForm() {
    var name = document.getElementById('name').value;
    if (name == "") {
        document.querySelector('.status').innerHTML = "Name cannot be empty";
        return false;
    }
    var email = document.getElementById('email').value;
    if (email == "") {
        document.querySelector('.status').innerHTML = "Email cannot be empty";
        return false;
    } else {
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!re.test(email)) {
            document.querySelector('.status').innerHTML = "Email format invalid";
            return false;
        }
    }
    var subject = document.getElementById('subject').value;
    if (subject == "") {
        document.querySelector('.status').innerHTML = "Subject cannot be empty";
        return false;
    }
    var message = document.getElementById('message').value;
    if (message == "") {
        document.querySelector('.status').innerHTML = "Message cannot be empty";
        return false;
    }
    document.querySelector('.status').innerHTML = "Sending...";
}