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
    public class bllProcessoDespesa
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcessoDespesa ProcessoDespesa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcessoDespesa(idProcesso, Descricao, Valor, Observacoes)
                                            VALUES(@idProcesso, @Descricao, @Valor, @Observacoes);
                                            SET @idProcessoDespesa = SCOPE_IDENTITY()";

                SqlCommand cmdProcessoDespesa = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoDespesa);

                cmdProcessoDespesa.Parameters.Add("idProcessoDespesa", SqlDbType.Int);
                cmdProcessoDespesa.Parameters["idProcessoDespesa"].Direction = ParameterDirection.Output;

                cmdProcessoDespesa.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoDespesa.idProcesso;
                cmdProcessoDespesa.Parameters.Add("Descricao", SqlDbType.VarChar).Value = ProcessoDespesa.Descricao;
                cmdProcessoDespesa.Parameters.Add("Valor", SqlDbType.Float).Value = ProcessoDespesa.Valor;
                cmdProcessoDespesa.Parameters.Add("Observacoes", SqlDbType.VarChar).Value = ProcessoDespesa.Observacoes;

                try
                {
                    connection.Open();
                    cmdProcessoDespesa.ExecuteNonQuery();

                    return (int)cmdProcessoDespesa.Parameters["idProcessoDespesa"].Value;
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
        public static void Update(dtoProcessoDespesa ProcessoDespesa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcessoDespesa SET 
                                        idProcesso = @idProcesso,
                                        Descricao = @Descricao,
                                        Valor = @Valor,
                                        Observacoes = @Observacoes
                                      WHERE idProcessoDespesa = @idProcessoDespesa";

                SqlCommand cmdProcessoDespesa = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoDespesa);

                cmdProcessoDespesa.Parameters.Add("idProcessoDespesa", SqlDbType.Int).Value = ProcessoDespesa.idProcessoDespesa;

                cmdProcessoDespesa.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoDespesa.idProcesso;
                cmdProcessoDespesa.Parameters.Add("Descricao", SqlDbType.VarChar).Value = ProcessoDespesa.Descricao;
                cmdProcessoDespesa.Parameters.Add("Valor", SqlDbType.Float).Value = ProcessoDespesa.Valor;
                cmdProcessoDespesa.Parameters.Add("Observacoes", SqlDbType.VarChar).Value = ProcessoDespesa.Observacoes;

                try
                {
                    connection.Open();
                    cmdProcessoDespesa.ExecuteNonQuery();
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
        public static void Delete(dtoProcessoDespesa ProcessoDespesa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoDespesa 
                                      WHERE idProcessoDespesa = @idProcessoDespesa";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoDespesa", SqlDbType.Int).Value = ProcessoDespesa.idProcessoDespesa;

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

        public static void Delete(int idProcessoDespesa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoDespesa 
                                      WHERE idProcessoDespesa = @idProcessoDespesa";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoDespesa", SqlDbType.Int).Value = idProcessoDespesa;

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
        public static dtoProcessoDespesa Get(int idProcessoDespesa)
        {
            dtoProcessoDespesa ProcessoDespesa = new dtoProcessoDespesa();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoDespesa
                                    WHERE idProcessoDespesa = @idProcessoDespesa";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoDespesa", SqlDbType.Int).Value = idProcessoDespesa;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoDespesa = cmdMenu.ExecuteReader();

                    if (drProcessoDespesa.Read())
                        PreencheCampos(drProcessoDespesa, ref ProcessoDespesa);
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

            return ProcessoDespesa;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoDespesa> GetAll(int idProcesso, string SortExpression)
        {
            List<dtoProcessoDespesa> ProcessoDespesas = new List<dtoProcessoDespesa>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                sbCondicao.AppendFormat(@" WHERE (tbProcessoDespesa.idProcesso = {0})", idProcesso.ToString());

                string stringSQL = String.Format(@"SELECT * 
                                                FROM tbProcessoDespesa 
                                                {0} 
                                                ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idProcessoDespesa"));

                SqlCommand cmdProcessoDespesa = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoDespesa = cmdProcessoDespesa.ExecuteReader();

                    while (drProcessoDespesa.Read())
                    {
                        dtoProcessoDespesa ProcessoDespesa = new dtoProcessoDespesa();

                        PreencheCampos(drProcessoDespesa, ref ProcessoDespesa);

                        ProcessoDespesas.Add(ProcessoDespesa);
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

            return ProcessoDespesas;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoDespesa> GetAll()
        {
            return GetAll(0, "");
        }

        private static void PreencheCampos(SqlDataReader drProcessoDespesa, ref dtoProcessoDespesa ProcessoDespesa)
        {

            if (drProcessoDespesa["idProcessoDespesa"] != DBNull.Value)
                ProcessoDespesa.idProcessoDespesa = Convert.ToInt32(drProcessoDespesa["idProcessoDespesa"].ToString());

            if (drProcessoDespesa["idProcesso"] != DBNull.Value)
                ProcessoDespesa.idProcesso = Convert.ToInt32(drProcessoDespesa["idProcesso"].ToString());

            if (drProcessoDespesa["Descricao"] != DBNull.Value)
                ProcessoDespesa.Descricao = drProcessoDespesa["Descricao"].ToString();

            if (drProcessoDespesa["Valor"] != DBNull.Value)
                ProcessoDespesa.Valor = Convert.ToSingle(drProcessoDespesa["Valor"].ToString());

            if (drProcessoDespesa["Observacoes"] != DBNull.Value)
                ProcessoDespesa.Observacoes = drProcessoDespesa["Observacoes"].ToString();

        }

        private static void ValidaCampos(ref dtoProcessoDespesa ProcessoDespesa)
        {

            if (String.IsNullOrEmpty(ProcessoDespesa.Descricao)) { ProcessoDespesa.Descricao = String.Empty; }
            if (String.IsNullOrEmpty(ProcessoDespesa.Observacoes)) { ProcessoDespesa.Observacoes = String.Empty; }

        }



    }
}
