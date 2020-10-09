<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="ProJur.WebApplication.Paginas.Manutencao.Usuario" %>
<%@ Register src="~/Controls/menuAcoesManutencao.ascx" tagname="menuAcoesManutencao" tagprefix="ProJur" %>
<%@ Register src="~/Controls/dialogNovaSenha.ascx" tagname="dialogNovaSenha" tagprefix="ProJur" %>
<%@ Register Src="~/Controls/dialogSelecaoPessoa.ascx" TagPrefix="ProJur" TagName="dialogSelecaoPessoa" %>
<%@ MasterType virtualpath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/gridview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/gridview.js" type="text/javascript"></script>

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/detailsview.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/detailsview.js" type="text/javascript"></script>

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/manutencao.main.css" rel="stylesheet" type="text/css" />
    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/cadastro.main.css" rel="stylesheet" type="text/css" />

    <link href="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Styles/modal-window.css" rel="stylesheet" type="text/css" />
    <script src="<%= ProJur.DataAccess.Configuracao.getEnderecoVirtualSite() %>/Scripts/modal-window-jquery.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <ProJur:menuAcoesManutencao ID="menuAcoes" runat="server" 
            NovoUrl="/Paginas/Manutencao/Usuario.aspx" 
            PesquisarUrl="/Paginas/Cadastro/Usuario.aspx" />

    <div>
        <asp:ValidationSummary ID="valSummary" runat="server"  
            CssClass="ErroValidacaoSummary" HeaderText="<span class='ErroValidacaoSummaryCabecalho'>Erro ao tentar gravar</span><br /><br />" />
    </div>

    <div class="manutencaoTituloSecao">
        [ Dados de Identificação ]
    </div>

    <div class="manutencaoCorpoSecao">

        <asp:DetailsView ID="dvManutencao" runat="server" Height="50px" Width="100%" 
            AutoGenerateRows="False" CellPadding="10" CssClass="DetailsView"
            GridLines="None" DataSourceID="dsManutencao" DataKeyNames="idUsuario" OnItemInserting="dvManutencao_ItemInserting" OnItemUpdating="dvManutencao_ItemUpdating" >
        
            <FieldHeaderStyle Width="180px" Font-Bold="True" Font-Size="11px" />
        
            <Fields>
            
                <asp:BoundField DataField="idUsuario" HeaderText="Código" InsertVisible="false" ReadOnly="true" />

                <asp:BoundField DataField="dataCadastro" HeaderText="Data de Cadastro" InsertVisible="false" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />

                <asp:BoundField DataField="dataUltimaAlteracao" HeaderText="Data da Últ. Alteração" InsertVisible="false" ReadOnly="true" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />

                <InfoVillage:BoundTextBoxField DataField="nomeCompleto" HeaderText="Nome Completo" ControlStyle-Width="300px" Required="true" MaxLength="50" />

                <InfoVillage:BoundTextBoxField DataField="Usuario" HeaderText="Login Usuário" ControlStyle-Width="300px" Required="true" MaxLength="50" />

                <asp:TemplateField HeaderText="Pessoa Vinculada">
                    <EditItemTemplate>
                        <asp:UpdatePanel ID="upPessoaVinculada" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtIdPessoaVinculada" runat="server" Width="120px" Text=' <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaVinculada"))).idPessoa %>'></asp:TextBox>
                                            <asp:HiddenField ID="hdIdPessoaVinculada" runat="server" Value='<%# Eval("idPessoaVinculada")%>' />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnPesquisarPessoaVinculada" runat="server" OnClick="btnPesquisarPessoaVinculada_Click"
                                                class="botao" ImageUrl="~/Images/Pesquisar.png" ValidationGroup="PesquisaPessoaVinculada"  />
                                        </td>
                                        <td>
                                            <asp:Literal ID="litNomePessoaVinculada" runat="server"
                                                Text='<%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaVinculada"))).NomeCompletoRazaoSocial %>'></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <a href='Pessoa.aspx?ID=<%# Eval("idPessoaVinculada") %>' target="_blank" >
                        <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaVinculada"))).CPFCNPJ %> -
                            <%# ProJur.Business.Bll.bllPessoa.Get(Convert.ToInt32(Eval("idPessoaVinculada"))).NomeCompletoRazaoSocial %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Senha" >
                    <ItemTemplate>
                        <a id="modal" href="#modalDialog">
                            <u>Clique aqui para alterar a senha</u>
                        </a>
                    </ItemTemplate>

                    <InsertItemTemplate>
                        <asp:TextBox runat="server" ID="txtSenha" Width="200px" MaxLength="20" Text='<%# Bind("Senha") %>' TextMode="Password" >
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqPassword" runat="server" 
                            CssClass="ErroValidacao" ErrorMessage="Senha está em branco" Text="*"
                            ControlToValidate="txtSenha" ></asp:RequiredFieldValidator>
                    </InsertItemTemplate>

                    <EditItemTemplate>
                        <a id="modal" href="#modalDialog">
                            <u>Clique aqui para alterar a senha</u>
                        </a>
                    </EditItemTemplate>

                </asp:TemplateField>

                <InfoVillage:BoundTextBoxField DataField="Email" HeaderText="Email" ControlStyle-Width="400px" MaxLength="100" />

                <asp:CheckBoxField DataField="Administrador" HeaderText="Usuário Administrador" />

                <asp:CheckBoxField DataField="usuarioPadraoAgendamento" HeaderText="Usuário Padrão Agendamento" />

                <asp:CheckBoxField DataField="Bloqueado" HeaderText="Bloqueado" />

            </Fields>
        
        </asp:DetailsView>

        <asp:ObjectDataSource ID="dsManutencao" runat="server" 
            DataObjectTypeName="ProJur.Business.Dto.dtoUsuario" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
            TypeName="ProJur.Business.Bll.bllUsuario" UpdateMethod="Update" 
            InsertMethod="Insert" oninserted="dsManutencao_Inserted" DeleteMethod="Delete" >
        
            <SelectParameters>
                <asp:QueryStringParameter Name="idUsuario" QueryStringField="ID" Type="Int32" />
            </SelectParameters>

            <InsertParameters>
                <asp:SessionParameter SessionField="IDUSUARIO" Name="idUsuarioLogado" Type="Int32"  />
                <asp:SessionParameter SessionField="IPUSUARIO" Name="ipUsuarioLogado" Type="String" />
            </InsertParameters>

            <UpdateParameters>
                <asp:Parameter Name="Administrador" Type="Boolean" />
                <asp:Parameter Name="usuarioPadraoAgendamento" Type="Boolean" />
                <asp:Parameter Name="Bloqueado" Type="Boolean" />
                <asp:Parameter Name="dataCadastro" Type="DateTime" />
                <asp:Parameter Name="dataUltimaAlteracao" Type="DateTime" />
            </UpdateParameters>
    
        </asp:ObjectDataSource>

        <ProJur:dialogNovaSenha ID="dialogNovaSenha" runat="server" />

        <br />
    </div>

    <div class="manutencaoTituloSecao">
        [ Módulos e Permissões ]
    </div>

    <div class="manutencaoCorpoSecao">

        <asp:GridView ID="grdPermissoes" runat="server" AutoGenerateColumns="False" 
            DataSourceID="dsResultado" 
            DataKeyNames="idPermissao, idMenu, Exibir, Imprimir, Novo, Excluir, Alterar, Pesquisar, Especial, idUsuarioModerador"
            GridLines="None" AllowSorting="True"  CssClass="GridView" ShowFooter="False" >

            <EmptyDataTemplate>
                <center>Nenhum módulo disponível para aplicação de permissões</center>
            </EmptyDataTemplate>
                                             
            <Columns>

                <asp:TemplateField HeaderText="Menu">  
                    <HeaderStyle Width="150px" /> 
                    <ItemTemplate>
                        <%# ProJur.Business.Bll.bllMenu.Get(Convert.ToInt32(Eval("idMenu"))).Descricao %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Exibir">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkExibir" runat="server" 
                                Checked='<%# Eval("Exibir") %>'
                                Enabled='<%# (dvManutencao.CurrentMode == DetailsViewMode.Edit || dvManutencao.CurrentMode == DetailsViewMode.Insert) %>' />
                    </ItemTemplate>                
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Imprimir">                    
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkImprimir" runat="server" 
                                Checked='<%# Eval("Imprimir") %>'
                                Enabled='<%# (dvManutencao.CurrentMode == DetailsViewMode.Edit || dvManutencao.CurrentMode == DetailsViewMode.Insert) %>' />
                    </ItemTemplate>                
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Novo">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkNovo" runat="server" 
                                Checked='<%# Eval("Novo") %>'
                                Enabled='<%# (dvManutencao.CurrentMode == DetailsViewMode.Edit || dvManutencao.CurrentMode == DetailsViewMode.Insert) %>' />
                    </ItemTemplate>                
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Excluir">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkExcluir" runat="server" 
                                Checked='<%# Eval("Excluir") %>'
                                Enabled='<%# (dvManutencao.CurrentMode == DetailsViewMode.Edit || dvManutencao.CurrentMode == DetailsViewMode.Insert) %>' />
                    </ItemTemplate>                
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Alterar">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkAlterar" runat="server" 
                                Checked='<%# Eval("Alterar") %>'
                                Enabled='<%# (dvManutencao.CurrentMode == DetailsViewMode.Edit || dvManutencao.CurrentMode == DetailsViewMode.Insert) %>' />
                    </ItemTemplate>                
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Pesquisar">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkPesquisar" runat="server" 
                                Checked='<%# Eval("Pesquisar") %>' 
                                Enabled='<%# (dvManutencao.CurrentMode == DetailsViewMode.Edit || dvManutencao.CurrentMode == DetailsViewMode.Insert) %>' />
                    </ItemTemplate>                
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Especial">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkEspecial" runat="server" 
                                Checked='<%# Eval("Especial") %>' 
                                Enabled='<%# (dvManutencao.CurrentMode == DetailsViewMode.Edit || dvManutencao.CurrentMode == DetailsViewMode.Insert) %>' />
                    </ItemTemplate>                
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Moderador">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>

                        <asp:Literal ID="litUsuarioModerador" runat="server" 
                            Visible='<%# !(dvManutencao.CurrentMode == DetailsViewMode.Edit) %>'
                            Text='<%# ProJur.Business.Bll.bllUsuario.Get(Convert.ToInt32(Eval("idUsuarioModerador"))).Usuario %>' >
                        </asp:Literal>

                        <asp:DropDownList ID="ddlModerador" runat="server"
                            AppendDataBoundItems="true" SelectedValue='<%# Bind("idUsuarioModerador") %>'
                            Visible='<%# (dvManutencao.CurrentMode == DetailsViewMode.Edit || dvManutencao.CurrentMode == DetailsViewMode.Insert) %>'
                            DataSourceID="dsModerador" DataTextField="Usuario" DataValueField="idUsuario" >
                            <asp:ListItem Text="< Nenhum >" Value="0"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:ObjectDataSource ID="dsModerador" runat="server" 
                            DataObjectTypeName="ProJur.Business.Dto.dtoUsuario"
                            SelectMethod="GetAll" TypeName="ProJur.Business.Bll.bllUsuario">     
                        </asp:ObjectDataSource>

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

        <asp:ObjectDataSource ID="dsResultado" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllByUsuario" SortParameterName="SortExpression"
            TypeName="ProJur.Business.Bll.bllUsuarioPermissao" >
            <SelectParameters>
                <asp:QueryStringParameter Name="idUsuario" QueryStringField="ID" Type="Int32" />            
            </SelectParameters>
        </asp:ObjectDataSource>

    </div>

    <asp:Button ID="btnNULL_DialogSelecaoPessoa" Style="display: none" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="mpeDialogSelecaoPessoa" runat="server" 
            TargetControlID="btnNULL_DialogSelecaoPessoa" PopupControlID="boxDialogSelecaoPessoa"  
            CancelControlID="btnNULL_DialogSelecaoPessoa" BackgroundCssClass="modalBackground" >
        <Animations>
            <OnShown>
                <FadeIn Duration=".5" Fps="30" />
            </OnShown>
            <OnHiding>
                <FadeOut Duration=".5" Fps="30" />
            </OnHiding>
        </Animations>
    </ajaxToolkit:ModalPopupExtender>

    <div id="boxDialogSelecaoPessoa" class="boxes" style="width: 730px">
        <div class="janela">
            <asp:UpdatePanel runat="server" ID="upDialogSelecaoPessoa">
                <ContentTemplate>
                    <ProJur:dialogSelecaoPessoa ID="dialogSelecaoPessoa" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
