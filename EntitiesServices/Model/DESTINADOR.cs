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
    
    public partial class DESTINADOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DESTINADOR()
        {
            this.DESTINADOR_ANEXO = new HashSet<DESTINADOR_ANEXO>();
            this.DESTINADOR_ANOTACOES = new HashSet<DESTINADOR_ANOTACOES>();
            this.DESTINADOR_ENVIO = new HashSet<DESTINADOR_ENVIO>();
        }
    
        public int DEST_CD_ID { get; set; }
        public string DEST_NM_NOME { get; set; }
        public byte[] DEST_NR_CNPJ { get; set; }
        public string DEST_NM_ENDERECO { get; set; }
        public string DEST_NR_NUMERO { get; set; }
        public string DEST_NM_COMPLEMENTO { get; set; }
        public string DEST_NM_BAIRRO { get; set; }
        public string DEST_NM_CIDADE { get; set; }
        public string DEST_NR_CEP { get; set; }
        public int UF_CD_ID { get; set; }
        public string DEST_NM_CONTATO { get; set; }
        public string DEST_EM_EMAIL { get; set; }
        public string DEST_NR_TELEFONE { get; set; }
        public string DEST_NR_CELULAR { get; set; }
        public byte[] DEST_TX_OBSERVACAO { get; set; }
        public int DEST_IN_ATIVO { get; set; }
    
        public virtual UF UF { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESTINADOR_ANEXO> DESTINADOR_ANEXO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESTINADOR_ANOTACOES> DESTINADOR_ANOTACOES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DESTINADOR_ENVIO> DESTINADOR_ENVIO { get; set; }
    }
}