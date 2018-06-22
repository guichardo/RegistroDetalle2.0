using RegistroDetalle2.DAL;
using RegistroDetalle2.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RegistroDetalle2._0.BLL
{
    /// <summary>
    /// En esta clase debemos programar toda la logica de negocios
    /// </summary>
    public class PersonasBLL
    {


        /// <summary>
        /// Permite guardar una entidad en la base de datos
        /// </summary>
        /// <param name="persona">Una instancia de Persona</param>
        /// <returns>Retorna True si guardo o Falso si falló </returns>
        public static bool Guardar(Persona persona)
        {
            bool paso = false;
            //Creamos una instancia del contexto para poder conectar con la BD
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Persona.Add(persona) != null)
                {
                    contexto.SaveChanges(); //Guardar los cambios
                    paso = true;
                }

                contexto.Dispose();//siempre hay que cerrar la conexion
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        /// <summary>
        /// Permite Modificar una entidad en la base de datos 
        /// </summary>
        /// <param name="persona">Una instancia de Persona</param>
        /// <returns>Retorna True si Modifico o Falso si falló </returns>
        public static bool Modificar(Persona persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(persona).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        /// <summary>
        /// Permite Eliminar una entidad en la base de datos
        /// </summary>
        ///<param name="id">El Id de la persona que se desea eliminar </param>
        /// <returns>Retorna True si Eliminó o Falso si falló </returns>
        public static bool Eliminar(int id)
        {
            bool paso = false;

            Contexto contexto = new Contexto();
            try
            {
                Persona persona = contexto.Persona.Find(id);

                contexto.Persona.Remove(persona);

                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        /// <summary>
        /// Permite Buscar una entidad en la base de datos
        /// </summary>
        ///<param name="id">El Id de la persona que se desea encontrar </param>
        /// <returns>Retorna la persona encontrada </returns>
        public static Persona Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Persona persona = new Persona();
            try
            {
                persona = contexto.Persona.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
            return persona;
        }

        /// <summary>
        /// Permite extraer una lista de Personas de la base de datos
        /// </summary> 
        ///<param name="expression">Expression Lambda conteniendo los filtros de busqueda </param>
        /// <returns>Retorna una lista de personas</returns>
        public static List<Persona> GetList(Expression<Func<Persona, bool>> expression)
        {
            List<Persona> Persona = new List<Persona>();
            Contexto contexto = new Contexto();
            try
            {
                Persona = contexto.Persona.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {

                throw;
            }

            return Persona;
        }
    }
}