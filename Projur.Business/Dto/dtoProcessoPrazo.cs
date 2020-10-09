using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoProcessoPrazo
    {

        public int idProcessoPrazo { get; set; }


        public Nullable<DateTime> dataCadastro { get; set; }

        public int idUsuarioCadastro { get; set; }

        public Nullable<DateTime> dataUltimaAlteracao { get; set; }

        public int idUsuarioUltimaAlteracao { get; set; }


        public int idProcesso { get; set; }

        public int idTipoPrazo { get; set; }

        public string Descricao { get; set; }

        public Nullable<DateTime> dataPublicacao { get; set; }

        public Nullable<DateTime> horaPublicacao
        {
            get
            {
                return this.dataPublicacao;
            }

            set
            {
                if (this.dataPublicacao != null)
                    this.dataPublicacao = Convert.ToDateTime(Convert.ToDateTime(dataPublicacao.ToString()).ToString("dd/MM/yyyy") + " " + Convert.ToDateTime(value).ToString("HH:mm"));
            }
        }

        public Nullable<DateTime> dataVencimento { get; set; }

        public Nullable<DateTime> horaVencimento
        {
            get
            {
                return this.dataVencimento;
            }

            set
            {
                if (this.dataVencimento != null)
                    this.dataVencimento = Convert.ToDateTime(Convert.ToDateTime(dataVencimento.ToString()).ToString("dd/MM/yyyy") + " " + Convert.ToDateTime(value).ToString("HH:mm"));
            }
        }

        public Nullable<DateTime> dataConclusao { get; set; }

        public int quantidadeDiasPrazo { get; set; }

        public string situacaoPrazo { get; set; }

        public int idPessoaAdvogadoResponsavel { get; set; }

    }
}
