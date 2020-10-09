<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuEsquerdaPrincipal.ascx.cs" Inherits="ProJur.WebApplication.Controls.menuEsquerdaPrincipal" %>

<div id="logotipoBox">
    <table width="100%" height="100%">
        <tr>
            <td>
                <img src='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/rca-logotipo-projur.png' />
            </td>
        </tr>
    </table>
</div>
<br />

<asp:Repeater ID="rptMenuGrupos" runat="server" 
    onitemdatabound="rptMenuGrupos_ItemDataBound">

    <ItemTemplate>

        <div id="menuPrincipalTituloGrupo">
            <a href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("Url") %>' >
                <asp:Literal ID="litTituloGrupo" runat="server" Text='<%# Eval("Descricao")%>'></asp:Literal>
            </a>
        </div>

        <asp:Repeater ID="rptMenuItens" runat="server">
        
            <HeaderTemplate>
                <ul id="listaMenuPrincipalGrupo">
            </HeaderTemplate>
            
            <ItemTemplate>
                <li>
                    <div>
                        <div style="width:100%; line-height: 14px; height:14px;">
                            <a href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><%# Eval("Url") %>' >
<%--                                <div class="listaMenuItemContador">
                                    10
                                </div>--%>
                                <div><%# Eval("Descricao")%></div>
                            </a>
                        </div>
                    </div>
                </li>
            </ItemTemplate>

            <FooterTemplate>
                </ul>
            </FooterTemplate>

        </asp:Repeater>

    </ItemTemplate>

</asp:Repeater>