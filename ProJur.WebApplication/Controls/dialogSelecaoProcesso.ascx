<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dialogSelecaoProcesso.ascx.cs" Inherits="ProJur.WebApplication.Controls.dialogSelecaoProcesso" %>

    <div id="janelaModalCabecalho">
        <table cellpadding="5px">
            <tr>
                <td>
                    <img alt="" src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/janela-pesquisar.png" />
                </td>
                <td>
                    <img alt="" src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/separadorBarraCabecalho.png" />
                </td>
                <td>
                    SELEÇÃO DE PESSOA
                </td>
            </tr>
        </table>
    </div>

    <div id="cadastroFiltros">
        <div id="cadastroFiltrosTitulo">Filtros:</div>
        <div id="cadastroFiltrosCampos">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPesquisa" runat="server" CssClass="campoTexto" 
                            Width="475px" ClientIDMode="Inherit"></asp:TextBox>
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

    <div style="clear:both"></div>

    <div id="cadastroOpcoesPesquisa">

    </div>

    <div id="cadastroCorpo">
        <div id="cadastroCorpoTitulo">
            Resultado:
        </div>

        <div id="cadastroCorpoResultado">
            <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" 
                DataSourceID="dsResultado"  onrowcommand="grdResultado_RowCommand" DataKeyNames="idProcesso, numeroProcesso" 
                GridLines="None" AllowSorting="True"  CssClass="GridView" ShowFooter="False" AllowPaging="True" >

                <EmptyDataTemplate>
                    <center>Nenhum processo encontrado</center>
                </EmptyDataTemplate>
                                             
                <Columns>

                    <asp:CommandField SelectText="Selecionar" ShowSelectButton="True"  >
                        <ItemStyle Width="80px"  HorizontalAlign="Center" Font-Underline="true" />       
                    </asp:CommandField>

                    <asp:BoundField DataField="numeroProcesso" HeaderText="Número Processo" 
                        SortExpression="tbProcesso.numeroProcesso" Visible="true" >
                        <HeaderStyle Width="100px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Cliente" SortExpression="tbProcesso.idPessoaCliente" >
                        <ItemTemplate>
                            <%#  ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).fisicaNomeCompleto %>
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
                TypeName="ProJur.Business.Bll.bllProcesso" 
                onselected="dsResultado_Selected">                
            </asp:ObjectDataSource>
        </div>

    </div>

    <div id="janelaModalRodape">

        <table cellpadding="5px">
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnFecharDialogSelecaoProcesso" runat="server" Text="Fechar" CssClass="botao" ValidationGroup="DialogSelecaoProcesso" OnClick="btnFechar_Click" />
                </td>
            </tr>
        </table>
    </div>