using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ProJur.Business.Bll;
using ProJur.Business.Dto;
using InfoVillage.DevLibrary;

namespace ProJur.WebApplication
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializaDefaultButton();
            txtUsuario.Focus();
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {

                dtoUsuario usuario = bllUsuario.GetByLogin(txtUsuario.Text);

                if (usuario != null)
                {
                    if (usuario.Senha == Hash.GetHash(txtSenha.Text, Hash.HashType.SHA1))
                    {
                        Session["IDUSUARIO"] = usuario.idUsuario;
                        Session["IPUSUARIO"] = Request.ServerVariables["REMOTE_HOST"];
                        Session["LOGINUSUARIO"] = txtUsuario.Text;

                        FormsAuthentication.RedirectFromLoginPage(txtUsuario.Text, true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Erro", "alert('Usuário ou senha incorretos');", true);
                        txtUsuario.Focus();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Erro", "alert('Usuário ou senha incorretos');", true);
                    txtUsuario.Focus();
                }
            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Erro", String.Format("alert('{0}');", Ex.Message), true);
            }
        }

        protected void InicializaDefaultButton()
        {
            //MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)this.FindControl("form1");
            myForm.DefaultButton = btnEntrar.UniqueID;
        }
    }
}