$('#meuForm').ready(function () {
    OpenModalValue();
});

function OpenModalValue() {
    var teste = $('#abreModal').val();
    if (teste == '666') {
        OpenModal();
    }
};

function OpenModal() {
    $('#ModalBuscaCliente').modal({
        show: true
    });
}

function CalculaTroco(saldo) {
    debugger;
    var valorTotal = document.getElementById("ValorTotal").value;
    //valorTotal = valorTotal.replace(',00', '');
    valorTotal = valorTotal.replace(',', '.');
    var dinheiro = document.getElementById("Dinheiro").value;
    if (dinheiro == '')
        dinheiro = '0';
    //dinheiro = dinheiro.replace(',00', '');
    dinheiro = dinheiro.replace(',', '.');
    dinheiro = (parseFloat(saldo) + parseFloat(dinheiro));
    if (dinheiro != valorTotal) {
        var ValTroco = (parseFloat(dinheiro) - parseFloat(valorTotal));
        var test = document.getElementById("teste");
        test.innerText = ValTroco;
        test.innerText = "Troco: R$ " + test.innerText.replace('.', ',');
        if (ValTroco > 0) {
            var trocoSaldo = document.getElementById("TrocoSaldo");
            trocoSaldo.removeAttribute("hidden");
        }
        else {
            var trocoSaldo = document.getElementById("TrocoSaldo");
            trocoSaldo.setAttribute("hidden", "hidden");
        }
    }
    if (dinheiro == valorTotal) {
        var div = document.getElementById("teste");
        div.innerHTML = "";
        var trocoSaldo = document.getElementById("TrocoSaldo");
        trocoSaldo.setAttribute("hidden", "hidden");
    }
}