<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Atendimento.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Cadastro.Atendimento" %>
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
            <uc1:menuAcoesCadastro ID="menuAcoesCadastro" runat="server" 
                    NovoUrl="/Paginas/Manutencao/Atendimento.aspx" 
                    NovoTexto="Incluir atendimento" 
                    OnExcluirClickHandler="btnExcluirSelecionados_Click" 
                    />
        </div>
    </div>

    <div style="clear:both"></div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>

            <div id="cadastroOpcoesPesquisa">

                <div style="float:left; margin-right: 40px;" runat="server" id="divFiltroUsuario">
                    <label class="titulo">Usuário:</label>
                    <br /><br />
                    <asp:DropDownList runat="server" ID="ddlUsuario"
                        DataSourceID="dsUsuario"
                        DataTextField="nomeCompleto"
                        DataValueField="idUsuario" 
                        AppendDataBoundItems="True" Width="250px" >
                        <asp:ListItem Value="0" Text="< Selecione >" />
                    </asp:DropDownList>

                    <asp:ObjectDataSource ID="dsUsuario" runat="server" 
                        DataObjectTypeName="ProJur.Business.Dto.dtoUsuario"
                        SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllUsuario">    
                        <SelectParameters>
                            <asp:Parameter DefaultValue="nomeCompleto" Name="SortExpression" Type="String" />
                        </SelectParameters> 
                    </asp:ObjectDataSource>
                </div>

                <div style="float:left; margin-right: 40px; ">
                    <label class="titulo">Tipo de Atendimento:</label>
                    <br /><br />
                    <asp:DropDownList runat="server" ID="ddlTipoAtendimento"  Width="250px">
                        <asp:ListItem Value="" Text="< Selecione >" />
                        <asp:ListItem Value="LP" Text="Local / Presencial" />
                        <asp:ListItem Value="T" Text="Telefônico"  />
                        <asp:ListItem Value="E" Text="Email"  />
                        <asp:ListItem Value="FO" Text="Follow-up / Ocorrências"  />
                    </asp:DropDownList>
                </div>

                <div style="clear: both; height: 30px;"> </div>

                <div>
            
                    <label class="titulo">Data de Abertura:</label>
                    <br />
                    <br />

                    <table>
                        <tr>
                            <td>
                                De 
                                <asp:TextBox ID="txtDataInicioAtendimento" runat="server" Width="90px"></asp:TextBox>
                            </td>
                            <td>
                                Até
                                    <asp:TextBox ID="txtDataFimAtendimento" runat="server" Width="90px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                </div>

            </div>

            <div id="cadastroCorpo">

                <div id="cadastroCorpoTitulo">
                    Resultado:
                </div>

                <div id="cadastroCorpoResultado">

                    <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="dsResultado"  OnRowDataBound="grdResultado_RowDataBound"
                        GridLines="None" AllowSorting="True"  CssClass="GridView"
                        ShowFooter="False" AllowPaging="true" >

                        <EmptyDataTemplate>
                            <center>Nenhum atendimento encontrado</center>
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
                                    <asp:HiddenField ID="hdIdAtendimento" runat="server" Value='<%# Eval("idAtendimento") %>' />
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Usuário" SortExpression="idUsuario">
                        
                                <ItemStyle Width="130px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <%# ProJur.Business.Bll.bllUsuario.Get(Convert.ToInt32(Eval("idUsuario"))).nomeCompleto %>
                                </ItemTemplate>
                            </asp:TemplateField> 

                            <asp:BoundField DataField="dataCadastro" HeaderText="Data <br />Lançamento" 
                                SortExpression="tbComissaoPagar.dataCadastro" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy HH:mm}" Visible="false">
                                <HeaderStyle Width="110px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Cliente" SortExpression="CASE WHEN tbPessoa.especiePessoa = 'F' THEN tbPessoaFisica.fisicaNomeCompleto WHEN tbPessoa.especiePessoa = 'J' THEN tbPessoaJuridica.juridicaRazaoSocial END">
                                <ItemTemplate>
                                    <%#  ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).NomeCompletoRazaoSocial %>
                                </ItemTemplate>
                            </asp:TemplateField>    

                            <asp:TemplateField HeaderText="Tipo de Atendimento" SortExpression="tipoAtendimento ">
                                <HeaderStyle Width="110px" HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                        <%#  ProJur.Business.Bll.bllAtendimento.RetornaDescricaoTipoAtendimento(Eval("tipoAtendimento"))%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Data / Hora <br />Atendimento" SortExpression="dataInicoAtendimento">
                        
                                <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    Início: <%# Convert.ToDateTime(ProJur.Business.Bll.bllAtendimento.Get(Convert.ToInt32(Eval("idAtendimento"))).dataInicioAtendimento).ToString("dd/MM/yyyy HH:mm") %>
                                    <br />
                                    Fim:  <%# Convert.ToDateTime(ProJur.Business.Bll.bllAtendimento.Get(Convert.ToInt32(Eval("idAtendimento"))).dataFimAtendimento).ToString("dd/MM/yyyy HH:mm") %>
                                </ItemTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField ShowHeader="false">
                        
                                <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                <ItemTemplate>
                                    <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idAtendimento", "/Paginas/Manutencao/Atendimento.aspx?ID={0} ") %>' title="Visualizar os dados" >
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
                        TypeName="ProJur.Business.Bll.bllAtendimento" 
                        onselected="dsResultado_Selected">                
                    </asp:ObjectDataSource>
                </div>

            </div>

        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>
