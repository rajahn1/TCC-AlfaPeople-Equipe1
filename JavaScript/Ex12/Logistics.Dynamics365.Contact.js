if (typeof Logistics === "undefined") Logistics = {};

if (typeof Logistics.Contact === "undefined") Logistics.Contact = {};

Logistics.Contact = {
  ...Logistics.Contact,
  OnCPFLoad: function (context) {
    var formContext = context.getFormContext();
    let cpf = formContext.getAttribute("cr192_cpf").getValue();
    if (cpf !== null) {
      if (Logistics.Util) {
        let getArrayOfCpfDigits =
          Logistics.Util.TransformStringInArrayDigits(cpf);
        if (!Logistics.Util.IsAValidCPF(getArrayOfCpfDigits)) {
          Logistics.Util.Alerta("Atenção!", "CPF inválido");
          formContext.getAttribute("cr192_cpf").setValue(null);
        }
      }
    }
  },
};
