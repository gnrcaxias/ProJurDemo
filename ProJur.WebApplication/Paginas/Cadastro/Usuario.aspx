<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Cadastro.Usuario" %>
<%@ Register src="~/Controls/menuAcoesCadastro.ascx" tagname="menuAcoesCadastro" tagprefix="ProJur" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/cadastro.main.css" rel="stylesheet" type="text/css" />
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/gridview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/gridview.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $('span.checkall input').on('click', function () {
                $(this).closest('table').find(':checkbox').prop('checked', this.checked);
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="cadastroFiltros">
        <div id="cadastroFiltrosTitulo">Filtros:</div>
        <div id="cadastroFiltrosCampos">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPesquisa" runat="server" CssClass="campoTexto" placeholder="Digite aqui os dados para pesquisa, mínimo 3 letras"
                            Width="475px"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="txtPesquisa_AutoCompleteExtender" runat="server" 
                            DelimiterCharacters="" Enabled="True" ServiceMethod="GetCompletionList" TargetControlID="txtPesquisa"
                            CompletionListCssClass="AutoExtender"  UseContextKey="true" 
                            CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                        </ajaxToolkit:AutoCompleteExtender>
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
            <ProJur:menuAcoesCadastro ID="menuAcoesCadastro" runat="server" NovoUrl="/Paginas/Manutencao/Usuario.aspx" NovoTexto="Incluir usuário" />
        </div>
    </div>

    <div style="clear:both"></div>

    <div id="cadastroOpcoesPesquisa">

    </div>

    <div id="cadastroCorpo">
        <div id="cadastroCorpoTitulo">
            Resultado:
        </div>

        <div id="cadastroCorpoResultado">

            <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" 
                DataSourceID="dsResultado"   OnRowDataBound="grdResultado_RowDataBound"
                GridLines="None" AllowSorting="True"  CssClass="GridView"
                PageSize="5" ShowFooter="False" >
                         
                <EmptyDataTemplate>
                    <center>Nenhum usuário encontrado</center>
                </EmptyDataTemplate>
                                                                         
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                        <HeaderStyle HorizontalAlign="Center" Width="32px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkExcluir" runat="server" CssClass="checkall">
                            </asp:CheckBox>                        
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:CheckBox ID="chkExcluir" runat="server">
                            </asp:CheckBox>
                            <asp:HiddenField ID="hdIdUsuario" runat="server" Value='<%# Eval("idUsuario") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:BoundField DataField="idUsuario" HeaderText="Código" 
                        SortExpression="idUsuario" HeaderStyle-Width="65px" >
                        <HeaderStyle Width="65px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="nomeCompleto" HeaderText="Nome" 
                        SortExpression="nomeCompleto" HeaderStyle-Width="160px" >
                        <HeaderStyle Width="160px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Usuario" HeaderText="Usuario" 
                        SortExpression="Usuario" HeaderStyle-Width="150px" >
                        <HeaderStyle Width="150px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    
                    <asp:TemplateField ShowHeader="false">
                        
                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />

                        <ItemTemplate>
                            <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idUsuario", "/Paginas/Manutencao/Usuario.aspx?ID={0} ") %>' title="Visualizar os dados" >
                                <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Visualizar.png" alt="Visualizar" border="0" />
                            </a>
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

                <HeaderStyle BackColor="#F1F1F2" /> 
                <SortedAscendingHeaderStyle CssClass="GridViewHeaderAscendingSort" />
                <SortedDescendingHeaderStyle CssClass="GridViewHeaderDescendingSort" />

                <PagerSettings FirstPageText="Primeiro" LastPageText="Último" 
                    Mode="NumericFirstLast" NextPageText="Próximo" PreviousPageText="Anterior" />
                <PagerStyle HorizontalAlign="Right" />
                <SortedAscendingCellStyle CssClass="GridViewCellSort" />
                <SortedDescendingCellStyle CssClass="GridViewCellSort" />

                <HeaderStyle HorizontalAlign="Left" />

            </asp:GridView>

            <div id="rodape">
                <asp:Literal ID="litTotalRegistros" runat="server"></asp:Literal>
            </div>

            <asp:ObjectDataSource ID="dsResultado" runat="server" 
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                TypeName="ProJur.Business.Bll.bllUsuario" 
                onselected="dsResultado_Selected">                
            </asp:ObjectDataSource>
        </div>

    </div>

</asp:Content>
