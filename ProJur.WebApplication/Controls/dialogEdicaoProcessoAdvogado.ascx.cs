using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProJur.WebApplication.Controls
{
    public partial class dialogEdicaoProcessoAdvogado : System.Web.UI.UserControl
    {
        public event EventHandler ConfirmarClickHandler;
        public event EventHandler FecharClickHandler;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnConfirmarClick(object sender, EventArgs e)
        {
            if (ConfirmarClickHandler != null)
                ConfirmarClickHandler(this, e);
        }

        protected void OnFecharClick(object sender, EventArgs e)
        {
            if (FecharClickHandler != null)
                FecharClickHandler(this, e);
        }

        protected void btnConfirmarDialogEdicaoProcessoAdvogado_Click(object sender, EventArgs e)
        {
            OnConfirmarClick(sender, e);
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            FecharClickHandler(sender, e);
        }

    }
}