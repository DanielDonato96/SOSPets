$(function () {   

    $('#btnLogin').click(function () {
        editAccount();
    });

});

var editAccount = function () {
    var formData = new FormData($('form')[0]);

    $.ajax({
        type: 'POST',
        url: '/Home/Login/',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.success) {
                location.href = "/";

            }
            else showNotificationModal(data.message);
            
        },
        error: function (data) {
            showNotificationModal('Falha no servidor ao tentar acessar sua conta, tente novamente mais tarde')

        }
    });
}

function showNotificationModal(message) {
    $('#notificationText').text(message);
    $('#notificationAnimalModal').modal('show');
}
