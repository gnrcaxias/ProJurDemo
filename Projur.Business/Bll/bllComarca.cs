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
    public class bllComarca
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoComarca Comarca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbComarca(Descricao, dataCadastro)
                                            VALUES(@Descricao, getdate());
                                            SET @idComarca = SCOPE_IDENTITY()";

                SqlCommand cmdComarca = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Comarca);

                cmdComarca.Parameters.Add("idComarca", SqlDbType.Int);
                cmdComarca.Parameters["idComarca"].Direction = ParameterDirection.Output;

                cmdComarca.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Comarca.Descricao;

                try
                {
                    connection.Open();
                    cmdComarca.ExecuteNonQuery();

                    return (int)cmdComarca.Parameters["idComarca"].Value;
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
        public static void Update(dtoComarca Comarca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbComarca SET 
                                        Descricao = @Descricao,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idComarca = @idComarca";

                SqlCommand cmdComarca = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Comarca);

                cmdComarca.Parameters.Add("idComarca", SqlDbType.Int).Value = Comarca.idComarca;
                cmdComarca.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Comarca.Descricao;

                try
                {
                    connection.Open();
                    cmdComarca.ExecuteNonQuery();
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
        public static void Delete(dtoComarca Comarca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbComarca 
                                      WHERE idComarca = @idComarca";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idComarca", SqlDbType.Int).Value = Comarca.idComarca;

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

        public static void Delete(int idComarca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbComarca 
                                      WHERE idComarca = @idComarca";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idComarca", SqlDbType.Int).Value = idComarca;

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
        public static dtoComarca Get(int idComarca)
        {
            dtoComarca Comarca = new dtoComarca();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbComarca
                                    WHERE idComarca = @idComarca";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idComarca", SqlDbType.Int).Value = idComarca;

                try
                {
                    connection.Open();
                    SqlDataReader drComarca = cmdMenu.ExecuteReader();

                    if (drComarca.Read())
                        PreencheCampos(drComarca, ref Comarca);
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

            return Comarca;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoComarca> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoComarca> Comarcas = new List<dtoComarca>();

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

                    sbCondicao.AppendFormat(@" (tbComarca.Descricao LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbComarca {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idComarca"));

                SqlCommand cmdComarca = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drComarca = cmdComarca.ExecuteReader();

                    while (drComarca.Read())
                    {
                        dtoComarca Comarca = new dtoComarca();

                        PreencheCampos(drComarca, ref Comarca);

                        Comarcas.Add(Comarca);
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

            return Comarcas;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoComarca> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoComarca> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }

        private static void PreencheCampos(SqlDataReader drComarca, ref dtoComarca Comarca)
        {

            if (drComarca["idComarca"] != DBNull.Value)
                Comarca.idComarca = Convert.ToInt32(drComarca["idComarca"].ToString());

            if (drComarca["dataCadastro"] != DBNull.Value)
                Comarca.dataCadastro = Convert.ToDateTime(drComarca["dataCadastro"]);
            else
                Comarca.dataCadastro = null;

            if (drComarca["dataUltimaAlteracao"] != DBNull.Value)
                Comarca.dataUltimaAlteracao = Convert.ToDateTime(drComarca["dataUltimaAlteracao"]);
            else
                Comarca.dataUltimaAlteracao = null;

            if (drComarca["Descricao"] != DBNull.Value)
                Comarca.Descricao = drComarca["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoComarca Comarca)
        {

            if (String.IsNullOrEmpty(Comarca.Descricao)) { Comarca.Descricao = String.Empty; }

        }

    }
}