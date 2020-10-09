using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System.Text;

namespace ProJur.WebApplication.Paginas.Manutencao
{
    public partial class ProcessoPrazo : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializaEventos();
            InicializaDefaultButton();

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
                {
                    ConfiguraModoCRUD(DetailsViewMode.Insert);

                    if (Request.QueryString["IdProcesso"] != null
                        && Request.QueryString["IdProcesso"] != String.Empty)
                        CarregaProcesso();
                }
                else
                {
                    dtoProcessoPrazo processoPrazo = bllProcessoPrazo.Get(Convert.ToInt32(Request.QueryString["ID"]));

                    if (processoPrazo != null && processoPrazo.idProcessoPrazo != 0)
                        ConfiguraModoCRUD(DetailsViewMode.ReadOnly);
                }
            }

            ConfiguraPrazoResponsaveis();

            Master.litCaminhoPrincipal.Text = "Manutenção > ";
            Master.litCaminhoSecundario.Text = "Processo > Prazo";
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
                if (Request.QueryString["IdProcesso"] != null 
                    && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                    Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
                else if (Request.QueryString["Origem"] != null 
                    && Request.QueryString["Origem"].Trim() != String.Empty
                    && Request.QueryString["Origem"].Trim() == "Agenda")
                    Response.Redirect(String.Format("{0}/Paginas/Cadastro/Agenda.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
            }
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (dvProcessoPrazo.CurrentMode == DetailsViewMode.Edit)
                        dvProcessoPrazo.UpdateItem(false);
                    else if (dvProcessoPrazo.CurrentMode == DetailsViewMode.Insert)
                        dvProcessoPrazo.InsertItem(true);
                }
                catch (Exception Ex)
                { }
            }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvProcessoPrazo.DeleteItem();

            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
            else if (Request.QueryString["Origem"] != null
                && Request.QueryString["Origem"].Trim() != String.Empty
                && Request.QueryString["Origem"].Trim() == "Agenda")
                Response.Redirect(String.Format("{0}/Paginas/Cadastro/Agenda.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }



        private void CarregaProcesso()
        {
            TextBox txtIdProcesso = (TextBox)dvProcessoPrazo.FindControl("txtIdProcesso");
            Literal litNumeroProcesso = (Literal)dvProcessoPrazo.FindControl("litNumeroProcesso");
            HiddenField hdIdProcesso = (HiddenField)dvProcessoPrazo.FindControl("hdIdProcesso");

            if (txtIdProcesso != null
                && litNumeroProcesso != null
                && hdIdProcesso != null
                && Request.QueryString["IdProcesso"] != null)
            {
                hdIdProcesso.Value = Request.QueryString["IdProcesso"];
                txtIdProcesso.Text = Request.QueryString["IdProcesso"];
                litNumeroProcesso.Text = bllProcesso.Get(Convert.ToInt32(Request.QueryString["IdProcesso"])).numeroProcesso;
            }
        }

        private void ConfiguraModoCRUD(DetailsViewMode novoModo)
        {
            dvProcessoPrazo.ChangeMode(novoModo);
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

            if (Request.QueryString["Origem"] != null 
                    && Request.QueryString["Origem"].Trim() != String.Empty
                    && Request.QueryString["Origem"].Trim() == "Agenda")
                menuAcoes.VoltarUrl = String.Format("{0}/Paginas/Cadastro/Agenda.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite());
            else
                menuAcoes.VoltarUrl = String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]);
        }



        protected void dsProcessoPrazo_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            //if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
            //    Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
            //else if (Request.QueryString["Origem"] != null
            //    && Request.QueryString["Origem"].Trim() != String.Empty
            //    && Request.QueryString["Origem"].Trim() == "Agenda")
            //    Response.Redirect(String.Format("{0}/Paginas/Cadastro/Agenda.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));

            StringBuilder sbQueryString = new StringBuilder();

            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                sbQueryString.AppendFormat("&IdProcesso={0}", Request.QueryString["IdProcesso"]);

            //if (sbQueryString.ToString() != String.Empty)
            //    sbQueryString.Append("&");

            if (Request.QueryString["Origem"] != null && Request.QueryString["Origem"].Trim() != String.Empty)
                sbQueryString.AppendFormat("&Origem={0}", Request.QueryString["Origem"]);

            Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}{2}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), e.ReturnValue, sbQueryString.ToString()));
        }

        protected void dvProcessoPrazo_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            //string idPessoa = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaAdvogado")).Value;
            //if (idPessoa.Trim() != String.Empty)
            //    e.Values["idPessoaAdvogadoResponsavel"] = idPessoa;

            string idProcesso = ((HiddenField)((DetailsView)sender).FindControl("hdIdProcesso")).Value;
            if (idProcesso.Trim() != String.Empty)
                e.Values["idProcesso"] = idProcesso;

            e.Values["idUsuarioCadastro"] = DataAccess.Sessions.IdUsuarioLogado;

            //if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
            //    e.Values["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

            // DESATIVADO POR SOLICITAÇÃO DA EPC, PREFEREM DEIXAR MANUAL
            //if (e.Values["idTipoPrazo"] != null
            //    && e.Values["idTipoPrazo"].ToString() != String.Empty)
            //{
            //    int quantidadeDiasPrazo = bllTipoPrazoProcessual.Get(Convert.ToInt32(e.Values["idTipoPrazo"].ToString())).quantidadeDiasPrazo;

            //    if (e.Values["dataPublicacao"] != null
            //        && e.Values["dataPublicacao"].ToString() != String.Empty)
            //    {
            //        DateTime dataVencimento = Convert.ToDateTime(e.Values["dataPublicacao"].ToString()).AddDays(quantidadeDiasPrazo);
            //        e.Values["dataVencimento"] = dataVencimento;
            //    }
            //}
        }

        protected void dvProcessoPrazo_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            //string idPessoa = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaAdvogado")).Value;
            //if (idPessoa.Trim() != String.Empty)
            //    e.NewValues["idPessoaAdvogadoResponsavel"] = idPessoa;

            string idProcesso = ((HiddenField)((DetailsView)sender).FindControl("hdIdProcesso")).Value;
            if (idProcesso.Trim() != String.Empty)
                e.NewValues["idProcesso"] = idProcesso;

            e.NewValues["idUsuarioUltimaAlteracao"] = DataAccess.Sessions.IdUsuarioLogado;

            //if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
            //    e.NewValues["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

            // DESATIVADO POR SOLICITAÇÃO DA EPC, PREFEREM DEIXAR MANUAL
            //if (e.NewValues["idTipoPrazo"] != null
            //    && e.NewValues["idTipoPrazo"].ToString() != String.Empty)
            //{
            //    int quantidadeDiasPrazo = bllTipoPrazoProcessual.Get(Convert.ToInt32(e.NewValues["idTipoPrazo"].ToString())).quantidadeDiasPrazo;

            //    if (e.NewValues["dataPublicacao"] != null
            //        && e.NewValues["dataPublicacao"].ToString() != String.Empty)
            //    {
            //        DateTime dataVencimento = Convert.ToDateTime(e.NewValues["dataPublicacao"].ToString()).AddDays(quantidadeDiasPrazo);
            //        e.NewValues["dataVencimento"] = dataVencimento;
            //    }
            //}
        }



        protected void SelecionarDialogSelecaoPessoa_Click(object sender, GridViewCommandEventArgs e)
        {
            //TextBox txtIdPessoaAdvogado = (TextBox)dvProcessoPrazo.FindControl("txtIdPessoaAdvogado");
            //Literal litNomePessoaAdvogado = (Literal)dvProcessoPrazo.FindControl("litNomePessoaAdvogado");
            //HiddenField hdIdPessoaAdvogado = (HiddenField)dvProcessoPrazo.FindControl("hdIdPessoaAdvogado");

            //if (txtIdPessoaAdvogado != null
            //    && litNomePessoaAdvogado != null
            //    && hdIdPessoaAdvogado != null)
            //{
            //    hdIdPessoaAdvogado.Value = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
            //    txtIdPessoaAdvogado.Text = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
            //    litNomePessoaAdvogado.Text = ((GridView)e.CommandSource).SelectedDataKey["NomeCompletoRazaoSocial"].ToString();
            //}

            //mpeDialogSelecaoPessoa.Hide();


            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != String.Empty
                && ((GridView)e.CommandSource).SelectedDataKey["idPessoa"] != null)
            {
                int idPessoaSelecionada = Convert.ToInt32(((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString());
                int idAgendaCompromisso = Convert.ToInt32(Request.QueryString["ID"].ToString());

                if (idPessoaSelecionada > 0 && idAgendaCompromisso > 0)
                {
                    InserirNovoResponsavel(idPessoaSelecionada, idAgendaCompromisso);

                    grdPrazoResponsavel.DataBind();
                    upPrazoResponsavel.Update();
                    mpeDialogSelecaoPessoa.Hide();
                }
            }
        }


        private void InserirNovoResponsavel(int idPessoa, int idProcessoPrazo)
        {
            if (!bllProcessoPrazoResponsavel.Exists(idProcessoPrazo, idPessoa))
            { 
                dtoProcessoPrazoResponsavel processoPrazoResponsavel = new dtoProcessoPrazoResponsavel();

                processoPrazoResponsavel.idProcessoPrazo = idProcessoPrazo;
                processoPrazoResponsavel.idPessoa = idPessoa;

                bllProcessoPrazoResponsavel.Insert(processoPrazoResponsavel);
            }
        }


        protected void FecharDialogSelecaoPessoa_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Hide();
        }

        protected void btnAdicionarResponsavel_Click(object sender, EventArgs e)
        {
            dialogSelecaoPessoa.tipoPessoaAdvogado = "1";
            dialogSelecaoPessoa.tipoPessoaColaborador = "1";

            dialogSelecaoPessoa.CarregarPesquisa();

            mpeDialogSelecaoPessoa.Show();
        }

        protected void btnPesquisarProcesso_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoProcesso.Show();
        }

        protected void FecharDialogSelecaoProcesso_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoProcesso.Hide();
        }

        protected void SelecionarDialogSelecaoProcesso_Click(object sender, GridViewCommandEventArgs e)
        {
            TextBox txtIdProcesso = (TextBox)dvProcessoPrazo.FindControl("txtIdProcesso");
            Literal litNumeroProcesso = (Literal)dvProcessoPrazo.FindControl("litNumeroProcesso");
            HiddenField hdIdProcesso = (HiddenField)dvProcessoPrazo.FindControl("hdIdProcesso");

            if (txtIdProcesso != null
                && litNumeroProcesso != null
                && hdIdProcesso != null)
            {
                hdIdProcesso.Value = ((GridView)e.CommandSource).SelectedDataKey["idProcesso"].ToString();
                txtIdProcesso.Text = ((GridView)e.CommandSource).SelectedDataKey["idProcesso"].ToString();
                litNumeroProcesso.Text = ((GridView)e.CommandSource).SelectedDataKey["numeroProcesso"].ToString();
            }

            mpeDialogSelecaoProcesso.Hide();

            //if (((GridView)e.CommandSource).SelectedDataKey["idProcesso"] != null)
            //{
            //    string idProcesso = ((GridView)e.CommandSource).SelectedDataKey["idProcesso"].ToString();

            //    if ((Request.QueryString["ID"] != null &&
            //        Request.QueryString["ID"] != String.Empty)
            //        && idProcesso != String.Empty)
            //    {
            //        dtoProcessoApenso processoApenso = new dtoProcessoApenso();

            //        processoApenso.idProcesso = Convert.ToInt32(Request.QueryString["ID"]);
            //        processoApenso.idProcessoVinculado = Convert.ToInt32(idProcesso);

            //        bllProcessoApenso.Insert(processoApenso);
            //    }

            //    mpeDialogSelecaoProcesso.Hide();

            //    grdProcessoApenso.DataBind();

            //    upProcessoApenso.Update();

            //}
        }

        protected void lnkPendenteConcluido_Click(object sender, EventArgs e)
        {
            if (dvProcessoPrazo.CurrentMode == DetailsViewMode.ReadOnly)
            {
                if (Request.QueryString["ID"] != null
                    && Request.QueryString["ID"].ToString() != String.Empty)
                {
                    dtoProcessoPrazo prazo = bllProcessoPrazo.Get(Convert.ToInt32(Request.QueryString["ID"]));
                    LinkButton lnkPendenteConcluido = (LinkButton)sender;

                    if (lnkPendenteConcluido != null)
                    {
                        if (lnkPendenteConcluido.Text == " - (<u>Concluir prazo</u>)")
                        {
                            prazo.dataConclusao = DateTime.Now;
                            prazo.situacaoPrazo = "C";
                        }
                        else
                        { 
                            prazo.situacaoPrazo = "P";
                            prazo.dataConclusao = null;
                        }

                        bllProcessoPrazo.Update(prazo);

                        dvProcessoPrazo.DataBind();
                    }
                }

            }
        }

        protected void dvProcessoPrazo_DataBound(object sender, EventArgs e)
        {
            LinkButton lnkPendenteConcluido = (LinkButton)dvProcessoPrazo.FindControl("lnkPendenteConcluido");

            if (dvProcessoPrazo.CurrentMode == DetailsViewMode.ReadOnly)
            {
                if (Request.QueryString["ID"] != null
                    && Request.QueryString["ID"].ToString() != String.Empty)
                {
                    if (lnkPendenteConcluido != null)
                    {
                        dtoProcessoPrazo prazo = bllProcessoPrazo.Get(Convert.ToInt32(Request.QueryString["ID"]));

                        lnkPendenteConcluido.Visible = true;

                        if (prazo.situacaoPrazo == "P")
                            lnkPendenteConcluido.Text = " - (<u>Concluir prazo</u>)";
                        else
                            lnkPendenteConcluido.Text = " - (<u>Tornar pendente</u>)";
                    }
                }
            }
            else
            {
                if (lnkPendenteConcluido != null)
                    lnkPendenteConcluido.Visible = false;
            }
        }

        protected string FormatarDataHora(object Valor, string Formato)
        {
            string Retorno = String.Empty;

            if (Valor != null)
            {
                DateTime dataHora = Convert.ToDateTime(Valor);
                Retorno = dataHora.ToString(Formato);
            }

            return Retorno;
        }

        public string FormatText(object input)
        {
            if (input != null
                && input.ToString() != String.Empty)
                return input.ToString().Replace("\n", "<br />");
            else
                return "";
        }

        protected void ConfiguraPrazoResponsaveis()
        {
            if (Request.QueryString["ID"] != null
                && Request.QueryString["ID"].ToString() != String.Empty)
                pnlPrazoResponsavel.Visible = true;
            else
                pnlPrazoResponsavel.Visible = false;
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$menuAcoes$btnGravar";
        }

    }
}