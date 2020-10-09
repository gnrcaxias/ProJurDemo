using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProJur.WebApplication.Controls
{
    public partial class dialogSelecaoUsuario1 : System.Web.UI.UserControl
    {

        public event EventHandler ConfirmarClickHandler;

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void OnConfirmarClick(object sender, EventArgs e)
        {
            if (ConfirmarClickHandler != null)
                ConfirmarClickHandler(this, e);
        }

        protected void btnConfirmarDialogSelecaoUsuario_Click(object sender, EventArgs e)
        {
            OnConfirmarClick(sender, e);
        }
    }
}