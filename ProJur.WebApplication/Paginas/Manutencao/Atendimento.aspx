<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Atendimento.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.Atendimento" %>
<%@ Register Src="~/Controls/menuAcoesManutencao.ascx" TagName="menuAcoesManutencao" TagPrefix="ProJur" %>
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

    <ProJur:menuAcoesManutencao ID="menuAcoes" runat="server" NovoUrl="/Paginas/Manutencao/Atendimento.aspx"
        PesquisarUrl="/Paginas/Cadastro/Atendimento.aspx" />

    <div>
        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="ErroValidacaoSummary"
            HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>

    <asp:Panel ID="pnlDadosControle" runat="server">

        <div id="manutencaoCorpoSecao">
            <asp:DetailsView ID="dvManutencao" runat="server" Width="100%" AutoGenerateRows="False"
                CellPadding="10" GridLines="None" DataSourceID="dsManutencao" 
                DataKeyNames="idAtendimento, idPessoaCliente" class="DetailsView" 
                 OnItemInserting="dvManutencao_ItemInserting" OnItemUpdating="dvManutencao_ItemUpdating" OnDataBound="dvManutencao_DataBound" >
                <FieldHeaderStyle  CssClass="manutencaoFieldHeader"  Width="180px"/>
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

                    <asp:BoundField DataField="idAtendimento" HeaderText="Código" InsertVisible="false"
                        ReadOnly="true" />

                    <asp:BoundField DataField="dataCadastro" HeaderText="Data de Cadastro" InsertVisible="false"
                        ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />

                    <asp:BoundField DataField="dataUltimaAlteracao" HeaderText="Data da Últ. Alteração"
                        InsertVisible="false" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />

                    <asp:TemplateField>
                        <HeaderStyle Height="1px"  />
                        <ItemStyle Height="1px" />
                        <ItemTemplate></ItemTemplate>
                        <HeaderTemplate></HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle CssClass="manutencaoTituloSecao" />
                        <ItemStyle CssClass="manutencaoTituloSecao" />
                        <HeaderTemplate >
                            [ Dados do Atendimento ]
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle Height="1px"  />
                        <ItemStyle Height="1px" />
                        <ItemTemplate></ItemTemplate>
                        <HeaderTemplate></HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Data / Hora Início">
                        <EditItemTemplate>
                            <asp:Literal runat="server" ID="litDataHoraInicio" Text='<%# Bind("dataInicioAtendimento") %>' >

                            </asp:Literal>
                        </EditItemTemplate>

                        <ItemTemplate>
                                <%# Eval("dataInicioAtendimento") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Data / Hora Fim">
                        <EditItemTemplate>
                            <asp:Literal runat="server" ID="litDataHoraFim" Text='<%# Bind("dataFimAtendimento") %>' >

                            </asp:Literal>
                        </EditItemTemplate>

                        <ItemTemplate>
                            <%# Eval("dataFimAtendimento") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo de Atendimento">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dllTipoAtendimento" SelectedValue='<%# Bind("tipoAtendimento") %>'
                                Width="250px" >

                                <asp:ListItem Value="" Text="< Selecione >" />
                                <asp:ListItem Value="LP" Text="Local / Presencial" />
                                <asp:ListItem Value="T" Text="Telefônico"  />
                                <asp:ListItem Value="E" Text="Email"  />
                                <asp:ListItem Value="FO" Text="Follow-up / Ocorrências"  />
                            </asp:DropDownList>
                           
                        </EditItemTemplate>

                        <ItemTemplate>
                                <%#  ProJur.Business.Bll.bllAtendimento.RetornaDescricaoTipoAtendimento(Eval("tipoAtendimento"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
               
                    <asp:TemplateField HeaderText="Usuário" InsertVisible="false">
                        <EditItemTemplate>
                            <asp:literal runat="server" Visible="false" ID="litUsuario"
                                Text='<%# ProJur.Business.Bll.bllUsuario.Get(Convert.ToInt32(Eval("idUsuario"))).nomeCompleto %>' >
                            </asp:literal>
                            
                            <asp:DropDownList runat="server" ID="ddlUsuario" Visible="false"
                                DataSourceID="dsUsuario"
                                DataTextField="nomeCompleto"
                                DataValueField="idUsuario" SelectedValue='<%# Bind("idUsuario") %>'
                                AppendDataBoundItems="True" Width="250px" >
                                <asp:ListItem Value="0" Text="< Selecione >" />
                            </asp:DropDownList>

                            <asp:ObjectDataSource ID="dsUsuario" runat="server" 
                                DataObjectTypeName="ProJur.Business.Dto.dtoUsuario" 
                                SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllUsuario">     
                            </asp:ObjectDataSource>
                        </EditItemTemplate>

                        <ItemTemplate>
                            <%# ProJur.Business.Bll.bllUsuario.Get(Convert.ToInt32(Eval("idUsuario"))).nomeCompleto %>
                        </ItemTemplate>
                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="Cliente">
                        <EditItemTemplate>
                            <asp:UpdatePanel ID="upClienteDetails" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtIdPessoaCliente" runat="server" Width="120px" Text=' <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).idPessoa %>'  OnTextChanged="txtIdPessoaCliente_Click"></asp:TextBox>
                                                <asp:HiddenField ID="hdIdPessoaCliente" runat="server" Value='<%# Eval("idPessoaCliente")%>' />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnPesquisarPessoaCliente" runat="server" OnClick="btnPesquisarPessoaCliente_Click"
                                                    class="botao" ImageUrl="~/Images/Pesquisar.png" ValidationGroup="PesquisaPessoaCliente"  />
                                            </td>
                                            <td>
                                                <asp:Literal ID="litNomePessoaCliente" runat="server"
                                                    Text='<%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).NomeCompletoRazaoSocial %>'></asp:Literal>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <a href='Pessoa.aspx?ID=<%# Eval("idPessoaCliente") %>' target="_blank" >
                            <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).CPFCNPJ %> -
                                <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).NomeCompletoRazaoSocial %>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Detalhamento">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDetalhamento" runat="server" TextMode="MultiLine" Width="100%" Height="70px"  Text='<%# Bind("Detalhamento") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#  FormatText(Eval("Detalhamento"))%>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderStyle Height="1px"  />
                        <ItemStyle Height="1px" />
                        <ItemTemplate></ItemTemplate>
                        <HeaderTemplate></HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Atalhos">
                        <ItemTemplate>
                            <a href='AgendaCompromisso.aspx' target="_blank" >
                                <u>(Agendar compromisso)</u>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Fields>
            </asp:DetailsView>

            <asp:ObjectDataSource ID="dsManutencao" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoAtendimento"
                OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllAtendimento"
                UpdateMethod="Update" InsertMethod="Insert" OnInserted="dsManutencao_Inserted"
                DeleteMethod="Delete">

                <SelectParameters>
                    <asp:QueryStringParameter Name="idAtendimento" QueryStringField="ID" Type="Int32" />
                </SelectParameters>

                <InsertParameters>
                    <asp:Parameter Name="dataInicioAtendimento" Type="DateTime" />
                    <asp:Parameter Name="dataFimAtendimento" Type="DateTime" />
                    <asp:Parameter Name="dataCadastro" Type="DateTime" />
                    <asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />
                </InsertParameters>

                <UpdateParameters>
                    <asp:QueryStringParameter Name="idAtendimento" QueryStringField="ID" Type="Int32" />
                    <asp:Parameter Name="dataInicioAtendimento" Type="DateTime" />
                    <asp:Parameter Name="dataFimAtendimento" Type="DateTime" />
                    <asp:Parameter Name="dataCadastro" Type="DateTime" />
                    <asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />
                    <asp:Parameter Name="Detalhamento" Type="String" />
                    <asp:Parameter Name="tipoAtendimento" Type="String" />
                </UpdateParameters>

            </asp:ObjectDataSource>
            <br />
        </div>

    </asp:Panel>

    <asp:Panel ID="pnlHistoricoAtendimentos" runat="server">
        <div class="manutencaoTituloSecao">
            [ Histórico Atendimentos ]
        </div>
        <div class="manutencaoCorpoSecao">
            <asp:UpdatePanel ID="upHistorico" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
   
                    <div id="historico" class="boxResultados">
                        <asp:GridView ID="grdHistorico" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="dsHistorico" GridLines="None" 
                            AllowSorting="True" CssClass="GridView" DataKeyNames="idAtendimento" >
                            <EmptyDataTemplate>
                                <center>
                                    Nenhum histórico encontrado</center>
                            </EmptyDataTemplate>
                            <Columns>

                           <asp:TemplateField HeaderText="Usuário" SortExpression="idUsuario">
                        
                                <ItemStyle Width="130px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <%# ProJur.Business.Bll.bllUsuario.Get(Convert.ToInt32(Eval("idUsuario"))).Usuario %>
                                </ItemTemplate>
                            </asp:TemplateField> 

                            <asp:TemplateField HeaderText="Detalhamento">
                                <ItemTemplate>
                                    <%#  FormatText(Eval("Detalhamento")) %>
                                </ItemTemplate>
                            </asp:TemplateField>    

                            <asp:TemplateField HeaderText="Tipo de Atendimento">
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

                        <asp:ObjectDataSource ID="dsHistorico" runat="server" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetHistorico" 
                            TypeName="ProJur.Business.Bll.bllAtendimento">            

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

</asp:Content>
