using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System;

namespace ProJur.WebApplication.Paginas.Manutencao
{

    public partial class Atendimento : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null
                    || Request.QueryString["ID"].Trim() == String.Empty)
                {
                    dvManutencao.ChangeMode(DetailsViewMode.Insert);

                    if (Request.QueryString["IDCLIENTE"] != null
                        && Request.QueryString["IDCLIENTE"].Trim() != String.Empty)
                    {
                        TextBox txtIdPessoaCliente = (TextBox)dvManutencao.FindControl("txtIdPessoaCliente");
                        Literal litNomePessoaCliente = (Literal)dvManutencao.FindControl("litNomePessoaCliente");
                        HiddenField hdIdPessoaCliente = (HiddenField)dvManutencao.FindControl("hdIdPessoaCliente");

                        if (txtIdPessoaCliente != null)
                        {
                            dtoPessoa cliente = bllPessoa.Get(Convert.ToInt32(Request.QueryString["IDCLIENTE"]));

                            hdIdPessoaCliente.Value = cliente.idPessoa.ToString();
                            txtIdPessoaCliente.Text = cliente.CPFCNPJ;
                            litNomePessoaCliente.Text = cliente.NomeCompletoRazaoSocial;
                        }
                    }

                    Literal litDataHoraInicio = (Literal)dvManutencao.FindControl("litDataHoraInicio");

                    litDataHoraInicio.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                }

                CarregarHistorico();
            }

            InicializaEventos();
            InicializaDefaultButton();

            if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
            {
                Master.litCaminhoPrincipal.Text = "Manutenção > ";
                Master.litCaminhoSecundario.Text = "Atendimento";
            }
            else
            {
                Master.litCaminhoPrincipal.Text = "Manutenção > Atendimento > ";
                Master.litCaminhoSecundario.Text = String.Format("{0}", bllPessoa.Get(bllAtendimento.Get(Convert.ToInt32(Request.QueryString["ID"])).idPessoaCliente).NomeCompletoRazaoSocial);

            }
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
                Response.Redirect(String.Format("{0}/Paginas/Cadastro/Atendimento.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvManutencao.CurrentMode == DetailsViewMode.Edit)
                {
                    dvManutencao.UpdateItem(false);
                }
                else
                    dvManutencao.InsertItem(true);
            }
            catch { }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvManutencao.DeleteItem();
            Response.Redirect(String.Format("{0}/Paginas/Cadastro/Atendimento.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void dsManutencao_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Response.Redirect(String.Format("{0}/Paginas/Manutencao/Atendimento.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), e.ReturnValue));
        }

        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);

            dialogSelecaoPessoa.FecharClickHandler += new EventHandler(FecharDialogSelecaoPessoa_Click);
            dialogSelecaoPessoa.SelecionarClickHandler += new GridViewCommandEventHandler(SelecionarDialogSelecaoPessoa_Click);
        }

        protected void FecharDialogSelecaoPessoa_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Hide();
        }

        protected void SelecionarDialogSelecaoPessoa_Click(object sender, GridViewCommandEventArgs e)
        {
            TextBox txtIdPessoaCliente = (TextBox)dvManutencao.FindControl("txtIdPessoaCliente");
            Literal litNomePessoaCliente = (Literal)dvManutencao.FindControl("litNomePessoaCliente");
            HiddenField hdIdPessoaCliente = (HiddenField)dvManutencao.FindControl("hdIdPessoaCliente");

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

        protected void btnPesquisarPessoaCliente_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Show();
        }

        public string FormatText(object input)
        {
            if (input != null
                && input.ToString() != String.Empty)
                return input.ToString().Replace("\n", "<br />");
            else
                return "";
        }

        protected void txtIdPessoaCliente_Click(object sender, EventArgs e)
        {
            CarregaPessoa();
            CarregarHistorico();
        }

        protected void CarregaPessoa()
        {
            TextBox txtIdPessoaCliente = (TextBox)dvManutencao.FindControl("txtIdPessoaCliente");
            Literal litNomePessoaCliente = (Literal)dvManutencao.FindControl("litNomePessoaCliente");
            HiddenField hdIdPessoaCliente = (HiddenField)dvManutencao.FindControl("hdIdPessoaCliente");

            dtoPessoa pessoa = bllPessoa.Get(Convert.ToInt32(txtIdPessoaCliente.Text));

            if (txtIdPessoaCliente != null && pessoa != null)
            {
                hdIdPessoaCliente.Value = pessoa.idPessoa.ToString();
                litNomePessoaCliente.Text = pessoa.NomeCompletoRazaoSocial;
            }
        }

        protected void dvManutencao_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            string idPessoaCliente = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaCliente")).Value;
            if (idPessoaCliente.Trim() != String.Empty)
                e.Values["idPessoaCliente"] = idPessoaCliente;

            if (dvManutencao.CurrentMode == DetailsViewMode.Insert)
            {
                e.Values["dataFimAtendimento"] = DateTime.Now;

                if (Session["IDUSUARIO"] != null
                    && Session["IDUSUARIO"].ToString() != String.Empty)
                    e.Values["idUsuario"] = Convert.ToInt32(Session["IDUSUARIO"].ToString());
            }


        }

        protected void dvManutencao_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            string idPessoaCliente = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaCliente")).Value;
            if (idPessoaCliente.Trim() != String.Empty)
                e.NewValues["idPessoaCliente"] = idPessoaCliente;

            e.NewValues["dataFimAtendimento"] = e.OldValues["dataFimAtendimento"];

            if (Session["IDUSUARIO"] != null
                && Session["IDUSUARIO"].ToString() != String.Empty)
            {
                if (!bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString())).Administrador)
                    e.NewValues["idUsuario"] = e.OldValues["idUsuario"];
            }

        }

        public void CarregarHistorico()
        {
            string idPessoa = String.Empty;
            string idIgnorado = String.Empty;

            if (Request.QueryString["ID"] != null
                && Request.QueryString["ID"].ToString() != String.Empty)
            {
                idIgnorado = Request.QueryString["ID"].ToString();

                if (dvManutencao.CurrentMode == DetailsViewMode.ReadOnly)
                    idPessoa = bllAtendimento.Get(Convert.ToInt32(idIgnorado)).idPessoaCliente.ToString();
                else
                    if ((HiddenField)dvManutencao.FindControl("hdIdPessoaCliente") != null)
                        idPessoa = ((HiddenField)dvManutencao.FindControl("hdIdPessoaCliente")).Value;
            }
            else
                idPessoa = ((HiddenField)dvManutencao.FindControl("hdIdPessoaCliente")).Value;

            if (dvManutencao.CurrentMode == DetailsViewMode.Insert
                && idPessoa == String.Empty)
            {
                idPessoa = "-1";
            }

            dsHistorico.SelectMethod = "GetHistorico";
            dsHistorico.SelectParameters.Clear();
            dsHistorico.SelectParameters.Add("idCliente", idPessoa);
            dsHistorico.SelectParameters.Add("idIgnorado", idIgnorado);
            dsHistorico.SelectParameters.Add("SortExpression", "dataInicioAtendimento ASC");

            dsHistorico.DataBind();
            upHistorico.Update();
        }

        protected void dvManutencao_DataBound(object sender, EventArgs e)
        {
            if (dvManutencao.CurrentMode == DetailsViewMode.Edit)
            {
                DropDownList ddlUsuario = ((DropDownList)((DetailsView)sender).FindControl("ddlUsuario"));
                Literal litUsuario = ((Literal)((DetailsView)sender).FindControl("litUsuario"));

                if (Session["IDUSUARIO"] != null
                    && Session["IDUSUARIO"].ToString() != String.Empty)
                {
                    dtoUsuario Usuario = bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()));
                    dtoUsuarioPermissao Permissao = bllUsuarioPermissao.Get(Usuario.idUsuario, "atendimento");

                    if (bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString())).Administrador || Permissao.Especial)
                    {
                        ddlUsuario.Visible = true;
                        litUsuario.Visible = false;
                    }
                    else
                    {
                        ddlUsuario.Visible = false;
                        litUsuario.Visible = true;
                    }

                }
            }
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$menuAcoes$btnGravar";
        }

        //protected void lnkAgendarCompromisso_Click(object sender, EventArgs e)
        //{
        //    txtIdPessoaCliente

        //    if (dvManutencao.CurrentMode == DetailsViewMode.ReadOnly)
        //    {
        //        if (Request.QueryString["ID"] != null
        //            && Request.QueryString["ID"].ToString() != String.Empty)
        //        {
        //            dtoAgendaCompromisso compromisso = bllAgendaCompromisso.Get(Convert.ToInt32(Request.QueryString["ID"]));
        //            LinkButton lnkPendenteConcluido = (LinkButton)sender;

        //            if (lnkPendenteConcluido != null)
        //            {
        //                if (lnkPendenteConcluido.Text == " - (<u>Concluir compromisso</u>)")
        //                    compromisso.situacaoCompromisso = "C";
        //                else
        //                    compromisso.situacaoCompromisso = "P";

        //                bllAgendaCompromisso.Update(compromisso);

        //                dvManutencao.DataBind();
        //            }
        //        }

        //    }
        //}

    }
}