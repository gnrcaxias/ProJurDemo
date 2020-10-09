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
    public class bllAgendaUsuario
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoAgendaUsuario AgendaUsuario)
        {
            if (Exists(AgendaUsuario))
                return 0;

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbAgendaUsuario(idAgendaCompromisso, idUsuario)
                                            VALUES(@idAgendaCompromisso, @idUsuario);
                                            SET @idAgendaUsuario = SCOPE_IDENTITY()";

                SqlCommand cmdAgendaUsuario = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref AgendaUsuario);

                cmdAgendaUsuario.Parameters.Add("idAgendaUsuario", SqlDbType.Int);
                cmdAgendaUsuario.Parameters["idAgendaUsuario"].Direction = ParameterDirection.Output;

                cmdAgendaUsuario.Parameters.Add("idAgendaCompromisso", SqlDbType.Int).Value = AgendaUsuario.idAgendaCompromisso;
                cmdAgendaUsuario.Parameters.Add("idUsuario", SqlDbType.Int).Value = AgendaUsuario.idUsuario;

                try
                {
                    connection.Open();
                    cmdAgendaUsuario.ExecuteNonQuery();

                    return (int)cmdAgendaUsuario.Parameters["idAgendaUsuario"].Value;
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
        public static void Update(dtoAgendaUsuario AgendaUsuario)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbAgendaUsuario SET 
                                        idAgendaCompromisso = @idAgendaCompromisso,
                                        idUsuario = @idUsuario
                                      WHERE idAgendaUsuario = @idAgendaUsuario";

                SqlCommand cmdAgendaUsuario = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref AgendaUsuario);

                cmdAgendaUsuario.Parameters.Add("idAgendaUsuario", SqlDbType.Int).Value = AgendaUsuario.idAgendaUsuario;
                cmdAgendaUsuario.Parameters.Add("idAgendaCompromisso", SqlDbType.Int).Value = AgendaUsuario.idAgendaCompromisso;
                cmdAgendaUsuario.Parameters.Add("idUsuario", SqlDbType.Int).Value = AgendaUsuario.idUsuario;

                try
                {
                    connection.Open();
                    cmdAgendaUsuario.ExecuteNonQuery();
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
        public static void Delete(dtoAgendaUsuario AgendaUsuario)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbAgendaUsuario 
                                      WHERE idAgendaUsuario = @idAgendaUsuario";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idAgendaUsuario", SqlDbType.Int).Value = AgendaUsuario.idAgendaUsuario;

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

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void DeleteByCompromissoUsuario(int idAgendaCompromisso, int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbAgendaUsuario 
                                      WHERE idUsuario = @idUsuario
                                        AND idAgendaCompromisso = @idAgendaCompromisso";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;
                cmdMenu.Parameters.Add("idAgendaCompromisso", SqlDbType.Int).Value = idAgendaCompromisso;

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


        public static void Delete(int idAgendaUsuario)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbAgendaUsuario 
                                      WHERE idAgendaUsuario = @idAgendaUsuario";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idAgendaUsuario", SqlDbType.Int).Value = idAgendaUsuario;

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

        public static bool Exists(dtoAgendaUsuario AgendaUsuario)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT 1 FROM tbAgendaUsuario
                                    WHERE idAgendaCompromisso = @idAgendaCompromisso
                                    AND idUsuario = @idUsuario";

                SqlCommand cmdExists = new SqlCommand(stringSQL, connection);

                cmdExists.Parameters.Add("idAgendaCompromisso", SqlDbType.Int).Value = AgendaUsuario.idAgendaCompromisso;
                cmdExists.Parameters.Add("idUsuario", SqlDbType.Int).Value = AgendaUsuario.idUsuario;

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



        [DataObjectMethod(DataObjectMethodType.Select)]
        public static dtoAgendaUsuario Get(int idAgendaUsuario)
        {
            dtoAgendaUsuario AgendaUsuario = new dtoAgendaUsuario();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbAgendaUsuario
                                    WHERE idAgendaUsuario = @idAgendaUsuario";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idAgendaUsuario", SqlDbType.Int).Value = idAgendaUsuario;

                try
                {
                    connection.Open();
                    SqlDataReader drAgendaUsuario = cmdMenu.ExecuteReader();

                    if (drAgendaUsuario.Read())
                        PreencheCampos(drAgendaUsuario, ref AgendaUsuario);
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

            return AgendaUsuario;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAgendaUsuario> GetAll(string SortExpression)
        {
            List<dtoAgendaUsuario> AgendaUsuarios = new List<dtoAgendaUsuario>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = String.Format("SELECT * FROM tbAgendaUsuario ORDER BY {0}", (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idAgendaUsuario"));

                SqlCommand cmdAgendaUsuario = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drAgendaUsuario = cmdAgendaUsuario.ExecuteReader();

                    while (drAgendaUsuario.Read())
                    {
                        dtoAgendaUsuario AgendaUsuario = new dtoAgendaUsuario();

                        PreencheCampos(drAgendaUsuario, ref AgendaUsuario);

                        AgendaUsuarios.Add(AgendaUsuario);
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

            return AgendaUsuarios;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoAgendaUsuario> GetAll()
        {
            return GetAll("");
        }

        public static List<dtoAgendaUsuario> GetByAgendaCompromisso(int idAgendaCompromisso)
        {
            List<dtoAgendaUsuario> AgendaUsuarios = new List<dtoAgendaUsuario>();

            if (idAgendaCompromisso > 0)
            {
                using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
                {
                    StringBuilder sbCondicao = new StringBuilder();

                    //if (idAgendaCompromisso.ToString() != "0")
                    //{
                    //if (sbCondicao.ToString() != String.Empty)
                    //sbCondicao.Append(" AND ");
                    //else
                    //sbCondicao.Append(" WHERE ");

                    sbCondicao.AppendFormat(@" WHERE (tbAgendaUsuario.idAgendaCompromisso = {0})", idAgendaCompromisso.ToString());
                    //}

                    string stringSQL = String.Format("SELECT * FROM tbAgendaUsuario {0} ORDER BY idAgendaUsuario", sbCondicao.ToString());

                    SqlCommand cmdAgendaUsuario = new SqlCommand(stringSQL, connection);

                    try
                    {
                        connection.Open();
                        SqlDataReader drAgendaUsuario = cmdAgendaUsuario.ExecuteReader();

                        while (drAgendaUsuario.Read())
                        {
                            dtoAgendaUsuario AgendaUsuario = new dtoAgendaUsuario();

                            PreencheCampos(drAgendaUsuario, ref AgendaUsuario);

                            AgendaUsuarios.Add(AgendaUsuario);
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
            }

            return AgendaUsuarios;
        }

        private static void PreencheCampos(SqlDataReader drAgendaUsuario, ref dtoAgendaUsuario AgendaUsuario)
        {

            if (drAgendaUsuario["idAgendaUsuario"] != DBNull.Value)
                AgendaUsuario.idAgendaUsuario = Convert.ToInt32(drAgendaUsuario["idAgendaUsuario"].ToString());

            if (drAgendaUsuario["idAgendaCompromisso"] != DBNull.Value)
                AgendaUsuario.idAgendaCompromisso =Convert.ToInt32(drAgendaUsuario["idAgendaCompromisso"].ToString());

            if (drAgendaUsuario["idUsuario"] != DBNull.Value)
                AgendaUsuario.idUsuario =Convert.ToInt32(drAgendaUsuario["idUsuario"].ToString());

        }

        private static void ValidaCampos(ref dtoAgendaUsuario AgendaUsuario)
        {


        }

    }


}
