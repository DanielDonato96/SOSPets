$(function () {   

    $(document).on("click", ".btn-delete", function () {
        btnDeleteSelected = $(this);
    });

    $(document).on("click", ".btn-update", function () {
        var id = $(this).attr('data-ID');
        var nome = $("input[data-index='" + id + "']").val();
        salvarCategoria(id, nome);
    });

    $('.btn-save').on('click', function () {
        //Como o id é passado como zero um novo registro será inserido
        salvarCategoria(0, $('#nome_categoria').val());
    });

});

var btnDeleteSelected;

function deletarCategoria() {

    $.ajax({
        type: "POST",
        url: "/Home/DeleteAnimaisCategorias/",
        data: { id: btnDeleteSelected.attr('data-ID') },
        success: function (data)
        {
            if (data.success)
            {
                btnDeleteSelected.closest('.animal_categoria').remove();
            }
            else
            {
                console.log('Falha na exclusão do registro');
            }
        }
    });

}

function salvarCategoria(id, nome) {

    $.ajax({
        type: "POST",
        url: "/Home/SaveAnimaisCategorias/",
        data: { id, nome },
        success: function (data) {
            if (data.success) {
                if (id == 0)
                {
                    $('#nome_categoria').val('');
                    //Adiciona o html do novo registro na tela
                    $('#lista-categorias').append('<div class="animal_categoria">' +
                         '<div class="categoria_nome" data-ID="' + data.id + '">Categoria: ' + data.nome + '</div>' +
                         '<input data-index="' + data.id + '" class="nome_categoria_atualizar" type="text" name="nome_categoria_atualizar">' +
                         '<button class="btn-update" data-ID="' + data.id + '">Atualizar</button>' +
                         '<button class="btn-delete" data-ID="' + data.id + '" data-toggle="modal" data-target="#deleteAnimaisCategoriasConfirmation">Deletar</button>' +
                         '</div>');
                }
                else
                {
                    //Atualiza o nome exibido na tela
                    $("div .categoria_nome[data-ID='" + id + "']").text('Categoria: ' + data.nome);
                    $("input[data-index='" + id + "']").val('');
                }
            }
            else {
                console.log('Falha na adição do registro');
            }
        }
    });

}

function checarInputCategoria()
{
    if ($('#nome_categoria').val().length > 2) $('.btn-save').removeAttr('disabled');
    else $('.btn-save').prop("disabled", true);    
}