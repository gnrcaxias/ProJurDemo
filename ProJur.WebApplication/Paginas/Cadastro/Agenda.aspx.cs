using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System.Collections;
using System.Text;


namespace ProJur.WebApplication.Paginas.Cadastro
{

    public partial class Agenda : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                InicializaCadastro();
            }

            InicializaEventos();
            InicializaDefaultButton();

            Master.litCaminhoPrincipal.Text = "Operacional > Cadastro > ";
            Master.litCaminhoSecundario.Text = "Agenda";
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
                dtoUsuarioPermissao permissao = bllUsuarioPermissao.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()), "agenda");

                if (!bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString())).Administrador)
                    divFiltroUsuario.Visible = false;
            }

            Calendar1.SelectedDate = DateTime.Now;
            rblVisualizacaoAgenda.SelectedValue = "D";
            
            grdResultado.Sort("DataHoraInicio", SortDirection.Ascending);

            CarregaPesquisa();
        }



        protected void btnExcluirSelecionados_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdResultado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkExcluir = (CheckBox)row.FindControl("chkExcluir");

                    HiddenField hdIdAgendaCompromisso = (HiddenField)row.FindControl("hdIdAgendaCompromisso");

                    dtoAgendaCompromisso agendaCompromisso = bllAgendaCompromisso.Get(Convert.ToInt32(hdIdAgendaCompromisso.Value));

                    if (chkExcluir.Checked && agendaCompromisso != null)
                        bllAgendaCompromisso.Delete(Convert.ToInt32(agendaCompromisso.idAgendaCompromisso));
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

                // LISTA USUARIOS
                StringBuilder sbListaUsuarios = new StringBuilder();
                foreach (ListItem Item in rblUsuario.Items)
                {
                    if (Item.Selected)
                    { 
                        if (sbListaUsuarios.ToString() != String.Empty)
                            sbListaUsuarios.Append(",");

                        sbListaUsuarios.Append(Item.Value);
                    }
                }
                if (usuario.Administrador)
                    dsResultado.SelectParameters.Add("listaUsuarios", sbListaUsuarios.ToString());
                else
                    dsResultado.SelectParameters.Add("listaUsuarios", usuario.idUsuario.ToString());

                // TIPO AGENDAMENTO
                StringBuilder sbTipoAgendamento = new StringBuilder();
                foreach (ListItem Item in cblTipoAgendamento.Items)
                {
                    if (Item.Selected)
                    {
                        if (sbTipoAgendamento.ToString() != String.Empty)
                            sbTipoAgendamento.Append(",");

                        sbTipoAgendamento.Append("'");
                        sbTipoAgendamento.Append(Item.Value);
                        sbTipoAgendamento.Append("'");
                    }
                }
                dsResultado.SelectParameters.Add("tipoAgendamento", sbTipoAgendamento.ToString());

                // STATUS
                StringBuilder sbStatus = new StringBuilder();
                foreach (ListItem Item in cblStatus.Items)
                {
                    if (Item.Selected)
                    {
                        if (sbStatus.ToString() != String.Empty)
                            sbStatus.Append(",");

                        sbStatus.Append("'");
                        sbStatus.Append(Item.Value);
                        sbStatus.Append("'");
                    }
                }

                dsResultado.SelectParameters.Add("Status", sbStatus.ToString());
                dsResultado.SelectParameters.Add("termoPesquisa", txtPesquisa.Text);

                string dataInicioDe = String.Empty;
                string dataInicioAte = String.Empty;
                switch (rblVisualizacaoAgenda.SelectedValue)
                {
                    case "":
                        lblResultado.InnerText = "Todos os dias";

                        dsResultado.SelectParameters.Add("dataInicioDe", "");
                        dsResultado.SelectParameters.Add("dataInicioAte", "");
                        dsResultado.SelectParameters.Add("dataTerminoDe", "");
                        dsResultado.SelectParameters.Add("dataTerminoAte", "");

                        break;

                    case "D":
                        dataInicioDe = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
                        dataInicioAte = Calendar1.SelectedDate.ToString("dd/MM/yyyy");

                        lblResultado.InnerText = "Dia " + dataInicioDe;

                        dsResultado.SelectParameters.Add("dataInicioDe", dataInicioDe);
                        dsResultado.SelectParameters.Add("dataInicioAte", dataInicioAte);
                        dsResultado.SelectParameters.Add("dataTerminoDe", "");
                        dsResultado.SelectParameters.Add("dataTerminoAte", "");

                        break;

                    case "S":
                        dataInicioDe = StartOfWeek(Calendar1.SelectedDate, DayOfWeek.Sunday).ToString("dd/MM/yyyy");
                        dataInicioAte = StartOfWeek(Calendar1.SelectedDate, DayOfWeek.Sunday).AddDays(6).ToString("dd/MM/yyyy");

                        lblResultado.InnerText = "Semana " + dataInicioDe + " - " + dataInicioAte;

                        dsResultado.SelectParameters.Add("dataInicioDe", dataInicioDe);
                        dsResultado.SelectParameters.Add("dataInicioAte", dataInicioAte);
                        dsResultado.SelectParameters.Add("dataTerminoDe", "");
                        dsResultado.SelectParameters.Add("dataTerminoAte", "");

                        break;

                    case "M":
                        dataInicioDe = FirstDayMonth(Calendar1.SelectedDate).ToString("dd/MM/yyyy");
                        dataInicioAte = LastDayMonth(Calendar1.SelectedDate).ToString("dd/MM/yyyy");

                        lblResultado.InnerText = "Mês " + dataInicioDe + " - " + dataInicioAte;

                        dsResultado.SelectParameters.Add("dataInicioDe", dataInicioDe);
                        dsResultado.SelectParameters.Add("dataInicioAte", dataInicioAte);
                        dsResultado.SelectParameters.Add("dataTerminoDe", "");
                        dsResultado.SelectParameters.Add("dataTerminoAte", "");

                        break;
                }
            }

            dsResultado.DataBind();
            grdResultado.DataBind();
        }

        protected void dsResultado_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            litTotalRegistros.Text = String.Format("{0} registro(s) encontrado(s)", ((List<dtoAgendaHibrida>)e.ReturnValue).Count.ToString());
        }



        protected DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        protected DateTime FirstDayMonth(DateTime dt)
        {
            DateTime now = dt;
            var startDate = new DateTime(now.Year, now.Month, 1);
            //var endDate = startDate.AddMonths(1).AddDays(-1);

            return startDate;
        }

        protected DateTime LastDayMonth(DateTime dt)
        {
            DateTime now = dt;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return endDate;
        }



        private void InicializaEventos()
        {
            menuAcoesCadastro.ExcluirClickHandler += new EventHandler(btnExcluirSelecionados_Click);
            menuAcoesCadastro.ImprimirClickHandler += new EventHandler(Imprimir_Click);
            menuAcoesCadastro.AcaoExtra1ClickHandler += new EventHandler(menuAcoesCadastro_AcaoExtra1ClickHandler);
            menuAcoesCadastro.AcaoExtra3ClickHandler += new EventHandler(menuAcoesCadastro_AcaoExtra3ClickHandler);
            menuAcoesCadastro.AcaoExtra4ClickHandler += new EventHandler(menuAcoesCadastro_AcaoExtra4ClickHandler);

            dialogSelecaoUsuario.ConfirmarClickHandler += new EventHandler(ConfirmarDialogSelecaoUsuario_Click);
            dialogSelecaoUsuarioRemover.ConfirmarClickHandler += new EventHandler(ConfirmarDialogSelecaoUsuarioRemover_Click);
        }


        protected void ConfirmarDialogSelecaoUsuario_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdResultado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkAcao = (CheckBox)row.FindControl("chkAcao");

                    if (chkAcao.Checked)
                    {
                        HiddenField hdIdAgendaHibrida = (HiddenField)row.FindControl("hdIdAgendaHibrida");
                        HiddenField hdTipoAgendamento = (HiddenField)row.FindControl("hdTipoAgendamento");

                        if (!string.IsNullOrWhiteSpace(hdTipoAgendamento.Value)
                            && !string.IsNullOrWhiteSpace(hdIdAgendaHibrida.Value))
                        {
                            if (hdTipoAgendamento.Value == "Prazo")
                            {
                                dtoProcessoPrazo prazo = bllProcessoPrazo.Get(Convert.ToInt32(hdIdAgendaHibrida.Value));

                                //if (compromisso != null)
                                //{
                                //    if (dialogSelecaoUsuario.ddlUsuario.SelectedValue != null
                                //        && dialogSelecaoUsuario.ddlUsuario.SelectedValue != String.Empty)
                                //    {
                                //        int idUsuarioSelecionado = Convert.ToInt32(dialogSelecaoUsuario.ddlUsuario.SelectedValue);

                                //        InserirParticipanteAgenda(idUsuarioSelecionado, compromisso.idAgendaCompromisso);
                                //    }
                                //}
                            }
                            else if (hdTipoAgendamento.Value == "Agenda")
                            {
                                dtoAgendaCompromisso compromisso = bllAgendaCompromisso.Get(Convert.ToInt32(hdIdAgendaHibrida.Value));

                                if (compromisso != null)
                                {
                                    if (dialogSelecaoUsuario.ddlUsuario.SelectedValue != null
                                        && dialogSelecaoUsuario.ddlUsuario.SelectedValue != String.Empty)
                                    {
                                        int idUsuarioSelecionado = Convert.ToInt32(dialogSelecaoUsuario.ddlUsuario.SelectedValue);

                                        InserirParticipanteAgenda(idUsuarioSelecionado, compromisso.idAgendaCompromisso);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            grdResultado.DataBind();
        }

        private void InserirParticipanteAgenda(int idUsuario, int idAgendaCompromisso)
        {
            dtoAgendaUsuario agendaUsuario = new dtoAgendaUsuario();

            agendaUsuario.idAgendaCompromisso = idAgendaCompromisso;
            agendaUsuario.idUsuario = idUsuario;

            bllAgendaUsuario.Insert(agendaUsuario);
        }

        private void RemoverParticipanteAgenda(int idUsuario, int idAgendaCompromisso)
        {
            dtoAgendaUsuario agendaUsuario = new dtoAgendaUsuario();

            agendaUsuario.idAgendaCompromisso = idAgendaCompromisso;
            agendaUsuario.idUsuario = idUsuario;

            bllAgendaUsuario.DeleteByCompromissoUsuario(idAgendaCompromisso, idUsuario);
        }

        protected void ConfirmarDialogSelecaoUsuarioRemover_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdResultado.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkAcao = (CheckBox)row.FindControl("chkAcao");

                    if (chkAcao.Checked)
                    {
                        HiddenField hdIdAgendaHibrida = (HiddenField)row.FindControl("hdIdAgendaHibrida");
                        HiddenField hdTipoAgendamento = (HiddenField)row.FindControl("hdTipoAgendamento");

                        if (!string.IsNullOrWhiteSpace(hdTipoAgendamento.Value)
                            && !string.IsNullOrWhiteSpace(hdIdAgendaHibrida.Value))
                        {
                            if (hdTipoAgendamento.Value == "Prazo")
                            {
                                dtoProcessoPrazo prazo = bllProcessoPrazo.Get(Convert.ToInt32(hdIdAgendaHibrida.Value));

                                //if (compromisso != null)
                                //{
                                //    if (dialogSelecaoUsuario.ddlUsuario.SelectedValue != null
                                //        && dialogSelecaoUsuario.ddlUsuario.SelectedValue != String.Empty)
                                //    {
                                //        int idUsuarioSelecionado = Convert.ToInt32(dialogSelecaoUsuario.ddlUsuario.SelectedValue);

                                //        InserirParticipanteAgenda(idUsuarioSelecionado, compromisso.idAgendaCompromisso);
                                //    }
                                //}
                            }
                            else if (hdTipoAgendamento.Value == "Agenda")
                            {
                                dtoAgendaCompromisso compromisso = bllAgendaCompromisso.Get(Convert.ToInt32(hdIdAgendaHibrida.Value));

                                if (compromisso != null)
                                {
                                    if (dialogSelecaoUsuario.ddlUsuario.SelectedValue != null
                                        && dialogSelecaoUsuario.ddlUsuario.SelectedValue != String.Empty)
                                    {
                                        int idUsuarioSelecionado = Convert.ToInt32(dialogSelecaoUsuario.ddlUsuario.SelectedValue);

                                        RemoverParticipanteAgenda(idUsuarioSelecionado, compromisso.idAgendaCompromisso);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            grdResultado.DataBind();
        }


        protected void Imprimir_Click(object sender, EventArgs e)
        {

            if (Session["IDUSUARIO"] != null
                && Session["IDUSUARIO"].ToString() != String.Empty)
            {
                dtoUsuario usuario = bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()));
                dtoUsuarioPermissao permissao = bllUsuarioPermissao.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()), "tarefa");
                StringBuilder sbQueryString = new StringBuilder();

                dsResultado.SelectParameters.Clear();

                // LISTA USUARIOS
                StringBuilder sbListaUsuarios = new StringBuilder();
                foreach (ListItem Item in rblUsuario.Items)
                {
                    if (Item.Selected)
                    {
                        if (sbListaUsuarios.ToString() != String.Empty)
                            sbListaUsuarios.Append(",");

                        sbListaUsuarios.Append(Item.Value);
                    }
                }

                if (usuario.Administrador)
                {
                    if (sbListaUsuarios.ToString() != String.Empty)
                    {
                        if (sbQueryString.ToString() != String.Empty)
                            sbQueryString.AppendFormat("&");

                        sbQueryString.AppendFormat("listaUsuarios={0}", sbListaUsuarios.ToString());
                    }
                }
                else
                {
                    if (usuario.idUsuario.ToString() != String.Empty)
                    {
                        if (sbQueryString.ToString() != String.Empty)
                            sbQueryString.AppendFormat("&");

                        sbQueryString.AppendFormat("listaUsuarios={0}", usuario.idUsuario.ToString());
                    }
                }

                // TIPO AGENDAMENTO
                StringBuilder sbTipoAgendamento = new StringBuilder();
                foreach (ListItem Item in cblTipoAgendamento.Items)
                {
                    if (Item.Selected)
                    {
                        if (sbTipoAgendamento.ToString() != String.Empty)
                            sbTipoAgendamento.Append(",");

                        sbTipoAgendamento.Append("%27");
                        sbTipoAgendamento.Append(Item.Value);
                        sbTipoAgendamento.Append("%27");
                    }
                }

                if (sbTipoAgendamento.ToString() != String.Empty)
                {
                    if (sbQueryString.ToString() != String.Empty)
                        sbQueryString.AppendFormat("&");
                    
                    sbQueryString.AppendFormat("tipoAgendamento={0}", sbTipoAgendamento.ToString());
                }

                // STATUS
                StringBuilder sbStatus = new StringBuilder();
                foreach (ListItem Item in cblStatus.Items)
                {
                    if (Item.Selected)
                    {
                        if (sbStatus.ToString() != String.Empty)
                            sbStatus.Append(",");

                        sbStatus.Append("%27");
                        sbStatus.Append(Item.Value);
                        sbStatus.Append("%27");
                    }
                }

                if (sbStatus.ToString() != String.Empty)
                {
                    if (sbQueryString.ToString() != String.Empty)
                        sbQueryString.AppendFormat("&");

                    sbQueryString.AppendFormat("Status={0}", sbStatus.ToString());
                }

                if (txtPesquisa.Text != String.Empty)
                {
                    if (sbQueryString.ToString() != String.Empty)
                        sbQueryString.AppendFormat("&");

                    sbQueryString.AppendFormat("termoPesquisa={0}", txtPesquisa.Text);
                }

                StringBuilder sbDatas = new StringBuilder();
                string dataInicioDe = String.Empty;
                string dataInicioAte = String.Empty;

                switch (rblVisualizacaoAgenda.SelectedValue)
                {
                    case "D":
                        dataInicioDe = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
                        dataInicioAte = Calendar1.SelectedDate.ToString("dd/MM/yyyy");

                        lblResultado.InnerText = "Dia " + dataInicioDe;

                        sbDatas.AppendFormat("dataInicioDe={0}&", dataInicioDe);
                        sbDatas.AppendFormat("dataInicioAte={0}", dataInicioAte);

                        break;

                    case "S":
                        dataInicioDe = StartOfWeek(Calendar1.SelectedDate, DayOfWeek.Sunday).ToString("dd/MM/yyyy");
                        dataInicioAte = StartOfWeek(Calendar1.SelectedDate, DayOfWeek.Sunday).AddDays(6).ToString("dd/MM/yyyy");

                        lblResultado.InnerText = "Semana " + dataInicioDe + " - " + dataInicioAte;

                        sbDatas.AppendFormat("dataInicioDe={0}&", dataInicioDe);
                        sbDatas.AppendFormat("dataInicioAte={0}", dataInicioAte);

                        break;

                    case "M":
                        dataInicioDe = FirstDayMonth(Calendar1.SelectedDate).ToString("dd/MM/yyyy");
                        dataInicioAte = LastDayMonth(Calendar1.SelectedDate).ToString("dd/MM/yyyy");

                        lblResultado.InnerText = "Mês " + dataInicioDe + " - " + dataInicioAte;

                        sbDatas.AppendFormat("dataInicioDe={0}&", dataInicioDe);
                        sbDatas.AppendFormat("dataInicioAte={0}", dataInicioAte);

                        break;
                }

                sbQueryString.AppendFormat("&{0}", sbDatas.ToString());

                //((LinkButton)sender).Attributes.Add("target", "_blank");
                //((LinkButton)sender).Attributes.Add("onclick", );

                //Response.Redirect(String.Format(@"http://sistemas.cicrisa.com.br/Servicos/CarregaRelatorio.aspx?{0}", sbQueryString.ToString()));
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NovaJanela", String.Format(@"window.open('http://sistemas.cicrisa.com.br/Servicos/CarregaRelatorio.aspx?{0}');", sbQueryString.ToString()), true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NovaJanela", String.Format(@"window.open('http://sistemas.cicrisa.com.br/Servicos/CarregaRelatorio.aspx');"), true);

                CarregaPesquisa();
            }
        }


        protected void menuAcoesCadastro_AcaoExtra1ClickHandler(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoPrazo.aspx?Origem=Agenda", DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void menuAcoesCadastro_AcaoExtra3ClickHandler(object sender, EventArgs e)
        {
            mpeDialogSelecaoUsuario.Show();
            //Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoPrazo.aspx?Origem=Agenda", DataAccess.Configuracao.getEnderecoVirtualSite()));
        }

        protected void menuAcoesCadastro_AcaoExtra4ClickHandler(object sender, EventArgs e)
        {
            mpeDialogSelecaoUsuarioRemover.Show();
            //Response.Redirect(String.Format("{0}/Paginas/Manutencao/ProcessoPrazo.aspx?Origem=Agenda", DataAccess.Configuracao.getEnderecoVirtualSite()));
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

        protected void grdResultado_RowDataBound(object sender, GridViewRowEventArgs e)
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

                    //string idAgendaHibrida = DataBinder.Eval(e.Row.DataItem, "idAgendaHibrida").ToString();
                    string Location = RetornaPaginaManutencao(DataBinder.Eval(e.Row.DataItem, "tipoAgendamento"), DataBinder.Eval(e.Row.DataItem, "idAgendaHibrida"));
                    //e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    //e.Row.Style["cursor"] = "pointer";
                    for (int i = 1; i < e.Row.Cells.Count - 2; i++)
                    {
                        //e.Row.Cells[i].Attributes["onClick"] = string.Format("location.href = '{0}';", Location);
                        //e.Row.Cells[i].Attributes["onClick"] = string.Format("window.open('{0}', '_parent');", Location);
                        e.Row.Cells[i].Style["cursor"] = "pointer";
                    }
                }
            }
        }

        protected void txtDataInicioInicial_TextChanged(object sender, EventArgs e)
        {
            CarregaPesquisa();
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //txtDataInicioInicial.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
            CarregaPesquisa();
        }

        protected void txtDataInicioFinal_TextChanged(object sender, EventArgs e)
        {
            CarregaPesquisa();
        }

        protected void txtDataTerminoInicial_TextChanged(object sender, EventArgs e)
        {
            CarregaPesquisa();
        }

        protected void txtDataTerminoFinal_TextChanged(object sender, EventArgs e)
        {
            CarregaPesquisa();
        }

        protected void rblUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPesquisa();
        }

        protected void cblStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPesquisa();
        }

        protected void cblTipoAgendamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPesquisa();
        }

        protected void rblVisualizacaoAgenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaPesquisa();
        }

        protected void grdResultado_Sorted(object sender, EventArgs e)
        {
            
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

        public string FormatText(object input)
        {
            if (input != null
                && input.ToString() != String.Empty)
                return input.ToString().Replace("\n", "<br />");
            else
                return "";
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


        protected void lnkConcluirAgendamento_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton lnkConcluirAgendamento = (ImageButton)sender;
            GridViewRow row = (GridViewRow)lnkConcluirAgendamento.NamingContainer;
            HiddenField hdIdAgendaHibrida = (HiddenField)row.FindControl("hdIdAgendaHibrida");
            HiddenField hdTipoAgendamento = (HiddenField)row.FindControl("hdTipoAgendamento");

            if (hdIdAgendaHibrida != null)
            {
                if (hdTipoAgendamento.Value.Trim() == "Agenda")
                {
                    dtoAgendaCompromisso compromisso = bllAgendaCompromisso.Get(Convert.ToInt32(hdIdAgendaHibrida.Value));
                    compromisso.situacaoCompromisso = "C";

                    bllAgendaCompromisso.Update(compromisso);
                }
                else if (hdTipoAgendamento.Value.Trim() == "Prazo")
                {
                    dtoProcessoPrazo prazo = bllProcessoPrazo.Get(Convert.ToInt32(hdIdAgendaHibrida.Value));
                    prazo.situacaoPrazo = "C";
                    prazo.dataConclusao = DateTime.Now;

                    bllProcessoPrazo.Update(prazo);
                }
            }

            grdResultado.DataBind();
        }

    }
}