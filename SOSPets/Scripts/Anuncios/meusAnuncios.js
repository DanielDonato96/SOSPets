var _PageSize = 6;
var _usuarioID;

$(document).ready(function () {

    _usuarioID = $('#user-logged-id').text();

    $('.lote-info-nav').clone().insertAfter('#content_list_lote');

    loadListAnimais(1, _PageSize, true);

    $(document).on("click", ".pagination .page-navegator", function () {
        let page = $(this).attr('data-page');

        if (page != undefined && page > -1) {
            loadListAnimais(page, _PageSize, true);
        }
    });

    $(document).on("click", ".pagination .page-arrow-navegator", function () {
        let navegationType = $(this).attr('data-navegator');
        let currentPage = $('.pagination .active-page').attr('current-page');
        let newPage;

        if (navegationType == 'previous')
        {
            newPage = (parseInt(currentPage) - 1)
        }
        else
        {
            newPage = (parseInt(currentPage) + 1)
            var biggestNum = 0;
            $('.pagination .page-navegator').each(function () {
                var currentNum = parseInt($(this).attr('data-page'));
                if (currentNum > biggestNum) {
                    biggestNum = currentNum;
                }
            });
            if (newPage > biggestNum)
                return false;
        }
        
        if (newPage > 0) {
            loadListAnimais(newPage, _PageSize, true);
        }


    });

});


var _xhr;

var loadListAnimais = function (pagina, limit, setPag) {

    var start = (pagina - 1) * _PageSize;

    if (_xhr && _xhr.readyState != 4) _xhr.abort();

    _xhr = $.ajax({
        url: '/Anuncios/AnunciosAnimaisListSearch', data: {
            start: start,
            limit: limit,
            estadoID: 0,
            cidadeID: 0,
            usuarioID: _usuarioID
        },
        type: 'get',
        beforeSend: function () {
            $('#content_list_lote').contents().hide();
            $('#content_list_lote').html('<span> Aguarde o processamento </span>');
        },
        success: function (html) {
            $('#content_list_lote').html(html);
        }
    });
};

var _loaded = false;

var getMessageLoad = function () {
    if (_loaded) {
        return 'Carregando Animais...';
    } else {
        _loaded = true;
        return 'Pesquisando Animais...';
    }
};

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return typeof sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
    return false;
};