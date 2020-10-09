using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoProcessoAdvogado
    {

        public int idProcessoAdvogado { get; set; }

        public int idProcesso { get; set; }

        public int idPessoaAdvogado { get; set; }

        public string tipoAdvogado { get; set; }

    }
}
