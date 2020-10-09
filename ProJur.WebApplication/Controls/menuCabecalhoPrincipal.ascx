<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuCabecalhoPrincipal.ascx.cs" Inherits="ProJur.WebApplication.Controls.menuCabecalhoPrincipal" %>

<table id="barraCabecalhoPrincipal" cellpadding="5px">
    <tr>
        <td>
            <a href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Default.aspx' title="Home">
                <img alt="" src='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/home_projur.png' border="0" />
            </a>
        </td>
                       
        <td>
            <img alt="" src='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/separadorBarraCabecalho.png' border="0" />
        </td>

        <td style="white-space: nowrap">
            <a href="/Default.aspx" title="Home">
                RCA PROJUR - Gestão de Processos e Departamentos Jurídicos
            </a>
        </td>

        <td width="100%" align="right">
            <asp:Literal ID="litUsuarioLogin" runat="server" Text="Nome do usuário"></asp:Literal>
        </td>
                        
        <td>
            <img alt="" src='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/separadorBarraCabecalho.png' />
        </td>

        <td>
            <img alt="" src='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/configuracoes.png' style="height: 20px; width: 20px" />
        </td>

        <td>
            <asp:ImageButton ID="cmdSair" runat="server" ImageUrl="~/Images/sair.png"  ToolTip="Sair do sistema"
                onclick="cmdSair_Click" style="height: 20px; width: 20px" />
        </td>
    </tr>
</table>