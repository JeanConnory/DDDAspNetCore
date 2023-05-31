using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test
{
    public class TesteLogin : BaseIntegration
    {
        [Fact]
        public async Task TesteDoToken()
        {
            await AdicionarToken();
        }
    }
}
