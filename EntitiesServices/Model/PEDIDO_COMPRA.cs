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
    
    public partial class PEDIDO_COMPRA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PEDIDO_COMPRA()
        {
            this.ITEM_PEDIDO_COMPRA = new HashSet<ITEM_PEDIDO_COMPRA>();
            this.PEDIDO_COMPRA_ANEXO = new HashSet<PEDIDO_COMPRA_ANEXO>();
            this.PEDIDO_COMPRA_ACOMPANHAMENTO = new HashSet<PEDIDO_COMPRA_ACOMPANHAMENTO>();
        }
    
        public int PECO_CD_ID { get; set; }
        public int USUA_CD_ID { get; set; }
        public string PECO_NM_NOME { get; set; }
        public string PECO_NR_NUMERO { get; set; }
        public string PECO_NR_NOTA_FISCAL { get; set; }
        public string PECO_DS_DESCRICAO { get; set; }
        public int PECO_IN_STATUS { get; set; }
        public System.DateTime PECO_DT_DATA { get; set; }
        public System.DateTime PECO_DT_PREVISTA { get; set; }
        public Nullable<System.DateTime> PECO_DT_FINAL { get; set; }
        public Nullable<System.DateTime> PECO_DT_APROVACAO { get; set; }
        public Nullable<System.DateTime> PECO_DT_CANCELAMENTO { get; set; }
        public string PECO_DS_JUSTIFICATIVA { get; set; }
        public byte[] PECO_TX_OBSERVACOES { get; set; }
        public int PECO_IN_ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_PEDIDO_COMPRA> ITEM_PEDIDO_COMPRA { get; set; }
        public virtual USUARIO_SUGESTAO USUARIO_SUGESTAO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO_COMPRA_ANEXO> PEDIDO_COMPRA_ANEXO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PEDIDO_COMPRA_ACOMPANHAMENTO> PEDIDO_COMPRA_ACOMPANHAMENTO { get; set; }
    }
}