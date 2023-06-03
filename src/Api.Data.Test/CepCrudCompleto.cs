using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class CepCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public CepCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de CEP")]
        [Trait("CRUD", "CepEntity")]
        public async Task E_Possivel_Realizar_CRUD_Cep()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repositorioMunicipio = new MunicipioImplementation(context);
                MunicipioEntity _entityMunicipio = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIbge = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("953aec48-babc-4cea-9aaa-5b74ebb4f836")
                };

                var _registroCriado = await _repositorioMunicipio.InsertAsync(_entityMunicipio);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entityMunicipio.Nome, _registroCriado.Nome);
                Assert.Equal(_entityMunicipio.CodIbge, _registroCriado.CodIbge);
                Assert.Equal(_entityMunicipio.UfId, _registroCriado.UfId);
                Assert.False(_entityMunicipio.Id == Guid.Empty);

                CepImplementation _repositorio = new CepImplementation(context);
                CepEntity _entityCep = new CepEntity
                {
                    Cep = "13.481-001",
                    Logradouro = Faker.Address.StreetName(),
                    Numero = "0 até 2000",
                    MunicipioId = _registroCriado.Id
                };

                var _registroCriadoCep = await _repositorio.InsertAsync(_entityCep);
                Assert.NotNull(_registroCriadoCep);
                Assert.Equal(_entityCep.Cep, _registroCriadoCep.Cep);
                Assert.Equal(_entityCep.Logradouro, _registroCriadoCep.Logradouro);
                Assert.Equal(_entityCep.Numero, _registroCriadoCep.Numero);
                Assert.Equal(_entityCep.MunicipioId, _registroCriadoCep.MunicipioId);
                Assert.False(_registroCriadoCep.Id == Guid.Empty);

                _entityCep.Logradouro = Faker.Address.StreetName();
                _entityCep.Id = _registroCriadoCep.Id;

                var _registroAtualizadoCep = await _repositorio.UpdateAsync(_entityCep);
                Assert.NotNull(_registroAtualizadoCep);
                Assert.Equal(_entityCep.Cep, _registroAtualizadoCep.Cep);
                Assert.Equal(_entityCep.Logradouro, _registroAtualizadoCep.Logradouro);
                Assert.Equal(_entityCep.Numero, _registroAtualizadoCep.Numero);
                Assert.Equal(_entityCep.MunicipioId, _registroAtualizadoCep.MunicipioId);
                Assert.True(_registroCriadoCep.Id == _entityCep.Id);

                var _registroExiste = await _repositorio.ExistAsync(_registroAtualizadoCep.Id);
                Assert.True(_registroExiste);

                var _registroSelecionadoCep = await _repositorio.SelectAsync(_registroAtualizadoCep.Id);
                Assert.NotNull(_registroSelecionadoCep);
                Assert.Equal(_registroAtualizadoCep.Cep, _registroSelecionadoCep.Cep);
                Assert.Equal(_registroAtualizadoCep.Logradouro, _registroSelecionadoCep.Logradouro);
                Assert.Equal(_registroAtualizadoCep.Numero, _registroSelecionadoCep.Numero);
                Assert.Equal(_registroAtualizadoCep.MunicipioId, _registroSelecionadoCep.MunicipioId);

                _registroSelecionadoCep = await _repositorio.SelectAsync(_registroAtualizadoCep.Cep);
                Assert.NotNull(_registroSelecionadoCep);
                Assert.Equal(_registroAtualizadoCep.Cep, _registroSelecionadoCep.Cep);
                Assert.Equal(_registroAtualizadoCep.Logradouro, _registroSelecionadoCep.Logradouro);
                Assert.Equal(_registroAtualizadoCep.Numero, _registroSelecionadoCep.Numero);
                Assert.Equal(_registroAtualizadoCep.MunicipioId, _registroSelecionadoCep.MunicipioId);
                Assert.NotNull(_registroSelecionadoCep.Municipio);
                Assert.Equal(_entityMunicipio.Nome, _registroSelecionadoCep.Municipio.Nome);
                Assert.NotNull(_registroSelecionadoCep.Municipio.Uf);
                Assert.Equal("CE", _registroSelecionadoCep.Municipio.Uf.Sigla);

                var _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                var _removeu = await _repositorio.DeleteAsync(_registroSelecionadoCep.Id);
                Assert.True(_todosRegistros.Count() == 0);

                _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 0);
            }
        }
    }
}
