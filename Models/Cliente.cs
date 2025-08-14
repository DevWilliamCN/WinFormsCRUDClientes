using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinFormsCRUDClientes.Models
{
    // Esta clase representa la tabla "cliente" en la base de datos.
    // Utilizo anotaciones para indicar el nombre de la tabla y las claves primarias.
    [Table("cliente")]
    public class Cliente
    {
        // Llave primaria de la tabla
        [Key]
        public int Id { get; set; }

        // Número de cédula del cliente
        public string Cedula { get; set; }

        // Nombre completo del cliente
        public string Nombre { get; set; }

        // Teléfono de contacto
        public string Telefono { get; set; }

        // Correo electrónico
        public string Correo { get; set; }

        // Dirección física del cliente
        public string Direccion { get; set; }
    }
}
