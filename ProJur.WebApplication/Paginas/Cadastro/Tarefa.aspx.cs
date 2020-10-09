using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;

namespace ProJur.WebApplication.Paginas.Cadastro
{
    public partial class Tarefa : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                InicializaCadastro();

            InicializaEventos();
            InicializaDefaultButton();
        }


        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = btnPesquisar.UniqueID;
        }

        protected void InicializaCadastro()
        {
            if (Session["IDUSUARIO"] != null
                && Session["IDUSUARIO"].ToString() != String.Empty)
            {
                dtoUsuario Usuario = bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()));
                dtoUsuarioPermissao permissao = bllUsuarioPermissao.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()), "atendimento");

                if (!bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString())).Administrador)
                    divFiltroUsuario.Visible = false;
            }

            grdResultado.Sort("idTarefa", SortDirection.Ascending);

            CarregaPesquisa();
        }


        protected void btnExcluirSelecionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdResultado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkExcluir = (CheckBox)row.FindControl("chkExcluir");

                    HiddenField hdIdTarefa = (HiddenField)row.FindControl("hdIdTarefa");

                    dtoTarefa Tarefa = bllTarefa.Get(Convert.ToInt32(hdIdTarefa.Value));

                    if (chkExcluir.Checked && Tarefa != null)
                        bllTarefa.Delete(Convert.ToInt32(Tarefa.idTarefa));
                }
            }

            grdResultado.DataBind();
        }

        protected void btnPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            CarregaPesquisa();
        }

        private void CarregaPesquisa()
        {
            if (Session["IDUSUARIO"] != null
                && Session["IDUSUARIO"].ToString() != String.Empty)
            {
                dtoUsuario usuario = bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()));
                dtoUsuarioPermissao permissao = bllUsuarioPermissao.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()), "tarefa");

                dsResultado.SelectParameters.Clear();

                if (usuario.Administrador)
                    dsResultado.SelectParameters.Add("idUsuarioResponsavel", ddlUsuarioResponsavel.SelectedValue);
                else
                    dsResultado.SelectParameters.Add("idUsuarioResponsavel", usuario.idUsuario.ToString());

                dsResultado.SelectParameters.Add("termoPesquisa", txtPesquisa.Text);
                dsResultado.SelectParameters.Add("dataCadastroInicio", txtDataCadastroInicio.Text);
                dsResultado.SelectParameters.Add("dataCadastroFim", txtDataCadastroFim.Text);
                dsResultado.SelectParameters.Add("dataPrevisaoInicio", txtDataPrevisaoInicio.Text);
                dsResultado.SelectParameters.Add("dataPrevisaoFim", txtDataPrevisaoFim.Text);
                dsResultado.SelectParameters.Add("dataConclusaoInicio", txtDataConclusaoInicio.Text);
                dsResultado.SelectParameters.Add("dataConclusaoFim", txtDataConclusaoFim.Text);
                dsResultado.SelectParameters.Add("situacaoTarefa", ddlSituacaoTarefa.SelectedValue);
            }

            dsResultado.DataBind();
            grdResultado.DataBind();
        }

        protected void dsResultado_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            litTotalRegistros.Text = String.Format("{0} registro(s) encontrado(s)", ((List<dtoTarefa>)e.ReturnValue).Count.ToString());
        }


        private void InicializaEventos()
        {
            menuAcoesCadastro.ExcluirClickHandler += new EventHandler(btnExcluirSelecionados_Click);
            menuAcoesCadastro.ImprimirClickHandler += new EventHandler(Imprimir_Click);
        }


        protected void Imprimir_Click(object sender, EventArgs e)
        {

        }

        protected void grdResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idTarefa = DataBinder.Eval(e.Row.DataItem, "idTarefa").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/Tarefa.aspx?ID=" + idTarefa;
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                //e.Row.Style["cursor"] = "pointer";
                for (int i = 1; i < e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
                }
            }
        }
    }
}