<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dialogSelecaoUsuario.ascx.cs" Inherits="ProJur.WebApplication.Controls.dialogSelecaoUsuario1" %>

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
                    SELEÇÃO DE USUÁRIO
                </td>
            </tr>
        </table>
    </div>

    <div>
        <asp:ValidationSummary ID="valSummaryDialogSelecaoUsuario" runat="server" ValidationGroup="DialogSelecaoUsuario"
            CssClass="ValidationSummary" />
    </div>

    <div id="janelaModalCorpo">
        <div id="descricao">
            Por favor, selecione um usuário:
        </div>
        <div>
            <table cellspacing="0" cellpadding="10" style="width: 100%; border-collapse: collapse;">
                <tbody>
                    <tr>
                        <td style="font-size: 11px; font-weight: bold; width: 130px;">
                            Usuário
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlUsuario"
                                DataSourceID="dsUsuario"
                                DataTextField="nomeCompleto" DataValueField="idUsuario"
                                AppendDataBoundItems="True" Width="330px">
                                <asp:ListItem Value="0" Text="< Selecione >"  />
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="dsUsuario" runat="server" 
                                DataObjectTypeName="ProJur.Business.Dto.dtoUsuario" 
                                SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllUsuario">
                            </asp:ObjectDataSource>
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
                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upDialogSelecaoUsuario" >
                        <ProgressTemplate>
                            <img src="/Images/ajax-loader.gif" alt="carregando..." />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>

                <td>
                    <asp:Button ID="btnConfirmarDialogSelecaoUsuario" runat="server" Text="Confirmar" CssClass="botaoPadrao"
                        OnClick="btnConfirmarDialogSelecaoUsuario_Click" ValidationGroup="DialogSelecaoUsuario" />
                </td>
                <td>
                    <asp:Button ID="btnFecharDialogSelecaoUsuario" runat="server" Text="Fechar" CssClass="botao" ValidationGroup="DialogSelecaoUsuario" />
                </td>
            </tr>
        </table>
    </div>