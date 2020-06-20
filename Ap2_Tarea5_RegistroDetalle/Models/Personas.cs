using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Ap2_Tarea5_RegistroDetalle.Validaciones;

namespace Ap2_Tarea5_RegistroDetalle.Models
{
    public class Personas
    {
        [Key]
        [IdValidacion]
        public int PersonaId { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir un nombre")]
        [NombresValidacion]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir un teléfono")]
        [TelefonoValidacion]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir una cédula")]
        [CedulaValidacion]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir una dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir una fecha")]
        [FechaValidacion]
        public DateTime FechaNacimiento { get; set; }

        public decimal Balance { get; set; }

        public Personas()
        {
            PersonaId = 0;
            Nombres = string.Empty;
            Telefono = string.Empty;
            Cedula = string.Empty;
            Direccion = string.Empty;
            FechaNacimiento = DateTime.Now;
            Balance = 0.0m;
        }
    }
}
