using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioLuta.Services
{
    public class LutaService : ILutaService
    {
        public bool VerificarGanhador(LutadorModel lutador1, LutadorModel lutador2)
        {
            if(lutador1.PctVitorias == lutador2.PctVitorias)
            {
                if (lutador1.QtdArtesMarciais == lutador2.QtdArtesMarciais)
                {
                    if (lutador1.TotalLutas > lutador2.TotalLutas)
                    {
                        lutador1.Vitorias++;
                        lutador1.TotalLutas++;
                        lutador2.Derrotas++;
                        lutador2.TotalLutas++;
                        return true;
                    }
                    else
                    {
                        lutador1.Derrotas++;
                        lutador1.TotalLutas++;
                        lutador2.Vitorias++;
                        lutador2.TotalLutas++;
                        return false;
                    }
                }
                else if(lutador1.QtdArtesMarciais > lutador2.QtdArtesMarciais)
                {
                    lutador1.Vitorias++;
                    lutador1.TotalLutas++;
                    lutador2.Derrotas++;
                    lutador2.TotalLutas++;
                    return true;
                }
                lutador1.Derrotas++;
                lutador1.TotalLutas++;
                lutador2.Vitorias++;
                lutador2.TotalLutas++;
                return false;
            }  
            else if(lutador1.PctVitorias > lutador2.PctVitorias)
            {
                lutador1.Vitorias++;
                lutador1.TotalLutas++;
                lutador2.Derrotas++;
                lutador2.TotalLutas++;
                return true;
            }
            lutador1.Derrotas++;
            lutador1.TotalLutas++;
            lutador2.Vitorias++;
            lutador2.TotalLutas++;
            return false;            
        }
    }
}
