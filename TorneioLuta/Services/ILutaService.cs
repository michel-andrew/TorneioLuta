using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioLuta.Services
{
    public interface ILutaService
    {
        bool VerificarGanhador(LutadorModel lutador1, LutadorModel lutador2);
    }
}
