using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Dto;
using ProJur.Business.Bll;

namespace ProJur.WebApplication.Controls
{
    public partial class menuEsquerdaPrincipal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            dtoUsuario usuario = bllUsuario.GetByLogin(HttpContext.Current.User.Identity.Name);

            if (usuario != null)
            {
                List<dtoMenu> menus = bllMenu.GetAllParentsWithChildrens(usuario.idUsuario);

                rptMenuGrupos.DataSource = menus;
                rptMenuGrupos.DataBind();
            }
        }

        protected void rptMenuGrupos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                dtoUsuario usuario = bllUsuario.GetByLogin(HttpContext.Current.User.Identity.Name);

                Repeater rptMenuItens = (Repeater)e.Item.FindControl("rptMenuItens");
                dtoMenu menuItem = (dtoMenu)e.Item.DataItem;
                List<dtoMenu> menusFilhos = null;

                if (usuario.Administrador)
                    menusFilhos = bllMenu.GetAllChildrens(menuItem.idMenu);
                else
                    menusFilhos = bllMenu.GetAllChildrensByUsuario(menuItem.idMenu, usuario.idUsuario);

                rptMenuItens.DataSource = menusFilhos;
                rptMenuItens.DataBind();
            }
        }
    }
 
}