<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Cadastro.Menu" %>
<%@ Register src="~/Controls/menuAcoesCadastro.ascx" tagname="menuAcoesCadastro" tagprefix="ProJur" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/cadastro.main.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="cadastroFiltros">
        <div id="cadastroFiltrosTitulo">Filtros:</div>
        <div id="cadastroFiltrosCampos">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPesquisa" runat="server" CssClass="campoTexto" 
                            Width="475px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnPesquisar" runat="server" class="botao" 
                            ImageUrl="~/Images/Pesquisar.png" onclick="btnPesquisar_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="cadastroFiltrosMaisOpcoesLink">
            <a href="" title="Mais opções de pesquisa">
                Mais opções de pesquisa
            </a>
        </div>
    </div>

    <div id="cadastroAcoes">
        <div id="cadastroAcoesTitulo">Ações:</div>
        <div id="cadastroAcoesListaItens">
            <ProJur:menuAcoesCadastro ID="menuAcoesCadastro" runat="server" NovoUrl="/Paginas/Manutencao/Menu.aspx" NovoTexto="Incluir menu" />
        </div>
    </div>

    <div style="clear:both"></div>

    <div id="cadastroOpcoesPesquisa">
        <label class="titulo">Bloqueados:</label>
        <asp:RadioButtonList ID="rblOpcoesBloqueados" runat="server" CellPadding="2" 
            CellSpacing="2">
            <asp:ListItem Selected="True">Todos</asp:ListItem>
            <asp:ListItem Value="1">Somente bloqueados</asp:ListItem>
            <asp:ListItem Value="0">Somente desbloqueados</asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <div id="cadastroCorpo">
        <div id="cadastroCorpoTitulo">
            Resultado:
        </div>

        <div id="cadastroCorpoResultado">
        
            <asp:TreeView ID="tvwMenu" runat="server" ShowCheckBoxes="Leaf"
                ontreenodecheckchanged="tvwMenu_TreeNodeCheckChanged" NodeIndent="10" 
                ontreenodedatabound="tvwMenu_TreeNodeDataBound" 
                ontreenodepopulate="tvwMenu_TreeNodePopulate" >
                               
                <DataBindings>
                    <asp:TreeNodeBinding DataMember="System.Data.DataRowView" 
                        TextField="Descricao" ValueField="idMenu"  />
                        
                </DataBindings>

                <LeafNodeStyle CssClass="TreeViewLeafNode" />
                <ParentNodeStyle Font-Bold="True" CssClass="TreeViewParentNode" />
                <RootNodeStyle Font-Bold="True" CssClass="TreeViewRootNode" />
            </asp:TreeView>

        </div>

    </div>

</asp:Content>
