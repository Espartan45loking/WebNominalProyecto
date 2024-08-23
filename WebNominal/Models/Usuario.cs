using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebNominal.Models
{
    public class Usuario
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string pass { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string genero { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Idusuario { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public int id { get; set; }
        public int Codempleado { get; set; }


    }
}