$(function () {   

    $('#btnChangePassword').click(function () {
        editAccount();
    });

});

var editAccount = function () {
    var formData = new FormData($('form')[0]);

    $.ajax({
        type: 'POST',
        url: '/Customer/ChangePassword/',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.success) {
                showNotificationModal(data.message);
            }
            else showNotificationModal(data.message);
            
        },
        error: function (data) {
            showNotificationModal('Falha no servidor ao tentar editar seus dados, tente novamente mais tarde')

        }
    });
}

function showNotificationModal(message) {
    $('#notificationText').text(message);
    $('#notificationAnimalModal').modal('show');
}
