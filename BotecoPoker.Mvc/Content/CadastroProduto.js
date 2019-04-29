$('#Valor').keyup(function () {
    debugger;
    var tamanho = $("#Valor").val().length;
    if (tamanho >= 2) {
        $("#Valor").maskMoney();
    }
});
$('#ValorCompra').keyup(function () {
    debugger;
    var tamanho = $("#ValorCompra").val().length;
    if (tamanho >= 2) {
        $("#ValorCompra").maskMoney();
    }
});