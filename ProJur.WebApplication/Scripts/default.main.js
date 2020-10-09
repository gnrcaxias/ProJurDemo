function openModal(objectid, page) {

    var r = window.showModalDialog(page,
                '', 'fullscreen=no,menubar=no,directories=no,toolbar=no,location=no');

    if (r != null) {

        document.getElementById(objectid).value = r;

        __doPostBack('ctl00$ContentPlaceHolder1$' + objectid, '');

    }
}

function AtribuiEfeitoDivOpcoesCadastros() {
    /* Efeito panel cadastros */
    $("#cadastroFiltrosMaisOpcoesLink a").click(function (event) {

        event.preventDefault();

        if (!$("#cadastroOpcoesPesquisa").hasClass("selected")) {
            $.cookie('ck_cadastroOpcoesPesquisa', 'expandido');
            $("#cadastroOpcoesPesquisa").addClass("selected");
            $("#cadastroOpcoesPesquisa").slideDown("500");
        } else {
            $.cookie('ck_cadastroOpcoesPesquisa', 'recolhido');
            $("#cadastroOpcoesPesquisa").removeClass("selected");
            $("#cadastroOpcoesPesquisa").slideUp("500");
        };
    });
}

function InicializaDefaults() {

    if ($.cookie('ck_cadastroOpcoesPesquisa') == 'expandido') {
        $("#cadastroOpcoesPesquisa").addClass("selected");
        $("#cadastroOpcoesPesquisa").css("display", "block");
    }
    else if ($.cookie('ck_cadastroOpcoesPesquisa') == null
    || $.cookie('cadastroOpcoesPesquisa') == 'recolhido') {
        $("#cadastroOpcoesPesquisa").removeClass("selected");
        $("#cadastroOpcoesPesquisa").css("display", "none");
    }
}

function AtribuiEfeitoSelecioneTudoCheckBox() {

    $(function () {
        $('span.checkall input').on('click', function () {
            $(this).closest('table').find(':checkbox').prop('checked', this.checked);
        });
    });

}

function AtribuiCalendarioCamposData()
{
	$(".campoData").datepicker({
	    numberOfMonths: 1,
	    showButtonPanel: false,
	    dateFormat: 'dd/mm/yy',
	    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
	    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
	    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
	    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
	    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
	    nextText: 'Próximo',
	    prevText: 'Anterior',
	    showAnim: "drop"
	});
}

function AtribuiMascaraCampos()
{
    $('.campoData').mask('00/00/0000');
    $('.campoHora').mask('00:00');
    $('.campoCPF').mask('000.000.000-00');
    $('.campoCNPJ').mask('00.000.000/0000-00');
    $('.campoCEP').mask('00000-000');
    $('.campoDecimal').mask('000.000.000.000.000,00', { reverse: true });
}

function AtribuiEfeitosCampoTexto() {

    // TEXTBOX
    $('input[type="text"]').addClass("campoTextoIdle");

    $('input[type="text"]').focus(function () {
        $(this).removeClass("campoTextoIdle").addClass("campoTextoFocus");
    });

    $('input[type="text"]').blur(function () {
        $(this).removeClass("campoTextoFocus").addClass("campoTextoIdle");
    });

    // TEXBOX PASSWORD
    $('input[type="password"]').addClass("campoTextoIdle");

    $('input[type="password"]').focus(function () {
        $(this).removeClass("campoTextoIdle").addClass("campoTextoFocus");
    });

    $('input[type="password"]').blur(function () {
        $(this).removeClass("campoTextoFocus").addClass("campoTextoIdle");
    });

    // DROPDOWN
    $('select').addClass("dropDownIdle");

    $('select').focus(function () {
        $(this).removeClass("dropDownIdle").addClass("dropDownFocus");
    });

    $('select').blur(function () {
        $(this).removeClass("dropDownFocus").addClass("dropDownIdle");
    });

}

