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
    $('#ModelDetalhesVenda').modal({
        show: true
    });
}