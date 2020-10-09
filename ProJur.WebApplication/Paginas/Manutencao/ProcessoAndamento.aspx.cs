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
    public partial class ProcessoAndamento : System.Web.UI.Page
    {
        public string idProcessoPeca = String.Empty;

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
                    dtoProcessoAndamento processoAndamento = bllProcessoAndamento.Get(Convert.ToInt32(Request.QueryString["ID"]));

                    if (processoAndamento != null && processoAndamento.idProcessoAndamento != 0)
                        ConfiguraModoCRUD(DetailsViewMode.ReadOnly);
                }
            }

            Master.litCaminhoPrincipal.Text = "Manutenção > ";
            Master.litCaminhoSecundario.Text = "Processo > Andamento";
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
                if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                    Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
            }

        }

        private void ConfiguraModoCRUD(DetailsViewMode novoModo)
        {
            switch (novoModo)
            {
                case DetailsViewMode.ReadOnly:
                    dvProcessoAndamento.ChangeMode(novoModo);
                    dvProcessoPeca.ChangeMode(novoModo);

                    break;
                case DetailsViewMode.Edit:
                    dvProcessoAndamento.ChangeMode(novoModo);

                    if (ExistePecaProcessual())
                        dvProcessoPeca.ChangeMode(novoModo);
                    else
                        dvProcessoPeca.ChangeMode(DetailsViewMode.Insert);

                    break;
                case DetailsViewMode.Insert:
                    dvProcessoAndamento.ChangeMode(novoModo);
                    dvProcessoPeca.ChangeMode(novoModo);

                    break;
                default:
                    break;
            }
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    if (dvProcessoPeca.CurrentMode == DetailsViewMode.Edit)
                        dvProcessoPeca.UpdateItem(false);
                    else if (dvProcessoPeca.CurrentMode == DetailsViewMode.Insert)
                    {
                        FileUpload fpArquivo = ((FileUpload)(dvProcessoPeca.FindControl("fpArquivo")));
                        TextBox txtDescricao = ((TextBox)(dvProcessoPeca.FindControl("txtDescricao")));

                        if (fpArquivo.HasFiles == true
                            || txtDescricao.Text.Trim() != String.Empty)
                            dvProcessoPeca.InsertItem(true);
                        else
                            dvProcessoPeca.ChangeMode(DetailsViewMode.ReadOnly);
                    }

                    if (dvProcessoAndamento.CurrentMode == DetailsViewMode.Edit)
                        dvProcessoAndamento.UpdateItem(false);
                    else if (dvProcessoAndamento.CurrentMode == DetailsViewMode.Insert)
                        dvProcessoAndamento.InsertItem(true);
                }
                catch (Exception Ex)
                { }
            }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvProcessoAndamento.DeleteItem();

            if (ExistePecaProcessual())
                dvProcessoPeca.DeleteItem();

            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
        }


        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);

            menuAcoes.VoltarUrl = String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]);
        }


        protected void dsProcessoAndamento_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/Processo.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["IdProcesso"]));
        }

        protected void dsProcessoPeca_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            idProcessoPeca = e.ReturnValue.ToString();
        }


        protected void dvProcessoAndamento_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
            { 
                e.Values["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

                FileUpload fpArquivo = ((FileUpload)(dvProcessoPeca.FindControl("fpArquivo")));
                TextBox txtDescricao = ((TextBox)(dvProcessoPeca.FindControl("txtDescricao")));

                if (fpArquivo.HasFiles == true
                    || txtDescricao.Text.Trim() != String.Empty)
                    e.Values["idProcessoPeca"] = idProcessoPeca;
            }
        }

        protected void dvProcessoAndamento_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
            { 
                e.NewValues["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

                FileUpload fpArquivo = ((FileUpload)(dvProcessoPeca.FindControl("fpArquivo")));
                TextBox txtDescricao = ((TextBox)(dvProcessoPeca.FindControl("txtDescricao")));

                if (fpArquivo.HasFiles == true
                    || txtDescricao.Text.Trim() != String.Empty)
                    e.NewValues["idProcessoPeca"] = idProcessoPeca;
            }
        }

        protected void dvProcessoPeca_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
            {
                e.Values["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

                FileUpload fpArquivo = ((FileUpload)((DetailsView)sender).FindControl("fpArquivo"));

                if (fpArquivo.HasFile)
                {
                    string fileName = String.Format("PROCESSOPECA-{0}-{1}-{2}", Request.QueryString["IdProcesso"].ToString(), Math.Abs(DateTime.Now.ToBinary()).ToString(), fpArquivo.FileName.Trim());
                    fpArquivo.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));
                    e.Values["caminhoArquivo"] = fileName;
                    e.Values["nomeArquivo"] = fpArquivo.FileName.Trim();
                }
            }

            
        }

        protected void dvProcessoPeca_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            if (Request.QueryString["IdProcesso"] != null && Request.QueryString["IdProcesso"].Trim() != String.Empty)
            {
                idProcessoPeca = e.Keys["idProcessoPeca"].ToString();

                e.NewValues["idProcesso"] = Convert.ToInt32(Request.QueryString["IdProcesso"]);

                FileUpload fpArquivo = ((FileUpload)((DetailsView)sender).FindControl("fpArquivo"));

                if (fpArquivo.FileName != String.Empty)
                {
                    string fileName = String.Format("PROCESSOPECA-{0}-{1}-{2}", Request.QueryString["IdProcesso"].ToString(), Math.Abs(DateTime.Now.ToBinary()).ToString(), fpArquivo.FileName.Trim());
                    fpArquivo.SaveAs(Server.MapPath(@"~\Uploads\" + fileName));
                    e.NewValues["caminhoArquivo"] = fileName;
                    e.NewValues["nomeArquivo"] = fpArquivo.FileName.Trim();
                }
                else
                {
                    e.NewValues["caminhoArquivo"] = e.OldValues["caminhoArquivo"];
                    e.NewValues["nomeArquivo"] = e.OldValues["nomeArquivo"];
                }
            }

        }

        protected void dvProcessoAndamento_DataBound(object sender, EventArgs e)
        {
            if (dvProcessoAndamento.CurrentMode == DetailsViewMode.Insert)
            {
                CheckBox chkVisivelCliente = (CheckBox)((DetailsView)sender).FindControl("chkVisivelCliente");
                chkVisivelCliente.Checked = true;
            }
        }

        protected void dvProcessoPeca_DataBound(object sender, EventArgs e)
        {
            if (dvProcessoPeca.CurrentMode == DetailsViewMode.Insert)
            {
                CheckBox chkVisivelCliente = (CheckBox)((DetailsView)sender).FindControl("chkVisivelCliente");
                chkVisivelCliente.Checked = true;
            }
        }

        protected string RetornaDescricaoCheckBox(object Valor)
        {
            string retorno = String.Empty;

            if (Valor != null)
            { 
                if (Convert.ToBoolean(Valor))
                    retorno = "Sim";
                else
                    retorno = "Não";
            }
            else
                retorno = "Não informado";

            return retorno;
        }

        public string FormatText(object input)
        {
            if (input != null
                && input.ToString() != String.Empty)
                return input.ToString().Replace("\n", "<br />");
            else
                return "";
        }


        protected bool ExistePecaProcessual()
        {
            bool Retorno = false;

            if (Request.QueryString["ID"] != null
                && Request.QueryString["ID"] != String.Empty)
            {
                dtoProcessoAndamento processoAndamento = bllProcessoAndamento.Get(Convert.ToInt32(Request.QueryString["ID"]));

                if (processoAndamento.idProcessoPeca > 0)
                    Retorno = true;
            }

            return Retorno;
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$menuAcoes$btnGravar";
        }

    }
}