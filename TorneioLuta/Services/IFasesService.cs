using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioLuta.Services
{
    public interface IFasesService
    {
        List<LutadorModel> FaseDeGrupos(List<LutadorModel> grupo);
        List<LutadorModel> QuartasDeFinal(List<LutadorModel> vencedoresGrupo1, List<LutadorModel> vencedoresGrupo2,
                                          List<LutadorModel> vencedoresGrupo3, List<LutadorModel> vencedoresGrupo4);
        Tuple<List<LutadorModel>, List<LutadorModel>> Semifinais(List<LutadorModel> lutadores);

    }
}
