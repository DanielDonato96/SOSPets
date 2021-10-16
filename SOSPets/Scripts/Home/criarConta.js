$(function () {   

    $('#btnEnter').click(function () {
        editAccount();
    });

});

var editAccount = function () {
    var formData = new FormData($('form')[0]);

    $.ajax({
        type: 'POST',
        url: '/Home/CreateAccount/',
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.success) {
                showNotificationModal(data.message);
                setTimeout(function () { location.href = "/Home/AcessarConta"; }, 2000);
            }
            else showNotificationModal(data.message);
            
        },
        error: function (data) {
            showNotificationModal('Falha no servidor ao tentar criar uma conta, tente novamente mais tarde')

        }
    });
}

function showNotificationModal(message) {
    $('#notificationText').text(message);
    $('#notificationAnimalModal').modal('show');
}
