using System;
using System.ComponentModel.DataAnnotations;

namespace RegistroDetalle2.Entidades
{
    //Debe ser PUBLIC para que sea visible para las demas capas
    public class Persona
    {
        //Esta es la llave primaria
        [Key]//hay que importar System.ComponentModel.DataAnnotations;
        public int PersonaId { get; set; }
        public DateTime Fecha { get; set; }
        public string Nombres { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }


        //todo: aprender a crear las propiedades de la forma corta.
        public Persona()
        {

        }

        public override string ToString()
        {
            return this.Nombres;
        }

    }
}