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
    
    public partial class PESQUISA_ITEM_OPCAO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PESQUISA_ITEM_OPCAO()
        {
            this.PESQUISA_RESPOSTA_ITEM = new HashSet<PESQUISA_RESPOSTA_ITEM>();
        }
    
        public int PEIO_CD_ID { get; set; }
        public int PEIT_CD_ID { get; set; }
        public string PEIO_NM_OPCAO { get; set; }
        public int PEIO_IN_ATIVO { get; set; }
    
        public virtual PESQUISA_ITEM PESQUISA_ITEM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PESQUISA_RESPOSTA_ITEM> PESQUISA_RESPOSTA_ITEM { get; set; }
    }
}
