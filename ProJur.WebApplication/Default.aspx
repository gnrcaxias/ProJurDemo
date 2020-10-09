<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="Default.aspx.cs" Inherits="ProJur.WebApplication.Default" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/cadastro.main.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/gridview.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div style="margin: 40px 20px 20px 20px ">

        <h1 style="color:#9c463f; font-weight: normal; text-transform:uppercase; font-size: 22px; ">
            <asp:Label ID="lblAgendaDoDia" runat="server" Text=""></asp:Label></h1>

        <table style="width: 100%">
            <tr>
                <td style="width: 33%; border-right: 1px dotted #bdbfc1; padding-right: 20px" valign="top">

                    <h2 style="color:#D17373; font-weight: normal; ">Compromissos</h2>

                    <asp:GridView ID="grdAgendaCompromissos" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="dsAgendaCompromissos" ShowHeader="false"
                        GridLines="None" AllowSorting="True"  CssClass="GridView"
                        PageSize="5" ShowFooter="False" OnRowDataBound="grdAgendaCompromissos_RowDataBound"  >

                        <EmptyDataTemplate>
                            <center>Nenhum compromisso encontrado para hoje</center>
                        </EmptyDataTemplate>
                                                            
                        <Columns>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>  
                                    <asp:HiddenField ID="hdIdAgendaHibrida" runat="server" Value='<%# Eval("idAgendaHibrida") %>' />
                                    <asp:HiddenField ID="hdTipoAgendamento" runat="server" Value='<%# Eval("tipoAgendamento") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Justify"  />
                                <ItemTemplate>
                                    <%# this.RetornaDescricaoCompromisso(Eval("idAgendaHibrida")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="False">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkConcluirAgendamento" runat="server" CommandName="Delete" ToolTip="Concluir agendamento"
                                                Text="Remover" ImageUrl="~/Images/calendar-check.png" OnClick="lnkConcluirAgendamento_Click"
                                                OnClientClick="return confirm('Deseja realmente concluir este compromisso?');" />
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

                    <asp:ObjectDataSource ID="dsAgendaCompromissos" runat="server" 
                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                        TypeName="ProJur.Business.Bll.bllAgendaHibrida">                
                    </asp:ObjectDataSource>
                </td>

                <td style="width: 33%; border-right: 1px dotted #bdbfc1; padding-right: 20px; padding-left: 20px" valign="top">

                    <h2 style="color:#D17373; font-weight: normal; ">Prazos</h2>

                    <asp:GridView ID="grdAgendaPrazos" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="dsAgendaPrazos" ShowHeader="false" OnRowDataBound="grdAgendaPrazos_RowDataBound"
                        GridLines="None" AllowSorting="True"  CssClass="GridView" AllowPaging="false"
                        PageSize="5" ShowFooter="False" >

                        <EmptyDataTemplate>
                            <center>Nenhum prazo encontrado para hoje</center>
                        </EmptyDataTemplate>
                                                            
                        <Columns>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>  
                                    <asp:HiddenField ID="hdIdAgendaHibrida" runat="server" Value='<%# Eval("idAgendaHibrida") %>' />
                                    <asp:HiddenField ID="hdTipoAgendamento" runat="server" Value='<%# Eval("tipoAgendamento") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Justify"  />
                                <ItemTemplate>
                                    <%# this.RetornaDescricaoPrazo(Eval("idAgendaHibrida")) %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="False">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="lnkConcluirPrazo" runat="server" CommandName="Delete" ToolTip="Concluir prazo"
                                                Text="Remover" ImageUrl="~/Images/calendar-check.png" OnClick="lnkConcluirPrazo_Click"
                                                OnClientClick="return confirm('Deseja realmente concluir este prazo?');" />
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

                    <asp:ObjectDataSource ID="dsAgendaPrazos" runat="server" 
                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" SortParameterName="SortExpression"
                        TypeName="ProJur.Business.Bll.bllAgendaHibrida">                
                    </asp:ObjectDataSource>
                </td>

                <td style="width: 33%; padding-left: 20px" valign="top">

                    <h2 style="color:#D17373; font-weight: normal; ">Aniversariantes</h2>

                    <asp:GridView ID="grdAniversariantes" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="dsAniversariantes" ShowHeader="false"
                        GridLines="None" AllowSorting="True"  CssClass="GridView"
                        PageSize="5" ShowFooter="False">

                        <EmptyDataTemplate>
                            <center>Nenhum aniversariante encontrado para hoje</center>
                        </EmptyDataTemplate>
                                                            
                        <Columns>

                            <asp:TemplateField>
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <%# this.RetornaDescricaoAniversariante(Eval("idPessoa")) %>
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

                    <asp:ObjectDataSource ID="dsAniversariantes" runat="server" 
                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAniversariantes" SortParameterName="SortExpression"
                        TypeName="ProJur.Business.Bll.bllPessoa">                
                    </asp:ObjectDataSource>
                </td>

            </tr>

        </table>

    </div>
    
    <div style="margin: 40px 20px 20px 20px; border-top: 1px dotted #D17373; ">
        
        <h1 style="color:#9c463f; font-weight: normal; text-transform:uppercase; font-size: 22px; ">Totalizadores</h1>

        <table style="width: 300px" class="GridView">
            <tr>
                <td>
                    <asp:Label ID="lblTotalProcessos" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTotalPessoas" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTotalAtendimentos" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
