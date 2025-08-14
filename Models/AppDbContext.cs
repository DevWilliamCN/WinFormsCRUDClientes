using System.Data.Entity;

namespace WinFormsCRUDClientes.Models
{
    // En esta parte realice que la clase se encargue de manejar la conexión con la base de datos 
    // y el mapeo de las entidades a sus tablas correspondientes usando Entity Framework.
    public class AppDbContext : DbContext
    {
        // Constructor que utiliza la cadena de conexión definida en App.config
        public AppDbContext() : base("name=ConexionSQL") { }

        // Constructor estático que evita que EF intente crear o modificar la base de datos automáticamente.
        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        // Representa la tabla "cliente" en la base de datos y permite hacer consultas y operaciones CRUD.
        public DbSet<Cliente> Clientes { get; set; }
    }
}
