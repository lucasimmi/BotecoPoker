$('#filtroCliente').on('click', function () {
    debugger;
    var nome = $('#Nome').val();
    var apelido = $('#Apelido').val();
    var codigo = $('#Codigo').val();
    var Pagina = $('#Pagina').val();
    $.ajax({
        type: "POST",
        URL: location.href,
        DataType: Pagina,
        Data: Pagina
    });
});

