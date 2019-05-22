

(function ($) {
    $.fn.focusTextToEnd = function(){
        this.focus();
        var $thisVal = this.val();
        this.val('').val($thisVal);
        return this;
    }
}(jQuery));


$(function () {


    $('#arquivo').on('change', function (e) {
        debugger
        var files = e.target.files;

        if (files.length > 0) {
            if (window.FormData !== undefined) {

                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }

            }
        }

    });


    $("#enviaChamado").click(function () {

        //Chamado.Descricao = $("#descricaoTexto").val();

        //var anexo = "teste";

        //var files = e.target.files;



        $.ajax({
            type: "POST",
            url: "Chamado/GravaChamado",
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                console.log(result);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });


        //var objChamado = {

        //    Motivo: Chamado.Motivo,
        //    EstagioProjeto: Chamado.EstagioProj,
        //    Sistema: Chamado.Sistema,
        //    Descricao: Chamado.Descricao,
        //    Files: files      
        //};


    })



    function handleDrop(e) {
        // this / e.target is current target element.

        if (e.stopPropagation) {
            e.stopPropagation(); // stops the browser from redirecting.
        }

        // See the section on the DataTransfer object.

        return false;
    }

    function handleDragEnd(e) {
        // this/e.target is the source node.

        [].forEach.call(cols, function (col) {
            col.classList.remove('over');
        });
    }

    var cols = document.querySelectorAll('.draggable');
    [].forEach.call(cols, function (col) {
        col.addEventListener('dragstart', handleDragStart, false);
        col.addEventListener('dragenter', handleDragEnter, false)
        col.addEventListener('dragover', handleDragOver, false);
        col.addEventListener('dragleave', handleDragLeave, false);
        col.addEventListener('drop', handleDrop, false);
        col.addEventListener('dragend', handleDragEnd, false);
    });

    $(document).on('click', '[data-iddiv]', function () {

        $("#" + $(this).attr('data-iddiv')).remove();
    });


});

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function MeusChamados() {
    $.ajax(
    {
        type: 'GET',
        url: '/Chamado/MeusChamados',
        dataType: 'html',
        cache: false,
        async: true,
        success: function (data) {
            $('#meusChamados').html(data);
        }
    });
}

var Chamado = {
    Motivo: "",
    EstagioProj: "",
    Sistema: "",
    Descricao: ""

}

function excluirHist(iddiv) {

    //preciso excluir a div inteira, botão, label e text
    $("#" + iddiv).remove();

}

$("#voltaMotivo").click(function () {
    $("#estagioProj").modal('hide');
    $("#inicioChamado").modal('show');
})

$("#voltaEstagioProj").click(function () {
    $("#goDescriChamado").modal('hide');
    $("#estagioProj").modal('show');
})

$("#voltaQualProj").click(function () {
    $("#goDescriChamado").modal('hide');
    $("#qualProj").modal('show');
})

$("#voltaDescricao").click(function () {

    $("#modalAnexos").modal('hide');
    $("#modalDescricaoChamado").modal('show');
})

$("#acoesDivList").hide();
$("#fimMultidescricao").hide();

$("#DropDAcoes").change(function () {
    
    //$("#acoesDivList").show();
   
    var x = $("#DropDAcoes option:selected").text();    

    var guid = uuidv4();

    $('#multidescricao')
        .append("<div class='drop-container clearfix' style='cursor: move' id='"
        + guid + "' class='row draggable' > <label class='col-sm-4 col-form-label' > <button type='button' class='btn btn-primary btn-xs'>"
        + x + "  </button> </label><div class='col-sm-8'><input class='form-control' id="
        + x.replace(/ /g, '') + " type='text' /> <button type='button' style='position: relative;top: -37px; left: -73px;' class='close' data-iddiv='"
        + guid + "'  aria-hidden='true' >&times; </button> </div>");

    //$('#multidescricao').append("<div draggable=true style='cursor: move' id='" + guid + "' class='row draggable' > <label class='col-sm-4 col-form-label' > <button type='button' class='btn btn-primary btn-xs'>" + x + "  </button> </label><div class='col-sm-8'><input class='form-control' id=" + x.replace(/ /g, '') + " type='text' /> <button type='button' style='position: relative;top: -37px; left: -73px;' class='close' data-iddiv='" + guid + "'  aria-hidden='true' >&times; </button> </div>");
    
    $('#multidescricao').sortable();

    $("#" + x.replace(/ /g, '')).focusTextToEnd();

    $("#fimMultidescricao").show();

    //$("#descricaoTexto").val($("#descricaoTexto").val() + $("div #multidescricao button").text() + ($("div #multidescricao input").val()));
    //debugger
    //var acoes = [];
    //$("[data-id]").each(function () {
    //    acoes.push($("button").text().join($("input").val()));

    //    $("#descricaoTexto").val(acoes);

    //});
    
    //$('#multidescricao')






    //if ("#descricaoTexto".length > 0) {
    //    $("#descricaoTexto").val($("#descricaoTexto").val() + ' ' + x);
    //}
    //else {
    //    $("#descricaoTexto").val(x);
    //}

    //$("#descricaoTexto").focusTextToEnd();



    //debugger
    //if (onclick("#multidescricao") && ("#multidescricao".length < 0)) {
    //    $("#fimMultidescricao").hide();
    //}
    //else {
    //    $("#fimMultidescricao").show();
    //}

})

