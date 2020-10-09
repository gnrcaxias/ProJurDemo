using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;

namespace ProJur.WebApplication.Paginas.Manutencao
{
    public partial class AgendaCompromisso : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null
                    || Request.QueryString["ID"].Trim() == String.Empty)
                    dvManutencao.ChangeMode(DetailsViewMode.Insert);
            }

            ConfiguraParticipantes();
            InicializaEventos();
            InicializaDefaultButton();

            //if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
            //{
            //    Master.litCaminhoPrincipal.Text = "Operacional > Manutenção > ";
            //    Master.litCaminhoSecundario.Text = "Agenda";
            //}
            //else
            //{
            //    Master.litCaminhoPrincipal.Text = "Operacional > Manutenção > ";
            //    Master.litCaminhoSecundario.Text = String.Format("Agenda: {0}", bllPessoa.Get(bllAgendaCompromisso.Get(Convert.ToInt32(Request.QueryString["ID"])). ).NomeCompletoRazaoSocial);

            //}

            Master.litCaminhoPrincipal.Text = "Manutenção > ";
            Master.litCaminhoSecundario.Text = "Agenda";
        }
        
        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$menuAcoes$btnGravar";
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            dvManutencao.ChangeMode(DetailsViewMode.Edit);
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null
                && Request.QueryString["ID"].Trim() != String.Empty)
                dvManutencao.ChangeMode(DetailsViewMode.ReadOnly);
            else
                Response.Redirect(String.Format("{0}/Paginas/Cadastro/Agenda.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvManutencao.CurrentMode == DetailsViewMode.Edit)
                    dvManutencao.UpdateItem(false);
                else
                    dvManutencao.InsertItem(true);
            }
            catch { }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvManutencao.DeleteItem();
            Response.Redirect(String.Format("{0}/Paginas/Cadastro/Agenda.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }


        protected void dsManutencao_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Response.Redirect(String.Format("{0}/Paginas/Manutencao/AgendaCompromisso.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), e.ReturnValue));
        }


        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);

            dialogSelecaoUsuario.ConfirmarClickHandler += new EventHandler(ConfirmarDialogSelecaoUsuario_Click);
        }


        protected void btnAdicionarAgendaUsuario_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoUsuario.Show();
        }

        protected void ConfirmarDialogSelecaoUsuario_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != String.Empty)
            {
                if (dialogSelecaoUsuario.ddlUsuario.SelectedValue != null
                    && dialogSelecaoUsuario.ddlUsuario.SelectedValue != String.Empty)
                {
                    int idUsuarioSelecionado = Convert.ToInt32(dialogSelecaoUsuario.ddlUsuario.SelectedValue);
                    int idAgendaCompromisso = 0;

                    idAgendaCompromisso = Convert.ToInt32(Request.QueryString["ID"].ToString());

                    InserirNovoParticipante(idUsuarioSelecionado, idAgendaCompromisso);

                    grdAgendaUsuario.DataBind();
                }
            }
        }


        private void InserirNovoParticipante(int idUsuario, int idAgendaCompromisso)
        {
            dtoAgendaUsuario agendaUsuario = new dtoAgendaUsuario();

            agendaUsuario.idAgendaCompromisso = idAgendaCompromisso;
            agendaUsuario.idUsuario = idUsuario; 

            bllAgendaUsuario.Insert(agendaUsuario);
        }


        protected void dvManutencao_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            string idPessoa = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoa")).Value;

            if (idPessoa.Trim() != String.Empty)
                e.Values["idPessoa"] = idPessoa;

            e.Values["idUsuarioCadastro"] = DataAccess.Sessions.IdUsuarioLogado;
        }

        protected void dvManutencao_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            string idPessoa = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoa")).Value;

            if (idPessoa.Trim() != String.Empty)
                e.NewValues["idPessoa"] = idPessoa;

            e.NewValues["idUsuarioUltimaAlteracao"] = DataAccess.Sessions.IdUsuarioLogado;
        }


        protected void dvManutencao_DataBound(object sender, EventArgs e)
        {
            LinkButton lnkPendenteConcluido = (LinkButton)dvManutencao.FindControl("lnkPendenteConcluido");

            if (dvManutencao.CurrentMode == DetailsViewMode.ReadOnly)
            {
                if (Request.QueryString["ID"] != null
                    && Request.QueryString["ID"].ToString() != String.Empty)
                {
                    if (lnkPendenteConcluido != null)
                    {
                        dtoAgendaCompromisso compromisso = bllAgendaCompromisso.Get(Convert.ToInt32(Request.QueryString["ID"]));

                        lnkPendenteConcluido.Visible = true;

                        if (compromisso.situacaoCompromisso == "P")
                            lnkPendenteConcluido.Text = " - (<u>Concluir compromisso</u>)";
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


        protected void lnkPendenteConcluido_Click(object sender, EventArgs e)
        {
            if (dvManutencao.CurrentMode == DetailsViewMode.ReadOnly)
            {
                if (Request.QueryString["ID"] != null
                    && Request.QueryString["ID"].ToString() != String.Empty)
                {
                    dtoAgendaCompromisso compromisso = bllAgendaCompromisso.Get(Convert.ToInt32(Request.QueryString["ID"]));
                    LinkButton lnkPendenteConcluido = (LinkButton)sender;

                    if (lnkPendenteConcluido != null)
                    {
                        if (lnkPendenteConcluido.Text == " - (<u>Concluir compromisso</u>)")
                            compromisso.situacaoCompromisso = "C";
                        else
                            compromisso.situacaoCompromisso = "P";

                        bllAgendaCompromisso.Update(compromisso);

                        dvManutencao.DataBind();
                    }
                }

            }
        }


        public string FormatText(object input)
        {
            if (input != null
                && input.ToString() != String.Empty)
                return input.ToString().Replace("\n", "<br />");
            else
                return "";
        }


        protected void FecharDialogSelecaoPessoa_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Hide();
        }

        protected void SelecionarDialogSelecaoPessoa_Click(object sender, GridViewCommandEventArgs e)
        {
            TextBox txtIdPessoa = (TextBox)dvManutencao.FindControl("txtIdPessoa");
            Literal litNomePessoa = (Literal)dvManutencao.FindControl("litNomePessoa");
            HiddenField hdIdPessoa = (HiddenField)dvManutencao.FindControl("hdIdPessoa");

            if (txtIdPessoa != null
                && litNomePessoa != null
                && hdIdPessoa != null)
            {
                hdIdPessoa.Value = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
                txtIdPessoa.Text = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
                litNomePessoa.Text = ((GridView)e.CommandSource).SelectedDataKey["NomeCompletoRazaoSocial"].ToString();
            }

            mpeDialogSelecaoPessoa.Hide();
        }

        protected void btnPesquisarPessoa_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Show();
        }

        protected void txtIdPessoa_Click(object sender, EventArgs e)
        {
            CarregaPessoa();
        }

        protected void CarregaPessoa()
        {
            TextBox txtIdPessoa = (TextBox)dvManutencao.FindControl("txtIdPessoa");
            Literal litNomePessoa = (Literal)dvManutencao.FindControl("litNomePessoa");
            HiddenField hdIdPessoa = (HiddenField)dvManutencao.FindControl("hdIdPessoa");

            dtoPessoa pessoa = bllPessoa.Get(Convert.ToInt32(txtIdPessoa.Text));

            if (txtIdPessoa != null && pessoa != null)
            {
                hdIdPessoa.Value = pessoa.idPessoa.ToString();
                litNomePessoa.Text = pessoa.NomeCompletoRazaoSocial;
            }
        }

        protected void ConfiguraParticipantes()
        {
            if (Request.QueryString["ID"] != null
                && Request.QueryString["ID"].ToString() != String.Empty)
                pnlAgendaUsuario.Visible = true;
            else
                pnlAgendaUsuario.Visible = false;
        }


    }
}