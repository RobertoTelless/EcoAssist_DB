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
    
    public partial class TICKET_ATENDIMENTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TICKET_ATENDIMENTO()
        {
            this.ATENDIMENTO_ACOMPANHAMENTO = new HashSet<ATENDIMENTO_ACOMPANHAMENTO>();
            this.ATENDIMENTO_ANEXO = new HashSet<ATENDIMENTO_ANEXO>();
        }
    
        public int ATEN_CD_ID { get; set; }
        public int USUA_CD_ID { get; set; }
        public int CAAT_CD_ID { get; set; }
        public Nullable<int> CLIE_CD_ID { get; set; }
        public Nullable<int> OSPR_CD_ID { get; set; }
        public Nullable<int> DEPT_CD_ID { get; set; }
        public Nullable<int> ORSE_CD_ID { get; set; }
        public int ATST_CD_ID { get; set; }
        public string ATEN_NR_NUMERO { get; set; }
        public string ATEN_NM_ASSUNTO { get; set; }
        public string ATEN_DS_DESCRICAO { get; set; }
        public int ATEN_IN_PRIORIDADE { get; set; }
        public System.DateTime ATEN_DT_INICIO { get; set; }
        public System.DateTime ATEN_DT_PREVISTA { get; set; }
        public Nullable<System.DateTime> ATEN_DT_ENCERRAMENTO { get; set; }
        public string ATEN_DS_ENCERRAMENTO { get; set; }
        public Nullable<System.DateTime> ATEN_DT_CANCELAMENTO { get; set; }
        public string ATEN_DS_CANCELAMENTO { get; set; }
        public int ATEN_IN_ATIVO { get; set; }
        public string ATEN_TX_OBSERVACAO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATENDIMENTO_ACOMPANHAMENTO> ATENDIMENTO_ACOMPANHAMENTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATENDIMENTO_ANEXO> ATENDIMENTO_ANEXO { get; set; }
        public virtual ATENDIMENTO_STATUS ATENDIMENTO_STATUS { get; set; }
        public virtual CATEGORIA_ATENDIMENTO CATEGORIA_ATENDIMENTO { get; set; }
        public virtual CLIENTE CLIENTE { get; set; }
        public virtual DEPARTAMENTO DEPARTAMENTO { get; set; }
        public virtual ORDEM_SERVICO ORDEM_SERVICO { get; set; }
        public virtual ORDEM_SERVICO_PRODUTO ORDEM_SERVICO_PRODUTO { get; set; }
        public virtual USUARIO_SUGESTAO USUARIO_SUGESTAO { get; set; }
    }
}
