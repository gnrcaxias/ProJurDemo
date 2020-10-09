using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing.Design;
using ProJur.Business.Bll;
using ProJur.Business.Dto;

namespace ProJur.WebApplication.Controls
{
    public partial class menuAcoesSubManutencao : System.Web.UI.UserControl
    {

        public enum estadoCRUD
        {
            Inserir,
            Visualizacao,
            Atualizar,
            Excluir
        }

        public event EventHandler EditarClickHandler;
        public event EventHandler ExcluirClickHandler;
        public event EventHandler SalvarClickHandler;
        public event EventHandler CancelarClickHandler;
        public event EventHandler ImprimirClickHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] == null
                    || Request.QueryString["ID"].Trim() == String.Empty)
                    AlteraEstadoCRUD(estadoCRUD.Inserir);
                else
                    AlteraEstadoCRUD(estadoCRUD.Visualizacao);
            }

            AplicaPermissoes();
        }

        protected void OnEditarClick(object sender, EventArgs e)
        {
            if (EditarClickHandler != null)
                EditarClickHandler(this, e);
        }

        protected void OnExcluirClick(object sender, EventArgs e)
        {
            if (ExcluirClickHandler != null)
                ExcluirClickHandler(this, e);
        }

        protected void OnSalvarClick(object sender, EventArgs e)
        {
            if (SalvarClickHandler != null)
                SalvarClickHandler(this, e);
        }

        protected void OnCancelarClick(object sender, EventArgs e)
        {
            if (CancelarClickHandler != null)
                CancelarClickHandler(this, e);
        }

        protected void OnImprimirClick(object sender, EventArgs e)
        {
            if (ImprimirClickHandler != null)
                ImprimirClickHandler(this, e);
        }

        protected void btnGravar_Click(object sender, ImageClickEventArgs e)
        {
            OnSalvarClick(sender, e);

            if (Page.IsValid)
                AlteraEstadoCRUD(estadoCRUD.Visualizacao);
        }

        protected void btnEditar_Click(object sender, ImageClickEventArgs e)
        {
            OnEditarClick(sender, e);

            if (Page.IsValid)
                AlteraEstadoCRUD(estadoCRUD.Atualizar);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            OnCancelarClick(sender, e);
            AlteraEstadoCRUD(estadoCRUD.Visualizacao);
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            OnImprimirClick(sender, e);
            AlteraEstadoCRUD(estadoCRUD.Visualizacao);
        }

        protected void btnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            OnExcluirClick(sender, e);
        }

        public estadoCRUD currentCRUD
        {
            get;
            set;
        }

        public void AlteraEstadoCRUD(estadoCRUD novoEstadoCRUD)
        {
            currentCRUD = novoEstadoCRUD;

            switch (currentCRUD)
            {
                case estadoCRUD.Inserir:
                case estadoCRUD.Atualizar:

                    liGravar.Visible = true;
                    liEditar.Visible = false;
                    liCancelar.Visible = true;
                    liVoltar.Visible = false;
                    liExcluir.Visible = false;

                    litModo.Text = "[ Modo de edição ]";

                    break;

                case estadoCRUD.Visualizacao:

                    liGravar.Visible = false;
                    liCancelar.Visible = false;
                    liEditar.Visible = true;
                    liVoltar.Visible = true;
                    liExcluir.Visible = true;

                    litModo.Text = "[ Modo de visualização ]";

                    break;
            }

            AplicaPermissoes();
        }

        public void AplicaPermissoes()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                dtoUsuario usuario = bllUsuario.GetByLogin(HttpContext.Current.User.Identity.Name);
                dtoUsuarioPermissao permissao = new dtoUsuarioPermissao();

                if (!usuario.Administrador)
                {
                    switch (this.Page.ToString())
                    {
                        case "ASP.paginas_manutencao_menu_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "menu");
                            break;

                        case "ASP.paginas_manutencao_usuario_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "usuario");
                            break;

                        case "ASP.paginas_manutencao_agenda_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "agenda");
                            break;

                        case "ASP.paginas_manutencao_areaprocessual_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "areaprocessual");
                            break;

                        case "ASP.paginas_manutencao_atendimento_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "atendimento");
                            break;

                        case "ASP.paginas_manutencao_categoriapeca_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "categoriapeca");
                            break;

                        case "ASP.paginas_manutencao_cidade_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "cidade");
                            break;

                        case "ASP.paginas_manutencao_comarca_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "comarca");
                            break;

                        case "ASP.paginas_manutencao_estado_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "estado");
                            break;

                        case "ASP.paginas_manutencao_faseprocessual_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "faseprocessual");
                            break;

                        case "ASP.paginas_manutencao_instancia_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "instancia");
                            break;

                        case "ASP.paginas_manutencao_pessoa_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "pessoa");
                            break;

                        case "ASP.paginas_manutencao_processo_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                        case "ASP.paginas_manutencao_situacaoprocesso_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "situacaoprocesso");
                            break;

                        case "ASP.paginas_manutencao_tarefa_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "tarefa");
                            break;

                        case "ASP.paginas_manutencao_tipoacao_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "tipoacao");
                            break;

                        case "ASP.paginas_manutencao_tipoprazoprocessual_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "tipoprazoprocessual");
                            break;

                        case "ASP.paginas_manutencao_vara_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "vara");
                            break;

                        case "ASP.paginas_manutencao_processoandamento_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                        case "ASP.paginas_manutencao_processoadvogado_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                        case "ASP.paginas_manutencao_processoapenso_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                        case "ASP.paginas_manutencao_processodespesa_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                        case "ASP.paginas_manutencao_processoparte_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                        case "ASP.paginas_manutencao_processopeca_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                        case "ASP.paginas_manutencao_processoprazo_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                        case "ASP.paginas_manutencao_processoterceiro_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                    }

                    if (permissao != null)
                    {
                        switch (currentCRUD)
                        {
                            case estadoCRUD.Inserir:

                                liGravar.Visible = permissao.Novo;
                                liEditar.Visible = false;
                                liCancelar.Visible = true;
                                liVoltar.Visible = false;
                                liExcluir.Visible = false;

                                break;

                            case estadoCRUD.Atualizar:

                                liGravar.Visible = permissao.Alterar;
                                liEditar.Visible = false;
                                liCancelar.Visible = true;
                                liVoltar.Visible = false;
                                liExcluir.Visible = false;

                                break;

                            case estadoCRUD.Visualizacao:

                                liGravar.Visible = false;
                                liCancelar.Visible = false;
                                liEditar.Visible = permissao.Alterar;
                                liVoltar.Visible = true;
                                liExcluir.Visible = permissao.Excluir;

                                break;
                        }
                    }

                    if (DesabilitarEditarExcluir)
                    {
                        liEditar.Visible = false;
                        liExcluir.Visible = false;
                    }



                }
            }
        }

        [UrlProperty("*.aspx")]
        [DefaultValue("")]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string VoltarUrl { get; set; }

        public bool DesabilitarEditarExcluir { get; set; }

    }
}