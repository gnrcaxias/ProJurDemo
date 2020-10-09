using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProJur.Business.Dto
{
    public class dtoPessoaSocio
    {

        public int idPessoaSocio { get; set; }

        public int idPessoa { get; set; }

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
        public string fisicaPISPASEP { get; set; }
        public string fisicaCTPSNumero { get; set; }
        public string fisicaCTPSSerie { get; set; }
        public Nullable<DateTime> fisicaCTPSDataExpedicao { get; set; }
        public string fisicaRegimeComunhaoBens { get; set; }

        // PESSOA ENDEREÇO
        public string enderecoLogradouro { get; set; }
        public string enderecoNumero { get; set; }
        public string enderecoComplemento { get; set; }
        public string enderecoBairro { get; set; }
        public string enderecoCEP { get; set; }
        public int enderecoIdCidade { get; set; }
        public int enderecoIdEstado { get; set; }

        // PESSOA CONTATO
        public string contatoTelefoneResidencial { get; set; }
        public string contatoTelefoneComercial { get; set; }
        public string contatoTelefoneCelular { get; set; }
        public string contatoEmail { get; set; }

    }
}
