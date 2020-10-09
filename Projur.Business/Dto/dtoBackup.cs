using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoBackup
    {

        public Nullable<DateTime> dataArquivo { get; set; }

        public string nomeArquivo { get; set; }

        public decimal tamanhoArquivo{ get; set; }

        public string caminhoHttp { get; set; }


    }
}
