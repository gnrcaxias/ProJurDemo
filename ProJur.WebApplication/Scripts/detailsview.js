
$(document).ready(function () {

    $(".DetailsView tbody tr").hover(
            function () { $(this).addClass("Highlight"); },
            function () { $(this).removeClass("Highlight"); }
        );
});