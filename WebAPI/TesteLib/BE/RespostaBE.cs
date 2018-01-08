using System;
using System.Collections.Generic;
using System.Text;

namespace TesteLib.BE
{
    public class RespostaBE
    {
        public bool Erro { get; set; }
        public List<string> Mensagem { get; set; }

        public static RespostaBE NovaResposta()
        {
            return new RespostaBE()
            {
                Mensagem = new List<string>(),
                Erro = false
            };
        }

    }
}
