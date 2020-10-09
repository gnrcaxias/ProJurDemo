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
    public partial class PessoaSocio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InicializaEventos();
            InicializaDefaultButton();

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
                    ConfiguraModoCRUD(DetailsViewMode.Insert);
                else
                {
                    dtoProcessoAdvogado processoAdvogado = bllProcessoAdvogado.Get(Convert.ToInt32(Request.QueryString["ID"]));

                    if (processoAdvogado != null && processoAdvogado.idProcessoAdvogado != 0)
                        ConfiguraModoCRUD(DetailsViewMode.ReadOnly);
                }
            }
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
                if (Request.QueryString["idPessoa"] != null && Request.QueryString["idPessoa"].Trim() != String.Empty)
                    Response.Redirect(String.Format("{0}/Paginas/Manutencao/Pessoa.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["idPessoa"]));
            }

        }

        private void ConfiguraModoCRUD(DetailsViewMode novoModo)
        {
            dvPessoaSocio.ChangeMode(novoModo);
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (dvPessoaSocio.CurrentMode == DetailsViewMode.Edit)
                        dvPessoaSocio.UpdateItem(false);
                    else
                        dvPessoaSocio.InsertItem(true);
                }
                catch (Exception Ex)
                { }
            }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvPessoaSocio.DeleteItem();

            if (Request.QueryString["idPessoa"] != null && Request.QueryString["idPessoa"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Pessoa.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["idPessoa"]));
        }


        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);

            menuAcoes.VoltarUrl = String.Format("{0}/Paginas/Manutencao/Pessoa.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["idPessoa"]);
        }

        protected void dsPessoaSocio_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (Request.QueryString["idPessoa"] != null && Request.QueryString["idPessoa"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Pessoa.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["idPessoa"]));
        }

        protected void dvPessoaSocio_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            if (Request.QueryString["idPessoa"] != null && Request.QueryString["idPessoa"].Trim() != String.Empty)
                e.Values["idPessoa"] = Convert.ToInt32(Request.QueryString["idPessoa"]);
        }

        protected void dvPessoaSocio_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            if (Request.QueryString["idPessoa"] != null && Request.QueryString["idPessoa"].Trim() != String.Empty)
                e.NewValues["idPessoa"] = Convert.ToInt32(Request.QueryString["idPessoa"]);
        }


        protected void reqEnderecoCidade_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args != null && args.Value.ToString().Trim() != String.Empty && args.Value.ToString().Trim() != "0");
        }

        protected void reqEnderecoEstado_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args != null && args.Value.ToString().Trim() != String.Empty && args.Value.ToString().Trim() != "0");
        }

        protected void reqDataNascimentoValida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                DateTime dtDataValida;

                args.IsValid = DateTime.TryParse(args.Value, out dtDataValida);
            }
        }

        protected void reqFisicaRGDataExpedicaoValida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                DateTime dtDataValida;

                args.IsValid = DateTime.TryParse(args.Value, out dtDataValida);
            }
        }

        protected void reqFisicaCTPSDataExpedicaoValida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                DateTime dtDataValida;

                args.IsValid = DateTime.TryParse(args.Value, out dtDataValida);
            }
        }

        protected void reqCNHDataHabilitacaoValida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                DateTime dtDataValida;

                args.IsValid = DateTime.TryParse(args.Value, out dtDataValida);
            }
        }

        protected void reqCNHDataEmissaoValida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                DateTime dtDataValida;

                args.IsValid = DateTime.TryParse(args.Value, out dtDataValida);
            }
        }

        protected void reqConjugueDataNascimentoValida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                DateTime dtDataValida;

                args.IsValid = DateTime.TryParse(args.Value, out dtDataValida);
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