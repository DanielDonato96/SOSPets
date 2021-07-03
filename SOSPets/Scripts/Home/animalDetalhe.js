$(function () {


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
                    console.log(data.message)
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
                else console.log('erro ' + data.message)
                
            },
            error: function (data)
            {
                console.log('Falha no servidor ao tentar salvar o animal, tente novamente mais tarde')
                
            }
        });

});
    
});

function deletarAnimal() {

    $.ajax({
        type: "POST",
        url: "/Home/DeleteAnimais/",
        data: { id: $('#animalID').val() },
        success: function (data) {
            if (data.success)
            {
                clearAnimalInputs();
                console.log('ok')
            }
            else {
                console.log('Falha na exclusão do registro');
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

