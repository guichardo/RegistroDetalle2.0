using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDetalle2.Entidades
{
    //Debe ser PUBLIC para que sea visible para las demas capas
    public class Articulos
    {
        [Key] //hay que importar System.ComponentModel.DataAnnotations;
        public int ArticuloId { get; set; }
        public string Descripcion { get; set; }
        public int Precio { get; set; }
        public int CantidadCotizada { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public Articulos()
        {

        }


        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}
