using System;
using System.Runtime.CompilerServices;

namespace ProJur.Business.Dto
{
    public class dtoBackupConfiguracaoFTP
    {

        public int idBackupConfiguracaoFTP { get; set; }

        public Nullable<DateTime> dataCadastro { get; set; }

        public Nullable<DateTime> dataUltimaAlteracao { get; set; }

        public string Host { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public bool Passivo { get; set; }

        public string caminhoHttp { get; set; }

    }
}
