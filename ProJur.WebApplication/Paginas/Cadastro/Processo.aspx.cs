using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;

namespace ProJur.WebApplication.Paginas.Cadastro
{
    public partial class Processo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                InicializaCadastro();

            InicializaEventos();
            InicializaDefaultButton();

            Master.litCaminhoPrincipal.Text = "Operacional > Cadastro > ";
            Master.litCaminhoSecundario.Text = "Processo";
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = btnPesquisar.UniqueID;
        }

        protected void InicializaCadastro()
        {
            CarregarPesquisa();
        }

        protected void btnExcluirSelecionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdResultado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkExcluir = (CheckBox)row.FindControl("chkExcluir");
                    HiddenField hdIdProcesso = (HiddenField)row.FindControl("hdIdProcesso");

                    dtoProcesso processo = bllProcesso.Get(Convert.ToInt32(hdIdProcesso.Value));

                    if (chkExcluir.Checked && processo != null)
                        bllProcesso.Delete(Convert.ToInt32(processo.idProcesso));
                }
            }

            grdResultado.DataBind();
        }


        private void CarregarPesquisa()
        {
            dsResultado.SelectParameters.Clear();
            dsResultado.SelectParameters.Add("termoPesquisa", txtPesquisa.Text);
            dsResultado.SelectParameters.Add("idAreaProcessual", rblAreaProcessual.SelectedValue);
            dsResultado.SelectParameters.Add("idComarca", rblComarca.SelectedValue);
            dsResultado.SelectParameters.Add("idInstancia", rblInstancia.SelectedValue);

            dsResultado.DataBind();
            grdResultado.DataBind();
        }

        protected void dsResultado_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            litTotalRegistros.Text = String.Format("{0} registro(s) encontrado(s)", ((List<dtoProcesso>)e.ReturnValue).Count.ToString());
        }

        private void InicializaEventos()
        {
            menuAcoesCadastro.ExcluirClickHandler += new EventHandler(btnExcluirSelecionados_Click);
            menuAcoesCadastro.ImprimirClickHandler += new EventHandler(Imprimir_Click);
        }

        protected void Imprimir_Click(object sender, EventArgs e)
        {

        }

        protected void btnPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            CarregarPesquisa();
        }

        protected void grdResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string idProcesso = DataBinder.Eval(e.Row.DataItem, "idProcesso").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/Processo.aspx?ID=" + idProcesso;
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

            List<dtoProcesso> listaItems = bllProcesso.GetAll("CASE WHEN especiePessoa = 'F' THEN fisicaNomeCompleto WHEN especiePessoa = 'J' THEN juridicaRazaoSocial ELSE '' END", prefixText).GroupBy(x => x.idPessoaCliente).Select(x => x.First()).ToList();
            List<string> listaRetorno = new List<string>();

            foreach (dtoProcesso item in listaItems)
            {
                listaRetorno.Add(String.Format("{0}", bllPessoa.Get(item.idPessoaCliente).NomeCompletoRazaoSocial));
            }

            return listaRetorno;

        }

    }
}