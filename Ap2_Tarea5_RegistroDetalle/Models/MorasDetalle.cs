using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Ap2_Tarea5_RegistroDetalle.Validaciones;

namespace Ap2_Tarea5_RegistroDetalle.Models
{
    public class MorasDetalle
    {
        [Key]
        public int Id { get; set; }
        public int MoraId { get; set; }
        public int PrestamoId { get; set; }
        public decimal Valor { get; set; }

        public MorasDetalle()
        {
            Id = 0;
            MoraId = 0;
            PrestamoId = 0;
            Valor = 0.0m;
        }

        public MorasDetalle(MorasDetalle morasDetalle)
        {
            Id = morasDetalle.Id;
            MoraId = morasDetalle.MoraId;
            PrestamoId = morasDetalle.PrestamoId;
            Valor = morasDetalle.Valor;
        }
    }
}
