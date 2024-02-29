if (typeof (Logistics) === "undefined") Logistics = {};
if (typeof (Logistics.Conta) === "undefined") Logistics.Conta = {};

Logistics.Conta = {

    // Lista de nomes logicos das colunas
    Enumerador: {
        Coluna: {
            ColunaCNPJ:     "cr192_cnpj",
            ColunaCPF:      "cr192_cpf",
            ColunaName:     "name"
        }
    },

    // função chamada para validar CNPJ
    OnChangeCNPJ : function(context) {
        const formContext = context.getFormContext();
        let meuCNPJ = formContext.getAttribute(Logistics.Conta.Enumerador.Coluna.ColunaCNPJ).getValue();

        if (Logistics.Validator.VerificaCNPJ(meuCNPJ)) { // se CNPJ for valido
            // limpa os digitos
            let cc = meuCNPJ.replace(/[^\d]/g, '');
            // formata o CNPJ
            cc = cc.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
            formContext.getAttribute(Logistics.Conta.Enumerador.Coluna.ColunaCNPJ).setValue(cc);
        } else { // se CNPJ nao for valido
            formContext.getAttribute(Logistics.Conta.Enumerador.Coluna.ColunaCNPJ).setValue(null);
            Logistics.Util.Alerta("Atencao!", "CNPJ inválido!");
        }
    },

    // Funcao para formatar o nome da conta
    // A primeira letra de cada palavra deve ser maiuscula e todo o resto minuscula
    OnChangeName : function(context) {
        const formContext = context.getFormContext();
        let nomeConta = formContext.getAttribute(Logistics.Conta.Enumerador.Coluna.ColunaName).getValue();

        if (nomeConta) {
            let rgx = /(\b[a-z](?!\s))/g;
            let nomeFormatado = nomeConta.toLowerCase().replace(rgx, function(x) {return x.toUpperCase(); });
            formContext.getAttribute(Logistics.Conta.Enumerador.Coluna.ColunaName).setValue(nomeFormatado);
        }
    }

}
