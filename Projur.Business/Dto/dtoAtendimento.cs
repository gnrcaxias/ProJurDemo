using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoAtendimento
    {

        public int idAtendimento { get; set; }

        public Nullable<DateTime> dataCadastro { get; set; }

        public int idUsuarioCadastro { get; set; }

        public Nullable<DateTime> dataUltimaAlteracao { get; set; }

        public int idUsuarioUltimaAlteracao { get; set; }

        public Nullable<DateTime> dataInicioAtendimento { get; set; }

        public Nullable<DateTime> dataFimAtendimento { get; set; }

        public string tipoAtendimento { get; set; }

        public string Detalhamento { get; set; }

        public int idUsuario { get; set; }

        public int idPessoaCliente { get; set; }

    }
}
