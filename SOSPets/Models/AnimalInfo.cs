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
        public int SituacaoAnimalID { get; set; }

        public string Descricao { get; set; }
        public string Especie { get; set; }
        public int EstadoID { get; set; }
        public int CidadeID { get; set; }
        public string Bairro { get; set; }
    }
}