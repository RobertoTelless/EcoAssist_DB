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
    
    public partial class STATUS_ORDEM_SERVICO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STATUS_ORDEM_SERVICO()
        {
            this.ORDEM_SERVICO = new HashSet<ORDEM_SERVICO>();
            this.ORDEM_SERVICO_HISTORICO = new HashSet<ORDEM_SERVICO_HISTORICO>();
        }
    
        public int STOS_CD_ID { get; set; }
        public string STOS_NM_NOME { get; set; }
        public int STOS_IN_ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO> ORDEM_SERVICO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDEM_SERVICO_HISTORICO> ORDEM_SERVICO_HISTORICO { get; set; }
    }
}