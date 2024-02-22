Logistics.Util = {

    Alerta : function(titulo, descricao) {

        const configuracaoTexto = {
            confirmButtonLabel: "OK",
            title: titulo,
            text: descricao
        };

        const configuracaoOpcoes = {
            height: 120,
            width: 200
        }

        XMLHttpRequest.Navigation.opnAlertDialog( configuracaoTexto, configuracaoOpcoes);
    }
}
