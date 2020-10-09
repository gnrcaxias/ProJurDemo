using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ProJur.WebApplication.Paginas.Manutencao
{
    public partial class Pessoa : System.Web.UI.Page
    {

        int newIdPessoa = 0;


        private enum especiePessoa
        {
            Nenhuma = 0,
            Fisica = 1,
            Juridica = 2
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InicializaEventos();
            InicializaDefaultButton();

            barraAcoes.btnGravar.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(barraAcoes.btnGravar, null) + ";");
            barraAcoes.btnExcluir.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(barraAcoes.btnExcluir, null) + ";");

            if (!Page.IsPostBack)
            {
                // NOVO REGISTRO
                if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
                {
                    ConfiguraTipoPessoa("", false, false, false, false, false);
                    ConfiguraModoCRUD(DetailsViewMode.Insert, especiePessoa.Nenhuma, false, false, false, false, false);
                }
                else // REGISTRO EXISTENTE
                {
                    dtoPessoa pessoa = bllPessoa.Get(Convert.ToInt32(Request.QueryString["ID"]));

                    if (pessoa != null && pessoa.idPessoa != 0)
                    {
                        ConfiguraModoCRUD(DetailsViewMode.ReadOnly, pessoa.especiePessoa, pessoa.tipoPessoaColaborador, pessoa.tipoPessoaAdvogado, pessoa.tipoPessoaCliente, pessoa.tipoPessoaParte, pessoa.tipoPessoaTerceiro);
                        ConfiguraTipoPessoa(pessoa.especiePessoa, pessoa.tipoPessoaColaborador, pessoa.tipoPessoaAdvogado, pessoa.tipoPessoaCliente, pessoa.tipoPessoaParte, pessoa.tipoPessoaTerceiro);
                        CarregaAgendaHibrida();
                    }

                    SelecionarTabRetornoSubManutencao();

                    if (dvManutencao.CurrentMode == DetailsViewMode.ReadOnly)
                    {
                        string scriptSelectTab = String.Empty;

                        if (pessoa.especiePessoa == "F")
                            scriptSelectTab = @"$('#tabs').tabs({active: 2});";
                        else
                            scriptSelectTab = @"$('#tabs').tabs({active: 3});";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", scriptSelectTab, true);
                    }
                }
            }

            // NOVO REGISTRO
            if (Request.QueryString["ID"] == null || Request.QueryString["ID"].Trim() == String.Empty)
            {
                Master.litCaminhoPrincipal.Text = "Manutenção > ";
                Master.litCaminhoSecundario.Text = "Pessoa";
            }
            else
            {    
                Master.litCaminhoPrincipal.Text = "Manutenção > Pessoa > ";
                Master.litCaminhoSecundario.Text = String.Format("{0}", bllPessoa.Get(Convert.ToInt32(Request.QueryString["ID"])).NomeCompletoRazaoSocial);
            }
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            dtoPessoa pessoa = bllPessoa.Get(Convert.ToInt32(Request.QueryString["ID"]));

            ConfiguraModoCRUD(DetailsViewMode.Edit, pessoa.especiePessoa, pessoa.tipoPessoaColaborador, pessoa.tipoPessoaAdvogado, pessoa.tipoPessoaCliente, pessoa.tipoPessoaParte, pessoa.tipoPessoaTerceiro);
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].Trim() != String.Empty)
            {
                dtoPessoa pessoa = bllPessoa.Get(Convert.ToInt32(Request.QueryString["ID"]));
                ConfiguraModoCRUD(DetailsViewMode.ReadOnly, pessoa.especiePessoa, pessoa.tipoPessoaColaborador, pessoa.tipoPessoaAdvogado, pessoa.tipoPessoaCliente, pessoa.tipoPessoaParte, pessoa.tipoPessoaTerceiro);
            }
            else
                Response.Redirect(String.Format("{0}/Paginas/Cadastro/Pessoa.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CheckBoxList chkTipoPessoa = (CheckBoxList)dvManutencao.FindControl("chkTipoPessoa");
                DropDownList ddlEspeciePessoa = (DropDownList)dvManutencao.FindControl("ddlEspeciePessoa");

                bool tipoPessoaColaborador = false;
                bool tipoPessoaCliente = false;
                bool tipoPessoaParte = false;
                bool tipoPessoaAdvogado = false;
                bool tipoPessoaTerceiro = false;

                if (chkTipoPessoa != null)
                {
                    foreach (ListItem item in chkTipoPessoa.Items)
                    {
                        if (item.Selected)
                        {
                            switch (item.Value)
                            {
                                case "tipoPessoaColaborador":
                                    tipoPessoaColaborador = true;
                                    break;

                                case "tipoPessoaCliente":
                                    tipoPessoaCliente = true;
                                    break;

                                case "tipoPessoaParte":
                                    tipoPessoaParte = true;
                                    break;

                                case "tipoPessoaAdvogado":
                                    tipoPessoaAdvogado = true;
                                    break;

                                case "tipoPessoaTerceiro":
                                    tipoPessoaTerceiro = true;
                                    break;
                            }
                        }
                    }
                }

                try
                {
                    if (dvManutencao.CurrentMode == DetailsViewMode.Edit)
                    {
                        dvManutencao.UpdateItem(false);
                        dvContato.UpdateItem(false);

                        if (tipoPessoaColaborador)
                            dvColaborador.UpdateItem(false);

                        if (tipoPessoaAdvogado)
                            dvAdvogado.UpdateItem(false);

                        dvReferencias.UpdateItem(false);

                        switch (ddlEspeciePessoa.SelectedValue)
                        {
                            case "F":
                                dvDadosProfissionais.UpdateItem(false);
                                dvPessoaFisica.UpdateItem(false);
                                break;

                            case "J":
                                dvPessoaJuridica.UpdateItem(false);
                                break;
                        }
                    }
                    else
                    {
                        dvManutencao.InsertItem(true);
                        dvContato.InsertItem(true);

                        if (tipoPessoaColaborador)
                            dvColaborador.InsertItem(true);

                        if (tipoPessoaAdvogado)
                            dvAdvogado.InsertItem(true);

                        dvReferencias.InsertItem(true);

                        switch (ddlEspeciePessoa.SelectedValue)
                        {
                            case "F":
                                dvDadosProfissionais.InsertItem(true);
                                dvPessoaFisica.InsertItem(true);
                                break;

                            case "J":
                                dvPessoaJuridica.InsertItem(true);
                                break;
                        }
                    }
                }
                catch (Exception Ex)
                {

                }
            }
        }

        protected void Excluir_Click(object sender, EventArgs e)
        {
            dvManutencao.DeleteItem();
            Response.Redirect(String.Format("{0}/Paginas/Cadastro/Pessoa.aspx", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite()));
        }


        private void SelecionarTabRetornoSubManutencao()
        {
            if (Page.Request.UrlReferrer != null)
            {
                if (Page.Request.UrlReferrer.ToString().IndexOf("PessoaSocio.aspx", 0) > 0)
                {
                    string scriptSelectTab = @"$('#tabs').tabs({active: 2});";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", scriptSelectTab, true);
                }

            }

        }



        protected void dsManutencao_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            newIdPessoa = (int)e.ReturnValue;
        }

        protected void dsPessoaGenerico_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            Response.Redirect(String.Format("{0}/Paginas/Manutencao/Pessoa.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), newIdPessoa));
        }

        protected void dvPessoaGenerico_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            e.Values["idPessoa"] = newIdPessoa;
        }


        protected void dvManutencao_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            DetailsView detailsView = (DetailsView)sender;
            CheckBoxList chkTipoPessoa = (CheckBoxList)detailsView.FindControl("chkTipoPessoa");

            List<dtoListItem> lstTipoPessoa = bllDataTable.GetTipoPessoa();

            foreach (dtoListItem item in lstTipoPessoa)
                e.Values.Add(item.ValorChave, false);

            foreach (ListItem item in chkTipoPessoa.Items)
                e.Values[item.Value] = false;

            foreach (ListItem item in chkTipoPessoa.Items)
                if (item.Selected)
                    e.Values[item.Value] = true;
        }

        protected void dvManutencao_DataBound(object sender, EventArgs e)
        {
            if (dvManutencao.CurrentMode == DetailsViewMode.Edit)
            {
                DetailsView detailsView = (DetailsView)sender;
                dtoPessoa pessoa = (dtoPessoa)detailsView.DataItem;
                CheckBoxList chkTipoPessoa = (CheckBoxList)detailsView.FindControl("chkTipoPessoa");

                List<dtoListItem> lstTipoPessoa = bllDataTable.GetTipoPessoa();

                foreach (dtoListItem item in lstTipoPessoa)
                    chkTipoPessoa.Items.FindByValue(item.ValorChave).Selected = (bool)pessoa.GetValue(item.ValorChave);
            }
        }

        protected void dvManutencao_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            DetailsView detailsView = (DetailsView)sender;
            CheckBoxList chkTipoPessoa = (CheckBoxList)detailsView.FindControl("chkTipoPessoa");

            List<dtoListItem> lstTipoPessoa = bllDataTable.GetTipoPessoa();

            foreach (dtoListItem item in lstTipoPessoa)
                e.NewValues.Add(item.ValorChave, false);

            foreach (ListItem item in chkTipoPessoa.Items)
                e.NewValues[item.Value] = false;

            foreach (ListItem item in chkTipoPessoa.Items)
                if (item.Selected)
                    e.NewValues[item.Value] = true;
        }

        private void InicializaEventos()
        {
            barraAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            barraAcoes.ExcluirClickHandler += new EventHandler(Excluir_Click);
            barraAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            barraAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);
        }

        protected void chkTipoPessoa_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList chkTipoPessoa = (CheckBoxList)sender;
            DetailsView detailsView = (DetailsView)chkTipoPessoa.NamingContainer;
            DropDownList ddlEspeciePessoa = (DropDownList)detailsView.FindControl("ddlEspeciePessoa");

            bool tipoPessoaColaborador = false;
            bool tipoPessoaCliente = false;
            bool tipoPessoaParte = false;
            bool tipoPessoaTerceiro = false;
            bool tipoPessoaAdvogado = false;

            foreach (ListItem item in chkTipoPessoa.Items)
            {
                if (item.Selected)
                {
                    switch (item.Value)
                    {
                        case "tipoPessoaColaborador":
                            tipoPessoaColaborador = true;
                            break;

                        case "tipoPessoaCliente":
                            tipoPessoaCliente = true;
                            break;

                        case "tipoPessoaParte":
                            tipoPessoaParte = true;
                            break;

                        case "tipoPessoaTerceiro":
                            tipoPessoaTerceiro = true;
                            break;

                        case "tipoPessoaAdvogado":
                            tipoPessoaAdvogado = true;
                            break;
                           
                    }
                }
            }

            ConfiguraTipoPessoa(ddlEspeciePessoa.SelectedValue, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaCliente, tipoPessoaParte, tipoPessoaTerceiro);
            ConfiguraModoCRUD(detailsView.CurrentMode, ddlEspeciePessoa.SelectedValue, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaCliente, tipoPessoaParte, tipoPessoaTerceiro);

        }

        protected void ddlEspeciePessoa_Click(object sender, EventArgs e)
        {
            DropDownList ddlEspeciePessoa = (DropDownList)sender;
            DetailsView detailsView = (DetailsView)ddlEspeciePessoa.NamingContainer;
            CheckBoxList chkTipoPessoa = (CheckBoxList)detailsView.FindControl("chkTipoPessoa");

            bool tipoPessoaColaborador = false;
            bool tipoPessoaCliente = false;
            bool tipoPessoaParte = false;
            bool tipoPessoaTerceiro = false;
            bool tipoPessoaAdvogado = false;

            foreach (ListItem item in chkTipoPessoa.Items)
            {
                if (item.Selected)
                {
                    switch (item.Value)
                    {
                        case "tipoPessoaColaborador":
                            tipoPessoaColaborador = true;
                            break;

                        case "tipoPessoaCliente":
                            tipoPessoaCliente = true;
                            break;

                        case "tipoPessoaParte":
                            tipoPessoaParte = true;
                            break;

                        case "tipoPessoaTerceiro":
                            tipoPessoaTerceiro = true;
                            break;

                        case "tipoPessoaAdvogado":
                            tipoPessoaAdvogado = true;
                            break;
                    }
                }
            }

            ConfiguraTipoPessoa(ddlEspeciePessoa.SelectedValue, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaCliente, tipoPessoaParte, tipoPessoaTerceiro);
            ConfiguraModoCRUD(detailsView.CurrentMode, ddlEspeciePessoa.SelectedValue, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaCliente, tipoPessoaParte, tipoPessoaTerceiro);
        }


        protected void btnAdicionarPessoaSocio_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != String.Empty)
                Response.Redirect(String.Format("{0}/Paginas/Manutencao/PessoaSocio.aspx?idPessoa={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), Request.QueryString["ID"]));
        }


        protected string ConverteEstadoCheckBox(object estado)
        {
            string retorno = String.Empty;

            if (estado != null && (bool)estado == true)
                retorno = "checked='checked'";

            return retorno;
        }

        private void AdicionaItemSelecioneDropDown(DropDownList ddl)
        {
            System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("< Selecione >", "0");
            ddl.Items.Insert(0, li);
        }



        private void ConfiguraModoCRUD(DetailsViewMode novoModo, string especiePessoa, bool tipoPessoaColaborador, bool tipoPessoaAdvogado, bool tipoPessoaCliente, bool tipoPessoaParte, bool tipoPessoaTerceiro)
        {
            switch (especiePessoa)
            {
                case "F":
                    this.ConfiguraModoCRUD(novoModo, Pessoa.especiePessoa.Fisica, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaCliente, tipoPessoaParte, tipoPessoaTerceiro);
                    break;

                case "J":
                    this.ConfiguraModoCRUD(novoModo, Pessoa.especiePessoa.Juridica, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaCliente, tipoPessoaParte, tipoPessoaTerceiro);
                    break;

                case "":
                    this.ConfiguraModoCRUD(novoModo, Pessoa.especiePessoa.Nenhuma, false, false, false, false, false);
                    break;
            }
        }

        private void ConfiguraModoCRUD(DetailsViewMode novoModo, especiePessoa especiePessoa, bool tipoPessoaColaborador, bool tipoPessoaAdvogado, bool tipoPessoaCliente, bool tipoPessoaParte, bool tipoPessoaTerceiro)
        {
            dvManutencao.ChangeMode(novoModo);

            if (especiePessoa != Pessoa.especiePessoa.Nenhuma)
            {
                if (tipoPessoaColaborador)
                    dvColaborador.ChangeMode(novoModo);

                if (tipoPessoaAdvogado)
                    dvAdvogado.ChangeMode(novoModo);

                dvContato.ChangeMode(novoModo);
                dvReferencias.ChangeMode(novoModo);
            }

            switch (especiePessoa)
            {
                case especiePessoa.Fisica:
                    dvPessoaFisica.ChangeMode(novoModo);
                    dvDadosProfissionais.ChangeMode(novoModo);
                    break;

                case especiePessoa.Juridica:
                    dvPessoaJuridica.ChangeMode(novoModo);
                    break;
            }
        }

        private void ConfiguraTipoPessoa(string especiePessoa, bool tipoPessoaColaborador, bool tipoPessoaAdvogado, bool tipoPessoaCliente, bool tipoPessoaParte, bool tipoPessoaTerceiro)
        {
            switch (especiePessoa)
            {
                case "F":
                    this.ConfiguraTipoPessoa(Pessoa.especiePessoa.Fisica, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaCliente, tipoPessoaParte, tipoPessoaTerceiro);
                    break;

                case "J":
                    this.ConfiguraTipoPessoa(Pessoa.especiePessoa.Juridica, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaCliente, tipoPessoaParte, tipoPessoaTerceiro);
                    break;

                case "":
                    this.ConfiguraTipoPessoa(Pessoa.especiePessoa.Nenhuma, tipoPessoaColaborador, tipoPessoaAdvogado, tipoPessoaCliente, tipoPessoaParte, tipoPessoaTerceiro);
                    break;
            }
        }

        private void ConfiguraTipoPessoa(especiePessoa especiePessoa, bool tipoPessoaColaborador, bool tipoPessoaAdvogado, bool tipoPessoaCliente, bool tipoPessoaParte, bool tipoPessoaTerceiro)
        {
            pnlPessoaFisica.Visible = false;
            liPessoaFisica.Visible = false;

            pnlPessoaJuridica.Visible = false;
            liPessoaJuridica.Visible = false;

            pnlPessoaSocio.Visible = false;
            liSocios.Visible = false;

            pnlColaborador.Visible = false;
            liColaborador.Visible = false;

            pnlContato.Visible = false;
            liContato.Visible = false;

            pnlDadosProfissionais.Visible = false;
            liDadosProfissionais.Visible = false;

            pnlReferencias.Visible = false;
            liReferencias.Visible = false;

            pnlAdvogado.Visible = false;
            liAdvogado.Visible = false;
            
            pnlVinculos.Visible = false;
            liVinculos.Visible = false;

            pnlVinculoAgendaHibrida.Visible = false;
            liAgenda.Visible = false;

            switch (especiePessoa)
            {
                case especiePessoa.Fisica:

                    pnlPessoaFisica.Visible = true;
                    liPessoaFisica.Visible = true;

                    pnlPessoaJuridica.Visible = false;
                    liPessoaJuridica.Visible = false;

                    if (tipoPessoaColaborador)
                    {
                        liColaborador.Visible = true;
                        pnlColaborador.Visible = true;
                    }

                    if (tipoPessoaAdvogado)
                    {
                        liAdvogado.Visible = true;
                        pnlAdvogado.Visible = true;
                    }


                    pnlContato.Visible = true;
                    liContato.Visible = true;

                    pnlDadosProfissionais.Visible = true;
                    liDadosProfissionais.Visible = true;

                    pnlReferencias.Visible = true;
                    liReferencias.Visible = true;

                    pnlVinculos.Visible = true;
                    liVinculos.Visible = true;

                    pnlVinculoAgendaHibrida.Visible = true;
                    liAgenda.Visible = true;

                    break;

                case especiePessoa.Juridica:

                    pnlPessoaFisica.Visible = false;
                    liPessoaFisica.Visible = false;

                    pnlPessoaJuridica.Visible = true;
                    liPessoaJuridica.Visible = true;

                    pnlPessoaSocio.Visible = true;
                    liSocios.Visible = true;

                    if (tipoPessoaColaborador)
                    {
                        liColaborador.Visible = true;
                        pnlColaborador.Visible = true;
                    }

                    if (tipoPessoaAdvogado)
                    {
                        liAdvogado.Visible = true;
                        pnlAdvogado.Visible = true;
                    }

                    pnlContato.Visible = true;
                    liContato.Visible = true;

                    pnlDadosProfissionais.Visible = false;
                    liDadosProfissionais.Visible = false;

                    pnlReferencias.Visible = true;
                    liReferencias.Visible = true;

                    pnlVinculos.Visible = true;
                    liVinculos.Visible = true;

                    pnlVinculoAgendaHibrida.Visible = true;
                    liAgenda.Visible = true;

                    break;
            }
        }


        protected void reqFisicaCPFObrigatorio_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                if (dvManutencao.CurrentMode == DetailsViewMode.Insert)
                    args.IsValid = !bllPessoa.ExistsPessoaFisica(args.Value.Replace(".", "").Replace("-", ""));
                else
                    args.IsValid = true;
            }
        }

        protected void reqEnderecoCidade_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args != null && args.Value.ToString().Trim() != String.Empty && args.Value.ToString().Trim() != "0");
        }

        protected void reqEnderecoEstado_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (args != null && args.Value.ToString().Trim() != String.Empty && args.Value.ToString().Trim() != "0");
        }

        protected void reqCNPJObrigatorio_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                if (dvManutencao.CurrentMode == DetailsViewMode.Insert)
                    args.IsValid = !bllPessoa.ExistsPessoaJuridica(args.Value.Replace(".", "").Replace("-", "").Replace("/", ""));
                else
                    args.IsValid = true;
            }
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

        protected void reqDadosProfissionaisDataAdmissaoValida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                DateTime dtDataValida;

                args.IsValid = DateTime.TryParse(args.Value, out dtDataValida);
            }
        }

        protected void reqColaboradorDataAdmissaoValida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args != null && args.Value.ToString().Trim() != String.Empty)
            {
                DateTime dtDataValida;

                args.IsValid = DateTime.TryParse(args.Value, out dtDataValida);
            }
        }


        public string FormatText(object input)
        {
            if (input != null
                && input.ToString() != String.Empty)
                return input.ToString().Replace("\n", "<br />");
            else
                return "";
        }

        protected string RetornaNomeUsuariosCompromisso(int idAgendaCompromisso)
        {
            List<dtoAgendaUsuario> agendaUsuarios = bllAgendaUsuario.GetByAgendaCompromisso(idAgendaCompromisso);
            dtoAgendaCompromisso compromisso = bllAgendaCompromisso.Get(idAgendaCompromisso);
            StringBuilder sbRetorno = new StringBuilder();

            if (agendaUsuarios.Count > 0)
            {
                foreach (dtoAgendaUsuario agendaUsuario in agendaUsuarios)
                {
                    if ((agendaUsuario.idUsuario != ProJur.DataAccess.Sessions.IdUsuarioLogado)
                        || (agendaUsuario.idUsuario == ProJur.DataAccess.Sessions.IdUsuarioLogado
                            && compromisso.idUsuarioCadastro != ProJur.DataAccess.Sessions.IdUsuarioLogado))
                    {
                        dtoUsuario usuario = bllUsuario.Get(agendaUsuario.idUsuario);

                        if (usuario.idUsuario > 0)
                        {
                            if (sbRetorno.ToString() != String.Empty)
                                sbRetorno.Append(", ");

                            sbRetorno.Append(usuario.nomeCompleto);
                        }
                    }
                }
            }

            return sbRetorno.ToString();
        }

        protected string RetornaNomeResponsaveisPrazo(int idAgendaPrazo)
        {
            List<dtoProcessoPrazoResponsavel> processoPrazoResponsaveis = bllProcessoPrazoResponsavel.GetByProcessoPrazo(idAgendaPrazo);
            StringBuilder sbRetorno = new StringBuilder();

            if (processoPrazoResponsaveis.Count > 0)
            {
                foreach (dtoProcessoPrazoResponsavel processoPrazoResponsavel in processoPrazoResponsaveis)
                {
                    dtoPessoa pessoa = bllPessoa.Get(processoPrazoResponsavel.idPessoa);

                    if (pessoa.idPessoa > 0)
                    {
                        if (sbRetorno.ToString() != String.Empty)
                            sbRetorno.Append(", ");

                        sbRetorno.Append(pessoa.NomeCompletoRazaoSocial);
                    }
                }
            }

            return sbRetorno.ToString();
        }

        public string RetornaDescricao(object idAgendaHibrida, object tipoAgendamento)
        {
            StringBuilder sbRetorno = new StringBuilder();

            if (tipoAgendamento != null
                && idAgendaHibrida != null)
            {
                if (tipoAgendamento.ToString() == "Agenda")
                {
                    dtoAgendaCompromisso compromisso = bllAgendaCompromisso.Get(Convert.ToInt32(idAgendaHibrida));
                    StringBuilder sbTipoPessoa = new StringBuilder();

                    dtoPessoa pessoa = bllPessoa.Get(compromisso.idPessoa);

                    if (pessoa.idPessoa > 0)
                    {
                        sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/Pessoa.aspx?ID={1}' target='_blank'>{2}: {3}</a>", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), compromisso.idPessoa, pessoa.TipoPessoaDescricao.ToUpper(), pessoa.NomeCompletoRazaoSocial);

                        sbRetorno.Append("<br />");
                        sbRetorno.Append("<br />");
                    }

                    sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/AgendaCompromisso.aspx?ID={1}' target='_blank'>{2}</a>", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString(), FormatText(compromisso.Descricao));
                }
                else
                {
                    dtoProcessoPrazo processoPrazo = bllProcessoPrazo.Get(Convert.ToInt32(idAgendaHibrida));
                    StringBuilder sbTipoPessoa = new StringBuilder();

                    if (processoPrazo != null && processoPrazo.idProcessoPrazo > 0)
                    {
                        dtoProcesso processo = bllProcesso.Get(processoPrazo.idProcesso);

                        if (processo != null && processo.idProcesso > 0)
                        {
                            dtoPessoa pessoa = bllPessoa.Get(processo.idPessoaCliente);

                            if (pessoa != null && pessoa.idPessoa > 0)
                            {
                                sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/Pessoa.aspx?ID={1}' target='_blank'>{2}: {3}</a>", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), processo.idPessoaCliente, pessoa.TipoPessoaDescricao.ToUpper(), pessoa.NomeCompletoRazaoSocial);

                                sbRetorno.Append("<br />");
                                sbRetorno.Append("<br />");
                            }
                        }

                        sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}&Origem=Agenda' target='_blank'>{2}</a>", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString(), FormatText(processoPrazo.Descricao));
                    }
                }
            }

            return sbRetorno.ToString();
        }

        public string RetornaDescricaoParticipantes(object idAgendaHibrida, object tipoAgendamento, object Responsaveis)
        {
            StringBuilder sbRetorno = new StringBuilder();

            if (tipoAgendamento != null
                && idAgendaHibrida != null)
            {
                if (tipoAgendamento.ToString() == "Prazo")
                {
                    //dtoProcessoPrazo prazo = bllProcessoPrazo.Get(Convert.ToInt32(idAgendaHibrida));
                    //dtoUsuario usuarioAbertura = bllUsuario.Get(prazo.idUsuarioCadastro);

                    //sbRetorno.AppendFormat("Advogado Responsável: <b style='font-weight: normal; color: #727376'>{0}</b>", Responsaveis.ToString());                    

                    dtoProcessoPrazo prazo = bllProcessoPrazo.Get(Convert.ToInt32(idAgendaHibrida));
                    dtoUsuario usuarioAbertura = bllUsuario.Get(prazo.idUsuarioCadastro);

                    sbRetorno.AppendFormat("Agendado por: <b style='font-weight: normal; color: #727376'>{0}</b>", usuarioAbertura.nomeCompleto);

                    if (Responsaveis != null)
                    {
                        sbRetorno.AppendFormat("<br />");
                        sbRetorno.AppendFormat("<br />");
                        sbRetorno.AppendFormat("Responsáveis: <b style='font-weight: normal; color: #727376'>{0}</b>", this.RetornaNomeResponsaveisPrazo(Convert.ToInt32(idAgendaHibrida)));
                    }
                }
                else if (tipoAgendamento.ToString() == "Agenda")
                {
                    dtoAgendaCompromisso compromisso = bllAgendaCompromisso.Get(Convert.ToInt32(idAgendaHibrida));
                    dtoUsuario usuarioAbertura = bllUsuario.Get(compromisso.idUsuarioCadastro);

                    sbRetorno.AppendFormat("Agendado por: <b style='font-weight: normal; color: #727376'>{0}</b>", usuarioAbertura.nomeCompleto);

                    if (Responsaveis != null)
                    {
                        sbRetorno.AppendFormat("<br />");
                        sbRetorno.AppendFormat("<br />");
                        sbRetorno.AppendFormat("Participantes: <b style='font-weight: normal; color: #727376'>{0}</b>", this.RetornaNomeUsuariosCompromisso(Convert.ToInt32(idAgendaHibrida)));
                    }
                }
            }

            return sbRetorno.ToString();
        }

        public string RetornaPaginaManutencao(object tipoAgendamento, object idAgendaHibrida)
        {
            string Retorno = String.Empty;

            if (tipoAgendamento != null
                && idAgendaHibrida != null)
            {
                switch (tipoAgendamento.ToString())
                {
                    case "Prazo":
                        Retorno = String.Format("{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}&Origem=Agenda", DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString());

                        break;

                    case "Agenda":
                        Retorno = String.Format("{0}/Paginas/Manutencao/AgendaCompromisso.aspx?ID={1}", DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString());
                        break;
                }
            }

            return Retorno;
        }

        protected void CarregaAgendaHibrida()
        {
            dsAgendaHibrida.SelectParameters.Add("listaUsuarios", "");
            dsAgendaHibrida.SelectParameters.Add("tipoAgendamento", "");
            dsAgendaHibrida.SelectParameters.Add("Status", "");
            dsAgendaHibrida.SelectParameters.Add("termoPesquisa", "");
            dsAgendaHibrida.SelectParameters.Add("dataInicioDe", "");
            dsAgendaHibrida.SelectParameters.Add("dataInicioAte", "");
            dsAgendaHibrida.SelectParameters.Add("dataTerminoDe", "");
            dsAgendaHibrida.SelectParameters.Add("dataTerminoAte", "");
            dsAgendaHibrida.SelectParameters.Add("idCliente", Request.QueryString["ID"].ToString());

            dsAgendaHibrida.DataBind();
            grdAgendaHibrida.DataBind();
        }

        protected void grdAgendaHibrida_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "tipoAgendamento").ToString() == "Prazo")
                    {
                        e.Row.Cells[6].Attributes.Add("class", "rowPrazo");
                    }
                    else if (DataBinder.Eval(e.Row.DataItem, "tipoAgendamento").ToString() == "Agenda")
                    {
                        e.Row.Cells[6].Attributes.Add("class", "rowAgenda");
                    }

                    string Location = RetornaPaginaManutencao(DataBinder.Eval(e.Row.DataItem, "tipoAgendamento"), DataBinder.Eval(e.Row.DataItem, "idAgendaHibrida"));

                    for (int i = 1; i < e.Row.Cells.Count - 1; i++)
                    {
                        e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                        e.Row.Cells[i].Style["cursor"] = "pointer";
                    }
                }
            }
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$barraAcoes$btnGravar";
        }

    }
}