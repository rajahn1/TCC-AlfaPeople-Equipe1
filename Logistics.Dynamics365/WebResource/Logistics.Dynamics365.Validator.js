if (typeof (Logistics) === "undefined") Logistics = {};
if (typeof (Logistics.Validator) === "undefined") Logistics.Validator = {};

Logistics.Validator = {

    // recebe um string com o CNPJ e retorna true se for valido
    VerificaCNPJ: function(cnpj) {
        // constantes para calculo do modulo 11
        var b = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        // transforma caracteres de digitos numericos em um array de inteiros
        var c = String(cnpj).replace(/[^\d]/g, '');
        // rejeita numeros com tamanho incorreto
        if (c.length !== 14) return false;
        // rejeita 00000000000000
        if (/0{14}/.test(c)) return false;
        // calcula e verifica o primeiro digito verificador
        for (var i = 0, n = 0; i < 12; n += c[i] * b[++i]);
        if (c[12] != (((n %= 11) < 2) ? 0 : 11 - n)) return false;
        // calcula e verifica o segundo digito verificador
        for (var i = 0, n = 0; i <= 12; n += c[i] * b[i++]);
        if (c[13] != (((n %= 11) < 2) ? 0 : 11 - n)) return false;
        // retorna true caso passe por todas as verificacoes
        return true;
    },

    // recebe um string com o CPF e retorna true se for valido
    VerificaCPF: function(cpf) {
        // transforma caracteres de digitos numericos em um array de inteiros
        var c = String(cpf).replace(/[^\d]+/g, '');
        // rejeita numeros com tamanho incorreto
        if (c.length !== 11) return false;
        // rejeita caso seja sequencia de digitos iguais
        if (/^(\d)\1*$/.test(c)) return false;
        // calcula e verifica o primeiro digito verificador
        let dv1 = 0;
        for (let i = 0; i < 9; i++) { dv1 += (10 - i) * parseInt(c.charAt(i)); }
        dv1 = (dv1 % 11) % 10;
        dv1 = (dv1 < 2) ? 0 : 11 - dv1;
        if (dv1 != parseInt(c.charAt(9))) return false;
        // calcula e verifica o segundo digito verificador
        let dv2 = 0;
        for (let i = 0; i < 10; i++) { dv2 += (11 - i) * parseInt(c.charAt(i)); }
        dv2 = (dv2 % 11) % 10;
        dv2 = (dv2 < 2) ? 0 : 11 - dv2;
        if (dv2 != parseInt(c.charAt(10))) return false;
        // retorna true caso passe por todas as verificacoes
        return true;
    }

}

