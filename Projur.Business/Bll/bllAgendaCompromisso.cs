using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using ProJur.Business.Dto;
using InfoVillage.DevLibrary;

namespace ProJur.Business.Bll
{

    [DataObject(true)]
    public class bllAgendaCompromisso
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoAgendaCompromisso AgendaCompromisso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbAgendaCompromisso(dataCadastro, idUsuarioCadastro, dataInicio, dataFim, Descricao, situacaoCompromisso, idPessoa)
                                            VALUES(getdate(), @idUsuarioCadastro, @dataInicio, @dataFim, @Descricao, @situacaoCompromisso, @idPessoa);
                                            SET @idAgendaCompromisso = SCOPE_IDENTITY()";



                SqlCommand cmdAgendaCompromisso = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref AgendaCompromisso);

                cmdAgendaCompromisso.Parameters.Add("idAgendaCompromisso", SqlDbType.Int);
                cmdAgendaCompromisso.Parameters["idAgendaCompromisso"].Direction = ParameterDirection.Output;

                cmdAgendaCompromisso.Parameters.Add("idUsuarioCadastro", SqlDbType.Int).Value = AgendaCompromisso.idUsuarioCadastro;
                cmdAgendaCompromisso.Parameters.Add("Descricao", SqlDbType.VarChar).Value = AgendaCompromisso.Descricao;
                cmdAgendaCompromisso.Parameters.Add("situacaoCompromisso", SqlDbType.VarChar).Value = AgendaCompromisso.situacaoCompromisso;

                if (AgendaCompromisso.dataInicio != null)
                    cmdAgendaCompromisso.Parameters.Add("dataInicio", SqlDbType.DateTime).Value = AgendaCompromisso.dataInicio;
                else
                    cmdAgendaCompromisso.Parameters.Add("dataInicio", SqlDbType.DateTime).Value = DBNull.Value;

                if (AgendaCompromisso.dataFim != null)
                    cmdAgendaCompromisso.Parameters.Add("dataFim", SqlDbType.DateTime).Value = AgendaCompromisso.dataFim;
                else
                    cmdAgendaCompromisso.Parameters.Add("dataFim", SqlDbType.DateTime).Value = DBNull.Value;

                cmdAgendaCompromisso.Parameters.Add("idPessoa", SqlDbType.Int).Value = AgendaCompromisso.idPessoa;

                try
                {
                    connection.Open();
                    cmdAgendaCompromisso.ExecuteNonQuery();

                    int idAgendaCompromisso = (int)cmdAgendaCompromisso.Parameters["idAgendaCompromisso"].Value;

                    // INSERE USUÁRIOS PADRÕES PARA O AGENDAMENTO
                    string stringSQLUsuariosPadroes = String.Format(@"INSERT INTO tbAgendaUsuario(idAgendaCompromisso, idUsuario)
                                                    SELECT {0}, idUsuario FROM tbUsuario WHERE usuarioPadraoAgendamento = 1", idAgendaCompromisso);

                    SqlCommand cmdAgendaUsuariosPadroes = new SqlCommand(stringSQLUsuariosPadroes, connection);
                    cmdAgendaUsuariosPadroes.ExecuteNonQuery();

                    return idAgendaCompromisso;
                }
                catch
                {
                    throw new ApplicationException("Erro ao inserir registro");
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void Update(dtoAgendaCompromisso AgendaCompromisso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbAgendaCompromisso SET 
                                        Descricao = @Descricao,
                                        situacaoCompromisso = @situacaoCompromisso,
                                        dataInicio = @dataInicio,
                                        dataFim = @dataFim,
                                        idUsuarioUltimaAlteracao = @idUsuarioUltimaAlteracao,
                                        dataUltimaAlteracao = getdate(),
                                        idPessoa = @idPessoa
                                      WHERE idAgendaCompromisso = @idAgendaCompromisso";

                SqlCommand cmdAgendaCompromisso = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref AgendaCompromisso);

                cmdAgendaCompromisso.Parameters.Add("idAgendaCompromisso", SqlDbType.Int).Value = AgendaCompromisso.idAgendaCompromisso;
                cmdAgendaCompromisso.Parameters.Add("Descricao", SqlDbType.VarChar).Value = AgendaCompromisso.Descricao;
                cmdAgendaCompromisso.Parameters.Add("situacaoCompromisso", SqlDbType.VarChar).Value = AgendaCompromisso.situacaoCompromisso;
                cmdAgendaCompromisso.Parameters.Add("idUsuarioUltimaAlteracao", SqlDbType.Int).Value = AgendaCompromisso.idUsuarioUltimaAlteracao;

                if (AgendaCompromisso.dataInicio != null)
                    cmdAgendaCompromisso.Parameters.Add("dataInicio", SqlDbType.DateTime).Value = AgendaCompromisso.dataInicio;
                else
                    cmdAgendaCompromisso.Parameters.Add("dataInicio", SqlDbType.DateTime).Value = DBNull.Value;

                if (AgendaCompromisso.dataFim != null)
                    cmdAgendaCompromisso.Parameters.Add("dataFim", SqlDbType.DateTime).Value = AgendaCompromisso.dataFim;
                else
                    cmdAgendaCompromisso.Parameters.Add("dataFim", SqlDbType.DateTime).Value = DBNull.Value;

                cmdAgendaCompromisso.Parameters.Add("idPessoa", SqlDbType.Int).Value = AgendaCompromisso.idPessoa;


                try
                {
                    connection.Open();
                    cmdAgendaCompromisso.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("Erro ao atualizar registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void Delete(dtoAgendaCompromisso AgendaCompromisso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbAgendaCompromisso 
                                      WHERE idAgendaCompromisso = @idAgendaCompromisso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idAgendaCompromisso", SqlDbType.Int).Value = AgendaCompromisso.idAgendaCompromisso;

                try
                {
                    connection.Open();
                    cmdMenu.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("Erro ao excluir registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static void Delete(int idAgendaCompromisso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbAgendaCompromisso 
                                      WHERE idAgendaCompromisso = @idAgendaCompromisso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idAgendaCompromisso", SqlDbType.Int).Value = idAgendaCompromisso;

                try
                {
                    connection.Open();
                    cmdMenu.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("Erro ao excluir registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static dtoAgendaCompromisso Get(int idAgendaCompromisso)
        {
            dtoAgendaCompromisso AgendaCompromisso = new dtoAgendaCompromisso();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbAgendaCompromisso
                                    WHERE idAgendaCompromisso = @idAgendaCompromisso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idAgendaCompromisso", SqlDbType.Int).Value = idAgendaCompromisso;

                try
                {
                    connection.Open();
                    SqlDataReader drAgendaCompromisso = cmdMenu.ExecuteReader();

                    if (drAgendaCompromisso.Read())
                        PreencheCampos(drAgendaCompromisso, ref AgendaCompromisso);
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

            return AgendaCompromisso;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAgendaCompromisso> GetAll(string SortExpression)
        {
            List<dtoAgendaCompromisso> AgendaCompromissos = new List<dtoAgendaCompromisso>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = String.Format("SELECT * FROM tbAgendaCompromisso ORDER BY {0}", (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idAgendaCompromisso"));

                SqlCommand cmdAgendaCompromisso = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drAgendaCompromisso = cmdAgendaCompromisso.ExecuteReader();

                    while (drAgendaCompromisso.Read())
                    {
                        dtoAgendaCompromisso AgendaCompromisso = new dtoAgendaCompromisso();

                        PreencheCampos(drAgendaCompromisso, ref AgendaCompromisso);

                        AgendaCompromissos.Add(AgendaCompromisso);
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

            return AgendaCompromissos;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAgendaCompromisso> GetAll()
        {
            return GetAll("");
        }

        public static List<dtoAgendaCompromisso> GetAll(string idUsuario, string situacaoCompromisso, string dataInicioInicial, string dataInicioFinal, string dataTerminoInicial, string dataTerminoFinal, string SortExpression, string termoPesquisa)
        {
            List<dtoAgendaCompromisso> agendaCompromissos = new List<dtoAgendaCompromisso>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                


                StringBuilder sbCondicao = new StringBuilder();

                // USUÁRIO
                if (idUsuario != null
                    && idUsuario != String.Empty
                    && idUsuario != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" EXISTS (SELECT 1 FROM tbAgendaUsuario WHERE idUsuario = {0} AND tbAgendaUsuario.idAgendaCompromisso = tbAgendaCompromisso.idAgendaCompromisso) ", idUsuario);
                }

                // SITUAÇÃO COMPROMISSO
                if (situacaoCompromisso != null
                    && situacaoCompromisso != String.Empty
                    && situacaoCompromisso != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" situacaoCompromisso = '{0}' ", situacaoCompromisso);
                }

                // DATA INÍCIO COMPROMISSO INICIAL
                if (dataInicioInicial != null
                    && dataInicioInicial.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataInicio >= '{0} 00:00:00' ", dataInicioInicial);
                }

                // DATA INÍCIO COMPROMISSO FINAL
                if (dataInicioFinal != null
                    && dataInicioFinal.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataInicio <= '{0} 23:59:59'", dataInicioFinal);
                }

                // DATA TÉRMINO INICIAL
                if (dataTerminoInicial != null
                    && dataTerminoInicial.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataFim >= '{0} 00:00:00' ", dataTerminoInicial);
                }

                // DATA PREVISÃO FINAL
                if (dataTerminoFinal != null
                    && dataTerminoFinal.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataFim <= '{0} 23:59:59'", dataTerminoFinal);
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
                                                FROM tbAgendaCompromisso
                                            {0} ORDER BY {1}", 
                                            sbCondicao.ToString(), 
                                            (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "tbAgendaCompromisso.idAgendaCompromisso"));

                SqlCommand cmdAgendaCompromisso = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drAgendaCompromisso = cmdAgendaCompromisso.ExecuteReader();

                    while (drAgendaCompromisso.Read())
                    {
                        dtoAgendaCompromisso agendaCompromisso = new dtoAgendaCompromisso();

                        PreencheCampos(drAgendaCompromisso, ref agendaCompromisso);

                        agendaCompromissos.Add(agendaCompromisso);
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

            return agendaCompromissos;
        }

        private static void PreencheCampos(SqlDataReader drAgendaCompromisso, ref dtoAgendaCompromisso AgendaCompromisso)
        {

            if (drAgendaCompromisso["idAgendaCompromisso"] != DBNull.Value)
                AgendaCompromisso.idAgendaCompromisso = Convert.ToInt32(drAgendaCompromisso["idAgendaCompromisso"].ToString());



            if (drAgendaCompromisso["idUsuarioCadastro"] != DBNull.Value)
                AgendaCompromisso.idUsuarioCadastro = Convert.ToInt32(drAgendaCompromisso["idUsuarioCadastro"].ToString());

            if (drAgendaCompromisso["idUsuarioUltimaAlteracao"] != DBNull.Value)
                AgendaCompromisso.idUsuarioUltimaAlteracao = Convert.ToInt32(drAgendaCompromisso["idUsuarioUltimaAlteracao"].ToString());

            if (drAgendaCompromisso["dataCadastro"] != DBNull.Value)
                AgendaCompromisso.dataCadastro = Convert.ToDateTime(drAgendaCompromisso["dataCadastro"]);
            else
                AgendaCompromisso.dataCadastro = null;

            if (drAgendaCompromisso["dataUltimaAlteracao"] != DBNull.Value)
                AgendaCompromisso.dataUltimaAlteracao = Convert.ToDateTime(drAgendaCompromisso["dataUltimaAlteracao"]);
            else
                AgendaCompromisso.dataUltimaAlteracao = null;




            if (drAgendaCompromisso["dataInicio"] != DBNull.Value)
                AgendaCompromisso.dataInicio = Convert.ToDateTime(drAgendaCompromisso["dataInicio"]);
            else
                AgendaCompromisso.dataInicio = null;

            if (drAgendaCompromisso["dataFim"] != DBNull.Value)
                AgendaCompromisso.dataFim = Convert.ToDateTime(drAgendaCompromisso["dataFim"]);
            else
                AgendaCompromisso.dataFim = null;

            if (drAgendaCompromisso["Descricao"] != DBNull.Value)
                AgendaCompromisso.Descricao = drAgendaCompromisso["Descricao"].ToString();

            if (drAgendaCompromisso["situacaoCompromisso"] != DBNull.Value)
                AgendaCompromisso.situacaoCompromisso = drAgendaCompromisso["situacaoCompromisso"].ToString();


            if (drAgendaCompromisso["idPessoa"] != DBNull.Value)
                AgendaCompromisso.idPessoa = Convert.ToInt32(drAgendaCompromisso["idPessoa"].ToString());

        }

        private static void ValidaCampos(ref dtoAgendaCompromisso AgendaCompromisso)
        {

            if (String.IsNullOrEmpty(AgendaCompromisso.Descricao)) { AgendaCompromisso.Descricao = String.Empty; }
            if (String.IsNullOrEmpty(AgendaCompromisso.situacaoCompromisso)) { AgendaCompromisso.situacaoCompromisso = String.Empty; }

        }

        public static string RetornaDescricaoSituacaoCompromisso(object situacaoCompromisso)
        {
            string retorno = String.Empty;

            if (situacaoCompromisso != null)
            {
                switch ((string)situacaoCompromisso)
                {
                    case "P":
                        retorno = "Pendente";
                        break;

                    case "C":
                        retorno = "Concluído";
                        break;
                }
            }

            return retorno;
        }

        
    }
}
