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
    public class bllAreaProcessual
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoAreaProcessual AreaProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbAreaProcessual(Descricao, dataCadastro)
                                            VALUES(@Descricao, getdate());
                                            SET @idAreaProcessual = SCOPE_IDENTITY()";

                SqlCommand cmdAreaProcessual = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref AreaProcessual);

                cmdAreaProcessual.Parameters.Add("idAreaProcessual", SqlDbType.Int);
                cmdAreaProcessual.Parameters["idAreaProcessual"].Direction = ParameterDirection.Output;

                cmdAreaProcessual.Parameters.Add("Descricao", SqlDbType.VarChar).Value = AreaProcessual.Descricao;

                try
                {
                    connection.Open();
                    cmdAreaProcessual.ExecuteNonQuery();

                    return (int)cmdAreaProcessual.Parameters["idAreaProcessual"].Value;
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
        public static void Update(dtoAreaProcessual AreaProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbAreaProcessual SET 
                                        Descricao = @Descricao,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idAreaProcessual = @idAreaProcessual";

                SqlCommand cmdAreaProcessual = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref AreaProcessual);

                cmdAreaProcessual.Parameters.Add("idAreaProcessual", SqlDbType.Int).Value = AreaProcessual.idAreaProcessual;
                cmdAreaProcessual.Parameters.Add("Descricao", SqlDbType.VarChar).Value = AreaProcessual.Descricao;

                try
                {
                    connection.Open();
                    cmdAreaProcessual.ExecuteNonQuery();
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
        public static void Delete(dtoAreaProcessual AreaProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbAreaProcessual 
                                      WHERE idAreaProcessual = @idAreaProcessual";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idAreaProcessual", SqlDbType.Int).Value = AreaProcessual.idAreaProcessual;

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

        public static void Delete(int idAreaProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbAreaProcessual 
                                      WHERE idAreaProcessual = @idAreaProcessual";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idAreaProcessual", SqlDbType.Int).Value = idAreaProcessual;

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
        public static dtoAreaProcessual Get(int idAreaProcessual)
        {
            dtoAreaProcessual AreaProcessual = new dtoAreaProcessual();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbAreaProcessual
                                    WHERE idAreaProcessual = @idAreaProcessual";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idAreaProcessual", SqlDbType.Int).Value = idAreaProcessual;

                try
                {
                    connection.Open();
                    SqlDataReader drAreaProcessual = cmdMenu.ExecuteReader();

                    if (drAreaProcessual.Read())
                        PreencheCampos(drAreaProcessual, ref AreaProcessual);
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

            return AreaProcessual;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAreaProcessual> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoAreaProcessual> AreaProcessuals = new List<dtoAreaProcessual>();

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

                    sbCondicao.AppendFormat(@" (tbAreaProcessual.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbAreaProcessual {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idAreaProcessual"));

                SqlCommand cmdAreaProcessual = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drAreaProcessual = cmdAreaProcessual.ExecuteReader();

                    while (drAreaProcessual.Read())
                    {
                        dtoAreaProcessual AreaProcessual = new dtoAreaProcessual();

                        PreencheCampos(drAreaProcessual, ref AreaProcessual);

                        AreaProcessuals.Add(AreaProcessual);
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

            return AreaProcessuals;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAreaProcessual> GetAll()
        {
            return GetAll("", "");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAreaProcessual> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }


        private static void PreencheCampos(SqlDataReader drAreaProcessual, ref dtoAreaProcessual AreaProcessual)
        {

            if (drAreaProcessual["idAreaProcessual"] != DBNull.Value)
                AreaProcessual.idAreaProcessual = Convert.ToInt32(drAreaProcessual["idAreaProcessual"].ToString());

            if (drAreaProcessual["dataCadastro"] != DBNull.Value)
                AreaProcessual.dataCadastro = Convert.ToDateTime(drAreaProcessual["dataCadastro"]);
            else
                AreaProcessual.dataCadastro = null;

            if (drAreaProcessual["dataUltimaAlteracao"] != DBNull.Value)
                AreaProcessual.dataUltimaAlteracao = Convert.ToDateTime(drAreaProcessual["dataUltimaAlteracao"]);
            else
                AreaProcessual.dataUltimaAlteracao = null;

            if (drAreaProcessual["Descricao"] != DBNull.Value)
                AreaProcessual.Descricao = drAreaProcessual["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoAreaProcessual AreaProcessual)
        {
            
            if (String.IsNullOrEmpty(AreaProcessual.Descricao)) { AreaProcessual.Descricao = String.Empty; }

        }

    }
}