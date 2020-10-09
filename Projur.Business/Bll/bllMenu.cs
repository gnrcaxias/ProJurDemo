using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ProJur.DataAccess;
using System.Data;
using System.ComponentModel;
using ProJur.Business.Dto;

namespace ProJur.Business.Bll
{
    [DataObject(true)]
    public class bllMenu
    {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int Insert(dtoMenu Menu)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"INSERT INTO tbMenu(idChave, idMenuPai, Descricao, Visivel, Url, dataCadastro, idUsuarioCadastro)
                                            VALUES(@idChave, @idMenuPai, @Descricao, @Visivel, @Url, getdate(), @idUsuarioCadastro);
                                            SET @idMenu = SCOPE_IDENTITY()";

                SqlCommand cmdInserir = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Menu);

                cmdInserir.Parameters.Add("idMenu", SqlDbType.Int);
                cmdInserir.Parameters["idMenu"].Direction = ParameterDirection.Output;

                cmdInserir.Parameters.Add("idUsuarioCadastro", SqlDbType.Int).Value = Menu.idUsuarioCadastro;

                cmdInserir.Parameters.Add("idChave", SqlDbType.VarChar).Value = Menu.idChave;
                cmdInserir.Parameters.Add("idMenuPai", SqlDbType.Int).Value = Menu.idMenuPai;
                cmdInserir.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Menu.Descricao;
                cmdInserir.Parameters.Add("Visivel", SqlDbType.Bit).Value = Menu.Visivel;
                cmdInserir.Parameters.Add("Url", SqlDbType.VarChar).Value = Menu.Url;

                try
                {
                    connection.Open();
                    cmdInserir.ExecuteNonQuery();
                    return (int)cmdInserir.Parameters["idMenu"].Value;
                }
                catch
                {
                    //throw new ApplicationException("

                    throw new ApplicationException("Erro ao inserir registro");
                }
                finally
                {
                    connection.Close();
                }
                
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void Update(dtoMenu Menu)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"UPDATE tbMenu SET 
                                        idChave = @idChave,
                                        idMenuPai = @idMenuPai,
                                        Descricao = @Descricao,
                                        Visivel = @Visivel,
                                        Url = @Url,
                                        dataUltimaAlteracao = getdate(),
                                        idUsuarioUltimaAlteracao = @idUsuarioUltimaAlteracao
                                      WHERE idMenu = @idMenu";

                SqlCommand cmdAtualizar = new SqlCommand(stringSQL, connection);

                ValidaCampos(ref Menu);

                cmdAtualizar.Parameters.Add("idUsuarioUltimaAlteracao", SqlDbType.Int).Value = Menu.idUsuarioUltimaAlteracao;

                cmdAtualizar.Parameters.Add("idChave", SqlDbType.VarChar).Value = Menu.idChave;
                cmdAtualizar.Parameters.Add("idMenu", SqlDbType.Int).Value = Menu.idMenu;
                cmdAtualizar.Parameters.Add("idMenuPai", SqlDbType.Int).Value = Menu.idMenuPai;
                cmdAtualizar.Parameters.Add("Descricao", SqlDbType.VarChar).Value = Menu.Descricao;
                cmdAtualizar.Parameters.Add("Visivel", SqlDbType.Bit).Value = Menu.Visivel;
                cmdAtualizar.Parameters.Add("Url", SqlDbType.VarChar).Value = Menu.Url;


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

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void Delete(dtoMenu Menu)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbMenu 
                                      WHERE idMenu = @idMenu";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, connection);
                cmdDeletar.Parameters.Add("idMenu", SqlDbType.Int).Value = Menu.idMenu;

