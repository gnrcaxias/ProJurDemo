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
    public partial class ProcessoDespesa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
                    ConfiguraModoCRUD(DetailsViewMode.Insert);
                else
                {
                    dtoProcessoDespesa processoDespesa = bllProcessoDespesa.Get(Convert.ToInt32(Request.QueryString["ID"]));

                    if (processoDespesa != null && processoDespesa.idProcessoDespesa != 0)
                        ConfiguraModoCRUD(DetailsViewMode.ReadOnly);
                }
            }

            InicializaEventos();
            InicializaDefaultButton();

            Master.litCaminhoPrincipal.Text = "Manutenção > ";
            Master.litCaminhoSecundario.Text = "Processo > Despesa";
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
            dvProcessoDespesa.ChangeMode(novoModo);
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (dvProcessoDespesa.CurrentMode == DetailsViewMode.Edit)
                    {
                        dvProcessoDespesa.UpdateItem(false);
                    }
                    else
                    {
                        dvProcessoDespesa.InsertItem(true);
                    }
                }
                catch (Exception Ex)
                { }
            }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvProcessoDespesa.DeleteItem();

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

        protected void dsProcessoDespesa_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
        }

        protected void dvProcessoDespesa_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                e.Values["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

            if (e.Values["Valor"] != null
                && e.Values["Valor"].ToString().Trim() != String.Empty)
                e.Values["Valor"] = e.Values["Valor"].ToString().Replace(",", ".");
            else
                e.Values["Valor"] = 0;
        }

        protected void dvProcessoDespesa_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                e.NewValues["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

            if (e.NewValues["Valor"] != null
                && e.NewValues["Valor"].ToString().Trim() != String.Empty)
                e.NewValues["Valor"] = e.NewValues["Valor"].ToString().Replace(",", ".");
            else
                e.NewValues["Valor"] = 0;
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$menuAcoes$btnGravar";
        }

    }
}