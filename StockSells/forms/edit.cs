
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using MySql.Data.MySqlClient;

namespace StockSells.forms
{
    public partial class edit : Form
    {
        public edit()
        {
            InitializeComponent();
        }

        public string TablaActiva { get; set; }
        public string IDSeleccionado { get; set; }

        private void edit_Load(object sender, EventArgs e)
        {

            OcultarTodosLosControles(); // 🔒 Oculta todos los campos al iniciar

            ConexionBD conexion = new ConexionBD();

            try
            {
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    if (string.IsNullOrWhiteSpace(IDSeleccionado))
                    {
                        MessageBox.Show("No se ha recibido un ID válido para edición.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string query = $"SELECT * FROM {TablaActiva} WHERE id = @ID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", IDSeleccionado);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblidselec.Text = reader["id"].ToString();

                                switch (TablaActiva)
                                {
                                    case "clientes":
                                        txtNombreCliente.Text = reader["nombre"].ToString();
                                        txtPaisCliente.Text = reader["pais"].ToString();
                                        MostrarControlesClientes();
                                        break;

                                    case "productos":
                                        txtNombreProducto.Text = reader["nombre"].ToString();
                                        txtCategoria.Text = reader["categoria_id"].ToString();
                                        txtProveedorID.Text = reader["proveedor_id"].ToString();
                                        txtPrecio.Text = reader["precio"].ToString();
                                        txtPrecioCost.Text = reader["precio_costo"].ToString();
                                        MostrarControlesProductos();
                                        break;

                                    case "usuarios":
                                        txtNombreUsuario.Text = reader["nombre"].ToString();
                                        txtContraUsuario.Text = reader["contra"].ToString();
                                        txtRolID.Text = reader["id_rol"].ToString();
                                        MostrarControlesUsuarios();
                                        break;

                                    case "ventas":
                                        txtNombreProductoVenta.Text = reader["nombre_producto"].ToString();
                                        txtClienteVenta.Text = reader["cliente"].ToString();
                                        dtpFechaVenta.Value = Convert.ToDateTime(reader["fecha"]);
                                        txtCantidadVenta.Text = reader["cantidad"].ToString();
                                        txtTotalVenta.Text = reader["total"].ToString();
                                        txtUbicacionVenta.Text = reader["ubicacion"].ToString();
                                        MostrarControlesVentas();
                                        break;

                                    case "proveedores":
                                        txtNombreProveedor.Text = reader["nombre"].ToString();
                                        txtPaisProveedor.Text = reader["pais"].ToString();
                                        MostrarControlesProveedores();
                                        break;

                                    case "compras":
                                        txtProveedorIDCompra.Text = reader["proveedor_id"].ToString();
                                        dtpFechaCompra.Value = Convert.ToDateTime(reader["fecha"]);
                                        txtTotalCompra.Text = reader["total"].ToString();
                                        MostrarControlesCompras();
                                        break;

                                    default:
                                        MessageBox.Show("La tabla seleccionada no está soportada para edición.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el registro especificado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OcultarTodosLosControles()
        {
            // Clientes
            txtNombreCliente.Visible = false;
            txtPaisCliente.Visible = false;

            // Productos
            txtNombreProducto.Visible = false;
            txtCategoria.Visible = false;
            txtProveedorID.Visible = false;
            txtPrecio.Visible = false;
            txtPrecioCost.Visible = false;

            // Usuarios
            txtNombreUsuario.Visible = false;
            txtContraUsuario.Visible = false;
            txtRolID.Visible = false;

            // Ventas
            txtNombreProductoVenta.Visible = false;
            txtClienteVenta.Visible = false;
            dtpFechaVenta.Visible = false;
            txtCantidadVenta.Visible = false;
            txtTotalVenta.Visible = false;
            txtUbicacionVenta.Visible = false;

            // Proveedores
            txtNombreProveedor.Visible = false;
            txtPaisProveedor.Visible = false;

            // Compras
            txtProveedorIDCompra.Visible = false;
            dtpFechaCompra.Visible = false;
            txtTotalCompra.Visible = false;

            // ID común
            lblidselec.Visible = false;
        }

        private void OcultarTodosLosControle()
        {
            // Clientes
            lblNombre.Visible = false;
            lblPais.Visible = false;
            txtNombreCliente.Visible = false;
            txtPaisCliente.Visible = false;

            // Productos
            lblNombreProducto.Visible = false;
            lblTipoCliente.Visible = false;
            lblid.Visible = false;
            lblUbicacionID.Visible = false;
            lbldepartamento.Visible = false;
            txtNombreProducto.Visible = false;
            txtCategoria.Visible = false;
            txtProveedorID.Visible = false;
            txtPrecio.Visible = false;
            txtPrecioCost.Visible = false;

            // Usuarios
            lblNombreUsuario.Visible = false;
            lblContraUsuario.Visible = false;
            lblRolID.Visible = false;
            txtNombreUsuario.Visible = false;
            txtContraUsuario.Visible = false;
            txtRolID.Visible = false;

            // Ventas
            lblProductoVenta.Visible = false;
            lblClienteVenta.Visible = false;
            lblFechaVenta.Visible = false;
            lblCantidadVenta.Visible = false;
            lblTotalVenta.Visible = false;
            lblUbicacionVenta.Visible = false;
            txtNombreProductoVenta.Visible = false;
            txtClienteVenta.Visible = false;
            dtpFechaVenta.Visible = false;
            txtCantidadVenta.Visible = false;
            txtTotalVenta.Visible = false;
            txtUbicacionVenta.Visible = false;

            // Proveedores
            lblNombreProveedor.Visible = false;
            lblPaisProveedor.Visible = false;
            txtNombreProveedor.Visible = false;
            txtPaisProveedor.Visible = false;

            // Compras
            lblProveedorIDCompra.Visible = false;
            lblFechaCompra.Visible = false;
            lblTotalCompra.Visible = false;
            txtProveedorIDCompra.Visible = false;
            dtpFechaCompra.Visible = false;
            txtTotalCompra.Visible = false;

            // ID común
            lblidselec.Visible = false;
        }


        private void MostrarControlesClientes()
        {
            txtNombreCliente.Visible = true;
            txtPaisCliente.Visible = true;
            lblidselec.Visible = true;
        }

        private void MostrarControlesProductos()
        {
            txtNombreProducto.Visible = true;
            txtCategoria.Visible = true;
            txtProveedorID.Visible = true;
            txtPrecio.Visible = true;
            txtPrecioCost.Visible = true;
            lblidselec.Visible = true;
        }

        private void MostrarControlesUsuarios()
        {
            txtNombreUsuario.Visible = true;
            txtContraUsuario.Visible = true;
            txtRolID.Visible = true;
            lblidselec.Visible = true;
        }

        private void MostrarControlesVentas()
        {
            txtNombreProductoVenta.Visible = true;
            txtClienteVenta.Visible = true;
            dtpFechaVenta.Visible = true;
            txtCantidadVenta.Visible = true;
            txtTotalVenta.Visible = true;
            txtUbicacionVenta.Visible = true;
            lblidselec.Visible = true;
        }

        private void MostrarControlesProveedores()
        {
            txtNombreProveedor.Visible = true;
            txtPaisProveedor.Visible = true;
            lblidselec.Visible = true;
        }

        private void MostrarControlesCompras()
        {
            txtProveedorIDCompra.Visible = true;
            dtpFechaCompra.Visible = true;
            txtTotalCompra.Visible = true;
            lblidselec.Visible = true;
        }

        private void MostrarControlesCliente()
        {
            lblNombre.Visible = true;
            lblPais.Visible = true;
            txtNombreCliente.Visible = true;
            txtPaisCliente.Visible = true;
            lblidselec.Visible = true;
        }




        private void btnguardar_Click(object sender, EventArgs e)
        {

            ConexionBD conexion = new ConexionBD();

            try
            {
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    // Validación previa del ID
                    if (string.IsNullOrWhiteSpace(lblidselec.Text))
                    {
                        MessageBox.Show("No se ha detectado el ID del registro a editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    MySqlCommand command = null;

                    switch (TablaActiva)
                    {
                        case "clientes":
                            command = new MySqlCommand("UPDATE clientes SET nombre = @Nombre, pais = @Pais WHERE id = @ID", connection);
                            command.Parameters.AddWithValue("@Nombre", txtNombreCliente.Text);
                            command.Parameters.AddWithValue("@Pais", txtPaisCliente.Text);
                            command.Parameters.AddWithValue("@ID", lblidselec.Text);
                            break;

                        case "productos":
                            command = new MySqlCommand(@"UPDATE productos SET nombre = @Nombre, categoria_id = @Categoria, proveedor_id = @Proveedor,
                                            precio = @Precio, precio_costo = @PrecioCosto WHERE id = @ID", connection);
                            command.Parameters.AddWithValue("@Nombre", txtNombreProducto.Text);
                            command.Parameters.AddWithValue("@Categoria", txtCategoria.Text);
                            command.Parameters.AddWithValue("@Proveedor", txtProveedorID.Text);
                            command.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                            command.Parameters.AddWithValue("@PrecioCosto", txtPrecioCost.Text);
                            command.Parameters.AddWithValue("@ID", lblidselec.Text);
                            break;

                        case "usuarios":
                            command = new MySqlCommand("UPDATE usuarios SET nombre = @Nombre, contra = @Contra, id_rol = @Rol WHERE id = @ID", connection);
                            command.Parameters.AddWithValue("@Nombre", txtNombreUsuario.Text);
                            command.Parameters.AddWithValue("@Contra", txtContraUsuario.Text);
                            command.Parameters.AddWithValue("@Rol", txtRolID.Text);
                            command.Parameters.AddWithValue("@ID", lblidselec.Text);
                            break;

                        case "ventas":
                            command = new MySqlCommand(@"UPDATE ventas SET nombre_producto = @Producto, cliente = @Cliente, fecha = @Fecha,
                                            cantidad = @Cantidad, total = @Total, ubicacion = @Ubicacion WHERE id = @ID", connection);
                            command.Parameters.AddWithValue("@Producto", txtNombreProductoVenta.Text);
                            command.Parameters.AddWithValue("@Cliente", txtClienteVenta.Text);
                            command.Parameters.AddWithValue("@Fecha", dtpFechaVenta.Value);
                            command.Parameters.AddWithValue("@Cantidad", txtCantidadVenta.Text);
                            command.Parameters.AddWithValue("@Total", txtTotalVenta.Text);
                            command.Parameters.AddWithValue("@Ubicacion", txtUbicacionVenta.Text);
                            command.Parameters.AddWithValue("@ID", lblidselec.Text);
                            break;

                        case "proveedores":
                            command = new MySqlCommand("UPDATE proveedores SET nombre = @Nombre, pais = @Pais WHERE id = @ID", connection);
                            command.Parameters.AddWithValue("@Nombre", txtNombreProveedor.Text);
                            command.Parameters.AddWithValue("@Pais", txtPaisProveedor.Text);
                            command.Parameters.AddWithValue("@ID", lblidselec.Text);
                            break;

                        case "compras":
                            command = new MySqlCommand("UPDATE compras SET proveedor_id = @ProveedorID, fecha = @Fecha, total = @Total WHERE id = @ID", connection);
                            command.Parameters.AddWithValue("@ProveedorID", txtProveedorIDCompra.Text);
                            command.Parameters.AddWithValue("@Fecha", dtpFechaCompra.Value);
                            command.Parameters.AddWithValue("@Total", txtTotalCompra.Text);
                            command.Parameters.AddWithValue("@ID", lblidselec.Text);
                            break;

                        default:
                            MessageBox.Show("No se ha seleccionado una tabla válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    command.ExecuteNonQuery();
                    MessageBox.Show("Registro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblRolID_Click(object sender, EventArgs e)
        {

        }
    }
}
