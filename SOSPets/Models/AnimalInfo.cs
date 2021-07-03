using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOSPets.Models
{
    public class AnimalInfo
    {
        public int AnimalID { get; set; }
        public string Nome { get; set; }       
        public string Whatsapp { get; set; } 
        public string FotoUrl { get; set; }
        public DateTime DataDesaparecimento { get; set; }              
        public int AnimalCategoriaID { get; set; }
        public string Especie { get; set; }
    }
}