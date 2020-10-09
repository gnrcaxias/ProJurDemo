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
    public class bllProcessoPrazo
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcessoPrazo ProcessoPrazo)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcessoPrazo(idProcesso, idUsuarioCadastro, dataCadastro, idTipoPrazo, Descricao, dataPublicacao, dataVencimento, dataConclusao, quantidadeDiasPrazo, situacaoPrazo, idPessoaAdvogadoResponsavel)
                                            VALUES(@idProcesso, @idUsuarioCadastro, getdate(), @idTipoPrazo, @Descricao, @dataPublicacao, @dataVencimento, @dataConclusao, @quantidadeDiasPrazo, @situacaoPrazo, @idPessoaAdvogadoResponsavel);
                                            SET @idProcessoPrazo = SCOPE_IDENTITY()";

                SqlCommand cmdProcessoPrazo = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoPrazo);

                cmdProcessoPrazo.Parameters.Add("idProcessoPrazo", SqlDbType.Int);
                cmdProcessoPrazo.Parameters["idProcessoPrazo"].Direction = ParameterDirection.Output;

                cmdProcessoPrazo.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoPrazo.idProcesso;
                cmdProcessoPrazo.Parameters.Add("idUsuarioCadastro", SqlDbType.Int).Value = ProcessoPrazo.idUsuarioCadastro;

                cmdProcessoPrazo.Parameters.Add("idTipoPrazo", SqlDbType.Int).Value = ProcessoPrazo.idTipoPrazo;
                cmdProcessoPrazo.Parameters.Add("Descricao", SqlDbType.VarChar).Value = ProcessoPrazo.Descricao;

                if (ProcessoPrazo.dataPublicacao != null)
                    cmdProcessoPrazo.Parameters.Add("dataPublicacao", SqlDbType.DateTime).Value = ProcessoPrazo.dataPublicacao;
                else
                    cmdProcessoPrazo.Parameters.Add("dataPublicacao", SqlDbType.DateTime).Value = DBNull.Value;

                if (ProcessoPrazo.dataVencimento != null)
                    cmdProcessoPrazo.Parameters.Add("dataVencimento", SqlDbType.DateTime).Value = ProcessoPrazo.dataVencimento;
                else
                    cmdProcessoPrazo.Parameters.Add("dataVencimento", SqlDbType.DateTime).Value = DBNull.Value;

                if (ProcessoPrazo.dataConclusao != null)
                    cmdProcessoPrazo.Parameters.Add("dataConclusao", SqlDbType.DateTime).Value = ProcessoPrazo.dataConclusao;
                else
                    cmdProcessoPrazo.Parameters.Add("dataConclusao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdProcessoPrazo.Parameters.Add("quantidadeDiasPrazo", SqlDbType.Int).Value = ProcessoPrazo.quantidadeDiasPrazo;
                cmdProcessoPrazo.Parameters.Add("situacaoPrazo", SqlDbType.VarChar).Value = ProcessoPrazo.situacaoPrazo;
                cmdProcessoPrazo.Parameters.Add("idPessoaAdvogadoResponsavel", SqlDbType.Int).Value = ProcessoPrazo.idPessoaAdvogadoResponsavel;

                try
                {
                    connection.Open();
                    cmdProcessoPrazo.ExecuteNonQuery();

                    int idProcessoPrazo = (int)cmdProcessoPrazo.Parameters["idProcessoPrazo"].Value;

                    // INSERE USUÁRIOS PADRÕES PARA O AGENDAMENTO
                    string stringSQLUsuariosPadroes = String.Format(@"INSERT INTO tbProcessoPrazoResponsavel(idProcessoPrazo, idPessoa)
                                                                    SELECT {0}, idPessoa FROM tbPessoaAdvogado WHERE advogadoPadraoPrazoProcessual = 1
                                                                    UNION ALL 
                                                                    SELECT {0}, idPessoa FROM tbPessoaColaborador WHERE colaboradorPadraoPrazoProcessual = 1"
                                                    , idProcessoPrazo);

                    SqlCommand cmdPessoasPadroes = new SqlCommand(stringSQLUsuariosPadroes, connection);
                    cmdPessoasPadroes.ExecuteNonQuery();

                    return idProcessoPrazo;
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
        public static void Update(dtoProcessoPrazo ProcessoPrazo)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcessoPrazo SET 
                                        idProcesso = @idProcesso,
                                        idUsuarioUltimaAlteracao = @idUsuarioUltimaAlteracao,
                                        dataUltimaAlteracao = getdate(),
                                        idTipoPrazo = @idTipoPrazo,
                                        Descricao = @Descricao,
                                        dataPublicacao = @dataPublicacao,
                                        dataVencimento = @dataVencimento,
                                        dataConclusao = @dataConclusao,
                                        quantidadeDiasPrazo = @quantidadeDiasPrazo,
                                        situacaoPrazo = @situacaoPrazo,
                                        idPessoaAdvogadoResponsavel = @idPessoaAdvogadoResponsavel
                                      WHERE idProcessoPrazo = @idProcessoPrazo";

                SqlCommand cmdProcessoPrazo = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoPrazo);

                cmdProcessoPrazo.Parameters.Add("idProcessoPrazo", SqlDbType.Int).Value = ProcessoPrazo.idProcessoPrazo;
                cmdProcessoPrazo.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoPrazo.idProcesso;

                cmdProcessoPrazo.Parameters.Add("idUsuarioUltimaAlteracao", SqlDbType.Int).Value = ProcessoPrazo.idUsuarioUltimaAlteracao;

                cmdProcessoPrazo.Parameters.Add("idTipoPrazo", SqlDbType.Int).Value = ProcessoPrazo.idTipoPrazo;
                cmdProcessoPrazo.Parameters.Add("Descricao", SqlDbType.VarChar).Value = ProcessoPrazo.Descricao;

                if (ProcessoPrazo.dataPublicacao != null)
                    cmdProcessoPrazo.Parameters.Add("dataPublicacao", SqlDbType.DateTime).Value = ProcessoPrazo.dataPublicacao;
                else
                    cmdProcessoPrazo.Parameters.Add("dataPublicacao", SqlDbType.DateTime).Value = DBNull.Value;

                if (ProcessoPrazo.dataVencimento != null)
                    cmdProcessoPrazo.Parameters.Add("dataVencimento", SqlDbType.DateTime).Value = ProcessoPrazo.dataVencimento;
                else
                    cmdProcessoPrazo.Parameters.Add("dataVencimento", SqlDbType.DateTime).Value = DBNull.Value;

                if (ProcessoPrazo.dataConclusao != null)
                    cmdProcessoPrazo.Parameters.Add("dataConclusao", SqlDbType.DateTime).Value = ProcessoPrazo.dataConclusao;
                else
                    cmdProcessoPrazo.Parameters.Add("dataConclusao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdProcessoPrazo.Parameters.Add("quantidadeDiasPrazo", SqlDbType.Int).Value = ProcessoPrazo.quantidadeDiasPrazo;
                cmdProcessoPrazo.Parameters.Add("situacaoPrazo", SqlDbType.VarChar).Value = ProcessoPrazo.situacaoPrazo;
                cmdProcessoPrazo.Parameters.Add("idPessoaAdvogadoResponsavel", SqlDbType.Int).Value = ProcessoPrazo.idPessoaAdvogadoResponsavel;

                try
                {
                    connection.Open();
                    cmdProcessoPrazo.ExecuteNonQuery();
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
        public static void Delete(dtoProcessoPrazo ProcessoPrazo)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoPrazo 
                                      WHERE idProcessoPrazo = @idProcessoPrazo";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoPrazo", SqlDbType.Int).Value = ProcessoPrazo.idProcessoPrazo;

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

        public static void Delete(int idProcessoPrazo)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoPrazo 
                                      WHERE idProcessoPrazo = @idProcessoPrazo";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoPrazo", SqlDbType.Int).Value = idProcessoPrazo;

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
        public static dtoProcessoPrazo Get(int idProcessoPrazo)
        {
            dtoProcessoPrazo ProcessoPrazo = new dtoProcessoPrazo();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoPrazo
                                    WHERE idProcessoPrazo = @idProcessoPrazo";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoPrazo", SqlDbType.Int).Value = idProcessoPrazo;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoPrazo = cmdMenu.ExecuteReader();

                    if (drProcessoPrazo.Read())
                        PreencheCampos(drProcessoPrazo, ref ProcessoPrazo);
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

            return ProcessoPrazo;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoPrazo> GetAll(int idProcesso, string SortExpression)
        {
            List<dtoProcessoPrazo> ProcessoPrazos = new List<dtoProcessoPrazo>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                sbCondicao.AppendFormat(@" WHERE (tbProcessoPrazo.idProcesso = {0})", idProcesso.ToString());

                string stringSQL = String.Format(@"SELECT * 
                                                FROM tbProcessoPrazo 
                                                {0} 
                                                ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idProcessoPrazo"));

                SqlCommand cmdProcessoPrazo = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoPrazo = cmdProcessoPrazo.ExecuteReader();

                    while (drProcessoPrazo.Read())
                    {
                        dtoProcessoPrazo ProcessoPrazo = new dtoProcessoPrazo();

                        PreencheCampos(drProcessoPrazo, ref ProcessoPrazo);

                        ProcessoPrazos.Add(ProcessoPrazo);
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

            return ProcessoPrazos;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoPrazo> GetAll()
        {
            return GetAll(0, "");
        }

        private static void PreencheCampos(SqlDataReader drProcessoPrazo, ref dtoProcessoPrazo ProcessoPrazo)
        {

            if (drProcessoPrazo["idProcessoPrazo"] != DBNull.Value)
                ProcessoPrazo.idProcessoPrazo = Convert.ToInt32(drProcessoPrazo["idProcessoPrazo"].ToString());

            if (drProcessoPrazo["idProcesso"] != DBNull.Value)
                ProcessoPrazo.idProcesso = Convert.ToInt32(drProcessoPrazo["idProcesso"].ToString());



            if (drProcessoPrazo["idUsuarioCadastro"] != DBNull.Value)
                ProcessoPrazo.idUsuarioCadastro = Convert.ToInt32(drProcessoPrazo["idUsuarioCadastro"].ToString());

            if (drProcessoPrazo["idUsuarioUltimaAlteracao"] != DBNull.Value)
                ProcessoPrazo.idUsuarioUltimaAlteracao = Convert.ToInt32(drProcessoPrazo["idUsuarioUltimaAlteracao"].ToString());

            if (drProcessoPrazo["dataCadastro"] != DBNull.Value)
                ProcessoPrazo.dataCadastro = Convert.ToDateTime(drProcessoPrazo["dataCadastro"]);
            else
                ProcessoPrazo.dataPublicacao = null;

            if (drProcessoPrazo["dataUltimaAlteracao"] != DBNull.Value)
                ProcessoPrazo.dataUltimaAlteracao = Convert.ToDateTime(drProcessoPrazo["dataUltimaAlteracao"]);
            else
                ProcessoPrazo.dataUltimaAlteracao = null;



            if (drProcessoPrazo["idTipoPrazo"] != DBNull.Value)
                ProcessoPrazo.idTipoPrazo = Convert.ToInt32(drProcessoPrazo["idTipoPrazo"].ToString());

            if (drProcessoPrazo["Descricao"] != DBNull.Value)
                ProcessoPrazo.Descricao = drProcessoPrazo["Descricao"].ToString();

            if (drProcessoPrazo["dataPublicacao"] != DBNull.Value)
                ProcessoPrazo.dataPublicacao = Convert.ToDateTime(drProcessoPrazo["dataPublicacao"]);
            else
                ProcessoPrazo.dataPublicacao = null;

            if (drProcessoPrazo["dataVencimento"] != DBNull.Value)
                ProcessoPrazo.dataVencimento = Convert.ToDateTime(drProcessoPrazo["dataVencimento"]);
            else
                ProcessoPrazo.dataVencimento = null;

            if (drProcessoPrazo["dataConclusao"] != DBNull.Value)
                ProcessoPrazo.dataConclusao = Convert.ToDateTime(drProcessoPrazo["dataConclusao"]);
            else
                ProcessoPrazo.dataConclusao = null;

            if (drProcessoPrazo["quantidadeDiasPrazo"] != DBNull.Value)
                ProcessoPrazo.quantidadeDiasPrazo = Convert.ToInt32(drProcessoPrazo["quantidadeDiasPrazo"].ToString());

            if (drProcessoPrazo["situacaoPrazo"] != DBNull.Value)
                ProcessoPrazo.situacaoPrazo = drProcessoPrazo["situacaoPrazo"].ToString();

            if (drProcessoPrazo["idPessoaAdvogadoResponsavel"] != DBNull.Value)
                ProcessoPrazo.idPessoaAdvogadoResponsavel = Convert.ToInt32(drProcessoPrazo["idPessoaAdvogadoResponsavel"].ToString());

        }

        private static void ValidaCampos(ref dtoProcessoPrazo ProcessoPrazo)
        {

            if (String.IsNullOrEmpty(ProcessoPrazo.Descricao)) { ProcessoPrazo.Descricao = String.Empty; }
            if (String.IsNullOrEmpty(ProcessoPrazo.situacaoPrazo)) { ProcessoPrazo.situacaoPrazo = String.Empty; }

        }

        public static string RetornaDescricaoSituacaoPrazo(object situacaoPrazo)
        {
            string retorno = String.Empty;

            if (situacaoPrazo != null)
            {
                switch ((string)situacaoPrazo)
                {
                    case "P":
                        retorno = "Pendente";
                        break;

                    case "C":
                        retorno = "Concluído";
                        break;
                }
            }

            return retorno;
        }



    }
}
