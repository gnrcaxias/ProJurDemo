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
    public class bllInstancia
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoInstancia Instancia)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbInstancia(Descricao, dataCadastro)
                                            VALUES(@Descricao, getdate());
                                            SET @idInstancia = SCOPE_IDENTITY()";

                SqlCommand cmdInstancia = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Instancia);

                cmdInstancia.Parameters.Add("idInstancia", SqlDbType.Int);
                cmdInstancia.Parameters["idInstancia"].Direction = ParameterDirection.Output;

                cmdInstancia.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Instancia.Descricao;

                try
                {
                    connection.Open();
                    cmdInstancia.ExecuteNonQuery();

                    return (int)cmdInstancia.Parameters["idInstancia"].Value;
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
        public static void Update(dtoInstancia Instancia)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbInstancia SET 
                                        Descricao = @Descricao,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idInstancia = @idInstancia";

                SqlCommand cmdInstancia = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Instancia);

                cmdInstancia.Parameters.Add("idInstancia", SqlDbType.Int).Value = Instancia.idInstancia;
                cmdInstancia.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Instancia.Descricao;

                try
                {
                    connection.Open();
                    cmdInstancia.ExecuteNonQuery();
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
        public static void Delete(dtoInstancia Instancia)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbInstancia 
                                      WHERE idInstancia = @idInstancia";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idInstancia", SqlDbType.Int).Value = Instancia.idInstancia;

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

        public static void Delete(int idInstancia)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbInstancia 
                                      WHERE idInstancia = @idInstancia";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idInstancia", SqlDbType.Int).Value = idInstancia;

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
        public static dtoInstancia Get(int idInstancia)
        {
            dtoInstancia Instancia = new dtoInstancia();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbInstancia
                                    WHERE idInstancia = @idInstancia";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idInstancia", SqlDbType.Int).Value = idInstancia;

                try
                {
                    connection.Open();
                    SqlDataReader drInstancia = cmdMenu.ExecuteReader();

                    if (drInstancia.Read())
                        PreencheCampos(drInstancia, ref Instancia);
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

            return Instancia;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoInstancia> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoInstancia> Instancias = new List<dtoInstancia>();

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

                    sbCondicao.AppendFormat(@" (tbInstancia.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbInstancia {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idInstancia"));

                SqlCommand cmdInstancia = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drInstancia = cmdInstancia.ExecuteReader();

                    while (drInstancia.Read())
                    {
                        dtoInstancia Instancia = new dtoInstancia();

                        PreencheCampos(drInstancia, ref Instancia);

                        Instancias.Add(Instancia);
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

            return Instancias;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoInstancia> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoInstancia> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }

        private static void PreencheCampos(SqlDataReader drInstancia, ref dtoInstancia Instancia)
        {

            if (drInstancia["idInstancia"] != DBNull.Value)
                Instancia.idInstancia = Convert.ToInt32(drInstancia["idInstancia"].ToString());

            if (drInstancia["dataCadastro"] != DBNull.Value)
                Instancia.dataCadastro = Convert.ToDateTime(drInstancia["dataCadastro"]);
            else
                Instancia.dataCadastro = null;

            if (drInstancia["dataUltimaAlteracao"] != DBNull.Value)
                Instancia.dataUltimaAlteracao = Convert.ToDateTime(drInstancia["dataUltimaAlteracao"]);
            else
                Instancia.dataUltimaAlteracao = null;

            if (drInstancia["Descricao"] != DBNull.Value)
                Instancia.Descricao = drInstancia["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoInstancia Instancia)
        {

            if (String.IsNullOrEmpty(Instancia.Descricao)) { Instancia.Descricao = String.Empty; }

        }

    }
}