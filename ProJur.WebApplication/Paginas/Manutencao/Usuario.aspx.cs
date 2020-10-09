using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System.Web.Security;

namespace ProJur.WebApplication.Paginas.Manutencao
{
    public partial class Usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
                    dvManutencao.ChangeMode(DetailsViewMode.Insert);
            }

            InicializaEventos();
            InicializaDefaultButton();

            Master.litCaminhoPrincipal.Text = "Manutenção > ";
            Master.litCaminhoSecundario.Text = "Usuário";
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            dvManutencao.ChangeMode(DetailsViewMode.Edit);
            grdPermissoes.DataBind();
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null
                && Request.QueryString["ID"].Trim() != String.Empty)
            {
                dvManutencao.ChangeMode(DetailsViewMode.ReadOnly);
                grdPermissoes.DataBind();
            }
            else
                Response.Redirect(String.Format("{0}/Paginas/Cadastro/Usuario.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvManutencao.CurrentMode == DetailsViewMode.Edit)
                {
                    dvManutencao.UpdateItem(false);

                    SalvarPermissoes(Convert.ToInt32(Request.QueryString["ID"]));
                }
                else
                    dvManutencao.InsertItem(true);

                grdPermissoes.DataBind();
            }
            catch { }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvManutencao.DeleteItem();


            Response.Redirect(String.Format("{0}/Paginas/Cadastro/Usuario.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void dsManutencao_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            SalvarPermissoes(Convert.ToInt32(e.ReturnValue));

            Response.Redirect(String.Format("{0}/Paginas/Manutencao/Usuario.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), e.ReturnValue));
        }

        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);

            dialogNovaSenha.ConfirmarClickHandler += new EventHandler(ConfirmarNovaSenha_Click);

            dialogSelecaoPessoa.FecharClickHandler += new EventHandler(FecharDialogSelecaoPessoa_Click);
            dialogSelecaoPessoa.SelecionarClickHandler += new GridViewCommandEventHandler(SelecionarDialogSelecaoPessoa_Click);
        }

        protected void ConfirmarNovaSenha_Click(object sender, EventArgs e)
        {
            if (dialogNovaSenha.txtNovaSenha.Text == dialogNovaSenha.txtConfirmacaoNovaSenha.Text)
            {
                if (bllUsuario.ChangePassword(Convert.ToInt32(Request.QueryString["ID"]), dialogNovaSenha.txtNovaSenha.Text))
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Senha sucesso", "alert('Senha alterada com sucesso!');", true);
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Senha erro", "alert('Falha ao alterar senha, tente novamente!');", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Senha erro", "alert('Falha ao alterar senha, tente novamente!');", true);
        }

        private void SalvarPermissoes(int idUsuario)
        {
            for (int i = 0; i < grdPermissoes.Rows.Count; i++)
            {
                grdPermissoes.SelectedIndex = i;

                dtoUsuarioPermissao Permissao = new dtoUsuarioPermissao();
                GridViewRow linhaAtual = grdPermissoes.SelectedRow;

                CheckBox chkAlterar = (CheckBox)linhaAtual.FindControl("chkAlterar");
                CheckBox chkEspecial = (CheckBox)linhaAtual.FindControl("chkEspecial");
                CheckBox chkExcluir = (CheckBox)linhaAtual.FindControl("chkExcluir");
                CheckBox chkExibir = (CheckBox)linhaAtual.FindControl("chkExibir");
                CheckBox chkImprimir = (CheckBox)linhaAtual.FindControl("chkImprimir");
                CheckBox chkNovo = (CheckBox)linhaAtual.FindControl("chkNovo");
                CheckBox chkPesquisar = (CheckBox)linhaAtual.FindControl("chkPesquisar");
                DropDownList ddlModerador = (DropDownList)linhaAtual.FindControl("ddlModerador");

                Permissao.idMenu = Convert.ToInt32(grdPermissoes.SelectedDataKey["idMenu"].ToString());
                Permissao.idPermissao = Convert.ToInt32(grdPermissoes.SelectedDataKey["idPermissao"].ToString());
                Permissao.idUsuario = idUsuario;
                Permissao.idUsuarioModerador = Convert.ToInt32(grdPermissoes.SelectedDataKey["idUsuarioModerador"].ToString());

                Permissao.Alterar = chkAlterar.Checked;
                Permissao.Especial = chkEspecial.Checked;
                Permissao.Excluir = chkExcluir.Checked;
                Permissao.Exibir = chkExibir.Checked;
                Permissao.Imprimir = chkImprimir.Checked;
                Permissao.Novo = chkNovo.Checked;
                Permissao.Pesquisar = chkPesquisar.Checked;
                Permissao.idUsuarioModerador = Convert.ToInt32(ddlModerador.SelectedValue);

                if (Permissao.idPermissao > 0)
                    bllUsuarioPermissao.Update(Permissao);
                else
                    bllUsuarioPermissao.Insert(Permissao);
            }
        }

        protected void btnPesquisarPessoaVinculada_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Show();
        }

        protected void FecharDialogSelecaoPessoa_Click(object sender, EventArgs e)
        {
            mpeDialogSelecaoPessoa.Hide();
        }

        protected void SelecionarDialogSelecaoPessoa_Click(object sender, GridViewCommandEventArgs e)
        {
            TextBox txtIdPessoaVinculada = (TextBox)dvManutencao.FindControl("txtIdPessoaVinculada");
            Literal litNomePessoaVinculada = (Literal)dvManutencao.FindControl("litNomePessoaVinculada");
            HiddenField hdIdPessoaVinculada = (HiddenField)dvManutencao.FindControl("hdIdPessoaVinculada");

            if (txtIdPessoaVinculada != null
                && litNomePessoaVinculada != null
                && hdIdPessoaVinculada != null)
            {
                hdIdPessoaVinculada.Value = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
                txtIdPessoaVinculada.Text = ((GridView)e.CommandSource).SelectedDataKey["idPessoa"].ToString();
                litNomePessoaVinculada.Text = ((GridView)e.CommandSource).SelectedDataKey["NomeCompletoRazaoSocial"].ToString();
            }

            mpeDialogSelecaoPessoa.Hide();
        }

        protected void dvManutencao_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            string idPessoaVinculada = ((TextBox)((DetailsView)sender).FindControl("txtIdPessoaVinculada")).Text;
            if (idPessoaVinculada.Trim() != String.Empty)
                e.Values["idPessoaVinculada"] = idPessoaVinculada;
        }

        protected void dvManutencao_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            string idPessoaVinculada = ((TextBox)((DetailsView)sender).FindControl("txtIdPessoaVinculada")).Text;

            if (idPessoaVinculada.Trim() != String.Empty)
                e.NewValues["idPessoaVinculada"] = idPessoaVinculada;
            else
                e.NewValues["idPessoaVinculada"] = 0;
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$menuAcoes$btnGravar";
        }

    }
}