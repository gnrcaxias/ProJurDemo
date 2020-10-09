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
    public class bllCidade
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoCidade Cidade)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbCidade(idEstado, Descricao, dataCadastro)
                                            VALUES(@idEstado, @Descricao, getdate());
                                            SET @idCidade = SCOPE_IDENTITY()";

                SqlCommand cmdCidade = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Cidade);

                cmdCidade.Parameters.Add("idCidade", SqlDbType.Int);
                cmdCidade.Parameters["idCidade"].Direction = ParameterDirection.Output;

                cmdCidade.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Cidade.Descricao;
                cmdCidade.Parameters.Add("idEstado", SqlDbType.Int).Value = Cidade.idEstado;

                try
                {
                    connection.Open();
                    cmdCidade.ExecuteNonQuery();

                    return (int)cmdCidade.Parameters["idCidade"].Value;
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
        public static void Update(dtoCidade Cidade)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbCidade SET 
                                        idEstado = @idEstado,
                                        Descricao = @Descricao,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idCidade = @idCidade";

                SqlCommand cmdCidade = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Cidade);

                cmdCidade.Parameters.Add("idCidade", SqlDbType.Int).Value = Cidade.idCidade;
                cmdCidade.Parameters.Add("idEstado", SqlDbType.Int).Value = Cidade.idEstado;
                cmdCidade.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Cidade.Descricao;

                try
                {
                    connection.Open();
                    cmdCidade.ExecuteNonQuery();
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
        public static void Delete(dtoCidade Cidade)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbCidade 
                                      WHERE idCidade = @idCidade";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idCidade", SqlDbType.Int).Value = Cidade.idCidade;

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

        public static void Delete(int idCidade)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbCidade 
                                      WHERE idCidade = @idCidade";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idCidade", SqlDbType.Int).Value = idCidade;

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
        public static dtoCidade Get(int idCidade)
        {
            dtoCidade Cidade = new dtoCidade();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbCidade
                                    WHERE idCidade = @idCidade";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idCidade", SqlDbType.Int).Value = idCidade;

                try
                {
                    connection.Open();
                    SqlDataReader drCidade = cmdMenu.ExecuteReader();

                    if (drCidade.Read())
                        PreencheCampos(drCidade, ref Cidade);
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

            return Cidade;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoCidade> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoCidade> Cidades = new List<dtoCidade>();

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

                    sbCondicao.AppendFormat(@" (tbCidade.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbCidade {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idCidade"));

                SqlCommand cmdCidade = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drCidade = cmdCidade.ExecuteReader();

                    while (drCidade.Read())
                    {
                        dtoCidade Cidade = new dtoCidade();

                        PreencheCampos(drCidade, ref Cidade);

                        Cidades.Add(Cidade);
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

            return Cidades;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoCidade> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoCidade> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }

        private static void PreencheCampos(SqlDataReader drCidade, ref dtoCidade Cidade)
        {

            if (drCidade["idCidade"] != DBNull.Value)
                Cidade.idCidade = Convert.ToInt32(drCidade["idCidade"].ToString());

            if (drCidade["idEstado"] != DBNull.Value)
                Cidade.idEstado = Convert.ToInt32(drCidade["idEstado"].ToString());

            if (drCidade["dataCadastro"] != DBNull.Value)
                Cidade.dataCadastro = Convert.ToDateTime(drCidade["dataCadastro"]);
            else
                Cidade.dataCadastro = null;

            if (drCidade["dataUltimaAlteracao"] != DBNull.Value)
                Cidade.dataUltimaAlteracao = Convert.ToDateTime(drCidade["dataUltimaAlteracao"]);
            else
                Cidade.dataUltimaAlteracao = null;

            if (drCidade["Descricao"] != DBNull.Value)
                Cidade.Descricao = drCidade["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoCidade Cidade)
        {

            if (String.IsNullOrEmpty(Cidade.Descricao)) { Cidade.Descricao = String.Empty; }

        }

    }
}