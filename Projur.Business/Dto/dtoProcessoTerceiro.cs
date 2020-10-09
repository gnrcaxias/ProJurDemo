using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{

    public class dtoProcessoTerceiro
    {

        public int idProcessoTerceiro { get; set; }

        public int idProcesso { get; set; }

        public int idPessoaTerceiro { get; set; }

        public string Observacoes { get; set; }

    }

}
