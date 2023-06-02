using System;
using Api.Domain.Dtos.Uf;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoCompleto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public int CodIbge { get; set; }

        public Guid UdId { get; set; }

        public UfDto Uf { get; set; }
    }
}
