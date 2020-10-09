<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pessoa.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Cadastro.Pessoa" %>
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

    <script type="text/javascript">
        /*
        function IAmSelected(source, eventArgs) {
            alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());
        }
        */

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
                            Width="475px" ClientIDMode="Inherit"></asp:TextBox>
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
            <uc1:menuAcoesCadastro ID="menuAcoesCadastro" runat="server" NovoUrl="/Paginas/Manutencao/Pessoa.aspx" NovoTexto="Incluir pessoa" />
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
                DataSourceID="dsResultado" PageSize="30" OnRowDataBound="grdResultado_RowDataBound"
                GridLines="None" AllowSorting="True"  CssClass="GridView" 
                ShowFooter="False" AllowPaging="True" >

                <EmptyDataTemplate>
                    <center>Nenhuma pessoa encontrada</center>
                </EmptyDataTemplate>
                                             
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkExcluir" runat="server" CssClass="checkall">
                            </asp:CheckBox>                        
                        </HeaderTemplate>

                        <ItemTemplate>
                            <asp:CheckBox ID="chkExcluir" runat="server">
                            </asp:CheckBox>
                            <asp:HiddenField ID="hdIdPessoa" runat="server" Value='<%# Eval("idPessoa") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>

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
                        SortExpression="CASE WHEN especiePessoa = 'F' THEN fisicaNomeCompleto WHEN especiePessoa = 'J' THEN juridicaRazaoSocial ELSE '' END" >
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
                            <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idPessoa", "/Paginas/Manutencao/Atendimento.aspx?IDCLIENTE={0} ") %>' target="_blank" title="Iniciar atendimento">
                                <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Atendimento.png" alt="Atender" border="0" />
                            </a>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="false">
                        
                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />

                        <ItemTemplate>
                            <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idPessoa", "/Paginas/Manutencao/Pessoa.aspx?ID={0} ") %>' title="Visualizar os dados" >
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

</asp:Content>
