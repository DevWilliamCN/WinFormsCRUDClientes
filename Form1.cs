using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsCRUDClientes.Models;

namespace WinFormsCRUDClientes
{
    public partial class Form1 : Form
    {
        // Contexto de base de datos (Entity Framework)
        private readonly AppDbContext _db = new AppDbContext();

        // Variable para guardar el Id del cliente seleccionado
        private int _idSeleccionado = 0;

        public Form1()
        {
            InitializeComponent();

            // Eventos para botones y grid
            dgvClientes.CellClick += dgvClientes_CellClick;
            btnCargar.Click += (s, e) => CargarDatos();
            btnAgregar.Click += btnAgregar_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnLimpiar.Click += (s, e) => Limpiar();

            // Cargo la lista de clientes al iniciar
            CargarDatos();
        }

        // Método para cargar la lista de clientes
        private void CargarDatos()
        {
            try
            {
                var lista = _db.Clientes
                               .OrderBy(c => c.Id)
                               .Select(c => new
                               {
                                   c.Id,
                                   c.Cedula,
                                   c.Nombre,
                                   c.Telefono,
                                   c.Correo,
                                   c.Direccion
                               })
                               .ToList();

                dgvClientes.DataSource = lista;
                dgvClientes.ClearSelection();
                _idSeleccionado = 0; // Reseteo el Id seleccionado
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show("Error al cargar datos: " + mensajeError, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Validación de datos antes de guardar o actualizar
        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Limpia los campos de texto
        private void Limpiar()
        {
            txtCedula.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();
            _idSeleccionado = 0;
            dgvClientes.ClearSelection();
            txtCedula.Focus();
        }

        // Agregar un nuevo cliente
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                var nuevo = new Cliente
                {
                    Cedula = txtCedula.Text?.Trim(),
                    Nombre = txtNombre.Text?.Trim(),
                    Telefono = txtTelefono.Text?.Trim(),
                    Correo = txtCorreo.Text?.Trim(),
                    Direccion = txtDireccion.Text?.Trim()
                };

                _db.Clientes.Add(nuevo);
                _db.SaveChanges();
                CargarDatos();
                Limpiar();
                MessageBox.Show("Cliente agregado.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show("No se pudo agregar: " + mensajeError, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Actualizar cliente existente
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un registro.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Validar()) return;

            try
            {
                var cliente = _db.Clientes.Find(_idSeleccionado);
                if (cliente == null)
                {
                    MessageBox.Show("No se encontró el cliente.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cliente.Cedula = txtCedula.Text?.Trim();
                cliente.Nombre = txtNombre.Text?.Trim();
                cliente.Telefono = txtTelefono.Text?.Trim();
                cliente.Correo = txtCorreo.Text?.Trim();
                cliente.Direccion = txtDireccion.Text?.Trim();

                _db.SaveChanges();
                CargarDatos();
                Limpiar();
                MessageBox.Show("Cliente actualizado.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show("No se pudo actualizar: " + mensajeError, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eliminar cliente
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un registro.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Eliminar el cliente seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var cliente = _db.Clientes.Find(_idSeleccionado);
                if (cliente != null)
                {
                    _db.Clientes.Remove(cliente);
                    _db.SaveChanges();
                    CargarDatos();
                    Limpiar();
                    MessageBox.Show("Cliente eliminado.", "OK",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string mensajeError = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show("No se pudo eliminar: " + mensajeError, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento al seleccionar una fila del DataGridView
        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvClientes.Rows[e.RowIndex];
            _idSeleccionado = Convert.ToInt32(row.Cells["Id"].Value);

            txtCedula.Text = row.Cells["Cedula"].Value?.ToString();
            txtNombre.Text = row.Cells["Nombre"].Value?.ToString();
            txtTelefono.Text = row.Cells["Telefono"].Value?.ToString();
            txtCorreo.Text = row.Cells["Correo"].Value?.ToString();
            txtDireccion.Text = row.Cells["Direccion"].Value?.ToString();
        }

        // Liberar recursos del contexto al cerrar el formulario
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _db?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
