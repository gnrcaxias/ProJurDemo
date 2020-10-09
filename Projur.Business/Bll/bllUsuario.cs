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
    public class bllUsuario
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoUsuario Usuario)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbUsuario(idGrupoUsuario, nomeCompleto, Usuario, Senha, Email, Bloqueado, Administrador, usuarioPadraoAgendamento, dataCadastro, idUsuarioCadastro, idPessoaVinculada)
                                            VALUES(@idGrupoUsuario, @nomeCompleto, @Usuario, @Senha, @Email, @Bloqueado, @Administrador, @usuarioPadraoAgendamento, getdate(), @idUsuarioCadastro, @idPessoaVinculada);
                                            SET @idUsuario = SCOPE_IDENTITY()";

                SqlCommand cmdUsuario = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Usuario);

                cmdUsuario.Parameters.Add("idUsuario", SqlDbType.Int);
                cmdUsuario.Parameters["idUsuario"].Direction = ParameterDirection.Output;

                cmdUsuario.Parameters.Add("idUsuarioCadastro", SqlDbType.Int).Value = Usuario.idUsuarioCadastro;

                cmdUsuario.Parameters.Add("idPessoaVinculada", SqlDbType.Int).Value = Usuario.idPessoaVinculada;

                cmdUsuario.Parameters.Add("idGrupoUsuario", SqlDbType.Int).Value = Usuario.idGrupoUsuario;
                cmdUsuario.Parameters.Add("nomeCompleto", SqlDbType.VarChar).Value = Usuario.nomeCompleto;
                cmdUsuario.Parameters.Add("Usuario", SqlDbType.VarChar).Value = Usuario.Usuario;
                cmdUsuario.Parameters.Add("Senha", SqlDbType.VarChar).Value = Hash.GetHash(Usuario.Senha, Hash.HashType.SHA1);
                cmdUsuario.Parameters.Add("Email", SqlDbType.VarChar).Value = Usuario.Email;

                cmdUsuario.Parameters.Add("Administrador", SqlDbType.Bit).Value = Usuario.Administrador;
                cmdUsuario.Parameters.Add("Bloqueado", SqlDbType.Bit).Value = Usuario.Bloqueado;
                cmdUsuario.Parameters.Add("usuarioPadraoAgendamento", SqlDbType.Bit).Value = Usuario.usuarioPadraoAgendamento;

                try
                {
                    connection.Open();
                    cmdUsuario.ExecuteNonQuery();

                    return (int)cmdUsuario.Parameters["idUsuario"].Value;
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
        public static void Update(dtoUsuario Usuario)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbUsuario SET 
                                        idPessoaVinculada = @idPessoaVinculada,
                                        idGrupoUsuario = @idGrupoUsuario,
                                        nomeCompleto = @nomeCompleto,
                                        Usuario = @Usuario,
                                        Email = @Email,
                                        Bloqueado = @Bloqueado,
                                        Administrador = @Administrador,
                                        usuarioPadraoAgendamento = @usuarioPadraoAgendamento,
                                        dataUltimaAlteracao = getdate(),
                                        idUsuarioUltimaAlteracao = @idUsuarioUltimaAlteracao
                                      WHERE idUsuario = @idUsuario";

                SqlCommand cmdUsuario = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Usuario);

                cmdUsuario.Parameters.Add("idPessoaVinculada", SqlDbType.Int).Value = Usuario.idPessoaVinculada;
                cmdUsuario.Parameters.Add("idUsuario", SqlDbType.Int).Value = Usuario.idUsuario;
                cmdUsuario.Parameters.Add("idGrupoUsuario", SqlDbType.Int).Value = Usuario.idGrupoUsuario;
                cmdUsuario.Parameters.Add("nomeCompleto", SqlDbType.VarChar).Value = Usuario.nomeCompleto;
                cmdUsuario.Parameters.Add("Usuario", SqlDbType.VarChar).Value = Usuario.Usuario;
                cmdUsuario.Parameters.Add("Email", SqlDbType.VarChar).Value = Usuario.Email;
                cmdUsuario.Parameters.Add("Bloqueado", SqlDbType.Bit).Value = Usuario.Bloqueado;
                cmdUsuario.Parameters.Add("Administrador", SqlDbType.Bit).Value = Usuario.Administrador;
                cmdUsuario.Parameters.Add("idUsuarioUltimaAlteracao", SqlDbType.Int).Value = Usuario.idUsuarioUltimaAlteracao;
                cmdUsuario.Parameters.Add("usuarioPadraoAgendamento", SqlDbType.Bit).Value = Usuario.usuarioPadraoAgendamento;

                try
                {
                    connection.Open();
                    cmdUsuario.ExecuteNonQuery();
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
        public static void Delete(dtoUsuario Usuario)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbUsuario 
                                      WHERE idUsuario = @idUsuario";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idUsuario", SqlDbType.Int).Value = Usuario.idUsuario;

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

        public static void Delete(int idUsuario)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbUsuario 
                                      WHERE idUsuario = @idUsuario";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

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
        public static dtoUsuario Get(int idUsuario)
        {
            dtoUsuario usuario = new dtoUsuario();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbUsuario
                                    WHERE idUsuario = @idUsuario";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

                try
                {
                    connection.Open();
                    SqlDataReader drUsuario = cmdMenu.ExecuteReader();

                    if (drUsuario.Read())
                        PreencheCampos(drUsuario, ref usuario);
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

            return usuario;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static dtoUsuario GetByLogin(string Usuario)
        {
            dtoUsuario usuario = null;

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                    FROM tbUsuario
                                    WHERE Usuario = @Usuario";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.Add("Usuario", SqlDbType.VarChar).Value = Usuario;

                try
                {
                    connection.Open();
                    SqlDataReader drUsuario = cmdMenu.ExecuteReader();

                    if (drUsuario.Read())
                    {
                        usuario = new dtoUsuario();
                        PreencheCampos(drUsuario, ref usuario);
                    }
                }
                catch (Exception Ex)
                {
                    throw new ApplicationException(Ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return usuario;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoUsuario> GetAll(string SortExpression, string termoPesquisa)
        {
            List<dtoUsuario> usuarios = new List<dtoUsuario>();

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

                    sbCondicao.AppendFormat(@" (tbUsuario.nomeCompleto LIKE '%{0}%') ", termoPesquisa);
                }

                string stringSQL = String.Format("SELECT * FROM tbUsuario {0} ORDER BY {1}", sbCondicao.ToString(), (SortExpression.Trim() != String.Empty ? SortExpression.Trim() : "idUsuario"));

                SqlCommand cmdUsuario = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drUsuario = cmdUsuario.ExecuteReader();

                    while (drUsuario.Read())
                    {
                        dtoUsuario usuario = new dtoUsuario();

                        PreencheCampos(drUsuario, ref usuario);

                        usuarios.Add(usuario);
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

            return usuarios;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoUsuario> GetAll()
        {
            return GetAll("");
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoUsuario> GetAll(string SortExpression)
        {
            return GetAll(SortExpression, "");
        }


        public static bool ChangePassword(int idUsuario, string novaSenha)
        {
            bool retorno = false;

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbUsuario SET 
                                        Senha = @Senha,
                                        DataUltimaAlteracao = getdate()
                                      WHERE idUsuario = @idUsuario";

                SqlCommand cmdUsuario = new SqlCommand(stringSQL, connection);

                cmdUsuario.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;
                cmdUsuario.Parameters.Add("Senha", SqlDbType.VarChar).Value = Hash.GetHash(novaSenha, Hash.HashType.SHA1);

                try
                {
                    connection.Open();
                    cmdUsuario.ExecuteNonQuery();
                    retorno = true;
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

            return retorno;
        }

        private static void PreencheCampos(SqlDataReader drUsuario, ref dtoUsuario Usuario)
        {

            // CAMPOS CHAVES
            if (drUsuario["idUsuario"] != DBNull.Value)
                Usuario.idUsuario = Convert.ToInt32(drUsuario["idUsuario"].ToString());

            if (drUsuario["idGrupoUsuario"] != DBNull.Value)
                Usuario.idGrupoUsuario = Convert.ToInt32(drUsuario["idGrupoUsuario"].ToString());

            if (drUsuario["idPessoaVinculada"] != DBNull.Value)
                Usuario.idPessoaVinculada = Convert.ToInt32(drUsuario["idPessoaVinculada"].ToString());

            // CAMPOS PADRAO DE SEGURANCA
            if (drUsuario["dataCadastro"] != DBNull.Value)
                Usuario.dataCadastro = Convert.ToDateTime(drUsuario["dataCadastro"]);
            else
                Usuario.dataCadastro = null;

            if (drUsuario["dataUltimaAlteracao"] != DBNull.Value)
                Usuario.dataUltimaAlteracao = Convert.ToDateTime(drUsuario["dataUltimaAlteracao"]);
            else
                Usuario.dataUltimaAlteracao = null;

            if (drUsuario["idUsuarioCadastro"] != DBNull.Value)
                Usuario.idUsuarioCadastro = Convert.ToInt32(drUsuario["idUsuarioCadastro"].ToString());

            if (drUsuario["idUsuarioUltimaAlteracao"] != DBNull.Value)
                Usuario.idUsuarioUltimaAlteracao = Convert.ToInt32(drUsuario["idUsuarioUltimaAlteracao"].ToString());

            if (drUsuario["Bloqueado"] != DBNull.Value)
                Usuario.Bloqueado = Convert.ToBoolean(drUsuario["Bloqueado"]);

            // CAMPOS NORMAIS
            if (drUsuario["Administrador"] != DBNull.Value)
                Usuario.Administrador = Convert.ToBoolean(drUsuario["Administrador"]);

            if (drUsuario["usuarioPadraoAgendamento"] != DBNull.Value)
                Usuario.usuarioPadraoAgendamento = Convert.ToBoolean(drUsuario["usuarioPadraoAgendamento"]);

            if (drUsuario["nomeCompleto"] != DBNull.Value)
                Usuario.nomeCompleto = drUsuario["nomeCompleto"].ToString();

            if (drUsuario["Usuario"] != DBNull.Value)
                Usuario.Usuario = drUsuario["Usuario"].ToString();

            if (drUsuario["Senha"] != DBNull.Value)
                Usuario.Senha = drUsuario["Senha"].ToString();

            if (drUsuario["Email"] != DBNull.Value)
                Usuario.Email = drUsuario["Email"].ToString();

        }

        private static void ValidaCampos(ref dtoUsuario Usuario)
        {
            if (String.IsNullOrEmpty(Usuario.nomeCompleto)) { Usuario.nomeCompleto = String.Empty; }
            if (String.IsNullOrEmpty(Usuario.Usuario)) { Usuario.Usuario = String.Empty; }
            if (String.IsNullOrEmpty(Usuario.Email)) { Usuario.Email = String.Empty; }
        }

    }
}