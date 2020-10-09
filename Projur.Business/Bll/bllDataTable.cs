using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ProJur.DataAccess;
using System.Data;
using System.ComponentModel;
using ProJur.Business.Dto;

namespace ProJur.Business.Bll
{

    [DataObject(true)]
    public static class bllDataTable
    {

        public static List<dtoListItem> GetTipoPessoa()
        {
            List<dtoListItem> lstTipoPessoa = new List<dtoListItem>();

            lstTipoPessoa.Add(new dtoListItem("tipoPessoaColaborador", "Colaborador"));
            lstTipoPessoa.Add(new dtoListItem("tipoPessoaCliente", "Cliente"));
            lstTipoPessoa.Add(new dtoListItem("tipoPessoaParte", "Parte"));
            lstTipoPessoa.Add(new dtoListItem("tipoPessoaAdvogado", "Advogado"));
            lstTipoPessoa.Add(new dtoListItem("tipoPessoaTerceiro", "Terceiro"));

            return lstTipoPessoa;
        }

        public static dtoListItem GetTipoPessoa(string valorChave)
        {
            return null;
        }


    }

}
