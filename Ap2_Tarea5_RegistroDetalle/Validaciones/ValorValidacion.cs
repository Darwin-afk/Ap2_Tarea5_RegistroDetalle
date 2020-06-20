using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ap2_Tarea5_RegistroDetalle.Validaciones
{
    public class ValorValidacion : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                decimal valor = Convert.ToDecimal(value);

                if (valor >= 1m)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("El monto debe mayor o igual a 1");

            }
            return new ValidationResult("Debes poner un monto");
        }
    }
}
