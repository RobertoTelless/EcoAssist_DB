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
    
    public partial class PRODUTO_PERGUNTAS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUTO_PERGUNTAS()
        {
            this.PRODUTO_PERGUNTAS_OPCAO = new HashSet<PRODUTO_PERGUNTAS_OPCAO>();
            this.PRODUTOS_RESPOSTA = new HashSet<PRODUTOS_RESPOSTA>();
        }
    
        public int PRPE_CD_ID { get; set; }
        public int TIPR_CD_ID { get; set; }
        public int TIIT_CD_ID { get; set; }
        public string PRPE_NM_PERGUNTA { get; set; }
        public int PRPE_IN_ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUTO_PERGUNTAS_OPCAO> PRODUTO_PERGUNTAS_OPCAO { get; set; }
        public virtual TIPO_PRODUTO TIPO_PRODUTO { get; set; }
        public virtual TIPO_ITEM_PESQUISA TIPO_ITEM_PESQUISA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUTOS_RESPOSTA> PRODUTOS_RESPOSTA { get; set; }
    }
}
