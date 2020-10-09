using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.Business.Dto;
using ProJur.Business.Bll;
using System.ComponentModel;
using System.Drawing.Design;

namespace ProJur.WebApplication.Controls
{
    public partial class menuAcoesCadastro : System.Web.UI.UserControl
    {
        public event EventHandler ExcluirClickHandler;
        public event EventHandler ImprimirClickHandler;
        public event EventHandler AcaoExtra1ClickHandler;
        public event EventHandler AcaoExtra2ClickHandler;
        public event EventHandler AcaoExtra3ClickHandler;
        public event EventHandler AcaoExtra4ClickHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lnkNovo.Text = this.NovoTexto;
                lnkNovo.NavigateUrl = String.Format("{0}{1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), this.NovoUrl);
            }

            AplicaPermissoes();
        }

        protected void OnExcluirClick(object sender, EventArgs e)
        {
            if (ExcluirClickHandler != null)
                ExcluirClickHandler(this, e);
        }

        protected void OnImprimirClick(object sender, EventArgs e)
        {
            if (ImprimirClickHandler != null)
                ImprimirClickHandler(this, e);
        }

        protected void OnAcaoExtra1Click(object sender, EventArgs e)
        {
            if (AcaoExtra1ClickHandler != null)
                AcaoExtra1ClickHandler(this, e);
        }

        protected void OnAcaoExtra2Click(object sender, EventArgs e)
        {
            if (AcaoExtra2ClickHandler != null)
                AcaoExtra2ClickHandler(this, e);
        }

        protected void OnAcaoExtra3Click(object sender, EventArgs e)
        {
            if (AcaoExtra3ClickHandler != null)
                AcaoExtra3ClickHandler(this, e);
        }

        protected void OnAcaoExtra4Click(object sender, EventArgs e)
        {
            if (AcaoExtra4ClickHandler != null)
                AcaoExtra4ClickHandler(this, e);
        }

        protected void btnImprimirListagem_Click(object sender, EventArgs e)
        {
            OnImprimirClick(sender, e);
        }

        protected void btnExcluirSelecionados_Click(object sender, EventArgs e)
        {
            OnExcluirClick(sender, e);
        }

        protected void btnAcaoExtra1_Click(object sender, EventArgs e)
        {
            OnAcaoExtra1Click(sender, e);
        }

        protected void btnAcaoExtra2_Click(object sender, EventArgs e)
        {
            OnAcaoExtra2Click(sender, e);
        }

        protected void btnAcaoExtra3_Click(object sender, EventArgs e)
        {
            OnAcaoExtra3Click(sender, e);
        }

        protected void btnAcaoExtra4_Click(object sender, EventArgs e)
        {
            OnAcaoExtra4Click(sender, e);
        }

        private void AplicaPermissoes()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                dtoUsuario usuario = bllUsuario.GetByLogin(HttpContext.Current.User.Identity.Name);
                dtoUsuarioPermissao permissao = new dtoUsuarioPermissao();

