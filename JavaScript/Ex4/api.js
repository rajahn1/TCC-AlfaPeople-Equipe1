// const getCEPData = (CEP) => {
//     const apiUrl = `https://viacep.com.br/ws/${CEP}/json/`;
//     fetch(apiUrl)
//     .then(response => {
//         if (!response.ok) {
//             throw new Error("Erro ao pegar resposta");
//         }
//         return response.json();
//     })
//     .then(data => {
//         console.log(data);
//     })
//     .catch(error => {
//         console.log(error.message);
//     });
// }
// module.exports = getCEPData;

async function getCEPData(CEP){
    const apiUrl = `https://viacep.com.br/ws/${CEP}/json/`;
    const response = await fetch(apiUrl);
    return response.json();
}

// (async () => {
//     const data = await getData(cep);
//     console.log(data);
// })();

module.exports = getCEPData;

