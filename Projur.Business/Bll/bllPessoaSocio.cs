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
    public class bllPessoaSocio
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void Insert(dtoPessoaSocio pessoaSocio)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbPessoaSocio
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
                                                    ,enderecoLogradouro
                                                    ,enderecoNumero
                                                    ,enderecoComplemento
                                                    ,enderecoBairro
                                                    ,enderecoCEP
                                                    ,enderecoIdCidade
                                                    ,enderecoIdEstado
                                                    ,contatoTelefoneResidencial
                                                    ,contatoTelefoneComercial
                                                    ,contatoTelefoneCelular
                                                    ,contatoEmail
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
                                                    ,@enderecoLogradouro
                                                    ,@enderecoNumero
                                                    ,@enderecoComplemento
                                                    ,@enderecoBairro
                                                    ,@enderecoCEP
                                                    ,@enderecoIdCidade
                                                    ,@enderecoIdEstado
                                                    ,@contatoTelefoneResidencial
                                                    ,@contatoTelefoneComercial
                                                    ,@contatoTelefoneCelular
                                                    ,@contatoEmail
                                                    )";

                SqlCommand cmdPessoaSocio = new SqlCommand(stringSQL, connection);

                cmdPessoaSocio.Parameters.Add("idPessoa", SqlDbType.Int).Value = pessoaSocio.idPessoa;

                // PESSOA FÍSICA
                cmdPessoaSocio.Parameters.Add("fisicaNomeCompleto", SqlDbType.VarChar).Value = pessoaSocio.fisicaNomeCompleto;

                if (pessoaSocio.fisicaCPF != null)
                    cmdPessoaSocio.Parameters.Add("fisicaCPF", SqlDbType.VarChar).Value = pessoaSocio.fisicaCPF.Replace(".", "").Replace("-", "");
                else
                    cmdPessoaSocio.Parameters.Add("fisicaCPF", SqlDbType.VarChar).Value = DBNull.Value;

                if (pessoaSocio.fisicaDataNascimento != null)
                    cmdPessoaSocio.Parameters.Add("fisicaDataNascimento", SqlDbType.DateTime).Value = pessoaSocio.fisicaDataNascimento;
                else
                    cmdPessoaSocio.Parameters.Add("fisicaDataNascimento", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoaSocio.Parameters.Add("fisicaSexo", SqlDbType.VarChar).Value = pessoaSocio.fisicaSexo;
                cmdPessoaSocio.Parameters.Add("fisicaEstadoCivil", SqlDbType.VarChar).Value = pessoaSocio.fisicaEstadoCivil;
                cmdPessoaSocio.Parameters.Add("fisicaProfissao", SqlDbType.VarChar).Value = pessoaSocio.fisicaProfissao;
                cmdPessoaSocio.Parameters.Add("fisicaNaturalidade", SqlDbType.VarChar).Value = pessoaSocio.fisicaNaturalidade;
                cmdPessoaSocio.Parameters.Add("fisicaRGNumero", SqlDbType.VarChar).Value = pessoaSocio.fisicaRGNumero;

                if (pessoaSocio.fisicaRGDataExpedicao != null)
                    cmdPessoaSocio.Parameters.Add("fisicaRGDataExpedicao", SqlDbType.DateTime).Value = pessoaSocio.fisicaRGDataExpedicao;
                else
                    cmdPessoaSocio.Parameters.Add("fisicaRGDataExpedicao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoaSocio.Parameters.Add("fisicaRGOrgaoExpedidor", SqlDbType.VarChar).Value = pessoaSocio.fisicaRGOrgaoExpedidor;
                cmdPessoaSocio.Parameters.Add("fisicaRGUFExpedidor", SqlDbType.VarChar).Value = pessoaSocio.fisicaRGUFExpedidor;
                cmdPessoaSocio.Parameters.Add("fisicaCNHNumero", SqlDbType.VarChar).Value = pessoaSocio.fisicaCNHNumero;
                cmdPessoaSocio.Parameters.Add("fisicaCNHCategoria", SqlDbType.VarChar).Value = pessoaSocio.fisicaCNHCategoria;

                if (pessoaSocio.fisicaCNHDataHabilitacao != null)
                    cmdPessoaSocio.Parameters.Add("fisicaCNHDataHabilitacao", SqlDbType.DateTime).Value = pessoaSocio.fisicaCNHDataHabilitacao;
                else
                    cmdPessoaSocio.Parameters.Add("fisicaCNHDataHabilitacao", SqlDbType.DateTime).Value = DBNull.Value;

                if (pessoaSocio.fisicaCNHDataEmissao != null)
                    cmdPessoaSocio.Parameters.Add("fisicaCNHDataEmissao", SqlDbType.DateTime).Value = pessoaSocio.fisicaCNHDataEmissao;
                else
                    cmdPessoaSocio.Parameters.Add("fisicaCNHDataEmissao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoaSocio.Parameters.Add("fisicaFiliacaoNomePai", SqlDbType.VarChar).Value = pessoaSocio.fisicaFiliacaoNomePai;
                cmdPessoaSocio.Parameters.Add("fisicaFiliacaoNomeMae", SqlDbType.VarChar).Value = pessoaSocio.fisicaFiliacaoNomeMae;
                cmdPessoaSocio.Parameters.Add("fisicaConjugueNomeCompleto", SqlDbType.VarChar).Value = pessoaSocio.fisicaConjugueNomeCompleto;

                if (pessoaSocio.fisicaConjugueCPF != null)
                    cmdPessoaSocio.Parameters.Add("fisicaConjugueCPF", SqlDbType.VarChar).Value = pessoaSocio.fisicaConjugueCPF.Replace(".", "").Replace("-", "");
                else
                    cmdPessoaSocio.Parameters.Add("fisicaConjugueCPF", SqlDbType.VarChar).Value = DBNull.Value;

                if (pessoaSocio.fisicaConjugueDataNascimento != null)
                    cmdPessoaSocio.Parameters.Add("fisicaConjugueDataNascimento", SqlDbType.DateTime).Value = pessoaSocio.fisicaConjugueDataNascimento;
                else
                    cmdPessoaSocio.Parameters.Add("fisicaConjugueDataNascimento", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoaSocio.Parameters.Add("fisicaNacionalidade", SqlDbType.VarChar).Value = pessoaSocio.fisicaNacionalidade;
                cmdPessoaSocio.Parameters.Add("fisicaEscolaridade", SqlDbType.VarChar).Value = pessoaSocio.fisicaEscolaridade;
                cmdPessoaSocio.Parameters.Add("fisicaPISPASEP", SqlDbType.VarChar).Value = pessoaSocio.fisicaPISPASEP;
                cmdPessoaSocio.Parameters.Add("fisicaCTPSNumero", SqlDbType.VarChar).Value = pessoaSocio.fisicaCTPSNumero;
                cmdPessoaSocio.Parameters.Add("fisicaCTPSSerie", SqlDbType.VarChar).Value = pessoaSocio.fisicaCTPSSerie;

                if (pessoaSocio.fisicaCTPSDataExpedicao != null)
                    cmdPessoaSocio.Parameters.Add("fisicaCTPSDataExpedicao", SqlDbType.DateTime).Value = pessoaSocio.fisicaCTPSDataExpedicao;
                else
                    cmdPessoaSocio.Parameters.Add("fisicaCTPSDataExpedicao", SqlDbType.DateTime).Value = DBNull.Value;

                cmdPessoaSocio.Parameters.Add("fisicaRegimeComunhaoBens", SqlDbType.VarChar).Value = pessoaSocio.fisicaRegimeComunhaoBens;

                // ENDEREÇO
                cmdPessoaSocio.Parameters.Add("enderecoLogradouro", SqlDbType.VarChar).Value = pessoaSocio.enderecoLogradouro;
                cmdPessoaSocio.Parameters.Add("enderecoNumero", SqlDbType.VarChar).Value = pessoaSocio.enderecoNumero;
                cmdPessoaSocio.Parameters.Add("enderecoComplemento", SqlDbType.VarChar).Value = pessoaSocio.enderecoComplemento;
                cmdPessoaSocio.Parameters.Add("enderecoBairro", SqlDbType.VarChar).Value = pessoaSocio.enderecoBairro;
                cmdPessoaSocio.Parameters.Add("enderecoCEP", SqlDbType.VarChar).Value = pessoaSocio.enderecoCEP;
                cmdPessoaSocio.Parameters.Add("enderecoIdCidade", SqlDbType.Int).Value = pessoaSocio.enderecoIdCidade;
                cmdPessoaSocio.Parameters.Add("enderecoIdEstado", SqlDbType.Int).Value = pessoaSocio.enderecoIdEstado;

                // CONTATO
                cmdPessoaSocio.Parameters.Add("contatoTelefoneResidencial", SqlDbType.VarChar).Value = pessoaSocio.contatoTelefoneResidencial;
                cmdPessoaSocio.Parameters.Add("contatoTelefoneComercial", SqlDbType.VarChar).Value = pessoaSocio.contatoTelefoneComercial;
                cmdPessoaSocio.Parameters.Add("contatoTelefoneCelular", SqlDbType.VarChar).Value = pessoaSocio.contatoTelefoneCelular;
                cmdPessoaSocio.Parameters.Add("contatoEmail", SqlDbType.VarChar).Value = pessoaSocio.contatoEmail;

                TrataParametrosNulos(cmdPessoaSocio.Parameters);
                TrataEspacosEmBranco(cmdPessoaSocio.Parameters);

                try
                {
                    connection.Open();
                    cmdPessoaSocio.ExecuteNonQuery();
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
        public static void Update(dtoPessoaSocio pessoaSocio)
        {
            if (!Exists(pessoaSocio))
                Insert(pessoaSocio);
            else
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    string stringSQL = @"UPDATE tbPessoaSocio
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
                                            ,enderecoLogradouro = @enderecoLogradouro
                                            ,enderecoNumero = @enderecoNumero
                                            ,enderecoComplemento = @enderecoComplemento
                                            ,enderecoBairro = @enderecoBairro
                                            ,enderecoCEP = @enderecoCEP
                                            ,enderecoIdCidade = @enderecoIdCidade
                                            ,enderecoIdEstado = @enderecoIdEstado
                                            ,contatoTelefoneResidencial = @contatoTelefoneResidencial
                                            ,contatoTelefoneComercial = @contatoTelefoneComercial
                                            ,contatoTelefoneCelular = @contatoTelefoneCelular
                                            ,contatoEmail = @contatoEmail
                                        WHERE idPessoaSocio = @idPessoaSocio";

                    SqlCommand cmdPessoaSocio = new SqlCommand(stringSQL, connection);

                    cmdPessoaSocio.Parameters.Add("idPessoa", SqlDbType.Int).Value = pessoaSocio.idPessoa;
                    cmdPessoaSocio.Parameters.Add("idPessoaSocio", SqlDbType.Int).Value = pessoaSocio.idPessoaSocio;

                    // PESSOA FÍSICA
                    cmdPessoaSocio.Parameters.Add("fisicaNomeCompleto", SqlDbType.VarChar).Value = pessoaSocio.fisicaNomeCompleto;

                    if (pessoaSocio.fisicaCPF != null)
                        cmdPessoaSocio.Parameters.Add("fisicaCPF", SqlDbType.VarChar).Value = pessoaSocio.fisicaCPF.Replace(".", "").Replace("-", "");
                    else
                        cmdPessoaSocio.Parameters.Add("fisicaCPF", SqlDbType.VarChar).Value = DBNull.Value;

                    if (pessoaSocio.fisicaDataNascimento != null)
                        cmdPessoaSocio.Parameters.Add("fisicaDataNascimento", SqlDbType.DateTime).Value = pessoaSocio.fisicaDataNascimento;
                    else
                        cmdPessoaSocio.Parameters.Add("fisicaDataNascimento", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoaSocio.Parameters.Add("fisicaSexo", SqlDbType.VarChar).Value = pessoaSocio.fisicaSexo;
                    cmdPessoaSocio.Parameters.Add("fisicaEstadoCivil", SqlDbType.VarChar).Value = pessoaSocio.fisicaEstadoCivil;
                    cmdPessoaSocio.Parameters.Add("fisicaProfissao", SqlDbType.VarChar).Value = pessoaSocio.fisicaProfissao;
                    cmdPessoaSocio.Parameters.Add("fisicaNaturalidade", SqlDbType.VarChar).Value = pessoaSocio.fisicaNaturalidade;
                    cmdPessoaSocio.Parameters.Add("fisicaRGNumero", SqlDbType.VarChar).Value = pessoaSocio.fisicaRGNumero;

                    if (pessoaSocio.fisicaRGDataExpedicao != null)
                        cmdPessoaSocio.Parameters.Add("fisicaRGDataExpedicao", SqlDbType.DateTime).Value = pessoaSocio.fisicaRGDataExpedicao;
                    else
                        cmdPessoaSocio.Parameters.Add("fisicaRGDataExpedicao", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoaSocio.Parameters.Add("fisicaRGOrgaoExpedidor", SqlDbType.VarChar).Value = pessoaSocio.fisicaRGOrgaoExpedidor;
                    cmdPessoaSocio.Parameters.Add("fisicaRGUFExpedidor", SqlDbType.VarChar).Value = pessoaSocio.fisicaRGUFExpedidor;
                    cmdPessoaSocio.Parameters.Add("fisicaCNHNumero", SqlDbType.VarChar).Value = pessoaSocio.fisicaCNHNumero;
                    cmdPessoaSocio.Parameters.Add("fisicaCNHCategoria", SqlDbType.VarChar).Value = pessoaSocio.fisicaCNHCategoria;

                    if (pessoaSocio.fisicaCNHDataHabilitacao != null)
                        cmdPessoaSocio.Parameters.Add("fisicaCNHDataHabilitacao", SqlDbType.DateTime).Value = pessoaSocio.fisicaCNHDataHabilitacao;
                    else
                        cmdPessoaSocio.Parameters.Add("fisicaCNHDataHabilitacao", SqlDbType.DateTime).Value = DBNull.Value;

                    if (pessoaSocio.fisicaCNHDataEmissao != null)
                        cmdPessoaSocio.Parameters.Add("fisicaCNHDataEmissao", SqlDbType.DateTime).Value = pessoaSocio.fisicaCNHDataEmissao;
                    else
                        cmdPessoaSocio.Parameters.Add("fisicaCNHDataEmissao", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoaSocio.Parameters.Add("fisicaFiliacaoNomePai", SqlDbType.VarChar).Value = pessoaSocio.fisicaFiliacaoNomePai;
                    cmdPessoaSocio.Parameters.Add("fisicaFiliacaoNomeMae", SqlDbType.VarChar).Value = pessoaSocio.fisicaFiliacaoNomeMae;
                    cmdPessoaSocio.Parameters.Add("fisicaConjugueNomeCompleto", SqlDbType.VarChar).Value = pessoaSocio.fisicaConjugueNomeCompleto;

                    if (pessoaSocio.fisicaConjugueCPF != null)
                        cmdPessoaSocio.Parameters.Add("fisicaConjugueCPF", SqlDbType.VarChar).Value = pessoaSocio.fisicaConjugueCPF.Replace(".", "").Replace("-", "");
                    else
                        cmdPessoaSocio.Parameters.Add("fisicaConjugueCPF", SqlDbType.VarChar).Value = DBNull.Value;

                    if (pessoaSocio.fisicaConjugueDataNascimento != null)
                        cmdPessoaSocio.Parameters.Add("fisicaConjugueDataNascimento", SqlDbType.DateTime).Value = pessoaSocio.fisicaConjugueDataNascimento;
                    else
                        cmdPessoaSocio.Parameters.Add("fisicaConjugueDataNascimento", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoaSocio.Parameters.Add("fisicaNacionalidade", SqlDbType.VarChar).Value = pessoaSocio.fisicaNacionalidade;
                    cmdPessoaSocio.Parameters.Add("fisicaEscolaridade", SqlDbType.VarChar).Value = pessoaSocio.fisicaEscolaridade;
                    cmdPessoaSocio.Parameters.Add("fisicaPISPASEP", SqlDbType.VarChar).Value = pessoaSocio.fisicaPISPASEP;
                    cmdPessoaSocio.Parameters.Add("fisicaCTPSNumero", SqlDbType.VarChar).Value = pessoaSocio.fisicaCTPSNumero;
                    cmdPessoaSocio.Parameters.Add("fisicaCTPSSerie", SqlDbType.VarChar).Value = pessoaSocio.fisicaCTPSSerie;

                    if (pessoaSocio.fisicaCTPSDataExpedicao != null)
                        cmdPessoaSocio.Parameters.Add("fisicaCTPSDataExpedicao", SqlDbType.DateTime).Value = pessoaSocio.fisicaCTPSDataExpedicao;
                    else
                        cmdPessoaSocio.Parameters.Add("fisicaCTPSDataExpedicao", SqlDbType.DateTime).Value = DBNull.Value;

                    cmdPessoaSocio.Parameters.Add("fisicaRegimeComunhaoBens", SqlDbType.VarChar).Value = pessoaSocio.fisicaRegimeComunhaoBens;

                    // ENDEREÇO
                    cmdPessoaSocio.Parameters.Add("enderecoLogradouro", SqlDbType.VarChar).Value = pessoaSocio.enderecoLogradouro;
                    cmdPessoaSocio.Parameters.Add("enderecoNumero", SqlDbType.VarChar).Value = pessoaSocio.enderecoNumero;
                    cmdPessoaSocio.Parameters.Add("enderecoComplemento", SqlDbType.VarChar).Value = pessoaSocio.enderecoComplemento;
                    cmdPessoaSocio.Parameters.Add("enderecoBairro", SqlDbType.VarChar).Value = pessoaSocio.enderecoBairro;
                    cmdPessoaSocio.Parameters.Add("enderecoCEP", SqlDbType.VarChar).Value = pessoaSocio.enderecoCEP;
                    cmdPessoaSocio.Parameters.Add("enderecoIdCidade", SqlDbType.Int).Value = pessoaSocio.enderecoIdCidade;
                    cmdPessoaSocio.Parameters.Add("enderecoIdEstado", SqlDbType.Int).Value = pessoaSocio.enderecoIdEstado;

                    // CONTATO
                    cmdPessoaSocio.Parameters.Add("contatoTelefoneResidencial", SqlDbType.VarChar).Value = pessoaSocio.contatoTelefoneResidencial;
                    cmdPessoaSocio.Parameters.Add("contatoTelefoneComercial", SqlDbType.VarChar).Value = pessoaSocio.contatoTelefoneComercial;
                    cmdPessoaSocio.Parameters.Add("contatoTelefoneCelular", SqlDbType.VarChar).Value = pessoaSocio.contatoTelefoneCelular;
                    cmdPessoaSocio.Parameters.Add("contatoEmail", SqlDbType.VarChar).Value = pessoaSocio.contatoEmail;

                    TrataParametrosNulos(cmdPessoaSocio.Parameters);
                    TrataEspacosEmBranco(cmdPessoaSocio.Parameters);

                    try
                    {
                        connection.Open();
                        cmdPessoaSocio.ExecuteNonQuery();
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


        public static bool Exists(dtoPessoaSocio pessoaSocio)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaSocio
                                    WHERE idPessoaSocio = @idPessoaSocio";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idPessoaSocio", SqlDbType.Int).Value = pessoaSocio.idPessoaSocio;

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

        public static bool Exists(string CPF)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbPessoaSocio
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


        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void Delete(dtoPessoaSocio pessoaSocio)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbPessoaSocio 
                                      WHERE idPessoaSocio = @idPessoaSocio";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idPessoaSocio", SqlDbType.Int).Value = pessoaSocio.idPessoaSocio;

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

        public static void Delete(int idPessoaSocio)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbPessoaSocio 
                                      WHERE idPessoaSocio = @idPessoaSocio";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idPessoaSocio", SqlDbType.Int).Value = idPessoaSocio;

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
        public static dtoPessoaSocio Get(int idPessoaSocio)
        {
            dtoPessoaSocio pessoaSocio = new dtoPessoaSocio();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbPessoaSocio
                                    WHERE tbPessoaSocio.idPessoaSocio = @idPessoaSocio";

                SqlCommand cmdPessoa = new SqlCommand(stringSQL, connection);

                cmdPessoa.Parameters.Add("idPessoaSocio", SqlDbType.Int).Value = idPessoaSocio;

                try
                {
                    connection.Open();
                    SqlDataReader drPessoaSocio = cmdPessoa.ExecuteReader();

                    if (drPessoaSocio.Read())
                        PreencheCampos(drPessoaSocio, ref pessoaSocio);
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

            return pessoaSocio;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoPessoaSocio> GetAll()
        {
            return GetAll(0, "");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoPessoaSocio> GetAll(int idPessoa, string SortExpression)
        {
            List<dtoPessoaSocio> pessoasSocios = new List<dtoPessoaSocio>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                //if (idPessoa > 0)
                sbCondicao.AppendFormat(@" WHERE (tbPessoaSocio.idPessoa = {0})", idPessoa.ToString());

                string stringSQL = String.Format(@"SELECT *
                                                    FROM tbPessoaSocio
                                                {0}
                                                ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "tbPessoaSocio.idPessoaSocio"));

                SqlCommand cmdPessoaSocio = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drPessoaSocio = cmdPessoaSocio.ExecuteReader();

                    while (drPessoaSocio.Read())
                    {
                        dtoPessoaSocio pessoaSocio = new dtoPessoaSocio();

                        PreencheCampos(drPessoaSocio, ref pessoaSocio);

                        pessoasSocios.Add(pessoaSocio);
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

            return pessoasSocios;
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

        private static void PreencheCampos(SqlDataReader drPessoaSocio, ref dtoPessoaSocio pessoaSocio)
        {

            if (drPessoaSocio["idPessoa"] != null
                && drPessoaSocio["idPessoa"] != DBNull.Value)
                pessoaSocio.idPessoa = Convert.ToInt32(drPessoaSocio["idPessoa"].ToString());

            if (drPessoaSocio["idPessoaSocio"] != null
                && drPessoaSocio["idPessoaSocio"] != DBNull.Value)
                pessoaSocio.idPessoaSocio = Convert.ToInt32(drPessoaSocio["idPessoaSocio"].ToString());

            // PESSOA CONTATO
            if (drPessoaSocio["contatoTelefoneResidencial"] != DBNull.Value)
                pessoaSocio.contatoTelefoneResidencial = drPessoaSocio["contatoTelefoneResidencial"].ToString();

            if (drPessoaSocio["contatoTelefoneComercial"] != DBNull.Value)
                pessoaSocio.contatoTelefoneComercial = drPessoaSocio["contatoTelefoneComercial"].ToString();

            if (drPessoaSocio["contatoTelefoneCelular"] != DBNull.Value)
                pessoaSocio.contatoTelefoneCelular = drPessoaSocio["contatoTelefoneCelular"].ToString();

            if (drPessoaSocio["contatoEmail"] != DBNull.Value)
                pessoaSocio.contatoEmail = drPessoaSocio["contatoEmail"].ToString();

            // PESSOA ENDEREÇO
            if (drPessoaSocio["enderecoLogradouro"] != DBNull.Value)
                pessoaSocio.enderecoLogradouro = drPessoaSocio["enderecoLogradouro"].ToString();

            if (drPessoaSocio["enderecoNumero"] != DBNull.Value)
                pessoaSocio.enderecoNumero = drPessoaSocio["enderecoNumero"].ToString();

            if (drPessoaSocio["enderecoComplemento"] != DBNull.Value)
                pessoaSocio.enderecoComplemento = drPessoaSocio["enderecoComplemento"].ToString();

            if (drPessoaSocio["enderecoBairro"] != DBNull.Value)
                pessoaSocio.enderecoBairro = drPessoaSocio["enderecoBairro"].ToString();

            if (drPessoaSocio["enderecoCEP"] != DBNull.Value)
                pessoaSocio.enderecoCEP = drPessoaSocio["enderecoCEP"].ToString();

            if (drPessoaSocio["enderecoIdCidade"] != DBNull.Value)
                pessoaSocio.enderecoIdCidade = Convert.ToInt32(drPessoaSocio["enderecoIdCidade"].ToString());

            if (drPessoaSocio["enderecoIdEstado"] != DBNull.Value)
                pessoaSocio.enderecoIdEstado = Convert.ToInt32(drPessoaSocio["enderecoIdEstado"].ToString());


            // PESSOA FÍSICA
            if (drPessoaSocio["fisicaNomeCompleto"] != DBNull.Value)
                pessoaSocio.fisicaNomeCompleto = drPessoaSocio["fisicaNomeCompleto"].ToString();

            if (drPessoaSocio["fisicaCPF"] != DBNull.Value)
                pessoaSocio.fisicaCPF = drPessoaSocio["fisicaCPF"].ToString();

            if (drPessoaSocio["fisicaDataNascimento"] != DBNull.Value)
                pessoaSocio.fisicaDataNascimento = Convert.ToDateTime(drPessoaSocio["fisicaDataNascimento"].ToString());
            else
                pessoaSocio.fisicaDataNascimento = null;

            if (drPessoaSocio["fisicaSexo"] != DBNull.Value)
                pessoaSocio.fisicaSexo = drPessoaSocio["fisicaSexo"].ToString();

            if (drPessoaSocio["fisicaEstadoCivil"] != DBNull.Value)
                pessoaSocio.fisicaEstadoCivil = drPessoaSocio["fisicaEstadoCivil"].ToString();

            if (drPessoaSocio["fisicaProfissao"] != DBNull.Value)
                pessoaSocio.fisicaProfissao = drPessoaSocio["fisicaProfissao"].ToString();

            if (drPessoaSocio["fisicaNaturalidade"] != DBNull.Value)
                pessoaSocio.fisicaNaturalidade = drPessoaSocio["fisicaNaturalidade"].ToString();

            if (drPessoaSocio["fisicaRGNumero"] != DBNull.Value)
                pessoaSocio.fisicaRGNumero = drPessoaSocio["fisicaRGNumero"].ToString();

            if (drPessoaSocio["fisicaRGDataExpedicao"] != DBNull.Value)
                pessoaSocio.fisicaRGDataExpedicao = Convert.ToDateTime(drPessoaSocio["fisicaRGDataExpedicao"].ToString());
            else
                pessoaSocio.fisicaRGDataExpedicao = null;

            if (drPessoaSocio["fisicaRGOrgaoExpedidor"] != DBNull.Value)
                pessoaSocio.fisicaRGOrgaoExpedidor = drPessoaSocio["fisicaRGOrgaoExpedidor"].ToString();

            if (drPessoaSocio["fisicaRGUFExpedidor"] != DBNull.Value)
                pessoaSocio.fisicaRGUFExpedidor = drPessoaSocio["fisicaRGUFExpedidor"].ToString();

            if (drPessoaSocio["fisicaCNHNumero"] != DBNull.Value)
                pessoaSocio.fisicaCNHNumero = drPessoaSocio["fisicaCNHNumero"].ToString();

            if (drPessoaSocio["fisicaCNHCategoria"] != DBNull.Value)
                pessoaSocio.fisicaCNHCategoria = drPessoaSocio["fisicaCNHCategoria"].ToString();

            if (drPessoaSocio["fisicaCNHDataHabilitacao"] != DBNull.Value)
                pessoaSocio.fisicaCNHDataHabilitacao = Convert.ToDateTime(drPessoaSocio["fisicaCNHDataHabilitacao"].ToString());
            else
                pessoaSocio.fisicaCNHDataHabilitacao = null;

            if (drPessoaSocio["fisicaCNHDataEmissao"] != DBNull.Value)
                pessoaSocio.fisicaCNHDataEmissao = Convert.ToDateTime(drPessoaSocio["fisicaCNHDataEmissao"].ToString());
            else
                pessoaSocio.fisicaCNHDataEmissao = null;

            if (drPessoaSocio["fisicaFiliacaoNomePai"] != DBNull.Value)
                pessoaSocio.fisicaFiliacaoNomePai = drPessoaSocio["fisicaFiliacaoNomePai"].ToString();

            if (drPessoaSocio["fisicaFiliacaoNomeMae"] != DBNull.Value)
                pessoaSocio.fisicaFiliacaoNomeMae = drPessoaSocio["fisicaFiliacaoNomeMae"].ToString();

            if (drPessoaSocio["fisicaConjugueNomeCompleto"] != DBNull.Value)
                pessoaSocio.fisicaConjugueNomeCompleto = drPessoaSocio["fisicaConjugueNomeCompleto"].ToString();

            if (drPessoaSocio["fisicaConjugueCPF"] != DBNull.Value)
                pessoaSocio.fisicaConjugueCPF = drPessoaSocio["fisicaConjugueCPF"].ToString();

            if (drPessoaSocio["fisicaConjugueDataNascimento"] != DBNull.Value)
                pessoaSocio.fisicaConjugueDataNascimento = Convert.ToDateTime(drPessoaSocio["fisicaConjugueDataNascimento"].ToString());
            else
                pessoaSocio.fisicaConjugueDataNascimento = null;

            if (drPessoaSocio["fisicaNacionalidade"] != DBNull.Value)
                pessoaSocio.fisicaNacionalidade = drPessoaSocio["fisicaNacionalidade"].ToString();

            if (drPessoaSocio["fisicaEscolaridade"] != DBNull.Value)
                pessoaSocio.fisicaEscolaridade = drPessoaSocio["fisicaEscolaridade"].ToString();

            if (drPessoaSocio["fisicaPISPASEP"] != DBNull.Value)
                pessoaSocio.fisicaPISPASEP = drPessoaSocio["fisicaPISPASEP"].ToString();

            if (drPessoaSocio["fisicaCTPSNumero"] != DBNull.Value)
                pessoaSocio.fisicaCTPSNumero = drPessoaSocio["fisicaCTPSNumero"].ToString();

            if (drPessoaSocio["fisicaCTPSSerie"] != DBNull.Value)
                pessoaSocio.fisicaCTPSSerie = drPessoaSocio["fisicaCTPSSerie"].ToString();

            if (drPessoaSocio["fisicaCTPSDataExpedicao"] != DBNull.Value)
                pessoaSocio.fisicaCTPSDataExpedicao = Convert.ToDateTime(drPessoaSocio["fisicaCTPSDataExpedicao"].ToString());
            else
                pessoaSocio.fisicaCTPSDataExpedicao = null;

            if (drPessoaSocio["fisicaRegimeComunhaoBens"] != DBNull.Value)
                pessoaSocio.fisicaRegimeComunhaoBens = drPessoaSocio["fisicaRegimeComunhaoBens"].ToString();

        }

    }
}
