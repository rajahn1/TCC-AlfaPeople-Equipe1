if (typeof(Logistics) === "undefined") Logistics = {};
if (typeof(Logistics.Conta) === "undefined") Logistics.Conta = {};
const getCEPData = require('./api.js');
Logistics.Conta = {
    OnChangeCEP: function(context) {
        const formContext = context.getFormContext();
        const CEPLabel = "address1_postalcode";
        const ruaLabel = "address1_line1";
        const cidadeLabel = "address1_city";
        const estadoLabel = "stateorprovince";

        const CEP = formContext.getAttribute(CEPLabel).getValue();
        console.log(CEP);
        (async() => {
            const data = await(getCEPData(CEP));
            console.log(data);
            const { cep, logradouro, bairro, localidade, uf } = data;

            formContext.getAttribute(CEPLabel).setValue(cep);
            formContext.getAttribute(ruaLabel).setValue(logradouro);
            formContext.getAttribute(cidadeLabel).setValue(localidade);
            formContext.getAttribute(estadoLabel).setValue(uf);
        })();
    }
}
