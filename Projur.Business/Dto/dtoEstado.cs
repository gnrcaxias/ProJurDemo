using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{    public class dtoEstado
    {

        public int idEstado { get; set; }

        public Nullable<DateTime> dataCadastro { get; set; }

        public int idUsuarioCadastro { get; set; }

        public Nullable<DateTime> dataUltimaAlteracao { get; set; }

        public int idUsuarioUltimaAlteracao { get; set; }

        public string siglaUF { get; set; }

        public string Descricao { get; set; }

        public string siglaUFDescricao
        {
            get
            {
                return siglaUF + " - " + Descricao;
            }
        }

    }}
