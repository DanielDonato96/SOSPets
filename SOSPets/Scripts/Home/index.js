var myIndex = 0;

$(function () {
    
    if ($('.carousel-item').length > 1) setCarouselPropagandas(); 
    getAnimaisEstados();
    $('#cmbEstado').change(getAnimaisCidades);

    $('#btnPesquisaAnimal').click(function () {
        searchAnimais();
    });

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

function getAnimaisEstados() {

    $.ajax({
        url: "/Home/GetAnimaisEstados",
        type: 'POST',
        success: function (data) {
            if (data.success) {
                $.each(data.estados, function (index, estado) {
                    $('#cmbEstado').append('<option value="' + estado.EstadoID + '" >' + estado.NomeEstado + ' (' + estado.QtdeAnimaisEstado + ') </option>');
                });
            }
        }

    })

}

function getAnimaisCidades() {

    let estadoID = $('#cmbEstado').val();

    if (estadoID != '0') {

        $.ajax({
            url: "/Home/GetAnimaisCidades",
            data: { estadoID },
            type: 'POST',           
            success: function (data) {
                if (data.success)
                {
                    $('#cmbCidades').html('<option value="0">Todas As Cidades</option>');
                    $.each(data.cidades, function (index, cidade) {
                        $('#cmbCidades').append('<option value="' + cidade.CidadeID + '" >' + cidade.NomeCidade + ' (' + cidade.QtdeAnimaisCidade + ') </option>');
                    });
                }
            }

        });
    }
    else
    {
        $('#cmbCidades').html('<option value="0">Todas As Cidades</option>');
    }

}

function searchAnimais() {

    let estadoID = $('#cmbEstado').val();
    let cidadeID = $('#cmbCidades').val();

    let searchUrl = '/Anuncios/AnunciosAnimaisIndex?pagina=1';
    if (estadoID != '0')
    {
        searchUrl += '&estadoID=' + estadoID;
        if (cidadeID != '0')
            searchUrl += '&cidadeID=' + cidadeID;
    }

    window.location.href = searchUrl;

}