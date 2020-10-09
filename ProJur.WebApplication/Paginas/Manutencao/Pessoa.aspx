<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pessoa.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.Pessoa" %>
<%@ Register Src="~/Controls/menuAcoesManutencao.ascx" TagName="menuAcoesManutencao" TagPrefix="ProJur" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/gridview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/gridview.js" type="text/javascript"></script>

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/detailsview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/detailsview.js" type="text/javascript"></script>

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/manutencao.main.css" rel="stylesheet" type="text/css" />
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/cadastro.main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs();
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <ProJur:menuAcoesManutencao ID="barraAcoes" runat="server" NovoUrl="/Paginas/Manutencao/Pessoa.aspx" PesquisarUrl="/Paginas/Cadastro/Pessoa.aspx" />

    <div>
        <asp:ValidationSummary ID="valSummary" runat="server" CssClass="ErroValidacaoSummary"
            HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>

    <div id="tabs" style="border: none;">

	    <ul>
		    <li runat="server" id="liAbertura"><a href="#tabs-1">Abertura</a></li>
		    <li runat="server" id="liPessoaFisica"><a href="#tabs-2">Pessoa Física</a></li>
		    <li runat="server" id="liPessoaJuridica"><a href="#tabs-3">Pessoa Jurídica</a></li>
            <li runat="server" id="liSocios"><a href="#tabs-4">Sócios</a></li>
            <li runat="server" id="liContato"><a href="#tabs-5">Contato</a></li>
            <li runat="server" id="liDadosProfissionais"><a href="#tabs-6">Dados Profissionais</a></li>
            <li runat="server" id="liReferencias"><a href="#tabs-7">Referências</a></li>
            <li runat="server" id="liColaborador"><a href="#tabs-8">Coladorador</a></li>
            <li runat="server" id="liAdvogado"><a href="#tabs-9">Advogado</a></li>
            <li runat="server" id="liVinculos"><a href="#tabs-10">Processos</a></li>
            <li runat="server" id="liAgenda"><a href="#tabs-11">Agenda</a></li>
	    </ul>

        <div id="tabs-1">
            <asp:Panel ID="pnlAbertura" runat="server">

                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvManutencao" runat="server" Width="100%" AutoGenerateRows="False"
                    CellPadding="10" GridLines="None" DataSourceID="dsManutencao" DataKeyNames="idPessoa"
                    class="DetailsView" OnItemInserting="dvManutencao_ItemInserting" OnDataBound="dvManutencao_DataBound"
                    OnItemUpdating="dvManutencao_ItemUpdating">
            
                    <FieldHeaderStyle CssClass="manutencaoFieldHeader" Width="190px" />

                    <Fields>
                        <asp:BoundField DataField="idPessoa" HeaderText="Código" InsertVisible="false" ReadOnly="true" />
                
                        <asp:BoundField DataField="dataCadastro" HeaderText="Data de Cadastro" InsertVisible="false"
                            ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                
                        <asp:BoundField DataField="dataUltimaAlteracao" HeaderText="Data da Últ. Alteração"
                            InsertVisible="false" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />

                        <asp:TemplateField HeaderText="Espécie de Pessoa">
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlEspeciePessoa" SelectedValue='<%# Bind("especiePessoa") %>'
                                    Width="150px" OnTextChanged="ddlEspeciePessoa_Click" AutoPostBack="true">
                                    <asp:ListItem Value="" Text="< Selecione >" />
                                    <asp:ListItem Value="F" Text="Física" />
                                    <asp:ListItem Value="J" Text="Jurídica" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEspeciePessoa" ErrorMessage="Espécie de pessoa não foi selecionada" ID="reqddlEspeciePessoa" CssClass="ErroValidacao" Text="*">

                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#  ProJur.Business.Bll.bllPessoa.RetornaDescricaoEspeciePessoa(Eval("especiePessoa"))%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tipo de Pessoa">
                            <EditItemTemplate>
                                <asp:CheckBoxList ID="chkTipoPessoa" runat="server"
                                    AutoPostBack="true" RepeatDirection="Horizontal" DataSourceID="dsTipoPessoa"
                                    DataTextField="Descricao" DataValueField="ValorChave" OnSelectedIndexChanged="chkTipoPessoa_OnSelectedIndexChanged">
                                </asp:CheckBoxList>
                                <asp:ObjectDataSource ID="dsTipoPessoa" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoListItem"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetTipoPessoa" TypeName="ProJur.Business.Bll.bllDataTable">
                                </asp:ObjectDataSource>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <input id="chkTipoPessoaColaborador" type="checkbox" <%# ConverteEstadoCheckBox(Eval("tipoPessoaColaborador")) %>
                                                disabled="disabled" />
                                            <label for="chkTipoPessoaColaborador">
                                                Colaborador</label>
                                        </td>
                                        <td>
                                            <input id="chkTipoPessoaCliente" type="checkbox" <%# ConverteEstadoCheckBox(Eval("tipoPessoaCliente")) %>
                                                disabled="disabled" />
                                            <label for="chkTipoPessoaCliente">
                                                Cliente</label>
                                        </td>
                                        <td>
                                            <input id="chkTipoPessoaParte" type="checkbox" <%# ConverteEstadoCheckBox(Eval("tipoPessoaParte")) %>
                                                disabled="disabled" />
                                            <label for="chkTipoPessoaParte">
                                                Parte</label>
                                        </td>
                                        <td>
                                            <input id="chkTipoPessoaAdvogado" type="checkbox" <%# ConverteEstadoCheckBox(Eval("tipoPessoaAdvogado")) %>
                                                disabled="disabled" />
                                            <label for="chkTipoPessoaAdvogado">
                                                Advogado
                                            </label>
                                        </td>
                                        <td>
                                            <input id="chkTipoPessoaTerceiro" type="checkbox" <%# ConverteEstadoCheckBox(Eval("tipoPessoaTerceiro")) %>
                                                disabled="disabled" />
                                            <label for="chkTipoPessoaTerceiro">
                                                Terceiro</label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Fields>
                </asp:DetailsView>
                    <asp:ObjectDataSource ID="dsManutencao" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoPessoa"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllPessoa"
                    UpdateMethod="Update" InsertMethod="Insert" DeleteMethod="Delete" OnInserted="dsManutencao_Inserted">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="tipoPessoaColaborador" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaCliente" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaParte" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaAdvogado" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaTerceiro" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaSocio" Type="Boolean" />
                        <asp:Parameter Name="dataCadastro" Type="DateTime" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="tipoPessoaColaborador" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaCliente" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaParte" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaAdvogado" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaTerceiro" Type="Boolean" />
                        <asp:Parameter Name="tipoPessoaSocio" Type="Boolean" />
                        <asp:Parameter Name="dataCadastro" Type="DateTime" />
                        <asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
                    <br />
                </div>

            </asp:Panel>
        </div>

        <div id="tabs-2">
            <asp:Panel ID="pnlPessoaFisica" runat="server" Visible="false">
        
                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvPessoaFisica" runat="server" Width="100%" AutoGenerateRows="False" 
                        CellPadding="10" GridLines="None" DataSourceID="dsPessoaFisica" DataKeyNames="idPessoa"
                        class="DetailsView" oniteminserting="dvPessoaGenerico_ItemInserting">
                        
                        <Fields>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate>
                                    [ Dados Principais ]
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
                                                Endereço
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
                                                NIT / PIS / PASEP
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaPISPASEP" runat="server" Text='<%# Bind("fisicaPISPASEP") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                CadSenha
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFisicaCadSenha" runat="server" Text='<%# Bind("fisicaCadSenha") %>' Width="100px"></asp:TextBox>
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
                                               NIT / PIS / PASEP
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("fisicaPISPASEP") %>
                                            </td>
                                            <td style="width: 100px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                               CadSenha
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("fisicaCadSenha") %>
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
<%--                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFisicaCPF" ErrorMessage="CPF está em branco" ID="reqFisicaCPFVazio" CssClass="ErroValidacao" Text="*">
                                                </asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="reqFisicaCPFObrigatorio" runat="server" ControlToValidate="txtFisicaCPF" CssClass="ErroValidacao" Text="*" ErrorMessage="Este CPF já existe" OnServerValidate="reqFisicaCPFObrigatorio_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>--%>
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

