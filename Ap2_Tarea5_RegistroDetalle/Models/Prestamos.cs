using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Ap2_Tarea5_RegistroDetalle.Validaciones;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ap2_Tarea5_RegistroDetalle.Models
{
    public class Prestamos
    {
        [Key]
        [IdValidacion]
        public int PrestamoId { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir una fecha")]
        [FechaValidacion]
        public DateTime Fecha { get; set; }

        public int PersonaId { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir un concepto")]
        public string Concepto { get; set; }

        [Required(ErrorMessage = "Es obligatorio introducir un monto")]
        [MontoValidacion]
        public decimal Monto { get; set; }

        public decimal Balance { get; set; }

        [ForeignKey("PrestamoId")]
        public virtual List<MorasDetalle> Detalle { get; set; }


        public Prestamos()
        {
            PrestamoId = 0;
            Fecha = DateTime.Now;
            PersonaId = 0;
            Concepto = string.Empty;
            Monto = 0.0m;
            Balance = 2600.0m;

            Detalle = new List<MorasDetalle>();
        }
    }
}
