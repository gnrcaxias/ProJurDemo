<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tarefa.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.Tarefa" %>
<%@ Register Src="~/Controls/menuAcoesManutencao.ascx" TagName="menuAcoesManutencao" TagPrefix="ProJur" %>
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
    
    <ProJur:menuAcoesManutencao ID="menuAcoes" runat="server" NovoUrl="/Paginas/Manutencao/Tarefa.aspx"
        PesquisarUrl="/Paginas/Cadastro/Tarefa.aspx" />

    <div>
        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="ErroValidacaoSummary"
            HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>

    <div id="manutencaoCorpoSecao">
        <asp:DetailsView ID="dvManutencao" runat="server" Width="100%" AutoGenerateRows="False"
            CellPadding="10" GridLines="None" DataSourceID="dsManutencao" DataKeyNames="idTarefa" class="DetailsView"
            OnItemInserting="dvManutencao_ItemInserting" OnItemUpdating="dvManutencao_ItemUpdating" OnDataBound="dvManutencao_DataBound" >
            <FieldHeaderStyle Width="180px" />
            <Fields>

                <asp:TemplateField>
                    <HeaderStyle CssClass="manutencaoTituloSecao" />
                    <ItemStyle CssClass="manutencaoTituloSecao" />
                    <HeaderTemplate >
                        [ Dados de Controle ]
                    </HeaderTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderStyle Height="1px"  />
                    <ItemStyle Height="1px" />
                    <ItemTemplate></ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                </asp:TemplateField>

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
                        [ Dados de Identificação ]
                    </HeaderTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderStyle Height="1px"  />
                    <ItemStyle Height="1px" />
                    <ItemTemplate></ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="idTarefa" HeaderText="Código" InsertVisible="false"
                    ReadOnly="true" />

                <InfoVillage:BoundTextBoxField DataField="Descricao" HeaderText="Descrição" ControlStyle-Width="300px"
                    Required="true" MaxLength="50" />

                <InfoVillage:BoundTextBoxField DataField="dataPrevisao" HeaderText="Data de Previsão" ControlStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}"
                    ControlStyle-CssClass="campoData"  />

                <InfoVillage:BoundTextBoxField DataField="dataConclusao" HeaderText="Data da Conclusão" ControlStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" 
                    ControlStyle-CssClass="campoData" />

                <asp:TemplateField HeaderText="Responsável">
                    <EditItemTemplate>
                        <asp:literal runat="server" Visible="false" ID="litUsuarioResponsavel"
                            Text='<%# ProJur.Business.Bll.bllUsuario.Get(Convert.ToInt32(Eval("idUsuarioResponsavel"))).nomeCompleto %>' >
                        </asp:literal>

                        <asp:DropDownList runat="server" ID="ddlUsuarioResponsavel"
                            DataSourceID="dsUsuarioResponsavel"
                            DataTextField="nomeCompleto"
                            DataValueField="idUsuario" SelectedValue='<%# Bind("idUsuarioResponsavel") %>'
                            AppendDataBoundItems="True" Width="330px">
                            <asp:ListItem Value="0" Text="< Selecione >"  />
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="dsUsuarioResponsavel" runat="server" 
                            DataObjectTypeName="ProJur.Business.Dto.dtoUsuario" 
                            SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllUsuario">
                        </asp:ObjectDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                            <%# ProJur.Business.Bll.bllUsuario.Get(Convert.ToInt32(Eval("idUsuarioResponsavel"))).nomeCompleto %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Situação Tarefa">
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlSituacaoTarefa" SelectedValue='<%# Bind("situacaoTarefa") %>'
                            Width="150px">
                            <asp:ListItem Value="" Text="< Selecione >" />
                            <asp:ListItem Value="P" Text="Pendente" />
                            <asp:ListItem Value="C" Text="Concluída" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#  ProJur.Business.Bll.bllTarefa.RetornaDescricaoSituacaoTarefa(Eval("situacaoTarefa"))%>
                    </ItemTemplate>
                </asp:TemplateField>

            </Fields>
        </asp:DetailsView>

        <asp:ObjectDataSource ID="dsManutencao" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoTarefa"
            OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllTarefa"
            UpdateMethod="Update" InsertMethod="Insert" OnInserted="dsManutencao_Inserted"
            DeleteMethod="Delete">

            <SelectParameters>
                <asp:QueryStringParameter Name="idTarefa" QueryStringField="ID" Type="Int32" />
            </SelectParameters>

            <InsertParameters>
				<asp:Parameter Name="dataCadastro" Type="DateTime" />
				<asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />				
				<asp:Parameter Name="dataPrevisao" Type="DateTime" />	
                <asp:Parameter Name="dataConclusao" Type="DateTime" />	
            </InsertParameters>

            <UpdateParameters>
                <asp:QueryStringParameter Name="idTarefa" QueryStringField="ID" Type="Int32" />
				<asp:Parameter Name="dataCadastro" Type="DateTime" />
				<asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />				
				<asp:Parameter Name="dataPrevisao" Type="DateTime" />	
                <asp:Parameter Name="dataConclusao" Type="DateTime" />	
            </UpdateParameters>

        </asp:ObjectDataSource>
        <br />
    </div>

</asp:Content>
