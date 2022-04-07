using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EntitiesServices.Model;
using System.Web;
using EntitiesServices.Attributes;

namespace SB_Dashboard_Presentation.ViewModels
{
    public class AnexosViewModel
    {
        [Key]
        public int Filtro { get; set; }
        public String Titulo { get; set; }
        public DateTime Data { get; set; }
        public Int32 Tipo { get; set; }
        public String Arquivo { get; set; }

    }
}