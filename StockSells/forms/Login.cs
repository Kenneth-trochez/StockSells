using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockSells
{
    public partial class Login : Form
    {


     

        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Viewtable_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Cadena de conexión a tu base de datos SQL Server
            ConexionBD conexion = new ConexionBD();

            string usuario = txtusuario.Text;
            string contraseña = txtpassword.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Usar el método ObtenerConexion() para obtener una conexión válida
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open(); // Abrir la conexión

                    // Consulta para verificar usuario y obtener su rol
                    string query = "SELECT Rol FROM Usuarios WHERE Nombre = @usuario AND Contra = @contraseña";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@usuario", usuario);
                        command.Parameters.AddWithValue("@contraseña", contraseña);

                        // Obtener el rol del usuario
                        object rol = command.ExecuteScalar();

                        if (rol != null)
                        {
                            string rolUsuario = rol.ToString(); // Convertir el Rol a string
                            MessageBox.Show("Inicio de sesión exitoso!", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Mostrar el formulario principal y aplicar permisos según el rol
                            Menu menuForm = new Menu();
                            menuForm.RolUsuario = rolUsuario; // Pasar el Rol al formulario principal
                            menuForm.Show();
                            this.Hide(); // Ocultar el formulario actual
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private bool mostrarContraseña = false;

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Alternar el estado de la contraseña
            mostrarContraseña = !mostrarContraseña;

            // Mostrar u ocultar la contraseña según el estado actual
            if (mostrarContraseña)
            {
                txtpassword.PasswordChar = '\0'; // Mostrar texto plano
            }
            else
            {
                txtpassword.PasswordChar = '*'; // Ocultar la contraseña
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Cerrar la aplicación completamente
            Application.Exit();
        }
    }
}
