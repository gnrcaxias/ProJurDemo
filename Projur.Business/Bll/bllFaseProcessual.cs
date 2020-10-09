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
    public class bllFaseProcessual
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoFaseProcessual FaseProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbFaseProcessual(Descricao, dataCadastro)
                                            VALUES(@Descricao, getdate());
                                            SET @idFaseProcessual = SCOPE_IDENTITY()";

                SqlCommand cmdFaseProcessual = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref FaseProcessual);

                cmdFaseProcessual.Parameters.Add("idFaseProcessual", SqlDbType.Int);
                cmdFaseProcessual.Parameters["idFaseProcessual"].Direction = ParameterDirection.Output;

                cmdFaseProcessual.Parameters.Add("Descricao", SqlDbType.VarChar).Value = FaseProcessual.Descricao;

                try
                {
                    connection.Open();
                    cmdFaseProcessual.ExecuteNonQuery();

                    return (int)cmdFaseProcessual.Parameters["idFaseProcessual"].Value;
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
        public static void Update(dtoFaseProcessual FaseProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbFaseProcessual SET 
                                        Descricao = @Descricao,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idFaseProcessual = @idFaseProcessual";

                SqlCommand cmdFaseProcessual = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref FaseProcessual);

                cmdFaseProcessual.Parameters.Add("idFaseProcessual", SqlDbType.Int).Value = FaseProcessual.idFaseProcessual;
                cmdFaseProcessual.Parameters.Add("Descricao", SqlDbType.VarChar).Value = FaseProcessual.Descricao;

                try
                {
                    connection.Open();
                    cmdFaseProcessual.ExecuteNonQuery();
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
        public static void Delete(dtoFaseProcessual FaseProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbFaseProcessual 
                                      WHERE idFaseProcessual = @idFaseProcessual";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idFaseProcessual", SqlDbType.Int).Value = FaseProcessual.idFaseProcessual;

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

        public static void Delete(int idFaseProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbFaseProcessual 
                                      WHERE idFaseProcessual = @idFaseProcessual";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idFaseProcessual", SqlDbType.Int).Value = idFaseProcessual;

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
        public static dtoFaseProcessual Get(int idFaseProcessual)
        {
            dtoFaseProcessual FaseProcessual = new dtoFaseProcessual();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbFaseProcessual
                                    WHERE idFaseProcessual = @idFaseProcessual";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idFaseProcessual", SqlDbType.Int).Value = idFaseProcessual;

                try
                {
                    connection.Open();
                    SqlDataReader drFaseProcessual = cmdMenu.ExecuteReader();

                    if (drFaseProcessual.Read())
                        PreencheCampos(drFaseProcessual, ref FaseProcessual);
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

            return FaseProcessual;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoFaseProcessual> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoFaseProcessual> FaseProcessuals = new List<dtoFaseProcessual>();

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

                    sbCondicao.AppendFormat(@" (tbFaseProcessual.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbFaseProcessual {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idFaseProcessual"));

                SqlCommand cmdFaseProcessual = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drFaseProcessual = cmdFaseProcessual.ExecuteReader();

                    while (drFaseProcessual.Read())
                    {
                        dtoFaseProcessual FaseProcessual = new dtoFaseProcessual();

                        PreencheCampos(drFaseProcessual, ref FaseProcessual);

                        FaseProcessuals.Add(FaseProcessual);
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

            return FaseProcessuals;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoFaseProcessual> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoFaseProcessual> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }

        private static void PreencheCampos(SqlDataReader drFaseProcessual, ref dtoFaseProcessual FaseProcessual)
        {

            if (drFaseProcessual["idFaseProcessual"] != DBNull.Value)
                FaseProcessual.idFaseProcessual = Convert.ToInt32(drFaseProcessual["idFaseProcessual"].ToString());

            if (drFaseProcessual["dataCadastro"] != DBNull.Value)
                FaseProcessual.dataCadastro = Convert.ToDateTime(drFaseProcessual["dataCadastro"]);
            else
                FaseProcessual.dataCadastro = null;

            if (drFaseProcessual["dataUltimaAlteracao"] != DBNull.Value)
                FaseProcessual.dataUltimaAlteracao = Convert.ToDateTime(drFaseProcessual["dataUltimaAlteracao"]);
            else
                FaseProcessual.dataUltimaAlteracao = null;

            if (drFaseProcessual["Descricao"] != DBNull.Value)
                FaseProcessual.Descricao = drFaseProcessual["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoFaseProcessual FaseProcessual)
        {

            if (String.IsNullOrEmpty(FaseProcessual.Descricao)) { FaseProcessual.Descricao = String.Empty; }

        }

    }
}