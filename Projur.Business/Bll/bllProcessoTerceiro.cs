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
    public class bllProcessoTerceiro
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcessoTerceiro ProcessoTerceiro)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcessoTerceiro(idProcesso, idPessoaTerceiro, Observacoes)
                                            VALUES(@idProcesso, @idPessoaTerceiro, @Observacoes);
                                            SET @idProcessoTerceiro = SCOPE_IDENTITY()";

                SqlCommand cmdProcessoTerceiro = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoTerceiro);

                cmdProcessoTerceiro.Parameters.Add("idProcessoTerceiro", SqlDbType.Int);
                cmdProcessoTerceiro.Parameters["idProcessoTerceiro"].Direction = ParameterDirection.Output;

                cmdProcessoTerceiro.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoTerceiro.idProcesso;
                cmdProcessoTerceiro.Parameters.Add("idPessoaTerceiro", SqlDbType.Int).Value = ProcessoTerceiro.idPessoaTerceiro;
                cmdProcessoTerceiro.Parameters.Add("Observacoes", SqlDbType.VarChar).Value = ProcessoTerceiro.Observacoes;

                try
                {
                    connection.Open();
                    cmdProcessoTerceiro.ExecuteNonQuery();

                    return (int)cmdProcessoTerceiro.Parameters["idProcessoTerceiro"].Value;
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
        public static void Update(dtoProcessoTerceiro ProcessoTerceiro)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcessoTerceiro SET 
                                        idProcesso = @idProcesso,
                                        idPessoaTerceiro = @idPessoaTerceiro,
                                        Observacoes = @Observacoes
                                      WHERE idProcessoTerceiro = @idProcessoTerceiro";

                SqlCommand cmdProcessoTerceiro = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoTerceiro);

                cmdProcessoTerceiro.Parameters.Add("idProcessoTerceiro", SqlDbType.Int).Value = ProcessoTerceiro.idProcessoTerceiro;

                cmdProcessoTerceiro.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoTerceiro.idProcesso;
                cmdProcessoTerceiro.Parameters.Add("idPessoaTerceiro", SqlDbType.Int).Value = ProcessoTerceiro.idPessoaTerceiro;
                cmdProcessoTerceiro.Parameters.Add("Observacoes", SqlDbType.VarChar).Value = ProcessoTerceiro.Observacoes;

                try
                {
                    connection.Open();
                    cmdProcessoTerceiro.ExecuteNonQuery();
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
        public static void Delete(dtoProcessoTerceiro ProcessoTerceiro)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoTerceiro 
                                      WHERE idProcessoTerceiro = @idProcessoTerceiro";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoTerceiro", SqlDbType.Int).Value = ProcessoTerceiro.idProcessoTerceiro;

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

        public static void Delete(int idProcessoTerceiro)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoTerceiro 
                                      WHERE idProcessoTerceiro = @idProcessoTerceiro";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoTerceiro", SqlDbType.Int).Value = idProcessoTerceiro;

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
        public static dtoProcessoTerceiro Get(int idProcessoTerceiro)
        {
            dtoProcessoTerceiro ProcessoTerceiro = new dtoProcessoTerceiro();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoTerceiro
                                    WHERE idProcessoTerceiro = @idProcessoTerceiro";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoTerceiro", SqlDbType.Int).Value = idProcessoTerceiro;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoTerceiro = cmdMenu.ExecuteReader();

                    if (drProcessoTerceiro.Read())
                        PreencheCampos(drProcessoTerceiro, ref ProcessoTerceiro);
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

            return ProcessoTerceiro;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoTerceiro> GetAll(int idProcesso, string SortExpression)
        {
            List<dtoProcessoTerceiro> ProcessoTerceiros = new List<dtoProcessoTerceiro>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                sbCondicao.AppendFormat(@" WHERE (tbProcessoTerceiro.idProcesso = {0})", idProcesso.ToString());

                string stringSQL = String.Format(@"SELECT * 
                                                FROM tbProcessoTerceiro 
                                                {0}
                                                ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idProcessoTerceiro"));

                SqlCommand cmdProcessoTerceiro = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoTerceiro = cmdProcessoTerceiro.ExecuteReader();

                    while (drProcessoTerceiro.Read())
                    {
                        dtoProcessoTerceiro ProcessoTerceiro = new dtoProcessoTerceiro();

                        PreencheCampos(drProcessoTerceiro, ref ProcessoTerceiro);

                        ProcessoTerceiros.Add(ProcessoTerceiro);
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

            return ProcessoTerceiros;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoTerceiro> GetAll()
        {
            return GetAll(0, "");
        }

        private static void PreencheCampos(SqlDataReader drProcessoTerceiro, ref dtoProcessoTerceiro ProcessoTerceiro)
        {

            if (drProcessoTerceiro["idProcessoTerceiro"] != DBNull.Value)
                ProcessoTerceiro.idProcessoTerceiro = Convert.ToInt32(drProcessoTerceiro["idProcessoTerceiro"].ToString());

            if (drProcessoTerceiro["idProcesso"] != DBNull.Value)
                ProcessoTerceiro.idProcesso = Convert.ToInt32(drProcessoTerceiro["idProcesso"].ToString());

            if (drProcessoTerceiro["idPessoaTerceiro"] != DBNull.Value)
                ProcessoTerceiro.idPessoaTerceiro = Convert.ToInt32(drProcessoTerceiro["idPessoaTerceiro"].ToString());

            if (drProcessoTerceiro["Observacoes"] != DBNull.Value)
                ProcessoTerceiro.Observacoes = drProcessoTerceiro["Observacoes"].ToString();

        }

        private static void ValidaCampos(ref dtoProcessoTerceiro ProcessoTerceiro)
        {

            if (String.IsNullOrEmpty(ProcessoTerceiro.Observacoes)) { ProcessoTerceiro.Observacoes = String.Empty; }

        }




    }
}
