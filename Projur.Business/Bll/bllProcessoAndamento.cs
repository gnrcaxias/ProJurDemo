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
    public class bllProcessoAndamento
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcessoAndamento ProcessoAndamento)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcessoAndamento(idProcesso, idProcessoPeca, dataPublicacao, Descricao, visivelCliente)
                                            VALUES(@idProcesso, @idProcessoPeca, @dataPublicacao, @Descricao, @visivelCliente);
                                            SET @idProcessoAndamento = SCOPE_IDENTITY()";

                SqlCommand cmdProcessoAndamento = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoAndamento);

                cmdProcessoAndamento.Parameters.Add("idProcessoAndamento", SqlDbType.Int);
                cmdProcessoAndamento.Parameters["idProcessoAndamento"].Direction = ParameterDirection.Output;

                cmdProcessoAndamento.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoAndamento.idProcesso;
                cmdProcessoAndamento.Parameters.Add("idProcessoPeca", SqlDbType.Int).Value = ProcessoAndamento.idProcessoPeca;

                if (ProcessoAndamento.dataPublicacao != null)
                    cmdProcessoAndamento.Parameters.Add("dataPublicacao", SqlDbType.DateTime).Value = ProcessoAndamento.dataPublicacao;
                else
                    cmdProcessoAndamento.Parameters.Add("dataPublicacao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdProcessoAndamento.Parameters.Add("Descricao", SqlDbType.VarChar).Value = ProcessoAndamento.Descricao;
                cmdProcessoAndamento.Parameters.Add("visivelCliente", SqlDbType.Bit).Value = ProcessoAndamento.visivelCliente;

                try
                {
                    connection.Open();
                    cmdProcessoAndamento.ExecuteNonQuery();

                    return (int)cmdProcessoAndamento.Parameters["idProcessoAndamento"].Value;
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
        public static void Update(dtoProcessoAndamento ProcessoAndamento)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcessoAndamento SET 
                                        idProcesso = @idProcesso,
                                        idProcessoPeca = @idProcessoPeca,
                                        dataPublicacao = @dataPublicacao,
                                        Descricao = @Descricao,
                                        visivelCliente = @visivelCliente
                                      WHERE idProcessoAndamento = @idProcessoAndamento";

                SqlCommand cmdProcessoAndamento = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoAndamento);

                cmdProcessoAndamento.Parameters.Add("idProcessoAndamento", SqlDbType.Int).Value = ProcessoAndamento.idProcessoAndamento;

                cmdProcessoAndamento.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoAndamento.idProcesso;
                cmdProcessoAndamento.Parameters.Add("idProcessoPeca", SqlDbType.Int).Value = ProcessoAndamento.idProcessoPeca;

                if (ProcessoAndamento.dataPublicacao != null)
                    cmdProcessoAndamento.Parameters.Add("dataPublicacao", SqlDbType.DateTime).Value = ProcessoAndamento.dataPublicacao;
                else
                    cmdProcessoAndamento.Parameters.Add("dataPublicacao", SqlDbType.DateTime).Value = DBNull.Value;


                cmdProcessoAndamento.Parameters.Add("Descricao", SqlDbType.VarChar).Value = ProcessoAndamento.Descricao;
                cmdProcessoAndamento.Parameters.Add("visivelCliente", SqlDbType.Bit).Value = ProcessoAndamento.visivelCliente;

                try
                {
                    connection.Open();
                    cmdProcessoAndamento.ExecuteNonQuery();
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
        public static void Delete(dtoProcessoAndamento ProcessoAndamento)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoAndamento 
                                      WHERE idProcessoAndamento = @idProcessoAndamento";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoAndamento", SqlDbType.Int).Value = ProcessoAndamento.idProcessoAndamento;

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

        public static void Delete(int idProcessoAndamento)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoAndamento 
                                      WHERE idProcessoAndamento = @idProcessoAndamento";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoAndamento", SqlDbType.Int).Value = idProcessoAndamento;

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
        public static dtoProcessoAndamento Get(int idProcessoAndamento)
        {
            dtoProcessoAndamento ProcessoAndamento = new dtoProcessoAndamento();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoAndamento
                                    WHERE idProcessoAndamento = @idProcessoAndamento";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoAndamento", SqlDbType.Int).Value = idProcessoAndamento;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoAndamento = cmdMenu.ExecuteReader();

                    if (drProcessoAndamento.Read())
                        PreencheCampos(drProcessoAndamento, ref ProcessoAndamento);
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

            return ProcessoAndamento;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoAndamento> GetAll(int idProcesso, string SortExpression)
        {
            List<dtoProcessoAndamento> ProcessoAndamentos = new List<dtoProcessoAndamento>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                sbCondicao.AppendFormat(@" WHERE (tbProcessoAndamento.idProcesso = {0})", idProcesso.ToString());

                string stringSQL = String.Format(@"SELECT * 
                                                FROM tbProcessoAndamento 
                                                {0} 
                                                ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idProcessoAndamento"));

                SqlCommand cmdProcessoAndamento = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoAndamento = cmdProcessoAndamento.ExecuteReader();

                    while (drProcessoAndamento.Read())
                    {
                        dtoProcessoAndamento ProcessoAndamento = new dtoProcessoAndamento();

                        PreencheCampos(drProcessoAndamento, ref ProcessoAndamento);

                        ProcessoAndamentos.Add(ProcessoAndamento);
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

            return ProcessoAndamentos;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoAndamento> GetAll()
        {
            return GetAll(0, "");
        }

        private static void PreencheCampos(SqlDataReader drProcessoAndamento, ref dtoProcessoAndamento ProcessoAndamento)
        {

            if (drProcessoAndamento["idProcessoAndamento"] != DBNull.Value)
                ProcessoAndamento.idProcessoAndamento = Convert.ToInt32(drProcessoAndamento["idProcessoAndamento"].ToString());

            if (drProcessoAndamento["idProcesso"] != DBNull.Value)
                ProcessoAndamento.idProcesso = Convert.ToInt32(drProcessoAndamento["idProcesso"].ToString());

            if (drProcessoAndamento["idProcessoPeca"] != DBNull.Value)
                ProcessoAndamento.idProcessoPeca = Convert.ToInt32(drProcessoAndamento["idProcessoPeca"].ToString());

            if (drProcessoAndamento["dataPublicacao"] != DBNull.Value)
                ProcessoAndamento.dataPublicacao = Convert.ToDateTime(drProcessoAndamento["dataPublicacao"]);
            else
                ProcessoAndamento.dataPublicacao = null;

            if (drProcessoAndamento["Descricao"] != DBNull.Value)
                ProcessoAndamento.Descricao = drProcessoAndamento["Descricao"].ToString();

            if (drProcessoAndamento["visivelCliente"] != DBNull.Value)
                ProcessoAndamento.visivelCliente = Convert.ToBoolean(drProcessoAndamento["visivelCliente"].ToString());

        }

        private static void ValidaCampos(ref dtoProcessoAndamento ProcessoAndamento)
        {

            if (String.IsNullOrEmpty(ProcessoAndamento.Descricao)) { ProcessoAndamento.Descricao = String.Empty; }

        }

    }

}
