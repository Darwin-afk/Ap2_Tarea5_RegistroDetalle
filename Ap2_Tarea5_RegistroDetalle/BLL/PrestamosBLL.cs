using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ap2_Tarea5_RegistroDetalle.Models;
using Ap2_Tarea5_RegistroDetalle.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ap2_Tarea5_RegistroDetalle.BLL
{
    public class PrestamosBLL
    {
        public static bool Guardar(Prestamos prestamo)
        {
            prestamo.Balance -= prestamo.Monto;

            if (!Existe(prestamo.PrestamoId))
                return Insertar(prestamo);
            else
                return Modificar(prestamo);
        }

        public static bool Existe(int id)
        {
            bool encontrado = false;
            Contexto contexto = new Contexto();

            try
            {
                encontrado = contexto.Prestamos.Any(p => p.PrestamoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        private static bool Insertar(Prestamos prestamo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                if(prestamo.Detalle.Count > 0)
                {
                    CrearMora(prestamo.Detalle);
                }

                contexto.Prestamos.Add(prestamo);
                paso = contexto.SaveChanges() > 0;

                if (paso)
                {
                    AumentarBalancePersona(prestamo);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        } 

        private static void CrearMora(List<MorasDetalle> detalle)
        {
            Moras mora = new Moras();

            foreach(var item in detalle)
            {
                mora.Total += item.Valor;
            }

            MorasBLL.Guardar(mora);

            AsignarMoraIdNueva(detalle);
        }

        private static void AsignarMoraIdNueva(List<MorasDetalle> detalle)
        {
            Moras mora = new Moras();
            Contexto contexto = new Contexto();

            try
            {
                mora = contexto.Moras.OrderByDescending(m => m.MoraId).First();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            foreach(var item in detalle)
            {
                item.MoraId = mora.MoraId;
            }
        }

        private static void AumentarBalancePersona(Prestamos prestamo)
        {
            var persona = PersonasBLL.Buscar(prestamo.PersonaId);
            persona.Balance += prestamo.Balance;
            PersonasBLL.Guardar(persona);
        }

        private static bool Modificar(Prestamos prestamo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var monto = prestamo.Monto;

                var anteriorPrestamo = PrestamosBLL.Buscar(prestamo.PrestamoId);
                prestamo.Monto += anteriorPrestamo.Monto;

                if (VerificarDetalleMoraId(prestamo.Detalle))
                {
                    BuscarMoraId(prestamo);
                }

                contexto.Database.ExecuteSqlRaw($"Delete FROM MorasDetalle Where PrestamoId={prestamo.PrestamoId}");
                foreach (var item in prestamo.Detalle)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(prestamo).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;

                if (paso)
                {
                    RestarBalancePersona(prestamo.PersonaId, monto);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        private static bool VerificarDetalleMoraId(List<MorasDetalle> detalle)
        {
            bool paso = false;

            foreach(var item in detalle)
            {
                if (item.MoraId == 0)
                {
                    paso = true;
                    break;
                }
            }

            return paso;
        }

        private static void BuscarMoraId(Prestamos prestamo)
        {
            Prestamos prestamoAnterior = new Prestamos();
            Contexto contexto = new Contexto();

            try
            {
                prestamoAnterior = PrestamosBLL.Buscar(prestamo.PrestamoId);

                if(prestamoAnterior.Detalle.Count > 0)
                {
                    AsignarMoraIdVieja(prestamo.Detalle, prestamoAnterior.Detalle[0].MoraId);
                }
                else
                {
                    CrearMora(prestamo.Detalle);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
        }

        private static void AsignarMoraIdVieja(List<MorasDetalle> detalle, int moraId)
        {
            foreach(var item in detalle)
            {
                item.MoraId = moraId;
            }
        }

        private static void RestarBalancePersona(int personaId, decimal monto)
        {
            var persona = PersonasBLL.Buscar(personaId);
            persona.Balance -= monto;
            PersonasBLL.Guardar(persona);
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var prestamo = contexto.Prestamos.Find(id);

                if (prestamo != null)
                {
                    Personas persona = PersonasBLL.Buscar(prestamo.PersonaId);
                    persona.Balance -= prestamo.Balance;
                    PersonasBLL.Guardar(persona);

                    contexto.Prestamos.Remove(prestamo);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Prestamos Buscar(int id)
        {
            Prestamos prestamo;
            Contexto contexto = new Contexto();

            try
            {
                prestamo = contexto.Prestamos.Include(p => p.Detalle)
                                   .Where(p => p.PrestamoId == id)
                                   .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return prestamo;
        }

        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            List<Prestamos> lista = new List<Prestamos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Prestamos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