                try
                {
                    connection.Open();
                    cmdDeletar.ExecuteNonQuery();
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

        public static void Delete(int idMenu)
        {
            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"DELETE tbMenu 
                                      WHERE idMenu = @idMenu";

                SqlCommand cmdDeletar = new SqlCommand(stringSQL, connection);
                cmdDeletar.Parameters.Add("idMenu", SqlDbType.Int).Value = idMenu;

                try
                {
                    connection.Open();
                    cmdDeletar.ExecuteNonQuery();
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
        public static dtoMenu Get(int idMenu)
        {
            dtoMenu menu = new dtoMenu();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT tb1.*, tb2.Descricao AS descricaoPai
                                    FROM tbMenu AS tb1
                                    LEFT JOIN tbMenu AS tb2
	                                    ON tb1.idMenuPai = tb2.idMenu
                                    WHERE tb1.idMenu = @idMenu";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                
                cmdMenu.Parameters.Add("idMenu", SqlDbType.Int).Value = idMenu;

                try
                {
                    connection.Open();
                    SqlDataReader drMenu = cmdMenu.ExecuteReader();

                    if (drMenu.Read())
                        PreencheCampos(drMenu, ref menu);
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

            return menu;
        }
        
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<dtoMenu> GetAll()
        {
            List<dtoMenu> menus = new List<dtoMenu>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                SqlCommand cmdMenu = new SqlCommand("SELECT * FROM tbMenu ORDER BY idMenuPai, idMenu, Ordem", connection);

                try
                {
                    connection.Open();
                    SqlDataReader drMenu = cmdMenu.ExecuteReader();

                    while (drMenu.Read())
                    {
                        Dto.dtoMenu menu = new Dto.dtoMenu();

                        PreencheCampos(drMenu, ref menu);

                        menus.Add(menu);
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

            return menus;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataSet GetDataSet()
        {
            DataSet dsMenu = new DataSet();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                SqlCommand cmdMenu = new SqlCommand("SELECT * FROM tbMenu ORDER BY idMenuPai, idMenu, Ordem", connection);

                SqlDataAdapter daMenu = new SqlDataAdapter(cmdMenu);

                try
                {
                    connection.Open();
                    daMenu.Fill(dsMenu);
                }
                catch
                {
                    throw new ApplicationException("Erro ao capturar dataset");
                }
                finally
                {
                    connection.Close();
                }
            }

            return dsMenu;
        }

        public static List<dtoMenu> GetAllParentsWithChildrens(int idUsuario)
        {
            List<dtoMenu> menus = new List<dtoMenu>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string condicaoSQL = String.Empty;
                dtoUsuario usuario = bllUsuario.Get(idUsuario);

                if (!usuario.Administrador)
                    condicaoSQL = @"AND EXISTS 
                                    (
			                        SELECT 1 FROM tbMenu AS tbMenu2 
                                        WHERE tbMenu2.idMenuPai = tbMenu.idMenu
				                        AND tbMenu2.idMenu IN 
										                    (
										                    SELECT idMenu 
										                    FROM tbUsuarioPermissao 
										                    LEFT JOIN tbUsuario 
											                    ON tbUsuarioPermissao.idUsuario = tbUsuario.idUsuario
										                    WHERE tbUsuarioPermissao.idUsuario = @idUsuario 
										                    AND tbUsuarioPermissao.Exibir = 1
										                    )
			                        )";

                string stringSQL = String.Format(@"SELECT * 
                                                    FROM tbMenu
                                                    WHERE idMenuPai = 0 
                                                    {0}
                                                ORDER BY Ordem", condicaoSQL);

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);
                cmdMenu.Parameters.AddWithValue("idUsuario", idUsuario);

                try
                {
                    connection.Open();
                    SqlDataReader drMenu = cmdMenu.ExecuteReader();

                    while (drMenu.Read())
                    {
                        Dto.dtoMenu menu = new Dto.dtoMenu();

                        PreencheCampos(drMenu, ref menu);

                        menus.Add(menu);
                    }

                }
                catch (Exception Ex)
                {
                    //throw new ApplicationException("Erro ao capturar todos os registros");
                    throw new ApplicationException(Ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return menus;
        }

        public static List<dtoMenu> GetOnlyChildrens()
        {
            List<dtoMenu> menus = new List<dtoMenu>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                string stringSQL = @"SELECT *
                                        FROM tbMenu
                                        WHERE 
                                        NOT EXISTS 
                                        (SELECT 1 
	                                        FROM tbMenu AS tbMenu1 
	                                        WHERE tbMenu1.idMenuPai = tbMenu.idMenu)
                                        ORDER By tbMenu.idMenuPai, tbMenu.idMenu";

                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                try
                {
                    connection.Open();
                    SqlDataReader drMenu = cmdMenu.ExecuteReader();

                    while (drMenu.Read())
                    {
                        Dto.dtoMenu menu = new Dto.dtoMenu();

                        PreencheCampos(drMenu, ref menu);

                        menus.Add(menu);
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

            return menus;            
        }

        public static List<dtoMenu> GetAllChildrensByUsuario(int idMenuPai, int idUsuario)
        {
            List<dtoMenu> menus = new List<dtoMenu>();

            string stringSQL = @"SELECT * 
                                FROM tbMenu 
                                WHERE 
                                idMenuPai = @idMenuPai
                                AND idMenu IN (SELECT idMenu FROM tbUsuarioPermissao WHERE idUsuario = @idUsuario AND Exibir = 1)
                                ORDER BY idMenu, Ordem";

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                SqlCommand cmdMenu = new SqlCommand(stringSQL, connection);

                cmdMenu.Parameters.AddWithValue("idMenuPai", idMenuPai);
                cmdMenu.Parameters.AddWithValue("idUsuario", idUsuario);

                try
                {
                    connection.Open();
                    SqlDataReader drMenu = cmdMenu.ExecuteReader();

                    while (drMenu.Read())
                    {
                        Dto.dtoMenu menu = new Dto.dtoMenu();

                        PreencheCampos(drMenu, ref menu);

                        menus.Add(menu);
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

            return menus;
        }

        public static List<dtoMenu> GetAllChildrens(int idMenuPai)
        {



            List<dtoMenu> menus = new List<dtoMenu>();

            using (SqlConnection connection = new SqlConnection(DataAccess.Configuracao.getConnectionString()))
            {
                SqlCommand cmdMenu = new SqlCommand("SELECT * FROM tbMenu WHERE idMenuPai = @idMenuPai ORDER BY idMenu, Ordem", connection);

                cmdMenu.Parameters.AddWithValue("idMenuPai", idMenuPai);

                try
                {
                    connection.Open();
                    SqlDataReader drMenu = cmdMenu.ExecuteReader();

                    while (drMenu.Read())
                    {
                        Dto.dtoMenu menu = new Dto.dtoMenu();

                        PreencheCampos(drMenu, ref menu);

                        menus.Add(menu);
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

            return menus;
        }


        private static void PreencheCampos(SqlDataReader drMenu, ref dtoMenu Menu)
        {

            // CAMPOS CHAVES
            if (drMenu["idChave"] != DBNull.Value)
                Menu.idChave = drMenu["idChave"].ToString();


            if (drMenu["idMenu"] != DBNull.Value)
                Menu.idMenu = Convert.ToInt32(drMenu["idMenu"].ToString());

            if (drMenu["idMenuPai"] != DBNull.Value)
                Menu.idMenuPai = Convert.ToInt32(drMenu["idMenuPai"].ToString());

            // CAMPOS PADRAO SEGURANCA
            if (ProJur.DataAccess.Utilitarios.ColumnExists(drMenu, "descricaoPai"))
            {
                if (drMenu["descricaoPai"] != DBNull.Value)
                    Menu.descricaoMenuPai = drMenu["descricaoPai"].ToString();
            }

            if (drMenu["idUsuarioCadastro"] != DBNull.Value)
                Menu.idUsuarioCadastro = Convert.ToInt32(drMenu["idUsuarioCadastro"].ToString());

            if (drMenu["idUsuarioUltimaAlteracao"] != DBNull.Value)
                Menu.idUsuarioCadastro = Convert.ToInt32(drMenu["idUsuarioUltimaAlteracao"].ToString());

            if (drMenu["dataCadastro"] != DBNull.Value)
                Menu.dataCadastro = Convert.ToDateTime(drMenu["dataCadastro"]);
            else
                Menu.dataCadastro = null;

            if (drMenu["dataUltimaAlteracao"] != DBNull.Value)
                Menu.dataUltimaAlteracao = Convert.ToDateTime(drMenu["dataUltimaAlteracao"]);
            else
                Menu.dataUltimaAlteracao = null;

            if (drMenu["Visivel"] != DBNull.Value)
                Menu.Visivel = Convert.ToBoolean(drMenu["Visivel"]);


            // CAMPOS NORMAIS
            if (drMenu["Url"] != DBNull.Value)
                Menu.Url = drMenu["Url"].ToString();


            if (drMenu["Descricao"] != DBNull.Value)
                Menu.Descricao = drMenu["Descricao"].ToString();

        }

        private static void ValidaCampos(ref dtoMenu Menu)
        {
            if (String.IsNullOrEmpty(Menu.Descricao)) { Menu.Descricao = String.Empty; }
            if (String.IsNullOrEmpty(Menu.descricaoMenuPai)) { Menu.descricaoMenuPai = String.Empty; }
            if (String.IsNullOrEmpty(Menu.Url)) { Menu.Url = String.Empty; }
        }

    }

}
