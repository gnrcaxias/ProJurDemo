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
    public class bllProcessoAdvogado
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcessoAdvogado ProcessoAdvogado)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcessoAdvogado(idProcesso, idPessoaAdvogado, tipoAdvogado)
                                            VALUES(@idProcesso, @idPessoaAdvogado, @tipoAdvogado);
                                            SET @idProcessoAdvogado = SCOPE_IDENTITY()";

                SqlCommand cmdProcessoAdvogado = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoAdvogado);

                cmdProcessoAdvogado.Parameters.Add("idProcessoAdvogado", SqlDbType.Int);
                cmdProcessoAdvogado.Parameters["idProcessoAdvogado"].Direction = ParameterDirection.Output;

                cmdProcessoAdvogado.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoAdvogado.idProcesso;
                cmdProcessoAdvogado.Parameters.Add("idPessoaAdvogado", SqlDbType.Int).Value = ProcessoAdvogado.idPessoaAdvogado;
                cmdProcessoAdvogado.Parameters.Add("tipoAdvogado", SqlDbType.VarChar).Value = ProcessoAdvogado.tipoAdvogado;

                try
                {
                    connection.Open();
                    cmdProcessoAdvogado.ExecuteNonQuery();

                    return (int)cmdProcessoAdvogado.Parameters["idProcessoAdvogado"].Value;
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
        public static void Update(dtoProcessoAdvogado ProcessoAdvogado)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcessoAdvogado SET 
                                        idProcesso = @idProcesso,
                                        idPessoaAdvogado = @idPessoaAdvogado,
                                        tipoAdvogado = @tipoAdvogado
                                      WHERE idProcessoAdvogado = @idProcessoAdvogado";

                SqlCommand cmdProcessoAdvogado = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoAdvogado);

                cmdProcessoAdvogado.Parameters.Add("idProcessoAdvogado", SqlDbType.Int).Value = ProcessoAdvogado.idProcessoAdvogado;

                cmdProcessoAdvogado.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoAdvogado.idProcesso;
                cmdProcessoAdvogado.Parameters.Add("idPessoaAdvogado", SqlDbType.Int).Value = ProcessoAdvogado.idPessoaAdvogado;
                cmdProcessoAdvogado.Parameters.Add("tipoAdvogado", SqlDbType.VarChar).Value = ProcessoAdvogado.tipoAdvogado;

                try
                {
                    connection.Open();
                    cmdProcessoAdvogado.ExecuteNonQuery();
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
        public static void Delete(dtoProcessoAdvogado ProcessoAdvogado)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoAdvogado 
                                      WHERE idProcessoAdvogado = @idProcessoAdvogado";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoAdvogado", SqlDbType.Int).Value = ProcessoAdvogado.idProcessoAdvogado;

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

        public static void Delete(int idProcessoAdvogado)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoAdvogado 
                                      WHERE idProcessoAdvogado = @idProcessoAdvogado";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoAdvogado", SqlDbType.Int).Value = idProcessoAdvogado;

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
        public static dtoProcessoAdvogado Get(int idProcessoAdvogado)
        {
            dtoProcessoAdvogado ProcessoAdvogado = new dtoProcessoAdvogado();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoAdvogado
                                    WHERE idProcessoAdvogado = @idProcessoAdvogado";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoAdvogado", SqlDbType.Int).Value = idProcessoAdvogado;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoAdvogado = cmdMenu.ExecuteReader();

                    if (drProcessoAdvogado.Read())
                        PreencheCampos(drProcessoAdvogado, ref ProcessoAdvogado);
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

            return ProcessoAdvogado;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoAdvogado> GetAll(int idProcesso, string SortExpression)
        {
            List<dtoProcessoAdvogado> ProcessoAdvogados = new List<dtoProcessoAdvogado>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                sbCondicao.AppendFormat(@" WHERE (tbProcessoAdvogado.idProcesso = {0})", idProcesso.ToString());

                string stringSQL = String.Format(@"SELECT * 
                                                FROM tbProcessoAdvogado 
                                                {0} ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idProcessoAdvogado"));

                SqlCommand cmdProcessoAdvogado = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoAdvogado = cmdProcessoAdvogado.ExecuteReader();

                    while (drProcessoAdvogado.Read())
                    {
                        dtoProcessoAdvogado ProcessoAdvogado = new dtoProcessoAdvogado();

                        PreencheCampos(drProcessoAdvogado, ref ProcessoAdvogado);

                        ProcessoAdvogados.Add(ProcessoAdvogado);
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

            return ProcessoAdvogados;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoAdvogado> GetAll()
        {
            return GetAll(0, "");
        }

        private static void PreencheCampos(SqlDataReader drProcessoAdvogado, ref dtoProcessoAdvogado processoAdvogado)
        {

            if (drProcessoAdvogado["idProcessoAdvogado"] != DBNull.Value)
                processoAdvogado.idProcessoAdvogado = Convert.ToInt32(drProcessoAdvogado["idProcessoAdvogado"].ToString());

            if (drProcessoAdvogado["idProcesso"] != DBNull.Value)
                processoAdvogado.idProcesso = Convert.ToInt32(drProcessoAdvogado["idProcesso"].ToString());

            if (drProcessoAdvogado["idPessoaAdvogado"] != DBNull.Value)
                processoAdvogado.idPessoaAdvogado = Convert.ToInt32(drProcessoAdvogado["idPessoaAdvogado"].ToString());

            if (drProcessoAdvogado["tipoAdvogado"] != DBNull.Value)
                processoAdvogado.tipoAdvogado = drProcessoAdvogado["tipoAdvogado"].ToString();

        }

        private static void ValidaCampos(ref dtoProcessoAdvogado processoAdvogado)
        {

            if (String.IsNullOrEmpty(processoAdvogado.tipoAdvogado)) { processoAdvogado.tipoAdvogado = String.Empty; }

        }

        public static string RetornaDescricaoTipoAdvogado(object tipoAdvogado)
        {
            string retorno = String.Empty;

            if (tipoAdvogado != null)
            {
                switch ((string)tipoAdvogado)
                {
                    case "R":
                        retorno = "Réu";
                        break;

                    case "A":
                        retorno = "Autor";
                        break;
                }
            }

            return retorno;
        }

    }
}
