using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProJur.DataAccess;
using System.Data;
using System.Text;
using ProJur.Business.Bll;
using System.Collections;

namespace ProJur.WebApplication.Paginas.Cadastro
{
    public partial class Menu : System.Web.UI.Page
    {
        ArrayList arNodesSelecionados = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaDados();

            InicializaEventos();

            Master.litCaminhoPrincipal.Text = "Gerenciador > Cadastro > ";
            Master.litCaminhoSecundario.Text = "Menu";
        }

        public void CarregaDados()
        {
            DataSet dsMenu = bllMenu.GetDataSet();

            tvwMenu.DataSource = new HierarchicalDataSet(dsMenu, "idMenu", "idMenuPai");
            tvwMenu.DataBind();
        }

        protected void btnExcluirSelecionados_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < arNodesSelecionados.Count; i++)
            {
                bllMenu.Delete(Convert.ToInt32(arNodesSelecionados[i]));
            }

            CarregaDados();
        }

        protected void tvwMenu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.Checked)
                arNodesSelecionados.Add(e.Node.Value.ToString());
        }

        protected void tvwMenu_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
        {
            e.Node.NavigateUrl = String.Format("{0}/Paginas/Manutencao/Menu.aspx?ID={1}", ProJur.DataAccess.Configuracao.getEnderecoVirtualSite(), e.Node.Value);
        }

        protected void btnPesquisar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void tvwMenu_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {

        }

        private void InicializaEventos()
        {
            menuAcoesCadastro.ExcluirClickHandler += new EventHandler(btnExcluirSelecionados_Click);
            menuAcoesCadastro.ImprimirClickHandler += new EventHandler(Imprimir_Click);
        }

        protected void Imprimir_Click(object sender, EventArgs e)
        {

        }

    }
}