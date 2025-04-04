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
            string connectionString = "Server=DESKTOP-VPG9DEB;Database=API_BD;Integrated Security=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;

                    // Validar que el ID no esté vacío
                    if (string.IsNullOrEmpty(txtId.Text))
                    {
                        MessageBox.Show("Por favor, ingrese un valor para el ID.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validar que el ID sea único en la tabla activa
                    string checkQuery = $"SELECT COUNT(*) FROM {TablaActiva} WHERE ID = @ID";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@ID", txtId.Text);
                    int exists = (int)checkCommand.ExecuteScalar();

                    if (exists > 0)
                    {
                        MessageBox.Show($"El ID '{txtId.Text}' ya existe en la tabla '{TablaActiva}'. Por favor, ingrese un ID único.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // INSERT dinámico según la tabla activa
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
                        command.Parameters.AddWithValue("@ID", txtId.Text); // Agregar el ID como en Clientes
                        command.Parameters.AddWithValue("@VentaID", txtVentaID.Text);
                        command.Parameters.AddWithValue("@ProductoID", txtProductoID.Text);
                        command.Parameters.AddWithValue("@CostoOperativo", txtCostoOperativo.Text);
                        command.Parameters.AddWithValue("@GananciaNeta", txtGananciaNeta.Text);
                    }
                    else if (TablaActiva == "Productos")
                    {
                        string query = "INSERT INTO Productos (ID, Nombre, Categoria, Precio, PrecioCosto) VALUES (@ID, @Nombre, @Categoria, @Precio, @PrecioCosto)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text); // Agregar el ID como en Clientes
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Categoria", txtCategoria.Text);
                        command.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                        command.Parameters.AddWithValue("@PrecioCosto", txtPrecioCosto.Text);
                    }
                    else if (TablaActiva == "Ubicaciones")
                    {
                        string query = "INSERT INTO Ubicaciones (ID, Ciudad, Region, Pais) VALUES (@ID, @Ciudad, @Region, @Pais)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text); // Agregar el ID como en Clientes
                        command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                        command.Parameters.AddWithValue("@Region", txtRegion.Text);
                        command.Parameters.AddWithValue("@Pais", txtPais.Text);
                    }
                    else if (TablaActiva == "Usuarios")
                    {
                        string query = "INSERT INTO Usuarios (ID, Nombre, Contra, Rol) VALUES (@ID, @Nombre, @Contra, @Rol)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text); // Agregar el ID como en Clientes
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Contra", txtContra.Text);
                        command.Parameters.AddWithValue("@Rol", txtRol.Text);
                    }
                    else if (TablaActiva == "Ventas")
                    {
                        string query = "INSERT INTO Ventas (ID, Producto, Cliente, Fecha, Cantidad, Total, UbicacionID) VALUES (@ID, @Producto, @Cliente, @Fecha, @Cantidad, @Total, @UbicacionID)";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", txtId.Text); // Agregar el ID como en Clientes
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

                    // Ejecutar la consulta
                    command.ExecuteNonQuery();
                    MessageBox.Show("Registro agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cerrar el formulario y devolver el resultado
                    this.DialogResult = DialogResult.OK;
                    this.Close();
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
    }
}
