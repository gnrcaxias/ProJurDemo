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
    public class bllProcessoPeca
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcessoPeca ProcessoPeca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcessoPeca(idProcesso, Descricao, nomeArquivo, caminhoArquivo, visivelCliente, idCategoriaPecaProcessual)
                                            VALUES(@idProcesso, @Descricao, @nomeArquivo, @caminhoArquivo, @visivelCliente, @idCategoriaPecaProcessual);
                                            SET @idProcessoPeca = SCOPE_IDENTITY()";

                SqlCommand cmdProcessoPeca = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoPeca);

                cmdProcessoPeca.Parameters.Add("idProcessoPeca", SqlDbType.Int);
                cmdProcessoPeca.Parameters["idProcessoPeca"].Direction = ParameterDirection.Output;

                cmdProcessoPeca.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoPeca.idProcesso;
                cmdProcessoPeca.Parameters.Add("Descricao", SqlDbType.VarChar).Value = ProcessoPeca.Descricao;
                cmdProcessoPeca.Parameters.Add("nomeArquivo", SqlDbType.VarChar).Value = ProcessoPeca.nomeArquivo;
                cmdProcessoPeca.Parameters.Add("caminhoArquivo", SqlDbType.VarChar).Value = ProcessoPeca.caminhoArquivo;
                cmdProcessoPeca.Parameters.Add("visivelCliente", SqlDbType.Bit).Value = ProcessoPeca.visivelCliente;
                cmdProcessoPeca.Parameters.Add("idCategoriaPecaProcessual", SqlDbType.Int).Value = ProcessoPeca.idCategoriaPecaProcessual;

                try
                {
                    connection.Open();
                    cmdProcessoPeca.ExecuteNonQuery();

                    return (int)cmdProcessoPeca.Parameters["idProcessoPeca"].Value;
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
        public static void Update(dtoProcessoPeca ProcessoPeca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcessoPeca SET 
                                        idProcesso = @idProcesso,
                                        Descricao = @Descricao,
                                        nomeArquivo = @nomeArquivo,
                                        caminhoArquivo = @caminhoArquivo,
                                        visivelCliente = @visivelCliente,
                                        idCategoriaPecaProcessual = @idCategoriaPecaProcessual
                                      WHERE idProcessoPeca = @idProcessoPeca";

                SqlCommand cmdProcessoPeca = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoPeca);

                cmdProcessoPeca.Parameters.Add("idProcessoPeca", SqlDbType.Int).Value = ProcessoPeca.idProcessoPeca;

                cmdProcessoPeca.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoPeca.idProcesso;
                cmdProcessoPeca.Parameters.Add("Descricao", SqlDbType.VarChar).Value = ProcessoPeca.Descricao;
                cmdProcessoPeca.Parameters.Add("nomeArquivo", SqlDbType.VarChar).Value = ProcessoPeca.nomeArquivo;
                cmdProcessoPeca.Parameters.Add("caminhoArquivo", SqlDbType.VarChar).Value = ProcessoPeca.caminhoArquivo;
                cmdProcessoPeca.Parameters.Add("visivelCliente", SqlDbType.Bit).Value = ProcessoPeca.visivelCliente;
                cmdProcessoPeca.Parameters.Add("idCategoriaPecaProcessual", SqlDbType.Int).Value = ProcessoPeca.idCategoriaPecaProcessual;

                try
                {
                    connection.Open();
                    cmdProcessoPeca.ExecuteNonQuery();
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
        public static void Delete(dtoProcessoPeca ProcessoPeca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoPeca 
                                      WHERE idProcessoPeca = @idProcessoPeca";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoPeca", SqlDbType.Int).Value = ProcessoPeca.idProcessoPeca;

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

        public static void Delete(int idProcessoPeca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoPeca 
                                      WHERE idProcessoPeca = @idProcessoPeca";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoPeca", SqlDbType.Int).Value = idProcessoPeca;

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


        public static bool Exists(int idPessoa)
        {
            bool retorno = false;

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1
                                    FROM idProcessoPeca
                                    WHERE idProcessoPeca.idPessoa = @idPessoa";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = idPessoa;

                try
                {
                    connection.Open();
                    retorno = (cmdPessoa.ExecuteScalar() != null && cmdPessoa.ExecuteScalar().ToString() == "1");
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
        public static dtoProcessoPeca Get(int idProcessoPeca)
        {
            dtoProcessoPeca ProcessoPeca = new dtoProcessoPeca();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoPeca
                                    WHERE idProcessoPeca = @idProcessoPeca";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoPeca", SqlDbType.Int).Value = idProcessoPeca;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoPeca = cmdMenu.ExecuteReader();

                    if (drProcessoPeca.Read())
                        PreencheCampos(drProcessoPeca, ref ProcessoPeca);
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

            return ProcessoPeca;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static dtoProcessoPeca GetByProcessoAndamento(int idProcessoAndamento)
        {
            dtoProcessoPeca ProcessoPeca = new dtoProcessoPeca();;

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoPeca
                                    WHERE idProcessoPeca = (SELECT idProcessoPeca FROM tbProcessoAndamento WHERE tbProcessoAndamento.idProcessoAndamento = @idProcessoAndamento)";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoAndamento", SqlDbType.Int).Value = idProcessoAndamento;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoPeca = cmdMenu.ExecuteReader();

                    if (drProcessoPeca.Read())
                        PreencheCampos(drProcessoPeca, ref ProcessoPeca);
                    else
                        ProcessoPeca = null;
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

            return ProcessoPeca;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoPeca> GetAll(int idProcesso, string SortExpression)
        {
            List<dtoProcessoPeca> ProcessoPecas = new List<dtoProcessoPeca>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                sbCondicao.AppendFormat(@" WHERE (tbProcessoPeca.idProcesso = {0})", idProcesso.ToString());

                string stringSQL = String.Format(@"SELECT * 
                                                FROM tbProcessoPeca 
                                                {0}
                                                ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idProcessoPeca"));

                SqlCommand cmdProcessoPeca = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoPeca = cmdProcessoPeca.ExecuteReader();

                    while (drProcessoPeca.Read())
                    {
                        dtoProcessoPeca ProcessoPeca = new dtoProcessoPeca();

                        PreencheCampos(drProcessoPeca, ref ProcessoPeca);

                        ProcessoPecas.Add(ProcessoPeca);
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

            return ProcessoPecas;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoPeca> GetAll()
        {
            return GetAll(0, "");
        }

        private static void PreencheCampos(SqlDataReader drProcessoPeca, ref dtoProcessoPeca ProcessoPeca)
        {

            if (drProcessoPeca["idProcessoPeca"] != DBNull.Value)
                ProcessoPeca.idProcessoPeca = Convert.ToInt32(drProcessoPeca["idProcessoPeca"].ToString());

            if (drProcessoPeca["idProcesso"] != DBNull.Value)
                ProcessoPeca.idProcesso = Convert.ToInt32(drProcessoPeca["idProcesso"].ToString());

            if (drProcessoPeca["Descricao"] != DBNull.Value)
                ProcessoPeca.Descricao = drProcessoPeca["Descricao"].ToString();

            if (drProcessoPeca["nomeArquivo"] != DBNull.Value)
                ProcessoPeca.nomeArquivo = drProcessoPeca["nomeArquivo"].ToString();

            if (drProcessoPeca["caminhoArquivo"] != DBNull.Value)
                ProcessoPeca.caminhoArquivo = drProcessoPeca["caminhoArquivo"].ToString();

            if (drProcessoPeca["visivelCliente"] != DBNull.Value)
                ProcessoPeca.visivelCliente = Convert.ToBoolean(drProcessoPeca["visivelCliente"].ToString());

            if (drProcessoPeca["idCategoriaPecaProcessual"] != DBNull.Value)
                ProcessoPeca.idCategoriaPecaProcessual = Convert.ToInt32(drProcessoPeca["idCategoriaPecaProcessual"].ToString());

        }

        private static void ValidaCampos(ref dtoProcessoPeca ProcessoPeca)
        {

            if (String.IsNullOrEmpty(ProcessoPeca.Descricao)) { ProcessoPeca.Descricao = String.Empty; }
            if (String.IsNullOrEmpty(ProcessoPeca.nomeArquivo)) { ProcessoPeca.nomeArquivo = String.Empty; }
            if (String.IsNullOrEmpty(ProcessoPeca.caminhoArquivo)) { ProcessoPeca.caminhoArquivo = String.Empty; }

        }



    }
}
