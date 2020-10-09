using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ProJur.Business.Dto;
using ProJur.Business.Bll;

namespace ProJur.WebApplication.Controls
{
    public partial class menuCabecalhoPrincipal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                dtoUsuario usuario = bllUsuario.GetByLogin(HttpContext.Current.User.Identity.Name);

                if (usuario != null)
                    litUsuarioLogin.Text = usuario.nomeCompleto;
            }
        }

        protected void cmdSair_Click(object sender, ImageClickEventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}