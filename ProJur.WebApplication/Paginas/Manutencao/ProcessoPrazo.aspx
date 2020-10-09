<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcessoPrazo.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.ProcessoPrazo" %>
<%@ Register Src="~/Controls/menuAcoesSubManutencao.ascx" TagName="menuAcoesSubManutencao" TagPrefix="ProJur" %>
<%@ Register Src="~/Controls/dialogSelecaoPessoa.ascx" TagPrefix="ProJur" TagName="dialogSelecaoPessoa" %>
<%@ Register Src="~/Controls/dialogSelecaoProcesso.ascx" TagPrefix="ProJur" TagName="dialogSelecaoProcesso" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/gridview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/gridview.js" type="text/javascript"></script>

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/detailsview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/detailsview.js" type="text/javascript"></script>

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/manutencao.main.css" rel="stylesheet" type="text/css" />
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/cadastro.main.css" rel="stylesheet" type="text/css" />

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/modal-window.css" rel="stylesheet" type="text/css" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ProJur:menuAcoesSubManutencao ID="menuAcoes" runat="server" NovoUrl="/Paginas/Manutencao/Processo.aspx" PesquisarUrl="/Paginas/Cadastro/Processo.aspx" 
            OnCancelarClickHandler="Cancelar_Click" OnEditarClickHandler="Editar_Click" OnExcluirClickHandler="Excluir_Click" OnSalvarClickHandler="Salvar_Click" />

    <div>
        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="ErroValidacaoSummary"
            HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>

    <asp:UpdatePanel runat="server" ID="upManutencaoProcesso">
        <ContentTemplate>

            <asp:Panel ID="pnlProcessoPrazo" runat="server">
                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvProcessoPrazo" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsProcessoPrazo" DataKeyNames="idProcessoPrazo"  OnDataBound="dvProcessoPrazo_DataBound"
                        class="DetailsView" OnItemInserting="dvProcessoPrazo_ItemInserting" OnItemUpdating="dvProcessoPrazo_ItemUpdating" >
                        
                        <Fields>
                            <asp:TemplateField InsertVisible="true" ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate >
                                    [ Dados do Prazo ]
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField InsertVisible="true" ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="idProcessoPrazo" HeaderText="Código" InsertVisible="false" ReadOnly="true" Visible="false" />

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Processo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="upProcessoDetails" runat="server">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtIdProcesso" runat="server" Width="120px" Text=' <%# ProJur.Business.Bll.bllProcesso.Get(Convert.ToInt32(Eval("idProcesso"))).idProcesso %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdIdProcesso" runat="server" Value='<%# Eval("idProcesso")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnPesquisarProcesso" runat="server" OnClick="btnPesquisarProcesso_Click"
                                                                        class="botao" ImageUrl="~/Images/Pesquisar.png" ValidationGroup="PesquisaProcesso"  />
                                                                </td>
                                                                <td>
                                                                    <asp:Literal ID="litNumeroProcesso" runat="server"
                                                                        Text='<%# ProJur.Business.Bll.bllProcesso.Get(Convert.ToInt32(Eval("idProcesso"))).numeroProcesso %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Processo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <a href='Processo.aspx?ID=<%# Eval("idProcesso") %>' target="_blank" >
                                                    <%# ProJur.Business.Bll.bllProcesso.Get(Convert.ToInt32(Eval("idProcesso"))).numeroProcesso %>
                                                </a>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Descrição
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Bind("Descricao") %>' Width="646px" Height="236px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Descrição
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# this.FormatText(Eval("Descricao")) %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Tipo do Prazo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlTipoPrazoProcessual"
                                                    DataSourceID="dsTipoPrazoProcessual"
                                                    DataTextField="Descricao"
                                                    DataValueField="idTipoPrazoProcessual" SelectedValue='<%# Bind("idTipoPrazo") %>'
                                                    AppendDataBoundItems="True" Width="330px">
                                                    <asp:ListItem Value="0" Text="< Selecione >"  />
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="dsTipoPrazoProcessual" runat="server" 
                                                    DataObjectTypeName="ProJur.Business.Dto.dtoTipoPrazoProcessual" 
                                                    SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllTipoPrazoProcessual">
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Tipo do Prazo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# ProJur.Business.Bll.bllTipoPrazoProcessual.Get(Convert.ToInt32(Eval("idTipoPrazo"))).Descricao %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Data de Publicação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDataPublicacao" runat="server" Text='<%# Bind("dataPublicacao") %>' Width="90px" CssClass="campoData"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Vencimento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDataVencimento" runat="server" Text='<%# Bind("dataVencimento") %>' Width="90px" CssClass="campoData"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Conclusão
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtdataConclusao" runat="server" Text='<%# Bind("dataConclusao") %>' Width="90px" CssClass="campoData"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Data de Publicação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 90px;">
                                                <%# Eval("dataPublicacao", "{0:dd/MM/yyyy}") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Vencimento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 90px;">
                                                <%# Eval("dataVencimento", "{0:dd/MM/yyyy}") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Conclusão
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 90px;">
                                                <%# Eval("dataConclusao", "{0:dd/MM/yyyy}") %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Situação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlSituacaoPrazo" Width="150px"
                                                    SelectedValue='<%# Bind("situacaoPrazo") %>' >
                                                    <asp:ListItem Value="P" Text="P - Pendente"  Selected="True" />
                                                    <asp:ListItem Value="C" Text="C - Concluído"  />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Situação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# ProJur.Business.Bll.bllProcessoPrazo.RetornaDescricaoSituacaoPrazo(Eval("situacaoPrazo")) %>
                                                <asp:LinkButton ID="lnkPendenteConcluido" runat="server" OnClick="lnkPendenteConcluido_Click">Ação</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

