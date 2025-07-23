using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockSells.forms
{
    public partial class Agreg : Form
    {
        public string TablaActiva { get; set; }

        public Agreg()
        {
            InitializeComponent();
        }

        private void Agreg_Load(object sender, EventArgs e)
        {
            // Ocultar todos los campos al iniciar
            lblNombre.Visible = txtNombreCliente.Visible = false;
            lblPais.Visible = txtPaisCliente.Visible = false;

            lblProveedorIDCompra.Visible = txtCategoria.Visible = false;
            lblNombreProveedor.Visible = txtProveedorID.Visible = false;
            lblPaisProveedor.Visible = txtPrecio.Visible = false;
            lblNombreUsuario.Visible = txtPrecioCost.Visible = false;
            lblNombreProducto.Visible = txtNombreProducto.Visible = false;

            lblNombreUsuario.Visible = txtNombreUsuario.Visible = false;
            lblContraUsuario.Visible = txtContraUsuario.Visible = false;
            lblRolID.Visible = txtRolID.Visible = false;

            lblProductoVenta.Visible = txtNombreProductoVenta.Visible = false;
            lblClienteVenta.Visible = txtClienteVenta.Visible = false;
            lblFechaVenta.Visible = dtpFechaVenta.Visible = false;
            lblCantidadVenta.Visible = txtCantidadVenta.Visible = false;
            lblTotalVenta.Visible = txtTotalVenta.Visible = false;
            lblUbicacionVenta.Visible = txtUbicacionVenta.Visible = false;

            lblNombreProveedor.Visible = txtNombreProveedor.Visible = false;
            lblPaisProveedor.Visible = txtPaisProveedor.Visible = false;

            lblProveedorIDCompra.Visible = txtProveedorIDCompra.Visible = false;
            lblFechaCompra.Visible = dtpFechaCompra.Visible = false;
            lblTotalCompra.Visible = txtTotalCompra.Visible = false;

            // Mostrar según la tabla activa
            switch (TablaActiva)
            {
                case "clientes":
                    lblNombre.Visible = txtNombreCliente.Visible = true;
                    lblPais.Visible = txtPaisCliente.Visible = true;
                    break;

                case "productos":
                    lblNombreProducto.Visible = txtNombreProducto.Visible = true;
                    lblProveedorIDCompra.Visible = txtCategoria.Visible = true;
                    lblNombreProveedor.Visible = txtProveedorID.Visible = true;
                    lblPaisProveedor.Visible = txtPrecio.Visible = true;
                    lblNombreUsuario.Visible = txtPrecioCost.Visible = true;
                    break;

                case "usuarios":
                    lblNombreUsuario.Visible = txtNombreUsuario.Visible = true;
                    lblContraUsuario.Visible = txtContraUsuario.Visible = true;
                    lblRolID.Visible = txtRolID.Visible = true;
                    break;

                case "ventas":
                    lblProductoVenta.Visible = txtNombreProductoVenta.Visible = true;
                    lblClienteVenta.Visible = txtClienteVenta.Visible = true;
                    lblFechaVenta.Visible = dtpFechaVenta.Visible = true;
                    lblCantidadVenta.Visible = txtCantidadVenta.Visible = true;
                    lblTotalVenta.Visible = txtTotalVenta.Visible = true;
                    lblUbicacionVenta.Visible = txtUbicacionVenta.Visible = true;
                    break;

                case "proveedores":
                    lblNombreProveedor.Visible = txtNombreProveedor.Visible = true;
                    lblPaisProveedor.Visible = txtPaisProveedor.Visible = true;
                    break;

                case "compras":
                    lblProveedorIDCompra.Visible = txtProveedorIDCompra.Visible = true;
                    lblFechaCompra.Visible = dtpFechaCompra.Visible = true;
                    lblTotalCompra.Visible = txtTotalCompra.Visible = true;
                    break;

                default:
                    MessageBox.Show("No se reconoció la tabla seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {


            ConexionBD conexion = new ConexionBD();

            try
            {
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();
                    MySqlCommand command = null;

                    switch (TablaActiva)
                    {
                        case "clientes":
                            command = new MySqlCommand("INSERT INTO clientes (nombre, pais) VALUES (@Nombre, @Pais)", connection);
                            command.Parameters.AddWithValue("@Nombre", txtNombreCliente.Text);
                            command.Parameters.AddWithValue("@Pais", txtPaisCliente.Text);
                            break;

                        case "productos":
                            command = new MySqlCommand("INSERT INTO productos (nombre, categoria_id, proveedor_id, precio, precio_costo) VALUES (@Nombre, @Categoria, @Proveedor, @Precio, @PrecioCosto)", connection);
                            command.Parameters.AddWithValue("@Nombre", txtNombreProducto.Text);
                            command.Parameters.AddWithValue("@Categoria", txtCategoria.Text);
                            command.Parameters.AddWithValue("@Proveedor", txtProveedorID.Text);
                            command.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                            command.Parameters.AddWithValue("@PrecioCosto", txtPrecioCost.Text);
                            break;

                        case "usuarios":
                            command = new MySqlCommand("INSERT INTO usuarios (nombre, contra, id_rol) VALUES (@Nombre, @Contra, @Rol)", connection);
                            command.Parameters.AddWithValue("@Nombre", txtNombreUsuario.Text);
                            command.Parameters.AddWithValue("@Contra", txtContraUsuario.Text);
                            command.Parameters.AddWithValue("@Rol", txtRolID.Text);
                            break;

                        case "ventas":
                            command = new MySqlCommand("INSERT INTO ventas (nombre_producto, cliente, fecha, cantidad, total, ubicacion) VALUES (@Producto, @Cliente, @Fecha, @Cantidad, @Total, @Ubicacion)", connection);
                            command.Parameters.AddWithValue("@Producto", txtNombreProductoVenta.Text);
                            command.Parameters.AddWithValue("@Cliente", txtClienteVenta.Text);
                            command.Parameters.AddWithValue("@Fecha", dtpFechaVenta.Value);
                            command.Parameters.AddWithValue("@Cantidad", txtCantidadVenta.Text);
                            command.Parameters.AddWithValue("@Total", txtTotalVenta.Text);
                            command.Parameters.AddWithValue("@Ubicacion", txtUbicacionVenta.Text);
                            break;

                        case "proveedores":
                            command = new MySqlCommand("INSERT INTO proveedores (nombre, pais) VALUES (@Nombre, @Pais)", connection);
                            command.Parameters.AddWithValue("@Nombre", txtNombreProveedor.Text);
                            command.Parameters.AddWithValue("@Pais", txtPaisProveedor.Text);
                            break;

                        case "compras":
                            command = new MySqlCommand("INSERT INTO compras (proveedor_id, fecha, total) VALUES (@ProveedorID, @Fecha, @Total)", connection);
                            command.Parameters.AddWithValue("@ProveedorID", txtProveedorIDCompra.Text);
                            command.Parameters.AddWithValue("@Fecha", dtpFechaCompra.Value);
                            command.Parameters.AddWithValue("@Total", txtTotalCompra.Text);
                            break;

                        default:
                            MessageBox.Show("No se ha seleccionado una tabla válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    command.ExecuteNonQuery();
                    MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error de MySQL: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVentaID_TextChanged(object sender, EventArgs e)
        {

        }

        private bool ValidarExistenciaEnBaseDeDatos(string tabla, string id)
        {
            try
            {
                ConexionBD conexion = new ConexionBD();
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    string query = $"SELECT COUNT(*) FROM {tabla} WHERE {id} = @ID";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", id);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar el ID en la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelClientes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtGananciaNeta_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblGananciaNeta_Click(object sender, EventArgs e)
        {

        }
    }
}
