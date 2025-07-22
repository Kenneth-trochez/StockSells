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
            // Asegurarse de que todos los campos estén ocultos inicialmente
            lblNombre.Visible = false; txtNombre.Visible = false;
            lblTipoCliente.Visible = false; txtTipoCliente.Visible = false;
            lblCuidad.Visible = false; txtCiudad.Visible = false;
            lblPais.Visible = false; txtPais.Visible = false;

            lblVentaID.Visible = false; txtVentaID.Visible = false;
            lblProductoID.Visible = false; txtProductoID.Visible = false;
            lblCostoOperativo.Visible = false; txtCostoOperativo.Visible = false;
            lblGananciaNeta.Visible = false; txtGananciaNeta.Visible = false;

            lblCategoria.Visible = false; txtCategoria.Visible = false;
            lblPrecio.Visible = false; txtPrecio.Visible = false;
            lblPrecioCosto.Visible = false; txtPrecioCosto.Visible = false;

            lblRegion.Visible = false; txtRegion.Visible = false;

            lblContra.Visible = false; txtContra.Visible = false;
            lblRol.Visible = false; txtRol.Visible = false;

            lblProducto.Visible = false; txtProducto.Visible = false;
            lblCliente.Visible = false; txtCliente.Visible = false;
            lblFecha.Visible = false; dtpFecha.Visible = false;
            lblCantidad.Visible = false; txtCantidad.Visible = false;
            lblTotal.Visible = false; txtTotal.Visible = false;
            lblUbicacionID.Visible = false; txtUbicacionID.Visible = false;

            // Mostrar los campos necesarios según la tabla activa
            if (TablaActiva == "Clientes")
            {
                lblNombre.Visible = true; txtNombre.Visible = true;
                lblTipoCliente.Visible = true; txtTipoCliente.Visible = true;
                lblCuidad.Visible = true; txtCiudad.Visible = true;
                lblPais.Visible = true; txtPais.Visible = true;
            }
            else if (TablaActiva == "FactoresDeCostos")
            {
                lblVentaID.Visible = true; txtVentaID.Visible = true;
                lblProductoID.Visible = true; txtProductoID.Visible = true;
                lblCostoOperativo.Visible = true; txtCostoOperativo.Visible = true;
                lblGananciaNeta.Visible = true; txtGananciaNeta.Visible = true;
            }
            else if (TablaActiva == "Productos")
            {
                lblNombre.Visible = true; txtNombre.Visible = true;
                lblCategoria.Visible = true; txtCategoria.Visible = true;
                lblPrecio.Visible = true; txtPrecio.Visible = true;
                lblPrecioCosto.Visible = true; txtPrecioCosto.Visible = true;
            }
            else if (TablaActiva == "Ubicaciones")
            {
                lblCuidad.Visible = true; txtCiudad.Visible = true;
                lblRegion.Visible = true; txtRegion.Visible = true;
                lblPais.Visible = true; txtPais.Visible = true;
            }
            else if (TablaActiva == "Usuarios")
            {
                lblNombre.Visible = true; txtNombre.Visible = true;
                lblContra.Visible = true; txtContra.Visible = true;
                lblRol.Visible = true; txtRol.Visible = true;
            }
            else if (TablaActiva == "Ventas")
            {
                lblProducto.Visible = true; txtProducto.Visible = true;
                lblCliente.Visible = true; txtCliente.Visible = true;
                lblFecha.Visible = true; dtpFecha.Visible = true; // Si usas DateTimePicker
                lblCantidad.Visible = true; txtCantidad.Visible = true;
                lblTotal.Visible = true; txtTotal.Visible = true;
                lblUbicacionID.Visible = true; txtUbicacionID.Visible = true;
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

                    if (TablaActiva == "clientes")
                    {
                        string query = @"INSERT INTO clientes (Nombre, cliente_id, pais, id_departamento) 
                             VALUES (@Nombre, @TipoCliente, @Pais, @Departamento)";
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@TipoCliente", txtTipoCliente.Text);
                        command.Parameters.AddWithValue("@Pais", txtPais.Text);
                        command.Parameters.AddWithValue("@Departamento", txtDepartamentoID.Text);
                    }
                    else if (TablaActiva == "productos")
                    {
                        string query = @"INSERT INTO productos (nombre, categoria_id, proveedor_id, Precio, precio_costo) 
                             VALUES (@Nombre, @Categoria, @Proveedor, @Precio, @PrecioCosto)";
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Categoria", txtCategoriaID.Text);
                        command.Parameters.AddWithValue("@Proveedor", txtProveedorID.Text);
                        command.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                        command.Parameters.AddWithValue("@PrecioCosto", txtPrecioCosto.Text);
                    }
                    else if (TablaActiva == "usuarios")
                    {
                        string query = @"INSERT INTO usuarios (nombre, contra, id_rol) 
                             VALUES (@Nombre, @Contra, @Rol)";
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Contra", txtContra.Text);
                        command.Parameters.AddWithValue("@Rol", txtRolID.Text);
                    }
                    else if (TablaActiva == "ventas")
                    {
                        string query = @"INSERT INTO ventas (nombre_producto, cliente, fecha, cantidad, total, ubicacion) 
                             VALUES (@Producto, @Cliente, @Fecha, @Cantidad, @Total, @Ubicacion)";
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Producto", txtProducto.Text);
                        command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                        command.Parameters.AddWithValue("@Fecha", dtpFecha.Value);
                        command.Parameters.AddWithValue("@Cantidad", txtCantidad.Text);
                        command.Parameters.AddWithValue("@Total", txtTotal.Text);
                        command.Parameters.AddWithValue("@Ubicacion", txtUbicacionID.Text);
                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado una tabla válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    command.ExecuteNonQuery();
                    MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
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

        private void txtId_Leave(object sender, EventArgs e)
        {
            // Obtener el prefijo según la tabla activa
            string prefijo = ObtenerFormatoID();

            if (string.IsNullOrEmpty(prefijo))
            {
                MessageBox.Show("No se ha seleccionado una tabla válida. Por favor, seleccione una tabla antes de ingresar el ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Focus();
                return;
            }

            // Validar el formato del ID ingresado
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtId.Text, $"^{prefijo}\\d{{3}}$"))
            {
                MessageBox.Show($"El ID debe tener el formato '{prefijo}000' a '{prefijo}999'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtId.Focus(); // Regresar el foco al TextBox para corregirlo
                return;
            }

            // Mensaje opcional de éxito si el formato es válido
            MessageBox.Show($"El ID '{txtId.Text}' tiene el formato válido para la tabla '{TablaActiva}'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string ObtenerFormatoID()
        {
            if (TablaActiva == "Clientes") return "C";
            if (TablaActiva == "FactoresDeCostos") return "F";
            if (TablaActiva == "Productos") return "P";
            if (TablaActiva == "Ubicaciones") return "U";
            if (TablaActiva == "Usuarios") return "A";
            if (TablaActiva == "Ventas") return "V";
            return string.Empty; // Retornar vacío si no hay tabla activa
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
       

    }
}
