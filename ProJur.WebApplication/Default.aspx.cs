using ProJur.Business.Bll;
using ProJur.Business.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProJur.WebApplication
{
    public partial class Default : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaPainel();

            Master.litCaminhoPrincipal.Text = "ProJur > ";
            Master.litCaminhoSecundario.Text = "Inicial";

        }



        protected void CarregaDia()
        {
            string diaExtenso = new CultureInfo("pt-BR").DateTimeFormat.GetDayName((DayOfWeek)DateTime.Now.DayOfWeek);
            //lblAgendaDoDia.Text = String.Format("Agenda do dia: {0}, {1}", DateTime.Now.ToString("dd/MM/yyyy"), diaExtenso);
            lblAgendaDoDia.Text = String.Format("{0}, {1}", DateTime.Now.ToString("dd/MM/yyyy"), diaExtenso);
        }

        protected void CarregaPainel()
        {
            CarregaDia();
            CarregaCompromissosAgenda();
            CarregaCompromissosPrazo();
            CarregaAniversariantes();
            CarregaTotalizadores();
        }



        protected void grdAgendaCompromissos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "tipoAgendamento").ToString() == "Agenda")
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "DataHoraInicio") != null)
                        {
                            DateTime dataHoraInicio = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DataHoraInicio").ToString());

                            if (DateTime.Now.Subtract(dataHoraInicio).Ticks > 0)
                                e.Row.Cells[0].Attributes.Add("style", "color: #ff4a3a;");
                        }
                    }


                    //string idVara = DataBinder.Eval(e.Row.DataItem, "idVara").ToString();
                    //string Location = ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() + "/Paginas/Manutencao/Vara.aspx?ID=" + idVara;
                    ////e.Row.Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    ////e.Row.Style["cursor"] = "pointer";
                    //for (int i = 1; i < e.Row.Cells.Count - 1; i++)
                    //{
                    //    e.Row.Cells[i].Attributes["onClick"] = string.Format("javascript:window.open('{0}', '_blank');", Location);
                    //    e.Row.Cells[i].Style["cursor"] = "pointer";
                    //}
                }
            }
        }

        protected void grdAgendaPrazos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "tipoAgendamento").ToString() == "Prazo")
                    {
                        if (DataBinder.Eval(e.Row.DataItem, "DataHoraInicio") != null)
                        {
                            DateTime dataHoraInicio = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "DataHoraInicio").ToString());

                            if (DateTime.Now.Subtract(dataHoraInicio).Days > 0)
                                e.Row.Cells[0].Attributes.Add("style", "color: #ff4a3a;");
                            //e.Row.Cells[0].Attributes.Add("style", "background-color: #f6e5e3; color: #ff4a3a;");
                        }
                    }
                }
            }
        }



        protected void CarregaCompromissosAgenda()
        {
            if (Session["IDUSUARIO"] != null
                && Session["IDUSUARIO"].ToString() != String.Empty)
            {
                dtoUsuario usuario = bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()));

                dsAgendaCompromissos.SelectParameters.Clear();

                dsAgendaCompromissos.SelectParameters.Add("listaUsuarios", usuario.idUsuario.ToString());
                dsAgendaCompromissos.SelectParameters.Add("tipoAgendamento", "'Agenda'");
                dsAgendaCompromissos.SelectParameters.Add("Status", "'P'");
                dsAgendaCompromissos.SelectParameters.Add("termoPesquisa", "");

                //dsAgendaCompromissos.SelectParameters.Add("dataInicioDe", DateTime.Now.ToString("dd/MM/yyyy"));
                dsAgendaCompromissos.SelectParameters.Add("dataInicioDe", String.Empty);
                dsAgendaCompromissos.SelectParameters.Add("dataInicioAte", DateTime.Now.ToString("dd/MM/yyyy"));
                //dsAgendaCompromissos.SelectParameters.Add("dataInicioAte", String.Empty);
                dsAgendaCompromissos.SelectParameters.Add("dataTerminoDe", "");
                //dsAgendaCompromissos.SelectParameters.Add("dataTerminoDe", DateTime.Now.ToString("dd/MM/yyyy"));
                dsAgendaCompromissos.SelectParameters.Add("dataTerminoAte", "");

                dsAgendaCompromissos.SelectParameters.Add("SortExpression", "DataHoraInicio");

                dsAgendaCompromissos.DataBind();
                grdAgendaCompromissos.DataBind();
            }        
        }

        protected void CarregaCompromissosPrazo()
        {
            if (Session["IDUSUARIO"] != null
                && Session["IDUSUARIO"].ToString() != String.Empty)
            {
                dtoUsuario usuario = bllUsuario.Get(Convert.ToInt32(Session["IDUSUARIO"].ToString()));

                dsAgendaPrazos.SelectParameters.Clear();

                dsAgendaPrazos.SelectParameters.Add("listaUsuarios", usuario.idUsuario.ToString());
                dsAgendaPrazos.SelectParameters.Add("tipoAgendamento", "'Prazo'");
                dsAgendaPrazos.SelectParameters.Add("Status", "'P'");
                dsAgendaPrazos.SelectParameters.Add("termoPesquisa", "");

                //dsAgendaPrazos.SelectParameters.Add("dataInicioDe", DateTime.Now.ToString("dd/MM/yyyy"));
                dsAgendaPrazos.SelectParameters.Add("dataInicioDe", String.Empty);
                dsAgendaPrazos.SelectParameters.Add("dataInicioAte", DateTime.Now.ToString("dd/MM/yyyy"));
                dsAgendaPrazos.SelectParameters.Add("dataTerminoDe", "");
                dsAgendaPrazos.SelectParameters.Add("dataTerminoAte", "");

                dsAgendaPrazos.DataBind();
                grdAgendaPrazos.DataBind();
            }
        }

        protected void CarregaAniversariantes()
        {
            dsAniversariantes.SelectParameters.Clear();

            dsAniversariantes.SelectParameters.Add("Dia", DateTime.Now.Day.ToString());
            dsAniversariantes.SelectParameters.Add("Mes", DateTime.Now.Month.ToString());
            dsAniversariantes.SelectParameters.Add("SortExpression", "dataNascimento");

            dsAniversariantes.DataBind();
            grdAniversariantes.DataBind();
        }

        protected void CarregaTotalizadores()
        {
            lblTotalAtendimentos.Text = String.Format("Atendimentos: <b style='font-weight: normal; color: #727376'>{0} encontrado(s)</b>", bllAtendimento.GetAll().Count.ToString());
            lblTotalPessoas.Text = String.Format("Pessoas: <b style='font-weight: normal; color: #727376'>{0} encontrada(s)</b>", bllPessoa.GetAll().Count.ToString());
            lblTotalProcessos.Text = String.Format("Processos: <b style='font-weight: normal; color: #727376'> {0} encontrado(s)</b>", bllProcesso.GetAll().Count.ToString());
        }


        protected string RetornaDescricaoCompromisso(object idAgendaHibrida)
        {
            StringBuilder sbRetorno = new StringBuilder();

            if (idAgendaHibrida != null)
            {
                dtoAgendaHibrida compromisso = bllAgendaHibrida.Get(Convert.ToInt32(idAgendaHibrida), "Agenda");
                dtoAgendaCompromisso agendaCompromisso = bllAgendaCompromisso.Get(Convert.ToInt32(idAgendaHibrida));
                dtoUsuario usuarioAbertura = bllUsuario.Get(agendaCompromisso.idUsuarioCadastro);

                sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/AgendaCompromisso.aspx?ID={1}' target='_blank' style='line-height: 20px'>", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString());

                if (compromisso.DataHoraInicio != null)
                {
                    DateTime dataHoraInicio = Convert.ToDateTime(compromisso.DataHoraInicio);

                    // SE ESTIVER ATRASADO
                    if (DateTime.Now.Subtract(dataHoraInicio).Ticks > 0)
                    {
                        string corDestaque = "#BC9893";
                        string corTitulo = "#BC9893";

                        if (DateTime.Now.ToString("dd/MM/yyyy") != dataHoraInicio.ToString("dd/MM/yyyy"))
                            sbRetorno.AppendFormat("<b style='font-weight: normal; font-size: 12px;'>{0} - {1}</b> <br />", Convert.ToDateTime(compromisso.DataHoraInicio).ToString("dd/MM/yyyy"), Convert.ToDateTime(compromisso.DataHoraInicio).ToString("HH:mm"));
                        else
                            sbRetorno.AppendFormat("<b style='font-weight: normal; font-size: 12px;'>Hoje - {0} hrs</b><br />", Convert.ToDateTime(compromisso.DataHoraInicio).ToString("HH:mm"));

                        dtoPessoa pessoa = bllPessoa.Get(agendaCompromisso.idPessoa);

                        if (pessoa.idPessoa > 0)
                            sbRetorno.AppendFormat("{0}: <b style='font-weight: normal; color: {1}'>{2}</b><br />", pessoa.TipoPessoaDescricao, corDestaque, pessoa.NomeCompletoRazaoSocial);

                        if (compromisso.Descricao != null)
                            sbRetorno.AppendFormat("<b style='font-weight: normal; color: {0}'>{1}</b> <br />", corDestaque, compromisso.Descricao);

                        sbRetorno.AppendFormat("Agendado por: <b style='font-weight: normal; color: {0}'>{1}</b>", corDestaque, usuarioAbertura.nomeCompleto);
                        
                        if (compromisso.Responsaveis != null
                            && compromisso.Responsaveis != String.Empty)
                        { 
                            sbRetorno.AppendFormat("<br />");
                            sbRetorno.AppendFormat("Participantes: <b style='font-weight: normal; color: {0}'>{1}</b> <br />", corDestaque, this.RetornaNomeUsuariosCompromisso(compromisso.idAgendaHibrida));//  compromisso.Responsaveis);
                        }
                    }
                    else // SE NÃO ESTIVER ATRASADO
                    {
                        sbRetorno.AppendFormat("<b style='font-weight: normal; font-size: 12px;'>Hoje - {0} hrs</b><br />", Convert.ToDateTime(compromisso.DataHoraInicio).ToString("HH:mm"));

                        dtoPessoa pessoa = bllPessoa.Get(agendaCompromisso.idPessoa);

                        if (pessoa.idPessoa > 0)
                            sbRetorno.AppendFormat("{0}: <b style='font-weight: normal; color: #727376'>{1}</b><br />", pessoa.TipoPessoaDescricao, pessoa.NomeCompletoRazaoSocial);

                        if (compromisso.Descricao != null)
                            sbRetorno.AppendFormat("<b style='font-weight: normal; color: #727376'>{0}</b> <br />", compromisso.Descricao);

                        sbRetorno.AppendFormat("Agendado por: <b style='font-weight: normal; color: #727376'>{0}</b>", usuarioAbertura.nomeCompleto);

                        if (compromisso.Responsaveis != null
                            && compromisso.Responsaveis != String.Empty)
                        {
                            sbRetorno.AppendFormat("<br />");
                            sbRetorno.AppendFormat("Participantes: <b style='font-weight: normal; color: #727376'>{0}</b> <br />", this.RetornaNomeUsuariosCompromisso(compromisso.idAgendaHibrida));//  compromisso.Responsaveis);
                        }
                            
                    }
                }
               
                sbRetorno.AppendFormat("</a>");
            }

            return sbRetorno.ToString();
        }

        protected string RetornaDescricaoPrazo(object idAgendaHibrida)
        {
            StringBuilder sbRetorno = new StringBuilder();

            if (idAgendaHibrida != null)
            {
                dtoAgendaHibrida prazoAgenda = bllAgendaHibrida.Get(Convert.ToInt32(idAgendaHibrida), "Prazo");
                dtoProcessoPrazo processoPrazo = bllProcessoPrazo.Get(Convert.ToInt32(idAgendaHibrida));

                //sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}' target='_blank' style='line-height: 20px'>", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString());

                if (prazoAgenda.DataHoraInicio != null)
                {
                    DateTime dataHoraInicio = Convert.ToDateTime(prazoAgenda.DataHoraInicio);
                    dtoTipoPrazoProcessual tipoPrazoProcessual = bllTipoPrazoProcessual.Get(processoPrazo.idTipoPrazo);
                    string numeroProcesso = bllProcesso.Get(processoPrazo.idProcesso).numeroProcesso;
                    string descricaoTipoPrazoProcessual = tipoPrazoProcessual.Descricao;
                    dtoProcesso processo = bllProcesso.Get(processoPrazo.idProcesso);

                    if (descricaoTipoPrazoProcessual == null || descricaoTipoPrazoProcessual.Trim() == String.Empty)
                        descricaoTipoPrazoProcessual = "Nenhum informado";

                    if (numeroProcesso == null || numeroProcesso.Trim() == String.Empty)
                        numeroProcesso = "Nenhum informado";

                    if (DateTime.Now.Subtract(dataHoraInicio).Days > 0)
                    {
                        string corDestaque = "#BC9893";
                        string corTitulo = "#BC9893";

                        sbRetorno.AppendFormat("<b style='font-weight: normal; font-size: 12px;'>{0}</b> <br />", Convert.ToDateTime(prazoAgenda.DataHoraInicio).ToString("dd/MM/yyyy"));

                        dtoPessoa pessoa = bllPessoa.Get(processo.idPessoaCliente);

                        if (pessoa.idPessoa > 0)
                            sbRetorno.AppendFormat("{0}: <b style='font-weight: normal; color: {1}'>{2}</b><br />", pessoa.TipoPessoaDescricao, corDestaque, pessoa.NomeCompletoRazaoSocial);

                        if (prazoAgenda.Descricao != null)
                            sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}&IdProcesso={2}' target='_blank' style='line-height: 20px'><b style='font-weight: normal; color: {3}'>{4}</b></a><br />", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString(), processoPrazo.idProcesso, corDestaque, prazoAgenda.Descricao);

                        sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}&IdProcesso={2}' target='_blank' style='line-height: 20px'>Prazo: <b style='font-weight: normal; color: {3}'>{4}</b></a><br />", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString(), processoPrazo.idProcesso, corDestaque, descricaoTipoPrazoProcessual);

                        sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/Processo.aspx?ID={1}' target='_blank' style='line-height: 20px'>Nº do Processo: <b style='font-weight: normal; color: {2}'>{3}</b></a><br />", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), processoPrazo.idProcesso.ToString(), corDestaque, numeroProcesso);

                        //if (prazoAgenda.Responsaveis != null
                        //    && prazoAgenda.Responsaveis != String.Empty)
                        //    sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}&IdProcesso={2}' target='_blank' style='line-height: 20px'>Advogado Responsável: <b style='font-weight: normal; color: {3}'>{4}</b></a><br />", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString(), processoPrazo.idProcesso, corDestaque, prazoAgenda.Responsaveis);
                    }
                    else
                    {
                        sbRetorno.AppendFormat("<b style='font-weight: normal; font-size: 12px;'>Hoje</b> <br />");

                        dtoPessoa pessoa = bllPessoa.Get(processo.idPessoaCliente);

                        if (pessoa.idPessoa > 0)
                            sbRetorno.AppendFormat("{0}: <b style='font-weight: normal; color: #727376'>{1}</b><br />", pessoa.TipoPessoaDescricao, pessoa.NomeCompletoRazaoSocial);

                        if (prazoAgenda.Descricao != null)
                            sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}&IdProcesso={2}' target='_blank' style='line-height: 20px'><b style='font-weight: normal; color: #727376'>{3}</b></a><br />", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString(), processoPrazo.idProcesso, prazoAgenda.Descricao);


                        sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}&IdProcesso={2}' target='_blank' style='line-height: 20px'>Prazo: <b style='font-weight: normal; color: #727376'>{3}</b></a><br />", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString(), processoPrazo.idProcesso, descricaoTipoPrazoProcessual);
                        sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/Processo.aspx?ID={1}' target='_blank' style='line-height: 20px'>Nº do Processo: <b style='font-weight: normal; color: #727376'>{2}</b></a><br />", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), processoPrazo.idProcesso.ToString(), numeroProcesso);

                        //if (prazoAgenda.Responsaveis != null
                        //    && prazoAgenda.Responsaveis != String.Empty)
                        //    sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/ProcessoPrazo.aspx?ID={1}&IdProcesso={2}' target='_blank' style='line-height: 20px'>Advogado Responsável: <b style='font-weight: normal; color: #727376'>{3}</b></a><br />", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idAgendaHibrida.ToString(), processoPrazo.idProcesso, prazoAgenda.Responsaveis);

                        


                    }
                }
            }

            return sbRetorno.ToString();
        }

        protected string RetornaDescricaoAniversariante(object idPessoa)
        {
            StringBuilder sbRetorno = new StringBuilder();

            if (idPessoa != null)
            {
                dtoPessoa pessoa = bllPessoa.Get(Convert.ToInt32(idPessoa));

                sbRetorno.AppendFormat("<a href='{0}/Paginas/Manutencao/Pessoa.aspx?ID={1}' target='_blank' style='line-height: 20px'>", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), idPessoa.ToString());

                if (pessoa.NomeCompletoRazaoSocial != null)
                    sbRetorno.AppendFormat("{0} <br />", pessoa.NomeCompletoRazaoSocial);

                sbRetorno.AppendFormat("</a>");
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
            }

            grdAgendaCompromissos.DataBind();
        }

        protected void lnkConcluirPrazo_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton lnkConcluirAgendamento = (ImageButton)sender;
            GridViewRow row = (GridViewRow)lnkConcluirAgendamento.NamingContainer;
            HiddenField hdIdAgendaHibrida = (HiddenField)row.FindControl("hdIdAgendaHibrida");
            HiddenField hdTipoAgendamento = (HiddenField)row.FindControl("hdTipoAgendamento");

            if (hdIdAgendaHibrida != null)
            {
                if (hdTipoAgendamento.Value.Trim() == "Prazo")
                {
                    dtoProcessoPrazo prazo = bllProcessoPrazo.Get(Convert.ToInt32(hdIdAgendaHibrida.Value));
                    prazo.situacaoPrazo = "C";
                    prazo.dataConclusao = DateTime.Now;

                    bllProcessoPrazo.Update(prazo);
                }
            }

            grdAgendaPrazos.DataBind();
        }
    }
}