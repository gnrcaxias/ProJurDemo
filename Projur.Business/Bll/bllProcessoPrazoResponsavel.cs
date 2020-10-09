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
    public class bllProcessoPrazoResponsavel
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcessoPrazoResponsavel ProcessoPrazoResponsavel)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcessoPrazoResponsavel(idProcessoPrazo, idPessoa)
                                            VALUES(@idProcessoPrazo, @idPessoa);
                                            SET @idProcessoPrazoResponsavel = SCOPE_IDENTITY()";

                SqlCommand cmdProcessoPrazoResponsavel = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoPrazoResponsavel);

                cmdProcessoPrazoResponsavel.Parameters.Add("idProcessoPrazoResponsavel", SqlDbType.Int);
                cmdProcessoPrazoResponsavel.Parameters["idProcessoPrazoResponsavel"].Direction = ParameterDirection.Output;

                cmdProcessoPrazoResponsavel.Parameters.Add("idProcessoPrazo", SqlDbType.Int).Value = ProcessoPrazoResponsavel.idProcessoPrazo;
                cmdProcessoPrazoResponsavel.Parameters.Add("idPessoa", SqlDbType.Int).Value = ProcessoPrazoResponsavel.idPessoa;

                try
                {
                    connection.Open();
                    cmdProcessoPrazoResponsavel.ExecuteNonQuery();

                    return (int)cmdProcessoPrazoResponsavel.Parameters["idProcessoPrazoResponsavel"].Value;
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
        public static void Update(dtoProcessoPrazoResponsavel ProcessoPrazoResponsavel)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcessoPrazoResponsavel SET 
                                        idProcessoPrazo = @idProcessoPrazo,
                                        idPessoa = @idPessoa
                                      WHERE idProcessoPrazoResponsavel = @idProcessoPrazoResponsavel";

                SqlCommand cmdProcessoPrazoResponsavel = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoPrazoResponsavel);

                cmdProcessoPrazoResponsavel.Parameters.Add("idProcessoPrazo", SqlDbType.Int).Value = ProcessoPrazoResponsavel.idProcessoPrazo;
                cmdProcessoPrazoResponsavel.Parameters.Add("idPessoa", SqlDbType.Int).Value = ProcessoPrazoResponsavel.idPessoa;

                try
                {
                    connection.Open();
                    cmdProcessoPrazoResponsavel.ExecuteNonQuery();
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
        public static void Delete(dtoProcessoPrazoResponsavel ProcessoPrazoResponsavel)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoPrazoResponsavel 
                                      WHERE idProcessoPrazoResponsavel = @idProcessoPrazoResponsavel";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoPrazoResponsavel", SqlDbType.Int).Value = ProcessoPrazoResponsavel.idProcessoPrazoResponsavel;

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

        public static void Delete(int idProcessoPrazoResponsavel)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoPrazoResponsavel 
                                      WHERE idProcessoPrazoResponsavel = @idProcessoPrazoResponsavel";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoPrazoResponsavel", SqlDbType.Int).Value = idProcessoPrazoResponsavel;

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

        public static bool Exists(int idProcessoPrazo, int idPessoa)
        {
            bool retorno = false;

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1
                                    FROM tbProcessoPrazoResponsavel
                                    WHERE idPessoa = @idPessoa AND idProcessoPrazo = @idProcessoPrazo";

                SqlCommand cmdProcessoPrazo = new SqlCommand(stringSQL, connection);

                cmdProcessoPrazo.Parameters.Add("idPessoa", SqlDbType.Int).Value = idPessoa;
                cmdProcessoPrazo.Parameters.Add("idProcessoPrazo", SqlDbType.Int).Value = idProcessoPrazo;

                try
                {
                    connection.Open();
                    retorno = (cmdProcessoPrazo.ExecuteScalar() != null && cmdProcessoPrazo.ExecuteScalar().ToString() == "1");
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

            return retorno;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static dtoProcessoPrazoResponsavel Get(int idProcessoPrazoResponsavel)
        {
            dtoProcessoPrazoResponsavel ProcessoPrazoResponsavel = new dtoProcessoPrazoResponsavel();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoPrazoResponsavel
                                    WHERE idProcessoPrazoResponsavel = @idProcessoPrazoResponsavel";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoPrazoResponsavel", SqlDbType.Int).Value = idProcessoPrazoResponsavel;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoPrazoResponsavel = cmdMenu.ExecuteReader();

                    if (drProcessoPrazoResponsavel.Read())
                        PreencheCampos(drProcessoPrazoResponsavel, ref ProcessoPrazoResponsavel);
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

            return ProcessoPrazoResponsavel;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoPrazoResponsavel> GetAll(string SortExpression)
        {
            List<dtoProcessoPrazoResponsavel> ProcessoPrazoResponsavels = new List<dtoProcessoPrazoResponsavel>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = String.Format("SELECT * FROM tbProcessoPrazoResponsavel ORDER BY {0}", (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idProcessoPrazoResponsavel"));

                SqlCommand cmdProcessoPrazoResponsavel = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoPrazoResponsavel = cmdProcessoPrazoResponsavel.ExecuteReader();

                    while (drProcessoPrazoResponsavel.Read())
                    {
                        dtoProcessoPrazoResponsavel ProcessoPrazoResponsavel = new dtoProcessoPrazoResponsavel();

                        PreencheCampos(drProcessoPrazoResponsavel, ref ProcessoPrazoResponsavel);

                        ProcessoPrazoResponsavels.Add(ProcessoPrazoResponsavel);
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

            return ProcessoPrazoResponsavels;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoPrazoResponsavel> GetAll()
        {
            return GetAll("");
        }

        public static List<dtoProcessoPrazoResponsavel> GetByProcessoPrazo(int idProcessoPrazo)
        {
            List<dtoProcessoPrazoResponsavel> ProcessoPrazoResponsavels = new List<dtoProcessoPrazoResponsavel>();

            if (idProcessoPrazo > 0)
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    StringBuilder sbCondicao = new StringBuilder();

                    sbCondicao.AppendFormat(@" WHERE (tbProcessoPrazoResponsavel.idProcessoPrazo = {0})", idProcessoPrazo.ToString());

                    string stringSQL = String.Format("SELECT * FROM tbProcessoPrazoResponsavel {0} ORDER BY idProcessoPrazoResponsavel", sbCondicao.ToString());

                    SqlCommand cmdProcessoPrazoResponsavel = new SqlCommand(stringSQL, connection);

                    try
                    {
                        connection.Open();
                        SqlDataReader drProcessoPrazoResponsavel = cmdProcessoPrazoResponsavel.ExecuteReader();

                        while (drProcessoPrazoResponsavel.Read())
                        {
                            dtoProcessoPrazoResponsavel ProcessoPrazoResponsavel = new dtoProcessoPrazoResponsavel();

                            PreencheCampos(drProcessoPrazoResponsavel, ref ProcessoPrazoResponsavel);

                            ProcessoPrazoResponsavels.Add(ProcessoPrazoResponsavel);
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
            }

            return ProcessoPrazoResponsavels;
        }

        private static void PreencheCampos(SqlDataReader drProcessoPrazoResponsavel, ref dtoProcessoPrazoResponsavel ProcessoPrazoResponsavel)
        {

            if (drProcessoPrazoResponsavel["idProcessoPrazoResponsavel"] != DBNull.Value)
                ProcessoPrazoResponsavel.idProcessoPrazoResponsavel = Convert.ToInt32(drProcessoPrazoResponsavel["idProcessoPrazoResponsavel"].ToString());

            if (drProcessoPrazoResponsavel["idProcessoPrazo"] != DBNull.Value)
                ProcessoPrazoResponsavel.idProcessoPrazo = Convert.ToInt32(drProcessoPrazoResponsavel["idProcessoPrazo"].ToString());

            if (drProcessoPrazoResponsavel["idPessoa"] != DBNull.Value)
                ProcessoPrazoResponsavel.idPessoa = Convert.ToInt32(drProcessoPrazoResponsavel["idPessoa"].ToString());

        }

        private static void ValidaCampos(ref dtoProcessoPrazoResponsavel ProcessoPrazoResponsavel)
        {


        }


    }

}
