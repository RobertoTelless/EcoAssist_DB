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
    
    public partial class ESTADO_FISICO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ESTADO_FISICO()
        {
            this.DESTINADOR_ENVIO_PRODUTO = new HashSet<DESTINADOR_ENVIO_PRODUTO>();
        }
    
        public int ESFI_CD_ID { get; set; }
        public string ESFI_NM_NOME { get; set; }
        public int ESFI_IN_ATIVO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESTINADOR_ENVIO_PRODUTO> DESTINADOR_ENVIO_PRODUTO { get; set; }
    }
}
