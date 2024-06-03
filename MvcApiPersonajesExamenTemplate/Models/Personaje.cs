using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcApiPersonajesExamenTemplate.Models
{
    public class Personaje
    {
        public int IdPersonaje {  get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
    }
}
