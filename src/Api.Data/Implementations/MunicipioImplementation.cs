using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class MunicipioImplementation : BaseRepository<MunicipioEntity>, IMunicipioRepository
    {
        private DbSet<MunicipioEntity> _dataset;

        public MunicipioImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<MunicipioEntity>();
        }

        public async Task<MunicipioEntity> GetCompleteByIbge(int codIbge)
        {
            return await _dataset.Include(m => m.Uf)
                    .FirstOrDefaultAsync(m => m.CodIbge == codIbge);
        }

        public async Task<MunicipioEntity> GetCompleteById(Guid id)
        {
            return await _dataset.Include(m => m.Uf)
                    .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
