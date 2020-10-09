<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcessoAdvogado.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.ProcessoAdvogado" %>
<%@ Register Src="~/Controls/menuAcoesSubManutencao.ascx" TagName="menuAcoesSubManutencao" TagPrefix="ProJur" %>
<%@ Register Src="~/Controls/dialogSelecaoPessoa.ascx" TagPrefix="ProJur" TagName="dialogSelecaoPessoa" %>
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



            <asp:Panel ID="pnlProcessoAdvogado" runat="server">
                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvProcessoAdvogado" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsProcessoAdvogado" DataKeyNames="idProcessoAdvogado"
                        class="DetailsView" OnItemInserting="dvProcessoAdvogado_ItemInserting" OnItemUpdating="dvProcessoAdvogado_ItemUpdating" >
                        <FieldHeaderStyle CssClass="manutencaoFieldHeader" Width="190px" />
                        <Fields>
                            <asp:TemplateField InsertVisible="true">
                                <HeaderStyle CssClass="manutencaoTituloSecao" />
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <HeaderTemplate >
                                    [ Dados do Advogado ]
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField InsertVisible="true">
                                <HeaderStyle Height="1px"  />
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                                <HeaderTemplate>
                                </HeaderTemplate>
                            </asp:TemplateField>

 <%--                           <asp:TemplateField HeaderText="Número do Processo">

                                <ItemTemplate>
                                        <%# ProJur.Business.Bll.bllProcesso.Get(Convert.ToInt32(Request.QueryString["IdProcesso"])).numeroProcesso %>
                                </ItemTemplate>

                            </asp:TemplateField>--%>

                            <asp:BoundField DataField="idProcessoAdvogado" HeaderText="Código" InsertVisible="false" ReadOnly="true" Visible="false" />

                            <asp:TemplateField HeaderText="Advogado">
                                <EditItemTemplate>
                                    <asp:UpdatePanel ID="upSelecionarAdvogado" runat="server">
                                        <ContentTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtIdPessoaAdvogado" runat="server" Width="120px" Text=' <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogado"))).idPessoa %>'></asp:TextBox>
                                                        <asp:HiddenField ID="hdIdPessoaAdvogado" runat="server" Value='<%# Eval("idPessoaAdvogado")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="btnSelecionarPessoaAdvogado" runat="server" OnClick="btnSelecionarPessoaAdvogado_Click"
                                                            class="botao" ImageUrl="~/Images/Pesquisar.png" ValidationGroup="SelecionarPessoaAdvogado"  />
                                                    </td>
                                                    <td>
                                                        <asp:Literal ID="litNomePessoaAdvogado" runat="server"
                                                            Text='<%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogado"))).NomeCompletoRazaoSocial %>'></asp:Literal>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <a href='Pessoa.aspx?ID=<%# Eval("idPessoaAdvogado") %>' target="_blank" >
                                        OAB: <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogado"))).advogadoNumeroOAB %> -
                                            <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogado"))).NomeCompletoRazaoSocial %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Parte">
                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlParteAdvogado" Width="330px"
                                        SelectedValue='<%# Bind("tipoAdvogado") %>' >
                                        <asp:ListItem Value="" Text="< Selecione >"  Selected="True" />
                                        <asp:ListItem Value="R" Text="R - Réu"  />
                                        <asp:ListItem Value="A" Text="A - Autor"  />
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <%# ProJur.Business.Bll.bllProcessoAdvogado.RetornaDescricaoTipoAdvogado(Eval("tipoAdvogado")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Fields>
                    </asp:DetailsView>
                    <asp:ObjectDataSource ID="dsProcessoAdvogado" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoProcessoAdvogado"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllProcessoAdvogado"
                    UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OnInserted="dsProcessoAdvogado_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idProcessoAdvogado" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

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

</asp:Content>
