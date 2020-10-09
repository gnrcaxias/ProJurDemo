using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using ProJur.Business.Dto;
using System.ComponentModel;
using System.Data.SqlClient;

namespace ProJur.Business.Bll
{
    public class bllBackupConfiguracaoFTP
    {

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static dtoBackupConfiguracaoFTP Get(int idBackupConfiguracaoFTP)
        {
            dtoBackupConfiguracaoFTP configuracaoFTP = null;

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string cmdText = "SELECT * FROM tbBackupConfiguracaoFTP WHERE idBackupConfiguracaoFTP = @idBackupConfiguracaoFTP";
                SqlCommand command = new SqlCommand(cmdText, connection);
                command.Parameters.AddWithValue("idBackupConfiguracaoFTP", idBackupConfiguracaoFTP);

                try
                {
                    connection.Open();
                    SqlDataReader drSelecionar = command.ExecuteReader();
                    if (drSelecionar.Read())
                    {
                        configuracaoFTP = new dtoBackupConfiguracaoFTP();
                        PreencheCampos(drSelecionar, ref configuracaoFTP);
                    }
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

            return configuracaoFTP;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoBackupConfiguracaoFTP backupConfiguracaoFTP)
        {
            int num;
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string cmdText = @"INSERT INTO tbBackupConfiguracaoFTP(idBackupConfiguracaoFTP, dataCadastro, Host, Usuario, Senha, Passivo, caminhoHttp) 
                                    VALUES(1, GETDATE(), @Host, @Usuario, @Senha, @Passivo, @caminhoHttp);";
                SqlCommand command = new SqlCommand(cmdText, connection);

                ValidaCampos(ref backupConfiguracaoFTP);
                
                command.Parameters.AddWithValue("Host", backupConfiguracaoFTP.Host);
                command.Parameters.AddWithValue("Usuario", backupConfiguracaoFTP.Usuario);
                command.Parameters.AddWithValue("Senha", backupConfiguracaoFTP.Senha);
                command.Parameters.AddWithValue("Passivo", backupConfiguracaoFTP.Passivo);
                command.Parameters.AddWithValue("caminhoHttp", backupConfiguracaoFTP.caminhoHttp);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    num = 1;
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
            return num;
        }

        private static void PreencheCampos(SqlDataReader drSelecionar, ref dtoBackupConfiguracaoFTP backupConfiguracaoFTP)
        {
            if (drSelecionar["idBackupConfiguracaoFTP"] != DBNull.Value)
            {
                backupConfiguracaoFTP.idBackupConfiguracaoFTP = Convert.ToInt32(drSelecionar["idBackupConfiguracaoFTP"].ToString());
            }

            if (drSelecionar["dataCadastro"] != DBNull.Value)
                backupConfiguracaoFTP.dataCadastro = Convert.ToDateTime(drSelecionar["dataCadastro"]);
            else
                backupConfiguracaoFTP.dataCadastro = null;

            if (drSelecionar["dataUltimaAlteracao"] != DBNull.Value)
                backupConfiguracaoFTP.dataUltimaAlteracao = Convert.ToDateTime(drSelecionar["dataUltimaAlteracao"]);
            else
                backupConfiguracaoFTP.dataUltimaAlteracao = null;


            if (drSelecionar["Host"] != DBNull.Value)
            {
                backupConfiguracaoFTP.Host = drSelecionar["Host"].ToString();
            }

            if (drSelecionar["Usuario"] != DBNull.Value)
            {
                backupConfiguracaoFTP.Usuario = drSelecionar["Usuario"].ToString();
            }

            if (drSelecionar["Senha"] != DBNull.Value)
            {
                backupConfiguracaoFTP.Senha = drSelecionar["Senha"].ToString();
            }

            if (drSelecionar["Passivo"] != DBNull.Value)
            {
                backupConfiguracaoFTP.Passivo = Convert.ToBoolean(drSelecionar["Passivo"].ToString());
            }

            if (drSelecionar["caminhoHttp"] != DBNull.Value)
            {
                backupConfiguracaoFTP.caminhoHttp = drSelecionar["caminhoHttp"].ToString();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void Update(dtoBackupConfiguracaoFTP backupConfiguracaoFTP)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string cmdText = @"UPDATE tbBackupConfiguracaoFTP SET                                         
                                    Host=@Host,                                         
                                    Usuario=@Usuario,                                         
                                    Senha=@Senha,                                         
                                    Passivo=@Passivo,
                                    dataUltimaAlteracao = getdate(),
                                    caminhoHttp=@caminhoHttp
                                WHERE idBackupConfiguracaoFTP = 1;";

                SqlCommand command = new SqlCommand(cmdText, connection);

                ValidaCampos(ref backupConfiguracaoFTP);

                command.Parameters.AddWithValue("Host", backupConfiguracaoFTP.Host);
                command.Parameters.AddWithValue("Usuario", backupConfiguracaoFTP.Usuario);
                command.Parameters.AddWithValue("Senha", backupConfiguracaoFTP.Senha);
                command.Parameters.AddWithValue("Passivo", backupConfiguracaoFTP.Passivo);
                command.Parameters.AddWithValue("caminhoHttp", backupConfiguracaoFTP.caminhoHttp);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
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

        private static void ValidaCampos(ref dtoBackupConfiguracaoFTP backupConfiguracaoFTP)
        {
            if (string.IsNullOrEmpty(backupConfiguracaoFTP.Host))
            {
                backupConfiguracaoFTP.Host = string.Empty;
            }

            if (string.IsNullOrEmpty(backupConfiguracaoFTP.Usuario))
            {
                backupConfiguracaoFTP.Usuario = string.Empty;
            }

            if (string.IsNullOrEmpty(backupConfiguracaoFTP.Senha))
            {
                backupConfiguracaoFTP.Senha = string.Empty;
            }

            if (string.IsNullOrEmpty(backupConfiguracaoFTP.caminhoHttp))
            {
                backupConfiguracaoFTP.caminhoHttp = string.Empty;
            }
        }

    }
}
