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
    public class bllProcesso
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcesso Processo)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcesso(dataCadastro
                                                            ,numeroProcesso
                                                            ,idPessoaCliente
                                                            ,idTipoAcao
                                                            ,objetoAcao
                                                            ,idVara
                                                            ,idComarca
                                                            ,idAreaProcessual
                                                            ,idFaseAtual
                                                            ,idSituacaoAtual
                                                            ,idInstancia
                                                            ,valorCausa
                                                            ,dataDistribuicao
                                                            ,dataBaixa)
                                                        VALUES
                                                            (getdate()
                                                            ,@numeroProcesso
                                                            ,@idPessoaCliente
                                                            ,@idTipoAcao
                                                            ,@objetoAcao
                                                            ,@idVara
                                                            ,@idComarca
                                                            ,@idAreaProcessual
                                                            ,@idFaseAtual
                                                            ,@idSituacaoAtual
                                                            ,@idInstancia
                                                            ,@valorCausa
                                                            ,@dataDistribuicao
                                                            ,@dataBaixa);
                                            SET @idProcesso = SCOPE_IDENTITY()";

                SqlCommand cmdProcesso = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Processo);

                cmdProcesso.Parameters.Add("idProcesso", SqlDbType.Int);
                cmdProcesso.Parameters["idProcesso"].Direction = ParameterDirection.Output;

                cmdProcesso.Parameters.Add("numeroProcesso", SqlDbType.VarChar).Value = Processo.numeroProcesso;
                cmdProcesso.Parameters.Add("idPessoaCliente", SqlDbType.Int).Value = Processo.idPessoaCliente;
                cmdProcesso.Parameters.Add("idTipoAcao", SqlDbType.Int).Value = Processo.idTipoAcao;
                cmdProcesso.Parameters.Add("objetoAcao", SqlDbType.VarChar).Value = Processo.objetoAcao;
                cmdProcesso.Parameters.Add("idVara", SqlDbType.Int).Value = Processo.idVara;
                cmdProcesso.Parameters.Add("idComarca", SqlDbType.Int).Value = Processo.idComarca;
                cmdProcesso.Parameters.Add("idAreaProcessual", SqlDbType.Int).Value = Processo.idAreaProcessual;
                cmdProcesso.Parameters.Add("idFaseAtual", SqlDbType.Int).Value = Processo.idFaseAtual;
                cmdProcesso.Parameters.Add("idSituacaoAtual", SqlDbType.Int).Value = Processo.idSituacaoAtual;
                cmdProcesso.Parameters.Add("idInstancia", SqlDbType.Int).Value = Processo.idInstancia;
                cmdProcesso.Parameters.Add("valorCausa", SqlDbType.Float).Value = Processo.valorCausa;

                if (Processo.dataDistribuicao != null)
                    cmdProcesso.Parameters.Add("dataDistribuicao", SqlDbType.DateTime).Value = Processo.dataDistribuicao;
                else
                    cmdProcesso.Parameters.Add("dataDistribuicao", SqlDbType.DateTime).Value = DBNull.Value;

                if (Processo.dataBaixa != null)
                    cmdProcesso.Parameters.Add("dataBaixa", SqlDbType.DateTime).Value = Processo.dataBaixa;
                else
                    cmdProcesso.Parameters.Add("dataBaixa", SqlDbType.DateTime).Value = DBNull.Value;

                try
                {
                    connection.Open();
                    cmdProcesso.ExecuteNonQuery();

                    int idProcesso = (int)cmdProcesso.Parameters["idProcesso"].Value;

                    // INSERE ADVOGADOS PADRÕES PARA O PROCESSO
                    string stringSQLAdvogadosPadroes = String.Format(@"INSERT INTO tbProcessoAdvogado(idProcesso, idPessoaAdvogado, tipoAdvogado)
                                                    SELECT {0}, idPessoa, 'A' FROM tbPessoaAdvogado WHERE advogadoPadraoProcesso = 1", idProcesso);

                    SqlCommand cmdProcessoAdvogadosPadroes = new SqlCommand(stringSQLAdvogadosPadroes, connection);
                    cmdProcessoAdvogadosPadroes.ExecuteNonQuery();

                    return idProcesso;
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
        public static void Update(dtoProcesso Processo)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcesso SET 
                                            dataUltimaAlteracao = getdate()
                                            ,numeroProcesso = @numeroProcesso
                                            ,idPessoaCliente = @idPessoaCliente
                                            ,idTipoAcao = @idTipoAcao
                                            ,objetoAcao = @objetoAcao
                                            ,idVara = @idVara
                                            ,idComarca = @idComarca
                                            ,idAreaProcessual = @idAreaProcessual
                                            ,idFaseAtual = @idAreaProcessual
                                            ,idSituacaoAtual = @idSituacaoAtual
                                            ,idInstancia = @idInstancia
                                            ,valorCausa = @valorCausa
                                            ,dataDistribuicao = @dataDistribuicao
                                            ,dataBaixa = @dataBaixa
                                      WHERE idProcesso = @idProcesso";

                SqlCommand cmdProcesso = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Processo);

                cmdProcesso.Parameters.Add("idProcesso", SqlDbType.Int).Value = Processo.idProcesso;
                cmdProcesso.Parameters.Add("numeroProcesso", SqlDbType.VarChar).Value = Processo.numeroProcesso;
                cmdProcesso.Parameters.Add("idPessoaCliente", SqlDbType.Int).Value = Processo.idPessoaCliente;
                cmdProcesso.Parameters.Add("idTipoAcao", SqlDbType.Int).Value = Processo.idTipoAcao;
                cmdProcesso.Parameters.Add("objetoAcao", SqlDbType.VarChar).Value = Processo.objetoAcao;
                cmdProcesso.Parameters.Add("idVara", SqlDbType.Int).Value = Processo.idVara;
                cmdProcesso.Parameters.Add("idComarca", SqlDbType.Int).Value = Processo.idComarca;
                cmdProcesso.Parameters.Add("idAreaProcessual", SqlDbType.Int).Value = Processo.idAreaProcessual;
                cmdProcesso.Parameters.Add("idFaseAtual", SqlDbType.Int).Value = Processo.idFaseAtual;
                cmdProcesso.Parameters.Add("idSituacaoAtual", SqlDbType.Int).Value = Processo.idSituacaoAtual;
                cmdProcesso.Parameters.Add("idInstancia", SqlDbType.Int).Value = Processo.idInstancia;
                cmdProcesso.Parameters.Add("valorCausa", SqlDbType.Float).Value = Processo.valorCausa;

                if (Processo.dataDistribuicao != null)
                    cmdProcesso.Parameters.Add("dataDistribuicao", SqlDbType.DateTime).Value = Processo.dataDistribuicao;
                else
                    cmdProcesso.Parameters.Add("dataDistribuicao", SqlDbType.DateTime).Value = DBNull.Value;

                if (Processo.dataBaixa != null)
                    cmdProcesso.Parameters.Add("dataBaixa", SqlDbType.DateTime).Value = Processo.dataBaixa;
                else
                    cmdProcesso.Parameters.Add("dataBaixa", SqlDbType.DateTime).Value = DBNull.Value;

                try
                {
                    connection.Open();
                    cmdProcesso.ExecuteNonQuery();
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
        public static void Delete(dtoProcesso Processo)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcesso 
                                      WHERE idProcesso = @idProcesso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcesso", SqlDbType.Int).Value = Processo.idProcesso;

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

        public static void Delete(int idProcesso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcesso 
                                      WHERE idProcesso = @idProcesso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcesso", SqlDbType.Int).Value = idProcesso;

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
        public static dtoProcesso Get(int idProcesso)
        {
            dtoProcesso Processo = new dtoProcesso();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcesso
                                    WHERE idProcesso = @idProcesso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcesso", SqlDbType.Int).Value = idProcesso;

                try
                {
                    connection.Open();
                    SqlDataReader drProcesso = cmdMenu.ExecuteReader();

                    if (drProcesso.Read())
                        PreencheCampos(drProcesso, ref Processo);
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

            return Processo;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcesso> GetAll(string SortExpression, string termoPesquisa)
        {
            return GetAll("", "", "", termoPesquisa, SortExpression);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcesso> GetAll(string idAreaProcessual, string idComarca, string idInstancia, string termoPesquisa, string SortExpression)
        {
            List<dtoProcesso> Processos = new List<dtoProcesso>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                if (termoPesquisa != null
                    && termoPesquisa.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    //sbCondicao.AppendFormat(@" ((tbPessoaFisica.fisicaCPF LIKE '%{0}%' OR tbPessoaJuridica.juridicaCNPJ LIKE '%{0}%') 
                    //                OR (tbPessoaFisica.fisicaNomeCompleto LIKE '%{0}%' OR tbPessoaJuridica.juridicaRazaoSocial LIKE '%{0}%'))", termoPesquisa);

                    sbCondicao.AppendFormat(@" ((tbPessoaFisica.fisicaCPF LIKE '%{0}%' OR tbPessoaJuridica.juridicaCNPJ LIKE '%{0}%') 
                                    OR (tbPessoaFisica.fisicaNomeCompleto LIKE '% {0}%' OR tbPessoaJuridica.juridicaRazaoSocial LIKE '% {0}%'))", termoPesquisa);
                    sbCondicao.AppendFormat(@" OR ((tbPessoaFisica.fisicaCPF LIKE '%{0}%' OR tbPessoaJuridica.juridicaCNPJ LIKE '%{0}%') 
                                    OR (tbPessoaFisica.fisicaNomeCompleto LIKE '{0}%' OR tbPessoaJuridica.juridicaRazaoSocial LIKE '{0}%'))", termoPesquisa);

                    sbCondicao.AppendFormat(@" OR (tbProcesso.numeroProcesso LIKE '%{0}%') ", termoPesquisa);
                }

                if (idAreaProcessual != null
                    && idAreaProcessual.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbProcesso.idAreaProcessual = {0})", idAreaProcessual);
                }

                if (idComarca != null
                    && idComarca.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbProcesso.idComarca = {0})", idComarca);
                }

                if (idInstancia != null
                    && idInstancia.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbProcesso.idInstancia = {0})", idInstancia);
                }

                string stringSQL = String.Format(@"SELECT * FROM tbProcesso 
                                                LEFT JOIN tbPessoa
                                                    ON tbPessoa.idPessoa = tbProcesso.idPessoaCliente
                                                LEFT JOIN tbPessoaFisica
                                                    ON tbProcesso.idPessoaCliente = tbPessoaFisica.idPessoa
                                                LEFT JOIN tbPessoaJuridica
                                                    ON tbProcesso.idPessoaCliente = tbPessoaJuridica.idPessoa 
                                                {0}
                                                ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "tbPessoaFisica.fisicaNomeCompleto, tbPessoaJuridica.juridicaRazaoSocial"));

                SqlCommand cmdProcesso = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcesso = cmdProcesso.ExecuteReader();

                    while (drProcesso.Read())
                    {
                        dtoProcesso Processo = new dtoProcesso();

                        PreencheCampos(drProcesso, ref Processo);

                        Processos.Add(Processo);
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

            return Processos;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcesso> GetAll()
        {
            return GetAll("", "", "", "", "");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcesso> GetAll(string SortExpression)
        {
            return GetAll("", "", "", "", SortExpression);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcesso> GetAllByPessoa(int idPessoa)
        {
            List<dtoProcesso> Processos = new List<dtoProcesso>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = String.Format("SELECT * FROM tbProcesso WHERE idPessoaCliente = {0} ORDER BY dataCadastro DESC", idPessoa);

                SqlCommand cmdProcesso = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcesso = cmdProcesso.ExecuteReader();

                    while (drProcesso.Read())
                    {
                        dtoProcesso Processo = new dtoProcesso();

                        PreencheCampos(drProcesso, ref Processo);

                        Processos.Add(Processo);
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

            return Processos;
        }

        private static void PreencheCampos(SqlDataReader drProcesso, ref dtoProcesso Processo)
        {

            if (drProcesso["idProcesso"] != DBNull.Value)
                Processo.idProcesso = Convert.ToInt32(drProcesso["idProcesso"].ToString());

            if (drProcesso["dataCadastro"] != DBNull.Value)
                Processo.dataCadastro = Convert.ToDateTime(drProcesso["dataCadastro"]);
            else
                Processo.dataCadastro = null;

            if (drProcesso["dataUltimaAlteracao"] != DBNull.Value)
                Processo.dataUltimaAlteracao = Convert.ToDateTime(drProcesso["dataUltimaAlteracao"]);
            else
                Processo.dataUltimaAlteracao = null;

            if (drProcesso["numeroProcesso"] != DBNull.Value)
                Processo.numeroProcesso = drProcesso["numeroProcesso"].ToString();

            if (drProcesso["idPessoaCliente"] != DBNull.Value)
                Processo.idPessoaCliente = Convert.ToInt32(drProcesso["idPessoaCliente"].ToString());

            if (drProcesso["idTipoAcao"] != DBNull.Value)
                Processo.idTipoAcao = Convert.ToInt32(drProcesso["idTipoAcao"].ToString());

            if (drProcesso["objetoAcao"] != DBNull.Value)
                Processo.objetoAcao = drProcesso["objetoAcao"].ToString();

            if (drProcesso["idVara"] != DBNull.Value)
                Processo.idVara = Convert.ToInt32(drProcesso["idVara"].ToString());

            if (drProcesso["idComarca"] != DBNull.Value)
                Processo.idComarca = Convert.ToInt32(drProcesso["idComarca"].ToString());

            if (drProcesso["idAreaProcessual"] != DBNull.Value)
                Processo.idAreaProcessual = Convert.ToInt32(drProcesso["idAreaProcessual"].ToString());

            if (drProcesso["idFaseAtual"] != DBNull.Value)
                Processo.idFaseAtual = Convert.ToInt32(drProcesso["idFaseAtual"].ToString());

            if (drProcesso["idSituacaoAtual"] != DBNull.Value)
                Processo.idSituacaoAtual = Convert.ToInt32(drProcesso["idSituacaoAtual"].ToString());

            if (drProcesso["idInstancia"] != DBNull.Value)
                Processo.idInstancia = Convert.ToInt32(drProcesso["idInstancia"].ToString());

            if (drProcesso["valorCausa"] != DBNull.Value)
                Processo.valorCausa = Convert.ToSingle(drProcesso["valorCausa"].ToString());

            if (drProcesso["dataDistribuicao"] != DBNull.Value)
                Processo.dataDistribuicao = Convert.ToDateTime(drProcesso["dataDistribuicao"]);
            else
                Processo.dataDistribuicao = null;

            if (drProcesso["dataBaixa"] != DBNull.Value)
                Processo.dataBaixa = Convert.ToDateTime(drProcesso["dataBaixa"]);
            else
                Processo.dataBaixa = null;

        }

        private static void ValidaCampos(ref dtoProcesso Processo)
        {

            if (String.IsNullOrEmpty(Processo.numeroProcesso)) { Processo.numeroProcesso = String.Empty; }
            if (String.IsNullOrEmpty(Processo.objetoAcao)) { Processo.objetoAcao = String.Empty; }

        }

    }

}
