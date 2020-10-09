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
    public partial class bllPessoa
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoa(especiePessoa, tipoPessoaCliente, tipoPessoaParte, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaTerceiro, dataCadastro)
                                            VALUES(@especiePessoa, @tipoPessoaCliente, @tipoPessoaParte, @tipoPessoaColaborador, @tipoPessoaAdvogado, @tipoPessoaTerceiro, getdate());
                                            SET @idPessoa = SCOPE_IDENTITY()";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int);
                cmdPessoa.Parameters["idPessoa"].Direction = ParameterDirection.Output;

                cmdPessoa.Parameters.Add("especiePessoa", SqlDbType.VarChar).Value = Pessoa.especiePessoa;
                cmdPessoa.Parameters.Add("tipoPessoaCliente", SqlDbType.Bit).Value = Pessoa.tipoPessoaCliente;
                cmdPessoa.Parameters.Add("tipoPessoaParte", SqlDbType.Bit).Value = Pessoa.tipoPessoaParte;
                cmdPessoa.Parameters.Add("tipoPessoaColaborador", SqlDbType.Bit).Value = Pessoa.tipoPessoaColaborador;
                cmdPessoa.Parameters.Add("tipoPessoaAdvogado", SqlDbType.Bit).Value = Pessoa.tipoPessoaAdvogado;
                cmdPessoa.Parameters.Add("tipoPessoaTerceiro", SqlDbType.Bit).Value = Pessoa.tipoPessoaTerceiro;

                TrataParametrosNulos(cmdPessoa.Parameters);
                TrataEspacosEmBranco(cmdPessoa.Parameters);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
                    return (int)cmdPessoa.Parameters["idPessoa"].Value;
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException("Erro ao inserir registro Principal");
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void Delete(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbPessoa 
                                      WHERE idPessoa = @idPessoa";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);
                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
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

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void Delete(int idPessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbPessoa 
                                      WHERE idPessoa = @idPessoa";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);
                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = idPessoa;

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
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


        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void Update(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbPessoa SET 
                                        especiePessoa = @especiePessoa,
                                        tipoPessoaCliente = @tipoPessoaCliente,
                                        tipoPessoaParte = @tipoPessoaParte,
                                        tipoPessoaColaborador = @tipoPessoaColaborador,
                                        tipoPessoaAdvogado = @tipoPessoaAdvogado,
                                        tipoPessoaTerceiro = @tipoPessoaTerceiro,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idPessoa = @idPessoa";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                cmdPessoa.Parameters.Add("especiePessoa", SqlDbType.VarChar).Value = Pessoa.especiePessoa;
                cmdPessoa.Parameters.Add("tipoPessoaCliente", SqlDbType.Bit).Value = Pessoa.tipoPessoaCliente;
                cmdPessoa.Parameters.Add("tipoPessoaParte", SqlDbType.Bit).Value = Pessoa.tipoPessoaParte;
                cmdPessoa.Parameters.Add("tipoPessoaColaborador", SqlDbType.Bit).Value = Pessoa.tipoPessoaColaborador;
                cmdPessoa.Parameters.Add("tipoPessoaAdvogado", SqlDbType.Bit).Value = Pessoa.tipoPessoaAdvogado;
                cmdPessoa.Parameters.Add("tipoPessoaTerceiro", SqlDbType.Bit).Value = Pessoa.tipoPessoaTerceiro;

                TrataParametrosNulos(cmdPessoa.Parameters);
                TrataEspacosEmBranco(cmdPessoa.Parameters);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
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


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static dtoPessoa Get(int idPessoa)
        {
            dtoPessoa pessoa = new dtoPessoa();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbPessoa
                                    LEFT JOIN tbPessoaFisica
                                        ON tbPessoa.idPessoa = tbPessoaFisica.idPessoa
                                    LEFT JOIN tbPessoaJuridica
                                        ON tbPessoa.idPessoa = tbPessoaJuridica.idPessoa
                                    LEFT JOIN tbPessoaEndereco
                                        ON tbPessoa.idPessoa = tbPessoaEndereco.idPessoa       
                                    LEFT JOIN tbPessoaDadosProfissionais
                                        ON tbPessoa.idPessoa = tbPessoaDadosProfissionais.idPessoa         
                                    LEFT JOIN tbPessoaReferencia
                                        ON tbPessoa.idPessoa = tbPessoaReferencia.idPessoa  
                                    LEFT JOIN tbPessoaAdvogado
                                        ON tbPessoa.idPessoa = tbPessoaAdvogado.idPessoa
                                    LEFT JOIN tbPessoaColaborador
                                        ON tbPessoa.idPessoa = tbPessoaColaborador.idPessoa 
                                    LEFT JOIN tbPessoaContato
                                        ON tbPessoa.idPessoa = tbPessoaContato.idPessoa 
                                    WHERE tbPessoa.idPessoa = @idPessoa";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = idPessoa;

                try
                {
                    connection.Open();
                    SqlDataReader drPessoa = cmdPessoa.ExecuteReader();

                    if (drPessoa.Read())
                        PreencheCampos(drPessoa, ref pessoa);
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

            return pessoa;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static dtoPessoa Get(string CPF)
        {
            dtoPessoa pessoa = new dtoPessoa();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbPessoa
                                    LEFT JOIN tbPessoaFisica
                                        ON tbPessoa.idPessoa = tbPessoaFisica.idPessoa
                                    LEFT JOIN tbPessoaJuridica
                                        ON tbPessoa.idPessoa = tbPessoaJuridica.idPessoa
                                    LEFT JOIN tbPessoaEndereco
                                        ON tbPessoa.idPessoa = tbPessoaEndereco.idPessoa       
                                    LEFT JOIN tbPessoaDadosProfissionais
                                        ON tbPessoa.idPessoa = tbPessoaDadosProfissionais.idPessoa         
                                    LEFT JOIN tbPessoaReferencia
                                        ON tbPessoa.idPessoa = tbPessoaReferencia.idPessoa  
                                    LEFT JOIN tbPessoaAdvogado
                                        ON tbPessoa.idPessoa = tbPessoaAdvogado.idPessoa
                                    LEFT JOIN tbPessoaColaborador
                                        ON tbPessoa.idPessoa = tbPessoaColaborador.idPessoa 
                                    LEFT JOIN tbPessoaContato
                                        ON tbPessoa.idPessoa = tbPessoaContato.idPessoa 
                                    WHERE tbPessoaPF.CPF = @CPF";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("CPF", SqlDbType.VarChar).Value = CPF;

                try
                {
                    connection.Open();
                    SqlDataReader drPessoa = cmdPessoa.ExecuteReader();

                    if (drPessoa.Read())
                        PreencheCampos(drPessoa, ref pessoa);
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

            return pessoa;
        }


        public static bool Exists(int idPessoa)
        {
            bool retorno = false;

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1
                                    FROM tbPessoa
                                    WHERE tbPessoa.idPessoa = @idPessoa";

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
        public static List<dtoPessoa> GetAll(string SortExpression, string termoPesquisa)
        {
            return GetAll("", "", "", "", "", termoPesquisa, SortExpression);

        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoPessoa> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoPessoa> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoPessoa> GetAll(string tipoPessoaAdvogado, string tipoPessoaCliente, string tipoPessoaColaborador, string tipoPessoaParte, string tipoPessoaTerceiro, string termoPesquisa, string SortExpression)
        {
            List<dtoPessoa> pessoas = new List<dtoPessoa>();

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

                    sbCondicao.AppendFormat(@" ((tbPessoaFisica.fisicaCPF LIKE '%{0}%' OR tbPessoaJuridica.juridicaCNPJ LIKE '%{0}%') 
                                    OR (tbPessoaFisica.fisicaNomeCompleto LIKE '% {0}%' OR tbPessoaJuridica.juridicaRazaoSocial LIKE '% {0}%'))", termoPesquisa);
                    sbCondicao.AppendFormat(@" OR ((tbPessoaFisica.fisicaCPF LIKE '%{0}%' OR tbPessoaJuridica.juridicaCNPJ LIKE '%{0}%') 
                                    OR (tbPessoaFisica.fisicaNomeCompleto LIKE '{0}%' OR tbPessoaJuridica.juridicaRazaoSocial LIKE '{0}%'))", termoPesquisa);
                }

                if (tipoPessoaAdvogado != null
                    && tipoPessoaAdvogado.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" OR ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbPessoa.tipoPessoaAdvogado = {0})", tipoPessoaAdvogado);
                }

                if (tipoPessoaCliente != null
                    && tipoPessoaCliente.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" OR ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbPessoa.tipoPessoaCliente = {0})", tipoPessoaCliente);
                }

                if (tipoPessoaColaborador != null
                    && tipoPessoaColaborador.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" OR ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbPessoa.tipoPessoaColaborador = {0})", tipoPessoaColaborador);
                }

                if (tipoPessoaParte != null
                    && tipoPessoaParte.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" OR ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbPessoa.tipoPessoaParte = {0})", tipoPessoaParte);
                }

                if (tipoPessoaTerceiro != null
                    && tipoPessoaTerceiro.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" OR ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbPessoa.tipoPessoaTerceiro = {0})", tipoPessoaTerceiro);
                }

                string stringSQL = String.Format(@"SELECT *
                                                    FROM tbPessoa
                                                    LEFT JOIN tbPessoaFisica
                                                        ON tbPessoa.idPessoa = tbPessoaFisica.idPessoa
                                                    LEFT JOIN tbPessoaJuridica
                                                        ON tbPessoa.idPessoa = tbPessoaJuridica.idPessoa
                                                    LEFT JOIN tbPessoaEndereco
                                                        ON tbPessoa.idPessoa = tbPessoaEndereco.idPessoa       
                                                    LEFT JOIN tbPessoaDadosProfissionais
                                                        ON tbPessoa.idPessoa = tbPessoaDadosProfissionais.idPessoa         
                                                    LEFT JOIN tbPessoaReferencia
                                                        ON tbPessoa.idPessoa = tbPessoaReferencia.idPessoa  
                                                    LEFT JOIN tbPessoaAdvogado
                                                        ON tbPessoa.idPessoa = tbPessoaAdvogado.idPessoa
                                                    LEFT JOIN tbPessoaColaborador
                                                        ON tbPessoa.idPessoa = tbPessoaColaborador.idPessoa 
                                                    LEFT JOIN tbPessoaContato
                                                        ON tbPessoa.idPessoa = tbPessoaContato.idPessoa 
                                                    {0}
                                                ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "tbPessoa.idPessoa"));

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drPessoa = cmdPessoa.ExecuteReader();

                    while (drPessoa.Read())
                    {
                        dtoPessoa pessoa = new dtoPessoa();

                        PreencheCampos(drPessoa, ref pessoa);

                        pessoas.Add(pessoa);
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

            return pessoas;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoPessoa> GetAniversariantes(string Mes, string Dia, string SortExpression)
        {
            List<dtoPessoa> pessoas = new List<dtoPessoa>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                if ((Mes != null && Mes != String.Empty)
                    && (Dia != null && Dia != String.Empty))
                {
                    sbCondicao.AppendFormat(@" WHERE 
                                            (CASE 
                                                WHEN tbPessoa.especiePessoa = 'F' THEN 
                                                    MONTH(tbPessoaFisica.fisicaDataNascimento)
                                                WHEN tbPessoa.especiePessoa = 'J' THEN 
                                                    MONTH(tbPessoaJuridica.juridicaDataFundacao)
                                            END) = {0} ", Mes);

                    sbCondicao.AppendFormat(@" AND
                                            (CASE 
                                                WHEN tbPessoa.especiePessoa = 'F' THEN 
                                                    DAY(tbPessoaFisica.fisicaDataNascimento)
                                                WHEN tbPessoa.especiePessoa = 'J' THEN 
                                                    DAY(tbPessoaJuridica.juridicaDataFundacao)
                                            END) = {0} ", Dia);
                }

                string stringSQL = String.Format(@"SELECT *
                                                    FROM tbPessoa
                                                    LEFT JOIN tbPessoaFisica
                                                        ON tbPessoa.idPessoa = tbPessoaFisica.idPessoa
                                                    LEFT JOIN tbPessoaJuridica
                                                        ON tbPessoa.idPessoa = tbPessoaJuridica.idPessoa
                                                    LEFT JOIN tbPessoaEndereco
                                                        ON tbPessoa.idPessoa = tbPessoaEndereco.idPessoa       
                                                    LEFT JOIN tbPessoaDadosProfissionais
                                                        ON tbPessoa.idPessoa = tbPessoaDadosProfissionais.idPessoa         
                                                    LEFT JOIN tbPessoaReferencia
                                                        ON tbPessoa.idPessoa = tbPessoaReferencia.idPessoa  
                                                    LEFT JOIN tbPessoaAdvogado
                                                        ON tbPessoa.idPessoa = tbPessoaAdvogado.idPessoa
                                                    LEFT JOIN tbPessoaColaborador
                                                        ON tbPessoa.idPessoa = tbPessoaColaborador.idPessoa 
                                                    LEFT JOIN tbPessoaContato
                                                        ON tbPessoa.idPessoa = tbPessoaContato.idPessoa 
                                                    {0}
                                                ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "tbPessoa.idPessoa"));

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drPessoa = cmdPessoa.ExecuteReader();

                    while (drPessoa.Read())
                    {
                        dtoPessoa pessoa = new dtoPessoa();

                        PreencheCampos(drPessoa, ref pessoa);

                        pessoas.Add(pessoa);
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

            return pessoas;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertPessoaFisica(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoaFisica
                                                    (idPessoa
                                                    ,fisicaNomeCompleto
                                                    ,fisicaCPF
                                                    ,fisicaDataNascimento
                                                    ,fisicaSexo
                                                    ,fisicaEstadoCivil
                                                    ,fisicaProfissao
                                                    ,fisicaNaturalidade
                                                    ,fisicaRGNumero
                                                    ,fisicaRGDataExpedicao
                                                    ,fisicaRGOrgaoExpedidor
                                                    ,fisicaRGUFExpedidor
                                                    ,fisicaCNHNumero
                                                    ,fisicaCNHCategoria
                                                    ,fisicaCNHDataHabilitacao
                                                    ,fisicaCNHDataEmissao
                                                    ,fisicaFiliacaoNomePai
                                                    ,fisicaFiliacaoNomeMae
                                                    ,fisicaConjugueNomeCompleto
                                                    ,fisicaConjugueCPF
                                                    ,fisicaConjugueDataNascimento
                                                    ,fisicaNacionalidade
                                                    ,fisicaEscolaridade
                                                    ,fisicaPISPASEP
                                                    ,fisicaCTPSNumero
                                                    ,fisicaCTPSSerie
                                                    ,fisicaCTPSDataExpedicao
                                                    ,fisicaRegimeComunhaoBens
                                                    ,fisicaCadSenha
                                                    )
                                             VALUES
                                                    (@idPessoa
                                                    ,@fisicaNomeCompleto
                                                    ,@fisicaCPF
                                                    ,@fisicaDataNascimento
                                                    ,@fisicaSexo
                                                    ,@fisicaEstadoCivil
                                                    ,@fisicaProfissao
                                                    ,@fisicaNaturalidade
                                                    ,@fisicaRGNumero
                                                    ,@fisicaRGDataExpedicao
                                                    ,@fisicaRGOrgaoExpedidor
                                                    ,@fisicaRGUFExpedidor
                                                    ,@fisicaCNHNumero
                                                    ,@fisicaCNHCategoria
                                                    ,@fisicaCNHDataHabilitacao
                                                    ,@fisicaCNHDataEmissao
                                                    ,@fisicaFiliacaoNomePai
                                                    ,@fisicaFiliacaoNomeMae
                                                    ,@fisicaConjugueNomeCompleto
                                                    ,@fisicaConjugueCPF
                                                    ,@fisicaConjugueDataNascimento
                                                    ,@fisicaNacionalidade
                                                    ,@fisicaEscolaridade
                                                    ,@fisicaPISPASEP
                                                    ,@fisicaCTPSNumero
                                                    ,@fisicaCTPSSerie
                                                    ,@fisicaCTPSDataExpedicao
                                                    ,@fisicaRegimeComunhaoBens
                                                    ,@fisicaCadSenha)";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;
                cmdPessoa.Parameters.Add("fisicaNomeCompleto", SqlDbType.VarChar).Value = Pessoa.fisicaNomeCompleto;

                if (Pessoa.fisicaCPF != null)
                    cmdPessoa.Parameters.Add("fisicaCPF", SqlDbType.VarChar).Value = Pessoa.fisicaCPF.Replace(".", "").Replace("-", "");
                else
                    cmdPessoa.Parameters.Add("fisicaCPF", SqlDbType.VarChar).Value = DBNull.Value;

                if (Pessoa.fisicaDataNascimento != null)
                    cmdPessoa.Parameters.Add("fisicaDataNascimento", SqlDbType.DateTime).Value = Pessoa.fisicaDataNascimento;
                else
                    cmdPessoa.Parameters.Add("fisicaDataNascimento", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoa.Parameters.Add("fisicaSexo", SqlDbType.VarChar).Value = Pessoa.fisicaSexo;
                cmdPessoa.Parameters.Add("fisicaEstadoCivil", SqlDbType.VarChar).Value = Pessoa.fisicaEstadoCivil;
                cmdPessoa.Parameters.Add("fisicaProfissao", SqlDbType.VarChar).Value = Pessoa.fisicaProfissao;
                cmdPessoa.Parameters.Add("fisicaNaturalidade", SqlDbType.VarChar).Value = Pessoa.fisicaNaturalidade;
                cmdPessoa.Parameters.Add("fisicaRGNumero", SqlDbType.VarChar).Value = Pessoa.fisicaRGNumero;

                if (Pessoa.fisicaRGDataExpedicao != null)
                    cmdPessoa.Parameters.Add("fisicaRGDataExpedicao", SqlDbType.DateTime).Value = Pessoa.fisicaRGDataExpedicao;
                else
                    cmdPessoa.Parameters.Add("fisicaRGDataExpedicao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoa.Parameters.Add("fisicaRGOrgaoExpedidor", SqlDbType.VarChar).Value = Pessoa.fisicaRGOrgaoExpedidor;
                cmdPessoa.Parameters.Add("fisicaRGUFExpedidor", SqlDbType.VarChar).Value = Pessoa.fisicaRGUFExpedidor;
                cmdPessoa.Parameters.Add("fisicaCNHNumero", SqlDbType.VarChar).Value = Pessoa.fisicaCNHNumero;
                cmdPessoa.Parameters.Add("fisicaCNHCategoria", SqlDbType.VarChar).Value = Pessoa.fisicaCNHCategoria;

                if (Pessoa.fisicaCNHDataHabilitacao != null)
                    cmdPessoa.Parameters.Add("fisicaCNHDataHabilitacao", SqlDbType.DateTime).Value = Pessoa.fisicaCNHDataHabilitacao;
                else
                    cmdPessoa.Parameters.Add("fisicaCNHDataHabilitacao", SqlDbType.DateTime).Value = DBNull.Value;
                
                if (Pessoa.fisicaCNHDataEmissao != null)
                    cmdPessoa.Parameters.Add("fisicaCNHDataEmissao", SqlDbType.DateTime).Value = Pessoa.fisicaCNHDataEmissao;
                else
                    cmdPessoa.Parameters.Add("fisicaCNHDataEmissao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoa.Parameters.Add("fisicaFiliacaoNomePai", SqlDbType.VarChar).Value = Pessoa.fisicaFiliacaoNomePai;
                cmdPessoa.Parameters.Add("fisicaFiliacaoNomeMae", SqlDbType.VarChar).Value = Pessoa.fisicaFiliacaoNomeMae;
                cmdPessoa.Parameters.Add("fisicaConjugueNomeCompleto", SqlDbType.VarChar).Value = Pessoa.fisicaConjugueNomeCompleto;

                if (Pessoa.fisicaConjugueCPF != null)
                    cmdPessoa.Parameters.Add("fisicaConjugueCPF", SqlDbType.VarChar).Value = Pessoa.fisicaConjugueCPF.Replace(".", "").Replace("-", "");
                else
                    cmdPessoa.Parameters.Add("fisicaConjugueCPF", SqlDbType.VarChar).Value = DBNull.Value;

                if (Pessoa.fisicaConjugueDataNascimento != null)
                    cmdPessoa.Parameters.Add("fisicaConjugueDataNascimento", SqlDbType.DateTime).Value = Pessoa.fisicaConjugueDataNascimento;
                else
                    cmdPessoa.Parameters.Add("fisicaConjugueDataNascimento", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoa.Parameters.Add("fisicaNacionalidade", SqlDbType.VarChar).Value = Pessoa.fisicaNacionalidade;
                cmdPessoa.Parameters.Add("fisicaEscolaridade", SqlDbType.VarChar).Value = Pessoa.fisicaEscolaridade;
                cmdPessoa.Parameters.Add("fisicaPISPASEP", SqlDbType.VarChar).Value = Pessoa.fisicaPISPASEP;
                cmdPessoa.Parameters.Add("fisicaCTPSNumero", SqlDbType.VarChar).Value = Pessoa.fisicaCTPSNumero;
                cmdPessoa.Parameters.Add("fisicaCTPSSerie", SqlDbType.VarChar).Value = Pessoa.fisicaCTPSSerie;

                if (Pessoa.fisicaCTPSDataExpedicao != null)
                    cmdPessoa.Parameters.Add("fisicaCTPSDataExpedicao", SqlDbType.DateTime).Value = Pessoa.fisicaCTPSDataExpedicao;
                else
                    cmdPessoa.Parameters.Add("fisicaCTPSDataExpedicao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoa.Parameters.Add("fisicaRegimeComunhaoBens", SqlDbType.VarChar).Value = Pessoa.fisicaRegimeComunhaoBens;

                cmdPessoa.Parameters.Add("fisicaCadSenha", SqlDbType.VarChar).Value = Pessoa.fisicaCadSenha;

                TrataParametrosNulos(cmdPessoa.Parameters);
                TrataEspacosEmBranco(cmdPessoa.Parameters);

                InsertEndereco(Pessoa);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException("Erro ao inserir registro PF");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdatePessoaFisica(dtoPessoa Pessoa)
        {
            if (!ExistsPessoaFisica(Pessoa))
                InsertPessoaFisica(Pessoa);
            else
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    string stringSQL = @"UPDATE tbPessoaFisica
                                        SET fisicaNomeCompleto = @fisicaNomeCompleto
                                            ,fisicaCPF = @fisicaCPF
                                            ,fisicaDataNascimento = @fisicaDataNascimento
                                            ,fisicaSexo = @fisicaSexo
                                            ,fisicaEstadoCivil = @fisicaEstadoCivil
                                            ,fisicaProfissao = @fisicaProfissao
                                            ,fisicaNaturalidade = @fisicaNaturalidade
                                            ,fisicaRGNumero = @fisicaRGNumero
                                            ,fisicaRGDataExpedicao = @fisicaRGDataExpedicao
                                            ,fisicaRGOrgaoExpedidor = @fisicaRGOrgaoExpedidor
                                            ,fisicaRGUFExpedidor = @fisicaRGUFExpedidor
                                            ,fisicaCNHNumero = @fisicaCNHNumero
                                            ,fisicaCNHCategoria = @fisicaCNHCategoria
                                            ,fisicaCNHDataHabilitacao = @fisicaCNHDataHabilitacao
                                            ,fisicaCNHDataEmissao = @fisicaCNHDataEmissao
                                            ,fisicaFiliacaoNomePai = @fisicaFiliacaoNomePai
                                            ,fisicaFiliacaoNomeMae = @fisicaFiliacaoNomeMae
                                            ,fisicaConjugueNomeCompleto = @fisicaConjugueNomeCompleto
                                            ,fisicaConjugueCPF = @fisicaConjugueCPF
                                            ,fisicaConjugueDataNascimento = @fisicaConjugueDataNascimento
                                            ,fisicaNacionalidade = @fisicaNacionalidade
                                            ,fisicaEscolaridade = @fisicaEscolaridade
                                            ,fisicaPISPASEP = @fisicaPISPASEP
                                            ,fisicaCTPSNumero = @fisicaCTPSNumero
                                            ,fisicaCTPSSerie = @fisicaCTPSSerie
                                            ,fisicaCTPSDataExpedicao = @fisicaCTPSDataExpedicao
                                            ,fisicaRegimeComunhaoBens = @fisicaRegimeComunhaoBens
                                            ,fisicaCadSenha = @fisicaCadSenha
                                        WHERE idPessoa = @idPessoa";

                    SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                    cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                    cmdPessoa.Parameters.Add("fisicaNomeCompleto", SqlDbType.VarChar).Value = Pessoa.fisicaNomeCompleto;

                    if (Pessoa.fisicaCPF != null)
                        cmdPessoa.Parameters.Add("fisicaCPF", SqlDbType.VarChar).Value = Pessoa.fisicaCPF.Replace(".", "").Replace("-", "");
                    else
                        cmdPessoa.Parameters.Add("fisicaCPF", SqlDbType.VarChar).Value = DBNull.Value;

                    if (Pessoa.fisicaDataNascimento != null)
                        cmdPessoa.Parameters.Add("fisicaDataNascimento", SqlDbType.DateTime).Value = Pessoa.fisicaDataNascimento;
                    else
                        cmdPessoa.Parameters.Add("fisicaDataNascimento", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoa.Parameters.Add("fisicaSexo", SqlDbType.VarChar).Value = Pessoa.fisicaSexo;
                    cmdPessoa.Parameters.Add("fisicaEstadoCivil", SqlDbType.VarChar).Value = Pessoa.fisicaEstadoCivil;
                    cmdPessoa.Parameters.Add("fisicaProfissao", SqlDbType.VarChar).Value = Pessoa.fisicaProfissao;
                    cmdPessoa.Parameters.Add("fisicaNaturalidade", SqlDbType.VarChar).Value = Pessoa.fisicaNaturalidade;
                    cmdPessoa.Parameters.Add("fisicaRGNumero", SqlDbType.VarChar).Value = Pessoa.fisicaRGNumero;

                    if (Pessoa.fisicaRGDataExpedicao != null)
                        cmdPessoa.Parameters.Add("fisicaRGDataExpedicao", SqlDbType.DateTime).Value = Pessoa.fisicaRGDataExpedicao;
                    else
                        cmdPessoa.Parameters.Add("fisicaRGDataExpedicao", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoa.Parameters.Add("fisicaRGOrgaoExpedidor", SqlDbType.VarChar).Value = Pessoa.fisicaRGOrgaoExpedidor;
                    cmdPessoa.Parameters.Add("fisicaRGUFExpedidor", SqlDbType.VarChar).Value = Pessoa.fisicaRGUFExpedidor;
                    cmdPessoa.Parameters.Add("fisicaCNHNumero", SqlDbType.VarChar).Value = Pessoa.fisicaCNHNumero;
                    cmdPessoa.Parameters.Add("fisicaCNHCategoria", SqlDbType.VarChar).Value = Pessoa.fisicaCNHCategoria;

                    if (Pessoa.fisicaCNHDataHabilitacao != null)
                        cmdPessoa.Parameters.Add("fisicaCNHDataHabilitacao", SqlDbType.DateTime).Value = Pessoa.fisicaCNHDataHabilitacao;
                    else
                        cmdPessoa.Parameters.Add("fisicaCNHDataHabilitacao", SqlDbType.DateTime).Value = DBNull.Value;

                    if (Pessoa.fisicaCNHDataEmissao != null)
                        cmdPessoa.Parameters.Add("fisicaCNHDataEmissao", SqlDbType.DateTime).Value = Pessoa.fisicaCNHDataEmissao;
                    else
                        cmdPessoa.Parameters.Add("fisicaCNHDataEmissao", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoa.Parameters.Add("fisicaFiliacaoNomePai", SqlDbType.VarChar).Value = Pessoa.fisicaFiliacaoNomePai;
                    cmdPessoa.Parameters.Add("fisicaFiliacaoNomeMae", SqlDbType.VarChar).Value = Pessoa.fisicaFiliacaoNomeMae;
                    cmdPessoa.Parameters.Add("fisicaConjugueNomeCompleto", SqlDbType.VarChar).Value = Pessoa.fisicaConjugueNomeCompleto;

                    if (Pessoa.fisicaConjugueCPF != null)
                        cmdPessoa.Parameters.Add("fisicaConjugueCPF", SqlDbType.VarChar).Value = Pessoa.fisicaConjugueCPF.Replace(".", "").Replace("-", "");
                    else
                        cmdPessoa.Parameters.Add("fisicaConjugueCPF", SqlDbType.VarChar).Value = DBNull.Value;

                    if (Pessoa.fisicaConjugueDataNascimento != null)
                        cmdPessoa.Parameters.Add("fisicaConjugueDataNascimento", SqlDbType.DateTime).Value = Pessoa.fisicaConjugueDataNascimento;
                    else
                        cmdPessoa.Parameters.Add("fisicaConjugueDataNascimento", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoa.Parameters.Add("fisicaNacionalidade", SqlDbType.VarChar).Value = Pessoa.fisicaNacionalidade;
                    cmdPessoa.Parameters.Add("fisicaEscolaridade", SqlDbType.VarChar).Value = Pessoa.fisicaEscolaridade;
                    cmdPessoa.Parameters.Add("fisicaPISPASEP", SqlDbType.VarChar).Value = Pessoa.fisicaPISPASEP;
                    cmdPessoa.Parameters.Add("fisicaCTPSNumero", SqlDbType.VarChar).Value = Pessoa.fisicaCTPSNumero;
                    cmdPessoa.Parameters.Add("fisicaCTPSSerie", SqlDbType.VarChar).Value = Pessoa.fisicaCTPSSerie;

                    if (Pessoa.fisicaCTPSDataExpedicao != null)
                        cmdPessoa.Parameters.Add("fisicaCTPSDataExpedicao", SqlDbType.DateTime).Value = Pessoa.fisicaCTPSDataExpedicao;
                    else
                        cmdPessoa.Parameters.Add("fisicaCTPSDataExpedicao", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoa.Parameters.Add("fisicaRegimeComunhaoBens", SqlDbType.VarChar).Value = Pessoa.fisicaRegimeComunhaoBens;

                    cmdPessoa.Parameters.Add("fisicaCadSenha", SqlDbType.VarChar).Value = Pessoa.fisicaCadSenha;

                    TrataParametrosNulos(cmdPessoa.Parameters);
                    TrataEspacosEmBranco(cmdPessoa.Parameters);

                    UpdateEndereco(Pessoa);

                    try
                    {
                        connection.Open();
                        cmdPessoa.ExecuteNonQuery();
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
        }


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertPessoaJuridica(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoaJuridica
                                                   (idPessoa
                                                   ,juridicaRazaoSocial
                                                   ,juridicaNomeFantasia
                                                   ,juridicaCNPJ
                                                   ,juridicaInscricaoMunicipal
                                                   ,juridicaInscricaoEstadual
                                                   ,juridicaRamoAtividade
                                                   ,juridicaDataFundacao
                                                   ,juridicaNomeCompletoSocio1
                                                   ,juridicaEmailSocio1
                                                   ,juridicaTelefoneResidencialSocio1
                                                   ,juridicaTelefoneCelularSocio1
                                                   ,juridicaNomeCompletoSocio2
                                                   ,juridicaEmailSocio2
                                                   ,juridicaTelefoneResidencialSocio2
                                                   ,juridicaTelefoneCelularSocio2)
                                             VALUES
                                                   (@idPessoa
                                                   ,@juridicaRazaoSocial
                                                   ,@juridicaNomeFantasia
                                                   ,@juridicaCNPJ
                                                   ,@juridicaInscricaoMunicipal
                                                   ,@juridicaInscricaoEstadual
                                                   ,@juridicaRamoAtividade
                                                   ,@juridicaDataFundacao
                                                   ,@juridicaNomeCompletoSocio1
                                                   ,@juridicaEmailSocio1
                                                   ,@juridicaTelefoneResidencialSocio1
                                                   ,@juridicaTelefoneCelularSocio1
                                                   ,@juridicaNomeCompletoSocio2
                                                   ,@juridicaEmailSocio2
                                                   ,@juridicaTelefoneResidencialSocio2
                                                   ,@juridicaTelefoneCelularSocio2)";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                cmdPessoa.Parameters.Add("juridicaRazaoSocial", SqlDbType.VarChar).Value = Pessoa.juridicaRazaoSocial;
                cmdPessoa.Parameters.Add("juridicaNomeFantasia", SqlDbType.VarChar).Value = Pessoa.juridicaNomeFantasia;

                if (Pessoa.juridicaCNPJ != null)
                    cmdPessoa.Parameters.Add("juridicaCNPJ", SqlDbType.VarChar).Value = Pessoa.juridicaCNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
                else
                    cmdPessoa.Parameters.Add("juridicaCNPJ", SqlDbType.VarChar).Value = DBNull.Value;

                cmdPessoa.Parameters.Add("juridicaInscricaoMunicipal", SqlDbType.VarChar).Value = Pessoa.juridicaInscricaoMunicipal;
                cmdPessoa.Parameters.Add("juridicaInscricaoEstadual", SqlDbType.VarChar).Value = Pessoa.juridicaInscricaoEstadual;
                
                if (Pessoa.juridicaDataFundacao != null)
                    cmdPessoa.Parameters.Add("juridicaDataFundacao", SqlDbType.DateTime).Value = Pessoa.juridicaDataFundacao;
                else
                    cmdPessoa.Parameters.Add("juridicaDataFundacao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoa.Parameters.Add("juridicaRamoAtividade", SqlDbType.VarChar).Value = Pessoa.juridicaRamoAtividade;
                cmdPessoa.Parameters.Add("juridicaNomeCompletoSocio1", SqlDbType.VarChar).Value = Pessoa.juridicaNomeCompletoSocio1;
                cmdPessoa.Parameters.Add("juridicaEmailSocio1", SqlDbType.VarChar).Value = Pessoa.juridicaEmailSocio1;
                cmdPessoa.Parameters.Add("juridicaTelefoneResidencialSocio1", SqlDbType.VarChar).Value = Pessoa.juridicaTelefoneResidencialSocio1;
                cmdPessoa.Parameters.Add("juridicaTelefoneCelularSocio1", SqlDbType.VarChar).Value = Pessoa.juridicaTelefoneCelularSocio1;
                cmdPessoa.Parameters.Add("juridicaNomeCompletoSocio2", SqlDbType.VarChar).Value = Pessoa.juridicaNomeCompletoSocio2;
                cmdPessoa.Parameters.Add("juridicaEmailSocio2", SqlDbType.VarChar).Value = Pessoa.juridicaEmailSocio2;
                cmdPessoa.Parameters.Add("juridicaTelefoneResidencialSocio2", SqlDbType.VarChar).Value = Pessoa.juridicaTelefoneResidencialSocio2;
                cmdPessoa.Parameters.Add("juridicaTelefoneCelularSocio2", SqlDbType.VarChar).Value = Pessoa.juridicaTelefoneCelularSocio2;

                TrataParametrosNulos(cmdPessoa.Parameters);
                TrataEspacosEmBranco(cmdPessoa.Parameters);

                InsertEndereco(Pessoa);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException("Erro ao inserir registro PJ");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdatePessoaJuridica(dtoPessoa Pessoa)
        {
            if (!ExistsPessoaJuridica(Pessoa))
                InsertPessoaJuridica(Pessoa);
            else
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    string stringSQL = @"UPDATE tbPessoaJuridica
                                       SET juridicaRazaoSocial = @juridicaRazaoSocial
                                          ,juridicaNomeFantasia = @juridicaNomeFantasia
                                          ,juridicaCNPJ = @juridicaCNPJ
                                          ,juridicaInscricaoMunicipal = @juridicaInscricaoMunicipal
                                          ,juridicaInscricaoEstadual = @juridicaInscricaoEstadual
                                          ,juridicaDataFundacao = @juridicaDataFundacao
                                          ,juridicaRamoAtividade = @juridicaRamoAtividade
                                          ,juridicaNomeCompletoSocio1 = @juridicaNomeCompletoSocio1
                                          ,juridicaEmailSocio1 = @juridicaEmailSocio1
                                          ,juridicaTelefoneResidencialSocio1 = @juridicaTelefoneResidencialSocio1
                                          ,juridicaTelefoneCelularSocio1 = @juridicaTelefoneCelularSocio1
                                          ,juridicaNomeCompletoSocio2 = @juridicaNomeCompletoSocio2
                                          ,juridicaEmailSocio2 = @juridicaEmailSocio2
                                          ,juridicaTelefoneResidencialSocio2 = @juridicaTelefoneResidencialSocio2
                                          ,juridicaTelefoneCelularSocio2 = @juridicaTelefoneCelularSocio2
                                        WHERE idPessoa = @idPessoa";

                    SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                    cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                    cmdPessoa.Parameters.Add("juridicaRazaoSocial", SqlDbType.VarChar).Value = Pessoa.juridicaRazaoSocial;
                    cmdPessoa.Parameters.Add("juridicaNomeFantasia", SqlDbType.VarChar).Value = Pessoa.juridicaNomeFantasia;

                    if (Pessoa.juridicaCNPJ != null)
                        cmdPessoa.Parameters.Add("juridicaCNPJ", SqlDbType.VarChar).Value = Pessoa.juridicaCNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
                    else
                        cmdPessoa.Parameters.Add("juridicaCNPJ", SqlDbType.VarChar).Value = DBNull.Value;

                    cmdPessoa.Parameters.Add("juridicaInscricaoMunicipal", SqlDbType.VarChar).Value = Pessoa.juridicaInscricaoMunicipal;
                    cmdPessoa.Parameters.Add("juridicaInscricaoEstadual", SqlDbType.VarChar).Value = Pessoa.juridicaInscricaoEstadual;
                    
                    if (Pessoa.juridicaDataFundacao != null)
                        cmdPessoa.Parameters.Add("juridicaDataFundacao", SqlDbType.DateTime).Value = Pessoa.juridicaDataFundacao;
                    else
                        cmdPessoa.Parameters.Add("juridicaDataFundacao", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoa.Parameters.Add("juridicaRamoAtividade", SqlDbType.VarChar).Value = Pessoa.juridicaRamoAtividade;
                    cmdPessoa.Parameters.Add("juridicaNomeCompletoSocio1", SqlDbType.VarChar).Value = Pessoa.juridicaNomeCompletoSocio1;
                    cmdPessoa.Parameters.Add("juridicaEmailSocio1", SqlDbType.VarChar).Value = Pessoa.juridicaEmailSocio1;
                    cmdPessoa.Parameters.Add("juridicaTelefoneResidencialSocio1", SqlDbType.VarChar).Value = Pessoa.juridicaTelefoneResidencialSocio1;
                    cmdPessoa.Parameters.Add("juridicaTelefoneCelularSocio1", SqlDbType.VarChar).Value = Pessoa.juridicaTelefoneCelularSocio1;
                    cmdPessoa.Parameters.Add("juridicaNomeCompletoSocio2", SqlDbType.VarChar).Value = Pessoa.juridicaNomeCompletoSocio2;
                    cmdPessoa.Parameters.Add("juridicaEmailSocio2", SqlDbType.VarChar).Value = Pessoa.juridicaEmailSocio2;
                    cmdPessoa.Parameters.Add("juridicaTelefoneResidencialSocio2", SqlDbType.VarChar).Value = Pessoa.juridicaTelefoneResidencialSocio2;
                    cmdPessoa.Parameters.Add("juridicaTelefoneCelularSocio2", SqlDbType.VarChar).Value = Pessoa.juridicaTelefoneCelularSocio2;

                    TrataParametrosNulos(cmdPessoa.Parameters);
                    TrataEspacosEmBranco(cmdPessoa.Parameters);

                    UpdateEndereco(Pessoa);

                    try
                    {
                        connection.Open();
                        cmdPessoa.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new ApplicationException("Erro ao atualizar registro PJ");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertEndereco(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoaEndereco
                                                    (idPessoa, enderecoLogradouro, enderecoNumero
                                                    ,enderecoComplemento, enderecoBairro, enderecoCEP
                                                    ,enderecoIdCidade, enderecoIdEstado)
                                            VALUES(@idPessoa, @enderecoLogradouro, @enderecoNumero
                                                    ,@enderecoComplemento, @enderecoBairro, @enderecoCEP
                                                    ,@enderecoIdCidade, @enderecoIdEstado)";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;
                cmdPessoa.Parameters.Add("enderecoLogradouro", SqlDbType.VarChar).Value = Pessoa.enderecoLogradouro;
                cmdPessoa.Parameters.Add("enderecoNumero", SqlDbType.VarChar).Value = Pessoa.enderecoNumero;
                cmdPessoa.Parameters.Add("enderecoComplemento", SqlDbType.VarChar).Value = Pessoa.enderecoComplemento;
                cmdPessoa.Parameters.Add("enderecoBairro", SqlDbType.VarChar).Value = Pessoa.enderecoBairro;
                cmdPessoa.Parameters.Add("enderecoCEP", SqlDbType.VarChar).Value = Pessoa.enderecoCEP;
                cmdPessoa.Parameters.Add("enderecoIdCidade", SqlDbType.Int).Value = Pessoa.enderecoIdCidade;
                cmdPessoa.Parameters.Add("enderecoIdEstado", SqlDbType.Int).Value = Pessoa.enderecoIdEstado;

                TrataParametrosNulos(cmdPessoa.Parameters);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException("Erro ao inserir registro Endereco");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateEndereco(dtoPessoa Pessoa)
        {
            if (!ExistsEndereco(Pessoa))
                InsertEndereco(Pessoa);
            else
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    string stringSQL = @"UPDATE tbPessoaEndereco
                                    SET enderecoLogradouro = @enderecoLogradouro
	                                    ,enderecoNumero = @enderecoNumero
	                                    ,enderecoComplemento = @enderecoComplemento
	                                    ,enderecoBairro = @enderecoBairro
	                                    ,enderecoCEP = @enderecoCEP
                                        ,enderecoIdCidade = @enderecoIdCidade
	                                    ,enderecoIdEstado = @enderecoIdEstado
                                        WHERE idPessoa = @idPessoa";

                    SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                    cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;
                    cmdPessoa.Parameters.Add("enderecoLogradouro", SqlDbType.VarChar).Value = Pessoa.enderecoLogradouro;
                    cmdPessoa.Parameters.Add("enderecoNumero", SqlDbType.VarChar).Value = Pessoa.enderecoNumero;
                    cmdPessoa.Parameters.Add("enderecoComplemento", SqlDbType.VarChar).Value = Pessoa.enderecoComplemento;
                    cmdPessoa.Parameters.Add("enderecoBairro", SqlDbType.VarChar).Value = Pessoa.enderecoBairro;
                    cmdPessoa.Parameters.Add("enderecoCEP", SqlDbType.VarChar).Value = Pessoa.enderecoCEP;
                    cmdPessoa.Parameters.Add("enderecoIdCidade", SqlDbType.Int).Value = Pessoa.enderecoIdCidade;
                    cmdPessoa.Parameters.Add("enderecoIdEstado", SqlDbType.Int).Value = Pessoa.enderecoIdEstado;

                    TrataParametrosNulos(cmdPessoa.Parameters);
                    TrataEspacosEmBranco(cmdPessoa.Parameters);

                    try
                    {
                        connection.Open();
                        cmdPessoa.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new ApplicationException("Erro ao atualizar registro Endereco");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertDadosProfissionais(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoaDadosProfissionais
                                                    (idPessoa, dadosProfissionaisNomeEmpresa, dadosProfissionaisCargo
                                                    ,dadosProfissionaisDataAdmissao, dadosProfissionaisEnderecoLogradouro
                                                    ,dadosProfissionaisEnderecoNumero, dadosProfissionaisEnderecoComplemento
                                                    ,dadosProfissionaisEnderecoBairro, dadosProfissionaisEnderecoCEP
                                                    ,dadosProfissionaisEnderecoIdCidade, dadosProfissionaisEnderecoIdEstado
                                                    ,dadosProfissionaisContatoNomeCompleto, dadosProfissionaisContatoTelefone)
                                            VALUES(@idPessoa, @dadosProfissionaisNomeEmpresa, @dadosProfissionaisCargo
                                                    ,@dadosProfissionaisDataAdmissao, @dadosProfissionaisEnderecoLogradouro
                                                    ,@dadosProfissionaisEnderecoNumero, @dadosProfissionaisEnderecoComplemento
                                                    ,@dadosProfissionaisEnderecoBairro, @dadosProfissionaisEnderecoCEP
                                                    ,@dadosProfissionaisEnderecoIdCidade, @dadosProfissionaisEnderecoIdEstado
                                                    ,@dadosProfissionaisContatoNomeCompleto, @dadosProfissionaisContatoTelefone)";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;
                cmdPessoa.Parameters.Add("dadosProfissionaisNomeEmpresa", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisNomeEmpresa;
                cmdPessoa.Parameters.Add("dadosProfissionaisCargo", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisCargo;

                if (Pessoa.dadosProfissionaisDataAdmissao != null)
                    cmdPessoa.Parameters.Add("dadosProfissionaisDataAdmissao", SqlDbType.DateTime).Value = Pessoa.dadosProfissionaisDataAdmissao;
                else
                    cmdPessoa.Parameters.Add("dadosProfissionaisDataAdmissao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoLogradouro", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoLogradouro;
                cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoNumero", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoNumero;
                cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoComplemento", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoComplemento;
                cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoBairro", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoBairro;
                cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoCEP", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoCEP;
                cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoIdCidade", SqlDbType.Int).Value = Pessoa.dadosProfissionaisEnderecoIdCidade;
                cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoIdEstado", SqlDbType.Int).Value = Pessoa.dadosProfissionaisEnderecoIdEstado;
                cmdPessoa.Parameters.Add("dadosProfissionaisContatoNomeCompleto", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisContatoNomeCompleto;
                cmdPessoa.Parameters.Add("dadosProfissionaisContatoTelefone", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisContatoTelefone;

                TrataParametrosNulos(cmdPessoa.Parameters);
                TrataEspacosEmBranco(cmdPessoa.Parameters);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException("Erro ao inserir registro Dados Profissionais");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateDadosProfissionais(dtoPessoa Pessoa)
        {
            if (!ExistsDadosProfissionais(Pessoa))
                InsertDadosProfissionais(Pessoa);
            else
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    string stringSQL = @"UPDATE tbPessoaDadosProfissionais
                                        SET dadosProfissionaisNomeEmpresa = @dadosProfissionaisNomeEmpresa
	                                    ,dadosProfissionaisCargo = @dadosProfissionaisCargo
	                                    ,dadosProfissionaisDataAdmissao = @dadosProfissionaisDataAdmissao
	                                    ,dadosProfissionaisEnderecoLogradouro = @dadosProfissionaisEnderecoLogradouro
	                                    ,dadosProfissionaisEnderecoNumero = @dadosProfissionaisEnderecoNumero
	                                    ,dadosProfissionaisEnderecoComplemento = @dadosProfissionaisEnderecoComplemento
	                                    ,dadosProfissionaisEnderecoBairro = @dadosProfissionaisEnderecoBairro
	                                    ,dadosProfissionaisEnderecoCEP = @dadosProfissionaisEnderecoCEP
	                                    ,dadosProfissionaisEnderecoIdCidade = @dadosProfissionaisEnderecoIdCidade
	                                    ,dadosProfissionaisEnderecoIdEstado = @dadosProfissionaisEnderecoIdEstado
	                                    ,dadosProfissionaisContatoNomeCompleto = @dadosProfissionaisContatoNomeCompleto
	                                    ,dadosProfissionaisContatoTelefone = @dadosProfissionaisContatoTelefone
                                        WHERE idPessoa = @idPessoa";

                    SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                    cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;
                    cmdPessoa.Parameters.Add("dadosProfissionaisNomeEmpresa", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisNomeEmpresa;
                    cmdPessoa.Parameters.Add("dadosProfissionaisCargo", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisCargo;
                    
                    if (Pessoa.dadosProfissionaisDataAdmissao != null)
                        cmdPessoa.Parameters.Add("dadosProfissionaisDataAdmissao", SqlDbType.DateTime).Value = Pessoa.dadosProfissionaisDataAdmissao;
                    else
                        cmdPessoa.Parameters.Add("dadosProfissionaisDataAdmissao", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoLogradouro", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoLogradouro;
                    cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoNumero", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoNumero;
                    cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoComplemento", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoComplemento;
                    cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoBairro", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoBairro;
                    cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoCEP", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisEnderecoCEP;
                    cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoIdCidade", SqlDbType.Int).Value = Pessoa.dadosProfissionaisEnderecoIdCidade;
                    cmdPessoa.Parameters.Add("dadosProfissionaisEnderecoIdEstado", SqlDbType.Int).Value = Pessoa.dadosProfissionaisEnderecoIdEstado;
                    cmdPessoa.Parameters.Add("dadosProfissionaisContatoNomeCompleto", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisContatoNomeCompleto;
                    cmdPessoa.Parameters.Add("dadosProfissionaisContatoTelefone", SqlDbType.VarChar).Value = Pessoa.dadosProfissionaisContatoTelefone;

                    TrataParametrosNulos(cmdPessoa.Parameters);
                    TrataEspacosEmBranco(cmdPessoa.Parameters);

                    try
                    {
                        connection.Open();
                        cmdPessoa.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new ApplicationException("Erro ao atualizar registro Dados Profissionais");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertReferencias(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoaReferencia
                                                (idPessoa
                                                ,referenciaNomeCompleto1
                                                ,referenciaTelefoneResidencial1
                                                ,referenciaTelefoneCelular1
                                                ,referenciaNomeCompleto2
                                                ,referenciaTelefoneResidencial2
                                                ,referenciaTelefoneCelular2)
                                            VALUES (@idPessoa
                                                ,@referenciaNomeCompleto1
                                                ,@referenciaTelefoneResidencial1
                                                ,@referenciaTelefoneCelular1
                                                ,@referenciaNomeCompleto2
                                                ,@referenciaTelefoneResidencial2
                                                ,@referenciaTelefoneCelular2)";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                cmdPessoa.Parameters.Add("referenciaNomeCompleto1", SqlDbType.VarChar).Value = Pessoa.referenciaNomeCompleto1;
                cmdPessoa.Parameters.Add("referenciaTelefoneResidencial1", SqlDbType.VarChar).Value = Pessoa.referenciaTelefoneResidencial1;
                cmdPessoa.Parameters.Add("referenciaTelefoneCelular1", SqlDbType.VarChar).Value = Pessoa.referenciaTelefoneCelular1;
                cmdPessoa.Parameters.Add("referenciaNomeCompleto2", SqlDbType.VarChar).Value = Pessoa.referenciaNomeCompleto2;
                cmdPessoa.Parameters.Add("referenciaTelefoneResidencial2", SqlDbType.VarChar).Value = Pessoa.referenciaTelefoneResidencial2;
                cmdPessoa.Parameters.Add("referenciaTelefoneCelular2", SqlDbType.VarChar).Value = Pessoa.referenciaTelefoneCelular2;

                TrataParametrosNulos(cmdPessoa.Parameters);
                TrataEspacosEmBranco(cmdPessoa.Parameters);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException("Erro ao inserir registro Referências");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateReferencias(dtoPessoa Pessoa)
        {
            if (!ExistsReferencia(Pessoa))
                InsertReferencias(Pessoa);
            else
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    string stringSQL = @"UPDATE tbPessoaReferencia
                                        SET referenciaNomeCompleto1 = @referenciaNomeCompleto1
                                          ,referenciaTelefoneResidencial1 = @referenciaTelefoneResidencial1
                                          ,referenciaTelefoneCelular1 = @referenciaTelefoneCelular1
                                          ,referenciaNomeCompleto2 = @referenciaNomeCompleto2
                                          ,referenciaTelefoneResidencial2 = @referenciaTelefoneResidencial2
                                          ,referenciaTelefoneCelular2 = @referenciaTelefoneCelular2
                                        WHERE idPessoa = @idPessoa";

                    SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                    cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                    cmdPessoa.Parameters.Add("referenciaNomeCompleto1", SqlDbType.VarChar).Value = Pessoa.referenciaNomeCompleto1;
                    cmdPessoa.Parameters.Add("referenciaTelefoneResidencial1", SqlDbType.VarChar).Value = Pessoa.referenciaTelefoneResidencial1;
                    cmdPessoa.Parameters.Add("referenciaTelefoneCelular1", SqlDbType.VarChar).Value = Pessoa.referenciaTelefoneCelular1;
                    cmdPessoa.Parameters.Add("referenciaNomeCompleto2", SqlDbType.VarChar).Value = Pessoa.referenciaNomeCompleto2;
                    cmdPessoa.Parameters.Add("referenciaTelefoneResidencial2", SqlDbType.VarChar).Value = Pessoa.referenciaTelefoneResidencial2;
                    cmdPessoa.Parameters.Add("referenciaTelefoneCelular2", SqlDbType.VarChar).Value = Pessoa.referenciaTelefoneCelular2;

                    TrataParametrosNulos(cmdPessoa.Parameters);
                    TrataEspacosEmBranco(cmdPessoa.Parameters);

                    try
                    {
                        connection.Open();
                        cmdPessoa.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new ApplicationException("Erro ao atualizar registro Rerefências");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertContato(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoaContato
                                                (idPessoa
                                                ,contatoTelefoneResidencial
                                                ,contatoTelefoneComercial
                                                ,contatoTelefoneCelular
                                                ,contatoTelefoneCelular1
                                                ,contatoTelefoneCelular2
                                                ,contatoEmail
                                               ,contatoObservacao)
                                            VALUES (@idPessoa
                                                ,@contatoTelefoneResidencial
                                                ,@contatoTelefoneComercial
                                                ,@contatoTelefoneCelular
                                                ,@contatoTelefoneCelular1
                                                ,@contatoTelefoneCelular2
                                                ,@contatoEmail
                                                ,@contatoObservacao)";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                cmdPessoa.Parameters.Add("contatoTelefoneResidencial", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneResidencial;
                cmdPessoa.Parameters.Add("contatoTelefoneComercial", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneComercial;
                cmdPessoa.Parameters.Add("contatoTelefoneCelular", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneCelular;
                cmdPessoa.Parameters.Add("contatoTelefoneCelular1", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneCelular1;
                cmdPessoa.Parameters.Add("contatoTelefoneCelular2", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneCelular2;
                cmdPessoa.Parameters.Add("contatoEmail", SqlDbType.VarChar).Value = Pessoa.contatoEmail;
                cmdPessoa.Parameters.Add("contatoObservacao", SqlDbType.VarChar).Value = Pessoa.contatoObservacao;

                TrataParametrosNulos(cmdPessoa.Parameters);
                TrataEspacosEmBranco(cmdPessoa.Parameters);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException("Erro ao inserir registro Referências");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateContato(dtoPessoa Pessoa)
        {
            if (!ExistsContato(Pessoa))
                InsertContato(Pessoa);
            else
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    string stringSQL = @"UPDATE tbPessoaContato
                                        SET contatoTelefoneResidencial = @contatoTelefoneResidencial
                                          ,contatoTelefoneComercial = @contatoTelefoneComercial
                                          ,contatoTelefoneCelular = @contatoTelefoneCelular
                                          ,contatoTelefoneCelular1 = @contatoTelefoneCelular1
                                          ,contatoTelefoneCelular2 = @contatoTelefoneCelular2
                                          ,contatoEmail = @contatoEmail
                                          ,contatoObservacao = @contatoObservacao
                                        WHERE idPessoa = @idPessoa";

                    SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                    cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                    cmdPessoa.Parameters.Add("contatoTelefoneResidencial", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneResidencial;
                    cmdPessoa.Parameters.Add("contatoTelefoneComercial", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneComercial;
                    cmdPessoa.Parameters.Add("contatoTelefoneCelular", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneCelular;
                    cmdPessoa.Parameters.Add("contatoTelefoneCelular1", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneCelular1;
                    cmdPessoa.Parameters.Add("contatoTelefoneCelular2", SqlDbType.VarChar).Value = Pessoa.contatoTelefoneCelular2;
                    cmdPessoa.Parameters.Add("contatoEmail", SqlDbType.VarChar).Value = Pessoa.contatoEmail;
                    cmdPessoa.Parameters.Add("contatoObservacao", SqlDbType.VarChar).Value = Pessoa.contatoObservacao;

                    TrataParametrosNulos(cmdPessoa.Parameters);
                    TrataEspacosEmBranco(cmdPessoa.Parameters);

                    try
                    {
                        connection.Open();
                        cmdPessoa.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new ApplicationException("Erro ao atualizar registro Rerefências");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertColaborador(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoaColaborador
                                                (idPessoa
                                                ,colaboradorCargo
                                                ,colaboradorDataAdmissao
                                                ,colaboradorPadraoPrazoProcessual)
                                            VALUES (@idPessoa
                                                ,@colaboradorCargo
                                                ,@colaboradorDataAdmissao,
                                                ,@colaboradorPadraoPrazoProcessual)";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                cmdPessoa.Parameters.Add("colaboradorCargo", SqlDbType.VarChar).Value = Pessoa.colaboradorCargo;

                if (Pessoa.colaboradorDataAdmissao != null)
                    cmdPessoa.Parameters.Add("colaboradorDataAdmissao", SqlDbType.DateTime).Value = Pessoa.colaboradorDataAdmissao;
                else
                    cmdPessoa.Parameters.Add("colaboradorDataAdmissao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoa.Parameters.Add("colaboradorPadraoPrazoProcessual", SqlDbType.Bit).Value = Pessoa.colaboradorPadraoPrazoProcessual;

                TrataParametrosNulos(cmdPessoa.Parameters);
                TrataEspacosEmBranco(cmdPessoa.Parameters);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException("Erro ao inserir registro Referências");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateColaborador(dtoPessoa Pessoa)
        {
            if (!ExistsColaborador(Pessoa))
                InsertColaborador(Pessoa);
            else
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    string stringSQL = @"UPDATE tbPessoaColaborador
                                        SET colaboradorCargo = @colaboradorCargo
                                        ,colaboradorDataAdmissao = @colaboradorDataAdmissao
                                        ,colaboradorPadraoPrazoProcessual = @colaboradorPadraoPrazoProcessual
                                        WHERE idPessoa = @idPessoa";

                    SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                    cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                    cmdPessoa.Parameters.Add("colaboradorCargo", SqlDbType.VarChar).Value = Pessoa.colaboradorCargo;

                    if (Pessoa.colaboradorDataAdmissao != null)
                        cmdPessoa.Parameters.Add("colaboradorDataAdmissao", SqlDbType.DateTime).Value = Pessoa.colaboradorDataAdmissao;
                    else
                        cmdPessoa.Parameters.Add("colaboradorDataAdmissao", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoa.Parameters.Add("colaboradorPadraoPrazoProcessual", SqlDbType.Bit).Value = Pessoa.colaboradorPadraoPrazoProcessual;

                    TrataParametrosNulos(cmdPessoa.Parameters);
                    TrataEspacosEmBranco(cmdPessoa.Parameters);

                    try
                    {
                        connection.Open();
                        cmdPessoa.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new ApplicationException("Erro ao atualizar registro Rerefências");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void InsertAdvogado(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoaAdvogado
                                                (idPessoa
                                                ,advogadoNumeroOAB
                                                ,advogadoPadraoProcesso
                                                ,advogadoPadraoPrazoProcessual)
                                            VALUES (@idPessoa
                                                ,@advogadoNumeroOAB
                                                ,@advogadoPadraoProcesso
                                                ,@advogadoPadraoPrazoProcessual)";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                cmdPessoa.Parameters.Add("advogadoNumeroOAB", SqlDbType.VarChar).Value = Pessoa.advogadoNumeroOAB;
                cmdPessoa.Parameters.Add("advogadoPadraoProcesso", SqlDbType.Bit).Value = Pessoa.advogadoPadraoProcesso;
                cmdPessoa.Parameters.Add("advogadoPadraoPrazoProcessual", SqlDbType.Bit).Value = Pessoa.advogadoPadraoPrazoProcessual;

                TrataParametrosNulos(cmdPessoa.Parameters);
                TrataEspacosEmBranco(cmdPessoa.Parameters);

                try
                {
                    connection.Open();
                    cmdPessoa.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException("Erro ao inserir registro Referências");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateAdvogado(dtoPessoa Pessoa)
        {
            if (!ExistsAdvogado(Pessoa))
                InsertAdvogado(Pessoa);
            else
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    string stringSQL = @"UPDATE tbPessoaAdvogado
                                        SET advogadoNumeroOAB = @advogadoNumeroOAB
                                        ,advogadoPadraoProcesso = @advogadoPadraoProcesso
                                        ,advogadoPadraoPrazoProcessual = @advogadoPadraoPrazoProcessual
                                        WHERE idPessoa = @idPessoa";

                    SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                    cmdPessoa.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                    cmdPessoa.Parameters.Add("advogadoNumeroOAB", SqlDbType.VarChar).Value = Pessoa.advogadoNumeroOAB;
                    cmdPessoa.Parameters.Add("advogadoPadraoProcesso", SqlDbType.Bit).Value = Pessoa.advogadoPadraoProcesso;
                    cmdPessoa.Parameters.Add("advogadoPadraoPrazoProcessual", SqlDbType.Bit).Value = Pessoa.advogadoPadraoPrazoProcessual;

                    TrataParametrosNulos(cmdPessoa.Parameters);
                    TrataEspacosEmBranco(cmdPessoa.Parameters);

                    try
                    {
                        connection.Open();
                        cmdPessoa.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw new ApplicationException("Erro ao atualizar registro Rerefências");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }


        public static bool ExistsDadosProfissionais(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaDadosProfissionais
                                    WHERE idPessoa = @idPessoa";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool ExistsEndereco(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaEndereco
                                    WHERE idPessoa = @idPessoa";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool ExistsReferencia(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaReferencia
                                    WHERE idPessoa = @idPessoa";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool ExistsAdvogado(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaAdvogado
                                    WHERE idPessoa = @idPessoa";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool ExistsContato(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaContato
                                    WHERE idPessoa = @idPessoa";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool ExistsColaborador(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaColaborador
                                    WHERE idPessoa = @idPessoa";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool ExistsPessoaFisica(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaFisica
                                    WHERE idPessoa = @idPessoa";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool ExistsPessoaFisica(string CPF)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaFisica
                                    WHERE fisicaCPF = @fisicaCPF";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("fisicaCPF", SqlDbType.VarChar).Value = CPF;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool ExistsPessoaJuridica(dtoPessoa Pessoa)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaJuridica
                                    WHERE idPessoa = @idPessoa";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idPessoa", SqlDbType.Int).Value = Pessoa.idPessoa;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static bool ExistsPessoaJuridica(string CNPJ)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaJuridica
                                    WHERE juridicaCNPJ = @juridicaCNPJ";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("juridicaCNPJ", SqlDbType.VarChar).Value = CNPJ;

                try
                {
                    connection.Open();
                    return (cmdExists.ExecuteScalar() != null && cmdExists.ExecuteScalar().ToString() == "1");
                }
                catch
                {
                    throw new ApplicationException("Erro ao buscar o registro");
                }
                finally
                {
                    connection.Close();
                }
            }

        }



    }

    // PRIVATE METHODS 
    public partial class bllPessoa
    {

        private static void PreencheCampos(SqlDataReader drPessoa, ref dtoPessoa Pessoa)
        {

            // DADOS CADASTRAIS
            if (drPessoa["idPessoa"] != null
                && drPessoa["idPessoa"] != DBNull.Value)
                Pessoa.idPessoa = Convert.ToInt32(drPessoa["idPessoa"].ToString());

            if (drPessoa["especiePessoa"] != DBNull.Value)
                Pessoa.especiePessoa = drPessoa["especiePessoa"].ToString();

            if (drPessoa["tipoPessoaCliente"] != DBNull.Value)
                Pessoa.tipoPessoaCliente = Convert.ToBoolean(drPessoa["tipoPessoaCliente"]);

            if (drPessoa["tipoPessoaParte"] != DBNull.Value)
                Pessoa.tipoPessoaParte = Convert.ToBoolean(drPessoa["tipoPessoaParte"]);

            if (drPessoa["tipoPessoaAdvogado"] != DBNull.Value)
                Pessoa.tipoPessoaAdvogado = Convert.ToBoolean(drPessoa["tipoPessoaAdvogado"]);

            if (drPessoa["tipoPessoaColaborador"] != DBNull.Value)
                Pessoa.tipoPessoaColaborador = Convert.ToBoolean(drPessoa["tipoPessoaColaborador"]);

            if (drPessoa["tipoPessoaTerceiro"] != DBNull.Value)
                Pessoa.tipoPessoaTerceiro = Convert.ToBoolean(drPessoa["tipoPessoaTerceiro"]);

            if (drPessoa["dataCadastro"] != DBNull.Value)
                Pessoa.dataCadastro = Convert.ToDateTime(drPessoa["dataCadastro"]);
            else
                Pessoa.dataCadastro = null;

            if (drPessoa["dataUltimaAlteracao"] != DBNull.Value)
                Pessoa.dataUltimaAlteracao = Convert.ToDateTime(drPessoa["dataUltimaAlteracao"]);
            else
                Pessoa.dataUltimaAlteracao = null;

            // PESSOA ADVOGADO
            if (drPessoa["advogadoNumeroOAB"] != DBNull.Value)
                Pessoa.advogadoNumeroOAB = drPessoa["advogadoNumeroOAB"].ToString();

            if (drPessoa["advogadoPadraoProcesso"] != DBNull.Value)
                Pessoa.advogadoPadraoProcesso = Convert.ToBoolean(drPessoa["advogadoPadraoProcesso"].ToString());

            if (drPessoa["advogadoPadraoPrazoProcessual"] != DBNull.Value)
                Pessoa.advogadoPadraoPrazoProcessual = Convert.ToBoolean(drPessoa["advogadoPadraoPrazoProcessual"].ToString());

            // PESSOA COLABORADOR
            if (drPessoa["colaboradorCargo"] != DBNull.Value)
                Pessoa.colaboradorCargo = drPessoa["colaboradorCargo"].ToString();

            if (drPessoa["colaboradorDataAdmissao"] != DBNull.Value)
                Pessoa.colaboradorDataAdmissao = Convert.ToDateTime(drPessoa["colaboradorDataAdmissao"].ToString());
            else
                Pessoa.colaboradorDataAdmissao = null;

            if (drPessoa["colaboradorPadraoPrazoProcessual"] != DBNull.Value)
                Pessoa.colaboradorPadraoPrazoProcessual = Convert.ToBoolean(drPessoa["colaboradorPadraoPrazoProcessual"].ToString());

            // PESSOA CONTATO
            if (drPessoa["contatoTelefoneResidencial"] != DBNull.Value)
                Pessoa.contatoTelefoneResidencial = drPessoa["contatoTelefoneResidencial"].ToString();

            if (drPessoa["contatoTelefoneComercial"] != DBNull.Value)
                Pessoa.contatoTelefoneComercial = drPessoa["contatoTelefoneComercial"].ToString();

            if (drPessoa["contatoTelefoneCelular"] != DBNull.Value)
                Pessoa.contatoTelefoneCelular = drPessoa["contatoTelefoneCelular"].ToString();

            if (drPessoa["contatoTelefoneCelular1"] != DBNull.Value)
                Pessoa.contatoTelefoneCelular1 = drPessoa["contatoTelefoneCelular1"].ToString();

            if (drPessoa["contatoTelefoneCelular2"] != DBNull.Value)
                Pessoa.contatoTelefoneCelular2 = drPessoa["contatoTelefoneCelular2"].ToString();

            if (drPessoa["contatoEmail"] != DBNull.Value)
                Pessoa.contatoEmail = drPessoa["contatoEmail"].ToString();

            if (drPessoa["contatoObservacao"] != DBNull.Value)
                Pessoa.contatoObservacao = drPessoa["contatoObservacao"].ToString();


            // PESSOA DADOS PROFISSIONAIS
            if (drPessoa["dadosProfissionaisNomeEmpresa"] != DBNull.Value)
                Pessoa.dadosProfissionaisNomeEmpresa = drPessoa["dadosProfissionaisNomeEmpresa"].ToString();

            if (drPessoa["dadosProfissionaisCargo"] != DBNull.Value)
                Pessoa.dadosProfissionaisCargo = drPessoa["dadosProfissionaisCargo"].ToString();

            if (drPessoa["dadosProfissionaisDataAdmissao"] != DBNull.Value)
                Pessoa.dadosProfissionaisDataAdmissao = Convert.ToDateTime(drPessoa["dadosProfissionaisDataAdmissao"].ToString());
            else
                Pessoa.dadosProfissionaisDataAdmissao = null;

            if (drPessoa["dadosProfissionaisEnderecoLogradouro"] != DBNull.Value)
                Pessoa.dadosProfissionaisEnderecoLogradouro = drPessoa["dadosProfissionaisEnderecoLogradouro"].ToString();

            if (drPessoa["dadosProfissionaisEnderecoNumero"] != DBNull.Value)
                Pessoa.dadosProfissionaisEnderecoNumero = drPessoa["dadosProfissionaisEnderecoNumero"].ToString();

            if (drPessoa["dadosProfissionaisEnderecoComplemento"] != DBNull.Value)
                Pessoa.dadosProfissionaisEnderecoComplemento = drPessoa["dadosProfissionaisEnderecoComplemento"].ToString();

            if (drPessoa["dadosProfissionaisEnderecoBairro"] != DBNull.Value)
                Pessoa.dadosProfissionaisEnderecoBairro = drPessoa["dadosProfissionaisEnderecoBairro"].ToString();

            if (drPessoa["dadosProfissionaisEnderecoCEP"] != DBNull.Value)
                Pessoa.dadosProfissionaisEnderecoCEP = drPessoa["dadosProfissionaisEnderecoCEP"].ToString();

            if (drPessoa["dadosProfissionaisEnderecoIdCidade"] != DBNull.Value)
                Pessoa.dadosProfissionaisEnderecoIdCidade = Convert.ToInt32(drPessoa["dadosProfissionaisEnderecoIdCidade"].ToString());

            if (drPessoa["dadosProfissionaisEnderecoIdEstado"] != DBNull.Value)
                Pessoa.dadosProfissionaisEnderecoIdEstado = Convert.ToInt32(drPessoa["dadosProfissionaisEnderecoIdEstado"].ToString());

            if (drPessoa["dadosProfissionaisContatoNomeCompleto"] != DBNull.Value)
                Pessoa.dadosProfissionaisContatoNomeCompleto = drPessoa["dadosProfissionaisContatoNomeCompleto"].ToString();

            if (drPessoa["dadosProfissionaisContatoTelefone"] != DBNull.Value)
                Pessoa.dadosProfissionaisContatoTelefone = drPessoa["dadosProfissionaisContatoTelefone"].ToString();

            // PESSOA ENDEREÇO
            if (drPessoa["enderecoLogradouro"] != DBNull.Value)
                Pessoa.enderecoLogradouro = drPessoa["enderecoLogradouro"].ToString();

            if (drPessoa["enderecoNumero"] != DBNull.Value)
                Pessoa.enderecoNumero = drPessoa["enderecoNumero"].ToString();

            if (drPessoa["enderecoComplemento"] != DBNull.Value)
                Pessoa.enderecoComplemento = drPessoa["enderecoComplemento"].ToString();

            if (drPessoa["enderecoBairro"] != DBNull.Value)
                Pessoa.enderecoBairro = drPessoa["enderecoBairro"].ToString();

            if (drPessoa["enderecoCEP"] != DBNull.Value)
                Pessoa.enderecoCEP = drPessoa["enderecoCEP"].ToString();

            if (drPessoa["enderecoIdCidade"] != DBNull.Value)
                Pessoa.enderecoIdCidade = Convert.ToInt32(drPessoa["enderecoIdCidade"].ToString());

            if (drPessoa["enderecoIdEstado"] != DBNull.Value)
                Pessoa.enderecoIdEstado = Convert.ToInt32(drPessoa["enderecoIdEstado"].ToString());


            // PESSOA FÍSICA
            if (drPessoa["fisicaNomeCompleto"] != DBNull.Value)
                Pessoa.fisicaNomeCompleto = drPessoa["fisicaNomeCompleto"].ToString();

            if (drPessoa["fisicaCPF"] != DBNull.Value)
                Pessoa.fisicaCPF = drPessoa["fisicaCPF"].ToString();

            if (drPessoa["fisicaDataNascimento"] != DBNull.Value)
                Pessoa.fisicaDataNascimento = Convert.ToDateTime(drPessoa["fisicaDataNascimento"].ToString());
            else
                Pessoa.fisicaDataNascimento = null;

            if (drPessoa["fisicaSexo"] != DBNull.Value)
                Pessoa.fisicaSexo = drPessoa["fisicaSexo"].ToString();

            if (drPessoa["fisicaEstadoCivil"] != DBNull.Value)
                Pessoa.fisicaEstadoCivil = drPessoa["fisicaEstadoCivil"].ToString();

            if (drPessoa["fisicaProfissao"] != DBNull.Value)
                Pessoa.fisicaProfissao = drPessoa["fisicaProfissao"].ToString();

            if (drPessoa["fisicaNaturalidade"] != DBNull.Value)
                Pessoa.fisicaNaturalidade = drPessoa["fisicaNaturalidade"].ToString();

            if (drPessoa["fisicaRGNumero"] != DBNull.Value)
                Pessoa.fisicaRGNumero = drPessoa["fisicaRGNumero"].ToString();

            if (drPessoa["fisicaRGDataExpedicao"] != DBNull.Value)
                Pessoa.fisicaRGDataExpedicao = Convert.ToDateTime(drPessoa["fisicaRGDataExpedicao"].ToString());
            else
                Pessoa.fisicaRGDataExpedicao = null;

            if (drPessoa["fisicaRGOrgaoExpedidor"] != DBNull.Value)
                Pessoa.fisicaRGOrgaoExpedidor = drPessoa["fisicaRGOrgaoExpedidor"].ToString();

            if (drPessoa["fisicaRGUFExpedidor"] != DBNull.Value)
                Pessoa.fisicaRGUFExpedidor = drPessoa["fisicaRGUFExpedidor"].ToString();

            if (drPessoa["fisicaCNHNumero"] != DBNull.Value)
                Pessoa.fisicaCNHNumero = drPessoa["fisicaCNHNumero"].ToString();

            if (drPessoa["fisicaCNHCategoria"] != DBNull.Value)
                Pessoa.fisicaCNHCategoria = drPessoa["fisicaCNHCategoria"].ToString();

            if (drPessoa["fisicaCNHDataHabilitacao"] != DBNull.Value)
                Pessoa.fisicaCNHDataHabilitacao = Convert.ToDateTime(drPessoa["fisicaCNHDataHabilitacao"].ToString());
            else
                Pessoa.fisicaCNHDataHabilitacao = null;

            if (drPessoa["fisicaCNHDataEmissao"] != DBNull.Value)
                Pessoa.fisicaCNHDataEmissao = Convert.ToDateTime(drPessoa["fisicaCNHDataEmissao"].ToString());
            else
                Pessoa.fisicaCNHDataEmissao = null;

            if (drPessoa["fisicaFiliacaoNomePai"] != DBNull.Value)
                Pessoa.fisicaFiliacaoNomePai = drPessoa["fisicaFiliacaoNomePai"].ToString();

            if (drPessoa["fisicaFiliacaoNomeMae"] != DBNull.Value)
                Pessoa.fisicaFiliacaoNomeMae = drPessoa["fisicaFiliacaoNomeMae"].ToString();

            if (drPessoa["fisicaConjugueNomeCompleto"] != DBNull.Value)
                Pessoa.fisicaConjugueNomeCompleto = drPessoa["fisicaConjugueNomeCompleto"].ToString();

            if (drPessoa["fisicaConjugueCPF"] != DBNull.Value)
                Pessoa.fisicaConjugueCPF = drPessoa["fisicaConjugueCPF"].ToString();

            if (drPessoa["fisicaConjugueDataNascimento"] != DBNull.Value)
                Pessoa.fisicaConjugueDataNascimento = Convert.ToDateTime(drPessoa["fisicaConjugueDataNascimento"].ToString());
            else
                Pessoa.fisicaConjugueDataNascimento = null;

            if (drPessoa["fisicaNacionalidade"] != DBNull.Value)
                Pessoa.fisicaNacionalidade = drPessoa["fisicaNacionalidade"].ToString();

            if (drPessoa["fisicaEscolaridade"] != DBNull.Value)
                Pessoa.fisicaEscolaridade = drPessoa["fisicaEscolaridade"].ToString();

            if (drPessoa["fisicaPISPASEP"] != DBNull.Value)
                Pessoa.fisicaPISPASEP = drPessoa["fisicaPISPASEP"].ToString();

            if (drPessoa["fisicaCTPSNumero"] != DBNull.Value)
                Pessoa.fisicaCTPSNumero = drPessoa["fisicaCTPSNumero"].ToString();

            if (drPessoa["fisicaCTPSSerie"] != DBNull.Value)
                Pessoa.fisicaCTPSSerie = drPessoa["fisicaCTPSSerie"].ToString();

            if (drPessoa["fisicaCTPSDataExpedicao"] != DBNull.Value)
                Pessoa.fisicaCTPSDataExpedicao = Convert.ToDateTime(drPessoa["fisicaCTPSDataExpedicao"].ToString());
            else
                Pessoa.fisicaCTPSDataExpedicao = null;

            if (drPessoa["fisicaRegimeComunhaoBens"] != DBNull.Value)
                Pessoa.fisicaRegimeComunhaoBens = drPessoa["fisicaRegimeComunhaoBens"].ToString();

            if (drPessoa["fisicaCadSenha"] != DBNull.Value)
                Pessoa.fisicaCadSenha = drPessoa["fisicaCadSenha"].ToString();


            // PESSOA JURÍDICA
            if (drPessoa["juridicaRazaoSocial"] != DBNull.Value)
                Pessoa.juridicaRazaoSocial = drPessoa["juridicaRazaoSocial"].ToString();

            if (drPessoa["juridicaNomeFantasia"] != DBNull.Value)
                Pessoa.juridicaNomeFantasia = drPessoa["juridicaNomeFantasia"].ToString();

            if (drPessoa["juridicaCNPJ"] != DBNull.Value)
                Pessoa.juridicaCNPJ = drPessoa["juridicaCNPJ"].ToString();

            if (drPessoa["juridicaInscricaoMunicipal"] != DBNull.Value)
                Pessoa.juridicaInscricaoMunicipal = drPessoa["juridicaInscricaoMunicipal"].ToString();

            if (drPessoa["juridicaInscricaoEstadual"] != DBNull.Value)
                Pessoa.juridicaInscricaoEstadual = drPessoa["juridicaInscricaoEstadual"].ToString();

            if (drPessoa["juridicaRamoAtividade"] != DBNull.Value)
                Pessoa.juridicaRamoAtividade = drPessoa["juridicaRamoAtividade"].ToString();

            if (drPessoa["juridicaDataFundacao"] != DBNull.Value)
                Pessoa.juridicaDataFundacao = Convert.ToDateTime(drPessoa["juridicaDataFundacao"].ToString());
            else
                Pessoa.juridicaDataFundacao = null;

            if (drPessoa["juridicaNomeCompletoSocio1"] != DBNull.Value)
                Pessoa.juridicaNomeCompletoSocio1 = drPessoa["juridicaNomeCompletoSocio1"].ToString();

            if (drPessoa["juridicaEmailSocio1"] != DBNull.Value)
                Pessoa.juridicaEmailSocio1 = drPessoa["juridicaEmailSocio1"].ToString();

            if (drPessoa["juridicaTelefoneResidencialSocio1"] != DBNull.Value)
                Pessoa.juridicaTelefoneResidencialSocio1 = drPessoa["juridicaTelefoneResidencialSocio1"].ToString();

            if (drPessoa["juridicaTelefoneCelularSocio1"] != DBNull.Value)
                Pessoa.juridicaTelefoneCelularSocio1 = drPessoa["juridicaTelefoneCelularSocio1"].ToString();

            if (drPessoa["juridicaNomeCompletoSocio2"] != DBNull.Value)
                Pessoa.juridicaNomeCompletoSocio2 = drPessoa["juridicaNomeCompletoSocio2"].ToString();

            if (drPessoa["juridicaEmailSocio2"] != DBNull.Value)
                Pessoa.juridicaEmailSocio2 = drPessoa["juridicaEmailSocio2"].ToString();

            if (drPessoa["juridicaTelefoneResidencialSocio2"] != DBNull.Value)
                Pessoa.juridicaTelefoneResidencialSocio2 = drPessoa["juridicaTelefoneResidencialSocio2"].ToString();

            if (drPessoa["juridicaTelefoneCelularSocio2"] != DBNull.Value)
                Pessoa.juridicaTelefoneCelularSocio2 = drPessoa["juridicaTelefoneCelularSocio2"].ToString();


            // PESSOA REFERENCIA
            if (drPessoa["referenciaNomeCompleto1"] != DBNull.Value)
                Pessoa.referenciaNomeCompleto1 = drPessoa["referenciaNomeCompleto1"].ToString();

            if (drPessoa["referenciaTelefoneResidencial1"] != DBNull.Value)
                Pessoa.referenciaTelefoneResidencial1 = drPessoa["referenciaTelefoneResidencial1"].ToString();

            if (drPessoa["referenciaTelefoneCelular1"] != DBNull.Value)
                Pessoa.referenciaTelefoneCelular1 = drPessoa["referenciaTelefoneCelular1"].ToString();

            if (drPessoa["referenciaNomeCompleto2"] != DBNull.Value)
                Pessoa.referenciaNomeCompleto2 = drPessoa["referenciaNomeCompleto2"].ToString();

            if (drPessoa["referenciaTelefoneResidencial2"] != DBNull.Value)
                Pessoa.referenciaTelefoneResidencial2 = drPessoa["referenciaTelefoneResidencial2"].ToString();

            if (drPessoa["referenciaTelefoneCelular2"] != DBNull.Value)
                Pessoa.referenciaTelefoneCelular2 = drPessoa["referenciaTelefoneCelular2"].ToString();


        }

        private static void TrataParametrosNulos(SqlParameterCollection Parametros)
        {
            foreach (SqlParameter parametro in Parametros)
            {
                //if (((parametro.SqlDbType == SqlDbType.VarChar && parametro.Value == null)
                //    || parametro.SqlDbType == SqlDbType.DateTime && parametro.Value == null)
                //    || parametro.SqlDbType == SqlDbType.DateTime && (DateTime)parametro.Value == new DateTime(1, 1, 1))
                //{
                //    parametro.Value = DBNull.Value;
                //}
                if ((parametro.SqlDbType == SqlDbType.VarChar && parametro.Value == null)
                    || parametro.SqlDbType == SqlDbType.DateTime && parametro.Value == null)
                {
                    parametro.Value = DBNull.Value;
                }

            }
        }

        private static void TrataEspacosEmBranco(SqlParameterCollection Parametros)
        {
            foreach (SqlParameter parametro in Parametros)
            {
                if (parametro.SqlDbType == SqlDbType.VarChar && parametro.Value != null)
                {
                    parametro.Value = parametro.Value.ToString().Trim();
                }
            }
        }

    }

    // PUBLIC METHODS
    public partial class bllPessoa
    {
        public static string RetornaDescricaoRegimeComunhaoBens(object siglaRegimeComunhaoBens)
        {
            string retorno = String.Empty;

            if (siglaRegimeComunhaoBens != null)
            {
                switch ((string)siglaRegimeComunhaoBens)
                {
                    case "CP":
                        retorno = "Comunhão parcial de bens";
                        break;
                    case "CU":
                        retorno = "Comunhão universal de bens";
                        break;
                    case "ST":
                        retorno = "Separação total de bens";
                        break;
                    case "PF":
                        retorno = "Participação final nos aquestos";
                        break;
                }
            }

            return retorno;
        }

        public static string RetornaDescricaoEscolaridade(object siglaEscolaridade)
        {
            string retorno = String.Empty;

            if (siglaEscolaridade != null)
            {
                switch ((string)siglaEscolaridade)
                {
                    case "FI":
                        retorno = "Fundamental incompleto";
                        break;
                    case "FC":
                        retorno = "Fundamental completo";
                        break;
                    case "MI":
                        retorno = "Ensino médio incompleto";
                        break;
                    case "MC":
                        retorno = "Ensino médio completo";
                        break;
                    case "SI":
                        retorno = "Superior incompleto";
                        break;
                    case "SC":
                        retorno = "Superior completo";
                        break;
                }
            }

            return retorno;
        }

        public static string RetornaDescricaoEstadoCivil(object siglaEstadoCivil)
        {
            string retorno = String.Empty;

            if (siglaEstadoCivil != null)
            {
                switch ((string)siglaEstadoCivil)
                {
                    case "S":
                        retorno = "Solteiro(a)";
                        break;
                    case "C":
                        retorno = "Casado(a)";
                        break;
                    case "D":
                        retorno = "Divorciado(a)";
                        break;
                    case "V":
                        retorno = "Viúvo(a)";
                        break;
                    case "P":
                        retorno = "Separado(a)";
                        break;
                }
            }

            return retorno;
        }

        public static string RetornaDescricaoSexo(object siglaSexo)
        {
            string retorno = String.Empty;

            if (siglaSexo != null)
            {
                switch ((string)siglaSexo)
                {
                    case "M":
                        retorno = "Masculino";
                        break;
                    case "F":
                        retorno = "Feminino";
                        break;
                }
            }

            return retorno;
        }

        public static string RetornaDescricaoEspeciePessoa(object siglaEspeciePessoa)
        {
            string retorno = String.Empty;

            if (siglaEspeciePessoa != null)
            {
                switch ((string)siglaEspeciePessoa)
                {
                    case "F":
                        retorno = "Física";
                        break;
                    case "J":
                        retorno = "Jurídica";
                        break;
                }
            }

            return retorno;
        }


    }

}
