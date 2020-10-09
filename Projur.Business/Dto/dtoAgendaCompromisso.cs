using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoAgendaCompromisso
    {

        public int idAgendaCompromisso { get; set; }

        public Nullable<DateTime> dataCadastro { get; set; }

        public int idUsuarioCadastro { get; set; }

        public Nullable<DateTime> dataUltimaAlteracao { get; set; }

        public int idUsuarioUltimaAlteracao { get; set; }

        public Nullable<DateTime> dataInicio { get; set; }

        public Nullable<DateTime> dataFim { get; set; }

        public Nullable<DateTime> horaInicio
        {
            get
            {
                return this.dataInicio;
            }

            set
            {
                if (this.dataInicio != null)
                    this.dataInicio = Convert.ToDateTime(Convert.ToDateTime(dataInicio.ToString()).ToString("dd/MM/yyyy") + " " + Convert.ToDateTime(value).ToString("HH:mm"));
            }
        }

        public Nullable<DateTime> horaFim
        {
            get
            {
                return this.dataFim;
            }

            set 
            {
                if (this.dataFim != null)
                    this.dataFim = Convert.ToDateTime(Convert.ToDateTime(dataFim.ToString()).ToString("dd/MM/yyyy") + " " + Convert.ToDateTime(value).ToString("HH:mm"));
            }
        }


        public string Descricao { get; set; }

        public string situacaoCompromisso { get; set; }

        public int idPessoa { get; set; }

    }
}
