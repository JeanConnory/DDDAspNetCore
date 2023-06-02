using System;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Seeds
{
    public static class UfSeeds
    {
        public static void Ufs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UfEntity>().HasData(
                new UfEntity()
                {
                    Id = Guid.NewGuid(),
                    Sigla = "CE",
                    Nome = "Ceará",
                    CreateAt = DateTime.UtcNow
                },
                new UfEntity()
                {
                    Id = Guid.NewGuid(),
                    Sigla = "SP",
                    Nome = "São Paulo",
                    CreateAt = DateTime.UtcNow
                }
            );
        }
    }
}
