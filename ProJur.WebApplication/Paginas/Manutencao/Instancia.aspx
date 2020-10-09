<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Instancia.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.Instancia" %>
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
    
    <ProJur:menuAcoesManutencao ID="menuAcoes" runat="server" NovoUrl="/Paginas/Manutencao/Instancia.aspx"
        PesquisarUrl="/Paginas/Cadastro/Instancia.aspx" />

    <div>
        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="ErroValidacaoSummary"
            HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>

    <div id="manutencaoCorpoSecao">
        <asp:DetailsView ID="dvManutencao" runat="server" Width="100%" AutoGenerateRows="False"
            CellPadding="10" GridLines="None" DataSourceID="dsManutencao" DataKeyNames="idInstancia" class="DetailsView">
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
                        [ Dados ]
                    </HeaderTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderStyle Height="1px"  />
                    <ItemStyle Height="1px" />
                    <ItemTemplate></ItemTemplate>
                    <HeaderTemplate></HeaderTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="idInstancia" HeaderText="Código" InsertVisible="false"
                    ReadOnly="true" />

                <InfoVillage:BoundTextBoxField DataField="Descricao" HeaderText="Descrição" ControlStyle-Width="300px"
                    Required="true" MaxLength="50" />

            </Fields>
        </asp:DetailsView>

        <asp:ObjectDataSource ID="dsManutencao" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoInstancia"
            OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllInstancia"
            UpdateMethod="Update" InsertMethod="Insert" OnInserted="dsManutencao_Inserted"
            DeleteMethod="Delete">

            <SelectParameters>
                <asp:QueryStringParameter Name="idInstancia" QueryStringField="ID" Type="Int32" />
            </SelectParameters>

            <UpdateParameters>
                <asp:QueryStringParameter Name="idInstancia" QueryStringField="ID" Type="Int32" />
				<asp:Parameter Name="dataCadastro" Type="DateTime" />
				<asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />				
            </UpdateParameters>

        </asp:ObjectDataSource>
        <br />
    </div>

</asp:Content>
