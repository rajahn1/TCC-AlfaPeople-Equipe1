if (typeof (Logistics) === "undefined") Logistics = {};
if (typeof (Logistics.Util) === "undefined") Logistics.Util = {};

Logistics.Util = {

    Alerta : function(titulo, descricao) {
        var configuracaoTexto = {
            confirmButtonLabel: "OK",
            title: titulo,
            text: descricao
        };

        var configuracaoOpcoes = {
            height: 120,
            width: 200
        };
        // alert("configuracaoTexto: " + configuracaoTexto.text + "\n - configuracaoOpcoes: " + configuracaoOpcoes.width);

        Xrm.Navigation.openAlertDialog( configuracaoTexto, configuracaoOpcoes).then(
            function(success) {console.log("Alert dialog closed");},
            function(error) {console.log(error.message);}
        );
    }
}
