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
    public class bllProcessoParte
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcessoParte ProcessoParte)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcessoParte(idProcesso, idPessoaParte, tipoParte)
                                            VALUES(@idProcesso, @idPessoaParte, @tipoParte);
                                            SET @idProcessoParte = SCOPE_IDENTITY()";

                SqlCommand cmdProcessoParte = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoParte);

                cmdProcessoParte.Parameters.Add("idProcessoParte", SqlDbType.Int);
                cmdProcessoParte.Parameters["idProcessoParte"].Direction = ParameterDirection.Output;

                cmdProcessoParte.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoParte.idProcesso;
                cmdProcessoParte.Parameters.Add("idPessoaParte", SqlDbType.Int).Value = ProcessoParte.idPessoaParte;
                cmdProcessoParte.Parameters.Add("tipoParte", SqlDbType.VarChar).Value = ProcessoParte.tipoParte;

                try
                {
                    connection.Open();
                    cmdProcessoParte.ExecuteNonQuery();

                    return (int)cmdProcessoParte.Parameters["idProcessoParte"].Value;
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
        public static void Update(dtoProcessoParte ProcessoParte)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcessoParte SET 
                                        idProcesso = @idProcesso,
                                        idPessoaParte = @idPessoaParte,
                                        tipoParte = @tipoParte
                                      WHERE idProcessoParte = @idProcessoParte";

                SqlCommand cmdProcessoParte = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoParte);

                cmdProcessoParte.Parameters.Add("idProcessoParte", SqlDbType.Int).Value = ProcessoParte.idProcessoParte;

                cmdProcessoParte.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoParte.idProcesso;
                cmdProcessoParte.Parameters.Add("idPessoaParte", SqlDbType.Int).Value = ProcessoParte.idPessoaParte;
                cmdProcessoParte.Parameters.Add("tipoParte", SqlDbType.VarChar).Value = ProcessoParte.tipoParte;

                try
                {
                    connection.Open();
                    cmdProcessoParte.ExecuteNonQuery();
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
        public static void Delete(dtoProcessoParte ProcessoParte)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoParte 
                                      WHERE idProcessoParte = @idProcessoParte";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoParte", SqlDbType.Int).Value = ProcessoParte.idProcessoParte;

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

        public static void Delete(int idProcessoParte)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoParte 
                                      WHERE idProcessoParte = @idProcessoParte";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoParte", SqlDbType.Int).Value = idProcessoParte;

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
        public static dtoProcessoParte Get(int idProcessoParte)
        {
            dtoProcessoParte ProcessoParte = new dtoProcessoParte();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoParte
                                    WHERE idProcessoParte = @idProcessoParte";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoParte", SqlDbType.Int).Value = idProcessoParte;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoParte = cmdMenu.ExecuteReader();

                    if (drProcessoParte.Read())
                        PreencheCampos(drProcessoParte, ref ProcessoParte);
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

            return ProcessoParte;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoParte> GetAll(int idProcesso, string SortExpression)
        {
            List<dtoProcessoParte> ProcessoPartes = new List<dtoProcessoParte>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                sbCondicao.AppendFormat(@" WHERE (tbProcessoParte.idProcesso = {0})", idProcesso.ToString());

                string stringSQL = String.Format(@"SELECT * 
                                                FROM tbProcessoParte 
                                                {0} 
                                                ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idProcessoParte"));

                SqlCommand cmdProcessoParte = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoParte = cmdProcessoParte.ExecuteReader();

                    while (drProcessoParte.Read())
                    {
                        dtoProcessoParte ProcessoParte = new dtoProcessoParte();

                        PreencheCampos(drProcessoParte, ref ProcessoParte);

                        ProcessoPartes.Add(ProcessoParte);
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

            return ProcessoPartes;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoParte> GetAll()
        {
            return GetAll(0, "");
        }

        private static void PreencheCampos(SqlDataReader drProcessoParte, ref dtoProcessoParte ProcessoParte)
        {

            if (drProcessoParte["idProcessoParte"] != DBNull.Value)
                ProcessoParte.idProcessoParte = Convert.ToInt32(drProcessoParte["idProcessoParte"].ToString());

            if (drProcessoParte["idProcesso"] != DBNull.Value)
                ProcessoParte.idProcesso = Convert.ToInt32(drProcessoParte["idProcesso"].ToString());

            if (drProcessoParte["idPessoaParte"] != DBNull.Value)
                ProcessoParte.idPessoaParte = Convert.ToInt32(drProcessoParte["idPessoaParte"].ToString());

            if (drProcessoParte["tipoParte"] != DBNull.Value)
                ProcessoParte.tipoParte = drProcessoParte["tipoParte"].ToString();

        }

        private static void ValidaCampos(ref dtoProcessoParte ProcessoParte)
        {

            if (String.IsNullOrEmpty(ProcessoParte.tipoParte)) { ProcessoParte.tipoParte = String.Empty; }

        }

        public static string RetornaDescricaoTipoParte(object tipoParte)
        {
            string retorno = String.Empty;

            if (tipoParte != null)
            {
                switch ((string)tipoParte)
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
