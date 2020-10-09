<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuAcoesCadastro.ascx.cs" Inherits="ProJur.WebApplication.Controls.menuAcoesCadastro" %>

<ul style="list-style-type: none; margin-bottom: 0px;">
    <li style="list-style-type: none; display: inline-block;" runat="server" id="liNovo" >
        <asp:HyperLink ID="lnkNovo" runat="server" CssClass="botaoAcaoCadastro">
            <%--<% Response.Write(this.NovoTexto); %>--%>
            <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Comandos-Small/<%= this.UrlImagemIncluir %>" border="0" title='<% Response.Write(this.NovoTexto); %>' />
        </asp:HyperLink>
    </li>
    <li style="list-style-type: none; display: inline-block;" runat="server" id="liExcluir">
        <asp:LinkButton ID="btnExcluirSelecionados" runat="server"  CssClass="botaoAcaoCadastro"
            onclick="btnExcluirSelecionados_Click" OnClientClick="javascript:return confirm('Deseja realmente excluir o(s) registro(s) selecionado(s)?');">
            <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Comandos-Small/remover-registro.png" border="0" title="Excluir Selecionados" />
            <%--Excluir Selecionados--%>
        </asp:LinkButton>
    </li>



    <li style="list-style-type: none; display: inline-block;" runat="server" id="liAcaoExtra1">
        <asp:LinkButton ID="btnAcaoExtra1" runat="server" onclick="btnAcaoExtra1_Click" CssClass="botaoAcaoCadastro">
            <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Comandos-Small/adicionar-prazo.png" border="0" title='<% Response.Write(this.AcaoExtra1Texto); %>' />
            
        </asp:LinkButton>
    </li>

    <li style="list-style-type: none; display: inline-block;" runat="server" id="liAcaoExtra2">
        <asp:LinkButton ID="btnAcaoExtra2" runat="server" onclick="btnAcaoExtra2_Click">
            <% Response.Write(this.AcaoExtra2Texto); %>
        </asp:LinkButton>
    </li>

    <li style="list-style-type: none; display: inline-block;" runat="server" id="liImprimir">
        <asp:LinkButton ID="btnImprimirListagem" runat="server" onclick="btnImprimirListagem_Click" CssClass="botaoAcaoCadastro" >
            <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Comandos-Small/imprimir.png" border="0" title="Imprimir" />

            <%--<% Response.Write(this.ImprimirTexto); %>--%>
        </asp:LinkButton>
    </li>
</ul>

<ul style="list-style-type: none; margin-top: 0px;" >
    <li style="list-style-type: none; display: inline-block;" runat="server" id="liAcaoExtra3">
        <asp:LinkButton ID="btnAcaoExtra3" runat="server" onclick="btnAcaoExtra3_Click" CssClass="botaoAcaoCadastro">
            <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Comandos-Small/adicionar-usuario.png" border="0" title='<% Response.Write(this.AcaoExtra3Texto); %>' />
        </asp:LinkButton>
    </li>
    <li style="list-style-type: none; display: inline-block;" runat="server" id="liAcaoExtra4">
        <asp:LinkButton ID="btnAcaoExtra4" runat="server" onclick="btnAcaoExtra4_Click" CssClass="botaoAcaoCadastro">
            <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Comandos-Small/remover-usuario.png" border="0" title='<% Response.Write(this.AcaoExtra4Texto); %>' />
        </asp:LinkButton>
    </li>
</ul>

