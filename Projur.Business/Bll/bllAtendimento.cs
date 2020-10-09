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
    public class bllAtendimento
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoAtendimento Atendimento)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbAtendimento(dataInicioAtendimento, dataFimAtendimento, tipoAtendimento, Detalhamento, idUsuario, idPessoaCliente, dataCadastro)
                                            VALUES(@dataInicioAtendimento, @dataFimAtendimento, @tipoAtendimento, @Detalhamento, @idUsuario, @idPessoaCliente, getdate());
                                            SET @idAtendimento = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Atendimento);

                cmdInserir.Parameters.Add("idAtendimento", SqlDbType.Int);
                cmdInserir.Parameters["idAtendimento"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("dataInicioAtendimento", SqlDbType.DateTime).Value = Atendimento.dataInicioAtendimento;
                cmdInserir.Parameters.Add("dataFimAtendimento", SqlDbType.DateTime).Value = Atendimento.dataFimAtendimento;
                cmdInserir.Parameters.Add("tipoAtendimento", SqlDbType.VarChar).Value = Atendimento.tipoAtendimento;
                cmdInserir.Parameters.Add("Detalhamento", SqlDbType.VarChar).Value = Atendimento.Detalhamento;
                cmdInserir.Parameters.Add("idUsuario", SqlDbType.Int).Value = Atendimento.idUsuario;
                cmdInserir.Parameters.Add("idPessoaCliente", SqlDbType.Int).Value = Atendimento.idPessoaCliente;

                try
                {
                    connection.Open();
                    cmdInserir.ExecuteNonQuery();
                    return (int)cmdInserir.Parameters["idAtendimento"].Value;
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

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void Delete(dtoAtendimento Atendimento)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbAtendimento 
                                      WHERE idAtendimento = @idAtendimento";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, connection);
                cmdDeletar.Parameters.Add("idAtendimento", SqlDbType.Int).Value = Atendimento.idAtendimento;

                try
                {
                    connection.Open();
                    cmdDeletar.ExecuteNonQuery();
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

        public static void Delete(int idAtendimento)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbAtendimento 
                                      WHERE idAtendimento = @idAtendimento";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, connection);
                cmdDeletar.Parameters.Add("idAtendimento", SqlDbType.Int).Value = idAtendimento;

                try
                {
                    connection.Open();
                    cmdDeletar.ExecuteNonQuery();
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
        public static void Update(dtoAtendimento Atendimento)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbAtendimento SET 
                                        dataInicioAtendimento = @dataInicioAtendimento,
                                        dataFimAtendimento = @dataFimAtendimento,
                                        tipoAtendimento = @tipoAtendimento,
                                        Detalhamento = @Detalhamento,
                                        idUsuario = @idUsuario,
                                        idPessoaCliente = @idPessoaCliente,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idAtendimento = @idAtendimento";

                SqlCommand cmdAtualizar = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Atendimento);

                cmdAtualizar.Parameters.Add("idAtendimento", SqlDbType.Int).Value = Atendimento.idAtendimento;

                cmdAtualizar.Parameters.Add("dataInicioAtendimento", SqlDbType.DateTime).Value = Atendimento.dataInicioAtendimento;
                cmdAtualizar.Parameters.Add("dataFimAtendimento", SqlDbType.DateTime).Value = Atendimento.dataFimAtendimento;
                cmdAtualizar.Parameters.Add("tipoAtendimento", SqlDbType.VarChar).Value = Atendimento.tipoAtendimento;
                cmdAtualizar.Parameters.Add("Detalhamento", SqlDbType.VarChar).Value = Atendimento.Detalhamento;
                cmdAtualizar.Parameters.Add("idUsuario", SqlDbType.Int).Value = Atendimento.idUsuario;
                cmdAtualizar.Parameters.Add("idPessoaCliente", SqlDbType.Int).Value = Atendimento.idPessoaCliente;

                try
                {
                    connection.Open();
                    cmdAtualizar.ExecuteNonQuery();
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
        public static dtoAtendimento Get(int idAtendimento)
        {
            dtoAtendimento atendimento = new dtoAtendimento();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {

                string stringSQL = @"SELECT *
                                    FROM tbAtendimento
                                    WHERE idAtendimento = @idAtendimento";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, connection);

                cmdSelecionar.Parameters.Add("idAtendimento", SqlDbType.Int).Value = idAtendimento;

                try
                {
                    connection.Open();
                    SqlDataReader drSelecao = cmdSelecionar.ExecuteReader();

                    if (drSelecao.Read())
                        PreencheCampos(drSelecao, ref atendimento);
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

            return atendimento;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAtendimento> GetAll(string SortExpression, string termoPesquisa)
        {
            return GetAll("0", "", "", "", SortExpression, termoPesquisa);
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAtendimento> GetAll(string SortExpression)
        {
            List<dtoAtendimento> atendimentos = new List<dtoAtendimento>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbQuery = new StringBuilder();
                StringBuilder sbCondicao = new StringBuilder();

                sbQuery.Append(@"SELECT * FROM tbAtendimento        
                                    LEFT JOIN tbPessoa
                                        ON tbPessoa.idPessoa = tbAtendimento.idPessoaCliente                         
                                    LEFT JOIN tbPessoaFisica
                                        ON tbPessoa.idPessoa = tbPessoaFisica.idPessoa
                                    LEFT JOIN tbPessoaJuridica
                                        ON tbPessoa.idPessoa = tbPessoaJuridica.idPessoa");

                sbQuery.AppendFormat(" ORDER BY {0}", (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idAtendimento"));

                SqlCommand cmdConvenio = new SqlCommand(sbQuery.ToString(), connection);

                try
                {
                    connection.Open();
                    SqlDataReader drSelecao = cmdConvenio.ExecuteReader();

                    while (drSelecao.Read())
                    {
                        dtoAtendimento atendimento = new dtoAtendimento();

                        PreencheCampos(drSelecao, ref atendimento);

                        atendimentos.Add(atendimento);
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

            return atendimentos;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAtendimento> GetHistorico(string SortExpression, string idCliente, string idIgnorado)
        {
            List<dtoAtendimento> atendimentos = new List<dtoAtendimento>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();
                StringBuilder sbQuery = new StringBuilder();
                string sOrderBy = String.Empty;

                // CLILENTE
                if (idCliente != null
                    && idCliente.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("tbAtendimento.idPessoaCliente  = {0}", idCliente);
                }

                sOrderBy = (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idAtendimento");

                sbQuery.AppendFormat("SELECT * FROM tbAtendimento {0} ORDER BY {1}", sbCondicao.ToString(), sOrderBy);

                SqlCommand cmdConvenio = new SqlCommand(sbQuery.ToString(), connection);

                try
                {
                    connection.Open();
                    SqlDataReader drSelecao = cmdConvenio.ExecuteReader();

                    while (drSelecao.Read())
                    {
                        dtoAtendimento atendimento = new dtoAtendimento();

                        PreencheCampos(drSelecao, ref atendimento);

                        atendimentos.Add(atendimento);
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

            return atendimentos;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAtendimento> GetAll()
        {
            return GetAll("");
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAtendimento> GetAll(string idUsuario, string tipoAtendimento, string dataInicioAtendimento, string dataFimAtendimento, string SortExpression, string termoPesquisa)
        {
            List<dtoAtendimento> atendimentos = new List<dtoAtendimento>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbQuery = new StringBuilder();
                StringBuilder sbCondicao = new StringBuilder();

                // USUÁRIO
                if (idUsuario != null
                    && idUsuario != String.Empty
                    && idUsuario != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" idUsuario = {0} ", idUsuario);
                }

                // TIPO DE ATENDIMENTO
                if (tipoAtendimento != null
                    && tipoAtendimento != String.Empty
                    && tipoAtendimento != "0")
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" tipoAtendimento = '{0}' ", tipoAtendimento);
                }

                // PERIODO INICIAL
                if (dataInicioAtendimento != null
                    && dataInicioAtendimento.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataInicioAtendimento >= '{0} 00:00:00' ", dataInicioAtendimento);
                }

                // PERIODO FINAL
                if (dataFimAtendimento != null
                    && dataFimAtendimento.Trim() != String.Empty)
                {
                    if (sbCondicao.ToString().Trim() == String.Empty)
                        sbCondicao.Append(" WHERE ");
                    else
                        sbCondicao.Append(" AND ");

                    sbCondicao.AppendFormat("dataFimAtendimento <= '{0} 23:59:59'", dataFimAtendimento);
                }

                // CONDIÇÕES
                if (termoPesquisa != null
                    && termoPesquisa != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" ((tbPessoaFisica.fisicaCPF LIKE '%{0}%' OR tbPessoaJuridica.juridicaCNPJ LIKE '%{0}%') 
                                            OR (tbPessoaFisica.fisicaNomeCompleto LIKE '%{0}%' OR tbPessoaJuridica.juridicaRazaoSocial LIKE '%{0}%'))", termoPesquisa);
                }

                sbQuery.Append(@"SELECT * FROM tbAtendimento        
                                                LEFT JOIN tbPessoa
                                                    ON tbPessoa.idPessoa = tbAtendimento.idPessoaCliente                    
                                                LEFT JOIN tbPessoaFisica
                                                    ON tbPessoa.idPessoa = tbPessoaFisica.idPessoa
                                                LEFT JOIN tbPessoaJuridica
                                                    ON tbPessoa.idPessoa = tbPessoaJuridica.idPessoa
                                                LEFT JOIN tbPessoaEndereco
                                                    ON tbPessoa.idPessoa = tbPessoaEndereco.idPessoa");

                if (sbCondicao.ToString() != String.Empty)
                    sbQuery.Append(sbCondicao.ToString());

                sbQuery.AppendFormat(" ORDER BY {0}", (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idAtendimento"));

                SqlCommand cmdConvenio = new SqlCommand(sbQuery.ToString(), connection);

                try
                {
                    connection.Open();
                    SqlDataReader drSelecao = cmdConvenio.ExecuteReader();

                    while (drSelecao.Read())
                    {
                        dtoAtendimento atendimento = new dtoAtendimento();

                        PreencheCampos(drSelecao, ref atendimento);

                        atendimentos.Add(atendimento);
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

            return atendimentos;
        }


        private static void PreencheCampos(SqlDataReader drSelecao, ref dtoAtendimento Atendimento)
        {

            if (drSelecao["idAtendimento"] != DBNull.Value)
                Atendimento.idAtendimento = Convert.ToInt32(drSelecao["idAtendimento"].ToString());

            if (drSelecao["dataCadastro"] != DBNull.Value)
                Atendimento.dataCadastro = Convert.ToDateTime(drSelecao["dataCadastro"]);
            else
                Atendimento.dataCadastro = null;

            if (drSelecao["dataUltimaAlteracao"] != DBNull.Value)
                Atendimento.dataUltimaAlteracao = Convert.ToDateTime(drSelecao["dataUltimaAlteracao"]);
            else
                Atendimento.dataUltimaAlteracao = null;

            if (drSelecao["dataInicioAtendimento"] != DBNull.Value)
                Atendimento.dataInicioAtendimento = Convert.ToDateTime(drSelecao["dataInicioAtendimento"]);
            else
                Atendimento.dataInicioAtendimento = null;

            if (drSelecao["dataFimAtendimento"] != DBNull.Value)
                Atendimento.dataFimAtendimento = Convert.ToDateTime(drSelecao["dataFimAtendimento"]);
            else
                Atendimento.dataFimAtendimento = null;

            if (drSelecao["tipoAtendimento"] != DBNull.Value)
                Atendimento.tipoAtendimento = drSelecao["tipoAtendimento"].ToString();

            if (drSelecao["Detalhamento"] != DBNull.Value)
                Atendimento.Detalhamento = drSelecao["Detalhamento"].ToString();

            if (drSelecao["idUsuario"] != DBNull.Value)
                Atendimento.idUsuario = Convert.ToInt32(drSelecao["idUsuario"].ToString());

            if (drSelecao["idPessoaCliente"] != DBNull.Value)
                Atendimento.idPessoaCliente = Convert.ToInt32(drSelecao["idPessoaCliente"]);

        }

        private static void ValidaCampos(ref dtoAtendimento Atendimento)
        {
            if (String.IsNullOrEmpty(Atendimento.Detalhamento)) { Atendimento.Detalhamento = String.Empty; }
            if (String.IsNullOrEmpty(Atendimento.tipoAtendimento)) { Atendimento.tipoAtendimento = String.Empty; }
        }

        public static string RetornaDescricaoTipoAtendimento(object tipoAtendimento)
        {
            string retorno = String.Empty;

            if (tipoAtendimento != null)
            {
                switch ((string)tipoAtendimento)
                {
                    case "LP":
                        retorno = "Local / Presencial";
                        break;

                    case "T":
                        retorno = "Telefônico";
                        break;

                    case "E":
                        retorno = "Email";
                        break;

                    case "FO":
                        retorno = "Follow-up / Ocorrências";
                        break;
                }
            }

            return retorno;

        }

    }
}