<%--                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Advogado Responsável
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="upSelecionarAdvogado" runat="server">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtIdPessoaAdvogado" runat="server" Width="120px" Text=' <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogadoResponsavel"))).idPessoa %>'></asp:TextBox>
                                                                    <asp:HiddenField ID="hdIdPessoaAdvogado" runat="server" Value='<%# Eval("idPessoaAdvogadoResponsavel")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnSelecionarPessoaAdvogado" runat="server" OnClick="btnSelecionarPessoaAdvogado_Click"
                                                                        class="botao" ImageUrl="~/Images/Pesquisar.png" ValidationGroup="SelecionarPessoaAdvogado"  />
                                                                </td>
                                                                <td>
                                                                    <asp:Literal ID="litNomePessoaAdvogado" runat="server"
                                                                        Text='<%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogadoResponsavel"))).NomeCompletoRazaoSocial %>'></asp:Literal>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Advogado Responsável
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <a href='Pessoa.aspx?ID=<%# Eval("idPessoaAdvogadoResponsavel") %>' target="_blank" >
                                                    OAB: <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogadoResponsavel"))).advogadoNumeroOAB %> -
                                                        <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogadoResponsavel"))).NomeCompletoRazaoSocial %>
                                                </a>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                        </Fields>
                    </asp:DetailsView>
                    <asp:ObjectDataSource ID="dsProcessoPrazo" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoProcessoPrazo"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllProcessoPrazo"
                    UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OnInserted="dsProcessoPrazo_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idProcessoPrazo" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>
                        <InsertParameters>
                            <asp:Parameter Name="dataPublicacao" Type="DateTime" />
                            <asp:Parameter Name="dataVencimento" Type="DateTime" />
                            <asp:Parameter Name="dataConclusao" Type="DateTime" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="dataPublicacao" Type="DateTime" />
                            <asp:Parameter Name="dataVencimento" Type="DateTime" />
                            <asp:Parameter Name="dataConclusao" Type="DateTime" />
                        </UpdateParameters>
                    </asp:ObjectDataSource>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Panel ID="pnlPrazoResponsavel" runat="server" Visible="true">
        <div class="manutencaoTituloSecao">
            [ Responsáveis ]
        </div>
        <div class="manutencaoCorpoSecao">
            <asp:UpdatePanel ID="upPrazoResponsavel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="opcoesPrazoResponsavel" class="boxOpcoes">
                        <ul>
                            <li>                            
                                <asp:ImageButton ID="btnAdicionarResponsavel" runat="server" 
                                    ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarResponsavel_Click" />
                            </li>
                        </ul>
                    </div>
                    <div id="processoTerceiro" class="boxResultados">
                        <asp:GridView ID="grdPrazoResponsavel" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="dsPrazoResponsavel" GridLines="None" 
                            AllowSorting="True" CssClass="GridView" DataKeyNames="idProcessoPrazoResponsavel" >
                            <EmptyDataTemplate>
                                <center>
                                    Nenhum responsável selecionado</center>
                            </EmptyDataTemplate>
                            <Columns>

