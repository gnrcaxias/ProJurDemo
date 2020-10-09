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
    public partial class Tarefa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null
                    || Request.QueryString["ID"].Trim() == String.Empty)
                    dvManutencao.ChangeMode(DetailsViewMode.Insert);
            }

            InicializaEventos();
            InicializaDefaultButton();
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            dvManutencao.ChangeMode(DetailsViewMode.Edit);
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null
                && Request.QueryString["ID"].Trim() != String.Empty)
            {
                dvManutencao.ChangeMode(DetailsViewMode.ReadOnly);
            }
            else
                Response.Redirect(String.Format("{0}/Paginas/Cadastro/Tarefa.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
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
            Response.Redirect(String.Format("{0}/Paginas/Cadastro/Tarefa.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void dsManutencao_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Response.Redirect(String.Format("{0}/Paginas/Manutencao/Tarefa.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), e.ReturnValue));
        }

        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);
        }


        protected void dvManutencao_DataBound(object sender, EventArgs e)
        {
            if (dvManutencao.CurrentMode == DetailsViewMode.Edit)
            {
                DropDownList ddlUsuarioResponsavel = ((DropDownList)((DetailsView)sender).FindControl("ddlUsuarioResponsavel"));
                Literal litUsuarioResponsavel = ((Literal)((DetailsView)sender).FindControl("litUsuarioResponsavel"));

                if (Session["IDUSUARIO"] != null
                    && Session["IDUSUARIO"].ToString() != String.Empty)
                {
                    dtoUsuario Usuario = bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()));
                    dtoUsuarioPermissao Permissao = bllUsuarioPermissao.Get(Usuario.idUsuario, "atendimento");

                    if (bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString())).Administrador)
                    {
                        ddlUsuarioResponsavel.Visible = true;
                        litUsuarioResponsavel.Visible = false;
                    }
                    else
                    {
                        ddlUsuarioResponsavel.Visible = false;
                        litUsuarioResponsavel.Visible = true;
                    }

                }
            }
        }

        protected void dvManutencao_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            //string idPessoaCliente = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaCliente")).Value;
            //if (idPessoaCliente.Trim() != String.Empty)
            //    e.Values["idPessoaCliente"] = idPessoaCliente;

            if (dvManutencao.CurrentMode == DetailsViewMode.Insert)
            {
                if (Session["IDUSUARIO"] != null
                    && Session["IDUSUARIO"].ToString() != String.Empty)
                    e.Values["idUsuarioResponsavel"] = Convert.ToInt32(Session["IDUSUARIO"].ToString());
            }
        }

        protected void dvManutencao_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            //string idPessoaCliente = ((HiddenField)((DetailsView)sender).FindControl("hdIdPessoaCliente")).Value;
            //if (idPessoaCliente.Trim() != String.Empty)
            //    e.NewValues["idPessoaCliente"] = idPessoaCliente;

            if (Session["IDUSUARIO"] != null
                && Session["IDUSUARIO"].ToString() != String.Empty)
            {
                if (!bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString())).Administrador)
                    e.NewValues["idUsuarioResponsavel"] = e.OldValues["idUsuario"];
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