using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ap2_Tarea5_RegistroDetalle.Validaciones
{
    public class MontoValidacion : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                decimal cantidad = Convert.ToDecimal(value);

                if (cantidad >= 0m)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("El monto debe mayor o igual a 0");

            }
            return new ValidationResult("Debes poner un monto");
        }
    }
}
