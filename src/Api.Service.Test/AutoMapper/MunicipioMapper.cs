using System;
using System.Collections.Generic;
using System.Linq;
using Api.Domain.Dtos.Municipio;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class MunicipioMapper : BaseTesteService
    {
        [Fact(DisplayName = "É possível mapear os Modelos Municipio")]
        public void E_Possivel_Mappear_os_Modelos_Municipio()
        {
            var model = new MunicipioModel
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.City(),
                CodIbge = Faker.RandomNumber.Next(1, 10000),
                UfId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var listaEntity = new List<MunicipioEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new MunicipioEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.City(),
                    CodIbge = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    Uf = new UfEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)
                    }
                };
                listaEntity.Add(item);
            }

            //Model => Entity
            var entity = Mapper.Map<MunicipioEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.CodIbge, model.CodIbge);
            Assert.Equal(entity.UfId, model.UfId);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            //Entity => DTO
            var userDto = Mapper.Map<MunicipioDto>(entity);
            Assert.Equal(userDto.Id, entity.Id);
            Assert.Equal(userDto.Nome, entity.Nome);
            Assert.Equal(userDto.CodIbge, entity.CodIbge);
            Assert.Equal(userDto.UfId, entity.UfId);

            var municipioDtoCompleto = Mapper.Map<MunicipioDtoCompleto>(listaEntity.FirstOrDefault());
            Assert.Equal(municipioDtoCompleto.Id, listaEntity.FirstOrDefault().Id);
            Assert.Equal(municipioDtoCompleto.Nome, listaEntity.FirstOrDefault().Nome);
            Assert.Equal(municipioDtoCompleto.CodIbge, listaEntity.FirstOrDefault().CodIbge);
            Assert.Equal(municipioDtoCompleto.UfId, listaEntity.FirstOrDefault().UfId);
            Assert.NotNull(municipioDtoCompleto.Uf);

            var listaDto = Mapper.Map<List<MunicipioDto>>(listaEntity);
            Assert.True(listaDto.Count() == listaEntity.Count());
            for (int i = 0; i < listaDto.Count(); i++)
            {
                Assert.Equal(listaDto[i].Id, listaEntity[i].Id);
                Assert.Equal(listaDto[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listaDto[i].CodIbge, listaEntity[i].CodIbge);
                Assert.Equal(listaDto[i].UfId, listaEntity[i].UfId);
            }

            var munDtoCreateResult = Mapper.Map<MunicipioDtoCreateResult>(entity);
            Assert.Equal(munDtoCreateResult.Id, entity.Id);
            Assert.Equal(munDtoCreateResult.Nome, entity.Nome);
            Assert.Equal(munDtoCreateResult.CodIbge, entity.CodIbge);
            Assert.Equal(munDtoCreateResult.UfId, entity.UfId);
            Assert.Equal(munDtoCreateResult.CreateAt, entity.CreateAt);

            var munDtoUpdateResult = Mapper.Map<MunicipioDtoUpdateResult>(entity);
            Assert.Equal(munDtoUpdateResult.Id, entity.Id);
            Assert.Equal(munDtoUpdateResult.Nome, entity.Nome);
            Assert.Equal(munDtoUpdateResult.CodIbge, entity.CodIbge);
            Assert.Equal(munDtoUpdateResult.UfId, entity.UfId);
            Assert.Equal(munDtoUpdateResult.UpdateAt, entity.UpdateAt);

            //Dto para Model
            var userModel = Mapper.Map<MunicipioModel>(userDto);
            Assert.Equal(userModel.Id, userDto.Id);
            Assert.Equal(userModel.Nome, userDto.Nome);
            Assert.Equal(userModel.CodIbge, userDto.CodIbge);
            Assert.Equal(userModel.UfId, userDto.UfId);

            var userDtoCreate = Mapper.Map<MunicipioDtoCreate>(userModel);
            Assert.Equal(userDtoCreate.Nome, userModel.Nome);
            Assert.Equal(userDtoCreate.CodIbge, userModel.CodIbge);
            Assert.Equal(userDtoCreate.UfId, userModel.UfId);

            var userDtoUpdate = Mapper.Map<MunicipioDtoUpdate>(userModel);
            Assert.Equal(userDtoUpdate.Id, userModel.Id);
            Assert.Equal(userDtoUpdate.Nome, userModel.Nome);
            Assert.Equal(userDtoUpdate.CodIbge, userModel.CodIbge);
            Assert.Equal(userDtoUpdate.UfId, userModel.UfId);
        }
    }
}
