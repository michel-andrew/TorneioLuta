using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioLuta.Services
{
    public class FasesService : IFasesService
    {

        private readonly ILutaService _lutaService;
        public FasesService(ILutaService lutaService)
        {
            _lutaService = lutaService;
        }
        public List<LutadorModel> FaseDeGrupos(List<LutadorModel> grupo)
        {
            var vencedores = new List<LutadorModel>();
            var grupoTemp = grupo.ToList();

            foreach (LutadorModel lutador in grupo)
            {
                foreach (LutadorModel lutadorAdversario in grupoTemp)
                {
                    if (lutador.Id != lutadorAdversario.Id)
                    {
                        if (_lutaService.VerificarGanhador(lutador, lutadorAdversario))
                        {
                            lutador.Pontos++;
                        }
                        else
                        {
                            grupo.Where(x => x.Id == lutadorAdversario.Id).Select(x => x.Pontos++);
                        }
                    }

                }
                grupoTemp.Remove(lutador);
            }

            grupo.OrderByDescending(x => x.Pontos);

            while (vencedores.Count != 2)
            {
                vencedores.Add(grupo.First());
                grupo.Remove(grupo.First());
            }

            return vencedores;
        }

        public List<LutadorModel> QuartasDeFinal(List<LutadorModel> vencedoresGrupo1, List<LutadorModel> vencedoresGrupo2,
                                                            List<LutadorModel> vencedoresGrupo3, List<LutadorModel> vencedoresGrupo4)
        {
            var semifinalistas = new List<LutadorModel>();

            bool ganhador1 = _lutaService.VerificarGanhador(vencedoresGrupo1[0], vencedoresGrupo2[1]) ? true : false;
            bool ganhador2 = _lutaService.VerificarGanhador(vencedoresGrupo1[1], vencedoresGrupo2[0]) ? true : false;
            bool ganhador3 = _lutaService.VerificarGanhador(vencedoresGrupo3[0], vencedoresGrupo4[1]) ? true : false;
            bool ganhador4 = _lutaService.VerificarGanhador(vencedoresGrupo3[1], vencedoresGrupo4[0]) ? true : false;

            if (ganhador1)
            {
                semifinalistas.Add(vencedoresGrupo1[0]);
            }
            else
            {
                semifinalistas.Add(vencedoresGrupo2[1]);
            }

            if (ganhador2)
            {
                semifinalistas.Add(vencedoresGrupo1[1]);
            }
            else
            {
                semifinalistas.Add(vencedoresGrupo2[0]);
            }

            if (ganhador3)
            {
                semifinalistas.Add(vencedoresGrupo3[0]);
            }
            else
            {
                semifinalistas.Add(vencedoresGrupo4[1]);
            }

            if (ganhador4)
            {
                semifinalistas.Add(vencedoresGrupo3[1]);
            }
            else
            {
                semifinalistas.Add(vencedoresGrupo4[0]);
            }

            return semifinalistas;
        }

        public Tuple<List<LutadorModel>, List<LutadorModel>> Semifinais(List<LutadorModel> lutadores)
        {
            var finalistas = new List<LutadorModel>();
            var disputaTerceiro = new List<LutadorModel>();

            bool ganhador1 = _lutaService.VerificarGanhador(lutadores[0], lutadores[1]) ? true : false;
            bool ganhador2 = _lutaService.VerificarGanhador(lutadores[2], lutadores[3]) ? true : false;

            if (ganhador1)
            {
                finalistas.Add(lutadores[0]);
                disputaTerceiro.Add(lutadores[1]);
            }
            else
            {
                finalistas.Add(lutadores[1]);
                disputaTerceiro.Add(lutadores[0]);
            }

            if (ganhador2)
            {
                finalistas.Add(lutadores[2]);
                disputaTerceiro.Add(lutadores[3]);
            }
            else
            {
                finalistas.Add(lutadores[2]);
                disputaTerceiro.Add(lutadores[3]);
            }

            return Tuple.Create(finalistas, disputaTerceiro);
        }
    }
}
