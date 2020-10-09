using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;

namespace ProJur.WebApplication.Paginas.Manutencao
{
    public partial class ProcessoPeca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializaEventos();
            InicializaDefaultButton();

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
                    ConfiguraModoCRUD(DetailsViewMode.Insert);
                else
                {
                    dtoProcessoPeca processoPeca = bllProcessoPeca.Get(Convert.ToInt32(Request.QueryString["ID"]));

                    if (processoPeca != null && processoPeca.idProcessoPeca != 0)
                        ConfiguraModoCRUD(DetailsViewMode.ReadOnly);
                }
            }

            Master.litCaminhoPrincipal.Text = "Manutenção > ";
            Master.litCaminhoSecundario.Text = "Processo > Peça Processual";
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            ConfiguraModoCRUD(DetailsViewMode.Edit);
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != String.Empty)
                ConfiguraModoCRUD(DetailsViewMode.ReadOnly);
            else
            {
                if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                    Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
            }

        }

        private void ConfiguraModoCRUD(DetailsViewMode novoModo)
        {
            dvProcessoPeca.ChangeMode(novoModo);
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (dvProcessoPeca.CurrentMode == DetailsViewMode.Edit)
                    {
                        dvProcessoPeca.UpdateItem(false);
                    }
                    else
                    {
                        dvProcessoPeca.InsertItem(true);
                    }
                }
                catch (Exception Ex)
                { }
            }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvProcessoPeca.DeleteItem();

            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
        }


        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);

            menuAcoes.VoltarUrl = String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]);
        }

        protected void dsProcessoPeca_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
        }

        protected void dvProcessoPeca_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
            { 
                e.Values["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

                FileUpload fpArquivo = ((FileUpload)((DetailsView)sender).FindControl("fpArquivo"));
                string fileName = String.Format("PROCESSOPECA-{0}-{1}-{2}", Request.QueryString["IdProcesso"].ToString(), Math.Abs(DateTime.Now.ToBinary()).ToString(), fpArquivo.FileName.Trim());
                fpArquivo.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));
                e.Values["caminhoArquivo"] = fileName;
                e.Values["nomeArquivo"] = fpArquivo.FileName.Trim();
            }
        }

        protected void dvProcessoPeca_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
            {
                e.NewValues["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

                FileUpload fpArquivo = ((FileUpload)((DetailsView)sender).FindControl("fpArquivo"));

                if (fpArquivo.FileName != String.Empty)
                { 
                    string fileName = String.Format("PROCESSOPECA-{0}-{1}-{2}", Request.QueryString["IdProcesso"].ToString(), Math.Abs(DateTime.Now.ToBinary()).ToString(), fpArquivo.FileName.Trim());
                    fpArquivo.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));
                    e.NewValues["caminhoArquivo"] = fileName;
                    e.NewValues["nomeArquivo"] = fpArquivo.FileName.Trim();
                }
                else
                {
                    e.NewValues["caminhoArquivo"] = e.OldValues["caminhoArquivo"];
                    e.NewValues["nomeArquivo"] = e.OldValues["nomeArquivo"];
                }
            }
                
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$menuAcoes$btnGravar";
        }

    }
}