<%--                                                <asp:CustomValidator ID="reqEnderecoCidade" runat="server" ControlToValidate="ddlCidade" CssClass="ErroValidacao" Text="*" ErrorMessage="Cidade está em branco" OnServerValidate="reqEnderecoCidade_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>--%>

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

                                                <%--<asp:CustomValidator ID="reqEnderecoEstado" runat="server" ControlToValidate="ddlEstado" CssClass="ErroValidacao" Text="*" ErrorMessage="Estado está em branco" OnServerValidate="reqEnderecoEstado_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>--%>

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
<%--                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataNascimento" ErrorMessage="Data Nascimento está em branco" ID="reqDataNascimentoVazio" CssClass="ErroValidacao" Text="*">
                                                </asp:RequiredFieldValidator>--%>
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
<%--                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSexo" ErrorMessage="Sexo não foi selecionado" ID="reqddlSexo" CssClass="ErroValidacao" Text="*">

                                                </asp:RequiredFieldValidator>--%>
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

                        </Fields>
                    </asp:DetailsView>

                    <asp:ObjectDataSource ID="dsPessoaFisica" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoPessoa"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllPessoa"
                        UpdateMethod="UpdatePessoaFisica" InsertMethod="InsertPessoaFisica" DeleteMethod="Delete" 
                        oninserted="dsPessoaGenerico_Inserted">
                
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
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
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                            <asp:Parameter Name="fisicaDataNascimento" Type="DateTime" />
                            <asp:Parameter Name="fisicaRGDataExpedicao" Type="DateTime" />
                            <asp:Parameter Name="fisicaCNHDataHabilitacao" Type="DateTime" />
                            <asp:Parameter Name="fisicaCNHDataEmissao" Type="DateTime" />
                            <asp:Parameter Name="fisicaCTPSDataExpedicao" Type="DateTime" />
                            <asp:Parameter Name="fisicaConjugueDataNascimento" Type="DateTime" />      
                        </UpdateParameters>

                    </asp:ObjectDataSource>
                    <br />
                </div>

            </asp:Panel>
        </div>

        <div id="tabs-3">
            <asp:Panel ID="pnlPessoaJuridica" runat="server" Visible="false">
                <div class="manutencaoTituloSecao">
                    [ Identificação ]
                </div>

                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvPessoaJuridica" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsPessoaJuridica" DataKeyNames="idPessoa"
                        class="DetailsView" oniteminserting="dvPessoaGenerico_ItemInserting">

                        <Fields>
                            
                            <asp:TemplateField ShowHeader="false" HeaderText="Razão Social">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Razão Social
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJuridicaRazaoSocial" runat="server" Text='<%# Bind("juridicaRazaoSocial") %>' Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Razão Social
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("juridicaRazaoSocial") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Nome Fantasia">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome Fantasia
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJuridicaNomeFantasia" runat="server" Text='<%# Bind("juridicaNomeFantasia") %>' Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome Fantasia
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("juridicaNomeFantasia") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="CNPJ">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                CNPJ
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCNPJ"  CssClass="campoCNPJ"  runat="server" Width="130px" Text='<%# Bind("juridicaCNPJ") %>'></asp:TextBox>
<%--                                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCNPJ" ErrorMessage="CNPJ está em branco" ID="reqCPFVazio" CssClass="ErroValidacao" Text="*">
                                                </asp:RequiredFieldValidator>--%>
