using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ap2_Tarea5_RegistroDetalle.BLL;
using System;
using System.Collections.Generic;
using System.Text;
using Ap2_Tarea5_RegistroDetalle.Models;

namespace Ap2_Tarea5_RegistroDetalle.BLL.Tests
{
    [TestClass()]
    public class PrestamosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Personas prePersona = PersonasBLL.Buscar(1);

            Prestamos prestamo = new Prestamos();
            prestamo.PrestamoId = 0;
            prestamo.Fecha = DateTime.Now;
            prestamo.PersonaId = 1;
            prestamo.Concepto = "Primer prestamo";
            prestamo.Monto = 0.0m;
            prestamo.Balance = 2600.0m;
            prestamo.Detalle.Add(new MorasDetalle{ Id = 0, MoraId = 0, PrestamoId = 0, Valor= 50});

            PrestamosBLL.Guardar(prestamo);

            Personas postPersona = PersonasBLL.Buscar(1);

            Assert.AreEqual(prePersona.Balance + 5200, postPersona.Balance);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            bool paso = PrestamosBLL.Existe(2);

            Assert.IsTrue(paso);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Personas Prepersona = PersonasBLL.Buscar(1);

            PrestamosBLL.Eliminar(1);

            Personas PostPersona = PersonasBLL.Buscar(1);

            Assert.AreEqual(Prepersona.Balance - 2600, PostPersona.Balance);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Prestamos prestamo = PrestamosBLL.Buscar(1);

            Assert.IsTrue(prestamo != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            List<Prestamos> listadoPrestamos = PrestamosBLL.GetList(p => true);

            Assert.IsTrue(listadoPrestamos != null);
        }
    }
}