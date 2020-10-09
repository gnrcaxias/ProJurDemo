<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.Menu" %>
<%@ Register src="~/Controls/menuAcoesManutencao.ascx" tagname="menuAcoesManutencao" tagprefix="ProJur" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/manutencao.main.css" rel="stylesheet" type="text/css" />

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/detailsview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/detailsview.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<ProJur:menuAcoesManutencao ID="menuAcoes" runat="server" 
        NovoUrl="/Paginas/Manutencao/Menu.aspx" 
        PesquisarUrl="/Paginas/Cadastro/Menu.aspx" />

<div>    
    <asp:ValidationSummary ID="valSummary" runat="server"  
        CssClass="ErroValidacaoSummary" HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
</div>

<div class="manutencaoTituloSecao">
    [ Dados de Identificação ]
</div>

<div class="manutencaoCorpoSecao">

    <asp:DetailsView ID="dvManutencao" runat="server" Height="50px" Width="100%" 
        AutoGenerateRows="False" CellPadding="10" CssClass="DetailsView"
        GridLines="None" DataSourceID="dsManutencao" DataKeyNames="idMenu">
        
        <FieldHeaderStyle Width="180px" Font-Bold="True" Font-Size="11px" />
        
        <Fields>
            
            <asp:BoundField DataField="idMenu" HeaderText="Código" InsertVisible="false" ReadOnly="true" />

            <asp:BoundField DataField="dataCadastro" HeaderText="Data de Cadastro" InsertVisible="false" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />

            <asp:BoundField DataField="dataUltimaAlteracao" HeaderText="Data da Últ. Alteração" InsertVisible="false" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />

            <asp:TemplateField HeaderText="Menu Superior">
                <EditItemTemplate>
                    <asp:DropDownList runat="server" ID="ddlMenuSuperior"
                        DataSourceID="dsMenuSuperior"
                        DataTextField="Descricao"
                        DataValueField="idMenu" SelectedValue='<%# Bind("idMenuPai") %>'
                        AppendDataBoundItems="True" Width="250px">
                        <asp:ListItem Value="0" Text="< Selecione >"  />
                    </asp:DropDownList>

                    <asp:ObjectDataSource ID="dsMenuSuperior" runat="server" 
                        DataObjectTypeName="ProJur.Business.Dto.dtoMenu" 
                        SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllMenu">     
                    </asp:ObjectDataSource>

                </EditItemTemplate>

                <ItemTemplate>
                    <%# Eval("DescricaoMenuPai")%>
                </ItemTemplate>
            </asp:TemplateField>

            <InfoVillage:BoundTextBoxField DataField="Descricao" HeaderText="Descrição" ControlStyle-Width="300px" Required="true" MaxLength="50" />

            <InfoVillage:BoundTextBoxField DataField="Url" HeaderText="Url" ControlStyle-Width="400px" MaxLength="100" />

            <InfoVillage:BoundTextBoxField DataField="idChave" HeaderText="Chave para Acesso" ControlStyle-Width="300px" Required="true" MaxLength="50" />

            <asp:CheckBoxField DataField="Visivel" HeaderText="Visível" />

        </Fields>
        
    </asp:DetailsView>

    <asp:ObjectDataSource ID="dsManutencao" runat="server" 
        DataObjectTypeName="ProJur.Business.Dto.dtoMenu" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
        TypeName="ProJur.Business.Bll.bllMenu" UpdateMethod="Update" 
        InsertMethod="Insert" oninserted="dsManutencao_Inserted" DeleteMethod="Delete" >
        
        <SelectParameters>
            <asp:QueryStringParameter Name="idMenu" QueryStringField="ID" Type="Int32" />
        </SelectParameters>

        <UpdateParameters>
            <asp:Parameter Name="idMenuPai" Type="Int32" />
            <asp:Parameter Name="idChave" Type="String" />
            <asp:Parameter Name="Descricao" Type="String" />
            <asp:Parameter Name="Url" Type="String" />
            <asp:Parameter Name="Visivel" Type="Boolean" />
            <asp:Parameter Name="dataCadastro" Type="DateTime" />
            <asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />
        </UpdateParameters>

    </asp:ObjectDataSource>

    <br />
</div>
</asp:Content>
