using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.Uf;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Uf
{
    public class QuandoRequisitarUf : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Uf()
        {
            await AdicionarToken();

            //GetAll
            response = await client.GetAsync($"{hostApi}Ufs");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<UfDto>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() == 2);
            Assert.True(listaFromJson.Where(l => l.Sigla == "CE").Count() == 1);

            //GetById
            var id = listaFromJson.Where(l => l.Sigla == "CE").FirstOrDefault().Id;
            response = await client.GetAsync($"{hostApi}Ufs/{id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var regSelecionado = JsonConvert.DeserializeObject<UfDto>(jsonResult);
            Assert.NotNull(regSelecionado);
            Assert.Equal("Ceará", regSelecionado.Nome);
            Assert.Equal("CE", regSelecionado.Sigla);
        }
    }
}
