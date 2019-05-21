$(document).ready(function () {
    $("#frmAnexo").on("change", "input[type='file']", function () {
        var vNomeArquivo = $(this).val().split("\\").pop();
        $(this).closest("td").children().eq(1).text(vNomeArquivo);
        $(this).closest("td").children().eq(3).val(vNomeArquivo);
    });

    $("#dlgExclusao").dialog({
        autoOpen: false,
        resizable: false,
        height: "auto",
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                var vLinha = $("#dlgID").text();
                $(".overlay").show();
                if ($(vLinha).closest("tr").children().eq(0).text() != "") {
                    $.ajax({
                        url: "../Home/ExcluirAnexo",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ pIdAnexo: $(vLinha).children().eq(0).text() }),
                        success: function (response) {
                            if (response.Resultado == "OK") {
                                $(vLinha).remove();
                            } else {
                                alert(vMsgErro + response.Resultado);
                            }
                        },
                        error: function (response) {
                            alert(vMsgErro + response.responseText);
                        },
                        complete: function () {
                            $(".overlay").hide();
                        }
                    });
                } else {
                    $(vLinha).closest("tr").remove();

                    if ($(vLinha).closest("table").children().length == 1) {
                        $(vLinha).closest("table").append("<tr><td colspan='6'>Não há anexos disponível.</td></tr>");
                    }

                    var vPrefixoModel = $("#txtModelAnexo").val();
                    for (var i = 0; i < $(".trAnexo").length; i++) {
                        var vLinha = $(".trAnexo").eq(i);
                        $(vLinha).closest("tr").children().eq(1).children().eq(2).attr("name", vPrefixoModel + "[" + i + "].File");
                        $(vLinha).closest("tr").children().eq(1).children().eq(2).attr("id", vPrefixoModel.replace(/\./, "_") + "_" + i);
                        $(vLinha).closest("tr").children().eq(1).children().eq(3).attr("name", vPrefixoModel + "[" + i + "].NmArquivo");
                    }
                }
                $(".overlay").hide();
                $(this).dialog("close");
            },

            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    }).prev(".ui-dialog-titlebar").css("background", "red").css("color", "white");
});

function fnSelecionarAnexo(vAnexo) {
    $(vAnexo).closest("td").children().eq(2).trigger("click");
}

function fnAddAnexo(vAba) {
    var vTabela = $(vAba).closest(".panel").find("tbody");

    var vIndex = $(".trAnexo").length;
    if (vTabela.children().eq(1).text() == "Não há anexos disponível.") {
        $(vTabela.children().eq(1)).remove();
    }

    vTabela.append($("#trModeloAnexo").clone().wrap("<div>").parent().html());

    var vNovaLinha = vTabela.children().eq(vTabela.children().length - 1);
    $(vNovaLinha).removeAttr("id");
    $(vNovaLinha).addClass("trAnexo");
    $(vNovaLinha).show();

    var vPrefixoModel = $("#txtModelAnexo").val();

    //Arquivo
    $(vNovaLinha).children().eq(1).children().eq(1).attr("name", vPrefixoModel + "[" + vIndex + "].File");
    $(vNovaLinha).children().eq(1).children().eq(1).removeAttr("readOnly");
    $(vNovaLinha).children().eq(1).children().eq(1).attr("id", vPrefixoModel.replace(/\./g, "_") + "_" + vIndex + "");
    $(vNovaLinha).children().eq(1).children().eq(2).attr("name", vPrefixoModel + "[" + vIndex + "].NmArquivo");

    $("#" + vPrefixoModel.replace(/\./g, "_") + "_" + vIndex).trigger("click");
}

function fnConfirmarExcluirAnexo(vAnexo) {
    var vDialog = $("#dlgExclusao").dialog();
    var vLinha = $(vAnexo).closest("tr");
    var vNomeArquivo = $(vLinha).children().eq(1);

    if ($(vLinha).children().eq(1).children().length > 0) {
        vNomeArquivo = $(vLinha).children().eq(1).children().eq(3).val();
    } else {
        vNomeArquivo = $(vNomeArquivo).text();
    }

    $("#dlgMensagem").html("Deseja realmente excluir o anexo " + vNomeArquivo + "?");
    $("#dlgID").html(vAnexo);

    vDialog.dialog("open");
}

function fnAbrirArquivo(vCampo) {
    var vAnexo = $(vCampo).closest("tr").children().eq(1).children().eq(2).get(0).files[0];

    if (vAnexo == undefined) {
        alert("Não há arquivo selecionado");
    } else {
        var vReader = new FileReader();
        vReader.readAsDataURL(vAnexo);
        vReader.onload = function (e) {
            var rawLog = vReader.result;
            var data = rawLog.substring(rawLog.indexOf("base64,") + 7);
            var fileName = vAnexo.name;
            var type = rawLog.substring(rawLog.indexOf("data:") + 5, rawLog.indexOf(";"));

            if (window.navigator && window.navigator.msSaveOrOpenBlob) { // IE workaround
                var byteCharacters = atob(data);
                var byteNumbers = new Array(byteCharacters.length);
                for (var i = 0; i < byteCharacters.length; i++) {
                    byteNumbers[i] = byteCharacters.charCodeAt(i);
                }
                var byteArray = new Uint8Array(byteNumbers);
                var blob = new Blob([byteArray], { type: type });
                window.navigator.msSaveOrOpenBlob(blob, fileName);
            }
            else { // much easier if not IE
                window.open("data:" + type + ";base64, " + data);
            }
        };
    }
}