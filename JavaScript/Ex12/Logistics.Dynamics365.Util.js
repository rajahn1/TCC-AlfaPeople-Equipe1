if (typeof Logistics === "undefined") Logistics = {};

if (typeof Logistics.Util === "undefined") Logistics.Util = {};

Logistics.Util = {
  ...Logistics.Util,
  Alerta: function (titulo, descricao) {
    const configuracaoTexto = {
      confirmButtonLabel: "Ok",
      title: titulo,
      text: descricao,
    };
    const ConfiguracaoOpcoes = {
      height: 120,
      width: 200,
    };

    Xrm.Navigation.openAlertDialog(configuracaoTexto, ConfiguracaoOpcoes);
  },
  RemoveAllNonDigits: function (cpf) {
    const onlyDigitsRegex = /\D/g;
    return cpf.replace(onlyDigitsRegex, "");
  },
  TransformStringInArrayDigits: function (cpfString) {
    const array = [];
    const stringWithoutSpecialCharacters =
      Logistics.Util.RemoveAllNonDigits(cpfString);
    let totalQuantityOfCharacter = stringWithoutSpecialCharacters.length;
    for (let i = 0; i < totalQuantityOfCharacter; i++) {
      array.push(parseInt(stringWithoutSpecialCharacters[i], 10));
    }
    return array;
  },
  MultiplyArrayOfNumberPer11: function (numbers) {
    let position = 11;
    let accumulator = 0;
    for (let i = 0; i < 10; i++) {
      accumulator += numbers[i] * position;
      position--;
    }
    return accumulator;
  },
  MultiplyArrayOfNumberPer9: function (numbers) {
    let position = 10;
    let accumulator = 0;
    for (let i = 0; i < 9; i++) {
      accumulator += numbers[i] * position;
      position--;
    }
    return accumulator;
  },
  GetModuleOfNumberByEleven: function (number) {
    const remain = number % 11;
    if (remain < 2) {
      return 0;
    }
    return 11 - remain;
  },
  GetDeepCopyOfArray: function (arrayOfNumber) {
    const totalSize = arrayOfNumber.length;
    const copy = [];
    for (let i = 0; i < totalSize; i++) {
      copy.push(arrayOfNumber[i]);
    }
    return copy;
  },
  GetTheFirstChecksum: function (arrayOfNumber) {
    const arrayWithoutTwoElements = Logistics.Util.GetDeepCopyOfArray(
      arrayOfNumber
    ).slice(0, -2);
    const accumulator = Logistics.Util.MultiplyArrayOfNumberPer9(
      arrayWithoutTwoElements
    );
    const checkSum = Logistics.Util.GetModuleOfNumberByEleven(accumulator);
    return checkSum;
  },
  GetTheSecondChecksum: function (arrayOfNumber) {
    const arrayWithoutTwoElements = Logistics.Util.GetDeepCopyOfArray(
      arrayOfNumber
    ).slice(0, -1);
    const accumulator = Logistics.Util.MultiplyArrayOfNumberPer11(
      arrayWithoutTwoElements
    );
    const checkSum = Logistics.Util.GetModuleOfNumberByEleven(accumulator);
    return checkSum;
  },
  IsInvalidSample: function (cpf) {
    const firstNumber = cpf[0];
    let quantityEqual = 0;
    for (let i = 1; i < 10; i++) {
      if (firstNumber == cpf[i]) {
        quantityEqual++;
      }
    }
    return quantityEqual == 9;
  },
  IsAValidCPF: function (cpf) {
    if (cpf.length !== 11 || Logistics.Util.IsInvalidSample(cpf)) {
      return false;
    }
    const firstChecksum = Logistics.Util.GetTheFirstChecksum(cpf);
    const secondChecksum = Logistics.Util.GetTheSecondChecksum(cpf);
    return firstChecksum === cpf[9] && secondChecksum === cpf[10];
  },
};
