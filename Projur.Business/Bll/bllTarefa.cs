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
    public class bllTarefa
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoTarefa Tarefa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbTarefa(dataCadastro, dataPrevisao, dataConclusao, Descricao, situacaoTarefa, idUsuarioResponsavel, idProcesso)
                                            VALUES(getdate(), @dataPrevisao, @dataConclusao, @Descricao, @situacaoTarefa, @idUsuarioResponsavel, @idProcesso);
                                            SET @idTarefa = SCOPE_IDENTITY()";

                SqlCommand cmdTarefa = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Tarefa);

                cmdTarefa.Parameters.Add("idTarefa", SqlDbType.Int);
                cmdTarefa.Parameters["idTarefa"].Direction = ParameterDirection.Output;

                cmdTarefa.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Tarefa.Descricao;
                cmdTarefa.Parameters.Add("situacaoTarefa", SqlDbType.VarChar).Value = Tarefa.situacaoTarefa;

                if (Tarefa.dataPrevisao != null)
                    cmdTarefa.Parameters.Add("dataPrevisao", SqlDbType.DateTime).Value = Tarefa.dataPrevisao;
                else
                    cmdTarefa.Parameters.Add("dataPrevisao", SqlDbType.DateTime).Value = DBNull.Value;

                if (Tarefa.dataConclusao != null)
                    cmdTarefa.Parameters.Add("dataConclusao", SqlDbType.DateTime).Value = Tarefa.dataConclusao;
                else
                    cmdTarefa.Parameters.Add("dataConclusao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdTarefa.Parameters.Add("idUsuarioResponsavel", SqlDbType.Int).Value = Tarefa.idUsuarioResponsavel;
                cmdTarefa.Parameters.Add("idProcesso", SqlDbType.Int).Value = Tarefa.idProcesso;

                try
                {
                    connection.Open();
                    cmdTarefa.ExecuteNonQuery();

                    return (int)cmdTarefa.Parameters["idTarefa"].Value;
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
        public static void Update(dtoTarefa Tarefa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbTarefa SET 
                                        Descricao = @Descricao,
                                        situacaoTarefa = @situacaoTarefa,
                                        dataPrevisao = @dataPrevisao,
                                        dataConclusao = @dataConclusao,
                                        idUsuarioResponsavel = @idUsuarioResponsavel,
                                        idProcesso = @idProcesso,        
                                        dataUltimaAlteracao = getdate()
                                      WHERE idTarefa = @idTarefa";

                SqlCommand cmdTarefa = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Tarefa);

                cmdTarefa.Parameters.Add("idTarefa", SqlDbType.Int).Value = Tarefa.idTarefa;
                cmdTarefa.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Tarefa.Descricao;
                cmdTarefa.Parameters.Add("situacaoTarefa", SqlDbType.VarChar).Value = Tarefa.situacaoTarefa;

                if (Tarefa.dataPrevisao != null)
                    cmdTarefa.Parameters.Add("dataPrevisao", SqlDbType.DateTime).Value = Tarefa.dataPrevisao;
                else
                    cmdTarefa.Parameters.Add("dataPrevisao", SqlDbType.DateTime).Value = DBNull.Value;

                if (Tarefa.dataConclusao != null)
                    cmdTarefa.Parameters.Add("dataConclusao", SqlDbType.DateTime).Value = Tarefa.dataConclusao;
                else
                    cmdTarefa.Parameters.Add("dataConclusao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdTarefa.Parameters.Add("idUsuarioResponsavel", SqlDbType.Int).Value = Tarefa.idUsuarioResponsavel;
                cmdTarefa.Parameters.Add("idProcesso", SqlDbType.Int).Value = Tarefa.idProcesso;

                try
                {
                    connection.Open();
                    cmdTarefa.ExecuteNonQuery();
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
        public static void Delete(dtoTarefa Tarefa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbTarefa 
                                      WHERE idTarefa = @idTarefa";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idTarefa", SqlDbType.Int).Value = Tarefa.idTarefa;

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

        public static void Delete(int idTarefa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbTarefa 
                                      WHERE idTarefa = @idTarefa";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idTarefa", SqlDbType.Int).Value = idTarefa;

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
        public static dtoTarefa Get(int idTarefa)
        {
            dtoTarefa Tarefa = new dtoTarefa();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbTarefa
                                    WHERE idTarefa = @idTarefa";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idTarefa", SqlDbType.Int).Value = idTarefa;

                try
                {
                    connection.Open();
                    SqlDataReader drTarefa = cmdMenu.ExecuteReader();

                    if (drTarefa.Read())
                        PreencheCampos(drTarefa, ref Tarefa);
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

            return Tarefa;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoTarefa> GetAll(string idUsuarioResponsavel, string situacaoTarefa, string dataCadastroInicio, string dataCadastroFim, string dataPrevisaoInicio, string dataPrevisaoFim, string dataConclusaoInicio, string dataConclusaoFim, string SortExpression, string termoPesquisa)
        {
            List<dtoTarefa> Tarefas = new List<dtoTarefa>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                // USUÁRIO
                if (idUsuarioResponsavel != null
                    && idUsuarioResponsavel != String.Empty
                    && idUsuarioResponsavel != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" idUsuarioResponsavel = {0} ", idUsuarioResponsavel);
                }

                // SITUAÇÃO TAREFA
                if (situacaoTarefa != null
                    && situacaoTarefa != String.Empty
                    && situacaoTarefa != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" situacaoTarefa = '{0}' ", situacaoTarefa);
                }

                // DATA CADASTRO INICIAL
                if (dataCadastroInicio != null
                    && dataCadastroInicio.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataCadastro >= '{0} 00:00:00' ", dataCadastroInicio);
                }

                // DATA CADASTRO FINAL
                if (dataCadastroFim != null
                    && dataCadastroFim.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataCadastro <= '{0} 23:59:59'", dataCadastroFim);
                }

                // DATA PREVISÃO INICIAL
                if (dataPrevisaoInicio != null
                    && dataPrevisaoInicio.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataPrevisao >= '{0} 00:00:00' ", dataPrevisaoInicio);
                }

                // DATA PREVISÃO FINAL
                if (dataPrevisaoFim != null
                    && dataPrevisaoFim.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataPrevisao <= '{0} 23:59:59'", dataPrevisaoFim);
                }

                // DATA CONCLUSÃO INICIAL
                if (dataConclusaoInicio != null
                    && dataConclusaoInicio.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataConclusao >= '{0} 00:00:00' ", dataConclusaoInicio);
                }

                // DATA CONCLUSÃO FINAL
                if (dataConclusaoFim != null
                    && dataConclusaoFim.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataConclusao <= '{0} 23:59:59'", dataConclusaoFim);
                }

                // CONDIÇÕES
                if (termoPesquisa != null
                    && termoPesquisa != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbTarefa.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbTarefa {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idTarefa"));

                SqlCommand cmdTarefa = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drTarefa = cmdTarefa.ExecuteReader();

                    while (drTarefa.Read())
                    {
                        dtoTarefa Tarefa = new dtoTarefa();

                        PreencheCampos(drTarefa, ref Tarefa);

                        Tarefas.Add(Tarefa);
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

            return Tarefas;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoTarefa> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoTarefa> GetAll(string SortExpression)
        {
            return GetAll("", "", "", "", "", "", "", "", SortExpression, "");
        }


        private static void PreencheCampos(SqlDataReader drTarefa, ref dtoTarefa Tarefa)
        {

            if (drTarefa["idTarefa"] != DBNull.Value)
                Tarefa.idTarefa = Convert.ToInt32(drTarefa["idTarefa"].ToString());

            if (drTarefa["dataCadastro"] != DBNull.Value)
                Tarefa.dataCadastro = Convert.ToDateTime(drTarefa["dataCadastro"]);
            else
                Tarefa.dataCadastro = null;

            if (drTarefa["dataUltimaAlteracao"] != DBNull.Value)
                Tarefa.dataUltimaAlteracao = Convert.ToDateTime(drTarefa["dataUltimaAlteracao"]);
            else
                Tarefa.dataUltimaAlteracao = null;

            if (drTarefa["dataPrevisao"] != DBNull.Value)
                Tarefa.dataPrevisao = Convert.ToDateTime(drTarefa["dataPrevisao"]);
            else
                Tarefa.dataPrevisao = null;

            if (drTarefa["dataConclusao"] != DBNull.Value)
                Tarefa.dataConclusao = Convert.ToDateTime(drTarefa["dataConclusao"]);
            else
                Tarefa.dataConclusao = null;

            if (drTarefa["Descricao"] != DBNull.Value)
                Tarefa.Descricao = drTarefa["Descricao"].ToString();

            if (drTarefa["situacaoTarefa"] != DBNull.Value)
                Tarefa.situacaoTarefa = drTarefa["situacaoTarefa"].ToString();

            if (drTarefa["idUsuarioResponsavel"] != DBNull.Value)
                Tarefa.idUsuarioResponsavel = Convert.ToInt32(drTarefa["idUsuarioResponsavel"].ToString());

            if (drTarefa["idProcesso"] != DBNull.Value)
                Tarefa.idProcesso = Convert.ToInt32(drTarefa["idProcesso"].ToString());

        }

        private static void ValidaCampos(ref dtoTarefa Tarefa)
        {

            if (String.IsNullOrEmpty(Tarefa.Descricao)) { Tarefa.Descricao = String.Empty; }
            if (String.IsNullOrEmpty(Tarefa.situacaoTarefa)) { Tarefa.situacaoTarefa = String.Empty; }

        }

        public static string RetornaDescricaoSituacaoTarefa(object situacaoTarefa)
        {
            string retorno = String.Empty;

            if (situacaoTarefa != null)
            {
                switch ((string)situacaoTarefa)
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
