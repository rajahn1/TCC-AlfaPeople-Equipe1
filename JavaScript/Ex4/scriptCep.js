if (typeof(Logistics) === "undefined") Logistics = {};
if (typeof(Logistics.Conta) === "undefined") Logistics.Conta = {};

Logistics.Conta = {

    fetchCEPData: async function (CEP) {
        try {
            var xhttp = new XMLHttpRequest();
            xhttp.open("GET", `https://viacep.com.br/ws/${CEP}/json/`, false);
            xhttp.setRequestHeader("Content-type", "application/json");
            xhttp.send();

            if (xhttp.status >=200 && xhttp.status < 300) {
                var response = JSON.parse(xhttp.responseText);
                return response;
            } else {
                const alertStrings = { confirmButtonLabel: "Ok", text: "Erro ao buscar CEP", title: "Erro" };
                const alertOptions = { height: 120, width: 260 };
                Xrm.Navigation.openAlertDialog(alertStrings, alertOptions)
                return null;
            }
        } catch (error) {
            const alertStrings = { confirmButtonLabel: "Ok", text: `Erro ao fazer requisição com API externa VIACEP: ${error}`, title: "Erro" };
            const alertOptions = { height: 120, width: 260 };
            Xrm.Navigation.openAlertDialog(alertStrings, alertOptions)
            return null;
        }
    },

    OnChangeCEP: function(context) {
        const formContext = context.getFormContext();

        const schemaNames = {
            CEP: "address1_postalcode",
            estado: "address1_stateorprovince",
            cidade: "address1_city",
            logradouro: "log_logradouro",
            bairro: "log_bairro",
            complemento: "log_complemento",
            IBGE: "log_codigoibge",
            DDD: "log_ddd",
        };

        const CEPValue = formContext.getAttribute(schemaNames.CEP).getValue();

        (async() => {
            const CEPData = await(Logistics.Conta.fetchCEPData(CEPValue));
            
            if (!CEPData) {
                formContext.getAttribute(schemaNames.estado).setValue("");
                formContext.getAttribute(schemaNames.cidade).setValue("");
                formContext.getAttribute(schemaNames.logradouro).setValue("");
                formContext.getAttribute(schemaNames.bairro).setValue("");
                formContext.getAttribute(schemaNames.complemento).setValue("");
                formContext.getAttribute(schemaNames.IBGE).setValue("");
                formContext.getAttribute(schemaNames.DDD).setValue("");
            }
            else {
                const { cep, logradouro, localidade, uf, complemento, bairro, ibge, ddd } = CEPData;
                
                formContext.getAttribute(schemaNames.CEP).setValue(cep);
                formContext.getAttribute(schemaNames.estado).setValue(uf);
                formContext.getAttribute(schemaNames.cidade).setValue(localidade);
                formContext.getAttribute(schemaNames.logradouro).setValue(logradouro);
                formContext.getAttribute(schemaNames.bairro).setValue(bairro);    
                formContext.getAttribute(schemaNames.complemento).setValue(complemento);    
                formContext.getAttribute(schemaNames.IBGE).setValue(ibge);
                formContext.getAttribute(schemaNames.DDD).setValue(ddd);
            }
        })();
    }
}
