using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProJur.WebApplication.Paginas.Cadastro
{
    public partial class Backup : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaPesquisa();
                InicializaCadastro();
            }
                

            InicializaEventos();
            InicializaDefaultButton();

            menuAcoesCadastro.MostrarExcluir = true;
            menuAcoesCadastro.UrlImagemIncluir = "configuracao.png";

            Master.litCaminhoPrincipal.Text = "ProJur > Gerenciador > ";
            Master.litCaminhoSecundario.Text = "Backups";
        }


        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = btnPesquisar.UniqueID;
        }

        protected void InicializaCadastro()
        {
            grdResultado.Sort("DATAARQUIVO", SortDirection.Descending);
        }


        protected void btnExcluirSelecionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdResultado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkExcluir = (CheckBox)row.FindControl("chkExcluir");

                    HiddenField hdNomeArquivo = (HiddenField)row.FindControl("hdNomeArquivo");

                    if (chkExcluir.Checked)
                        bllBackup.DeleteFile(hdNomeArquivo.Value);
                }
            }

            grdResultado.DataBind();
        }

        protected void btnPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            ListarArquivosDiretorioFTP();
        }

        private void CarregaPesquisa()
        {
            //grdResultado.DataSource = bllBackup.GetAll().ToList();
            //grdResultado.DataBind();
            dsResultado.DataBind();
            grdResultado.DataBind();
        }

        private void InicializaEventos()
        {
            menuAcoesCadastro.ExcluirClickHandler += new EventHandler(btnExcluirSelecionados_Click);
            menuAcoesCadastro.ImprimirClickHandler += new EventHandler(Imprimir_Click);
        }



        protected void dsResultado_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            litTotalRegistros.Text = String.Format("{0} registro(s) encontrado(s)", ((List<dtoBackup>)e.ReturnValue).Count.ToString());
            litTamanhoTotal.Text = String.Format("{0} MB ocupados no servidor", ((List<dtoBackup>)e.ReturnValue).Sum(x => x.tamanhoArquivo).ToString("0"));
        }


        protected void Imprimir_Click(object sender, EventArgs e)
        {

        }

        protected void ListarArquivosDiretorioFTP()
        {



        }

        protected string RetornaUrlBackup(object nomeArquivo)
        {
            string Retorno = String.Empty;

            if (nomeArquivo != null)
            {
                dtoBackupConfiguracaoFTP configuracaoFTP = bllBackupConfiguracaoFTP.Get(1);

                if (configuracaoFTP != null)
                {
                    if (configuracaoFTP.Host.Trim() != String.Empty
                        && configuracaoFTP.Usuario.Trim() != String.Empty
                        && configuracaoFTP.Senha.Trim() != String.Empty)
                    {

                        if (configuracaoFTP.Host.IndexOf("ftp:") < 0)
                            configuracaoFTP.Host = String.Format("{0}{1}", @"ftp://", configuracaoFTP.Host);

                        if (configuracaoFTP.Host.Trim().LastIndexOf("/") != configuracaoFTP.Host.Trim().Length - 1)
                            configuracaoFTP.Host = String.Format("{0}{1}", configuracaoFTP.Host, @"/");

                        //FTP Server URL.
                        string ftp = configuracaoFTP.Host;

                        //FTP Folder name. Leave blank if you want to list files from root folder.
                        string ftpFolder = "";

                        Retorno = String.Format("{0}{1}{2}", ftp, ftpFolder, nomeArquivo.ToString());

                    }
                }

            }

            return Retorno;

        }

        protected void DownloadArquivoFTP(object nomeArquivo)
        {



        }

        protected void linkDownloadArquivo_Click(object sender, EventArgs e)
        {
            dtoBackupConfiguracaoFTP configuracaoFTP = bllBackupConfiguracaoFTP.Get(1);

            if (configuracaoFTP != null)
            {
                if (configuracaoFTP.Host.Trim() != String.Empty
                    && configuracaoFTP.Usuario.Trim() != String.Empty
                    && configuracaoFTP.Senha.Trim() != String.Empty)
                {

                    if (configuracaoFTP.Host.IndexOf("ftp:") < 0)
                        configuracaoFTP.Host = String.Format("{0}{1}", @"ftp://", configuracaoFTP.Host);

                    if (configuracaoFTP.Host.Trim().LastIndexOf("/") != configuracaoFTP.Host.Trim().Length - 1)
                        configuracaoFTP.Host = String.Format("{0}{1}", configuracaoFTP.Host, @"/");

                    string fileName = (sender as LinkButton).CommandArgument;

                    //FTP Server URL.
                    string ftp = configuracaoFTP.Host;

                    string ftpFolder = "";

                    try
                    {
                        //Create FTP Request.
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(String.Format("{0}{1}{2}", ftp, ftpFolder, fileName));
                        request.Method = WebRequestMethods.Ftp.DownloadFile;

                        //Enter FTP Server credentials.
                        request.Credentials = new NetworkCredential(configuracaoFTP.Usuario, configuracaoFTP.Senha);


                        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                        Stream responseStream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(responseStream);
                        Console.WriteLine(reader.ReadToEnd());

                        //Console.WriteLine("Download Complete, status {0}", response.StatusDescription);

                        reader.Close();
                        response.Close();

                        //request.UsePassive = configuracaoFTP.Passivo;
                        //request.UseBinary = true;
                        //request.EnableSsl = false;
                        ////request.KeepAlive = true;

                        ////Fetch the Response and read it into a MemoryStream object.
                        //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                        //using (MemoryStream stream = new MemoryStream())
                        //{
                        //    //Download the File.
                        //    response.GetResponseStream().CopyTo(stream);

                        //    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                        //    Response.AddHeader("Content-Length", stream.Length.ToString());
                        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //    Response.BinaryWrite(stream.ToArray());
                        //    //Response.End();
                        //    Response.Flush();
                        //    Response.Close();
                        //}
                        //using (FileStream outputStream = new FileStream(fileName, FileMode.OpenOrCreate))
                        //using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        //using (Stream ftpStream = response.GetResponseStream())
                        //{
                        //    int bufferSize = 2048;
                        //    int readCount;
                        //    byte[] buffer = new byte[bufferSize];
                        //    readCount = ftpStream.Read(buffer, 0, bufferSize);
                        //    while (readCount > 0)
                        //    {
                        //        outputStream.Write(buffer, 0, readCount);
                        //        readCount = ftpStream.Read(buffer, 0, bufferSize);
                        //    }
                        //}

                    }
                    catch (WebException ex)
                    {
                        throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
                    }
                }
            }
        }

        protected void lnkRemoverArquivoFTP_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton lnkRemoverArquivoFTP = (ImageButton)sender;
            GridViewRow row = (GridViewRow)lnkRemoverArquivoFTP.NamingContainer;
            HiddenField hdNomeArquivo = (HiddenField)row.FindControl("hdNomeArquivo");

            if (hdNomeArquivo != null)
            {
                bllBackup.DeleteFile(hdNomeArquivo.Value);
            }

            grdResultado.DataBind();
        }


    }
}
