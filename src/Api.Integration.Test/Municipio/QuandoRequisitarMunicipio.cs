using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.Municipio;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test.Municipio
{
    public class QuandoRequisitarMunicipio : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Municipio()
        {
            await AdicionarToken();

            var municipioDto = new MunicipioDtoCreate()
            {
                Nome = "Fortaleza",
                CodIbge = 123456,
                UfId = new Guid("953aec48-babc-4cea-9aaa-5b74ebb4f836")
            };

            //Post
            var response = await PostJsonAsync(municipioDto, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Fortaleza", registroPost.Nome);
            Assert.Equal(123456, registroPost.CodIbge);
            Assert.True(registroPost.Id != default(Guid));

            //GetAll
            response = await client.GetAsync($"{hostApi}municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaFromJson = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResult);
            Assert.NotNull(listaFromJson);
            Assert.True(listaFromJson.Count() > 0);
            Assert.True(listaFromJson.Where(r => r.Id == registroPost.Id).Count() == 1);

            var updateMunicipioDto = new MunicipioDtoUpdate()
            {
                Id = registroPost.Id,
                Nome = "Limeira",
                CodIbge = 3526902,
                UfId = new Guid("6f426888-069c-4de1-9b3e-59c8ef6b1786")
            };

            //PUT
            var stringContent = new StringContent(JsonConvert.SerializeObject(updateMunicipioDto),
                                    Encoding.UTF8, "application/json");
            response = await client.PutAsync($"{hostApi}Municipios", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroAtualizado = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Limeira", registroAtualizado.Nome);
            Assert.Equal(3526902, registroAtualizado.CodIbge);

            //GET Id
            response = await client.GetAsync($"{hostApi}Municipios/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionado = JsonConvert.DeserializeObject<MunicipioDto>(jsonResult);
            Assert.NotNull(registroSelecionado);
            Assert.Equal(registroSelecionado.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionado.CodIbge, registroAtualizado.CodIbge);

            //GET Complete/Id
            response = await client.GetAsync($"{hostApi}Municipios/Complete/{registroAtualizado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoCompleto = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(registroSelecionadoCompleto);
            Assert.Equal(registroSelecionadoCompleto.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionadoCompleto.CodIbge, registroAtualizado.CodIbge);
            Assert.NotNull(registroSelecionadoCompleto.Uf);
            Assert.Equal("São Paulo", registroSelecionadoCompleto.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoCompleto.Uf.Sigla);

            //GET byIBGE/CodIBGE
            response = await client.GetAsync($"{hostApi}Municipios/byIBGE/{registroAtualizado.CodIbge}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroSelecionadoIBGECompleto = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(registroSelecionadoIBGECompleto);
            Assert.Equal(registroSelecionadoIBGECompleto.Nome, registroAtualizado.Nome);
            Assert.Equal(registroSelecionadoIBGECompleto.CodIbge, registroAtualizado.CodIbge);
            Assert.NotNull(registroSelecionadoIBGECompleto.Uf);
            Assert.Equal("São Paulo", registroSelecionadoIBGECompleto.Uf.Nome);
            Assert.Equal("SP", registroSelecionadoIBGECompleto.Uf.Sigla);

            //DELETE
            response = await client.DeleteAsync($"{hostApi}Municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //GET ID depois do DELETE
            response = await client.GetAsync($"{hostApi}Municipios/{registroSelecionado.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
