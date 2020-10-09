using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using ProJur.Business.Dto;
using InfoVillage.DevLibrary;
using System.Collections;

namespace ProJur.Business.Bll
{

    [DataObject(true)]
    public class bllAgendaHibrida
    {

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static dtoAgendaHibrida Get(int idAgendaHibrida, string tipoAgendamento)
        {
            dtoAgendaHibrida AgendaHibrida = new dtoAgendaHibrida();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM vwAgendaHibrida
                                    WHERE idAgendaHibrida = @idAgendaHibrida
                                    AND tipoAgendamento = @tipoAgendamento";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idAgendaHibrida", SqlDbType.Int).Value = idAgendaHibrida;
                cmdMenu.Parameters.Add("tipoAgendamento", SqlDbType.VarChar).Value = tipoAgendamento;

                try
                {
                    connection.Open();
                    SqlDataReader drAgendaHibrida = cmdMenu.ExecuteReader();

                    if (drAgendaHibrida.Read())
                        PreencheCampos(drAgendaHibrida, ref AgendaHibrida);
                }
                catch
                {
                    throw new ApplicationException("Erro ao capturar registro");
                }
                finally
                {
                    connection.Close();
                }
            }

            return AgendaHibrida;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAgendaHibrida> GetAll(string tipoAgendamento, string listaUsuarios, string Status, string dataInicioDe, string dataInicioAte, string dataTerminoDe, string dataTerminoAte, string SortExpression, string termoPesquisa)
        {
            return GetAll("", tipoAgendamento, listaUsuarios, Status, dataInicioDe, dataInicioAte, dataTerminoDe, dataTerminoAte, SortExpression, termoPesquisa);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAgendaHibrida> GetAll(string idCliente, string tipoAgendamento, string listaUsuarios, string Status, string dataInicioDe, string dataInicioAte, string dataTerminoDe, string dataTerminoAte, string SortExpression, string termoPesquisa)
        {
            List<dtoAgendaHibrida> AgendaHibridas = new List<dtoAgendaHibrida>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();
                    
                // Lista de Usuários
                if (listaUsuarios != null
                    && listaUsuarios != String.Empty
                    && listaUsuarios != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" CASE
	                                            WHEN tipoAgendamento = 'Prazo' 
                                                    AND EXISTS(SELECT 1 FROM tbProcessoPrazoResponsavel WHERE tbProcessoPrazoResponsavel.idProcessoPrazo = vwAgendaHibrida.idAgendaHibrida AND tbProcessoPrazoResponsavel.idPessoa IN (SELECT idPessoaVinculada FROM tbUsuario WHERE idUsuario IN ({0}))) THEN
		                                            1
	                                            WHEN tipoAgendamento = 'Agenda' 
			                                        AND (EXISTS(SELECT 1 FROM tbAgendaUsuario WHERE tbAgendaUsuario.idAgendaCompromisso = vwAgendaHibrida.idAgendaHibrida AND tbAgendaUsuario.idUsuario IN ({0})) 
			                                        OR EXISTS(SELECT 1 FROM tbAgendaCompromisso WHERE tbAgendaCompromisso.idAgendaCompromisso = vwAgendaHibrida.idAgendaHibrida AND tbAgendaCompromisso.idUsuarioCadastro = {0})) THEN
		                                            1
                                               END = 1", listaUsuarios);
                }

                // TIPO AGENDAMENTO
                if (tipoAgendamento != null
                    && tipoAgendamento != String.Empty
                    && tipoAgendamento != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" tipoAgendamento IN ({0}) ", tipoAgendamento);
                }

                // STATUS
                if (idCliente != null
                    && idCliente != String.Empty
                    && idCliente != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" idCliente IN ({0}) ", idCliente);
                }

