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
    
    public partial class UF
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UF()
        {
            this.DESTINADOR = new HashSet<DESTINADOR>();
            this.LEILOEIRO = new HashSet<LEILOEIRO>();
            this.PARCEIRO_ENDERECO = new HashSet<PARCEIRO_ENDERECO>();
            this.PRESTADOR_AJUDANTE = new HashSet<PRESTADOR_AJUDANTE>();
            this.PRESTADOR_ENDERECO = new HashSet<PRESTADOR_ENDERECO>();
            this.PRESTADOR_MOTORISTA = new HashSet<PRESTADOR_MOTORISTA>();
            this.REGIAO_COBERTURA = new HashSet<REGIAO_COBERTURA>();
        }
    
        public int UF_CD_ID { get; set; }
        public string UF_NM_NOME { get; set; }
        public string UF_SG_SIGLA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESTINADOR> DESTINADOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LEILOEIRO> LEILOEIRO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PARCEIRO_ENDERECO> PARCEIRO_ENDERECO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_AJUDANTE> PRESTADOR_AJUDANTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_ENDERECO> PRESTADOR_ENDERECO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRESTADOR_MOTORISTA> PRESTADOR_MOTORISTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REGIAO_COBERTURA> REGIAO_COBERTURA { get; set; }
    }
}