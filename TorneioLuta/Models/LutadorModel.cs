    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioLuta
{
    public class LutadorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public List<string> ArtesMarciais { get; set; }
        public int TotalLutas { get; set; }
        public int Derrotas { get; set; }
        public int Vitorias { get; set; }
        public int PctVitorias { get; set; }
        public int QtdArtesMarciais { get; set; }
        public int Pontos { get; set; }
    }
}
