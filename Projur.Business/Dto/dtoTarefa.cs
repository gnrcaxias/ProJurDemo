using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoTarefa
    {

        public int idTarefa { get; set; }

        public Nullable<DateTime> dataCadastro { get; set; }

        public int idUsuarioCadastro { get; set; }

        public Nullable<DateTime> dataUltimaAlteracao { get; set; }

        public int idUsuarioUltimaAlteracao { get; set; }

        public int idProcesso { get; set; }

        public Nullable<DateTime> dataPrevisao { get; set; }

        public Nullable<DateTime> dataConclusao { get; set; }

        public string Descricao { get; set; }

        public int idUsuarioResponsavel { get; set; }

        public string situacaoTarefa { get; set; }

    }
}
