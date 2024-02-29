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

    // fun��o chamada para validar CNPJ
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
            Logistics.Util.Alerta("Atencao!", "CNPJ inv�lido!");
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
    },

    OnLoadOpportunity: function (context) {
        const formContext = context.getFormContext();
        const isIntegrated = formContext.getAttribute("log_integrado").getValue();
        const controls = formContext.ui.controls.get();
        if (isIntegrated) {
            for (let i in controls){
                controls[i].setDisabled(true);
            }
        }
    },

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
                return null;
            }
        } catch (error) {
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
    },

}
