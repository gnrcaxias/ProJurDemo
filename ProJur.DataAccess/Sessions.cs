using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace ProJur.DataAccess
{
    public class Sessions
    {

        public static int IdUsuarioLogado
        {
            get
            {
                System.Web.SessionState.HttpSessionState Session = HttpContext.Current.Session;

                //var myValue = Session["IDUSUARIO"];

                if (Session["IDUSUARIO"] != null
                    && Session["IDUSUARIO"].ToString() != String.Empty)
                {
                    return Convert.ToInt32(Session["IDUSUARIO"].ToString());
                }
                else
                    return 0;
            }
        }

    }
}
