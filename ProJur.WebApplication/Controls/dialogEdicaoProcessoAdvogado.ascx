<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dialogEdicaoProcessoAdvogado.ascx.cs" Inherits="ProJur.WebApplication.Controls.dialogEdicaoProcessoAdvogado" %>


<div id="janelaModalCabecalho">
        <table cellpadding="5px">
            <tr>
                <td>
                    <img alt="" src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/menuPrincipal.png" />
                </td>
                <td>
                    <img alt="" src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/separadorBarraCabecalho.png" />
                </td>
                <td>
                    ADVOGADOS PARTES
                </td>
            </tr>
        </table>
    </div>

    <div>
        <asp:ValidationSummary ID="valSummaryDialogEdicaoProcessoAdvogado" runat="server" ValidationGroup="DialogEdicaoProcessoAdvogado"
            CssClass="ValidationSummary" />
    </div>

    <div id="janelaModalCorpo">
        <div id="descricao">
            Por favor, selecione um advogado:
        </div>
        <div>
            <table cellspacing="0" cellpadding="10" style="width: 100%; border-collapse: collapse;">
                <tbody>
                    <tr>
                        <td style="font-size: 11px; font-weight: bold; width: 130px;">
                            Advogado
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlPessoaAdvogado"
                                DataSourceID="dsPessoaAdvogado"
                                DataTextField="NomeCompletoRazaoSocial" DataValueField="idPessoa"
                                AppendDataBoundItems="True" Width="330px">
                                <asp:ListItem Value="0" Text="< Selecione >"  />
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="dsPessoaAdvogado" runat="server" 
                                DataObjectTypeName="ProJur.Business.Dto.dtoPessoa" 
                                SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllPessoa">
                                <SelectParameters>
                                    <asp:Parameter Name="tipoPessoaCliente" DefaultValue="" />
                                    <asp:Parameter Name="tipoPessoaParte" DefaultValue="" />
                                    <asp:Parameter Name="tipoPessoaAdvogado" DefaultValue="1" />
                                    <asp:Parameter Name="tipoPessoaColaborador" DefaultValue="" />
                                    <asp:Parameter Name="tipoPessoaTerceiro" DefaultValue="" />
                                    <asp:Parameter Name="termoPesquisa" DefaultValue="" />
                                    <asp:Parameter Name="SortExpression" DefaultValue="CASE WHEN especiePessoa = 'F' THEN fisicaNomeCompleto WHEN especiePessoa = 'J' THEN juridicaRazaoSocial ELSE '' END" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 11px; font-weight: bold; width: 130px;">
                            Parte do Advogado
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlParteAdvogado" Width="330px">
                                <asp:ListItem Value="" Text="< Selecione >"  Selected="True" />
                                <asp:ListItem Value="R" Text="R - Réu"  />
                                <asp:ListItem Value="A" Text="A - Autor"  />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

    <div id="janelaModalRodape">
        <table cellpadding="5px">
            <tr>
                <td>
                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upDialogEdicaoProcessoAdvogado" >
                        <ProgressTemplate>
                            <img src="/Images/ajax-loader.gif" alt="carregando..." />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>

                <td>
                    <asp:Button ID="btnConfirmarDialogEdicaoProcessoAdvogado" runat="server" Text="Confirmar" CssClass="botaoPadrao"
                        OnClick="btnConfirmarDialogEdicaoProcessoAdvogado_Click" ValidationGroup="DialogEdicaoProcessoAdvogado" />
                </td>
                <td>
                    <asp:Button ID="btnFecharDialogEdicaoProcessoAdvogado" runat="server" Text="Fechar" CssClass="botao" ValidationGroup="DialogEdicaoProcessoAdvogado" />
                </td>
            </tr>
        </table>
    </div>