function RemoveAcentosSomenteMaiusculasInputs() {

    $('textarea').keydown(function (event) {

        if (((event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 128 && event.keyCode <= 163)) && !(event.shiftKey) && !(event.ctrlKey) && !(event.metaKey) && !(event.altKey)) {
            var $t = $(this);

            event.preventDefault();

            var char = String.fromCharCode(event.keyCode);

            char = char.replace(/[a]/g, 'a').replace(/[éèê]/g, 'e').replace(/[óòôõ]/g, 'o').replace(/[úùû]/g, 'u').replace(/[ÁÀÂÃ]/g, 'A').replace(/[ÉÈÊ]/g, 'E').replace(/[ÓÒÔÔ]/g, 'O').replace(/[ÚÙÛ]/g, 'U').replace(/[Ç]/g, 'C').replace(/[ç]/g, 'c').toUpperCase();
            var inicialStart = this.selectionStart;

            $t.val($t.val().slice(0, this.selectionStart) + char + $t.val().slice(this.selectionEnd));
            this.setSelectionRange(inicialStart + 1, inicialStart + 1);
        }

    });

    $('textarea').keypress(function (event) {

        if (event.charCode == 231 || event.charCode == 199) {
            var $t = $(this);

            event.preventDefault();

            var char = 'C';

            var inicialStart = this.selectionStart;

            $t.val($t.val().slice(0, this.selectionStart) + char + $t.val().slice(this.selectionEnd));
            this.setSelectionRange(inicialStart + 1, inicialStart + 1);
        }

    });

    $('input:text').keydown(function (event) {

        if (((event.keyCode >= 65 && event.keyCode <= 90) || (event.keyCode >= 128 && event.keyCode <= 163)) && !(event.shiftKey) && !(event.ctrlKey) && !(event.metaKey) && !(event.altKey)) {
            var $t = $(this);

            event.preventDefault();

            var char = String.fromCharCode(event.keyCode);

            char = char.replace(/[a]/g, 'a').replace(/[éèê]/g, 'e').replace(/[óòôõ]/g, 'o').replace(/[úùû]/g, 'u').replace(/[ÁÀÂÃ]/g, 'A').replace(/[ÉÈÊ]/g, 'E').replace(/[ÓÒÔÔ]/g, 'O').replace(/[ÚÙÛ]/g, 'U').replace(/[Ç]/g, 'C').replace(/[ç]/g, 'c').toUpperCase();
            var inicialStart = this.selectionStart;

            $t.val($t.val().slice(0, this.selectionStart) + char + $t.val().slice(this.selectionEnd));
            this.setSelectionRange(inicialStart + 1, inicialStart + 1);
        }

    });

    $('input:text').keypress(function (event) {

        if (event.charCode == 231 || event.charCode == 199) {
            var $t = $(this);

            event.preventDefault();

            var char = 'C';

            var inicialStart = this.selectionStart;

            $t.val($t.val().slice(0, this.selectionStart) + char + $t.val().slice(this.selectionEnd));
            this.setSelectionRange(inicialStart + 1, inicialStart + 1);
        }

    });

}

function LoadjQueryDefaults() {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
}

function EndRequestHandler() {
    InicializaDefaults();
    //AtribuiEfeitoDivOpcoesCadastros();
    AtribuiEfeitosCampoTexto();
    AtribuiEfeitoSelecioneTudoCheckBox();
    AtribuiCalendarioCamposData();
    AtribuiMascaraCampos();
    AtribuiEfeitoGridView();

    window.scrollTo = function () { }
}

$(document).ready(function () {
    InicializaDefaults();
    AtribuiEfeitoDivOpcoesCadastros();
    AtribuiEfeitosCampoTexto();
    AtribuiEfeitoSelecioneTudoCheckBox();
    AtribuiCalendarioCamposData();
    AtribuiMascaraCampos();

    //RemoveAcentosSomenteMaiusculasInputs();
})

function AtribuiEfeitoGridView() {

    $(".GridView tbody tr").hover(
        function () { $(this).addClass("Highlight"); },
        function () { $(this).removeClass("Highlight"); }
    );
}