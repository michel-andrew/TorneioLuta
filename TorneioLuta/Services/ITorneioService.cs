using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioLuta.Services
{
    public interface ITorneioService
    {
        List<LutadorModel> ListarLutadores();
        Tuple<List<LutadorModel>, List<LutadorModel>, List<LutadorModel>, List<LutadorModel>> CriarGrupos(List<LutadorModel> lutadores);
        List<LutadorModel> IniciarTorneio();
    }
}