<%--                                                <asp:CustomValidator ID="reqCNPJObrigatorio" runat="server" ControlToValidate="txtCNPJ" CssClass="ErroValidacao" Text="*" ErrorMessage="Este txtCNPJ já existe" OnServerValidate="reqCNPJObrigatorio_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>--%>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Inscrição Estadual
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJuridicaInscricaoEstadual" runat="server" Text='<%# Bind("juridicaInscricaoEstadual") %>' Width="120px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Inscrição Municipal
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJuridicaInscricaoMunicipal" runat="server" Text='<%# Bind("juridicaInscricaoMunicipal") %>' Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                CNPJ
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 130px;">
                                                <%#  ProJur.DataAccess.Utilitarios.AplicarMascaraCNPJ(Eval("juridicaCNPJ")) %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Inscrição Estadual
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 120px;">
                                                <%# Eval("juridicaInscricaoEstadual") %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Inscrição Municipal
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("juridicaInscricaoMunicipal") %>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" HeaderText="Inscrição Municipal">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Ramo de Atividade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtJuridicaRamoAtividade" runat="server" Text='<%# Bind("juridicaRamoAtividade") %>' Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Fundação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDataFundacao"  CssClass="campoData"  runat="server" Width="80px" Text='<%# Bind("juridicaDataFundacao") %>'></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Ramo de Atividade
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 200px;">
                                                <%# Eval("juridicaRamoAtividade") %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Fundação
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 80px;">
                                                <%# ProJur.DataAccess.Utilitarios.AplicaFormatacaoData(Eval("juridicaDataFundacao")) %>
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

                                                <%--<asp:CustomValidator ID="reqEnderecoCidade" runat="server" ControlToValidate="ddlCidade" CssClass="ErroValidacao" Text="*" ErrorMessage="Cidade está em branco" OnServerValidate="reqEnderecoCidade_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>--%>

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

                                                <%--<asp:CustomValidator ID="reqEnderecoEstado" runat="server" ControlToValidate="ddlEstado" CssClass="ErroValidacao" Text="*" ErrorMessage="Estado está em branco" OnServerValidate="reqEnderecoEstado_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>--%>

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

                        </Fields>
                    </asp:DetailsView>

                    <asp:ObjectDataSource ID="dsPessoaJuridica" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoPessoa"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllPessoa"
                        UpdateMethod="UpdatePessoaJuridica" InsertMethod="InsertPessoaJuridica" DeleteMethod="Delete" 
                        oninserted="dsPessoaGenerico_Inserted">
                
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>

                        <UpdateParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                            <asp:Parameter Name="juridicaDataFundacao" Type="DateTime" />
                        </UpdateParameters>

                    </asp:ObjectDataSource>

                    <br />
                </div>
            </asp:Panel>
        </div>

        <div id="tabs-4">
            <asp:Panel ID="pnlPessoaSocio" runat="server" Visible="true">
                <div class="manutencaoTituloSecao">
                    [ Sócios ]
                </div>
                <div class="manutencaoCorpoSecao">
                    <asp:UpdatePanel ID="upPessoaSocio" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="opcoesPessoaSocio" class="boxOpcoes">
                                <ul>
                                    <li>
                                        <asp:ImageButton ID="btnAdicionarPessoaSocio" runat="server" ToolTip="Adicionar sócio"
                                            ImageUrl="~/Images/Comandos/Adicionar.png" onclick="btnAdicionarPessoaSocio_Click" />
                                    </li>
                                </ul>
                            </div>

                            <div id="pessoaSocio" class="boxResultados">
                                <asp:GridView ID="grdPessoaSocio" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsPessoaSocio" GridLines="None" 
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idPessoaSocio" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhum sócio encontrado</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:BoundField DataField="idPessoaSocio" HeaderText="Código" SortExpression="idPessoaSocio" >
                                            <HeaderStyle Width="65px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Nome Completo">
                        
                                            <ItemTemplate>
                                                <a href='PessoaSocio.aspx?ID=<%# Eval("idPessoaSocio") %>' target="_blank" >
                                                    <%# ProJur.Business.Bll.bllPessoaSocio.Get(Convert.ToInt32(Eval("idPessoaSocio"))).fisicaNomeCompleto %>
                                                </a>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False">
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkRemoverItem" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="Remover" ImageUrl="~/Images/Comandos-Small/remover.png" CssClass="botaoAcao"
                                                            ToolTip="Remover sócio"
                                                            OnClientClick="return confirm('Deseja realmente remover este sócio?');"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">
                        
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idPessoaSocio", "/Paginas/Manutencao/PessoaSocio.aspx?ID={0}") %>&idPessoa=<%= Request.QueryString["ID"] %>' title="Visualizar os dados" >
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
                                <asp:ObjectDataSource ID="dsPessoaSocio" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllPessoaSocio" 
                                    DataObjectTypeName="ProJur.Business.Dto.dtoPessoaSocio" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </div>

        <div id="tabs-5">
            <asp:Panel ID="pnlContato" runat="server" Visible="false">
    
                <div class="manutencaoTituloSecao">
                    [ Contato ]
                </div>

                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvContato" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsContato" DataKeyNames="idPessoa"
                        class="DetailsView" oniteminserting="dvPessoaGenerico_ItemInserting">
                        <Fields>

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

                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Celular 1
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <asp:TextBox ID="txtContatoTelefoneCelular" runat="server" Text='<%# Bind("contatoTelefoneCelular") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular 2
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <asp:TextBox ID="txtContatoTelefoneCelular1" runat="server" Text='<%# Bind("contatoTelefoneCelular1") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular 3
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <asp:TextBox ID="txtContatoTelefoneCelular2" runat="server" Text='<%# Bind("contatoTelefoneCelular2") %>' Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Celular 1
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("contatoTelefoneCelular") %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular 2
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("contatoTelefoneCelular1") %>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular 3
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("contatoTelefoneCelular2") %>
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

                            <asp:TemplateField ShowHeader="false">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Observações
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtContatoObservacao" runat="server" Text='<%# Bind("contatoObservacao") %>' Width="500px" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                            </td>
 
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Observações
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 500px;">
                                                <%# this.FormatText(Eval("contatoObservacao")) %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Fields>
                    </asp:DetailsView>

                    <asp:ObjectDataSource ID="dsContato" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoPessoa"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllPessoa"
                        UpdateMethod="UpdateContato" InsertMethod="InsertContato" DeleteMethod="Delete" >
                
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>

                        <UpdateParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                        </UpdateParameters>

                    </asp:ObjectDataSource>

                    <br />
                </div>

            </asp:Panel>
        </div>

        <div id="tabs-6">
            <asp:Panel ID="pnlDadosProfissionais" runat="server" Visible="false">
    
                <div class="manutencaoCorpoSecao">
                    <asp:DetailsView ID="dvDadosProfissionais" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsDadosProfissionais" DataKeyNames="idPessoa"
                        class="DetailsView" oniteminserting="dvPessoaGenerico_ItemInserting" >
                        <FieldHeaderStyle CssClass="manutencaoFieldHeader" Width="120px" />
                        <Fields>

                            <asp:TemplateField ShowHeader="false" HeaderText="Nome da Empresa">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome da Empresa
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDadosProfissionaisNomeEmpresa" runat="server" Text='<%# Bind("dadosProfissionaisNomeEmpresa") %>' Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Nome da Empresa
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <%# Eval("dadosProfissionaisNomeEmpresa") %>
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
                                                Cargo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDadosProfissionaisCargo" runat="server" Width="200px" Text='<%# Bind("dadosProfissionaisCargo") %>'></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Admissão
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDadosProfissionaisDataAdmissao"  CssClass="campoData"  runat="server" Width="100px" Text='<%# Bind("dadosProfissionaisDataAdmissao") %>'></asp:TextBox>
                                                <asp:CustomValidator ID="reqDadosProfissionaisDataAdmissaoValida" runat="server" ControlToValidate="txtDadosProfissionaisDataAdmissao" CssClass="ErroValidacao" Text="*" ErrorMessage="Data de Adminissão Profissional inválida" OnServerValidate="reqDadosProfissionaisDataAdmissaoValida_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Cargo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 200px;">
                                                <%#  Eval("dadosProfissionaisCargo") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Data de Admissão
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%#  ProJur.DataAccess.Utilitarios.AplicaFormatacaoData(Eval("dadosProfissionaisDataAdmissao")) %>
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
                                                <asp:TextBox ID="txtDadosProfissionaisEnderecoLogradouro" runat="server" Text='<%# Bind("dadosProfissionaisEnderecoLogradouro") %>' Width="500px"></asp:TextBox>
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
                                                <%# Eval("dadosProfissionaisEnderecoLogradouro") %>
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
                                                <asp:TextBox ID="txtDadosProfissionaisEnderecoNumero" runat="server" Text='<%# Bind("dadosProfissionaisEnderecoNumero") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Complemento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEnderecoComplemento" runat="server" Text='<%# Bind("dadosProfissionaisEnderecoComplemento") %>' Width="200px"></asp:TextBox>
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
                                                <%# Eval("dadosProfissionaisEnderecoNumero") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Complemento
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 200px;">
                                                <%# Eval("dadosProfissionaisEnderecoComplemento") %>
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
                                                <asp:TextBox ID="txtDadosProfissionaisEnderecoBairro" runat="server" Text='<%# Bind("dadosProfissionaisEnderecoBairro") %>' Width="250px"></asp:TextBox>
                                            </td>
                                            <td style="width: 30px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                CEP
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDadosProfissionaisEnderecoCEP" runat="server" Text='<%# Bind("dadosProfissionaisEnderecoCEP") %>' Width="80px" CssClass="campoCEP"></asp:TextBox>
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
                                                <%# Eval("dadosProfissionaisEnderecoBairro") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                CEP
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 80px;">
                                                <%# Eval("dadosProfissionaisEnderecoCEP") %>
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
                                                    DataValueField="idCidade" SelectedValue='<%# Bind("dadosProfissionaisEnderecoIdCidade") %>'
                                                    AppendDataBoundItems="True" Width="250px">  
                                                    <asp:ListItem Value="0" Text="< Selecione >" />
                                                </asp:DropDownList>

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
                                                    DataValueField="idEstado" SelectedValue='<%# Bind("dadosProfissionaisEnderecoIdCidade") %>'
                                                    AppendDataBoundItems="True" Width="200px">  
                                                    <asp:ListItem Value="0" Text="< Selecione >" />
                                                </asp:DropDownList>

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
                                                <%# ProJur.Business.Bll.bllCidade.Get(Convert.ToInt32(Eval("dadosProfissionaisEnderecoIdCidade"))).Descricao %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Estado
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 200px;">
                                                <%# ProJur.Business.Bll.bllEstado.Get(Convert.ToInt32(Eval("dadosProfissionaisEnderecoIdEstado"))).siglaUF %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderStyle CssClass="manutencaoTituloSecao" />
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <HeaderTemplate >
                                    [ Contato Empresa ]
                                </HeaderTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderStyle Height="1px"  />
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                                <HeaderTemplate></HeaderTemplate>
                            </asp:TemplateField>

                            <InfoVillage:BoundTextBoxField DataField="dadosProfissionaisContatoNomeCompleto" HeaderText="Nome Completo"
                                ControlStyle-Width="500px" MaxLength="50" />

                            <InfoVillage:BoundTextBoxField DataField="dadosProfissionaisContatoTelefone" HeaderText="Telefone"
                                ControlStyle-Width="150px" MaxLength="20" />

                        </Fields>
                    </asp:DetailsView>

                    <asp:ObjectDataSource ID="dsDadosProfissionais" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoPessoa"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllPessoa"
                        UpdateMethod="UpdateDadosProfissionais" InsertMethod="InsertDadosProfissionais" DeleteMethod="Delete" >
                
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>

                        <InsertParameters>
                            <asp:Parameter Name="dadosProfissionaisDataAdmissao" Type="DateTime" />
                        </InsertParameters>

                        <UpdateParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                            <asp:Parameter Name="dadosProfissionaisDataAdmissao" Type="DateTime" />
                        </UpdateParameters>

                    </asp:ObjectDataSource>

                    <br />
                </div>

            </asp:Panel>
        </div>

        <div id="tabs-7">
            <asp:Panel ID="pnlReferencias" runat="server" Visible="false">

                <div class="manutencaoCorpoSecao">

                    <asp:DetailsView ID="dvReferencias" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsReferencias" DataKeyNames="idPessoa"
                        class="DetailsView" oniteminserting="dvPessoaGenerico_ItemInserting">

                        <Fields>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate>
                                    [ 1 - Referência ]
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
                                                Nome Completo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtReferenciaNomeCompleto1" runat="server" Text='<%# Bind("referenciaNomeCompleto1") %>' Width="500px"></asp:TextBox>
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
                                                <%# Eval("referenciaNomeCompleto1") %>
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
                                                Telefone
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtReferenciaTelefoneResidencial1" runat="server" Text='<%# Bind("referenciaTelefoneResidencial1") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtReferenciaTelefoneCelular1" runat="server" Text='<%# Bind("referenciaTelefoneCelular1") %>' Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Telefone
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("referenciaTelefoneResidencial1") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("referenciaTelefoneCelular1") %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <HeaderStyle Height="1px"  />
                                <ItemStyle Height="1px" />
                                <ItemTemplate></ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemStyle CssClass="manutencaoTituloSecao" />
                                <ItemTemplate>
                                    [ 2 - Referência ]
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
                                                Nome Completo
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtReferenciaNomeCompleto2" runat="server" Text='<%# Bind("referenciaNomeCompleto2") %>'  Width="500px"></asp:TextBox>
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
                                                <%# Eval("referenciaNomeCompleto2") %>
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
                                                Telefone
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtReferenciaTelefoneResidencial2" runat="server" Text='<%# Bind("referenciaTelefoneResidencial2") %>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtReferenciaTelefoneCelular2" runat="server" Text='<%# Bind("referenciaTelefoneCelular2") %>' Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td class="manutencaoFieldHeader">
                                                Telefone
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("referenciaTelefoneResidencial2") %>
                                            </td>
                                            <td style="width: 50px;">

                                            </td>
                                            <td class="manutencaoFieldHeader">
                                                Celular
                                            </td>
                                            <td style="width: 10px;">

                                            </td>
                                            <td style="width: 100px;">
                                                <%# Eval("referenciaTelefoneCelular2") %>
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>

                            </asp:TemplateField>
                        </Fields>
                    </asp:DetailsView>

                    <asp:ObjectDataSource ID="dsReferencias" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoPessoa"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllPessoa"
                        UpdateMethod="UpdateReferencias" InsertMethod="InsertReferencias" DeleteMethod="Delete" >
                
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>

                        <UpdateParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                        </UpdateParameters>

                    </asp:ObjectDataSource>

                    <br />
                </div>

            </asp:Panel>
        </div>

        <div id="tabs-8">
            <asp:Panel ID="pnlColaborador" runat="server" Visible="false">
                
                <div class="manutencaoCorpoSecao">

                    <asp:DetailsView ID="dvColaborador" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsColaborador" DataKeyNames="idPessoa"
                        class="DetailsView"
                        oniteminserting="dvPessoaGenerico_ItemInserting" >
                        <FieldHeaderStyle CssClass="manutencaoFieldHeader" Width="190px" />
                        <Fields>

                            <InfoVillage:BoundTextBoxField DataField="colaboradorCargo" HeaderText="Cargo" ControlStyle-Width="300px"
                                MaxLength="50" />

                            <asp:TemplateField HeaderText="Data de Admissão">

                                <ItemTemplate>
                                    <%#  ProJur.DataAccess.Utilitarios.AplicaFormatacaoData(Eval("colaboradorDataAdmissao")) %>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox ID="txtColaboradorDataAdmissao"  CssClass="campoData"  runat="server" Width="100px" Text='<%# Bind("colaboradorDataAdmissao") %>'></asp:TextBox>
                                    <asp:CustomValidator ID="reqColaboradorDataAdmissaoValida" runat="server" ControlToValidate="txtColaboradorDataAdmissao" CssClass="ErroValidacao" Text="*" ErrorMessage="Data de Admissão inválida" OnServerValidate="reqColaboradorDataAdmissaoValida_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                                </EditItemTemplate>

                            </asp:TemplateField>

                            <asp:CheckBoxField DataField="colaboradorPadraoPrazoProcessual" HeaderText="Colaborador Padrão para Prazos" />

                        </Fields>
                    </asp:DetailsView>

                    <asp:ObjectDataSource ID="dsColaborador" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoPessoa"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllPessoa"
                        UpdateMethod="UpdateColaborador" InsertMethod="InsertColaborador">
                
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>

                        <InsertParameters>
                            <asp:Parameter Name="colaboradorDataAdmissao" Type="DateTime" />
                        </InsertParameters>

                        <UpdateParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                            <asp:Parameter Name="colaboradorDataAdmissao" Type="DateTime" />
                        </UpdateParameters>

                    </asp:ObjectDataSource>

                    <br />
                </div>

            </asp:Panel>
        </div>

        <div id="tabs-9">
            <asp:Panel ID="pnlAdvogado" runat="server" Visible="false">

                <div class="manutencaoCorpoSecao">

                    <asp:DetailsView ID="dvAdvogado" runat="server" Width="100%" AutoGenerateRows="False"
                        CellPadding="10" GridLines="None" DataSourceID="dsAdvogado" DataKeyNames="idPessoa"
                        class="DetailsView" oniteminserting="dvPessoaGenerico_ItemInserting">
                        <FieldHeaderStyle CssClass="manutencaoFieldHeader" Width="190px" />
                        <Fields>

                            <InfoVillage:BoundTextBoxField DataField="advogadoNumeroOAB" HeaderText="Número OAB"
                                ControlStyle-Width="150px" MaxLength="30" />

                            <asp:CheckBoxField DataField="advogadoPadraoProcesso" HeaderText="Advogado Padrão para Processos" />

                            <asp:CheckBoxField DataField="advogadoPadraoPrazoProcessual" HeaderText="Advogado Padrão para Prazos" />


                        </Fields>
                    </asp:DetailsView>

                    <asp:ObjectDataSource ID="dsAdvogado" runat="server" DataObjectTypeName="ProJur.Business.Dto.dtoPessoa"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" TypeName="ProJur.Business.Bll.bllPessoa"
                        UpdateMethod="UpdateAdvogado" InsertMethod="InsertAdvogado" DeleteMethod="Delete" >
                
                        <SelectParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                        </SelectParameters>

                        <UpdateParameters>
                            <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Int32" />
                        </UpdateParameters>

                    </asp:ObjectDataSource>

                    <br />
                </div>

            </asp:Panel>
        </div>

        <div id="tabs-10">

            <asp:Panel ID="pnlVinculos" runat="server">

                <div class="manutencaoTituloSecao">
                    [ Processos ]
                </div>

                <div class="manutencaoCorpoSecao">

                    <asp:UpdatePanel ID="upVinculos" runat="server" UpdateMode="Conditional" >
                        <ContentTemplate>
                            <div id="vinculos" class="boxResultados">
                                <asp:GridView ID="grdVinculoProcesso" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsVinculoProcesso" GridLines="None" 
                                    AllowSorting="True" CssClass="GridView" DataKeyNames="idProcesso" >
                                    <EmptyDataTemplate>
                                        <center>
                                            Nenhum processo encontrado</center>
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:BoundField DataField="numeroProcesso" HeaderText="Número Processo" 
                                            SortExpression="tbProcesso.numeroProcesso" Visible="true" >
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Objeto da Ação" SortExpression="tbProcesso.objetoAcao" >
                                            <ItemTemplate>
                                                <%# Eval("objetoAcao") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">
                        
                                            <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("idProcesso", "/Paginas/Manutencao/Processo.aspx?ID={0} ") %>' title="Visualizar os dados" >
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
                                <asp:ObjectDataSource ID="dsVinculoProcesso" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllByPessoa"
                                    TypeName="Projur.Business.Bll.bllProcesso" 
                                    DataObjectTypeName="Projur.Business.Dto.dtoProcesso" DeleteMethod="Delete" >
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="idPessoa" QueryStringField="ID" Type="Object" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <br />
                </div>
            </asp:Panel>
        </div>

        <div id="tabs-11">

            <asp:Panel ID="pnlVinculoAgendaHibrida" runat="server">

                <div class="manutencaoTituloSecao">
                    [ Agenda ]
                </div>

                <div class="manutencaoCorpoSecao">

                    <asp:UpdatePanel ID="upVinculoAgendaHibrida" runat="server" UpdateMode="Conditional" >
                        <ContentTemplate>
                            <div id="vinculosAgendaHibrida" class="boxResultados">

                                <asp:GridView ID="grdAgendaHibrida" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="dsAgendaHibrida" AllowPaging="true"
                                    GridLines="None" AllowSorting="True"  CssClass="GridView"
                                    PageSize="30" ShowFooter="False" OnRowDataBound="grdAgendaHibrida_RowDataBound">

                                    <EmptyDataTemplate>
                                        <center>Nenhum compromisso ou prazo encontrado</center>
                                    </EmptyDataTemplate>
                                                            
                                    <Columns>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>  
                                                <asp:HiddenField ID="hdIdAgendaHibrida" runat="server" Value='<%# Eval("idAgendaHibrida") %>' />
                                                <asp:HiddenField ID="hdTipoAgendamento" runat="server" Value='<%# Eval("tipoAgendamento") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="idAgendaHibrida" HeaderText="Código" 
                                            SortExpression="idAgendaHibrida" HeaderStyle-Width="65px" Visible="false" >
                                            <HeaderStyle Width="65px"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="dataHoraInicio" HeaderText="Data" SortExpression="dataHoraInicio" DataFormatString="{0:dd/MM/yyyy}" >
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="dataHoraInicio" HeaderText="Hora" SortExpression="dataHoraInicio" DataFormatString="{0:HH:mm}" >
                                            <HeaderStyle Width="100px"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Descrição" SortExpression="Descricao">

                                            <ItemStyle HorizontalAlign="Justify" VerticalAlign="Middle" />

                                            <ItemTemplate>
                                                <%# RetornaDescricao(Eval("idAgendaHibrida"),  Eval("tipoAgendamento")) %>
                                            </ItemTemplate>
                    
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false">

                                            <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Middle" />

                                            <ItemTemplate>
                                                    <%#  this.RetornaDescricaoParticipantes(Eval("idAgendaHibrida"),  Eval("tipoAgendamento"), Eval("Responsaveis"))%>
                                            </ItemTemplate>
                    
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="tipoAgendamento" ShowHeader="false">
                                            <HeaderStyle Width="50px"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:TemplateField ShowHeader="false">
                        
                                            <ItemStyle Width="20px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                            <ItemTemplate>
                                                <a class="botaoAcao" href='<%# this.RetornaPaginaManutencao(Eval("tipoAgendamento"), Eval("idAgendaHibrida")) %>' title="Visualizar os dados" >
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

                                <asp:ObjectDataSource ID="dsAgendaHibrida" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                                    TypeName="ProJur.Business.Bll.bllAgendaHibrida" >                
                                </asp:ObjectDataSource>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <br />
                </div>
            </asp:Panel>
        </div>

    </div>

</asp:Content>