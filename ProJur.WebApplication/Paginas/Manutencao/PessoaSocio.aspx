<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PessoaSocio.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.PessoaSocio" %>
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

    <ProJur:menuAcoesSubManutencao ID="menuAcoes" runat="server" NovoUrl="/Paginas/Manutencao/Pessoa.aspx" PesquisarUrl="/Paginas/Cadastro/Pessoa.aspx" 
            OnCancelarClickHandler="Cancelar_Click" OnEditarClickHandler="Editar_Click" OnExcluirClickHandler="Excluir_Click" OnSalvarClickHandler="Salvar_Click" />

    <div>
        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="ErroValidacaoSummary"
            HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>

    <asp:UpdatePanel runat="server" ID="upManutencaoProcesso">
        <ContentTemplate>

            <asp:Panel ID="pnlPessoaSocio" runat="server">
                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvPessoaSocio" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsPessoaSocio" DataKeyNames="idPessoaSocio"
                        class="DetailsView" OnItemInserting="dvPessoaSocio_ItemInserting" OnItemUpdating="dvPessoaSocio_ItemUpdating">
                        <Fields>

                            <asp:BoundField DataField="idPessoaSocio" HeaderText="Código" InsertVisible="false" ReadOnly="true" Visible="false" />

                            <asp:TemplateField ShowHeader="false" HeaderText="Nome Completo">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome Completo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaNomeCompleto" runat="server" Text='<%# Bind("fisicaNomeCompleto") %>' Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome Completo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 500px;">
                                                <%# Eval("fisicaNomeCompleto") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Nacionalidade">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nacionalidade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaNacionalidade" runat="server" Text='<%# Bind("fisicaNacionalidade") %>' Width="130px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Estado Civil
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlEstadoCivil" SelectedValue='<%# Bind("fisicaEstadoCivil") %>'
                                                    Width="110px">
                                                    <asp:ListItem Value="" Text="< Selecione >" />
                                                    <asp:ListItem Value="S" Text="Solteiro(a)" />
                                                    <asp:ListItem Value="C" Text="Casado(a)" />
                                                    <asp:ListItem Value="D" Text="Divorciado(a)" />
                                                    <asp:ListItem Value="V" Text="Viúvo(a)" />
                                                    <asp:ListItem Value="P" Text="Separado(a)" />
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Profissão
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaProfissao" runat="server" Text='<%# Bind("fisicaProfissao") %>' Width="130px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nacionalidade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 130px;">
                                                <%# Eval("fisicaNacionalidade") %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Estado Civil
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 110px;">
                                                <%#  ProJur.Business.Bll.bllPessoa.RetornaDescricaoEstadoCivil(Eval("fisicaEstadoCivil"))%>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Profissão
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 200px;">
                                                <%#  Eval("fisicaProfissao") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Estado Civil - Escolaridade">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Escolaridade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlEscolaridade" SelectedValue='<%# Bind("fisicaEscolaridade") %>'
                                                    Width="200px">
                                                    <asp:ListItem Value="" Text="< Selecione >" />
                                                    <asp:ListItem Value="FI" Text="Fundamental incompleto" />
                                                    <asp:ListItem Value="FC" Text="Fundamental completo" />
                                                    <asp:ListItem Value="MI" Text="Ensino médio incompleto" />
                                                    <asp:ListItem Value="MC" Text="Ensino médio completo" />
                                                    <asp:ListItem Value="SI" Text="Superior incompleto" />
                                                    <asp:ListItem Value="SC" Text="Superior completo" />
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                PIS / PASEP
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaPISPASEP" runat="server" Text='<%# Bind("fisicaPISPASEP") %>' Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Escolaridade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 200px;">
                                                <%#  ProJur.Business.Bll.bllPessoa.RetornaDescricaoEscolaridade(Eval("fisicaEscolaridade"))%>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                PIS / PASEP
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("fisicaPISPASEP") %>
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
                                                RG - Número
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaRGNumero" runat="server" Text='<%# Bind("fisicaRGNumero") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Órgão Exp.
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaRGOrgaoExpedidor" runat="server" Text='<%# Bind("fisicaRGOrgaoExpedidor") %>' Width="50px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                UF Exp.
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaRGUFExpedidor" runat="server" Text='<%# Bind("fisicaRGUFExpedidor") %>' Width="30px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Exp.
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaRGDataExpedicao"  CssClass="campoData"  runat="server" Width="80px" Text='<%# Bind("fisicaRGDataExpedicao") %>'></asp:TextBox>
                                                <asp:CustomValidator ID="reqFisicaRGDataExpedicaoValida" runat="server" ControlToValidate="txtFisicaRGDataExpedicao" CssClass="ErroValidacao" Text="*" ErrorMessage="Data de Expedição RG inválida" OnServerValidate="reqFisicaRGDataExpedicaoValida_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                                            </td>

                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                RG - Número
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("fisicaRGNumero") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Órgão Exp.
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 50px;">
                                                <%# Eval("fisicaRGOrgaoExpedidor") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                UF Exp.
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 30px;">
                                                <%# Eval("fisicaRGUFExpedidor") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Exp.
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 80px;">
                                                <%# Eval("fisicaRGDataExpedicao", "{0:dd/MM/yyyy}") %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="CPF">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                CPF
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaCPF"  CssClass="campoCPF"  runat="server" Width="120px" Text='<%# Bind("fisicaCPF") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFisicaCPF" ErrorMessage="CPF está em branco" ID="reqFisicaCPFVazio" CssClass="ErroValidacao" Text="*">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                CPF
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%#  ProJur.DataAccess.Utilitarios.AplicarMascaraCPF(Eval("fisicaCPF")) %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate >
                                    [ Endereço ]
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Logradouro">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Logradouro
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEnderecoLogradouro" runat="server" Text='<%# Bind("enderecoLogradouro") %>' Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Logradouro
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("enderecoLogradouro") %>
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
                                                Número
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEnderecoNumero" runat="server" Text='<%# Bind("enderecoNumero") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Complemento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEnderecoComplemento" runat="server" Text='<%# Bind("enderecoComplemento") %>' Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Número
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("enderecoNumero") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Complemento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 200px;">
                                                <%# Eval("enderecoComplemento") %>
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
                                                Bairro
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEnderecoBairro" runat="server" Text='<%# Bind("enderecoBairro") %>' Width="250px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                CEP
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEnderecoCEP" runat="server" Text='<%# Bind("enderecoCEP") %>' Width="80px" CssClass="campoCEP"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Bairro
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 250px;">
                                                <%# Eval("enderecoBairro") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                CEP
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 80px;">
                                                <%# Eval("enderecoCEP") %>
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
                                                Cidade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlCidade"
                                                    DataSourceID="dsCidade"
                                                    DataTextField="Descricao"
                                                    DataValueField="idCidade" SelectedValue='<%# Bind("enderecoIdCidade") %>'
                                                    AppendDataBoundItems="True" Width="250px">  
                                                    <asp:ListItem Value="0" Text="< Selecione >" />
                                                </asp:DropDownList>

                                                <asp:CustomValidator ID="reqEnderecoCidade" runat="server" ControlToValidate="ddlCidade" CssClass="ErroValidacao" Text="*" ErrorMessage="Cidade está em branco" OnServerValidate="reqEnderecoCidade_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>

                                                <asp:ObjectDataSource ID="dsCidade" runat="server" 
                                                    DataObjectTypeName="ProJur.Business.Dto.dtoCidade"
                                                    SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllCidade">   

                                                    <SelectParameters>
                                                        <asp:Parameter Name="SortExpression" DefaultValue="Descricao"  />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Estado
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlEstado"
                                                    DataSourceID="dsEstado"
                                                    DataTextField="siglaUFDescricao"
                                                    DataValueField="idEstado" SelectedValue='<%# Bind("enderecoIdEstado") %>'
                                                    AppendDataBoundItems="True" Width="200px">  
                                                    <asp:ListItem Value="0" Text="< Selecione >" />
                                                </asp:DropDownList>

                                                <asp:CustomValidator ID="reqEnderecoEstado" runat="server" ControlToValidate="ddlEstado" CssClass="ErroValidacao" Text="*" ErrorMessage="Estado está em branco" OnServerValidate="reqEnderecoEstado_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>

                                                <asp:ObjectDataSource ID="dsEstado" runat="server" 
                                                    DataObjectTypeName="ProJur.Business.Dto.dtoEstado" 
                                                    SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllEstado">   
                             
                                                    <SelectParameters>
                                                        <asp:Parameter Name="SortExpression" DefaultValue="siglaUF"  />
                                                    </SelectParameters>

                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Cidade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 250px;">
                                                <%# ProJur.Business.Bll.bllCidade.Get(Convert.ToInt32(Eval("enderecoIdCidade"))).Descricao %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Estado
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 200px;">
                                                <%# ProJur.Business.Bll.bllEstado.Get(Convert.ToInt32(Eval("enderecoIdEstado"))).siglaUF %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate>
                                    [ Dados complementares ]
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

<asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Data de Nascimento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDataNascimento"  CssClass="campoData"  runat="server" Width="100px" Text='<%# Bind("fisicaDataNascimento") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataNascimento" ErrorMessage="Data Nascimento está em branco" ID="reqDataNascimentoVazio" CssClass="ErroValidacao" Text="*">
                                                </asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="reqDataNascimentoValida" runat="server" ControlToValidate="txtDataNascimento" CssClass="ErroValidacao" Text="*" ErrorMessage="Data de Nascimento inválida" OnServerValidate="reqDataNascimentoValida_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Sexo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlSexo" SelectedValue='<%# Bind("fisicaSexo") %>'
                                                    Width="100px">
                                                    <asp:ListItem Value="" Text="< Selecione >" />
                                                    <asp:ListItem Value="M" Text="Masculino" />
                                                    <asp:ListItem Value="F" Text="Feminino" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSexo" ErrorMessage="Sexo não foi selecionado" ID="reqddlSexo" CssClass="ErroValidacao" Text="*">

                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Naturalidade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaNaturalidade" runat="server" Text='<%# Bind("fisicaNaturalidade") %>' Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Data de Nascimento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%#  ProJur.DataAccess.Utilitarios.AplicaFormatacaoData(Eval("fisicaDataNascimento")) %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Sexo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%#  ProJur.Business.Bll.bllPessoa.RetornaDescricaoSexo(Eval("fisicaSexo")) %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Naturalidade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 150px;">
                                                <%# Eval("fisicaNaturalidade") %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate>
                                    [ CTPS - Carteira de Trabalho ]
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="CTPS">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Número
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaCTPSNumero" runat="server" Text='<%# Bind("fisicaCTPSNumero") %>' Width="150px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Série - UF
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaCTPSSerie" runat="server" Text='<%# Bind("fisicaCTPSSerie") %>' Width="150px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Emissão
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaCTPSDataExpedicao"  CssClass="campoData"  runat="server" Width="100px" Text='<%# Bind("fisicaCTPSDataExpedicao") %>'></asp:TextBox>
                                                <asp:CustomValidator ID="reqFisicaCTPSDataExpedicaoValida" runat="server" ControlToValidate="txtFisicaCTPSDataExpedicao" CssClass="ErroValidacao" Text="*" ErrorMessage="Data de Expdição CTPS inválida" OnServerValidate="reqFisicaCTPSDataExpedicaoValida_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Número
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 150px;">
                                                <%#  Eval("fisicaCTPSNumero") %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Série - UF
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 150px;">
                                                <%#  Eval("fisicaCTPSSerie") %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Emissão
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("fisicaCTPSDataExpedicao", "{0:dd/MM/yyyy}") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate>
                                    [ CNH ]
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

<asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Número
                                            </td>
                                            <td style="width: 5px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaCNHNumero" runat="server" Text='<%# Bind("fisicaCNHNumero") %>' Width="110px"></asp:TextBox>
                                            </td>
                                            <td style="width: 20px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Categoria
                                            </td>
                                            <td style="width: 5px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaCNHCategoria" runat="server" Text='<%# Bind("fisicaCNHCategoria") %>' Width="30px"></asp:TextBox>
                                            </td>
                                            <td style="width: 20px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data 1ª Habilitação
                                            </td>
                                            <td style="width: 5px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCNHDataHabilitacao"  CssClass="campoData"  runat="server" Width="70px" Text='<%# Bind("fisicaCNHDataHabilitacao") %>'></asp:TextBox>
                                                <asp:CustomValidator ID="reqCNHDataHabilitacaoValida" runat="server" ControlToValidate="txtCNHDataHabilitacao" CssClass="ErroValidacao" Text="*" ErrorMessage="Data da 1ª Habilitação inválida" OnServerValidate="reqCNHDataHabilitacaoValida_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                                            </td>
                                            <td style="width: 20px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data Emissão
                                            </td>
                                            <td style="width: 5px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCNHDataEmissao"  CssClass="campoData"  runat="server" Width="70px" Text='<%# Bind("fisicaCNHDataEmissao") %>'></asp:TextBox>
                                                <asp:CustomValidator ID="reqCNHDataEmissaoValida" runat="server" ControlToValidate="txtCNHDataEmissao" CssClass="ErroValidacao" Text="*" ErrorMessage="Data de Emissão CNH inválida" OnServerValidate="reqCNHDataEmissaoValida_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Número
                                            </td>
                                            <td style="width: 5px;">

                                            </td>
                                            <td style="width: 110px;">
                                                <%# Eval("fisicaCNHNumero") %>
                                            </td>
                                            <td style="width: 20px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Categoria
                                            </td>
                                            <td style="width: 5px;">

                                            </td>
                                            <td style="width: 30px;">
                                                <%# Eval("fisicaCNHCategoria") %>
                                            </td>
                                            <td style="width: 20px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data 1ª Habilitação
                                            </td>
                                            <td style="width: 5px;">

                                            </td>
                                            <td style="width: 70px;">
                                                <%# ProJur.DataAccess.Utilitarios.AplicaFormatacaoData(Eval("fisicaCNHDataHabilitacao")) %>
                                            </td>
                                            <td style="width: 20px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data Emissão
                                            </td>
                                            <td style="width: 5px;">

                                            </td>
                                            <td style="width: 70px;">
                                                <%# ProJur.DataAccess.Utilitarios.AplicaFormatacaoData(Eval("fisicaCNHDataEmissao")) %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>                            

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate>
                                    [ Filiação ]
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Nome do Pai">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome do Pai
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaFiliacaoNomePai" runat="server" Text='<%# Bind("fisicaFiliacaoNomePai") %>' Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome do Pai
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("fisicaFiliacaoNomePai") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Nome da Mãe">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome da Mãe
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaFiliacaoNomeMae" runat="server" Text='<%# Bind("fisicaFiliacaoNomeMae") %>' Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome da Mãe
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("fisicaFiliacaoNomeMae") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate>
                                    [ Conjuguê ]
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Nome Completo">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome Completo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaConjugueNomeCompleto" runat="server" Text='<%# Bind("fisicaConjugueNomeCompleto") %>' Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome Completo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("fisicaConjugueNomeCompleto") %>
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
                                                CPF
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtconjugueCPF"  CssClass="campoCPF"  runat="server" Width="120px" Text='<%# Bind("fisicaConjugueCPF") %>'></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Nascimento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtConjugueDataNascimento"  CssClass="campoData"  runat="server" Width="100px" Text='<%# Bind("fisicaConjugueDataNascimento") %>'></asp:TextBox>
                                                <asp:CustomValidator ID="reqConjugueDataNascimentoValida" runat="server" ControlToValidate="txtConjugueDataNascimento" CssClass="ErroValidacao" Text="*" ErrorMessage="Data de Nascimento Conjuguê inválida" OnServerValidate="reqConjugueDataNascimentoValida_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                CPF
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 120px;">
                                                <%#  ProJur.DataAccess.Utilitarios.AplicarMascaraCPF(Eval("fisicaConjugueCPF")) %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Nascimento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%#  ProJur.DataAccess.Utilitarios.AplicaFormatacaoData(Eval("fisicaConjugueDataNascimento")) %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Regime de Comunhão de Bens">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Regime de Comunhão de Bens
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="dllRegimeComunhaoBens" SelectedValue='<%# Bind("fisicaRegimeComunhaoBens") %>'
                                                    Width="250px">
                                                    <asp:ListItem Value="" Text="< Selecione >" />
                                                    <asp:ListItem Value="CP" Text="Comunhão parcial de bens" />
                                                    <asp:ListItem Value="CU" Text="Comunhão universal de bens" />
                                                    <asp:ListItem Value="ST" Text="Separação total de bens" />
                                                    <asp:ListItem Value="PF" Text="Participação final nos aquestos" />
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Regime de Comunhão de Bens
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%#  ProJur.Business.Bll.bllPessoa.RetornaDescricaoRegimeComunhaoBens(Eval("fisicaRegimeComunhaoBens"))%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

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
                                    [ Dados de Contato ]
                                </HeaderTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderStyle Height="1px"  />
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                                <HeaderTemplate></HeaderTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Telefone Residencial
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContatoTelefoneResidencial" runat="server" Text='<%# Bind("contatoTelefoneResidencial") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Telefone Comercial
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContatoTelefoneComercial" runat="server" Text='<%# Bind("contatoTelefoneComercial") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContatoTelefoneCelular" runat="server" Text='<%# Bind("contatoTelefoneCelular") %>' Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Telefone Residencial
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("contatoTelefoneResidencial") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Telefone Comercial
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("contatoTelefoneComercial") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("contatoTelefoneCelular") %>
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
                                                Email
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContatoEmail" runat="server" Text='<%# Bind("contatoEmail") %>' Width="500px"></asp:TextBox>
                                            </td>
 
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Email
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 500px;">
                                                <%# Eval("contatoEmail") %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>


                        </Fields>
                    </asp:DetailsView>

                    <asp:ObjectDataSource ID="dsPessoaSocio" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoPessoaSocio"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllPessoaSocio"
                        UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" 
                        oninserted="dsPessoaSocio_Inserted">
                
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idPessoaSocio" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>

                        <InsertParameters>
                            <asp:Parameter Name="fisicaDataNascimento" Type="DateTime" />
                            <asp:Parameter Name="fisicaRGDataExpedicao" Type="DateTime" />
                            <asp:Parameter Name="fisicaCNHDataHabilitacao" Type="DateTime" />
                            <asp:Parameter Name="fisicaCNHDataEmissao" Type="DateTime" />
                            <asp:Parameter Name="fisicaCTPSDataExpedicao" Type="DateTime" />
                            <asp:Parameter Name="fisicaConjugueDataNascimento" Type="DateTime" />
                            <asp:Parameter Name="fisicaConjugueCPF" Type="String" />                                       
                        </InsertParameters>

                        <UpdateParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="idPessoa" Type="Int32" />
                            <asp:Parameter Name="fisicaDataNascimento" Type="DateTime" />
                            <asp:Parameter Name="fisicaRGDataExpedicao" Type="DateTime" />
                            <asp:Parameter Name="fisicaCNHDataHabilitacao" Type="DateTime" />
                            <asp:Parameter Name="fisicaCNHDataEmissao" Type="DateTime" />
                            <asp:Parameter Name="fisicaCTPSDataExpedicao" Type="DateTime" />
                            <asp:Parameter Name="fisicaConjugueDataNascimento" Type="DateTime" />      
                        </UpdateParameters>

                    </asp:ObjectDataSource>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
