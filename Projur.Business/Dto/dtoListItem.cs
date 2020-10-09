using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ProJur.Business.Dto
{

    public class dtoListItem
    {

        public dtoListItem()
        {

        }

        public dtoListItem(string ValorChave, string Descricao)
        {
            this.ValorChave = ValorChave;
            this.Descricao = Descricao;
        }

        public string ValorChave { get; set; }

        public string Descricao { get; set; }

    }
}