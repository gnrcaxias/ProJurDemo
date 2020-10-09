<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dialogSelecaoPessoa.ascx.cs" Inherits="ProJur.WebApplication.Controls.dialogSelecaoPessoa" %>

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
                DataSourceID="dsResultado"  DataKeyNames="idPessoa, CPFCNPJ, NomeCompletoRazaoSocial" 
                GridLines="None" AllowSorting="True"  CssClass="GridView" AllowPaging="true"
                PageSize="10" ShowFooter="False" onrowcommand="grdResultado_RowCommand" >

                <EmptyDataTemplate>
                    <center>Nenhuma pessoa encontrada</center>
                </EmptyDataTemplate>
                                             
                <Columns>

                    <asp:CommandField SelectText="Selecionar" ShowSelectButton="True"  >
                        <ItemStyle Width="80px"  HorizontalAlign="Center" Font-Underline="true" />       
                    </asp:CommandField>

                    <asp:BoundField DataField="idPessoa" HeaderText="Código" 
                        SortExpression="tbPessoa.idPessoa" Visible="false" >
                        <HeaderStyle Width="40px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="CPF / CNPJ" SortExpression="CASE WHEN especiePessoa = 'F' THEN fisicaCPF WHEN especiePessoa = 'J' THEN juridicaCNPJ ELSE '' END" >
                        <HeaderStyle Width="150px"></HeaderStyle>
                        <ItemTemplate>
                            <%#  ProJur.DataAccess.Utilitarios.AplicarMascaraCPFCNPJ(Eval("CPFCNPJ"), Eval("especiePessoa")) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="NomeCompletoRazaoSocial" HeaderText="Nome / Razão Social" 
                        SortExpression="CASE WHEN especiePessoa = 'F' THEN juridicaNomeCompleto WHEN especiePessoa = 'J' THEN juridicaRazaoSocial ELSE '' END" >
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Cidade">
                        <HeaderStyle Width="150px"></HeaderStyle>
                        <ItemTemplate>
                            <%# ProJur.Business.Bll.bllCidade.Get(Convert.ToInt32(Eval("enderecoIdCidade"))).Descricao %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <HeaderStyle Width="40px"></HeaderStyle>
                        <ItemTemplate>
                            <%# ProJur.Business.Bll.bllEstado.Get(Convert.ToInt32(Eval("enderecoIdEstado"))).siglaUF %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="false">
                        
                        <ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle" />

                        <ItemTemplate>
                            <a href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idPessoa", "/Paginas/Manutencao/Atendimento.aspx?IDCLIENTE={0} ") %>' target="_blank">
                                <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Atendimento.png" alt="Atender" border="0" />
                            </a>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="false">
                        
                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />

                        <ItemTemplate>
                            <a href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idPessoa", "/Paginas/Manutencao/Pessoa.aspx?ID={0} ") %>' >
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
                TypeName="ProJur.Business.Bll.bllPessoa" 
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
                    <asp:Button ID="btnFecharDialogSelecaoPessoa" runat="server" Text="Fechar" CssClass="botao" ValidationGroup="DialogSelecaoPessoa" OnClick="btnFechar_Click" />
                </td>
            </tr>
        </table>
    </div>