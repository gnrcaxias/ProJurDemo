using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProJur.WebApplication.Controls
{
    public partial class dialogSelecaoPessoa : System.Web.UI.UserControl
    {
        public event EventHandler FecharClickHandler;
        public event GridViewCommandEventHandler SelecionarClickHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                InicializaCadastro();
        }

        protected void InicializaCadastro()
        {
            CarregarPesquisa();

            grdResultado.Sort("CASE WHEN especiePessoa = 'F' THEN fisicaNomeCompleto WHEN especiePessoa = 'J' THEN juridicaRazaoSocial ELSE '' END", SortDirection.Ascending);
        }

        protected void dsResultado_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            litTotalRegistros.Text = String.Format("{0} registro(s) encontrado(s)", ((List<dtoPessoa>)e.ReturnValue).Count.ToString());
        }

        protected void btnPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            CarregarPesquisa();
        }

        public void CarregarPesquisa()
        {
            dsResultado.SelectParameters.Clear();
            dsResultado.SelectParameters.Add("termoPesquisa", txtPesquisa.Text.Replace(".", "").Replace("-", "").Replace("/", "").Trim());
            dsResultado.SelectParameters.Add("tipoPessoaAdvogado", tipoPessoaAdvogado);
            dsResultado.SelectParameters.Add("tipoPessoaCliente", tipoPessoaCliente);
            dsResultado.SelectParameters.Add("tipoPessoaColaborador", tipoPessoaColaborador);
            dsResultado.SelectParameters.Add("tipoPessoaParte", tipoPessoaParte);
            dsResultado.SelectParameters.Add("tipoPessoaTerceiro", tipoPessoaTerceiro);

            dsResultado.DataBind();
            grdResultado.DataBind();
        }

        protected void OnFecharClick(object sender, EventArgs e)
        {
            if (FecharClickHandler != null)
                FecharClickHandler(this, e);
        }

        protected void OnSelecionarClick(object sender, GridViewCommandEventArgs e)
        {
            if (SelecionarClickHandler != null)
                SelecionarClickHandler(this, e);
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            FecharClickHandler(sender, e);
        }

        protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToUpper() == "SELECT")
            {
                grdResultado.SelectedIndex = Convert.ToInt32(e.CommandArgument.ToString());
                SelecionarClickHandler(sender, e);
            }
        }

        [DefaultValue("")]
        public string tipoPessoaAdvogado { get; set; }

        [DefaultValue("")]
        public string tipoPessoaCliente { get; set; }

        [DefaultValue("")]
        public string tipoPessoaColaborador { get; set; }

        [DefaultValue("")]
        public string tipoPessoaParte { get; set; }

        [DefaultValue("")]
        public string tipoPessoaTerceiro { get; set; }

    }
}