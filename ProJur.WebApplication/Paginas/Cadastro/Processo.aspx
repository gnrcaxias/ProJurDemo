<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Processo.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Cadastro.Processo" %>
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
            <uc1:menuAcoesCadastro ID="menuAcoesCadastro" runat="server" NovoUrl="/Paginas/Manutencao/Processo.aspx" NovoTexto="Incluir processo" />
        </div>
    </div>

    <div style="clear:both"></div>

    <div id="cadastroOpcoesPesquisa">

        <table>
            <tr>
                <td valign="top" style="border-right: 1px dotted #bdbfc1; padding-right: 20px; " >
                    <div>
                        <label class="titulo">Área Processual:</label>                        
                        <div style="margin-top: 10px;">
                            <asp:RadioButtonList runat="server" ID="rblAreaProcessual"
                                DataSourceID="dsAreaProcessual"
                                DataTextField="Descricao"
                                DataValueField="idAreaProcessual" 
                                AppendDataBoundItems="True" Width="150px" >
                                <asp:ListItem Text="< Todas >" Value="" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:ObjectDataSource ID="dsAreaProcessual" runat="server" 
                                DataObjectTypeName="ProJur.Business.Dto.dtoAreaProcessual"
                                SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllAreaProcessual">    
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Descricao" Name="SortExpression" Type="String" />
                                </SelectParameters> 
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                </td>
                <td valign="top" style="border-right: 1px dotted #bdbfc1; padding-right: 20px; padding-left: 10px;" >
                    <div>
                        <label class="titulo">Instância:</label>                        
                        <div style="margin-top: 10px;">
                            <asp:RadioButtonList runat="server" ID="rblInstancia"
                                DataSourceID="dsInstancia"
                                DataTextField="Descricao"
                                DataValueField="idInstancia" 
                                AppendDataBoundItems="True" Width="150px"  >
                                <asp:ListItem Text="< Todas >" Value="" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:ObjectDataSource ID="dsInstancia" runat="server" 
                                DataObjectTypeName="ProJur.Business.Dto.dtoInstancia"
                                SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllInstancia">    
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Descricao" Name="SortExpression" Type="String" />
                                </SelectParameters> 
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                </td>

                <td valign="top" style="padding-right: 20px; padding-left: 10px;" >
                    <div>
                        <label class="titulo">Comarca:</label>                        
                        <div style="margin-top: 10px;">
                            <asp:RadioButtonList runat="server" ID="rblComarca"
                                DataSourceID="dsComarca"
                                DataTextField="Descricao"
                                DataValueField="idComarca" 
                                AppendDataBoundItems="True" Width="150px"  >
                                <asp:ListItem Text="< Todas >" Value="" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:ObjectDataSource ID="dsComarca" runat="server" 
                                DataObjectTypeName="ProJur.Business.Dto.dtoComarca"
                                SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllComarca">    
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="Descricao" Name="SortExpression" Type="String" />
                                </SelectParameters> 
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                </td>
            </tr>

        </table>

    </div>

    <div id="cadastroCorpo">
        <div id="cadastroCorpoTitulo">
            Resultado:
        </div>

        <div id="cadastroCorpoResultado">

            <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" 
                DataSourceID="dsResultado" PageSize="100"  OnRowDataBound="grdResultado_RowDataBound"
                GridLines="None" AllowSorting="True"  CssClass="GridView" 
                ShowFooter="False" AllowPaging="True" >

                <EmptyDataTemplate>
                    <center>Nenhum processo encontrado</center>
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
                            <asp:HiddenField ID="hdIdProcesso" runat="server" Value='<%# Eval("idProcesso") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:BoundField DataField="numeroProcesso" HeaderText="Número Processo" 
                        SortExpression="tbProcesso.numeroProcesso" Visible="true" >
                        <HeaderStyle Width="250px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Cliente" SortExpression="tbPessoaFisica.fisicaNomeCompleto, tbPessoaJuridica.juridicaRazaoSocial" >
                        <ItemTemplate>
                            <%#  ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).fisicaNomeCompleto %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="false">
                        
                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />

                        <ItemTemplate>
                            <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idProcesso", "/Paginas/Manutencao/Processo.aspx?ID={0} ") %>' title="Visualizar os dados" >
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
                TypeName="ProJur.Business.Bll.bllProcesso" 
                onselected="dsResultado_Selected">                
            </asp:ObjectDataSource>
        </div>

    </div>

</asp:Content>
