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
    public class bllTipoPrazoProcessual
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoTipoPrazoProcessual TipoPrazoProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbTipoPrazoProcessual(Descricao, quantidadeDiasPrazo, dataCadastro)
                                            VALUES(@Descricao, @quantidadeDiasPrazo, getdate());
                                            SET @idTipoPrazoProcessual = SCOPE_IDENTITY()";

                SqlCommand cmdTipoPrazoProcessual = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref TipoPrazoProcessual);

                cmdTipoPrazoProcessual.Parameters.Add("idTipoPrazoProcessual", SqlDbType.Int);
                cmdTipoPrazoProcessual.Parameters["idTipoPrazoProcessual"].Direction = ParameterDirection.Output;

                cmdTipoPrazoProcessual.Parameters.Add("Descricao", SqlDbType.VarChar).Value = TipoPrazoProcessual.Descricao;
                cmdTipoPrazoProcessual.Parameters.Add("quantidadeDiasPrazo", SqlDbType.Int).Value = TipoPrazoProcessual.quantidadeDiasPrazo;

                try
                {
                    connection.Open();
                    cmdTipoPrazoProcessual.ExecuteNonQuery();

                    return (int)cmdTipoPrazoProcessual.Parameters["idTipoPrazoProcessual"].Value;
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
        public static void Update(dtoTipoPrazoProcessual TipoPrazoProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbTipoPrazoProcessual SET 
                                        Descricao = @Descricao,
                                        quantidadeDiasPrazo = @quantidadeDiasPrazo,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idTipoPrazoProcessual = @idTipoPrazoProcessual";

                SqlCommand cmdTipoPrazoProcessual = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref TipoPrazoProcessual);

                cmdTipoPrazoProcessual.Parameters.Add("idTipoPrazoProcessual", SqlDbType.Int).Value = TipoPrazoProcessual.idTipoPrazoProcessual;
                cmdTipoPrazoProcessual.Parameters.Add("Descricao", SqlDbType.VarChar).Value = TipoPrazoProcessual.Descricao;
                cmdTipoPrazoProcessual.Parameters.Add("quantidadeDiasPrazo", SqlDbType.Int).Value = TipoPrazoProcessual.quantidadeDiasPrazo;

                try
                {
                    connection.Open();
                    cmdTipoPrazoProcessual.ExecuteNonQuery();
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
        public static void Delete(dtoTipoPrazoProcessual TipoPrazoProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbTipoPrazoProcessual 
                                      WHERE idTipoPrazoProcessual = @idTipoPrazoProcessual";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idTipoPrazoProcessual", SqlDbType.Int).Value = TipoPrazoProcessual.idTipoPrazoProcessual;

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

        public static void Delete(int idTipoPrazoProcessual)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbTipoPrazoProcessual 
                                      WHERE idTipoPrazoProcessual = @idTipoPrazoProcessual";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idTipoPrazoProcessual", SqlDbType.Int).Value = idTipoPrazoProcessual;

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
        public static dtoTipoPrazoProcessual Get(int idTipoPrazoProcessual)
        {
            dtoTipoPrazoProcessual TipoPrazoProcessual = new dtoTipoPrazoProcessual();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbTipoPrazoProcessual
                                    WHERE idTipoPrazoProcessual = @idTipoPrazoProcessual";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idTipoPrazoProcessual", SqlDbType.Int).Value = idTipoPrazoProcessual;

                try
                {
                    connection.Open();
                    SqlDataReader drTipoPrazoProcessual = cmdMenu.ExecuteReader();

                    if (drTipoPrazoProcessual.Read())
                        PreencheCampos(drTipoPrazoProcessual, ref TipoPrazoProcessual);
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

            return TipoPrazoProcessual;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoTipoPrazoProcessual> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoTipoPrazoProcessual> TipoPrazoProcessuals = new List<dtoTipoPrazoProcessual>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                // CONDIÇÕES
                if (termoPesquisa != null
                    && termoPesquisa != String.Empty)
                {
                    if (sbCondicao.ToString() != String.Empty)
                        sbCondicao.Append(" AND ");
                    else
                        sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" (tbTipoPrazoProcessual.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbTipoPrazoProcessual {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idTipoPrazoProcessual"));

                SqlCommand cmdTipoPrazoProcessual = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drTipoPrazoProcessual = cmdTipoPrazoProcessual.ExecuteReader();

                    while (drTipoPrazoProcessual.Read())
                    {
                        dtoTipoPrazoProcessual TipoPrazoProcessual = new dtoTipoPrazoProcessual();

                        PreencheCampos(drTipoPrazoProcessual, ref TipoPrazoProcessual);

                        TipoPrazoProcessuals.Add(TipoPrazoProcessual);
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

            return TipoPrazoProcessuals;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoTipoPrazoProcessual> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoTipoPrazoProcessual> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }



        private static void PreencheCampos(SqlDataReader drTipoPrazoProcessual, ref dtoTipoPrazoProcessual TipoPrazoProcessual)
        {

            if (drTipoPrazoProcessual["idTipoPrazoProcessual"] != DBNull.Value)
                TipoPrazoProcessual.idTipoPrazoProcessual = Convert.ToInt32(drTipoPrazoProcessual["idTipoPrazoProcessual"].ToString());

            if (drTipoPrazoProcessual["dataCadastro"] != DBNull.Value)
                TipoPrazoProcessual.dataCadastro = Convert.ToDateTime(drTipoPrazoProcessual["dataCadastro"]);
            else
                TipoPrazoProcessual.dataCadastro = null;

            if (drTipoPrazoProcessual["dataUltimaAlteracao"] != DBNull.Value)
                TipoPrazoProcessual.dataUltimaAlteracao = Convert.ToDateTime(drTipoPrazoProcessual["dataUltimaAlteracao"]);
            else
                TipoPrazoProcessual.dataUltimaAlteracao = null;

            if (drTipoPrazoProcessual["Descricao"] != DBNull.Value)
                TipoPrazoProcessual.Descricao = drTipoPrazoProcessual["Descricao"].ToString();

            if (drTipoPrazoProcessual["quantidadeDiasPrazo"] != DBNull.Value)
                TipoPrazoProcessual.quantidadeDiasPrazo = Convert.ToInt32(drTipoPrazoProcessual["quantidadeDiasPrazo"].ToString());

        }

        private static void ValidaCampos(ref dtoTipoPrazoProcessual TipoPrazoProcessual)
        {

            if (String.IsNullOrEmpty(TipoPrazoProcessual.Descricao)) { TipoPrazoProcessual.Descricao = String.Empty; }

        }

    }
}