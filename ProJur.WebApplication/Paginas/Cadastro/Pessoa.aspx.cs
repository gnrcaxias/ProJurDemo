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
    public partial class Pessoa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                InicializaCadastro();

            InicializaEventos();
            InicializaDefaultButton();

            Master.litCaminhoPrincipal.Text = "Operacional > Cadastro > ";
            Master.litCaminhoSecundario.Text = "Pessoa";
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

            grdResultado.Sort("CASE WHEN especiePessoa = 'F' THEN fisicaNomeCompleto WHEN especiePessoa = 'J' THEN juridicaRazaoSocial ELSE '' END", SortDirection.Ascending);
        }

        protected void btnExcluirSelecionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdResultado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkExcluir = (CheckBox)row.FindControl("chkExcluir");
                    HiddenField hdIdPessoa = (HiddenField)row.FindControl("hdIdPessoa");

                    dtoPessoa pessoa = bllPessoa.Get(Convert.ToInt32(hdIdPessoa.Value));

                    if (chkExcluir.Checked && pessoa != null)
                        bllPessoa.Delete(Convert.ToInt32(pessoa.idPessoa));
                }
            }

            grdResultado.DataBind();
        }


        private void CarregarPesquisa()
        {
            dsResultado.SelectParameters.Clear();
            dsResultado.SelectParameters.Add("termoPesquisa", txtPesquisa.Text);

            dsResultado.DataBind();
            grdResultado.DataBind();
        }

        protected void dsResultado_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            litTotalRegistros.Text = String.Format("{0} registro(s) encontrado(s)", ((List<dtoPessoa>)e.ReturnValue).Count.ToString());
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
                string idPessoa = DataBinder.Eval(e.Row.DataItem, "idPessoa").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()  + "/Paginas/Manutencao/Pessoa.aspx?ID=" + idPessoa;

                for (int i = 1; i < e.Row.Cells.Count - 2; i++)
                {
                    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    e.Row.Cells[i].Style["cursor"] = "pointer";
                }
                //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                
                //e.Row.Style["cursor"] = "pointer";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCompletionList(string prefixText, int count)
        {
            List<dtoPessoa> listaPessoas = bllPessoa.GetAll("CASE WHEN especiePessoa = 'F' THEN fisicaNomeCompleto WHEN especiePessoa = 'J' THEN juridicaRazaoSocial ELSE '' END", prefixText);
            List<string> listaPessoaNome = new List<string>();

            foreach (dtoPessoa item in listaPessoas)
            {
                //listaPessoaNome.Add(String.Format("{0} - {1}", item.CPFCNPJ, item.NomeCompletoRazaoSocial));
                listaPessoaNome.Add(String.Format("{0}", item.NomeCompletoRazaoSocial));
                //listaPessoaNome.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(item.NomeCompletoRazaoSocial, item.CPFCNPJ));
            }

            return listaPessoaNome;
        }
    }
}