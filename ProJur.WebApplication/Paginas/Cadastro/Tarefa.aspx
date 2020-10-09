<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tarefa.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Cadastro.Tarefa" %>
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
            <uc1:menuAcoesCadastro ID="menuAcoesCadastro" runat="server" NovoUrl="/Paginas/Manutencao/Tarefa.aspx" NovoTexto="Incluir tarefa" />
        </div>
    </div>

    <div style="clear:both"></div>

    <div id="cadastroOpcoesPesquisa">
        <div style="float:left; margin-right: 40px;" runat="server" id="divFiltroUsuario">
            <label class="titulo">Responsável:</label>
            <br /><br />
            <asp:DropDownList runat="server" ID="ddlUsuarioResponsavel"
                DataSourceID="dsUsuarioResponsavel"
                DataTextField="nomeCompleto"
                DataValueField="idUsuario" 
                AppendDataBoundItems="True" Width="250px" >
                <asp:ListItem Value="0" Text="< Selecione >" />
            </asp:DropDownList>

            <asp:ObjectDataSource ID="dsUsuarioResponsavel" runat="server" 
                DataObjectTypeName="ProJur.Business.Dto.dtoUsuario"
                SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllUsuario">    
                <SelectParameters>
                    <asp:Parameter DefaultValue="nomeCompleto" Name="SortExpression" Type="String" />
                </SelectParameters> 
            </asp:ObjectDataSource>
        </div>

        <div style="float:left; margin-right: 40px; ">
            <label class="titulo">Situação da Tarefa:</label>
            <br /><br />
            <asp:DropDownList runat="server" ID="ddlSituacaoTarefa" Width="150px">
                <asp:ListItem Value="" Text="< Selecione >" Selected="True" />
                <asp:ListItem Value="P" Text="Pendente" />
                <asp:ListItem Value="C" Text="Concluída" />
            </asp:DropDownList>
        </div>

        <div style="float:left; margin-right: 40px;" runat="server" id="divFiltroDataCadastro">
            <label class="titulo">Data de Cadastro:</label>
            <br />
            <br />

            <table>
                <tr>
                    <td>
                        De 
                        <asp:TextBox ID="txtDataCadastroInicio" runat="server" Width="90px"></asp:TextBox>
                    </td>
                    <td>
                        Até
                            <asp:TextBox ID="txtDataCadastroFim" runat="server" Width="90px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

        <div style="clear: both; height: 30px;"> </div>


        <div style="float:left; margin-right: 40px;" runat="server" id="divFiltroDataPrevisao">
            <label class="titulo">Data de Previsão:</label>
            <br />
            <br />

            <table>
                <tr>
                    <td>
                        De 
                        <asp:TextBox ID="txtDataPrevisaoInicio" runat="server" Width="90px"></asp:TextBox>
                    </td>
                    <td>
                        Até
                            <asp:TextBox ID="txtDataPrevisaoFim" runat="server" Width="90px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

        <div style="float:left; margin-right: 40px;" runat="server" id="divFiltroDataConclusao">
            <label class="titulo">Data de Conclusão:</label>
            <br />
            <br />

            <table>
                <tr>
                    <td>
                        De 
                        <asp:TextBox ID="txtDataConclusaoInicio" runat="server" Width="90px"></asp:TextBox>
                    </td>
                    <td>
                        Até
                            <asp:TextBox ID="txtDataConclusaoFim" runat="server" Width="90px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

        <div style="clear: both; height: 30px;"> </div>

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
                    <center>Nenhuma tarefa encontrada</center>
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
                            <asp:HiddenField ID="hdIdTarefa" runat="server" Value='<%# Eval("idTarefa") %>' />
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:BoundField DataField="idTarefa" HeaderText="Código" 
                        SortExpression="idTarefa" HeaderStyle-Width="65px" >
                        <HeaderStyle Width="65px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="descricao" HeaderText="Descrição" 
                        SortExpression="descricao" >
                    </asp:BoundField>

                    <asp:BoundField DataField="dataPrevisao" HeaderText="Data de Previsão" SortExpression="dataPrevisao" DataFormatString="{0:dd/MM/yyyy}" >
                        <HeaderStyle Width="75px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="dataConclusao" HeaderText="Data de Conclusão" SortExpression="dataConclusao" DataFormatString="{0:dd/MM/yyyy}" >
                        <HeaderStyle Width="75px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Responsável" >
                        <ItemTemplate>
                            <%#  ProJur.Business.Bll.bllUsuario.Get(Convert.ToInt32(Eval("idUsuarioResponsavel"))).nomeCompleto %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Situação" >
                        <ItemTemplate>
                            <%#  ProJur.Business.Bll.bllTarefa.RetornaDescricaoSituacaoTarefa(Eval("situacaoTarefa")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ShowHeader="false">
                        
                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />

                        <ItemTemplate>
                            <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idTarefa", "/Paginas/Manutencao/Tarefa.aspx?ID={0} ") %>' title="Visualizar os dados" >
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
                TypeName="ProJur.Business.Bll.bllTarefa" 
                onselected="dsResultado_Selected">                
            </asp:ObjectDataSource>
        </div>

    </div>

</asp:Content>
