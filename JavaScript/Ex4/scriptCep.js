if (typeof(Logistics) === "undefined") Logistics = {};
if (typeof(Logistics.Conta) === "undefined") Logistics.Conta = {};

Logistics.Conta = {

    GetCEPData: async function (CEP) {
        var xhttp = new XMLHttpRequest();
        xhttp.open("GET", `https://viacep.com.br/ws/${CEP}/json/`, false);
        xhttp.setRequestHeader("Content-type", "application/json");
        xhttp.send();
        var response = JSON.parse(xhttp.responseText);
        return response;
    },

    OnChangeCEP: function(context) {
        const formContext = context.getFormContext();

        const CEPLabel = "address1_postalcode";
        const ruaLabel = "address1_line1";
        const cidadeLabel = "address1_city";
        const estadoLabel = "address1_stateorprovince";

        const CEP = formContext.getAttribute(CEPLabel).getValue();

        (async() => {
            const data = await(Logistics.Conta.GetCEPData(CEP));
            if (!data) {
                formContext.getAttribute(ruaLabel).setValue("");
                formContext.getAttribute(cidadeLabel).setValue("");
                formContext.getAttribute(estadoLabel).setValue("");
            }
            else {
                const { cep, logradouro, localidade, uf } = data;
                formContext.getAttribute(CEPLabel).setValue(cep);
                formContext.getAttribute(ruaLabel).setValue(logradouro);
                formContext.getAttribute(cidadeLabel).setValue(localidade);
                formContext.getAttribute(estadoLabel).setValue(uf);
            }
        })();
    }
}
