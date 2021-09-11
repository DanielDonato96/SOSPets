$(function () {   

    $('#btnEditAccount').click(function () {
        editAccount();
    });

});

var editAccount = function () {
    var formData = new FormData($('form')[0]);

    $.ajax({
        type: 'POST',
        url: '/Customer/EditAccount/',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.success) {
                showNotificationModal(data.message);
                $('form')[0].reset();

            }
            else showNotificationModal(data.message);
            
        },
        error: function (data) {
            showNotificationModal('Falha no servidor ao tentar alterar sua conta, tente novamente mais tarde')

        }
    });
}

function showNotificationModal(message) {
    $('#notificationText').text(message);
    $('#notificationAnimalModal').modal('show');
}