                if (!usuario.Administrador)
                {
                    switch (this.Page.ToString())
                    {
                        case "ASP.paginas_cadastro_menu_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "menu");
                            break;

                        case "ASP.paginas_cadastro_usuario_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "usuario");
                            break;

                        case "ASP.paginas_cadastro_agenda_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "agenda");
                            break;

                        case "ASP.paginas_cadastro_areaprocessual_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "areaprocessual");
                            break;

                        case "ASP.paginas_cadastro_atendimento_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "atendimento");
                            break;

                        case "ASP.paginas_cadastro_categoriapeca_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "categoriapeca");
                            break;

                        case "ASP.paginas_cadastro_cidade_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "cidade");
                            break;

                        case "ASP.paginas_cadastro_comarca_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "comarca");
                            break;

                        case "ASP.paginas_cadastro_estado_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "estado");
                            break;

                        case "ASP.paginas_cadastro_faseprocessual_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "faseprocessual");
                            break;

                        case "ASP.paginas_cadastro_instancia_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "instancia");
                            break;

                        case "ASP.paginas_cadastro_pessoa_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "pessoa");
                            break;

                        case "ASP.paginas_cadastro_processo_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "processo");
                            break;

                        case "ASP.paginas_cadastro_situacaoprocesso_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "situacaoprocesso");
                            break;

                        case "ASP.paginas_cadastro_tarefa_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "tarefa");
                            break;

                        case "ASP.paginas_cadastro_tipoacao_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "tipoacao");
                            break;

                        case "ASP.paginas_cadastro_tipoprazoprocessual_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "tipoprazoprocessual");
                            break;

                        case "ASP.paginas_cadastro_vara_aspx":
                            permissao = bllUsuarioPermissao.Get(usuario.idUsuario, "vara");
                            break;

                    }

                    if (permissao != null)
                    {
                        liExcluir.Visible = permissao.Excluir && MostrarExcluir;
                        liImprimir.Visible = permissao.Imprimir && MostrarImprimir;
                        liNovo.Visible = permissao.Novo && MostrarIncluir;
                        liAcaoExtra1.Visible = permissao.Especial && MostrarAcaoExtra1;
                        liAcaoExtra2.Visible = permissao.Especial && MostrarAcaoExtra2;
                        liAcaoExtra3.Visible = permissao.Especial && MostrarAcaoExtra3;
                        liAcaoExtra4.Visible = permissao.Especial && MostrarAcaoExtra4;
                    }
                }
                else
                {
                    liExcluir.Visible = MostrarExcluir;
                    liImprimir.Visible = MostrarImprimir;
                    liNovo.Visible = MostrarIncluir;
                    liAcaoExtra1.Visible = MostrarAcaoExtra1;
                    liAcaoExtra2.Visible = MostrarAcaoExtra2;
                    liAcaoExtra3.Visible = MostrarAcaoExtra3;
                    liAcaoExtra4.Visible = MostrarAcaoExtra4;
                }
            }
        }

        [UrlProperty("*.aspx")]
        [DefaultValue("")]
        [Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string NovoUrl { get; set; }

        [DefaultValue("")]
        public string NovoTexto { get; set; }

        [DefaultValue("")]
        public string ImprimirTexto { get; set; }

        [DefaultValue("")]
        public string AcaoExtra1Texto { get; set; }

        [DefaultValue("")]
        public string AcaoExtra1ValidationGroup { get; set; }

        [DefaultValue("")]
        public string AcaoExtra2Texto { get; set; }

        [DefaultValue("")]
        public string AcaoExtra3Texto { get; set; }

        [DefaultValue("")]
        public string AcaoExtra4Texto { get; set; }

        bool _MostrarIncluir = true;
        public bool MostrarIncluir
        {
            get
            {
                return _MostrarIncluir;
            }

            set
            {
                _MostrarIncluir = value;
            }
        }

        bool _MostrarExcluir = true;
        public bool MostrarExcluir
        {
            get
            {
                return _MostrarExcluir;
            }

            set
            {
                _MostrarExcluir = value;
            }
        }

        bool _MostrarAcaoExtra1 = false;
        public bool MostrarAcaoExtra1
        {
            get
            {
                return _MostrarAcaoExtra1;
            }

            set
            {
                _MostrarAcaoExtra1 = value;
            }
        }

        bool _MostrarAcaoExtra2 = false;
        public bool MostrarAcaoExtra2
        {
            get
            {
                return _MostrarAcaoExtra2;
            }

            set
            {
                _MostrarAcaoExtra2 = value;
            }
        }

        bool _MostrarAcaoExtra3 = false;
        public bool MostrarAcaoExtra3
        {
            get
            {
                return _MostrarAcaoExtra3;
            }

            set
            {
                _MostrarAcaoExtra3 = value;
            }
        }

        bool _MostrarAcaoExtra4 = false;
        public bool MostrarAcaoExtra4
        {
            get
            {
                return _MostrarAcaoExtra4;
            }

            set
            {
                _MostrarAcaoExtra4 = value;
            }
        }

        bool _MostrarImprimir = false;
        public bool MostrarImprimir
        {
            get
            {
                return _MostrarImprimir;
            }

            set
            {
                _MostrarImprimir = value;
            }
        }

        string _UrlImagemIncluir = "adicionar-registro.png";
        public string UrlImagemIncluir
        {
            get
            {
                return _UrlImagemIncluir;
            }

            set
            {
                _UrlImagemIncluir = value;
            }
        }

    }
}