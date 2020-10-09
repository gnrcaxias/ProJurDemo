using ProJur.Business.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProJur.WebApplication.Paginas.Manutencao
{
    public partial class BackupConfiguracaoFTP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                if (bllBackupConfiguracaoFTP.Get(1) == null)
                {
                    dvManutencao.ChangeMode(DetailsViewMode.Insert);
                    menuAcoes.AlteraEstadoCRUD(WebApplication.Controls.menuAcoesManutencao.estadoCRUD.Inserir);
                }
                else
                {
                    dvManutencao.ChangeMode(DetailsViewMode.ReadOnly);
                    menuAcoes.AlteraEstadoCRUD(WebApplication.Controls.menuAcoesManutencao.estadoCRUD.Visualizacao);
                }
            }

            InicializaEventos();
            InicializaDefaultButton();

            menuAcoes.ExibirBotaoExcluir = false;
            menuAcoes.ExibirBotaoPesquisar = true;
            menuAcoes.ExibirBotaoNovo = false;

            Master.litCaminhoPrincipal.Text = "ProJur > Gerenciador > ";
            Master.litCaminhoSecundario.Text = "Configuração FTP do Backup";
        }

        protected void InicializaDefaultButton()
        {
            MasterPage myMasterPage = (MasterPage)Page.Master;
            System.Web.UI.HtmlControls.HtmlForm myForm = (System.Web.UI.HtmlControls.HtmlForm)myMasterPage.FindControl("form1");
            myForm.DefaultButton = "ctl00$MainContent$menuAcoes$btnGravar";
        }

        private void InicializaEventos()
        {
            menuAcoes.EditarClickHandler += new EventHandler(Editar_Click);
            menuAcoes.SalvarClickHandler += new EventHandler(Salvar_Click);
            menuAcoes.CancelarClickHandler += new EventHandler(Cancelar_Click);
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            dvManutencao.ChangeMode(DetailsViewMode.Edit);
        }

        protected void Cancelar_Click(object sender, EventArgs e)
        {
            if (bllBackupConfiguracaoFTP.Get(1) != null)
                dvManutencao.ChangeMode(DetailsViewMode.ReadOnly);
            else
                Response.Redirect("~/Paginas/Cadastro/Backup.aspx");
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dvManutencao.CurrentMode == DetailsViewMode.Edit)
                    dvManutencao.UpdateItem(false);
                else
                    dvManutencao.InsertItem(true);

                menuAcoes.AlteraEstadoCRUD(WebApplication.Controls.menuAcoesManutencao.estadoCRUD.Visualizacao);
            }
            catch (Exception Ex)
            { }
        }


    }
}