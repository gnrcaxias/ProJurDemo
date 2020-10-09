using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoProcesso
    {

        public int idProcesso  { get; set; }

        public Nullable<DateTime> dataCadastro { get; set; }

        public int idUsuarioCadastro { get; set; }

        public Nullable<DateTime> dataUltimaAlteracao { get; set; }

        public int idUsuarioUltimaAlteracao { get; set; }

        public string numeroProcesso { get; set; }

        public int idPessoaCliente { get; set; }

        public int idTipoAcao { get; set; }

        public string objetoAcao { get; set; }

        public int idVara { get; set; }

        public int idComarca { get; set; }

        public int idAreaProcessual { get; set; }

        public int idFaseAtual { get; set; }

        public int idSituacaoAtual { get; set; }

        public int idInstancia { get; set; }

        public float valorCausa { get; set; }

        public Nullable<DateTime> dataDistribuicao { get; set; }

        public Nullable<DateTime> dataBaixa { get; set; }

    }
}
