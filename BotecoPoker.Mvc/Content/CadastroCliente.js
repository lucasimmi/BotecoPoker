$('#CPF').keyup(function () {
    var tamanho = $("#CPF").val().length;
    var inputCpf = $("#CPF");

    if (tamanho <= 11) {
        inputCpf.mask("999.999.999-99");
    }
    if (inputCpf.val() == '..-')
        inputCpf.mask("");
});

$("#CPF").keydown(function () {
    if ($("#CPF").val() == '..-')
        $("#CPF").mask("");
});

$(function () {
    $('#RG').bind('keydown', soNums); // o "#input" é o input que vc quer aplicar a funcionalidade
});

function soNums(e) {

    //teclas adicionais permitidas (tab,delete,backspace,setas direita e esquerda)
    keyCodesPermitidos = new Array(8, 9, 37, 39, 46);

    //numeros e 0 a 9 do teclado alfanumerico
    for (x = 48; x <= 57; x++) {
        keyCodesPermitidos.push(x);
    }

    //numeros e 0 a 9 do teclado numerico
    for (x = 96; x <= 105; x++) {
        keyCodesPermitidos.push(x);
    }

    //Pega a tecla digitada
    keyCode = e.which;

    //Verifica se a tecla digitada é permitida
    if ($.inArray(keyCode, keyCodesPermitidos) != -1) {
        return true;
    }
    return false;
}

$('#Celular').keyup(function () {
    var tamanho = $("#Celular").val().length;
    if (tamanho <= 10) {
        $("#Celular").mask("(99)99999-9999");
    }
    if ($("#Celular").val() == '()-')
        $("#Celular").mask("");
});
$('#Celular').keydown(function () {
    if ($("#Celular").val() == '()-')
        $("#Celular").mask("");
});

$('#Telefone').keyup(function () {
    var tamanho = $("#Telefone").val().length;
    if (tamanho <= 10) {
        $("#Telefone").mask("(99)9999-9999");
    }
    if ($("#Telefone").val() == '()-')
        $("#Telefone").mask("");
});
$('#Telefone').keydown(function () {
    if ($("#Telefone").val() == '()-')
        $("#Telefone").mask("");
});