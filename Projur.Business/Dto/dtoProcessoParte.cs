using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoProcessoParte
    {
            
        public int idProcessoParte { get; set; }
        
        public int idProcesso { get; set; }
        
        public int idPessoaParte { get; set; }

        public string tipoParte { get; set; }

    }
}
