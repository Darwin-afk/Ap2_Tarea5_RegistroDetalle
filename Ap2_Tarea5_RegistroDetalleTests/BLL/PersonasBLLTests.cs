using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ap2_Tarea5_RegistroDetalle.BLL;
using System;
using System.Collections.Generic;
using System.Text;
using Ap2_Tarea5_RegistroDetalle.Models;

namespace Ap2_Tarea5_RegistroDetalle.BLL.Tests
{
    [TestClass()]
    public class PersonasBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Personas persona = new Personas();

            persona.PersonaId = 0;
            persona.Nombres = "Jose Rivera";
            persona.Telefono = "809-555-1234";
            persona.Cedula = "402-1234567-8";
            persona.Direccion = "El valle";
            persona.FechaNacimiento = DateTime.Now;
            persona.Balance = 0.0m;

            bool paso = PersonasBLL.Guardar(persona);

            Assert.IsTrue(paso);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            bool paso = PersonasBLL.Existe(1);

            Assert.IsTrue(paso);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            bool paso = PersonasBLL.Eliminar(1);

            Assert.IsTrue(paso);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Personas persona = PersonasBLL.Buscar(1);

            Assert.IsTrue(persona != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            List<Personas> listadoPersonas = PersonasBLL.GetList(p => true);

            Assert.IsTrue(listadoPersonas != null);
        }
    }
}