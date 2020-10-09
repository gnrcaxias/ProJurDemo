using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProJur.Business.Dto
{
    public class dtoUsuarioPermissao
    {

        public int idPermissao { get; set; }
        public int idMenu { get; set; }
        public int idUsuario { get; set; }
        public int idUsuarioModerador { get; set; }

        public Nullable<DateTime> dataCadastro { get; set; }
        public Nullable<DateTime> dataUltimaAlteracao { get; set; }
        public int idUsuarioCadastro { get; set; }
        public int idUsuarioUltimaAlteracao { get; set; }

        public bool Exibir { get; set; }
        public bool Imprimir { get; set; }
        public bool Novo { get; set; }
        public bool Excluir { get; set; }
        public bool Alterar { get; set; }
        public bool Pesquisar { get; set; }
        public bool Especial { get; set; }

    }
}
