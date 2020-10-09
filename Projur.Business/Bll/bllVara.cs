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
    public class bllVara
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoVara Vara)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbVara(Descricao, dataCadastro)
                                            VALUES(@Descricao, getdate());
                                            SET @idVara = SCOPE_IDENTITY()";

                SqlCommand cmdVara = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Vara);

                cmdVara.Parameters.Add("idVara", SqlDbType.Int);
                cmdVara.Parameters["idVara"].Direction = ParameterDirection.Output;

                cmdVara.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Vara.Descricao;

                try
                {
                    connection.Open();
                    cmdVara.ExecuteNonQuery();

                    return (int)cmdVara.Parameters["idVara"].Value;
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
        public static void Update(dtoVara Vara)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbVara SET 
                                        Descricao = @Descricao,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idVara = @idVara";

                SqlCommand cmdVara = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Vara);

                cmdVara.Parameters.Add("idVara", SqlDbType.Int).Value = Vara.idVara;
                cmdVara.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Vara.Descricao;

                try
                {
                    connection.Open();
                    cmdVara.ExecuteNonQuery();
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
        public static void Delete(dtoVara Vara)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbVara 
                                      WHERE idVara = @idVara";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idVara", SqlDbType.Int).Value = Vara.idVara;

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

        public static void Delete(int idVara)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbVara 
                                      WHERE idVara = @idVara";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idVara", SqlDbType.Int).Value = idVara;

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
        public static dtoVara Get(int idVara)
        {
            dtoVara Vara = new dtoVara();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbVara
                                    WHERE idVara = @idVara";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idVara", SqlDbType.Int).Value = idVara;

                try
                {
                    connection.Open();
                    SqlDataReader drVara = cmdMenu.ExecuteReader();

                    if (drVara.Read())
                        PreencheCampos(drVara, ref Vara);
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

            return Vara;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoVara> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoVara> Varas = new List<dtoVara>();

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

                    sbCondicao.AppendFormat(@" (tbVara.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbVara {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idVara"));

                SqlCommand cmdVara = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drVara = cmdVara.ExecuteReader();

                    while (drVara.Read())
                    {
                        dtoVara Vara = new dtoVara();

                        PreencheCampos(drVara, ref Vara);

                        Varas.Add(Vara);
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

            return Varas;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoVara> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoVara> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }


        private static void PreencheCampos(SqlDataReader drVara, ref dtoVara Vara)
        {

            if (drVara["idVara"] != DBNull.Value)
                Vara.idVara = Convert.ToInt32(drVara["idVara"].ToString());

            if (drVara["dataCadastro"] != DBNull.Value)
                Vara.dataCadastro = Convert.ToDateTime(drVara["dataCadastro"]);
            else
                Vara.dataCadastro = null;

            if (drVara["dataUltimaAlteracao"] != DBNull.Value)
                Vara.dataUltimaAlteracao = Convert.ToDateTime(drVara["dataUltimaAlteracao"]);
            else
                Vara.dataUltimaAlteracao = null;

            if (drVara["Descricao"] != DBNull.Value)
                Vara.Descricao = drVara["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoVara Vara)
        {

            if (String.IsNullOrEmpty(Vara.Descricao)) { Vara.Descricao = String.Empty; }

        }

    }
}