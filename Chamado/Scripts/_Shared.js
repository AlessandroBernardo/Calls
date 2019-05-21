var myInterval;
var vMsgErro = "Ocorreu um erro inesperado, favor avisar a Governança de TI.\n";

var vIdiomaDatatable =
{
    "sEmptyTable": "Nenhum registro encontrado",
    "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
    "sInfoFiltered": "(Filtrados de _MAX_ registros)",
    "sInfoPostFix": "",
    "sInfoThousands": ".",
    "sLengthMenu": "Mostrando _MENU_ resultados por página",
    "sLoadingRecords": "Carregando...",
    "sProcessing": "Processando...",
    "sZeroRecords": "Nenhum registro encontrado",
    "sSearch": "Pesquisar",
    "oPaginate": {
        "sNext": "Próximo",
        "sPrevious": "Anterior",
        "sFirst": "Primeiro",
        "sLast": "Último"
    },
    "oAria": {
        "sSortAscending": ": Ordenar colunas de forma ascendente",
        "sSortDescending": ": Ordenar colunas de forma descendente"
    }
};

jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-br-pre": function (a) {
        var x;
        if ($.trim(a) !== "") {
            var frDatea = $.trim(a).split(" ");
            var frTimea = (undefined != frDatea[1]) ? frDatea[1].split(":") : [00, 00, 00];
            var frDatea2 = frDatea[0].split("/");
            x = (frDatea2[2] + frDatea2[1] + frDatea2[0] + frTimea[0] + frTimea[1] + ((undefined != frTimea[2]) ? frTimea[2] : 0)) * 1;
        }
        else {
            x = -Infinity;
        }
        return x;
    },
    "date-br-asc": function (a, b) {
        return a - b;
    },
    "date-br-desc": function (a, b) {
        return b - a;
    }
});

$(document).ready(function () {
    $("form").on("submit", function () {
        $(".overlay").show();
    });

    $("input").attr("autocomplete", "off");
    fnExpiraSessao();
    $(".overlay").hide();
});

function fnExpiraSessao() {
    var vSessaoMinuto = $("#Session").val();
    $("#lblSession").html(lPad((vSessaoMinuto - vSessaoMinuto % 60) / 60, 2) + "h" + lPad(vSessaoMinuto % 60, 2));

    myInterval = setInterval(function () {
        vSessaoMinuto = vSessaoMinuto - 1;
        $("#lblSession").html(lPad((vSessaoMinuto - vSessaoMinuto % 60) / 60, 2) + "h" + lPad(vSessaoMinuto % 60, 2));

        if (vSessaoMinuto == 0) {
            //SALVAR RASCUNHO SE O SISTEMA POSSUIR ESSA FUNCIONALIDADE
            fnRedirecionar();
        }
    }, 1000 * 60);
}

function fnRedirecionar() {
    clearInterval(myInterval);
    alert("Sessão expirada,\nSerá redirecionado para login.");

    window.location = "../Home/Login";
}

function lPad(str, max) {
    if (str == undefined) {
        str = 0;
    }
    str = str.toString();
    return str.length < max ? lPad("0" + str, max) : str;
}

function rPad(str, max) {
    if (str == undefined) {
        str = 0;
    }
    str = str.toString();
    return str.length < max ? rPad(str + "0", max) : str;
}

function fnNumerico(vCampo) {
    $(vCampo).val($(vCampo).val().replace(/\D/g, ''));
}

function fnFormataDataHora(vData) {
    return lPad(vData.getDate(), 2) + "/" + lPad(vData.getMonth() + 1, 2) + "/" + vData.getFullYear() + " " + lPad(vData.getHours(), 2) + ":" + lPad(vData.getMinutes(), 2) + ":" + lPad(vData.getSeconds(), 2)
}