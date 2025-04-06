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

            // Validar el formato del ID antes de proceder
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtId.Text, $"^{ObtenerFormatoID()}\\d{{3}}$"))
            {
                MessageBox.Show($"El ID debe tener el formato '{ObtenerFormatoID()}000' a '{ObtenerFormatoID()}999'. Por favor, ingrese un ID válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtId.Focus();
                return;
            }

            // Proceder con el guardado
            try
            {
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();
                    SqlCommand command = null; // Inicializar la variable command

                    // Construir el INSERT según la tabla activa
                    if (TablaActiva == "Clientes")
                    {
                        string query = "INSERT INTO Clientes (ID, Nombre, TipoCliente, Ciudad, Pais) VALUES (@ID, @Nombre, @TipoCliente, @Ciudad, @Pais)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@TipoCliente", txtTipoCliente.Text);
                        command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                        command.Parameters.AddWithValue("@Pais", txtPais.Text);
                    }
                    else if (TablaActiva == "FactoresDeCostos")
                    {
                        string query = "INSERT INTO FactoresDeCostos (ID, VentaID, ProductoID, CostoOperativo, GananciaNeta) VALUES (@ID, @VentaID, @ProductoID, @CostoOperativo, @GananciaNeta)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text);
                        command.Parameters.AddWithValue("@VentaID", txtVentaID.Text);
                        command.Parameters.AddWithValue("@ProductoID", txtProductoID.Text);
                        command.Parameters.AddWithValue("@CostoOperativo", txtCostoOperativo.Text);
                        command.Parameters.AddWithValue("@GananciaNeta", txtGananciaNeta.Text);
                    }
                    else if (TablaActiva == "Productos")
                    {
                        string query = "INSERT INTO Productos (ID, Nombre, Categoria, Precio, PrecioCosto) VALUES (@ID, @Nombre, @Categoria, @Precio, @PrecioCosto)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Categoria", txtCategoria.Text);
                        command.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                        command.Parameters.AddWithValue("@PrecioCosto", txtPrecioCosto.Text);
                    }
                    else if (TablaActiva == "Ubicaciones")
                    {
                        string query = "INSERT INTO Ubicaciones (ID, Ciudad, Region, Pais) VALUES (@ID, @Ciudad, @Region, @Pais)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text);
                        command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                        command.Parameters.AddWithValue("@Region", txtRegion.Text);
                        command.Parameters.AddWithValue("@Pais", txtPais.Text);
                    }
                    else if (TablaActiva == "Usuarios")
                    {
                        string query = "INSERT INTO Usuarios (ID, Nombre, Contra, Rol) VALUES (@ID, @Nombre, @Contra, @Rol)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Contra", txtContra.Text);
                        command.Parameters.AddWithValue("@Rol", txtRol.Text);
                    }
                    else if (TablaActiva == "Ventas")
                    {
                        string query = "INSERT INTO Ventas (ID, Producto, Cliente, Fecha, Cantidad, Total, UbicacionID) VALUES (@ID, @Producto, @Cliente, @Fecha, @Cantidad, @Total, @UbicacionID)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text);
                        command.Parameters.AddWithValue("@Producto", txtProducto.Text);
                        command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                        command.Parameters.AddWithValue("@Fecha", dtpFecha.Value); // Si usas DateTimePicker
                        command.Parameters.AddWithValue("@Cantidad", txtCantidad.Text);
                        command.Parameters.AddWithValue("@Total", txtTotal.Text);
                        command.Parameters.AddWithValue("@UbicacionID", txtUbicacionID.Text);
                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado una tabla válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Ejecutar la consulta INSERT
                    if (command != null)
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cerrar el formulario y devolver el resultado
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
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
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();
                    string query = $"SELECT COUNT(*) FROM {tabla} WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", id);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // Retorna true si el ID existe
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

        private void txtVentaID_Leave(object sender, EventArgs e)
        {
            ValidarID("Ventas", txtVentaID.Text, "V");
        }

        private void txtProductoID_Leave(object sender, EventArgs e)
        {
            ValidarID("Productos", txtProductoID.Text, "P");
        }

        private void txtUbicacionID_Leave(object sender, EventArgs e)
        {
            ValidarID("Ubicaciones", txtUbicacionID.Text, "U");
        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            ValidarID("Clientes", txtCliente.Text, "C");
        }

        private void ValidarID(string tabla, string idIngresado, string prefijo)
        {
            if (string.IsNullOrEmpty(idIngresado))
            {
                MessageBox.Show("El campo no puede estar vacío. Por favor, ingrese un ID.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar el formato del ID (por ejemplo, "V000")
            if (!System.Text.RegularExpressions.Regex.IsMatch(idIngresado, $"^{prefijo}\\d{{3}}$"))
            {
                MessageBox.Show($"El ID debe tener el formato '{prefijo}000' a '{prefijo}999'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                ConexionBD conexion = new ConexionBD();
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    // Verificar si el ID existe en la tabla correspondiente
                    string query = $"SELECT COUNT(*) FROM {tabla} WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", idIngresado);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        MessageBox.Show($"El ID '{idIngresado}' no existe en la tabla '{tabla}'. Por favor ingrese un ID válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al validar el ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarTodosLosIDs()
        {
            // Validar VentaID
            if (!ValidarIDExistente("Ventas", txtVentaID.Text, "V")) return false;
            // Validar ProductoID
            if (!ValidarIDExistente("Productos", txtProductoID.Text, "P")) return false;
            // Validar UbicacionID
            if (!ValidarIDExistente("Ubicaciones", txtUbicacionID.Text, "U")) return false;
            // Validar ClienteID
            if (!ValidarIDExistente("Clientes", txtCliente.Text, "C")) return false;

            return true;
        }

        private bool ValidarIDExistente(string tabla, string idIngresado, string prefijo)
        {
            if (string.IsNullOrEmpty(idIngresado) || !System.Text.RegularExpressions.Regex.IsMatch(idIngresado, $"^{prefijo}\\d{{3}}$"))
            {
                MessageBox.Show($"El ID '{idIngresado}' no es válido. Por favor ingrese un ID en el formato '{prefijo}000'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                ConexionBD conexion = new ConexionBD();
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();
                    string query = $"SELECT COUNT(*) FROM {tabla} WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", idIngresado);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        MessageBox.Show($"El ID '{idIngresado}' no existe en la tabla '{tabla}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar el ID '{idIngresado}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

    }
}
