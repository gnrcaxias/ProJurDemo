using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoUsuario
    {

        public int idUsuario { get; set; }
        public int idGrupoUsuario { get; set; }
        public int idPessoaVinculada { get; set; }

        public Nullable<DateTime> dataCadastro { get; set; }
        public Nullable<DateTime> dataUltimaAlteracao { get; set; }
        public int idUsuarioCadastro { get; set; }
        public int idUsuarioUltimaAlteracao { get; set; }
        public bool Bloqueado { get; set; }

        public string nomeCompleto { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }

        public bool Administrador { get; set; }
        public bool usuarioPadraoAgendamento { get; set; }

    }
}
