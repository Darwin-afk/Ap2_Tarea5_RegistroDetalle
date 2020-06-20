using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ap2_Tarea5_RegistroDetalle.BLL;
using System;
using System.Collections.Generic;
using System.Text;
using Ap2_Tarea5_RegistroDetalle.Models;

namespace Ap2_Tarea5_RegistroDetalle.BLL.Tests
{
    [TestClass()]
    public class MorasBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Moras moras = new Moras();

            moras.MoraId = 0;
            moras.Fecha = DateTime.Now;
            moras.Total = 0;

            bool paso = MorasBLL.Guardar(moras);

            Assert.IsTrue(paso);
        }

        [TestMethod()]
        public void ExisteTest()
        {
            bool paso = MorasBLL.Existe(1);

            Assert.IsTrue(paso);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            bool paso = MorasBLL.Eliminar(1);

            Assert.IsTrue(paso);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Moras moras = MorasBLL.Buscar(1);

            Assert.IsTrue(moras != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            List<Moras> listadoMoras = MorasBLL.GetList(m => true);

            Assert.IsTrue(listadoMoras != null);
        }
    }
}