using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EntitiesServices.Model;
using System.Web;
using EntitiesServices.Attributes;

namespace SB_Dashboard_Presentation.ViewModels
{
    public class ComentarioViewModel
    {
        [Key]
        public int Filtro { get; set; }
        public String Foto { get; set; }
        public DateTime Data { get; set; }
        public String Texto { get; set; }
        public String Nome { get; set; }

    }
}