$("#fimMultidescricao").click(function () {

    //quando o usuaário clicar, preciso achar uma forma de pegar
    //de cada linha, o texto do botão + o valor do input
    //e setar no campo descrição

    var inputs = [];

    $("div#multidescricao div.drop-container").each(function (item) {

        var id = item.id;

        var descbotao = item.children('button#' + id).text();
        var inputval = item.children('input#' + id).val();

        var combine = descbotao + " " + inputval;

        inputs.push(combine);

    });


    debugger
    $("#descricaoTexto").val(inputs.join(' '));


})

$("#btnAcoes div").click(function () {

    //$("#acoesDivList").show();
    debugger
    var x = $(this).attr('id');

    var guid = uuidv4();

    $('#multidescricao').append("<div class='drop-container clearfix' style='cursor: move' id='" + guid + "' class='row draggable' > <label class='col-sm-4 col-form-label' > <button type='button' class='btn btn-primary btn-xs'>" + x + "  </button> </label><div class='col-sm-8'><input class='form-control' id=" + x.replace(/ /g, '') + " type='text' /> <button type='button' style='position: relative;top: -37px; left: -73px;' class='close' data-iddiv='" + guid + "'  aria-hidden='true' >&times; </button> </div>");
     

    $('#multidescricao').sortable();

    $("#" + x.replace(/ /g, '')).focusTextToEnd();
 

    $("#fimMultidescricao").show();
})



//$("#lns").click(function () {

//    $("#descricaoTexto").val($('#lns').text());

//})

//$("#aob").click(function () {

//    if ($("#acoesDivList").hide = true) {
//        $("#acoesDivList").show();
//        //e pega o botão e add na div
//    }
//    else {
//        //pega o botão e add na div
//        alert('está show'); 
//    }
//    $("#descricaoTexto").val($('#aob').val());

//})

function motivoChamado(motivo) {

    Chamado.Motivo = motivo;

    $("#inicioChamado").modal('hide');
    $("#estagioProj").modal('show');

    if (Chamado.Motivo == "duvida_consulta") {
        $("#h5PMotivo").text($("#duvida_consulta").text());
    }
    else {
        $("#h5PMotivo").text($("#erro_problema").text());
    }

}

function estagioProj(estagio) {

    Chamado.EstagioProj = estagio;

    $("#estagioProjeto").modal('hide');
    $("#qualProj").modal('show');
       
    $("#h5MotivoInQj").text($("#h5PMotivo").text());

    if (Chamado.EstagioProj == "piloto_homol") {
        $("#h5PEstagProj").text($("#piloto_homol").text());
    }
    else {
        $("#h5PEstagProj").text($("#producao").text());
    }    
}

function descricao() {

    Chamado.Sistema = $('#SystemDropDownEscolha').val();
    $("#qualProj").modal('hide');
    $("#modalDescricaoChamado").modal('show');

    $("#h5MotivoInDescr").text($("#h5MotivoInQj").text());

    $("#h5EstagProjInDescr").text($("#h5PEstagProj").text());

    //$("#h5h5EstagProjInDescr").text($("#h5PEstagProj").text());

    $("#h5PProj").text(Chamado.Sistema);
}

function insereAnexo() {

    Chamado.Descricao = $('#descricaoTexto').val();

    $("#modalDescricaoChamado").modal('hide');
    $("#modalAnexos").modal('show');
}



var data = new FormData();



function ModalEditar() {

    $("#chamaModal").click(function (id) {
        $("#modalEditarChamado").modal("show");
    });
}

function EditarChamado(id) {
    $.ajax(
   {
       type: 'GET',
       url: '/Chamado/Edit',
       dataType: 'html',
       data: { id: id },
       cache: false,
       async: true,
       success: function (data) {

           $('#meusChamados').html(data);
       }
   });
}

function ChamadosPorFiltro() {
    $.ajax(
 {
     type: 'GET',
     url: '/Chamado/MeusChamados',
     dataType: 'html',
     data: { filtro: $('#filtro').val() },
     cache: false,
     async: true,
     success: function (data) {
         $('#meusChamados').html(data);
     }
 });

}



//$("#goDescricaoChamado").click(function () {

//    Chamado.Sistema = $('#SystemDropDownEscolha').val();
//    $("#qualProj").modal('hide');
//    $("#descricaoChamado").modal('show');


//    //qualProjeto(proj);
//})



//$('.approved').on('click', function () {

//    var taskid = $(this).parent().parent().attr('id');

//    $.ajax({
//        url: '/Chamado/MeusChamados',
//        type: 'GET',
//        data: { filtro: taskid },
//        success: function (data) {
//            window.location.reload();
//        }
//    });

//});

//$('#Filtro').click(function () {

//    var url = "/Chamado/MeusChamados/";
//    var infoBusca = $("#Filtro").val();
//    //var objChamado = {
//    //    motivo: $("#Motivo").val(),
//    //    sistema: $("#SystemDropDown").val(),
//    //    descricao: $("#Descricao").val()
//    //};
//    $.get(url, infoBusca, function (data) {
//    }
//    )
//})



//function EditarChamado() {
//    $.ajax(
//    {
//        type: 'GET',
//        url: '/Chamado/MeusChamados',
//        dataType: 'html',
//        cache: false,
//        async: true,
//        success: function (data) {
//            $('#divContent').html(data);
//        }
//    });
//}

//function ChamaModal() {
//    $("#chamaModal").click(function () {
//        $("#modalEditar").modal("show");
//    });
//}