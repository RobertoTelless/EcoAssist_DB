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
    
    public partial class DESTINADOR_ENVIO_ANOTACOES
    {
        public int DEAT_CD_ID { get; set; }
        public int DEEN_CD_ID { get; set; }
        public int USUA_CD_ID { get; set; }
        public System.DateTime DEAT_DT_ANOTACAO { get; set; }
        public int DEAT_DS_ANOTACAO { get; set; }
    
        public virtual DESTINADOR_ENVIO DESTINADOR_ENVIO { get; set; }
        public virtual USUARIO_SUGESTAO USUARIO_SUGESTAO { get; set; }
    }
}
