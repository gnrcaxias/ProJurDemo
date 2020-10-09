<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgendaCompromisso.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.AgendaCompromisso" %>
<%@ Register Src="~/Controls/menuAcoesManutencao.ascx" TagName="menuAcoesManutencao" TagPrefix="ProJur" %>
<%@ Register Src="~/Controls/dialogSelecaoUsuario.ascx" TagPrefix="ProJur" TagName="dialogSelecaoUsuario" %>
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
    
    <ProJur:menuAcoesManutencao ID="menuAcoes" runat="server" NovoUrl="/Paginas/Manutencao/AgendaCompromisso.aspx"
        PesquisarUrl="/Paginas/Cadastro/Agenda.aspx" />
    <div>
        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="ErroValidacaoSummary"
            HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>
    
    <div id="manutencaoCorpoSecao">
        <asp:DetailsView ID="dvManutencao" runat="server" Width="100%" AutoGenerateRows="False"
            CellPadding="10" GridLines="None" DataSourceID="dsManutencao" 
            DataKeyNames="idAgendaCompromisso" class="DetailsView" OnItemInserting="dvManutencao_ItemInserting" OnItemUpdating="dvManutencao_ItemUpdating" OnDataBound="dvManutencao_DataBound">
            <FieldHeaderStyle Width="160px" />
            <Fields>

                <asp:TemplateField InsertVisible="false">
                    <HeaderStyle CssClass="manutencaoTituloSecao" />
                    <ItemStyle CssClass="manutencaoTituloSecao" />
                    <HeaderTemplate >
                        [ Dados de Controle ]
                    </HeaderTemplate>
                </asp:TemplateField>

                <asp:TemplateField InsertVisible="false">
                    <HeaderStyle Height="1px"  />
                    <ItemStyle Height="1px" />
                    <ItemTemplate></ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="dataCadastro" HeaderText="Data de Cadastro" InsertVisible="false"
                    ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />

                <asp:BoundField DataField="dataUltimaAlteracao" HeaderText="Data da Últ. Alteração"
                    InsertVisible="false" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />

                <asp:TemplateField InsertVisible="false">
                    <HeaderStyle Height="1px"  />
                    <ItemStyle Height="1px" />
                    <ItemTemplate></ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderStyle CssClass="manutencaoTituloSecao" />
                    <ItemStyle CssClass="manutencaoTituloSecao" />
                    <HeaderTemplate >
                        [ Dados do compromisso ]
                    </HeaderTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderStyle Height="1px"  />
                    <ItemStyle Height="1px" />
                    <ItemTemplate></ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="idAgendaCompromisso" HeaderText="Código" InsertVisible="false"
                    ReadOnly="true" />

                    <asp:TemplateField HeaderText="Cliente">
                        <EditItemTemplate>
                            <asp:UpdatePanel ID="upClienteDetails" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtIdPessoa" runat="server" Width="120px" Text=' <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoa"))).idPessoa %>'  OnTextChanged="txtIdPessoa_Click"></asp:TextBox>
                                                <asp:HiddenField ID="hdIdPessoa" runat="server" Value='<%# Eval("idPessoa")%>' />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnPesquisarPessoa" runat="server" OnClick="btnPesquisarPessoa_Click"
                                                    class="botao" ImageUrl="~/Images/Pesquisar.png" ValidationGroup="PesquisaPessoa"  />
                                            </td>
                                            <td>
                                                <asp:Literal ID="litNomePessoa" runat="server"
                                                    Text='<%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoa"))).NomeCompletoRazaoSocial %>'></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <a href='Pessoa.aspx?ID=<%# Eval("idPessoa") %>' target="_blank" >
                            <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoa"))).CPFCNPJ %> -
                                <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoa"))).NomeCompletoRazaoSocial %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>

                <asp:TemplateField HeaderText="Descrição">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtDescricao" Text='<%# Bind("Descricao") %>'  TextMode="MultiLine" Width="646px" Height="236px">
                        </asp:TextBox>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <%# this.FormatText(Eval("Descricao")) %>
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Data">
                    <EditItemTemplate>

                        <asp:TextBox runat="server" ID="txtDataInicio" Text='<%# Bind("dataInicio") %>'  CssClass="campoData" Width="90px">
                        </asp:TextBox>
                        até
                        <asp:TextBox runat="server" ID="txtDataFim" Text='<%# Bind("dataFim") %>'  CssClass="campoData" Width="90px">
                        </asp:TextBox>

                    </EditItemTemplate>

                    <ItemTemplate>
                        <%# Eval("dataInicio", "{0:dd/MM/yyyy}") %> até <%# Eval("dataFim", "{0:dd/MM/yyyy}") %>
                    </ItemTemplate>

                </asp:TemplateField>


                <asp:TemplateField HeaderText="Hora">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtHoraInicio" Text='<%# Bind("horaInicio", "{0:HH:mm}") %>' CssClass="campoHora" Width="50px">
                        </asp:TextBox>
                        até
                        <asp:TextBox runat="server" ID="txtHoraFim" Text='<%# Bind("horaFim", "{0:HH:mm}") %>' CssClass="campoHora" Width="50px">
                        </asp:TextBox>
                    </EditItemTemplate>

                    <ItemTemplate>
                        <%# Eval("horaInicio", "{0:HH:mm}") %> até <%# Eval("horaFim", "{0:HH:mm}") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Situação Compromisso">
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlSituacaoCompromisso" SelectedValue='<%# Bind("situacaoCompromisso") %>'
                            Width="150px">
                            <asp:ListItem Value="" Text="< Selecione >" />
                            <asp:ListItem Value="P" Text="Pendente" Selected="True" />
                            <asp:ListItem Value="C" Text="Concluído" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#  ProJur.Business.Bll.bllAgendaCompromisso.RetornaDescricaoSituacaoCompromisso(Eval("situacaoCompromisso"))%>
                        <asp:LinkButton ID="lnkPendenteConcluido" runat="server" OnClick="lnkPendenteConcluido_Click">Ação</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Fields>
        </asp:DetailsView>

        <asp:ObjectDataSource ID="dsManutencao" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoAgendaCompromisso"
            OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllAgendaCompromisso"
            UpdateMethod="Update" InsertMethod="Insert" OnInserted="dsManutencao_Inserted"
            DeleteMethod="Delete">

            <SelectParameters>
                <asp:QueryStringParameter Name="idAgendaCompromisso" QueryStringField="ID" Type="Int32" />
            </SelectParameters>

            <InsertParameters>
				<asp:Parameter Name="dataCadastro" Type="DateTime" />
				<asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />				
				<asp:Parameter Name="dataInicio" Type="DateTime" />	
				<asp:Parameter Name="dataFim" Type="DateTime" />	
            </InsertParameters>

            <UpdateParameters>
                <asp:QueryStringParameter Name="idAgendaCompromisso" QueryStringField="ID" Type="Int32" />
				<asp:Parameter Name="dataCadastro" Type="DateTime" />
				<asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />				
				<asp:Parameter Name="dataInicio" Type="DateTime" />	
				<asp:Parameter Name="dataFim" Type="DateTime" />	
            </UpdateParameters>

        </asp:ObjectDataSource>
        <br />
    </div>

    <asp:Panel ID="pnlAgendaUsuario" runat="server" Visible="true">
        <div class="manutencaoTituloSecao">
            [ Usuários Participantes ]
        </div>
        <div class="manutencaoCorpoSecao">
            <asp:UpdatePanel ID="upAgendaUsuario" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="opcoesAgendaUsuario" class="boxOpcoes">
                        <ul>
                            <li>                            
                                <asp:ImageButton ID="btnAdicionarAgendaUsuario" runat="server" 
                                    ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarAgendaUsuario_Click" />
                            </li>
                        </ul>
                    </div>
                    <div id="processoTerceiro" class="boxResultados">
                        <asp:GridView ID="grdAgendaUsuario" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="dsAgendaUsuario" GridLines="None" 
                            AllowSorting="True" CssClass="GridView" DataKeyNames="idAgendaUsuario" >
                            <EmptyDataTemplate>
                                <center>
                                    Nenhum usuário encontrado</center>
                            </EmptyDataTemplate>
                            <Columns>

                                <asp:BoundField DataField="idAgendaUsuario" HeaderText="Código" SortExpression="idAgendaUsuario" >
                                    <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Usuário">
                                    <ItemTemplate>
                                        <%# ProJur.Business.Bll.bllUsuario.Get(Convert.ToInt32(Eval("idUsuario"))).nomeCompleto %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="False">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png"
                                                    OnClientClick="return confirm('Deseja realmente remover este usuário?');"  />
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
                        <asp:ObjectDataSource ID="dsAgendaUsuario" runat="server" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetByAgendaCompromisso"
                            TypeName="ProJur.Business.Bll.bllAgendaUsuario" 
                            DataObjectTypeName="ProJur.Business.Dto.dtoAgendaUsuario" DeleteMethod="Delete" >
                            <SelectParameters>
                                <asp:QueryStringParameter Name="idAgendaCompromisso" QueryStringField="ID" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>

    <asp:Button ID="btnNULL_DialogSelecaoUsuario" Style="display: none" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeDialogSelecaoUsuario" runat="server" 
            TargetControlID="btnNULL_DialogSelecaoUsuario" PopupControlID="boxDialogSelecaoUsuario"  
            CancelControlID="btnNULL_DialogSelecaoUsuario" BackgroundCssClass="modalBackground" >
        <Animations>
            <OnShown>
                <FadeIn Duration=".5" Fps="30" />
            </OnShown>
            <OnHiding>
                <FadeOut Duration=".5" Fps="30" />
            </OnHiding>
        </Animations>
    </ajaxToolkit:ModalPopupExtender>

    <div id="boxDialogSelecaoUsuario" class="boxes" style="width: 530px">
        <div class="janela">
            <asp:UpdatePanel runat="server" ID="upDialogSelecaoUsuario">
                <ContentTemplate>
                    <ProJur:dialogSelecaoUsuario ID="dialogSelecaoUsuario" runat="server"  
                         />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="dialogSelecaoUsuario"/>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

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
