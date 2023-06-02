using System;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public int CodIbge { get; set; }

        public Guid UdId { get; set; }
    }
}
