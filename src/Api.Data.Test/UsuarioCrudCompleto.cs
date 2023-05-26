using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_realizar_CRUD_Usuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repositorio = new UserImplementation(context);
                UserEntity _entity = new UserEntity()
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var _registroCriado = await _repositorio.InsertAsync(_entity);

                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Email, _registroCriado.Email);
                Assert.Equal(_entity.Name, _registroCriado.Name);
                Assert.False(_registroCriado.Id == Guid.Empty);

                _entity.Name = Faker.Name.First();
                var _regAtualizado = await _repositorio.UpdateAsync(_entity);
                Assert.NotNull(_regAtualizado);
                Assert.Equal(_entity.Email, _regAtualizado.Email);
                Assert.Equal(_entity.Name, _regAtualizado.Name);

                var _regExiste = await _repositorio.ExistAsync(_regAtualizado.Id);
                Assert.True(_regExiste);

                var _regSelecionado = await _repositorio.SelectAsync(_regAtualizado.Id);
                Assert.NotNull(_regSelecionado);
                Assert.Equal(_regAtualizado.Email, _regSelecionado.Email);
                Assert.Equal(_regAtualizado.Name, _regSelecionado.Name);

                var _todosReg = await _repositorio.SelectAsync();
                Assert.NotNull(_todosReg);
                Assert.True(_todosReg.Count() >= 1);

                var _removeu = await _repositorio.DeleteAsync(_registroCriado.Id);
                Assert.True(_removeu);

                var _usuarioPadrao = await _repositorio.FindByLogin("adm@mail.com");
                Assert.NotNull(_usuarioPadrao);
                Assert.Equal("adm@mail.com", _usuarioPadrao.Email);
                Assert.Equal("Admin", _usuarioPadrao.Name);
            }
        }
    }
}
