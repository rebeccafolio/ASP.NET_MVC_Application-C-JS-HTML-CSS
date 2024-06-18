
$(function () {
    $('a.email').mouseover(function () {
        $(this).addClass("Email");
        prev = $(this).text();
        $(this).text("Email Vendor Contact");     
    }).mouseout(function () {
        $(this).removeClass("Email");
        $(this).text(prev)               
    });
})