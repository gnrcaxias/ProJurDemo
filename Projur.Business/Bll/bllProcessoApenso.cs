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
    public class bllProcessoApenso
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoProcessoApenso ProcessoApenso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbProcessoApenso(idProcesso, idProcessoVinculado)
                                            VALUES(@idProcesso, @idProcessoVinculado);
                                            SET @idProcessoApenso = SCOPE_IDENTITY()";

                SqlCommand cmdProcessoApenso = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoApenso);

                cmdProcessoApenso.Parameters.Add("idProcessoApenso", SqlDbType.Int);
                cmdProcessoApenso.Parameters["idProcessoApenso"].Direction = ParameterDirection.Output;

                cmdProcessoApenso.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoApenso.idProcesso;
                cmdProcessoApenso.Parameters.Add("idProcessoVinculado", SqlDbType.Int).Value = ProcessoApenso.idProcessoVinculado;

                try
                {
                    connection.Open();
                    cmdProcessoApenso.ExecuteNonQuery();

                    return (int)cmdProcessoApenso.Parameters["idProcessoApenso"].Value;
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
        public static void Update(dtoProcessoApenso ProcessoApenso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbProcessoApenso SET 
                                        idProcesso = @idProcesso,
                                        idProcessoVinculado = @idProcessoVinculado
                                      WHERE idProcessoApenso = @idProcessoApenso";

                SqlCommand cmdProcessoApenso = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref ProcessoApenso);

                cmdProcessoApenso.Parameters.Add("idProcessoApenso", SqlDbType.Int).Value = ProcessoApenso.idProcessoApenso;

                cmdProcessoApenso.Parameters.Add("idProcesso", SqlDbType.Int).Value = ProcessoApenso.idProcesso;
                cmdProcessoApenso.Parameters.Add("idProcessoVinculado", SqlDbType.Int).Value = ProcessoApenso.idProcessoVinculado;

                try
                {
                    connection.Open();
                    cmdProcessoApenso.ExecuteNonQuery();
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
        public static void Delete(dtoProcessoApenso ProcessoApenso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoApenso 
                                      WHERE idProcessoApenso = @idProcessoApenso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoApenso", SqlDbType.Int).Value = ProcessoApenso.idProcessoApenso;

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

        public static void Delete(int idProcessoApenso)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbProcessoApenso 
                                      WHERE idProcessoApenso = @idProcessoApenso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idProcessoApenso", SqlDbType.Int).Value = idProcessoApenso;

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
        public static dtoProcessoApenso Get(int idProcessoApenso)
        {
            dtoProcessoApenso ProcessoApenso = new dtoProcessoApenso();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbProcessoApenso
                                    WHERE idProcessoApenso = @idProcessoApenso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idProcessoApenso", SqlDbType.Int).Value = idProcessoApenso;

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoApenso = cmdMenu.ExecuteReader();

                    if (drProcessoApenso.Read())
                        PreencheCampos(drProcessoApenso, ref ProcessoApenso);
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

            return ProcessoApenso;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoApenso> GetAll(int idProcesso, string SortExpression)
        {
            List<dtoProcessoApenso> ProcessoApensos = new List<dtoProcessoApenso>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                StringBuilder sbCondicao = new StringBuilder();

                sbCondicao.AppendFormat(@" WHERE (tbProcessoApenso.idProcesso = {0})", idProcesso.ToString());

                string stringSQL = String.Format(@"SELECT * 
                                                FROM tbProcessoApenso 
                                                {0} 
                                                ORDER BY {1}", 
                                                sbCondicao.ToString(), 
                                                (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idProcessoApenso"));

                SqlCommand cmdProcessoApenso = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drProcessoApenso = cmdProcessoApenso.ExecuteReader();

                    while (drProcessoApenso.Read())
                    {
                        dtoProcessoApenso ProcessoApenso = new dtoProcessoApenso();

                        PreencheCampos(drProcessoApenso, ref ProcessoApenso);

                        ProcessoApensos.Add(ProcessoApenso);
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

            return ProcessoApensos;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoProcessoApenso> GetAll()
        {
            return GetAll(0, "");
        }

        private static void PreencheCampos(SqlDataReader drProcessoApenso, ref dtoProcessoApenso ProcessoApenso)
        {

            if (drProcessoApenso["idProcessoApenso"] != DBNull.Value)
                ProcessoApenso.idProcessoApenso = Convert.ToInt32(drProcessoApenso["idProcessoApenso"].ToString());

            if (drProcessoApenso["idProcesso"] != DBNull.Value)
                ProcessoApenso.idProcesso = Convert.ToInt32(drProcessoApenso["idProcesso"].ToString());

            if (drProcessoApenso["idProcessoVinculado"] != DBNull.Value)
                ProcessoApenso.idProcessoVinculado = Convert.ToInt32(drProcessoApenso["idProcessoVinculado"].ToString());

        }

        private static void ValidaCampos(ref dtoProcessoApenso ProcessoApenso)
        {

        }

    }
}
