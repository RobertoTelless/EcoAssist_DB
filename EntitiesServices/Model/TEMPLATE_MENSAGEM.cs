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
    
    public partial class TEMPLATE_MENSAGEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TEMPLATE_MENSAGEM()
        {
            this.CLIENTE_MARKETING_ATIVO = new HashSet<CLIENTE_MARKETING_ATIVO>();
        }
    
        public int TEMP_CD_ID { get; set; }
        public int TEMP_IN_TIPO { get; set; }
        public string TEM_NM_NOME { get; set; }
        public byte[] TEMP_TX_MENSAGEM { get; set; }
        public string TEMP_AQ_MENSAGEM { get; set; }
        public int TEMP_IN_ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLIENTE_MARKETING_ATIVO> CLIENTE_MARKETING_ATIVO { get; set; }
    }
}
