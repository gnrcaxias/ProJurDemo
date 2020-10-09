<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menuAcoesManutencao.ascx.cs" Inherits="ProJur.WebApplication.Controls.menuAcoesManutencao" %>

<div id="manutencaoAcoes">
    
    <div id="manutencaoAcoesTituloModo">
        <asp:Literal ID="litModo" runat="server" Text="[Modo de visualização]">
        </asp:Literal>
    </div>
    <div>
        <ul>

            <li runat="server" id="liNovo">
                <a href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><% Response.Write(this.NovoUrl); %>' title="Novo" >
                    <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Comandos/Novo.png" width="36px" height="36px" border="0" />
                </a>
            </li>

            <li runat="server" id="liEditar">
                <asp:ImageButton ID="btnEditar" ImageUrl="~/Images/Comandos/Editar.png" ToolTip="Editar"
                    runat="server" Width="36px" Height="36px" onclick="btnEditar_Click" /></li>

            <li runat="server" id="liGravar">
                <asp:ImageButton ID="btnGravar" ImageUrl="~/Images/Comandos/Salvar.png" ToolTip="Gravar"
                    runat="server" Width="36px" Height="36px" onclick="btnGravar_Click" /></li>
            
            <li runat="server" id="liCancelar">
                <asp:LinkButton ID="btnCancelar" runat="server" onclick="btnCancelar_Click" CausesValidation="false" ToolTip="Cancelar" >
                    <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Comandos/Cancelar.png" width="36px" height="36px" border="0"  />
                </asp:LinkButton>
            </li>

            <li runat="server" id="liExcluir">
                <asp:ImageButton ID="btnExcluir" ImageUrl="~/Images/Comandos/Excluir.png" OnClientClick="javascript: return confirm('Deseja realmente excluir este registro?');"
                    runat="server" Width="36px" Height="36px" onclick="btnExcluir_Click" ToolTip="Excluir" /></li>

            <li runat="server" id="liPesquisar">
                <a href='<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %><% Response.Write(this.PesquisarUrl); %>' title="Pesquisar" >
                    <img src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Images/Comandos/Pesquisar.png" width="36px" height="36px" border="0" />
                </a>
            </li>

            <li runat="server" id="liImprimir">
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Images/Comandos/Imprimir.png" ToolTip="Imprimir"
                    runat="server" Width="36px" Height="36px" onclick="btnImprimir_Click" />
            </li>
        </ul>    
    </div>

    <div style="clear:both">
    </div>

</div>