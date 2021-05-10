using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TorneioLuta.Services
{
    public class TorneioService : ITorneioService
    {
        private readonly ILutaService _lutaService;
        private readonly IFasesService _fasesService;
        public TorneioService(ILutaService lutaService, IFasesService fasesService)
        {
            _lutaService = lutaService;
            _fasesService = fasesService;
        }

        public List<LutadorModel> ListarLutadores()
        {
            List<LutadorModel> lutadores = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-api-key", "e07cb70a-7e83-486b-b88d-7b2b180d7299");
                client.BaseAddress = new Uri("https://apidev-mbb.t-systems.com.br:8443/edgemicro_tsdev/torneioluta/api/competidores");
                var responseTask = client.GetAsync("competidores");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var lista = result.Content.ReadAsAsync<List<LutadorModel>>();
                    lista.Wait();
                    lutadores = lista.Result;
                }
                else
                {
                    throw new Exception("Não foi possível obter a lista de lutadores");
                }
            }

            foreach (var lutador in lutadores)
            {
                lutador.TotalLutas = lutador.Vitorias + lutador.Derrotas;
                lutador.PctVitorias = (int)Math.Round((double)((double)lutador.Vitorias / (double)lutador.TotalLutas) * 100);
                lutador.QtdArtesMarciais = lutador.ArtesMarciais.Count;
            }
            return lutadores.ToList();
        }

        public Tuple<List<LutadorModel>, List<LutadorModel>, List<LutadorModel>, List<LutadorModel>> CriarGrupos(List<LutadorModel> lutadores)
        {

            var grupo1 = new List<LutadorModel>();
            var grupo2 = new List<LutadorModel>();
            var grupo3 = new List<LutadorModel>();
            var grupo4 = new List<LutadorModel>();

            var lutadoresOrdenados = lutadores.OrderBy(lutadores => lutadores.Idade);

            foreach (LutadorModel lutador in lutadoresOrdenados)
            {
                if (grupo1.Count < 5)
                {
                    grupo1.Add(lutador);
                }
                else if (grupo1.Count == 5 && grupo2.Count < 5)
                {
                    grupo2.Add(lutador);
                }
                else if (grupo2.Count == 5 && grupo3.Count < 5)
                {
                    grupo3.Add(lutador);
                }
                else if (grupo3.Count == 5 && grupo4.Count < 5)
                {
                    grupo4.Add(lutador);
                }
            }
            return Tuple.Create(grupo1, grupo2, grupo3, grupo4);
        }

        public List<LutadorModel> IniciarTorneio()
        {
            var lutadores = ListarLutadores();
            var grupos = CriarGrupos(lutadores);

            var vencedoresGrupo1 = _fasesService.FaseDeGrupos(grupos.Item1);
            var vencedoresGrupo2 = _fasesService.FaseDeGrupos(grupos.Item2);
            var vencedoresGrupo3 = _fasesService.FaseDeGrupos(grupos.Item3);
            var vencedoresGrupo4 = _fasesService.FaseDeGrupos(grupos.Item4);

            var podio = new List<LutadorModel>();

            var semifinalistas = _fasesService.QuartasDeFinal(vencedoresGrupo1, vencedoresGrupo2, vencedoresGrupo3, vencedoresGrupo4);

            var finalistas = _fasesService.Semifinais(semifinalistas);

            var final = _lutaService.VerificarGanhador(finalistas.Item1.First(), finalistas.Item1.Last());

            var terceiroLugar = _lutaService.VerificarGanhador(finalistas.Item2.First(), finalistas.Item2.Last());

            if (final)
            {
                podio.Add(finalistas.Item1.First());
                podio.Add(finalistas.Item1.Last());
            }
            else
            {
                podio.Add(finalistas.Item1.Last());
                podio.Add(finalistas.Item1.First());
            }

            if (terceiroLugar)
            {
                podio.Add(finalistas.Item2.First());
            }
            else
            {
                podio.Add(finalistas.Item2.Last());
            }
            return podio;
        }
    }
} 

