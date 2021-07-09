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

    //var buscaImovel = false;
    var estadoID = 0;
    var cidadeID = 0;
    //var segmento = '';
    //var bairro = '';

    //var url = $(location).attr('href');
    //var imovelParamsIndex = url.indexOf('imoveis/');
    //if (imovelParamsIndex > -1) {
    //    buscaImovel = true;
    //    var imovelParams = url.substring(imovelParamsIndex, (url.length - 1));
    //    var paramsArray = imovelParams.split('/');

    //    estado = paramsArray[1];
    //    cidade = paramsArray[2];
    //    bairro = paramsArray[3];
    //    segmento = paramsArray[4];
    //}

    //var urlpart = "?pagina=" + Math.ceil(start / limit + 1);

    try {
        window.history.pushState({ url: "" + $(this).attr('href') + "" }, $(this).attr('title'), urlpart);
    } catch (e) {
        console.log(e);
    }


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