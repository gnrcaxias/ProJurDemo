using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ProJur.DataAccess;
using System.Data;
using ProJur.Business.Dto;

namespace ProJur.Business.Bll
{

    [DataObject(true)]
    public class bllUsuarioPermissao
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoUsuarioPermissao Permissao)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbUsuarioPermissao(idMenu, idUsuario, idUsuarioModerador, Exibir, Imprimir, Novo, Excluir, Alterar, Pesquisar, Especial, dataCadastro, idUsuarioCadastro)
                                            VALUES(@idMenu, @idUsuario, @idUsuarioModerador, @Exibir, @Imprimir, @Novo, @Excluir, @Alterar, @Pesquisar, @Especial, getdate(), @idUsuarioCadastro);
                                            SET @idPermissao = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, connection);

                cmdInserir.Parameters.Add("idPermissao", SqlDbType.Int);
                cmdInserir.Parameters["idPermissao"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idMenu", SqlDbType.Int).Value = Permissao.idMenu;
                cmdInserir.Parameters.Add("idUsuario", SqlDbType.Int).Value = Permissao.idUsuario;
                cmdInserir.Parameters.Add("idUsuarioModerador", SqlDbType.Int).Value = Permissao.idUsuarioModerador;
                cmdInserir.Parameters.Add("idUsuarioCadastro", SqlDbType.Int).Value = Permissao.idUsuarioCadastro;

                cmdInserir.Parameters.Add("Exibir", SqlDbType.Bit).Value = Permissao.Exibir;
                cmdInserir.Parameters.Add("Imprimir", SqlDbType.Bit).Value = Permissao.Imprimir;
                cmdInserir.Parameters.Add("Novo", SqlDbType.Bit).Value = Permissao.Novo;
                cmdInserir.Parameters.Add("Excluir", SqlDbType.Bit).Value = Permissao.Excluir;
                cmdInserir.Parameters.Add("Alterar", SqlDbType.Bit).Value = Permissao.Alterar;
                cmdInserir.Parameters.Add("Pesquisar", SqlDbType.Bit).Value = Permissao.Pesquisar;
                cmdInserir.Parameters.Add("Especial", SqlDbType.Bit).Value = Permissao.Especial;

                try
                {
                    connection.Open();
                    cmdInserir.ExecuteNonQuery();
                    return (int)cmdInserir.Parameters["idPermissao"].Value;
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
        public static void Update(dtoUsuarioPermissao Permissao)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbUsuarioPermissao SET 
                                        idMenu = @idMenu,
                                        idUsuario = @idUsuario,
                                        idUsuarioModerador = @idUsuarioModerador,
                                        Exibir = @Exibir,
                                        Imprimir = @Imprimir,
                                        Novo = @Novo,
                                        Excluir = @Excluir,
                                        Alterar = @Alterar,
                                        Pesquisar = @Pesquisar,
                                        Especial = @Especial,
                                        dataUltimaAlteracao = getdate(),
                                        idUsuarioUltimaAlteracao = @idUsuarioUltimaAlteracao
                                      WHERE idPermissao = @idPermissao";

                SqlCommand cmdAtualizar = new SqlCommand(stringSQL, connection);

                cmdAtualizar.Parameters.Add("idPermissao", SqlDbType.Int).Value = Permissao.idPermissao;

                cmdAtualizar.Parameters.Add("idMenu", SqlDbType.Int).Value = Permissao.idMenu;
                cmdAtualizar.Parameters.Add("idUsuario", SqlDbType.Int).Value = Permissao.idUsuario;
                cmdAtualizar.Parameters.Add("idUsuarioModerador", SqlDbType.Int).Value = Permissao.idUsuarioModerador;

                cmdAtualizar.Parameters.Add("Exibir", SqlDbType.Bit).Value = Permissao.Exibir;
                cmdAtualizar.Parameters.Add("Imprimir", SqlDbType.Bit).Value = Permissao.Imprimir;
                cmdAtualizar.Parameters.Add("Novo", SqlDbType.Bit).Value = Permissao.Novo;
                cmdAtualizar.Parameters.Add("Excluir", SqlDbType.Bit).Value = Permissao.Excluir;
                cmdAtualizar.Parameters.Add("Alterar", SqlDbType.Bit).Value = Permissao.Alterar;
                cmdAtualizar.Parameters.Add("Pesquisar", SqlDbType.Bit).Value = Permissao.Pesquisar;
                cmdAtualizar.Parameters.Add("Especial", SqlDbType.Bit).Value = Permissao.Especial;

                cmdAtualizar.Parameters.Add("idUsuarioUltimaAlteracao", SqlDbType.Int).Value = Permissao.idUsuarioUltimaAlteracao;

                try
                {
                    connection.Open();
                    cmdAtualizar.ExecuteNonQuery();
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


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoUsuarioPermissao> GetAllByUsuario(int idUsuario, string SortExpression)
        {
            List<dtoUsuarioPermissao> Permissoes = new List<dtoUsuarioPermissao>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                try
                {
                    string stringSQL = @"SELECT 
                                    tbMenu.idMenu
                                FROM tbMenu
                                WHERE 
                                (NOT EXISTS 
                                (SELECT 1 
                                    FROM tbMenu AS tbMenu1 
                                    WHERE tbMenu1.idMenuPai = tbMenu.idMenu))
                                ORDER BY tbMenu.idMenuPai, tbMenu.idMenu";

                    SqlCommand cmdSelecionarMenus = new SqlCommand(stringSQL, connection);

                    connection.Open();
                    SqlDataReader drPermissao = cmdSelecionarMenus.ExecuteReader();

                    while (drPermissao.Read())
                    {
                        dtoUsuarioPermissao Permissao = new dtoUsuarioPermissao();

                        Permissao.idMenu = Convert.ToInt32(drPermissao["idMenu"].ToString());
                        Permissoes.Add(Permissao);
                    }
                }
                catch
                {
                    throw new ApplicationException("Erro ao capturar os menus para aplicar permissão");
                }
                finally
                {
                    connection.Close();
                }

                try
                {
                    string stringSQL = @"SELECT *
                                        FROM tbUsuarioPermissao
                                        WHERE idUsuario = @idUsuario";

                    SqlCommand cmdSelecionarPermissoesUsuario = new SqlCommand(stringSQL, connection);

                    cmdSelecionarPermissoesUsuario.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;

                    connection.Open();
                    SqlDataReader drPermissaoUsuario = cmdSelecionarPermissoesUsuario.ExecuteReader();

                    while (drPermissaoUsuario.Read())
                    {
                        dtoUsuarioPermissao Permissao = Permissoes.Find(t => t.idMenu == Convert.ToInt32(drPermissaoUsuario["idMenu"].ToString()));

                        if (Permissao != null)
                            PreencheCampos(drPermissaoUsuario, ref Permissao);
                    }
                }
                catch
                {
                    throw new ApplicationException("Erro ao selecionar permissões");
                }
                finally
                {
                    connection.Close();
                }

            }

            return Permissoes;
        }

        public static dtoUsuarioPermissao Get(int idUsuario, string chaveAcesso)
        {
            dtoUsuarioPermissao permissao = new dtoUsuarioPermissao();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                        FROM tbUsuarioPermissao
                                        LEFT JOIN tbMenu 
                                            ON tbUsuarioPermissao.idMenu = tbMenu.idMenu
                                        WHERE tbUsuarioPermissao.idUsuario = @idUsuario
                                        AND tbMenu.idChave = @idChave ";

                SqlCommand cmdSelecionar = new SqlCommand(stringSQL, connection);

                cmdSelecionar.Parameters.Add("idUsuario", SqlDbType.Int).Value = idUsuario;
                cmdSelecionar.Parameters.Add("idChave", SqlDbType.VarChar).Value = chaveAcesso.ToLower();

                try
                {
                    connection.Open();
                    SqlDataReader drSelecionar = cmdSelecionar.ExecuteReader();

                    if (drSelecionar.Read())
                        PreencheCampos(drSelecionar, ref permissao);
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

            return permissao;
        }


        private static void PreencheCampos(SqlDataReader drPermissao, ref dtoUsuarioPermissao Permissao)
        {

            // CAMPOS CHAVES
            if (drPermissao["idPermissao"] != DBNull.Value)
                Permissao.idPermissao = Convert.ToInt32(drPermissao["idPermissao"].ToString());

            if (drPermissao["idMenu"] != DBNull.Value)
                Permissao.idMenu = Convert.ToInt32(drPermissao["idMenu"].ToString());

            if (drPermissao["idUsuario"] != DBNull.Value)
                Permissao.idUsuario = Convert.ToInt32(drPermissao["idUsuario"].ToString());

            if (drPermissao["idUsuarioModerador"] != DBNull.Value)
                Permissao.idUsuarioModerador = Convert.ToInt32(drPermissao["idUsuarioModerador"].ToString());

            // CAMPOS PADRAO SEGURANCA
            if (drPermissao["dataCadastro"] != DBNull.Value)
                Permissao.dataCadastro = Convert.ToDateTime(drPermissao["dataCadastro"]);
            else
                Permissao.dataCadastro = null;

            if (drPermissao["dataUltimaAlteracao"] != DBNull.Value)
                Permissao.dataUltimaAlteracao = Convert.ToDateTime(drPermissao["dataUltimaAlteracao"]);
            else
                Permissao.dataUltimaAlteracao = null;

            if (drPermissao["idUsuarioCadastro"] != DBNull.Value)
                Permissao.idUsuarioCadastro = Convert.ToInt32(drPermissao["idUsuarioCadastro"].ToString());

            if (drPermissao["idUsuarioUltimaAlteracao"] != DBNull.Value)
                Permissao.idUsuarioUltimaAlteracao = Convert.ToInt32(drPermissao["idUsuarioUltimaAlteracao"].ToString());

            // CAMPOS NORMAIS
            if (drPermissao["Exibir"] != DBNull.Value)
                Permissao.Exibir = Convert.ToBoolean(drPermissao["Exibir"].ToString());

            if (drPermissao["Imprimir"] != DBNull.Value)
                Permissao.Imprimir = Convert.ToBoolean(drPermissao["Imprimir"].ToString());

            if (drPermissao["Novo"] != DBNull.Value)
                Permissao.Novo = Convert.ToBoolean(drPermissao["Novo"].ToString());

            if (drPermissao["Excluir"] != DBNull.Value)
                Permissao.Excluir = Convert.ToBoolean(drPermissao["Excluir"].ToString());

            if (drPermissao["Alterar"] != DBNull.Value)
                Permissao.Alterar = Convert.ToBoolean(drPermissao["Alterar"].ToString());

            if (drPermissao["Pesquisar"] != DBNull.Value)
                Permissao.Pesquisar = Convert.ToBoolean(drPermissao["Pesquisar"].ToString());

            if (drPermissao["Especial"] != DBNull.Value)
                Permissao.Especial = Convert.ToBoolean(drPermissao["Especial"].ToString());

        }

    }
}
