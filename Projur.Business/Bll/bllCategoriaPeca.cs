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
    public class bllCategoriaPeca
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoCategoriaPeca CategoriaPeca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbCategoriaPeca(Descricao, dataCadastro)
                                            VALUES(@Descricao, getdate());
                                            SET @idCategoriaPeca = SCOPE_IDENTITY()";

                SqlCommand cmdCategoriaPeca = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref CategoriaPeca);

                cmdCategoriaPeca.Parameters.Add("idCategoriaPeca", SqlDbType.Int);
                cmdCategoriaPeca.Parameters["idCategoriaPeca"].Direction = ParameterDirection.Output;

                cmdCategoriaPeca.Parameters.Add("Descricao", SqlDbType.VarChar).Value = CategoriaPeca.Descricao;

                try
                {
                    connection.Open();
                    cmdCategoriaPeca.ExecuteNonQuery();

                    return (int)cmdCategoriaPeca.Parameters["idCategoriaPeca"].Value;
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
        public static void Update(dtoCategoriaPeca CategoriaPeca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbCategoriaPeca SET 
                                        Descricao = @Descricao,
                                        dataUltimaAlteracao = getdate()
                                      WHERE idCategoriaPeca = @idCategoriaPeca";

                SqlCommand cmdCategoriaPeca = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref CategoriaPeca);

                cmdCategoriaPeca.Parameters.Add("idCategoriaPeca", SqlDbType.Int).Value = CategoriaPeca.idCategoriaPeca;
                cmdCategoriaPeca.Parameters.Add("Descricao", SqlDbType.VarChar).Value = CategoriaPeca.Descricao;

                try
                {
                    connection.Open();
                    cmdCategoriaPeca.ExecuteNonQuery();
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
        public static void Delete(dtoCategoriaPeca CategoriaPeca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbCategoriaPeca 
                                      WHERE idCategoriaPeca = @idCategoriaPeca";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idCategoriaPeca", SqlDbType.Int).Value = CategoriaPeca.idCategoriaPeca;

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

        public static void Delete(int idCategoriaPeca)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbCategoriaPeca 
                                      WHERE idCategoriaPeca = @idCategoriaPeca";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idCategoriaPeca", SqlDbType.Int).Value = idCategoriaPeca;

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
        public static dtoCategoriaPeca Get(int idCategoriaPeca)
        {
            dtoCategoriaPeca CategoriaPeca = new dtoCategoriaPeca();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbCategoriaPeca
                                    WHERE idCategoriaPeca = @idCategoriaPeca";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idCategoriaPeca", SqlDbType.Int).Value = idCategoriaPeca;

                try
                {
                    connection.Open();
                    SqlDataReader drCategoriaPeca = cmdMenu.ExecuteReader();

                    if (drCategoriaPeca.Read())
                        PreencheCampos(drCategoriaPeca, ref CategoriaPeca);
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

            return CategoriaPeca;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoCategoriaPeca> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoCategoriaPeca> CategoriaPecas = new List<dtoCategoriaPeca>();

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

                    sbCondicao.AppendFormat(@" (tbCategoriaPeca.Descricao LIKE '%{0}%') ", termoPesquisa);
                }                

                string stringSQL = String.Format("SELECT * FROM tbCategoriaPeca {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idCategoriaPeca"));

                SqlCommand cmdCategoriaPeca = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drCategoriaPeca = cmdCategoriaPeca.ExecuteReader();

                    while (drCategoriaPeca.Read())
                    {
                        dtoCategoriaPeca CategoriaPeca = new dtoCategoriaPeca();

                        PreencheCampos(drCategoriaPeca, ref CategoriaPeca);

                        CategoriaPecas.Add(CategoriaPeca);
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

            return CategoriaPecas;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoCategoriaPeca> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoCategoriaPeca> GetAll()
        {
            return GetAll("");
        }


        private static void PreencheCampos(SqlDataReader drCategoriaPeca, ref dtoCategoriaPeca CategoriaPeca)
        {

            if (drCategoriaPeca["idCategoriaPeca"] != DBNull.Value)
                CategoriaPeca.idCategoriaPeca = Convert.ToInt32(drCategoriaPeca["idCategoriaPeca"].ToString());

            if (drCategoriaPeca["dataCadastro"] != DBNull.Value)
                CategoriaPeca.dataCadastro = Convert.ToDateTime(drCategoriaPeca["dataCadastro"]);
            else
                CategoriaPeca.dataCadastro = null;

            if (drCategoriaPeca["dataUltimaAlteracao"] != DBNull.Value)
                CategoriaPeca.dataUltimaAlteracao = Convert.ToDateTime(drCategoriaPeca["dataUltimaAlteracao"]);
            else
                CategoriaPeca.dataUltimaAlteracao = null;

            if (drCategoriaPeca["Descricao"] != DBNull.Value)
                CategoriaPeca.Descricao = drCategoriaPeca["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoCategoriaPeca CategoriaPeca)
        {

            if (String.IsNullOrEmpty(CategoriaPeca.Descricao)) { CategoriaPeca.Descricao = String.Empty; }

        }

    }
}