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
    public class bllSituacaoProcesso
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoSituacaoProcesso SituacaoProcesso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbSituacaoProcesso(Descricao, dataCadastro)
                                            VALUES(@Descricao, getdate());
                                            SET @idSituacaoProcesso = SCOPE_IDENTITY()";

                SqlCommand cmdSituacaoProcesso = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref SituacaoProcesso);

                cmdSituacaoProcesso.Parameters.Add("idSituacaoProcesso", SqlDbType.Int);
                cmdSituacaoProcesso.Parameters["idSituacaoProcesso"].Direction = ParameterDirection.Output;

                cmdSituacaoProcesso.Parameters.Add("Descricao", SqlDbType.VarChar).Value = SituacaoProcesso.Descricao;

                try
                {
                    connection.Open();
                    cmdSituacaoProcesso.ExecuteNonQuery();

                    return (int)cmdSituacaoProcesso.Parameters["idSituacaoProcesso"].Value;
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
        public static void Update(dtoSituacaoProcesso SituacaoProcesso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbSituacaoProcesso SET 
                                        Descricao = @Descricao,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idSituacaoProcesso = @idSituacaoProcesso";

                SqlCommand cmdSituacaoProcesso = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref SituacaoProcesso);

                cmdSituacaoProcesso.Parameters.Add("idSituacaoProcesso", SqlDbType.Int).Value = SituacaoProcesso.idSituacaoProcesso;
                cmdSituacaoProcesso.Parameters.Add("Descricao", SqlDbType.VarChar).Value = SituacaoProcesso.Descricao;

                try
                {
                    connection.Open();
                    cmdSituacaoProcesso.ExecuteNonQuery();
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
        public static void Delete(dtoSituacaoProcesso SituacaoProcesso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbSituacaoProcesso 
                                      WHERE idSituacaoProcesso = @idSituacaoProcesso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idSituacaoProcesso", SqlDbType.Int).Value = SituacaoProcesso.idSituacaoProcesso;

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

        public static void Delete(int idSituacaoProcesso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbSituacaoProcesso 
                                      WHERE idSituacaoProcesso = @idSituacaoProcesso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idSituacaoProcesso", SqlDbType.Int).Value = idSituacaoProcesso;

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
        public static dtoSituacaoProcesso Get(int idSituacaoProcesso)
        {
            dtoSituacaoProcesso SituacaoProcesso = new dtoSituacaoProcesso();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbSituacaoProcesso
                                    WHERE idSituacaoProcesso = @idSituacaoProcesso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idSituacaoProcesso", SqlDbType.Int).Value = idSituacaoProcesso;

                try
                {
                    connection.Open();
                    SqlDataReader drSituacaoProcesso = cmdMenu.ExecuteReader();

                    if (drSituacaoProcesso.Read())
                        PreencheCampos(drSituacaoProcesso, ref SituacaoProcesso);
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

            return SituacaoProcesso;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoSituacaoProcesso> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoSituacaoProcesso> SituacaoProcessos = new List<dtoSituacaoProcesso>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                // CONDIÇÕES
                if (termoPesquisa != null
                    && termoPesquisa != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbSituacaoProcesso.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbSituacaoProcesso {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idSituacaoProcesso"));

                SqlCommand cmdSituacaoProcesso = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drSituacaoProcesso = cmdSituacaoProcesso.ExecuteReader();

                    while (drSituacaoProcesso.Read())
                    {
                        dtoSituacaoProcesso SituacaoProcesso = new dtoSituacaoProcesso();

                        PreencheCampos(drSituacaoProcesso, ref SituacaoProcesso);

                        SituacaoProcessos.Add(SituacaoProcesso);
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

            return SituacaoProcessos;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoSituacaoProcesso> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoSituacaoProcesso> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }

        private static void PreencheCampos(SqlDataReader drSituacaoProcesso, ref dtoSituacaoProcesso SituacaoProcesso)
        {

            if (drSituacaoProcesso["idSituacaoProcesso"] != DBNull.Value)
                SituacaoProcesso.idSituacaoProcesso = Convert.ToInt32(drSituacaoProcesso["idSituacaoProcesso"].ToString());

            if (drSituacaoProcesso["dataCadastro"] != DBNull.Value)
                SituacaoProcesso.dataCadastro = Convert.ToDateTime(drSituacaoProcesso["dataCadastro"]);
            else
                SituacaoProcesso.dataCadastro = null;

            if (drSituacaoProcesso["dataUltimaAlteracao"] != DBNull.Value)
                SituacaoProcesso.dataUltimaAlteracao = Convert.ToDateTime(drSituacaoProcesso["dataUltimaAlteracao"]);
            else
                SituacaoProcesso.dataUltimaAlteracao = null;

            if (drSituacaoProcesso["Descricao"] != DBNull.Value)
                SituacaoProcesso.Descricao = drSituacaoProcesso["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoSituacaoProcesso SituacaoProcesso)
        {

            if (String.IsNullOrEmpty(SituacaoProcesso.Descricao)) { SituacaoProcesso.Descricao = String.Empty; }

        }

    }
}