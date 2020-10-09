<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProcessoDespesa.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.ProcessoDespesa" %>
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

    <ProJur:menuAcoesSubManutencao ID="menuAcoes" runat="server" NovoUrl="/Paginas/Manutencao/Processo.aspx" PesquisarUrl="/Paginas/Cadastro/Processo.aspx" />

    <div>
        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="ErroValidacaoSummary"
            HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>

    <asp:UpdatePanel runat="server" ID="upManutencaoProcesso">
        <ContentTemplate>

            <asp:Panel ID="pnlProcessoTerceiro" runat="server">
                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvProcessoDespesa" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsProcessoDespesa" DataKeyNames="idProcessoDespesa"
                        class="DetailsView" OnItemInserting="dvProcessoDespesa_ItemInserting" OnItemUpdating="dvProcessoDespesa_ItemUpdating" >
                        <FieldHeaderStyle CssClass="manutencaoFieldHeader" Width="190px" />
                        <Fields>
                            <asp:TemplateField InsertVisible="true">
                                <HeaderStyle CssClass="manutencaoTituloSecao" />
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <HeaderTemplate >
                                    [ Dados da Despesa ]
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

                            <asp:BoundField DataField="idProcessoDespesa" HeaderText="Código" InsertVisible="false" ReadOnly="true" Visible="false" />

                            <InfoVillage:BoundTextBoxField DataField="Descricao" HeaderText="Descrição"
                                ControlStyle-Width="200px" />

                            <InfoVillage:BoundTextBoxField DataField="Valor" HeaderText="Valor" ControlStyle-Width="300px" DataFormatString="{0:C}" ControlStyle-CssClass="campoDecimal" />

                            <InfoVillage:BoundTextBoxField DataField="Observacoes" HeaderText="Observações" TextMode="MultiLine" 
                                ControlStyle-Width="100%" ControlStyle-Height="200px" />

                        </Fields>
                    </asp:DetailsView>
                    <asp:ObjectDataSource ID="dsProcessoDespesa" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoProcessoDespesa"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllProcessoDespesa"
                    UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OnInserted="dsProcessoDespesa_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idProcessoDespesa" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
