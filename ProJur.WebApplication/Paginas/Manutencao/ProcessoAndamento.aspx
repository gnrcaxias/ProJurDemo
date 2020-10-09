<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcessoAndamento.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.ProcessoAndamento" %>
<%@ Register Src="~/Controls/menuAcoesSubManutencao.ascx" TagName="menuAcoesSubManutencao" TagPrefix="ProJur" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/gridview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/gridview.js" type="text/javascript"></script>

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/detailsview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/detailsview.js" type="text/javascript"></script>

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/manutencao.main.css" rel="stylesheet" type="text/css" />
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/cadastro.main.css" rel="stylesheet" type="text/css" />

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

            <asp:Panel ID="pnlProcessoAndamento" runat="server">
                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvProcessoAndamento" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsProcessoAndamento" DataKeyNames="idProcessoAndamento"
                        class="DetailsView" OnItemInserting="dvProcessoAndamento_ItemInserting" OnItemUpdating="dvProcessoAndamento_ItemUpdating" OnDataBound="dvProcessoAndamento_DataBound" >
                        <FieldHeaderStyle CssClass="manutencaoFieldHeader" Width="190px" />
                        <Fields>
                            <asp:TemplateField InsertVisible="true">
                                <HeaderStyle CssClass="manutencaoTituloSecao" />
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <HeaderTemplate >
                                    [ Dados do Andamento ]
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

                            <asp:BoundField DataField="idProcessoAndamento" HeaderText="Código" InsertVisible="false" ReadOnly="true" Visible="false" />

                            <InfoVillage:BoundTextBoxField DataField="Descricao" HeaderText="Descrição" TextMode="MultiLine" 
                                ControlStyle-Width="100%" ControlStyle-Height="200px" />

                            <InfoVillage:BoundTextBoxField DataField="dataPublicacao" HeaderText="Data de Publicação" ControlStyle-Width="300px" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"  ControlStyle-CssClass="campoData" />

                            <asp:TemplateField HeaderText="Visível para o Cliente">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkVisivelCliente" runat="server" Checked='<%# Bind("visivelCliente") %>' />
                                </EditItemTemplate>
                                
                                <ItemTemplate>
                                    <%# this.RetornaDescricaoCheckBox(Eval("visivelCliente")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

<%--                            <asp:CheckBoxField HeaderText="Visível Cliente" DataField="visivelCliente">
                                <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:CheckBoxField>--%>

                        </Fields>
                    </asp:DetailsView>
                    <asp:ObjectDataSource ID="dsProcessoAndamento" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoProcessoAndamento"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllProcessoAndamento"
                    UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OnInserted="dsProcessoAndamento_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idProcessoAndamento" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="dataCadastro" Type="DateTime" />
                            <asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />
                            <asp:Parameter Name="dataPublicacao" Type="DateTime" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="dataCadastro" Type="DateTime" />
                            <asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />
                            <asp:Parameter Name="dataPublicacao" Type="DateTime" />
                        </InsertParameters>
                    </asp:ObjectDataSource>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlProcessoPeca" runat="server">
                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvProcessoPeca" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsProcessoPeca" DataKeyNames="idProcessoPeca"
                        class="DetailsView" OnItemInserting="dvProcessoPeca_ItemInserting" OnItemUpdating="dvProcessoPeca_ItemUpdating" OnDataBound="dvProcessoPeca_DataBound" >
                        <FieldHeaderStyle CssClass="manutencaoFieldHeader" Width="190px" />

                        <EmptyDataTemplate>
                            Nenhuma peça foi anexada a este andamento
                        </EmptyDataTemplate>

                        <Fields>
                            <asp:TemplateField InsertVisible="true">
                                <HeaderStyle CssClass="manutencaoTituloSecao" />
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <HeaderTemplate >
                                    [ Dados da Peça Processual ]
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

                            <asp:BoundField DataField="idProcessoPeca" HeaderText="Código" InsertVisible="false" ReadOnly="true" />

                            <asp:TemplateField HeaderText="Descrição">

                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Bind("Descricao") %>' Height="236px" Width="646px"></asp:TextBox>
                                </EditItemTemplate>                                   
                            
                                <ItemTemplate>
                                    <%# this.FormatText(Eval("Descricao")) %>
                                </ItemTemplate>

                            </asp:TemplateField>

<%--                            <InfoVillage:BoundTextBoxField DataField="Descricao" HeaderText="Descrição" TextMode="MultiLine" 
                                ControlStyle-Width="100%" ControlStyle-Height="200px" />--%>

                            <asp:TemplateField HeaderText="Categoria">

                                <EditItemTemplate>
                                    <asp:DropDownList runat="server" ID="dllCategoriaPeca"
                                        DataSourceID="dsCategoriaPeca"
                                        DataTextField="Descricao"
                                        DataValueField="idCategoriaPeca" SelectedValue='<%# Bind("idCategoriaPecaProcessual") %>'
                                        AppendDataBoundItems="True" Width="330px">
                                        <asp:ListItem Value="0" Text="< Selecione >"  />
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="dsCategoriaPeca" runat="server" 
                                        DataObjectTypeName="ProJur.Business.Dto.dtoCategoriaPeca" 
                                        SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllCategoriaPeca">
                                    </asp:ObjectDataSource>
                                </EditItemTemplate>                                   
                        
                                <ItemTemplate>
                                    <%# ProJur.Business.Bll.bllCategoriaPeca.Get(Convert.ToInt32(Eval("idCategoriaPecaProcessual"))).Descricao %>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Arquivo">

                                <EditItemTemplate>
                                    <asp:HiddenField ID="hfCaminhoArquivo" runat="server" Value='<%# Bind("caminhoArquivo")%>' />
                                    <asp:HiddenField ID="hfNomeArquivo" runat="server" Value='<%# Bind("nomeArquivo")%>' />
                                    <asp:FileUpload ID="fpArquivo" runat="server" Width="300px" Size="35"  />
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <a href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualUpload() %>/<%# Eval("caminhoArquivo")%>' target="_blank">
                                       <%# Eval("nomeArquivo")%>
                                    </a>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Visível para o Cliente">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkVisivelCliente" runat="server" Checked='<%# Bind("visivelCliente") %>' />
                                </EditItemTemplate>
                                
                                <ItemTemplate>
                                    <%# this.RetornaDescricaoCheckBox(Eval("visivelCliente")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

<%--                            <asp:CheckBoxField HeaderText="Visível Cliente" DataField="visivelCliente">
                                <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:CheckBoxField>--%>

                        </Fields>
                    </asp:DetailsView>
                    <asp:ObjectDataSource ID="dsProcessoPeca" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoProcessoPeca"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetByProcessoAndamento" TypeName="ProJur.Business.Bll.bllProcessoPeca"
                    UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OnInserted="dsProcessoPeca_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idProcessoAndamento" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
