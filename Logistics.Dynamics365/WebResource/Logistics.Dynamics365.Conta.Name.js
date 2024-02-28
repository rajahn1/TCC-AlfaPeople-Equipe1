if (typeof (Logistics) === "undefined") Logistics = {};
if (typeof (Logistics.Conta) === "undefined") Logistics.Conta = {};
if (typeof (Logistics.Conta.Name) === "undefined") Logistics.Conta.Name = {};

Logistics.Conta.Name = {

    // Funcao para formatar o nome da conta
    // A primeira letra de cada palavra deve ser maiuscula e todo o resto minuscula
    OnChangeName : function(context) {
        const formContext = context.getFormContext();
        let nomeConta = formContext.getAttribute("name").getValue();

        if (nomeConta) {
            let rgx = /(\b[a-z](?!\s))/g;
            let nomeFormatado = nomeConta.toLowerCase().replace(rgx, function(x) {return x.toUpperCase(); });
            formContext.getAttribute("name").setValue(nomeFormatado);
        }
    }

}
