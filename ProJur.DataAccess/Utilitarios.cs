using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.ComponentModel;

namespace ProJur.DataAccess
{
    public class Utilitarios
    {

        public static string AplicaFormatacaoData(object Texto)
        {
            string Retorno = String.Empty;

            if (Texto != null
                && Texto.ToString().Trim() != String.Empty)
            {
                DateTime dtResultado;

                if (DateTime.TryParse(Texto.ToString(), out dtResultado))
                {
                    Retorno = dtResultado.ToString("dd/MM/yyyy");
                }

            }

            return Retorno;
        }

        public static bool ColumnExists(SqlDataReader dr, string columnName)
        {
            bool existeColuna = false;

            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).ToUpper().Trim() == columnName.Trim().ToUpper())
                {
                    existeColuna = true;
                    break;
                }
            }

            return existeColuna;
        }

        public static string AplicarMascaraCPFCNPJ(object Texto, object especiePessoa)
        {
            if (Texto != null
                && Texto.ToString().Trim() != String.Empty
                && especiePessoa != null
                && especiePessoa.ToString().Trim() != String.Empty)
            {
                MaskedTextProvider oMascara = null;

                if (especiePessoa.ToString() == "F")
                    oMascara = new MaskedTextProvider(@"999\.999\.999\-99");
                else if (especiePessoa.ToString() == "J")
                    oMascara = new MaskedTextProvider(@"99\.999\.999\/9999\-99");

                oMascara.Set(Texto.ToString());

                return oMascara.ToString();
            }
            else
                return String.Empty;
        }

        public static string AplicarMascaraCPF(object Texto)
        {
            if (Texto != null
                && Texto.ToString().Trim() != String.Empty)
            {

                MaskedTextProvider oMascara = null;

                oMascara = new MaskedTextProvider(@"999\.999\.999\-99");

                oMascara.Set(Texto.ToString());

                return oMascara.ToString();
            }
            else
                return String.Empty;
        }

        public static string AplicarMascaraCNPJ(object Texto)
        {
            if (Texto != null
                && Texto.ToString().Trim() != String.Empty)
            {

                MaskedTextProvider oMascara = null;

                oMascara = new MaskedTextProvider(@"99\.999\.999\/9999\-99");

                oMascara.Set(Texto.ToString());

                return oMascara.ToString();
            }
            else
                return String.Empty;
        }


    }
}
