$(function () {

    getCidades(true);

    $("#form_cadastro_animal").submit(function (form) {
        event.preventDefault();       

        var formData = new FormData($('form')[0]);

        $.ajax({
            type: 'POST',
            url: '/Home/SaveAnimal/',
            data: formData, 
            cache: false,
            contentType: false,
            processData: false,
            success: function (data)
            {
                if (data.success)
                {
                    showNotificationModal(data.message)
                    $('#img_animal').attr('src', data.FotoUrl);

                    if (data.newRecord)
                    {
                        $('#animalID').val(data.animalID);
                        $('.foto_animal_cadastrado').show();
                        $('.new_data_desaparecimento_container').hide();
                        $('.data_desaparecimento_readonly_container').show();
                        $('#data_desaparecimento_readonly').val(data.dataDesaparecimento);
                    }
                    
                }
                else showNotificationModal(data.message)
                
            },
            error: function (data)
            {
                showNotificationModal('Falha no servidor ao tentar salvar o animal, tente novamente mais tarde')
                
            }
        });

    });

    $('#estado').on('change', function () {
        getCidades();
    });

});

function getCidades(primeiraChamada = false) {

    $.ajax({
        type: "POST",
        url: "/Home/GetCidades/",
        data: { estadoID: $('#estado').val() },
        success: function (data) {
            if (data.success)
            {
                $('#cidade').html('');
                $(data.cidades).each(function (index, cidade) {
                    $('#cidade').append('<option value="' + cidade.CidadeID + '">' + cidade.Nome + '</option>');
                });

                if (primeiraChamada == true) {
                    setCidadeAtual();
                }

            }
            else
            {
                showNotificationModal('Falha ao recuperar Cidades');
            }
        }
    });

}

function deletarAnimal() {

    $.ajax({
        type: "POST",
        url: "/Home/DeleteAnimais/",
        data: { id: $('#animalID').val() },
        success: function (data) {
            if (data.success)
            {
                clearAnimalInputs();
                
            }
            else {
                showNotificationModal('Falha na exclusão do registro');
            }
        }
    });

}

function clearAnimalInputs() {

    $('#animalID').val(''); 
    $('#nome_animal').val('');
    $('#whatsapp').val('');

    $('.data_desaparecimento_readonly_container').hide();

    $('#img_animal').attr('src', '');
    $('#img_animal').attr('alt', 'Sem Foto');

    $('#btn_delete_animal').hide();
}

function setCidadeAtual() {

    //Quando o usuário entrar na página de um animal que já existe, a cidade atual é carregada 

    let cidade_atual = parseInt($('#cidade_atual').val());

    if (cidade_atual != undefined) {
        $('#cidade').val(cidade_atual).change();
    }

}

function showNotificationModal(message) {
    $('#notificationText').text(message);
    $('#notificationAnimalModal').modal('show');
}