                // STATUS
                if (Status != null
                    && Status != String.Empty
                    && Status != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" Status IN ({0}) ", Status);
                }

                // DATA INÍCIO DE
                if (dataInicioDe != null
                    && dataInicioDe.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataHoraInicio >= '{0} 00:00:00' ", dataInicioDe);
                }

                // DATA INÍCIO ATÉ
                if (dataInicioAte != null
                    && dataInicioAte.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataHoraInicio <= '{0} 23:59:59'", dataInicioAte);
                }

                // DATA TÉRMINO DE
                if (dataTerminoDe != null
                    && dataTerminoDe.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataHoraFim >= '{0} 00:00:00' ", dataTerminoDe);
                }

                // DATA TÉRMINO ATÉ
                if (dataTerminoAte != null
                    && dataTerminoAte.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataHoraFim <= '{0} 23:59:59'", dataTerminoDe);
                }

                // CONDIÇÕES
                if (termoPesquisa != null
                    && termoPesquisa != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (Descricao LIKE '%{0}%') ", termoPesquisa);
                }


                string stringSQL = String.Format(@"SELECT * 
                                                FROM vwAgendaHibrida 
                                                {0}
                                                ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "dataHoraInicio"));

                SqlCommand cmdAgendaHibrida = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drAgendaHibrida = cmdAgendaHibrida.ExecuteReader();

                    while (drAgendaHibrida.Read())
                    {
                        dtoAgendaHibrida AgendaHibrida = new dtoAgendaHibrida();

                        PreencheCampos(drAgendaHibrida, ref AgendaHibrida);

                        AgendaHibridas.Add(AgendaHibrida);
                    }

                }
                catch
                {
                    throw new ApplicationException("Erro ao capturar todos os registros");
                }
                finally
                {
                    connection.Close();
                }
            }

            return AgendaHibridas;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAgendaHibrida> GetAll()
        {
            return GetAll("", "", "", "", "", "", "", "", "", "");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAgendaHibrida> GetAll(string SortExpression)
        {
            return GetAll("", "", "", "", "", "", "", "", SortExpression, "");
        }

        public static void Delete(int idAgendaCompromisso_idProcessoPrazo, string tipoAgendamento)
        {
////            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
////            {
////                string stringSQL = @"DELETE tbAgendaUsuario 
////                                      WHERE idAgendaUsuario = @idAgendaUsuario";

////                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
////                cmdMenu.Parameters.Add("idAgendaUsuario", SqlDbType.Int).Value = idAgendaUsuario;

////                try
////                {
////                    connection.Open();
////                    cmdMenu.ExecuteNonQuery();
////                }
////                catch
////                {
////                    throw new ApplicationException("Erro ao excluir registro");
////                }
////                finally
////                {
////                    connection.Close();
////                }
////            }
        }

        private static void PreencheCampos(SqlDataReader drAgendaHibrida, ref dtoAgendaHibrida AgendaHibrida)
        {

            if (drAgendaHibrida["idAgendaHibrida"] != DBNull.Value)
                AgendaHibrida.idAgendaHibrida = Convert.ToInt32(drAgendaHibrida["idAgendaHibrida"].ToString());

            if (drAgendaHibrida["DataHoraInicio"] != DBNull.Value)
                AgendaHibrida.DataHoraInicio = Convert.ToDateTime(drAgendaHibrida["DataHoraInicio"]);
            else
                AgendaHibrida.DataHoraInicio = null;

            if (drAgendaHibrida["DataHoraFim"] != DBNull.Value)
                AgendaHibrida.DataHoraFim = Convert.ToDateTime(drAgendaHibrida["DataHoraFim"]);
            else
                AgendaHibrida.DataHoraFim = null;

            if (drAgendaHibrida["Descricao"] != DBNull.Value)
                AgendaHibrida.Descricao = drAgendaHibrida["Descricao"].ToString();

            if (drAgendaHibrida["Responsaveis"] != DBNull.Value)
                AgendaHibrida.Responsaveis = drAgendaHibrida["Responsaveis"].ToString();

            if (drAgendaHibrida["Status"] != DBNull.Value)
                AgendaHibrida.Status = drAgendaHibrida["Status"].ToString();

            if (drAgendaHibrida["tipoAgendamento"] != DBNull.Value)
                AgendaHibrida.tipoAgendamento = drAgendaHibrida["tipoAgendamento"].ToString();

        }

    }


}
