<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs"
     Inherits="ProJur.WebApplication.Paginas.Cadastro.Agenda" %>
<%@ Register src="~/Controls/menuAcoesCadastro.ascx" tagname="menuAcoesCadastro" tagprefix="uc1" %>
<%@ Register Src="~/Controls/dialogSelecaoUsuario.ascx" TagPrefix="ProJur" TagName="dialogSelecaoUsuario" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/cadastro.main.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/gridview.js" type="text/javascript"></script>

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/modal-window.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            $('span.checkall input').on('click', function () {
                $(this).closest('table').find(':checkbox').prop('checked', this.checked);
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div id="cadastroFiltros">
        <div id="cadastroFiltrosTitulo">Filtros:</div>
        <div id="cadastroFiltrosCampos">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPesquisa" runat="server" CssClass="campoTexto" placeholder="Digite aqui os dados para pesquisa, mínimo 3 letras"
                            Width="475px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnPesquisar" runat="server" class="botao" 
                            ImageUrl="~/Images/Pesquisar.png" onclick="btnPesquisar_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="cadastroFiltrosMaisOpcoesLink">
            <a href="" title="Mais opções de pesquisa">
                Mais opções de pesquisa
            </a>
        </div>
    </div>

    <div id="cadastroAcoes">
        <div id="cadastroAcoesTitulo">Ações:</div>
        <div id="cadastroAcoesListaItens">
            <uc1:menuAcoesCadastro ID="menuAcoesCadastro" runat="server" NovoUrl="/Paginas/Manutencao/AgendaCompromisso.aspx" NovoTexto="Incluir compromisso" AcaoExtra1Texto="Incluir Prazo" MostrarAcaoExtra1="true" MostrarExcluir="false" MostrarImprimir="true" ImprimirTexto="Imprimir" MostrarAcaoExtra3="true" MostrarAcaoExtra4="true" AcaoExtra3Texto="Incluir participante" AcaoExtra4Texto="Remover participante"  />
        </div>
    </div>

    <div style="clear:both"></div>

    <asp:UpdatePanel ID="upAgenda" runat="server">
        <ContentTemplate>

    <div id="cadastroOpcoesPesquisa">

        <table>
            <tr>
                <td valign="top" style="border-right: 1px dotted #bdbfc1; padding-right: 20px" >
    


                    <div>
                        <label class="titulo">Calendário:</label>
                        <div id="calendario" style="margin-top: 8px;">
                            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                                <NextPrevStyle VerticalAlign="Bottom" />
                                <OtherMonthDayStyle ForeColor="#808080" />
                                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                                <SelectorStyle BackColor="#CCCCCC" />
                                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <WeekendDayStyle BackColor="#FFFFCC" />
                            </asp:Calendar>
                        </div>
                    </div>

                    <div style="margin-top: 25px;">
                        <label class="titulo">Visualização:</label>
                        <div style="margin-top: 10px;">
                            <asp:RadioButtonList runat="server" ID="rblVisualizacaoAgenda" Width="150px"
                                 OnSelectedIndexChanged="rblVisualizacaoAgenda_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="" Text="Tudo"  />
                                <asp:ListItem Value="D" Text="Dia" Selected="True"/>
                                <asp:ListItem Value="S" Text="Semana" />
                                <asp:ListItem Value="M" Text="Mês" />
                            </asp:RadioButtonList>
                        </div>
                    </div>

<%--                    <div style="margin-top: 25px;">
                        <label class="titulo" ">Data de Início:</label>
                        <table style="margin-top: 5px;">
                            <tr>
                                <td>
                                    De 
                                    <asp:TextBox ID="txtDataInicioInicial" runat="server" Width="90px" CssClass="campoData" 
                                        AutoPostBack="True" OnTextChanged="txtDataInicioInicial_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    Até
                                        <asp:TextBox ID="txtDataInicioFinal" runat="server" Width="90px" CssClass="campoData" 
                                            OnTextChanged="txtDataInicioFinal_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style="margin-top: 25px;">
                        <label class="titulo">Data de Término:</label>
                        <table style="margin-top: 5px;">
                            <tr>
                                <td>
                                    De 
                                    <asp:TextBox ID="txtDataTerminoInicial" runat="server" Width="90px" CssClass="campoData" 
                                        OnTextChanged="txtDataTerminoInicial_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                                <td>
                                    Até
                                        <asp:TextBox ID="txtDataTerminoFinal" runat="server" Width="90px" CssClass="campoData" 
                                            OnTextChanged="txtDataTerminoFinal_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>--%>


                </td>
                <td valign="top" style="border-right: 1px dotted #bdbfc1; padding-right: 20px; padding-left: 10px;" >
                    <div runat="server" id="divFiltroUsuario">
                        <label class="titulo">Agendas de Usuários:</label>                        
                        <div style="margin-top: 10px;">
                            <asp:RadioButtonList runat="server" ID="rblUsuario"
                                DataSourceID="dsUsuario"
                                DataTextField="nomeCompleto"
                                DataValueField="idUsuario" 
                                AppendDataBoundItems="True" Width="250px" 
                                OnSelectedIndexChanged="rblUsuario_SelectedIndexChanged" 
                                AutoPostBack="true">
                                <asp:ListItem Text="< Todos >" Value="" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:ObjectDataSource ID="dsUsuario" runat="server" 
                                DataObjectTypeName="ProJur.Business.Dto.dtoUsuario"
                                SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllUsuario">    
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="nomeCompleto" Name="SortExpression" Type="String" />
                                </SelectParameters> 
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                </td>
                <td valign="top" style="padding-right: 20px; padding-left: 10px;" >
                    <div>
                        <label class="titulo">Mostrar o status:</label>
                        <div style="margin-top: 10px;">
                            <asp:CheckBoxList runat="server" ID="cblStatus" Width="150px" 
                                OnSelectedIndexChanged="cblStatus_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="P" Text="Pendente" Selected="True" />
                                <asp:ListItem Value="C" Text="Concluído" />
                            </asp:CheckBoxList>
                        </div>
                    </div>

                    <div style="margin-top: 25px;">
                        <label class="titulo">Mostrar os agendamento dos:</label>
                        <div style="margin-top: 10px;">
                            <asp:CheckBoxList runat="server" ID="cblTipoAgendamento" Width="150px" 
                                OnSelectedIndexChanged="cblTipoAgendamento_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="Prazo" Text="Prazos" Selected="True" />
                                <asp:ListItem Value="Agenda" Text="Compromissos" Selected="True" />
                            </asp:CheckBoxList>
                        </div>
                    </div>

<%--                    <div style="margin-top: 25px;">
                        <label class="titulo">Vizualização da:</label>
                        <div style="margin-top: 10px;">
                            <asp:RadioButtonList runat="server" ID="rblVisualizacaoAgenda" Width="150px"
                                 OnSelectedIndexChanged="rblVisualizacaoAgenda_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="D" Text="Dia" />
                                <asp:ListItem Value="S" Text="Semana" Selected="True" />
                                <asp:ListItem Value="M" Text="Mês" />
                            </asp:RadioButtonList>
                        </div>
                    </div>--%>

                </td>
            </tr>

        </table>

    </div>


<%--            </ContentTemplate>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div id="cadastroCorpo">
        <div id="cadastroCorpoTitulo">
            <b>Resultado:</b>
            <br />
            <label id="lblResultado" runat="server">Semana 01/05/2015 - 07/05/2015</label>
        </div>

        <div id="cadastroCorpoResultado">

            <asp:GridView ID="grdResultado" runat="server" AutoGenerateColumns="False" 
                DataSourceID="dsResultado" AllowPaging="true"
                GridLines="None" AllowSorting="True"  CssClass="GridView"
                PageSize="30" ShowFooter="False" OnRowDataBound="grdResultado_RowDataBound" OnSorted="grdResultado_Sorted" >

                <EmptyDataTemplate>
                    <center>Nenhum compromisso ou prazo encontrado</center>
                </EmptyDataTemplate>
                                                            
                <Columns>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">

                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAcao" runat="server" CssClass="checkall">
                            </asp:CheckBox>                        
                        </HeaderTemplate>

                        <ItemTemplate>  
                            <asp:CheckBox ID="chkAcao" runat="server">
                            </asp:CheckBox>
                            <asp:HiddenField ID="hdIdAgendaHibrida" runat="server" Value='<%# Eval("idAgendaHibrida") %>' />
                            <asp:HiddenField ID="hdTipoAgendamento" runat="server" Value='<%# Eval("tipoAgendamento") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="idAgendaHibrida" HeaderText="Código" 
                        SortExpression="idAgendaHibrida" HeaderStyle-Width="65px" Visible="false" >
                        <HeaderStyle Width="65px"></HeaderStyle>
                    </asp:BoundField>

                    <asp:HyperLinkField NavigateUrl="http://www.terra.com.br"
                          DataTextField="dataHoraInicio">
                        <HeaderStyle Width="100px"></HeaderStyle>
                    </asp:HyperLinkField>

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

<%--                    <asp:BoundField DataField="Responsaveis" HeaderText="Responsáveis" 
                        SortExpression="Responsaveis" >
                    </asp:BoundField>--%>

                    <asp:BoundField DataField="tipoAgendamento" ShowHeader="false">
                        <HeaderStyle Width="50px"></HeaderStyle>
                    </asp:BoundField>

<%--                    <asp:TemplateField HeaderText="Status" >
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemTemplate>
                            <%#  ProJur.Business.Bll.bllAgendaCompromisso.RetornaDescricaoSituacaoCompromisso(Eval("Status")) %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    
                    

                    <asp:TemplateField ShowHeader="False">
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="lnkConcluirAgendamento" runat="server" CausesValidation="False" CommandName="Delete" ToolTip="Concluir agendamento"
                                        Text="Remover" ImageUrl="~/Images/calendar-check.png" OnClick="lnkConcluirAgendamento_Click"
                                        OnClientClick="return confirm('Deseja realmente concluir este compromisso / prazo?');" />
                        </ItemTemplate>
                    </asp:TemplateField>

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

            <div id="rodape">
                <asp:Literal ID="litTotalRegistros" runat="server"></asp:Literal>
            </div>

            <asp:ObjectDataSource ID="dsResultado" runat="server" 
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                TypeName="ProJur.Business.Bll.bllAgendaHibrida" 
                onselected="dsResultado_Selected">                
            </asp:ObjectDataSource>
        </div>

    </div>

            </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID="btnNULL_DialogSelecaoUsuario" Style="display: none" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeDialogSelecaoUsuario" runat="server" 
            TargetControlID="btnNULL_DialogSelecaoUsuario" PopupControlID="boxDialogSelecaoUsuario"  
            CancelControlID="btnNULL_DialogSelecaoUsuario" BackgroundCssClass="modalBackground" >
        <Animations>
            <OnShown>
                <FadeIn Duration=".5" Fps="30" />
            </OnShown>
            <OnHiding>
                <FadeOut Duration=".5" Fps="30" />
            </OnHiding>
        </Animations>
    </ajaxToolkit:ModalPopupExtender>

    <div id="boxDialogSelecaoUsuario" class="boxes" style="width: 530px">
        <div class="janela">
            <asp:UpdatePanel runat="server" ID="upDialogSelecaoUsuario">
                <ContentTemplate>
                    <ProJur:dialogSelecaoUsuario ID="dialogSelecaoUsuario" runat="server"  
                         />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="dialogSelecaoUsuario"/>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:Button ID="btnNULL_DialogSelecaoUsuarioRemover" Style="display: none" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeDialogSelecaoUsuarioRemover" runat="server" 
            TargetControlID="btnNULL_DialogSelecaoUsuarioRemover" PopupControlID="boxDialogSelecaoUsuarioRemover"  
            CancelControlID="btnNULL_DialogSelecaoUsuarioRemover" BackgroundCssClass="modalBackground" >
        <Animations>
            <OnShown>
                <FadeIn Duration=".5" Fps="30" />
            </OnShown>
            <OnHiding>
                <FadeOut Duration=".5" Fps="30" />
            </OnHiding>
        </Animations>
    </ajaxToolkit:ModalPopupExtender>

    <div id="boxDialogSelecaoUsuarioRemover" class="boxes" style="width: 530px">
        <div class="janela">
            <asp:UpdatePanel runat="server" ID="upDialogSelecaoUsuarioRemover">
                <ContentTemplate>
                    <ProJur:dialogSelecaoUsuario ID="dialogSelecaoUsuarioRemover" runat="server"  
                         />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="dialogSelecaoUsuarioRemover"/>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
