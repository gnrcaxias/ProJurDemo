
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProJur.WebApplication
{
    public partial class plesklib : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> liArquivos = new List<string>();
            
            //Cria comunicação com o servidor
            //Definir o diretório a ser listado
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://pluralltecnologia.com.br/meusbackups/");

            //Define que a ação vai ser de listar diretório
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            //Credenciais para o login (usuario, senha)
            request.Credentials = new NetworkCredential("pluralltecnolog", "plurall@1612#");
            
            //modo passivo
            request.UsePassive = true;

            //dados binarios
            request.UseBinary = true;

            //setar o KeepAlive para true
            request.KeepAlive = true;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                //Criando a Stream para pegar o retorno
                Stream responseStream = response.GetResponseStream();
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    //Adicionar os arquivos na lista
                    liArquivos = reader.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                }
            }

            //Responder a lista dos arquivos
            foreach (string item in liArquivos)
            {
                Response.Write(item + "<br />");
            }
        }
    }
}