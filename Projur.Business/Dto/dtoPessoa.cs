using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{

    public class dtoPessoa
    {

        // PESSOA
        public int idPessoa { get; set; }
        public Nullable<DateTime> dataCadastro { get; set; }
        public int idUsuarioCadastro { get; set; }
        public Nullable<DateTime> dataUltimaAlteracao { get; set; }
        public int idUsuarioUltimaAlteracao { get; set; }
        public bool tipoPessoaCliente { get; set; }
        public bool tipoPessoaParte { get; set; }
        public bool tipoPessoaAdvogado { get; set; }
        public bool tipoPessoaColaborador { get; set; }
        public bool tipoPessoaTerceiro { get; set; }
        public string especiePessoa { get; set; }

        // PESSOA ADVOGADO
        public string advogadoNumeroOAB { get; set; }
        public bool advogadoPadraoProcesso { get; set; }
        public bool advogadoPadraoPrazoProcessual { get; set; }

        // PESSOA COLABORADOR
        public string colaboradorCargo { get; set; }
        public Nullable<DateTime> colaboradorDataAdmissao { get; set; }
        public bool colaboradorPadraoPrazoProcessual { get; set; }

        // PESSOA CONTATO
        public string contatoTelefoneResidencial { get; set; }
        public string contatoTelefoneComercial { get; set; }
        public string contatoTelefoneCelular { get; set; }
        public string contatoTelefoneCelular1 { get; set; }
        public string contatoTelefoneCelular2 { get; set; }
        public string contatoEmail { get; set; }
        public string contatoObservacao { get; set; }

        // PESSOA DADOS PROFISSIONAIS
        public string dadosProfissionaisNomeEmpresa { get; set; }
        public string dadosProfissionaisCargo { get; set; }
        public Nullable<DateTime> dadosProfissionaisDataAdmissao { get; set; }
        public string dadosProfissionaisEnderecoLogradouro { get; set; }
        public string dadosProfissionaisEnderecoNumero { get; set; }
        public string dadosProfissionaisEnderecoComplemento { get; set; }
        public string dadosProfissionaisEnderecoBairro { get; set; }
        public string dadosProfissionaisEnderecoCEP { get; set; }
        public int dadosProfissionaisEnderecoIdCidade { get; set; }
        public int dadosProfissionaisEnderecoIdEstado { get; set; }
        public string dadosProfissionaisContatoNomeCompleto { get; set; }
        public string dadosProfissionaisContatoTelefone { get; set; }

        // PESSOA ENDEREÇO
        public string enderecoLogradouro { get; set; }
        public string enderecoNumero { get; set; }
        public string enderecoComplemento { get; set; }
        public string enderecoBairro { get; set; }
        public string enderecoCEP { get; set; }
        public int enderecoIdCidade { get; set; }
        public int enderecoIdEstado { get; set; }

        // PESSOA FÍSICA
        public string fisicaNomeCompleto { get; set; }
        public string fisicaCPF { get; set; }
        public Nullable<DateTime> fisicaDataNascimento { get; set; }
        public string fisicaSexo { get; set; }
        public string fisicaEstadoCivil { get; set; }
        public string fisicaProfissao { get; set; }
        public string fisicaNaturalidade { get; set; }
        public string fisicaRGNumero { get; set; }
        public Nullable<DateTime> fisicaRGDataExpedicao { get; set; }
        public string fisicaRGOrgaoExpedidor { get; set; }
        public string fisicaRGUFExpedidor { get; set; }
        public string fisicaCNHNumero { get; set; }
        public string fisicaCNHCategoria { get; set; }
        public Nullable<DateTime> fisicaCNHDataHabilitacao { get; set; }
        public Nullable<DateTime> fisicaCNHDataEmissao { get; set; }
        public string fisicaFiliacaoNomePai { get; set; }
        public string fisicaFiliacaoNomeMae { get; set; }
        public string fisicaConjugueNomeCompleto { get; set; }
        public string fisicaConjugueCPF { get; set; }
        public Nullable<DateTime> fisicaConjugueDataNascimento { get; set; }
        public string fisicaNacionalidade { get; set; }
        public string fisicaEscolaridade { get; set; }
        public string fisicaPISPASEP   { get; set; }
        public string fisicaCTPSNumero { get; set; }
        public string fisicaCTPSSerie { get; set; }
        public Nullable<DateTime> fisicaCTPSDataExpedicao { get; set; }
        public string fisicaRegimeComunhaoBens { get; set; }
        public string fisicaCadSenha { get; set; }


        // PESSOA JURÍDICA
        public string juridicaRazaoSocial { get; set; }
        public string juridicaNomeFantasia { get; set; }
        public string juridicaCNPJ { get; set; }
        public string juridicaInscricaoMunicipal { get; set; }
        public string juridicaInscricaoEstadual { get; set; }
        public string juridicaRamoAtividade { get; set; }
        public Nullable<DateTime> juridicaDataFundacao { get; set; }
        public string juridicaNomeCompletoSocio1 { get; set; }
        public string juridicaEmailSocio1 { get; set; }
        public string juridicaTelefoneResidencialSocio1 { get; set; }
        public string juridicaTelefoneCelularSocio1 { get; set; }
        public string juridicaNomeCompletoSocio2 { get; set; }
        public string juridicaEmailSocio2 { get; set; }
        public string juridicaTelefoneResidencialSocio2 { get; set; }
        public string juridicaTelefoneCelularSocio2 { get; set; }

        // PESSOA REFERENCIA
        public string referenciaNomeCompleto1 { get; set; }
        public string referenciaTelefoneResidencial1 { get; set; }
        public string referenciaTelefoneCelular1 { get; set; }
        public string referenciaNomeCompleto2 { get; set; }
        public string referenciaTelefoneResidencial2 { get; set; }
        public string referenciaTelefoneCelular2 { get; set; }



        public string CPFCNPJ
        {
            get
            {
                string retorno = String.Empty;

                switch (this.especiePessoa)
                {
                    case "F":
                        retorno = this.fisicaCPF;
                        break;

                    case "J":
                        retorno = this.juridicaCNPJ;
                        break;
                }

                return retorno;
            }
        }

        public string TipoPessoaDescricao
        {
            get
            {
                StringBuilder sbTipoPessoa = new StringBuilder();

                if (tipoPessoaAdvogado)
                    sbTipoPessoa.Append("Advogado");

                if (tipoPessoaCliente)
                {
                    if (sbTipoPessoa.ToString() != String.Empty)
                        sbTipoPessoa.Append(", ");

                    sbTipoPessoa.Append("Cliente");
                }

                if (tipoPessoaColaborador)
                {
                    if (sbTipoPessoa.ToString() != String.Empty)
                        sbTipoPessoa.Append(", ");

                    sbTipoPessoa.Append("Colaborador");
                }

                if (tipoPessoaParte)
                {
                    if (sbTipoPessoa.ToString() != String.Empty)
                        sbTipoPessoa.Append(", ");

                    sbTipoPessoa.Append("Parte");
                }

                if (tipoPessoaTerceiro)
                {
                    if (sbTipoPessoa.ToString() != String.Empty)
                        sbTipoPessoa.Append(", ");

                    sbTipoPessoa.Append("Terceiro");
                }

                return sbTipoPessoa.ToString();
            }
        }

        public string NomeCompletoRazaoSocial
        {
            get
            {
                string retorno = String.Empty;

                switch (this.especiePessoa)
                {
                    case "F":
                        retorno = this.fisicaNomeCompleto;
                        break;

                    case "J":
                        retorno = this.juridicaRazaoSocial;
                        break;
                }

                return retorno;
            }
        }



        public object GetValue(string Campo)
        {
            object retorno = null;

            switch (Campo.Trim().ToUpper())
            {
                case "IDPESSOA":
                    retorno = this.idPessoa;
                    break;

                case "DATACADASTRO":
                    retorno = this.dataCadastro;
                    break;

                case "DATAULTIMAALTERACAO":
                    retorno = this.dataUltimaAlteracao;
                    break;

                case "TIPOPESSOACLIENTE":
                    retorno = this.tipoPessoaCliente;
                    break;

                case "TIPOPESSOACOLABORADOR":
                    retorno = this.tipoPessoaColaborador;
                    break;

                case "TIPOPESSOAPARTE":
                    retorno = this.tipoPessoaParte;
                    break;

                case "TIPOPESSOAADVOGADO":
                    retorno = this.tipoPessoaAdvogado;
                    break;

                case "TIPOPESSOATERCEIRO":
                    retorno = this.tipoPessoaTerceiro;
                    break;
            }

            return retorno;
        }

    }

}
