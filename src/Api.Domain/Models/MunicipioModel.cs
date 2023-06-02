using System;

namespace Api.Domain.Models
{
    public class MunicipioModel : BaseModel
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private int _codIbge;
        public int CodIbge
        {
            get { return _codIbge; }
            set { _codIbge = value; }
        }

        private Guid _ufId;
        public Guid UfId
        {
            get { return _ufId; }
            set { _ufId = value; }
        }
    }
}
