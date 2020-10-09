<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dialogNovaSenha.ascx.cs" Inherits="ProJur.WebApplication.Controls.dialogNovaSenha" %>

<div id="boxes">
    <div class="janela" id="modalDialog" style="top: 218px; left: 492.5px; display: none;">
        <div id="janelaModalCabecalho">
            <table cellpadding="5px">
                <tr>
                    <td>
                        <img alt="" src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/login.png" />
                    </td>
                    <td>
                        <img alt="" src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/separadorBarraCabecalho.png" />
                    </td>
                    <td>
                        NOVA SENHA
                    </td>
                </tr>
            </table>
        </div>

        <div>
            <asp:ValidationSummary ID="valSummaryNovaSenha" runat="server" ValidationGroup="NovaSenha"
                CssClass="ValidationSummary" />
        </div>

        <div id="janelaModalCorpo">
            <div id="descricao">
                Por favor, digite a nova senha abaixo:
            </div>
            <div>
                <table cellspacing="0" cellpadding="10" style="width: 100%; border-collapse: collapse;">
                    <tbody>
                        <tr>
                            <td style="font-size: 11px; font-weight: bold; width: 100px;">
                                Nova senha:
                                <asp:RequiredFieldValidator ID="reqNovaSenha" runat="server" ErrorMessage="Nova senha está em branco"
                                    CssClass="ErroValidacao" ValidationGroup="NovaSenha" ControlToValidate="txtNovaSenha"
                                    Text="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNovaSenha" runat="server" Style="width: 200px;" MaxLength="20"
                                    ValidationGroup="NovaSenha" TextMode="Password" AutoComplete="Off"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 11px; font-weight: bold; width: 100px;">
                                Confirmação:
                                <asp:RequiredFieldValidator ID="reqConfirmacaoNovaSenha" runat="server" CssClass="ErroValidacao"
                                    ErrorMessage="Confirmação da nova senha está em branco" ValidationGroup="NovaSenha"
                                    ControlToValidate="txtConfirmacaoNovaSenha" Text="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvConfirmacaoSenha" runat="server" ErrorMessage="Senhas não conferem"
                                    ControlToValidate="txtNovaSenha" ControlToCompare="txtConfirmacaoNovaSenha" CssClass="ErroValidacao"
                                    ValidationGroup="NovaSenha" Text="*" Display="None" SetFocusOnError="true"></asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConfirmacaoNovaSenha" runat="server" Style="width: 200px;" MaxLength="20"
                                    ValidationGroup="NovaSenha" TextMode="Password" AutoComplete="Off">
                                </asp:TextBox>
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
                        <asp:Button ID="btnConfirmarNovaSenha" runat="server" Text="Confirmar" CssClass="botaoPadrao"
                            OnClick="btnConfirmarNovaSenha_Click" ValidationGroup="NovaSenha" />
                    </td>
                    <td>
                        <input type="button" class="botao" id="btnFechar" value="Fechar" />
                    </td>
                </tr>
            </table>
        </div>

    </div>
</div>
