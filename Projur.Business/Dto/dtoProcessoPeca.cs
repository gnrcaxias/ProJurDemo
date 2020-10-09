using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    
    public class dtoProcessoPeca
    {

        public int idProcessoPeca { get; set; }

        public int idProcesso { get; set; }
        
        public string Descricao { get; set; }

        public string nomeArquivo { get; set; }

        public string caminhoArquivo { get; set; }
        
        public bool visivelCliente { get; set; }

        public int idCategoriaPecaProcessual { get; set; }

    }

}
