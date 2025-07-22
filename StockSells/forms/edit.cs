
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
        public string ID { get; set; }

        private void edit_Load(object sender, EventArgs e)
        {

            ConexionBD conexion = new ConexionBD();

            try
            {
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    // Mapear tabla y campo clave real
                    string campoID = "";

                    if (TablaActiva == "clientes") campoID = "Nombre";
                    else if (TablaActiva == "productos") campoID = "id_producto";
                    else if (TablaActiva == "usuarios") campoID = "id_usuario";
                    else if (TablaActiva == "ventas") campoID = "id_ventas";
                    else
                    {
                        MessageBox.Show("La tabla seleccionada no está disponible en la base actual.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string query = $"SELECT * FROM {TablaActiva} WHERE {campoID} = @ID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (TablaActiva == "clientes")
                                {
                                    txtNombre.Text = reader["Nombre"].ToString();
                                    txtTipoCliente.Text = reader["cliente_id"].ToString();
                                    txtPais.Text = reader["pais"].ToString();
                                    txtDepartamentoID.Text = reader["id_departamento"].ToString();
                                }
                                else if (TablaActiva == "productos")
                                {
                                    txtProductoID.Text = reader["id_producto"].ToString();
                                    txtNombre.Text = reader["nombre"].ToString();
                                    txtCategoriaID.Text = reader["categoria_id"].ToString();
                                    txtProveedorID.Text = reader["proveedor_id"].ToString();
                                    txtPrecio.Text = reader["Precio"].ToString();
                                    txtPrecioCosto.Text = reader["precio_costo"].ToString();
                                }
                                else if (TablaActiva == "usuarios")
                                {
                                    lblidselec.Text = reader["id_usuario"].ToString();
                                    txtNombre.Text = reader["nombre"].ToString();
                                    txtContra.Text = reader["contra"].ToString();
                                    txtRolID.Text = reader["id_rol"].ToString();
                                }
                                else if (TablaActiva == "ventas")
                                {
                                    lblidselec.Text = reader["id_ventas"].ToString();
                                    txtCliente.Text = reader["cliente"].ToString();
                                    txtProducto.Text = reader["nombre_producto"].ToString();
                                    txtCantidad.Text = reader["cantidad"].ToString();
                                    dtpFecha.Value = Convert.ToDateTime(reader["fecha"]);
                                    txtTotal.Text = reader["total"].ToString();
                                    txtUbicacionID.Text = reader["ubicacion"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar el registro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    string query = "";
                    MySqlCommand command = new MySqlCommand();

                    if (TablaActiva == "clientes")
                    {
                        query = "UPDATE clientes SET Nombre = @Nombre, cliente_id = @TipoCliente, pais = @Pais, id_departamento = @Departamento WHERE Nombre = @ID";
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@TipoCliente", txtTipoCliente.Text);
                        command.Parameters.AddWithValue("@Pais", txtPais.Text);
                        command.Parameters.AddWithValue("@Departamento", txtDepartamentoID.Text);
                    }
                    else if (TablaActiva == "productos")
                    {
                        query = "UPDATE productos SET nombre = @Nombre, categoria_id = @Categoria, Precio = @Precio, precio_costo = @PrecioCosto WHERE id_producto = @ID";
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Categoria", txtCategoriaID.Text);
                        command.Parameters.AddWithValue("@Precio", txtPrecio.Text);
                        command.Parameters.AddWithValue("@PrecioCosto", txtPrecioCosto.Text);
                    }
                    else if (TablaActiva == "usuarios")
                    {
                        query = "UPDATE usuarios SET nombre = @Nombre, contra = @Contra, id_rol = @Rol WHERE id_usuario = @ID";
                        command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        command.Parameters.AddWithValue("@Contra", txtContra.Text);
                        command.Parameters.AddWithValue("@Rol", txtRolID.Text);
                    }
                    else if (TablaActiva == "ventas")
                    {
                        query = @"UPDATE ventas SET nombre_producto = @Producto, cliente = @Cliente, fecha = @Fecha, cantidad = @Cantidad, total = @Total, ubicacion = @Ubicacion 
                      WHERE id_ventas = @ID";
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

                    command.Parameters.AddWithValue("@ID", lblidselec.Text);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registro actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se realizó ninguna actualización. Verifique el ID o los datos ingresados.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show($"Error de MySQL: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
