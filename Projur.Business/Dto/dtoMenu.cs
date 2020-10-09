using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProJur.Business.Dto
{
    public class dtoMenu
    {

        public int idMenu { get; set; }
        public int idMenuPai { get; set; }
        public string idChave { get; set; }

        public Nullable<DateTime> dataCadastro { get; set; }
        public Nullable<DateTime> dataUltimaAlteracao { get; set; }
        public int idUsuarioCadastro { get; set; }
        public int idUsuarioUltimaAlteracao { get; set; }
        public bool Visivel { get; set; }

        public string Descricao { get; set; }
        public string descricaoMenuPai { get; set; }
        public string Url { get; set; }

    }
}
