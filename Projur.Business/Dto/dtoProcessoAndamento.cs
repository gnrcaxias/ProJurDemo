using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoProcessoAndamento
    {

        public int idProcessoAndamento { get; set; }

        public int idProcesso { get; set; }

        public int idProcessoPeca { get; set; }

        public Nullable<DateTime> dataPublicacao { get; set; }

        public string Descricao { get; set; }

        public bool visivelCliente { get; set; }

    }
}
