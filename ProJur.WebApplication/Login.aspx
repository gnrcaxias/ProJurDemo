<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProJur.WebApplication.Login" %>
<%@ Register src="~/Controls/menuCabecalhoPrincipal.ascx" tagname="menuCabecalhoPrincipal" tagprefix="ProJur" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    
    <title></title>

    <script src="Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="Scripts/jquery.mask.min.js" type="text/javascript"></script>
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/jquery-ui.min.js" type="text/javascript"></script>

    <link href="~/Styles/principal.main.css" rel="stylesheet" type="text/css" />

    <link href="~/Styles/default.main.css" rel="stylesheet" type="text/css" />    
    <script src="Scripts/default.main.js" type="text/javascript"></script>

    <link href="~/Styles/login.main.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/login.main.js" type="text/javascript"></script>

    <script  type="text/javascript">
        window.onload = function () {
            document.getElementById("txtUsuario").focus();
        };
    </script>

</head>
        
<body>
    <form id="form1" runat="server" >

    <div id="mask"  style="display: none"></div>

    <div id="paginaPrincipal">

        <div id="cabecalhoPrincipal">

            <table id="barraCabecalhoPrincipal" cellpadding="5px">
                <tr>
                    <td>
                        <img alt="" src="Images/home_projur.png" border="0" />
                    </td>
                        
                    <td>
                        <img alt="" src="Images/separadorBarraCabecalho.png" border="0" />
                    </td>

                    <td style="white-space: nowrap">
                        RCA PROJUR - Gestão de Processos e Departamentos Jurídicos
                    </td>

                    <td style="width:100%">
                    </td>

                </tr>
            </table>
            
        </div>

    </div>

    <div id="login" class="boxLogin" style="display: none">
            
        <div id="Header">
            <table cellpadding="5px">
                <tbody>
                    <tr>
                        <td>
                            <img src="Images/login.png" alt="">
                        </td>
                        <td>
                            <img src="Images/separadorBarraCabecalho.png" alt="">
                        </td>
                        <td>
                            LOGIN
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div>
            <asp:ValidationSummary ID="valSummaryNovaSenha" runat="server" CssClass="ValidationSummary" />
        </div>

        <div id="Body">
            <div id="descricao">
                Por favor, digite o usuário e senha abaixo:
            </div>
            <div>
            <table cellspacing="0" cellpadding="10" style="width: 100%; border-collapse: collapse;">
                <tbody>
                    <tr>
                        <td style="font-size: 11px; font-weight: bold; width: 100px;">
                            Usuário:
                            <asp:RequiredFieldValidator ID="reqUsuario" runat="server" ErrorMessage="Usuário está em branco"
                                CssClass="ErroValidacao" ControlToValidate="txtUsuario"
                                Text="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUsuario" runat="server" Style="width: 200px;" MaxLength="20"
                                    AutoComplete="Off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 11px; font-weight: bold; width: 100px;">
                            Senha:
                            <asp:RequiredFieldValidator ID="reqNovaSenha" runat="server" ErrorMessage="Senha está em branco"
                                CssClass="ErroValidacao" ValidationGroup="NovaSenha" ControlToValidate="txtSenha"
                                Text="*" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSenha" runat="server" Style="width: 200px;" MaxLength="20"
                                TextMode="Password" AutoComplete="Off"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>                    
            </div>
        </div>

        <div id="Footer">

            <div style="margin: 0 auto; width: 41px">
                <asp:Button ID="btnEntrar" runat="server" Text="Entrar" CssClass="botaoPadrao" 
                    onclick="btnEntrar_Click"   />
            </div>

        </div>

    </div>

    </form>

</body>
</html>
