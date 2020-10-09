
$(document).ready(function () {
    $('a[id=modal]').click(function (e) {

        e.preventDefault();

        var id = $(this).attr('href');
        var maskHeight = $(document).height();
        var maskWidth = $(window).width();

        $('#mask').css({ 'width': maskWidth, 'height': maskHeight });
        $('#mask').fadeTo("fast", 0.8);

        //Get the window height and width
        var winH = $(window).height();
        var winW = $(window).width();

        $(id).css('top', winH / 2 - $(id).height() / 2);
        $(id).css('left', winW / 2 - $(id).width() / 2);

        $(id).fadeIn(1000);
    });
    $('.janela #btnFechar').click(function (e) {
        e.preventDefault();
        $('#mask').hide();
        $('.janela').hide();
        $('#modalDialog input[type="password"]').val('');
        $('.ErroValidacao').hide();
        $('.ValidationSummary').hide();

    });
    $('#mask').click(function () {
        $(this).hide();
        $('.janela').hide();
        $('#modalDialog input[type="password"]').val('');
        $('.ErroValidacao').hide();
        $('.ValidationSummary').hide();
    });
}); 
