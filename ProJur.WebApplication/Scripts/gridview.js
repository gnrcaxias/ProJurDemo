
$(document).ready(function () {

    AtribuiEfeitoGridView();
});

function AtribuiEfeitoGridView() {

    $(".GridView tbody tr").hover(
        function () { $(this).addClass("Highlight"); },
        function () { $(this).removeClass("Highlight"); }
    );

    $(".GridView tbody tr + tr").hover(
    function () { $(this).addClass("Highlight"); },
    function () { $(this).removeClass("Highlight"); }
);
}