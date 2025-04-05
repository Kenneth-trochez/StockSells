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
    public partial class edit : Form
    {
        public edit()
        {
            InitializeComponent();
        }
        public string TablaActiva { get; set; }
        public string ID { get; set; }

        private void edit_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=MSI\\SQLEXPRESS;Database=API;Integrated Security=True;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Construir la consulta SELECT para obtener los datos del registro
                    string query = $"SELECT * FROM {TablaActiva} WHERE ID = @ID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", ID);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Completar los campos del formulario según la tabla activa
                        if (TablaActiva == "Clientes")
                        {
                            txtId.Text = reader["ID"].ToString();
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtTipoCliente.Text = reader["TipoCliente"].ToString();
                            txtCiudad.Text = reader["Ciudad"].ToString();
                            txtPais.Text = reader["Pais"].ToString();
                        }
                        else if (TablaActiva == "FactoresDeCostos")
                        {
                            txtId.Text = reader["ID"].ToString();
                            txtVentaID.Text = reader["VentaID"].ToString();
                            txtProductoID.Text = reader["ProductoID"].ToString();
                            txtCostoOperativo.Text = reader["CostoOperativo"].ToString();
                            txtGananciaNeta.Text = reader["GananciaNeta"].ToString();
                        }
                        else if (TablaActiva == "Productos")
                        {
                            txtId.Text = reader["ID"].ToString();
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtCategoria.Text = reader["Categoria"].ToString();
                            txtPrecio.Text = reader["Precio"].ToString();
                            txtPrecioCosto.Text = reader["PrecioCosto"].ToString();
                        }
                        else if (TablaActiva == "Ubicaciones")
                        {
                            txtId.Text = reader["ID"].ToString();
                            txtCiudad.Text = reader["Cuidad"].ToString();
                            txtRegion.Text = reader["Region"].ToString();
                            txtPais.Text = reader["Pais"].ToString();
                        }
                        else if (TablaActiva == "Usuarios")
                        {
                            txtId.Text = reader["ID"].ToString();
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtContra.Text = reader["Contra"].ToString();
                            txtRol.Text = reader["Rol"].ToString();
                        }
                        else if (TablaActiva == "Ventas")
                        {
                            txtId.Text = reader["ID"].ToString();
                            txtProducto.Text = reader["Producto"].ToString();
                            txtCliente.Text = reader["Cliente"].ToString();
                            dtpFecha.Value = Convert.ToDateTime(reader["Fecha"]);
                            txtCantidad.Text = reader["Cantidad"].ToString();
                            txtTotal.Text = reader["Total"].ToString();
                            txtUbicacionID.Text = reader["UbicacionID"].ToString();
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar el registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    // Construir la consulta UPDATE según la tabla activa
                    string query = "";
                    SqlCommand command;

                    if (TablaActiva == "Clientes")
                    {
                        query = "UPDATE Clientes SET Nombre = @Nombre, TipoCliente = @TipoCliente, Ciudad = @Ciudad, Pais = @Pais WHERE ID = @ID";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@TipoCliente", txtTipoCliente.Text);
                        command.Parameters.AddWithValue("@Ciudad", txtCiudad.Text);
                        command.Parameters.AddWithValue("@Pais", txtPais.Text);
                    }
                    else if (TablaActiva == "FactoresDeCostos")
                    {
                        query = "UPDATE FactoresDeCostos SET VentaID = @VentaID, ProductoID = @ProductoID, CostoOperativo = @CostoOperativo, GananciaNeta = @GananciaNeta WHERE ID = @ID";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@VentaID", txtVentaID.Text);
                        command.Parameters.AddWithValue("@ProductoID", txtProductoID.Text);
                        command.Parameters.AddWithValue("@CostoOperativo", txtCostoOperativo.Text);
                        command.Parameters.AddWithValue("@GananciaNeta", txtGananciaNeta.Text);
                    }
                    else if (TablaActiva == "Productos")
                    {
                        query = "UPDATE Productos SET Nombre = @Nombre, Categoria = @Categoria, Precio = @Precio, PrecioCosto = @PrecioCosto WHERE ID = @ID";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Categoria", txtCategoria.Text);
                        command.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                        command.Parameters.AddWithValue("@PrecioCosto", txtPrecioCosto.Text);
                    }
                    else if (TablaActiva == "Ubicaciones")
                    {
                        query = "UPDATE Ubicaciones SET Cuidad = @Cuidad, Region = @Region, Pais = @Pais WHERE ID = @ID";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Cuidad", txtCiudad.Text);
                        command.Parameters.AddWithValue("@Region", txtRegion.Text);
                        command.Parameters.AddWithValue("@Pais", txtPais.Text);
                    }
                    else if (TablaActiva == "Usuarios")
                    {
                        query = "UPDATE Usuarios SET Nombre = @Nombre, Contra = @Contra, Rol = @Rol WHERE ID = @ID";
                        command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Contra", txtContra.Text);
                        command.Parameters.AddWithValue("@Rol", txtRol.Text);
                    }
                    else if (TablaActiva == "Ventas")
                    {
                        query = "UPDATE Ventas SET Producto = @Producto, Cliente = @Cliente, Fecha = @Fecha, Cantidad = @Cantidad, Total = @Total, UbicacionID = @UbicacionID WHERE ID = @ID";
                        command = new SqlCommand(query, connection);
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

                    // Agregar el parámetro ID para identificar el registro a actualizar
                    command.Parameters.AddWithValue("@ID", txtId.Text);

                    // Ejecutar el comando UPDATE
                    command.ExecuteNonQuery();
                    MessageBox.Show("Registro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cerrar el formulario
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
