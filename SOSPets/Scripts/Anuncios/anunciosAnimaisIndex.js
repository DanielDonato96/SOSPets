var _PageSize = 20;

$(document).ready(function () {

    $('.lote-info-nav').clone().insertAfter('#content_list_lote');

    //$('.buttons-top .top-button').click(changeAba);


    //pega valor do atributo para paginação            
    //var pagina = getUrlParameter('pagina');

    //if (pagina == null) pagina = 1;
    //else pagina = parseInt(pagina);

    var pagina = 1;

    var start = (pagina - 1) * _PageSize;

    //Carrega abas
    loadListAnimais(start, _PageSize, true);
    //getLotesPorCidade();
    //$('#LoteEstado').change(getLotesPorCidade);
    //$('#LoteCidade').change(getLotesPorBairro);
});


var _xhr;

var loadListAnimais = function (start, limit, setPag) {

    var estadoID = getUrlParameter('estadoID');
    var cidadeID = getUrlParameter('cidadeID');

    //try {
    //    window.history.pushState({ url: "" + $(this).attr('href') + "" }, $(this).attr('title'), urlpart);
    //} catch (e) {
    //    console.log(e);
    //}


    if (_xhr && _xhr.readyState != 4) _xhr.abort();

    _xhr = $.ajax({
        url: '/Anuncios/AnunciosAnimaisListSearch', data: {
            //categoria: $('search#lot').attr('cat'),
            //subcategoria: $('search#lot').attr('sub'),
            //term: $('search#lot').attr('term'),
            start: start,
            limit: limit,
            //listaId: $('search#lot').attr('lista-id'),
            //slug: _cid = $('search#lot').attr('slug'),
            //buscaImovel: buscaImovel,
            //bairro: bairro,
            //segmento: segmento,
            estadoID: estadoID,      
            cidadeID: cidadeID          
        },
        type: 'get',
        beforeSend: function () {
            //$('#content_list_lote').contents().hide();
            //$(loaderHtml('loader_lote', getMessageLoad(), 'margin: 45px auto;')).appendTo('#content_list_lote');
            //$('#loader_lote').fadeIn(500);
        },
        success: function (html) {
            $('#content_list_lote').html(html);
            //$('#loader_lote').fadeOut(500, function () {
            //    $('#content_list_lote').html(html);
            //    if (setPag) {
            //        setPagging();
            //        $('.lote-info-nav').fadeIn(500);
            //    }
            //});

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