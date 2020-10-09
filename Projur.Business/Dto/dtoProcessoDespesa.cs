using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{

    public class dtoProcessoDespesa
    {

        public int idProcessoDespesa { get; set; }

        public int idProcesso { get; set; }

        public string Descricao { get; set; }

        public float Valor { get; set; }

        public string Observacoes { get; set; }

    }

}
