<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Backup.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Cadastro.Backup" %>
<%@ Register src="~/Controls/menuAcoesCadastro.ascx" tagname="menuAcoesCadastro" tagprefix="uc1" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/cadastro.main.css" rel="stylesheet" type="text/css" />
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
            <uc1:menuAcoesCadastro ID="menuAcoesCadastro" runat="server" NovoUrl="/Paginas/Manutencao/BackupConfiguracaoFTP.aspx?ID=1" NovoTexto="Configuração FTP" />
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
                GridLines="None" AllowSorting="True"  CssClass="GridView" DataSourceID="dsResultado"
                PageSize="5" ShowFooter="False">

                <EmptyDataTemplate>
                    <center>Nenhum backup encontrado</center>
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
                            <asp:HiddenField ID="hdNomeArquivo" runat="server" Value='<%# Eval("nomeArquivo") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Arquivo" SortExpression="nomeArquivo">

                        <ItemTemplate>
<%--                            <asp:LinkButton ID="linkDownloadArquivo" runat="server" CommandArgument='<%# Eval("nomeArquivo") %>' OnClick="linkDownloadArquivo_Click">
                                <%# Eval("nomeArquivo") %>
                            </asp:LinkButton>--%>

                            <a class="botaoAcao" href='<%# Eval("caminhoHttp") %><%# Eval("nomeArquivo") %>' title="Baixar arquivo" >
                                <%# Eval("nomeArquivo") %>
                            </a>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:BoundField  DataField="dataArquivo" HeaderText="Data / Hora" 
                        SortExpression="dataArquivo" HeaderStyle-Width="65px" >
                        <HeaderStyle Width="150px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="tamanhoArquivo" HeaderText="Tamanho" 
                        SortExpression="tamanhoArquivo" DataFormatString="{0:N0} MB" >
                        <HeaderStyle Width="150px"></HeaderStyle>
                    </asp:BoundField>
                    
                    <asp:TemplateField ShowHeader="False">
                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkRemoverArquivoFTP" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                        ToolTip="Remover arquivo do ftp"   OnClick="lnkRemoverArquivoFTP_Click"
                                        OnClientClick="return confirm('Deseja realmente remover este arquivo?');"  />
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
                <br />
                <asp:Literal ID="litTamanhoTotal" runat="server"></asp:Literal>
            </div>

            <asp:ObjectDataSource ID="dsResultado" runat="server" 
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                TypeName="ProJur.Business.Bll.bllBackup" 
                onselected="dsResultado_Selected">                
            </asp:ObjectDataSource>
        </div>

    </div>

</asp:Content>
