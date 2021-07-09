var myIndex = 0;

$(function () {
    
    if ($('.carousel-item').length > 1) setCarouselPropagandas(); 
    getEstados();
});


function setCarouselPropagandas() {
    var i;
    var x = $(".carousel-item");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    myIndex++;
    if (myIndex > x.length) { myIndex = 1 }
    x[myIndex - 1].style.display = "block";
    setTimeout(setCarouselPropagandas, 5000);
}

function getEstados() {

    $.ajax({
        url: "/Home/GetAnimaisPorEstado",
        type: 'POST',
        success: function (data) {
            if (data.success) {
                $.each(data.estados, function (index, estado) {
                    $('#cmbEstado').append('<option value="' + estado.uf + '" >' + estado.NomeEstado + ' (' + estado.QtdeAnimaisEstado + ') </option>');
                });
            }
        }

    })

}