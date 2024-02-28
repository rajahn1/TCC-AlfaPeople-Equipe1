if (typeof (Logistics) === "undefined") Logistics = {};
if (typeof (Logistics.Conta) === "undefined") Logistics.Conta = {};
if (typeof (Logistics.Conta.CNPJ) === "undefined") Logistics.Conta.CNPJ = {};

Logistics.Conta.CNPJ = {

    // função chamada para validar CNPJ
    OnChangeCNPJ : function(context) {
        const formContext = context.getFormContext();
        let meuCNPJ = formContext.getAttribute("cr192_cnpj").getValue();

        if (Logistics.Validator.VerificaCNPJ(meuCNPJ)) { // se CNPJ for valido
            // limpa os digitos
            let cc = meuCNPJ.replace(/[^\d]/g, '');
            // formata o CNPJ
            cc = cc.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
            formContext.getAttribute("cr192_cnpj").setValue(cc);
        } else { // se CNPJ nao for valido
            formContext.getAttribute("cr192_cnpj").setValue(null);
            Logistics.Util.Alerta("Atencao!", "CNPJ inválido!");
        }

}