<%--                                <asp:BoundField DataField="idProcessoPrazoResponsavel" HeaderText="Código" SortExpression="idProcessoPrazoResponsavel" >
                                    <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>

                                <asp:TemplateField HeaderText="Responsável">
                                    <ItemTemplate>
                                        <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoa"))).NomeCompletoRazaoSocial %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemStyle Width="250px" HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoa"))).TipoPessoaDescricao.ToUpper() %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="False">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png"
                                                    OnClientClick="return confirm('Deseja realmente remover este responsável?');"  />
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
                        <asp:ObjectDataSource ID="dsPrazoResponsavel" runat="server" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetByProcessoPrazo"
                            TypeName="ProJur.Business.Bll.bllProcessoPrazoResponsavel" 
                            DataObjectTypeName="ProJur.Business.Dto.dtoProcessoPrazoResponsavel" DeleteMethod="Delete" >
                            <SelectParameters>
                                <asp:QueryStringParameter Name="idProcessoPrazo" QueryStringField="ID" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:Button ID="btnNULL_DialogSelecaoPessoa" Style="display: none" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeDialogSelecaoPessoa" runat="server" 
            TargetControlID="btnNULL_DialogSelecaoPessoa" PopupControlID="boxDialogSelecaoPessoa"  
            CancelControlID="btnNULL_DialogSelecaoPessoa" BackgroundCssClass="modalBackground" >
        <Animations>
            <OnShown>
                <FadeIn Duration=".5" Fps="30" />
            </OnShown>
            <OnHiding>
                <FadeOut Duration=".5" Fps="30" />
            </OnHiding>
        </Animations>
    </ajaxToolkit:ModalPopupExtender>

    <div id="boxDialogSelecaoPessoa" class="boxes" style="width: 730px">
        <div class="janela">
            <asp:UpdatePanel runat="server" ID="upDialogSelecaoPessoa">
                <ContentTemplate>
                    <ProJur:dialogSelecaoPessoa ID="dialogSelecaoPessoa" runat="server" 
                        OnSelecionarClickHandler="SelecionarDialogSelecaoPessoa_Click" OnFecharClickHandler="FecharDialogSelecaoPessoa_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:Button ID="btnNULL_DialogSelecaoProcesso" Style="display: none" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeDialogSelecaoProcesso" runat="server" 
            TargetControlID="btnNULL_DialogSelecaoProcesso" PopupControlID="boxDialogSelecaoProcesso"  
            CancelControlID="btnNULL_DialogSelecaoProcesso" BackgroundCssClass="modalBackground" >
        <Animations>
            <OnShown>
                <FadeIn Duration=".5" Fps="30" />
            </OnShown>
            <OnHiding>
                <FadeOut Duration=".5" Fps="30" />
            </OnHiding>
        </Animations>
    </ajaxToolkit:ModalPopupExtender>

    <div id="boxDialogSelecaoProcesso" class="boxes" style="width: 730px">
        <div class="janela">
            <asp:UpdatePanel runat="server" ID="upDialogSelecaoProcesso">
                <ContentTemplate>
                    <ProJur:dialogSelecaoProcesso ID="dialogSelecaoProcesso" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
