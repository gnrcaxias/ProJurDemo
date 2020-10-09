
$(document).ready(function () {

    var maskHeight = $(document).height();
    var maskWidth = $(window).width();

    $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
    $('#mask').fadeTo("fast", 0.8);

    var winH = $(window).height();
    var winW = $(window).width();

    $('#login').css('top', winH / 2 - $('#login').height() / 2);
    $('#login').css('left', winW / 2 - $('#login').width() / 2);

    $('#login').fadeTo("slow", 1);

});