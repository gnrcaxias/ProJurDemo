using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoAgendaHibrida
    {

        public int idAgendaHibrida { get; set; }

        public string Descricao { get; set; }

        public Nullable<DateTime> DataHoraInicio { get; set; }

        public Nullable<DateTime> DataHoraFim { get; set; }

        public string tipoAgendamento { get; set; }

        public string Responsaveis { get; set; }

        public string Status { get; set; }

        public int idCliente { get; set;  }

    }
}
