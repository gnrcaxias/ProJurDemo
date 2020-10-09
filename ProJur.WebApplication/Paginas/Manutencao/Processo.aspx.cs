using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProJur.WebApplication.Paginas.Manutencao
{
    public partial class Processo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
                    ConfiguraModoCRUD(DetailsViewMode.Insert);
                else
                {
                    dtoProcesso processo = bllProcesso.Get(Convert.ToInt32(Request.QueryString["ID"]));

                    if (processo != null && processo.idProcesso != 0)
                        ConfiguraModoCRUD(DetailsViewMode.ReadOnly);
                }

                SelecionarTabRetornoSubManutencao();
            }

            InicializaDefaultButton();
            InicializaEventos();

            if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
            {
                Master.litCaminhoPrincipal.Text = "Manutenção > ";
                Master.litCaminhoSecundario.Text = "Processo";
            }
            else
            {
                Master.litCaminhoPrincipal.Text = "Manutenção > Processo > ";
                Master.litCaminhoSecundario.Text = String.Format("{0}", bllPessoa.Get(bllProcesso.Get(Convert.ToInt32(Request.QueryString["ID"])).idPessoaCliente).NomeCompletoRazaoSocial);

            }
        }

        private void SelecionarTabRetornoSubManutencao()
        {
            if (Page.Request.UrlReferrer != null)
            {
                if (Page.Request.UrlReferrer.ToString().IndexOf("ProcessoParte.aspx", 0) > 0)
                {
                    string scriptSelectTab = @"$('#tabs').tabs({active: 1});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "kk", scriptSelectTab, true);
                }

                if (Page.Request.UrlReferrer.ToString().IndexOf("ProcessoAdvogado.aspx", 0) > 0)
                {
                    string scriptSelectTab = @"$('#tabs').tabs({active: 2});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "kk", scriptSelectTab, true);
                }

                if (Page.Request.UrlReferrer.ToString().IndexOf("ProcessoAndamento.aspx", 0) > 0)
                {
                    string scriptSelectTab = @"$('#tabs').tabs({active: 3});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "kk", scriptSelectTab, true);
                }

                if (Page.Request.UrlReferrer.ToString().IndexOf("ProcessoPeca.aspx", 0) > 0)
                {
                    string scriptSelectTab = @"$('#tabs').tabs({active: 4});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "kk", scriptSelectTab, true);
                }

                if (Page.Request.UrlReferrer.ToString().IndexOf("ProcessoPrazo.aspx", 0) > 0)
                {
                    string scriptSelectTab = @"$('#tabs').tabs({active: 5});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "kk", scriptSelectTab, true);
                }

                if (Page.Request.UrlReferrer.ToString().IndexOf("Default.aspx", 0) > 0)
                {
                    string scriptSelectTab = @"$('#tabs').tabs({active: 5});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "kk", scriptSelectTab, true);
                }

                if (Page.Request.UrlReferrer.ToString().IndexOf("ProcessoDespesa.aspx", 0) > 0)
                {
                    string scriptSelectTab = @"$('#tabs').tabs({active: 7});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "kk", scriptSelectTab, true);
                }

                if (Page.Request.UrlReferrer.ToString().IndexOf("ProcessoTerceiro.aspx", 0) > 0)
                {
                    string scriptSelectTab = @"$('#tabs').tabs({active: 8});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "kk", scriptSelectTab, true);
                }
            }

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
                Response.Redirect(String.Format("{0}/Paginas/Cadastro/Processo.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (dvAbertura.CurrentMode == DetailsViewMode.Edit)
                        dvAbertura.UpdateItem(false);
                    else
                        dvAbertura.InsertItem(true);
                }
                catch (Exception Ex)
                { }
            }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvAbertura.DeleteItem();

            Response.Redirect(String.Format("{0}/Paginas/Cadastro/Processo.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        public string FormatText(object input)
        {
            if (input != null
                && input.ToString() != String.Empty)
                return input.ToString().Replace("\n", "<br />");
            else
                return "";
        }

        protected void dataDistribuicao_ServerValidate(object source, ServerValidateEventArgs args)
        {
            TextBox TextdataBaixa = (TextBox)dvAbertura.FindControl("TextdataBaixa");

            if (args.Value != null
                && args.Value != String.Empty
                && TextdataBaixa.Text != null
                && TextdataBaixa.Text != String.Empty)
            {
                DateTime dataPublicacao;
                DateTime dataBaixa;

                if (DateTime.TryParse(args.Value, out dataPublicacao)
                    && DateTime.TryParse(TextdataBaixa.Text, out dataBaixa))
                {
                    if (dataPublicacao.Subtract(dataBaixa).Ticks > 0)
                    {
                        args.IsValid = false;
                    }
                }
            }
        }


        protected void btnPesquisarPessoaCliente_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Show();
        }

        protected void btnAdicionarProcessoAdvogado_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoAdvogado.aspx?IdProcesso={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["ID"]));
        }

        protected void btnAdicionarProcessoAndamento_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoAndamento.aspx?IdProcesso={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["ID"]));
        }

        protected void btnAdicionarProcessoApenso_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoProcesso.Show();
        }

        protected void btnAdicionarProcessoDespesa_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoDespesa.aspx?IdProcesso={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["ID"]));
        }

        protected void btnAdicionarProcessoParte_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoParte.aspx?IdProcesso={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["ID"]));
        }

        protected void btnAdicionarProcessoPeca_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoPeca.aspx?IdProcesso={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["ID"]));
        }

        protected void btnAdicionarProcessoPrazo_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoPrazo.aspx?IdProcesso={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["ID"]));
        }

        protected void btnAdicionarProcessoTerceiro_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoTerceiro.aspx?IdProcesso={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["ID"]));
        }

        protected void dsAbertura_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), e.ReturnValue));
        }

        protected void dvAbertura_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            string idPessoaCliente = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaCliente")).Value;
            if (idPessoaCliente.Trim() != String.Empty)
                e.Values["idPessoaCliente"] = idPessoaCliente;

            if (e.Values["valorCausa"] != null
                && e.Values["valorCausa"].ToString().Trim() != String.Empty)
                e.Values["valorCausa"] = e.Values["valorCausa"].ToString().Replace(".", "").Replace(",", ".");
            else
                e.Values["valorCausa"] = 0;
        }

        protected void dvAbertura_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            string idPessoaCliente = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaCliente")).Value;
            if (idPessoaCliente.Trim() != String.Empty)
                e.NewValues["idPessoaCliente"] = idPessoaCliente;

            if (e.NewValues["valorCausa"] != null
                && e.NewValues["valorCausa"].ToString().Trim() != String.Empty)
                e.NewValues["valorCausa"] = e.NewValues["valorCausa"].ToString().Replace(".", "").Replace(",", ".");
            else
                e.NewValues["valorCausa"] = 0;
        }

        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);

            dialogSelecaoPessoa.FecharClickHandler += new EventHandler(FecharDialogSelecaoPessoa_Click);
            dialogSelecaoPessoa.SelecionarClickHandler += new GridViewCommandEventHandler(SelecionarDialogSelecaoPessoa_Click);

            dialogSelecaoProcesso.FecharClickHandler += new EventHandler(FecharDialogSelecaoProcesso_Click);
            dialogSelecaoProcesso.SelecionarClickHandler += new GridViewCommandEventHandler(SelecionarDialogSelecaoProcesso_Click);
        }

        private void ConfiguraModoCRUD(DetailsViewMode novoModo)
        {
            dvAbertura.ChangeMode(novoModo);
        }


        protected void FecharDialogSelecaoPessoa_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Hide();
        }

        protected void SelecionarDialogSelecaoPessoa_Click(object sender, GridViewCommandEventArgs e)
        {
            TextBox txtIdPessoaCliente = (TextBox)dvAbertura.FindControl("txtIdPessoaCliente");
            Literal litNomePessoaCliente = (Literal)dvAbertura.FindControl("litNomePessoaCliente");
            HiddenField hdIdPessoaCliente = (HiddenField)dvAbertura.FindControl("hdIdPessoaCliente");

            if (txtIdPessoaCliente != null
                && litNomePessoaCliente != null
                && hdIdPessoaCliente != null)
            {
                hdIdPessoaCliente.Value = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
                txtIdPessoaCliente.Text = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
                litNomePessoaCliente.Text = ((GridView)e.CommandSource).SelectedDataKey["NomeCompletoRazaoSocial"].ToString();
            }

            mpeDialogSelecaoPessoa.Hide();
        }


        protected void FecharDialogSelecaoProcesso_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoProcesso.Hide();
        }

        protected void SelecionarDialogSelecaoProcesso_Click(object sender, GridViewCommandEventArgs e)
        {
            if (((GridView)e.CommandSource).SelectedDataKey["idProcesso"] != null)
            {
                string idProcesso = ((GridView)e.CommandSource).SelectedDataKey["idProcesso"].ToString();

                if ((Request.QueryString["ID"] != null &&
                    Request.QueryString["ID"] != String.Empty)
                    && idProcesso != String.Empty)
                {
                    dtoProcessoApenso processoApenso = new dtoProcessoApenso();

                    processoApenso.idProcesso = Convert.ToInt32(Request.QueryString["ID"]);
                    processoApenso.idProcessoVinculado = Convert.ToInt32(idProcesso);

                    bllProcessoApenso.Insert(processoApenso);
                }

                mpeDialogSelecaoProcesso.Hide();

                grdProcessoApenso.DataBind();

                upProcessoApenso.Update();

            }
        }

        public string RetornaHTMLPecaProcessual(object idProcessoAndamento)
        {
            StringBuilder sbHTML = new StringBuilder();

            if (idProcessoAndamento != null)
            {
                dtoProcessoPeca processoPeca = bllProcessoPeca.GetByProcessoAndamento(Convert.ToInt32(idProcessoAndamento));

                if (processoPeca != null)
                {
                    sbHTML.AppendFormat("<a href='{0}/{1}' target='_blank' title='Visualizar peça'>", ProJur.DataAccess.Configuracao.getEnderecoVirtualUpload(), processoPeca.caminhoArquivo);
                    sbHTML.AppendFormat("<img src='{0}/Images/anexo.png' alt='Visualizar peça' border='0' />", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite());
                    sbHTML.Append("</a>");
                }
            }

            return sbHTML.ToString();
        }

        protected void grdProcessoParte_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idProcessoParte = DataBinder.Eval(e.Row.DataItem, "idProcessoParte").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/ProcessoParte.aspx?ID=" + idProcessoParte;
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                //e.Row.Style["cursor"] = "pointer";
                for (int i = 0; i < e.Row.Cells.Count - 2; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
                }
            }
        }

        protected void grdProcessoAdvogado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idProcessoAdvogado = DataBinder.Eval(e.Row.DataItem, "idProcessoAdvogado").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/ProcessoAdvogado.aspx?ID=" + idProcessoAdvogado;
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                //e.Row.Style["cursor"] = "pointer";
                for (int i = 0; i < e.Row.Cells.Count - 2; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
                }
            }
        }

        protected void grdProcessoAndamento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idProcessoAndamento = DataBinder.Eval(e.Row.DataItem, "idProcessoAndamento").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/ProcessoAndamento.aspx?ID=" + idProcessoAndamento;
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                //e.Row.Style["cursor"] = "pointer";
                for (int i = 0; i < e.Row.Cells.Count - 2; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
                }
            }
        }

        protected void grdProcessoPeca_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idProcessoPeca = DataBinder.Eval(e.Row.DataItem, "idProcessoPeca").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/ProcessoPeca.aspx?ID=" + idProcessoPeca;
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                //e.Row.Style["cursor"] = "pointer";
                for (int i = 0; i < e.Row.Cells.Count - 2; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
                }
            }
        }

        protected void grdProcessoPrazo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idProcessoPrazo = DataBinder.Eval(e.Row.DataItem, "idProcessoPrazo").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/ProcessoPrazo.aspx?ID=" + idProcessoPrazo;
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                //e.Row.Style["cursor"] = "pointer";
                for (int i = 0; i < e.Row.Cells.Count - 2; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
                }
            }
        }


        protected void grdProcessoDespesa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idProcessoDespesa = DataBinder.Eval(e.Row.DataItem, "idProcessoDespesa").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/ProcessoDespesa.aspx?ID=" + idProcessoDespesa;
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                //e.Row.Style["cursor"] = "pointer";
                for (int i = 0; i < e.Row.Cells.Count - 2; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
                }
            }
        }

        protected void grdProcessoTerceiro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idProcessoTerceiro = DataBinder.Eval(e.Row.DataItem, "idProcessoTerceiro").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/ProcessoTerceiro.aspx?ID=" + idProcessoTerceiro;
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                //e.Row.Style["cursor"] = "pointer";
                for (int i = 0; i < e.Row.Cells.Count - 2; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
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