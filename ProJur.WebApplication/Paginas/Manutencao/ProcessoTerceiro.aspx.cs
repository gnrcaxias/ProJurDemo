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
    public partial class ProcessoTerceiro : System.Web.UI.Page
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
                    dtoProcessoAdvogado processoAdvogado = bllProcessoAdvogado.Get(Convert.ToInt32(Request.QueryString["ID"]));

                    if (processoAdvogado != null && processoAdvogado.idProcessoAdvogado != 0)
                        ConfiguraModoCRUD(DetailsViewMode.ReadOnly);
                }
            }

            Master.litCaminhoPrincipal.Text = "Manutenção > ";
            Master.litCaminhoSecundario.Text = "Processo > Terceiro";
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
            dvProcessoTerceiro.ChangeMode(novoModo);
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (dvProcessoTerceiro.CurrentMode == DetailsViewMode.Edit)
                    {
                        dvProcessoTerceiro.UpdateItem(false);
                    }
                    else
                    {
                        dvProcessoTerceiro.InsertItem(true);
                    }
                }
                catch (Exception Ex)
                { }
            }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvProcessoTerceiro.DeleteItem();

            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
        }


        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);

            dialogSelecaoPessoa.FecharClickHandler += new EventHandler(FecharDialogSelecaoPessoa_Click);
            dialogSelecaoPessoa.SelecionarClickHandler += new GridViewCommandEventHandler(SelecionarDialogSelecaoPessoa_Click);

            menuAcoes.VoltarUrl = String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]);
        }

        protected void dsProcessoTerceiro_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
        }

        protected void FecharDialogSelecaoPessoa_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Hide();
        }

        protected void SelecionarDialogSelecaoPessoa_Click(object sender, GridViewCommandEventArgs e)
        {
            TextBox txtIdPessoaTerceiro = (TextBox)dvProcessoTerceiro.FindControl("txtIdPessoaTerceiro");
            Literal litNomePessoaTerceiro = (Literal)dvProcessoTerceiro.FindControl("litNomePessoaTerceiro");
            HiddenField hdIdPessoaTerceiro = (HiddenField)dvProcessoTerceiro.FindControl("hdIdPessoaTerceiro");

            if (txtIdPessoaTerceiro != null
                && litNomePessoaTerceiro != null
                && hdIdPessoaTerceiro != null)
            {
                hdIdPessoaTerceiro.Value = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
                txtIdPessoaTerceiro.Text = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
                litNomePessoaTerceiro.Text = ((GridView)e.CommandSource).SelectedDataKey["NomeCompletoRazaoSocial"].ToString();
            }

            mpeDialogSelecaoPessoa.Hide();
        }

        protected void dvProcessoTerceiro_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            string idPessoa = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaTerceiro")).Value;
            if (idPessoa.Trim() != String.Empty)
                e.Values["idPessoaTerceiro"] = idPessoa;

            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                e.Values["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);
        }

        protected void dvProcessoTerceiro_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            string idPessoa = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaTerceiro")).Value;
            if (idPessoa.Trim() != String.Empty)
                e.NewValues["idPessoaTerceiro"] = idPessoa;

            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                e.NewValues["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);
        }

        protected void btnSelecionarPessoaTerceiro_Click(object sender, EventArgs e)
        {
            dialogSelecaoPessoa.tipoPessoaTerceiro = "1";
            dialogSelecaoPessoa.CarregarPesquisa();

            mpeDialogSelecaoPessoa.Show();
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$menuAcoes$btnGravar";
        }

    }
}