using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EntitiesServices.Model;
using System.Web;
using EntitiesServices.Attributes;

namespace SB_Dashboard_Presentation.ViewModels
{
    public class ModeloViewModel
    {
        [Key]
        public Int32 Filtro { get; set; }
        public DateTime FiltroData { get; set; }
        public String FiltroTexto { get; set; }
        public String MesAno { get; set; }
        public Decimal Valor { get; set; }
        public String Observacao { get; set; }
        public DateTime? EmissaoInicio { get; set; }
        public DateTime? EmissaoFinal { get; set; }
        public DateTime? VencimentoInicio { get; set; }
        public DateTime? VencimentoFinal { get; set; }
        public DateTime? RecebimentoInicio { get; set; }
        public DateTime? RecebimentoFinal { get; set; }
        public DateTime? PagamentoInicio { get; set; }
        public DateTime? PagamentoFinal { get; set; }
        public String CentroCusto { get; set; }
        public String CentroLucro { get; set; }
        public String Beneficiario { get; set; }
        public String Sacado { get; set; }
        public String UF { get; set; }
        public String Cidade { get; set; }
        public String Especialidade { get; set; }
        public String Situacao { get; set; }
        public String Tipo { get; set; }
        public String LiberadoPag { get; set; }
        public Int32? ExecPositivo { get; set; }
        public Int32? ExecNegativo { get; set; }
        public Int32? Parcelamento { get; set; }
        public Int32? LancPagar { get; set; }
        public Int32? Criticidade { get; set; }
        public Int32? Probabilidade { get; set; }

        public bool Check
        {
            get
            {
                if (Filtro == 1)
                {
                    return true;
                }
                return false;
            }
            set
            {
                Filtro = (value == true) ? 1 : 0;
            }
        }

    }
}