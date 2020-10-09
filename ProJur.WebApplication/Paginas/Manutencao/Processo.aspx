<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Processo.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.Processo" %>
<%@ Register Src="~/Controls/menuAcoesManutencao.ascx" TagName="menuAcoesManutencao" TagPrefix="ProJur" %>
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

    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs();
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ProJur:menuAcoesManutencao ID="menuAcoes" runat="server" NovoUrl="/Paginas/Manutencao/Processo.aspx" PesquisarUrl="/Paginas/Cadastro/Processo.aspx" />

    <div>
        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="ErroValidacaoSummary"
            HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>


    <div id="tabs" style="border: none;">

	    <ul>
		    <li runat="server" id="liAbertura"><a href="#tabs-1">Abertura</a></li>
		    <li runat="server" id="liPartes"><a href="#tabs-2">Partes</a></li>
            <li runat="server" id="liAdvogados"><a href="#tabs-3">Advogados</a></li>
            <li runat="server" id="liAndamentos"><a href="#tabs-4">Andamentos</a></li>
            <li runat="server" id="liPecas"><a href="#tabs-5">Peças Processuais</a></li>
            <li runat="server" id="liPrazos"><a href="#tabs-6">Prazos</a></li>
		    <li runat="server" id="liApensos"><a href="#tabs-7">Apensos</a></li>
            <li runat="server" id="liDespesas"><a href="#tabs-8">Despesas</a></li>
            <li runat="server" id="liTerceiros"><a href="#tabs-9">Terceiros</a></li>
	    </ul>

        <div id="tabs-1">

            <asp:Panel ID="pnlAbertura" runat="server">
                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvAbertura" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsAbertura" DataKeyNames="idProcesso"
                        class="DetailsView" OnItemInserting="dvAbertura_ItemInserting" OnItemUpdating="dvAbertura_ItemUpdating" >
                        <Fields>

                            <asp:TemplateField InsertVisible="false" ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate >
                                    [ Dados de Controle ]
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField InsertVisible="false" ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Código do Processo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("idProcesso") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Cadastro
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("dataCadastro", "{0:dd/MM/yyyy HH:mm:ss}") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data da Últ. Alteração
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("dataUltimaAlteracao", "{0:dd/MM/yyyy HH:mm:ss}") %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate>
                                    [ Dados de Abertura ]
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Número do Processo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNumeroProcesso" runat="server" Text='<%# Bind("numeroProcesso") %>' Width="150px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Distribuição
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDataDistribuicao"  CssClass="campoData"  runat="server" Width="100px" Text='<%# Bind("dataDistribuicao") %>' ></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Número do Processo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 150px;">
                                                <%# Eval("numeroProcesso") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Distribuição
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("dataDistribuicao", "{0:dd/MM/yyyy}") %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Cliente">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Cliente
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="upClienteDetails" runat="server">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtIdPessoaCliente" runat="server" Width="120px" Text=' <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).idPessoa %>'></asp:TextBox>
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
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Cliente
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <a href='Pessoa.aspx?ID=<%# Eval("idPessoaCliente") %>' target="_blank" >
                                                <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).CPFCNPJ %> -
                                                    <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaCliente"))).NomeCompletoRazaoSocial %>
                                                </a>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Cliente">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Tipo de Ação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="dllTipoAcao"
                                                    DataSourceID="dsTipoAcao"
                                                    DataTextField="Descricao"
                                                    DataValueField="idTipoAcao" SelectedValue='<%# Bind("idTipoAcao") %>'
                                                    AppendDataBoundItems="True" Width="330px">
                                                    <asp:ListItem Value="0" Text="< Selecione >"  />
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="dsTipoAcao" runat="server" 
                                                    DataObjectTypeName="ProJur.Business.Dto.dtoTipoAcao" 
                                                    SelectMethod="GetAll" TypeName="ProJur.Business.Bll.blLTipoAcao">
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Tipo de Ação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <a href='TipoAcao.aspx?ID=<%# Eval("idTipoAcao") %>' target="_blank" >
                                                    <%# ProJur.Business.Bll.bllTipoAcao.Get(Convert.ToInt32(Eval("idTipoAcao"))).Descricao %>
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
                                                Objeto de Ação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtObjetoAcao" runat="server" Text='<%# Bind("objetoAcao") %>' Height="236px" Width="646px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Objeto de Ação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("objetoAcao") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Vara
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlVara"
                                                    DataSourceID="dsVara"
                                                    DataTextField="Descricao"
                                                    DataValueField="idVara" SelectedValue='<%# Bind("idVara") %>'
                                                    AppendDataBoundItems="True" Width="250px">
                                                    <asp:ListItem Value="0" Text="< Selecione >"  />
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="dsVara" runat="server" 
                                                    DataObjectTypeName="ProJur.Business.Dto.dtoVara" 
                                                    SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllVara">
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Comarca
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlComarca"
                                                    DataSourceID="dsComarca"
                                                    DataTextField="Descricao"
                                                    DataValueField="idComarca" SelectedValue='<%# Bind("idComarca") %>'
                                                    AppendDataBoundItems="True" Width="250px">
                                                    <asp:ListItem Value="0" Text="< Selecione >"  />
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="dsComarca" runat="server" 
                                                    DataObjectTypeName="ProJur.Business.Dto.dtoComarca" 
                                                    SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllComarca">
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Vara
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 250px;">
                                                <a href='Vara.aspx?ID=<%# Eval("idVara") %>' target="_blank" >
                                                    <%# ProJur.Business.Bll.bllVara.Get(Convert.ToInt32(Eval("idVara"))).Descricao %>
                                                </a>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Comarca
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 250px;">
                                                    <a href='Comarca.aspx?ID=<%# Eval("idComarca") %>' target="_blank" >
                                                        <%# ProJur.Business.Bll.bllComarca.Get(Convert.ToInt32(Eval("idComarca"))).Descricao %>
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
                                                Instância
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlInstancia"
                                                    DataSourceID="dsInstancia"
                                                    DataTextField="Descricao"
                                                    DataValueField="idInstancia" SelectedValue='<%# Bind("idInstancia") %>'
                                                    AppendDataBoundItems="True" Width="250px">
                                                    <asp:ListItem Value="0" Text="< Selecione >"  />
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="dsInstancia" runat="server" 
                                                    DataObjectTypeName="ProJur.Business.Dto.dtoInstancia" 
                                                    SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllInstancia">
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Área Processual
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlAreaProcessual"
                                                    DataSourceID="dsAreaProcessual"
                                                    DataTextField="Descricao"
                                                    DataValueField="idAreaProcessual" SelectedValue='<%# Bind("idAreaProcessual") %>'
                                                    AppendDataBoundItems="True" Width="250px">
                                                    <asp:ListItem Value="0" Text="< Selecione >"  />
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="dsAreaProcessual" runat="server" 
                                                    DataObjectTypeName="ProJur.Business.Dto.dtoAreaProcessual" 
                                                    SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllAreaProcessual">
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Instância
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 250px;">
                                                <a href='Instancia.aspx?ID=<%# Eval("idInstancia") %>' target="_blank" >
                                                    <%# ProJur.Business.Bll.bllInstancia.Get(Convert.ToInt32(Eval("idInstancia"))).Descricao %>
                                                </a>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Área Processual
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 250px;">
                                               <a href='AreaProcessual.aspx?ID=<%# Eval("idAreaProcessual") %>' target="_blank" >
                                                    <%# ProJur.Business.Bll.bllAreaProcessual.Get(Convert.ToInt32(Eval("idAreaProcessual"))).Descricao %>
                                                </a>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Situação">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Situação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlSituacaoAtual"
                                                    DataSourceID="dsSituacaoProcesso"
                                                    DataTextField="Descricao"
                                                    DataValueField="idSituacaoProcesso" SelectedValue='<%# Bind("idSituacaoAtual") %>'
                                                    AppendDataBoundItems="True" Width="250px">
                                                    <asp:ListItem Value="0" Text="< Selecione >"  />
                                                </asp:DropDownList>
                                                <asp:ObjectDataSource ID="dsSituacaoProcesso" runat="server" 
                                                    DataObjectTypeName="ProJur.Business.Dto.dtoSituacaoProcesso" 
                                                    SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllSituacaoProcesso">
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Valor da Causa
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtValorCausa" runat="server" Text='<%# Bind("valorCausa", "{0:N2}") %>' Width="100px" CssClass="campoDecimal" ></asp:TextBox>
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
                                                <a href='SituacaoProcesso.aspx?ID=<%# Eval("idSituacaoAtual") %>' target="_blank" >
                                                    <%# ProJur.Business.Bll.bllSituacaoProcesso.Get(Convert.ToInt32(Eval("idSituacaoAtual"))).Descricao %>
                                                </a>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                           <td class="manutencaoFieldHeader">
                                                Valor da Causa
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("valorCausa", "{0:C}") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>    

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate >
                                    [ Dados de Fechamento ]
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Data da Baixa
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDataBaixa"  CssClass="campoData"  runat="server" Width="100px" Text='<%# Bind("dataBaixa") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Data da Baixa
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 150px;">
                                                <%# Eval("dataBaixa", "{0:dd/MM/yyyy}") %>
                                            </td>
        
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Fields>
                    </asp:DetailsView>
                    <asp:ObjectDataSource ID="dsAbertura" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoProcesso"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllProcesso"
                    UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OnInserted="dsAbertura_Inserted">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idProcesso" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="dataCadastro" Type="DateTime" />
                            <asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />
                            <asp:Parameter Name="dataDistribuicao" Type="DateTime" />
                            <asp:Parameter Name="dataBaixa" Type="DateTime" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="dataCadastro" Type="DateTime" />
                            <asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />
                            <asp:Parameter Name="dataDistribuicao" Type="DateTime" />
                            <asp:Parameter Name="dataBaixa" Type="DateTime" />
                        </InsertParameters>
                    </asp:ObjectDataSource>
                </div>
            </asp:Panel>

        </div>

        <div id="tabs-2">

            <asp:Panel ID="pnlProcessoParte" runat="server" Visible="true">
                <div class="manutencaoTituloSecao">
                    [ Partes ]
                </div>
                <div class="manutencaoCorpoSecao">
                    <asp:UpdatePanel ID="upProcessoParte" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="opcoesProcessoParte" class="boxOpcoes">
                                <ul>
                                    <li>
                                        <asp:ImageButton ID="btnAdicionarProcessoParte" runat="server" ToolTip="Adicionar parte"
                                            ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarProcessoParte_Click" />
                                    </li>
                                </ul>                            
                            </div>
                            <div id="processoParte" class="boxResultados">
                                <asp:GridView ID="grdProcessoParte" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsProcessoParte" GridLines="None" OnRowDataBound="grdProcessoParte_RowDataBound"
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idProcessoParte" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhuma parte encontrada</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:BoundField DataField="idProcessoParte" HeaderText="Código" SortExpression="idProcessoParte" >
                                            <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Parte">
                        
                                            <ItemTemplate>
                                                    <a href='Pessoa.aspx?ID=<%# Eval("idPessoaParte") %>' target="_blank" >
                                                        <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaParte"))).NomeCompletoRazaoSocial %>
                                                    </a>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CPF / CNPJ">
                        
                                            <ItemTemplate>
                                                <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaParte"))).CPFCNPJ %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tipo Parte">
                        
                                            <ItemTemplate>
                                                <%# ProJur.Business.Bll.bllProcessoParte.RetornaDescricaoTipoParte(Eval("tipoParte")) %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                                            ToolTip="Remover do processo"
                                                            OnClientClick="return confirm('Deseja realmente remover esta parte?');"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idProcessoParte", "/Paginas/Manutencao/ProcessoParte.aspx?ID={0}") %>&IdProcesso=<%= Request.QueryString["ID"] %>' title="Visualizar os dados" >
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
                                <asp:ObjectDataSource ID="dsProcessoParte" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllProcessoParte" 
                                    DataObjectTypeName="ProJur.Business.Dto.dtoProcessoParte" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idProcesso" QueryStringField="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>

        </div>

        <div id="tabs-3">

            <asp:Panel ID="pnlProcessoAdvogado" runat="server" Visible="true">
                <div class="manutencaoTituloSecao">
                    [ Advogados ]
                </div>
                <div class="manutencaoCorpoSecao">
                    <asp:UpdatePanel ID="upProcessoAdvogado" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="opcoesProcessoAdvogado" class="boxOpcoes">
                                <ul>
                                    <li>
                                        <asp:ImageButton ID="btnAdicionarProcessoAdvogado" runat="server" ToolTip="Adicionar advogado"
                                            ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarProcessoAdvogado_Click" />
                                    </li>
                                </ul>
                            </div>
                            <div id="processoAdvogado" class="boxResultados">
                                <asp:GridView ID="grdProcessoAdvogado" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsProcessoAdvogado" GridLines="None" OnRowDataBound="grdProcessoAdvogado_RowDataBound"
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idProcessoAdvogado" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhum advogado encontrado</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:BoundField DataField="idProcessoAdvogado" HeaderText="Código" SortExpression="idProcessoAdvogado" >
                                            <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Advogado">
                        
                                            <ItemTemplate>
                                                <a href='Pessoa.aspx?ID=<%# Eval("idPessoaAdvogado") %>' target="_blank" >
                                                    <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogado"))).NomeCompletoRazaoSocial %>
                                                </a>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="OAB">
                        
                                            <ItemTemplate>
                                                <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogado"))).advogadoNumeroOAB %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tipo Advogado">
                        
                                            <ItemTemplate>
                                                <%# ProJur.Business.Bll.bllProcessoAdvogado.RetornaDescricaoTipoAdvogado(Eval("tipoAdvogado")) %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                                            ToolTip="Remover do processo"
                                                            OnClientClick="return confirm('Deseja realmente remover este advogado?');"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">
                        
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idProcessoAdvogado", "/Paginas/Manutencao/ProcessoAdvogado.aspx?ID={0}") %>&IdProcesso=<%= Request.QueryString["ID"] %>' title="Visualizar os dados" >
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
                                <asp:ObjectDataSource ID="dsProcessoAdvogado" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllProcessoAdvogado" 
                                    DataObjectTypeName="ProJur.Business.Dto.dtoProcessoAdvogado" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idProcesso" QueryStringField="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>

        </div>

        <div id="tabs-4">

            <asp:Panel ID="pnlProcessoAndamento" runat="server" Visible="true">
                <div class="manutencaoTituloSecao">
                    [ Andamentos ]
                </div>
                <div class="manutencaoCorpoSecao">
                    <asp:UpdatePanel ID="upProcessoAndamento" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="opcoesProcessoAndamento" class="boxOpcoes">
                                <ul>
                                    <li>
                                        <asp:ImageButton ID="btnAdicionarProcessoAndamento" runat="server" ToolTip="Adicionar andamento"
                                            ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarProcessoAndamento_Click" />
                                    </li>
                                </ul>
                            </div>
                            <div id="processoAndamento" class="boxResultados">
                                <asp:GridView ID="grdProcessoAndamento" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsProcessoAndamento" GridLines="None" OnRowDataBound="grdProcessoAndamento_RowDataBound"
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idProcessoAndamento" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhum andamento encontrado</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:BoundField DataField="idProcessoAndamento" HeaderText="Código" SortExpression="idProcessoAndamento" >
                                            <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="dataPublicacao" HeaderText="Data de Publicação" SortExpression="dataPublicacao" DataFormatString="{0:dd/MM/yyyy}" >
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Descrição">
                                            <ItemTemplate>
                                                <%#  FormatText(Eval("Descricao")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>    

                                        <asp:CheckBoxField HeaderText="Visível Cliente" DataField="visivelCliente">
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" ></HeaderStyle>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:CheckBoxField>

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemTemplate>
                                                 <%# this.RetornaHTMLPecaProcessual(Eval("idProcessoAndamento")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                                            ToolTip="Remover do processo"
                                                            OnClientClick="return confirm('Deseja realmente remover este andamento?');"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idProcessoAndamento", "/Paginas/Manutencao/ProcessoAndamento.aspx?ID={0}") %>&IdProcesso=<%= Request.QueryString["ID"] %>' title="Visualizar os dados" >
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
                                <asp:ObjectDataSource ID="dsProcessoAndamento" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllProcessoAndamento" 
                                    DataObjectTypeName="ProJur.Business.Dto.dtoProcessoAndamento" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idProcesso" QueryStringField="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>

        </div>

        <div id="tabs-5">

            <asp:Panel ID="pnlProcessoPeca" runat="server" Visible="true">
                <div class="manutencaoTituloSecao">
                    [ Peças Processuais ]
                </div>
                <div class="manutencaoCorpoSecao">
                    <asp:UpdatePanel ID="upProcessoPeca" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="opcoesProcessoPeca" class="boxOpcoes">
                                <ul>
                                    <li>
                                        <asp:ImageButton ID="btnAdicionarProcessoPeca" runat="server" ToolTip="Adicionar peça" 
                                            ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarProcessoPeca_Click" />
                                    </li>
                                </ul>
                            </div>
                            <div id="processoPeca" class="boxResultados">
                                <asp:GridView ID="grdProcessoPeca" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsProcessoPeca" GridLines="None" OnRowDataBound="grdProcessoPeca_RowDataBound"
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idProcessoPeca" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhuma peça encontrada</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:BoundField DataField="idProcessoPeca" HeaderText="Código" SortExpression="v" >
                                            <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Descrição">
                        
                                            <ItemTemplate>
                                                <a href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualUpload() %><%# Eval("caminhoArquivo", "/{0} ") %>' target="_blank" >
                                                    <%# Eval("Descricao") %>
                                                </a>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Categoria">
                                            <ItemTemplate>
                                                <%# ProJur.Business.Bll.bllCategoriaPeca.Get(Convert.ToInt32(Eval("idCategoriaPecaProcessual"))).Descricao %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CheckBoxField HeaderText="Visível Cliente" DataField="visivelCliente">
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" ></HeaderStyle>
                                            <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:CheckBoxField>

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                                            ToolTip="Remover do processo"
                                                            OnClientClick="return confirm('Deseja realmente remover esta peça?');"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idProcessoPeca", "/Paginas/Manutencao/ProcessoPeca.aspx?ID={0}") %>&IdProcesso=<%= Request.QueryString["ID"] %>' title="Visualizar os dados" >
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
                                <asp:ObjectDataSource ID="dsProcessoPeca" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllProcessoPeca" 
                                    DataObjectTypeName="ProJur.Business.Dto.dtoProcessoPeca" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idProcesso" QueryStringField="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>

        </div>

        <div id="tabs-6">

            <asp:Panel ID="pnlProcessoPrazo" runat="server" Visible="true">
                <div class="manutencaoTituloSecao">
                    [ Prazos ]
                </div>
                <div class="manutencaoCorpoSecao">
                    <asp:UpdatePanel ID="upProcessoPrazo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="opcoesProcessoPrazo" class="boxOpcoes">
                                <ul>
                                    <li>
                                        <asp:ImageButton ID="btnAdicionarProcessoPrazo" runat="server" ToolTip="Adicionar prazo" 
                                            ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarProcessoPrazo_Click" />
                                    </li>
                                </ul>
                            </div>
                            <div id="processoPrazo" class="boxResultados">
                                <asp:GridView ID="grdProcessoPrazo" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsProcessoPrazo" GridLines="None" OnRowDataBound="grdProcessoPrazo_RowDataBound"
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idProcessoPrazo" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhum prazo encontrado</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:TemplateField HeaderText="Tipo Prazo">
                                            <ItemTemplate>
                                                <%# ProJur.Business.Bll.bllTipoPrazoProcessual.Get(Convert.ToInt32(Eval("idTipoPrazo"))).Descricao %>
                                            </ItemTemplate>
                                        </asp:TemplateField>  

                                        <asp:BoundField DataField="dataPublicacao" HeaderText="Data de Publicação" SortExpression="dataPublicacao" DataFormatString="{0:dd/MM/yyyy}" >
                                            <HeaderStyle Width="75px"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="dataVencimento" HeaderText="Data de Vencimento" SortExpression="dataVencimento" DataFormatString="{0:dd/MM/yyyy}" >
                                            <HeaderStyle Width="75px"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Advogado Responsável">
                                            <ItemTemplate>
                                                <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaAdvogadoResponsavel"))).NomeCompletoRazaoSocial %>
                                            </ItemTemplate>
                                        </asp:TemplateField>  

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                                            ToolTip="Remover do processo"
                                                            OnClientClick="return confirm('Deseja realmente remover este prazo?');"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idProcessoPrazo", "/Paginas/Manutencao/ProcessoPrazo.aspx?ID={0}") %>&IdProcesso=<%= Request.QueryString["ID"] %>' title="Visualizar os dados" >
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
                                <asp:ObjectDataSource ID="dsProcessoPrazo" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllProcessoPrazo" 
                                    DataObjectTypeName="ProJur.Business.Dto.dtoProcessoPrazo" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idProcesso" QueryStringField="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>

        </div>

        <div id="tabs-7">

            <asp:Panel ID="pnlProcessoApenso" runat="server" Visible="true">
                <div class="manutencaoTituloSecao">
                    [ Apensos ]
                </div>
                <div class="manutencaoCorpoSecao">
                    <asp:UpdatePanel ID="upProcessoApenso" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="opcoesProcessoApenso" class="boxOpcoes">
                                <ul>
                                    <li>
                                        <asp:ImageButton ID="btnAdicionarProcessoApenso" runat="server" ToolTip="Adicionar apenso" 
                                            ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarProcessoApenso_Click" />
                                    </li>
                                </ul>
                            </div>
                            <div id="processoApenso" class="boxResultados">
                                <asp:GridView ID="grdProcessoApenso" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsProcessoApenso" GridLines="None"
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idProcessoApenso" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhum apenso encontrado</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:BoundField DataField="idProcessoApenso" HeaderText="Código" SortExpression="idProcessoApenso" >
                                            <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Número Processo">
                                            <ItemTemplate>
                                                <%# ProJur.Business.Bll.bllProcesso.Get(Convert.ToInt32(Eval("idProcessoVinculado"))).numeroProcesso %>
                                            </ItemTemplate>
                                        </asp:TemplateField>    

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                                            ToolTip="Remover do processo"
                                                            OnClientClick="return confirm('Deseja realmente remover este apenso?');"  />
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
                                <asp:ObjectDataSource ID="dsProcessoApenso" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllProcessoApenso" 
                                    DataObjectTypeName="ProJur.Business.Dto.dtoProcessoApenso" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idProcesso" QueryStringField="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>

        </div>

        <div id="tabs-8">

            <asp:Panel ID="pnlProcessoDespesa" runat="server" Visible="true">
                <div class="manutencaoTituloSecao">
                    [ Despesas ]
                </div>
                <div class="manutencaoCorpoSecao">
                    <asp:UpdatePanel ID="upProcessoDespesa" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="opcoesProcessoDespesa" class="boxOpcoes">
                                <ul>
                                    <li>
                                        <asp:ImageButton ID="btnAdicionarProcessoDespesa" runat="server" ToolTip="Adicionar despesa" 
                                            ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarProcessoDespesa_Click" />
                                    </li>
                                </ul>
                            </div>
                            <div id="processoDespesa" class="boxResultados">
                                <asp:GridView ID="grdProcessoDespesa" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsProcessoDespesa" GridLines="None" OnRowDataBound="grdProcessoDespesa_RowDataBound"
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idProcessoDespesa" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhuma despesa encontrada</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:BoundField DataField="idProcessoDespesa" HeaderText="Código" SortExpression="idProcessoDespesa" >
                                            <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Descrição">
                                            <ItemTemplate>
                                                <%#  FormatText(Eval("Descricao")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>    
                                        
                                        <asp:BoundField DataField="Valor" HeaderText="Valor da Despesa" ControlStyle-Width="300px" DataFormatString="{0:C}" />

                                        <asp:TemplateField HeaderText="Observações">
                                            <ItemTemplate>
                                                <%#  FormatText(Eval("Observacoes")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>  

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                                            ToolTip="Remover do processo"
                                                            OnClientClick="return confirm('Deseja realmente remover esta despesa?');"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idProcessoDespesa", "/Paginas/Manutencao/ProcessoDespesa.aspx?ID={0}") %>&IdProcesso=<%= Request.QueryString["ID"] %>' title="Visualizar os dados" >
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
                                <asp:ObjectDataSource ID="dsProcessoDespesa" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllProcessoDespesa" 
                                    DataObjectTypeName="ProJur.Business.Dto.dtoProcessoDespesa" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idProcesso" QueryStringField="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>

        </div>

        <div id="tabs-9">

            <asp:Panel ID="pnlProcessoTerceiro" runat="server" Visible="true">
                <div class="manutencaoTituloSecao">
                    [ Terceiros ]
                </div>
                <div class="manutencaoCorpoSecao">
                    <asp:UpdatePanel ID="upProcessoTerceiro" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="opcoesProcessoTerceiro" class="boxOpcoes">
                                <ul>
                                    <li>
                                        <asp:ImageButton ID="btnAdicionarProcessoTerceiro" runat="server" ToolTip="Adicionar terceiro" 
                                            ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarProcessoTerceiro_Click" />
                                    </li>
                                </ul>
                            </div>
                            <div id="processoTerceiro" class="boxResultados">
                                <asp:GridView ID="grdProcessoTerceiro" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsProcessoTerceiro" GridLines="None" OnRowDataBound="grdProcessoTerceiro_RowDataBound"
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idProcessoTerceiro" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhum terceiro encontrado</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:BoundField DataField="idProcessoTerceiro" HeaderText="Código" SortExpression="idProcessoTerceiro" >
                                            <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Terceiro">
                                            <ItemTemplate>
                                                <a href='Pessoa.aspx?ID=<%# Eval("idPessoaTerceiro") %>' target="_blank" >
                                                    CPF/CNPJ: <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaTerceiro"))).CPFCNPJ %> -
                                                        <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaTerceiro"))).NomeCompletoRazaoSocial %>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                                            ToolTip="Remover do processo"
                                                            OnClientClick="return confirm('Deseja realmente remover este terceiro?');"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idProcessoTerceiro", "/Paginas/Manutencao/ProcessoTerceiro.aspx?ID={0}") %>&IdProcesso=<%= Request.QueryString["ID"] %>' title="Visualizar os dados" >
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
                                <asp:ObjectDataSource ID="dsProcessoTerceiro" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllProcessoTerceiro" 
                                    DataObjectTypeName="ProJur.Business.Dto.dtoProcessoTerceiro" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idProcesso" QueryStringField="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>

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
                    <ProJur:dialogSelecaoPessoa ID="dialogSelecaoPessoa" runat="server" />
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

    <script type="text/javascript" src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/jquery.mask.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.campoData').mask('00/00/0000');
            $('.campoCPF').mask('000.000.000-00');
            $('.campoCNPJ').mask('00.000.000/0000-00');
            $('.campoCEP').mask('00000-000');
            $('.campoTelefone').mask('(00) 0000-00000');
            $('.campoDecimal').mask('000.000.000.000.000,00', { reverse: true });
        });
    </script>

</asp:Content>
    