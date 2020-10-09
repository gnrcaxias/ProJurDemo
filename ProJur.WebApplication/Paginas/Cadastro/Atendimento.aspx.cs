using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System.Linq;

namespace ProJur.WebApplication.Paginas.Cadastro
{
    public partial class Atendimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InicializaCadastro();
            }

            InicializaEventos();
            InicializaDefaultButton();

            Master.litCaminhoPrincipal.Text = "CRM > Cadastro > ";
            Master.litCaminhoSecundario.Text = "Atendimento";
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
                dtoUsuarioPermissao permissao = bllUsuarioPermissao.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()), "tarefa");

                if (!bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString())).Administrador)
                    divFiltroUsuario.Visible = false;
            }

            grdResultado.Sort("tbAtendimento.dataCadastro DESC", SortDirection.Ascending);

            CarregaPesquisa();
        }

        protected void btnExcluirSelecionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdResultado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkExcluir = (CheckBox)row.FindControl("chkExcluir");
                    HiddenField hdIdAtendimento = (HiddenField)row.FindControl("hdIdAtendimento");

                    dtoAtendimento atendimento = bllAtendimento.Get(Convert.ToInt32(hdIdAtendimento.Value));

                    if (chkExcluir.Checked && atendimento != null)
                        bllAtendimento.Delete(Convert.ToInt32(atendimento.idAtendimento));
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
                dtoUsuario Usuario = bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()));
                dtoUsuarioPermissao permissao = bllUsuarioPermissao.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()), "atendimento");

                dsResultado.SelectParameters.Clear();

                if (Usuario.Administrador)
                    dsResultado.SelectParameters.Add("idUsuario", ddlUsuario.SelectedValue);
                else
                    dsResultado.SelectParameters.Add("idUsuario", Usuario.idUsuario.ToString());

                dsResultado.SelectParameters.Add("termoPesquisa", txtPesquisa.Text);
                dsResultado.SelectParameters.Add("dataInicioAtendimento", txtDataInicioAtendimento.Text);
                dsResultado.SelectParameters.Add("dataFimAtendimento", txtDataFimAtendimento.Text);
                dsResultado.SelectParameters.Add("tipoAtendimento", ddlTipoAtendimento.SelectedValue);
            }

            dsResultado.DataBind();
        }

        protected void dsResultado_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            litTotalRegistros.Text = String.Format("{0} registro(s) encontrado(s)", ((List<dtoAtendimento>)e.ReturnValue).Count.ToString());
        }


        private void InicializaEventos()
        {
            menuAcoesCadastro.ExcluirClickHandler += new EventHandler(btnExcluirSelecionados_Click);
        }

        protected void grdResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idAtendimento = DataBinder.Eval(e.Row.DataItem, "idAtendimento").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/Atendimento.aspx?ID=" + idAtendimento;
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                //e.Row.Style["cursor"] = "pointer";
                for (int i = 1; i < e.Row.Cells.Count - 1; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCompletionList(string prefixText, int count)
        {

            List<dtoAtendimento> listaItems = bllAtendimento.GetAll("CASE WHEN especiePessoa = 'F' THEN fisicaNomeCompleto WHEN especiePessoa = 'J' THEN juridicaRazaoSocial ELSE '' END", prefixText).GroupBy(x => x.idPessoaCliente).Select(x => x.First()).ToList();
            List<string> listaRetorno = new List<string>();

            foreach (dtoAtendimento item in listaItems)
            {
                listaRetorno.Add(String.Format("{0}", bllPessoa.Get(item.idPessoaCliente).NomeCompletoRazaoSocial));
            }

            return listaRetorno;

        }
    }
}