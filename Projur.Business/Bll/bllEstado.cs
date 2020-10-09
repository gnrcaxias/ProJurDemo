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
    public class bllEstado
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoEstado Estado)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbEstado(Descricao, siglaUF, dataCadastro)
                                            VALUES(@Descricao, @siglaUF, getdate());
                                            SET @idEstado = SCOPE_IDENTITY()";

                SqlCommand cmdEstado = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Estado);

                cmdEstado.Parameters.Add("idEstado", SqlDbType.Int);
                cmdEstado.Parameters["idEstado"].Direction = ParameterDirection.Output;

                cmdEstado.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Estado.Descricao;
                cmdEstado.Parameters.Add("siglaUF", SqlDbType.VarChar).Value = Estado.siglaUF;

                try
                {
                    connection.Open();
                    cmdEstado.ExecuteNonQuery();

                    return (int)cmdEstado.Parameters["idEstado"].Value;
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
        public static void Update(dtoEstado Estado)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbEstado SET 
                                        Descricao = @Descricao,
                                        siglaUF = @siglaUF,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idEstado = @idEstado";

                SqlCommand cmdEstado = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Estado);

                cmdEstado.Parameters.Add("idEstado", SqlDbType.Int).Value = Estado.idEstado;
                cmdEstado.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Estado.Descricao;
                cmdEstado.Parameters.Add("siglaUF", SqlDbType.VarChar).Value = Estado.siglaUF;

                try
                {
                    connection.Open();
                    cmdEstado.ExecuteNonQuery();
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
        public static void Delete(dtoEstado Estado)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbEstado 
                                      WHERE idEstado = @idEstado";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idEstado", SqlDbType.Int).Value = Estado.idEstado;

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

        public static void Delete(int idEstado)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbEstado 
                                      WHERE idEstado = @idEstado";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idEstado", SqlDbType.Int).Value = idEstado;

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
        public static dtoEstado Get(int idEstado)
        {
            dtoEstado Estado = new dtoEstado();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbEstado
                                    WHERE idEstado = @idEstado";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idEstado", SqlDbType.Int).Value = idEstado;

                try
                {
                    connection.Open();
                    SqlDataReader drEstado = cmdMenu.ExecuteReader();

                    if (drEstado.Read())
                        PreencheCampos(drEstado, ref Estado);
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

            return Estado;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoEstado> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoEstado> Estados = new List<dtoEstado>();

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

                    sbCondicao.AppendFormat(@" (tbEstado.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbEstado {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idEstado"));

                SqlCommand cmdEstado = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drEstado = cmdEstado.ExecuteReader();

                    while (drEstado.Read())
                    {
                        dtoEstado Estado = new dtoEstado();

                        PreencheCampos(drEstado, ref Estado);

                        Estados.Add(Estado);
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

            return Estados;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoEstado> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoEstado> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }


        private static void PreencheCampos(SqlDataReader drEstado, ref dtoEstado Estado)
        {

            if (drEstado["idEstado"] != DBNull.Value)
                Estado.idEstado = Convert.ToInt32(drEstado["idEstado"].ToString());

            if (drEstado["dataCadastro"] != DBNull.Value)
                Estado.dataCadastro = Convert.ToDateTime(drEstado["dataCadastro"]);
            else
                Estado.dataCadastro = null;

            if (drEstado["dataUltimaAlteracao"] != DBNull.Value)
                Estado.dataUltimaAlteracao = Convert.ToDateTime(drEstado["dataUltimaAlteracao"]);
            else
                Estado.dataUltimaAlteracao = null;

            if (drEstado["Descricao"] != DBNull.Value)
                Estado.Descricao = drEstado["Descricao"].ToString();

            if (drEstado["siglaUF"] != DBNull.Value)
                Estado.siglaUF = drEstado["siglaUF"].ToString();

        }

        private static void ValidaCampos(ref dtoEstado Estado)
        {

            if (String.IsNullOrEmpty(Estado.Descricao)) { Estado.Descricao = String.Empty; }
            if (String.IsNullOrEmpty(Estado.siglaUF)) { Estado.siglaUF = String.Empty; }

        }

    }
}