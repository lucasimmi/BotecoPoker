$('#BuyIn').keyup(function () {
    var tamanho = $("#BuyIn").val().length;
    if (tamanho >= 2) {
        $("#BuyIn").maskMoney();
    }
});
$('#ReBuy').keyup(function () {
    var tamanho = $("#ReBuy").val().length;
    if (tamanho >= 2) {
        $("#ReBuy").maskMoney();
    }
});
$('#Addon').keyup(function () {
    var tamanho = $("#Addon").val().length;
    if (tamanho >= 2) {
        $("#Addon").maskMoney();
    }
});
$('#Jantar').keyup(function () {
    var tamanho = $("#Jantar").val().length;
    if (tamanho >= 2) {
        $("#Jantar").maskMoney();
    }
});
$('#Jackpot').keyup(function () {
    var tamanho = $("#Jackpot").val().length;
    if (tamanho >= 2) {
        $("#Jackpot").maskMoney();
    }
}); 
$('#TaxaAdm').keyup(function () {
    var tamanho = $("#TaxaAdm").val().length;
    if (tamanho >= 2) {
        $("#TaxaAdm").maskMoney();
    }
});