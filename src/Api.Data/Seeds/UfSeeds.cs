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
                    Id = new Guid("953aec48-babc-4cea-9aaa-5b74ebb4f836"),
                    Sigla = "CE",
                    Nome = "Ceará",
                    CreateAt = DateTime.UtcNow
                },
                new UfEntity()
                {
                    Id = new Guid("6f426888-069c-4de1-9b3e-59c8ef6b1786"),
                    Sigla = "SP",
                    Nome = "São Paulo",
                    CreateAt = DateTime.UtcNow
                }
            );
        }
    }
}
