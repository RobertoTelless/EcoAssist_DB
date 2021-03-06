//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitiesServices.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CONTA_RECEBER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONTA_RECEBER()
        {
            this.CONTA_RECEBER_ANEXO = new HashSet<CONTA_RECEBER_ANEXO>();
            this.CONTA_RECEBER_PARCELA = new HashSet<CONTA_RECEBER_PARCELA>();
            this.CONTA_RECEBER_RATEIO = new HashSet<CONTA_RECEBER_RATEIO>();
        }
    
        public int CARE_CD_ID { get; set; }
        public int USUA_CD_ID { get; set; }
        public int PARC_CD_ID { get; set; }
        public int FOPA_CD_ID { get; set; }
        public int PERI_CD_ID { get; set; }
        public int CECU_CD_ID { get; set; }
        public string CARE_DS_DESCRICAO { get; set; }
        public System.DateTime CARE_DT_LANCAMENTO { get; set; }
        public Nullable<System.DateTime> CARE_DT_LIQUIDACAO { get; set; }
        public System.DateTime CARE_DT_VENCIMENTO { get; set; }
        public Nullable<System.DateTime> CARE_DT_INICIO_PARCELA { get; set; }
        public System.DateTime CARE_DT_COMPETENCIA { get; set; }
        public string CARE_NR_DOCUMENTO { get; set; }
        public int CARE_NR_ATRASO { get; set; }
        public decimal CARE_VL_VALOR { get; set; }
        public Nullable<decimal> CARE_VL_VALOR_LIQUIDADO { get; set; }
        public Nullable<decimal> CARE_VL_PARCELADO { get; set; }
        public Nullable<decimal> CARE_VL_DESCONTO { get; set; }
        public Nullable<decimal> CARE_VL_JUROS { get; set; }
        public Nullable<decimal> CARE_VL_TAXAS { get; set; }
        public Nullable<decimal> CARE_VL_SALDO { get; set; }
        public Nullable<decimal> CARE_VL_PARCIAL { get; set; }
        public Nullable<decimal> CARE_VL_RECEBIDO { get; set; }
        public int CARE_IN_PARCELADA { get; set; }
        public int CARE_IN_LIQUIDADA { get; set; }
        public int CARE_IN_ATIVO { get; set; }
        public int CARE_IN_PARCIAL { get; set; }
        public byte[] CARE_TX_OBSERVACAO { get; set; }
    
        public virtual CENTRO_CUSTO CENTRO_CUSTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_RECEBER_ANEXO> CONTA_RECEBER_ANEXO { get; set; }
        public virtual FORM_PAGTO_RECTO FORM_PAGTO_RECTO { get; set; }
        public virtual USUARIO_SUGESTAO USUARIO_SUGESTAO { get; set; }
        public virtual PARCEIRO PARCEIRO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_RECEBER_PARCELA> CONTA_RECEBER_PARCELA { get; set; }
        public virtual PERIODICIDADE PERIODICIDADE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTA_RECEBER_RATEIO> CONTA_RECEBER_RATEIO { get; set; }
    }
}
