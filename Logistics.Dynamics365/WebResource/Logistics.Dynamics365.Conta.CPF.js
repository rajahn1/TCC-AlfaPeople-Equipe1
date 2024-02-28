if (typeof (Logistics) === "undefined") Logistics = {};
if (typeof (Logistics.Conta) === "undefined") Logistics.Conta = {};
if (typeof (Logistics.Conta.CPF) === "undefined") Logistics.Conta.CPF = {};

Logistics.Conta.CPF = {

    // função para validar CPF
    OnChangeCPF : function (context) {
        const formContext = context.getFormContext();
        let meuCPF = formContext.getAttribute("cr192_cpf").getValue();

        if (Logistics.Validator.VerificaCPF(meuCPF)) { // se CPF for valido
            // limpa os digitos
            let cc = meuCPF.replace(/[^\d]+/g, '');
            // formata o CPF
            cc = cc.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4");
            formContext.getAttribute("cr192_cpf").setValue(cc);
        } else { // se CPF nao for valido
            formContext.getAttribute("cr192_cpf").setValue(null);
            Logistics.Util.Alerta("Atencao!", "CPF inválido!");
        }
    }

}
