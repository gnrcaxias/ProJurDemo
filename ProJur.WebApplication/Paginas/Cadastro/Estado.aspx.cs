﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;

namespace ProJur.WebApplication.Paginas.Cadastro
{
    public partial class Estado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                InicializaCadastro();

            InicializaEventos();
            InicializaDefaultButton();

            Master.litCaminhoPrincipal.Text = "ProJur > Cadastro > ";
            Master.litCaminhoSecundario.Text = "Estado";
        }


        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = btnPesquisar.UniqueID;
        }

        protected void InicializaCadastro()
        {
            grdResultado.Sort("idEstado", SortDirection.Ascending);
        }


        protected void btnExcluirSelecionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdResultado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkExcluir = (CheckBox)row.FindControl("chkExcluir");

                    HiddenField hdIdEstado = (HiddenField)row.FindControl("hdIdEstado");

                    dtoEstado Estado = bllEstado.Get(Convert.ToInt32(hdIdEstado.Value));

                    if (chkExcluir.Checked && Estado != null)
                        bllEstado.Delete(Convert.ToInt32(Estado.idEstado));
                }
            }

            grdResultado.DataBind();
        }

        protected void btnPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            dsResultado.SelectParameters.Clear();
            dsResultado.SelectParameters.Add("termoPesquisa", txtPesquisa.Text);

            dsResultado.DataBind();
            grdResultado.DataBind();
        }

        private void CarregaPesquisa()
        {

        }

        protected void dsResultado_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            litTotalRegistros.Text = String.Format("{0} registro(s) encontrado(s)", ((List<dtoEstado>)e.ReturnValue).Count.ToString());
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
                string idEstado = DataBinder.Eval(e.Row.DataItem, "idEstado").ToString();
                string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/Estado.aspx?ID=" + idEstado;
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
            List<dtoEstado> listaItems = bllEstado.GetAll("Descricao", prefixText);
            List<string> listaRetorno = new List<string>();

            foreach (dtoEstado item in listaItems)
            {
                listaRetorno.Add(String.Format("{0}", item.Descricao));
            }

            return listaRetorno;
        }

    }
}