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
    public class bllTipoAcao
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoTipoAcao TipoAcao)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbTipoAcao(Descricao, dataCadastro)
                                            VALUES(@Descricao, getdate());
                                            SET @idTipoAcao = SCOPE_IDENTITY()";

                SqlCommand cmdTipoAcao = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref TipoAcao);

                cmdTipoAcao.Parameters.Add("idTipoAcao", SqlDbType.Int);
                cmdTipoAcao.Parameters["idTipoAcao"].Direction = ParameterDirection.Output;

                cmdTipoAcao.Parameters.Add("Descricao", SqlDbType.VarChar).Value = TipoAcao.Descricao;

                try
                {
                    connection.Open();
                    cmdTipoAcao.ExecuteNonQuery();

                    return (int)cmdTipoAcao.Parameters["idTipoAcao"].Value;
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
        public static void Update(dtoTipoAcao TipoAcao)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbTipoAcao SET 
                                        Descricao = @Descricao,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idTipoAcao = @idTipoAcao";

                SqlCommand cmdTipoAcao = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref TipoAcao);

                cmdTipoAcao.Parameters.Add("idTipoAcao", SqlDbType.Int).Value = TipoAcao.idTipoAcao;
                cmdTipoAcao.Parameters.Add("Descricao", SqlDbType.VarChar).Value = TipoAcao.Descricao;

                try
                {
                    connection.Open();
                    cmdTipoAcao.ExecuteNonQuery();
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
        public static void Delete(dtoTipoAcao TipoAcao)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbTipoAcao 
                                      WHERE idTipoAcao = @idTipoAcao";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idTipoAcao", SqlDbType.Int).Value = TipoAcao.idTipoAcao;

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

        public static void Delete(int idTipoAcao)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbTipoAcao 
                                      WHERE idTipoAcao = @idTipoAcao";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idTipoAcao", SqlDbType.Int).Value = idTipoAcao;

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
        public static dtoTipoAcao Get(int idTipoAcao)
        {
            dtoTipoAcao TipoAcao = new dtoTipoAcao();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbTipoAcao
                                    WHERE idTipoAcao = @idTipoAcao";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idTipoAcao", SqlDbType.Int).Value = idTipoAcao;

                try
                {
                    connection.Open();
                    SqlDataReader drTipoAcao = cmdMenu.ExecuteReader();

                    if (drTipoAcao.Read())
                        PreencheCampos(drTipoAcao, ref TipoAcao);
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

            return TipoAcao;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoTipoAcao> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoTipoAcao> TipoAcaos = new List<dtoTipoAcao>();

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

                    sbCondicao.AppendFormat(@" (tbTipoAcao.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbTipoAcao {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idTipoAcao"));

                SqlCommand cmdTipoAcao = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drTipoAcao = cmdTipoAcao.ExecuteReader();

                    while (drTipoAcao.Read())
                    {
                        dtoTipoAcao TipoAcao = new dtoTipoAcao();

                        PreencheCampos(drTipoAcao, ref TipoAcao);

                        TipoAcaos.Add(TipoAcao);
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

            return TipoAcaos;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoTipoAcao> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoTipoAcao> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }

        private static void PreencheCampos(SqlDataReader drTipoAcao, ref dtoTipoAcao TipoAcao)
        {

            if (drTipoAcao["idTipoAcao"] != DBNull.Value)
                TipoAcao.idTipoAcao = Convert.ToInt32(drTipoAcao["idTipoAcao"].ToString());

            if (drTipoAcao["dataCadastro"] != DBNull.Value)
                TipoAcao.dataCadastro = Convert.ToDateTime(drTipoAcao["dataCadastro"]);
            else
                TipoAcao.dataCadastro = null;

            if (drTipoAcao["dataUltimaAlteracao"] != DBNull.Value)
                TipoAcao.dataUltimaAlteracao = Convert.ToDateTime(drTipoAcao["dataUltimaAlteracao"]);
            else
                TipoAcao.dataUltimaAlteracao = null;

            if (drTipoAcao["Descricao"] != DBNull.Value)
                TipoAcao.Descricao = drTipoAcao["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoTipoAcao TipoAcao)
        {

            if (String.IsNullOrEmpty(TipoAcao.Descricao)) { TipoAcao.Descricao = String.Empty; }

        }

    }
}