using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using StockSells.forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace StockSells
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void CargarTablas()
        {
            // Cadena de conexión a tu base de datos SQL Server
            ConexionBD conexion = new ConexionBD();

            // Crear un DataTable para combinar datos
            DataTable combinedTable = new DataTable();

            try
            {
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open(); // Abrir la conexión

                    // Lista de tablas y sus checkboxes
                    var tablas = new Dictionary<CheckBox, string>
            {
                { checkBox1, "Clientes" },
                { checkBox2, "FactoresDeCostos" },
                { checkBox3, "Productos" },
                { checkBox4, "Ubicaciones" },
                { checkBox5, "Usuarios" },
                { checkBox6, "Ventas" }
            };

                    foreach (var item in tablas)
                    {
                        // Verificar si el checkbox está activo
                        if (item.Key.Checked)
                        {
                            string query = $"SELECT * FROM {item.Value}"; // Consulta para la tabla

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                {
                                    DataTable tempTable = new DataTable();
                                    adapter.Fill(tempTable);

                                    // Agregar columna para identificar la tabla (opcional)
                                    tempTable.Columns.Add("Tabla", typeof(string));
                                    foreach (DataRow row in tempTable.Rows)
                                    {
                                        row["Tabla"] = item.Value;
                                    }

                                    // Combinar con el DataTable principal
                                    combinedTable.Merge(tempTable);
                                }
                            }
                        }
                    }

                    // Mostrar los datos en el DataGridView
                    dataGridView1.DataSource = combinedTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Menu_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarVista();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CargarTablas();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CargarTablas();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CargarTablas();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            CargarTablas();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarVista();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario FormAgregar
            Agreg formAgregar = new Agreg();

            // Determinar qué checkbox está activo y pasar el nombre de la tabla al formulario
            if (checkBox1.Checked)
            {
                formAgregar.TablaActiva = "Clientes";
                formAgregar.Text = "Agregar Nuevo Registro a Clientes";
            }
            else if (checkBox2.Checked)
            {
                formAgregar.TablaActiva = "FactoresDeCostos";
                formAgregar.Text = "Agregar Nuevo Registro a FactoresDeCostos";
            }
            else if (checkBox3.Checked)
            {
                formAgregar.TablaActiva = "Productos";
                formAgregar.Text = "Agregar Nuevo Registro a Productos";
            }
            else if (checkBox4.Checked)
            {
                formAgregar.TablaActiva = "Ubicaciones";
                formAgregar.Text = "Agregar Nuevo Registro a Ubicaciones";
            }
            else if (checkBox5.Checked)
            {
                formAgregar.TablaActiva = "Usuarios";
                formAgregar.Text = "Agregar Nuevo Registro a Usuarios";
            }
            else if (checkBox6.Checked)
            {
                formAgregar.TablaActiva = "Ventas";
                formAgregar.Text = "Agregar Nuevo Registro a Ventas";
            }
            else
            {
                MessageBox.Show("Seleccione una tabla para agregar datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mostrar el formulario como cuadro de diálogo
            if (formAgregar.ShowDialog() == DialogResult.OK)
            {
                // Recargar el DataGridView después de agregar
                CargarTablas();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();

            try
            {
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    // Validar que haya una fila seleccionada en el DataGridView
                    if (dataGridView1.CurrentRow == null)
                    {
                        MessageBox.Show("Por favor, seleccione un registro del DataGridView para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    
                    if (!dataGridView1.Columns.Contains("ID"))
                    {
                        MessageBox.Show("La columna 'ID' no está presente en los datos actuales del DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

              
                    object idValue = dataGridView1.CurrentRow.Cells["ID"].Value;

                    if (idValue == null || string.IsNullOrEmpty(idValue.ToString()))
                    {
                        MessageBox.Show("El valor del ID seleccionado está vacío o es nulo. Por favor, seleccione un registro válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // El ID es un valor alfanumérico, así que lo tratamos como texto
                    string id = idValue.ToString();

                    
                    string query = "";
                    if (checkBox1.Checked) query = "DELETE FROM Clientes WHERE ID = @ID";
                    else if (checkBox2.Checked) query = "DELETE FROM FactoresDeCostos WHERE ID = @ID";
                    else if (checkBox3.Checked) query = "DELETE FROM Productos WHERE ID = @ID";
                    else if (checkBox4.Checked) query = "DELETE FROM Ubicaciones WHERE ID = @ID";
                    else if (checkBox5.Checked) query = "DELETE FROM Usuarios WHERE ID = @ID";
                    else if (checkBox6.Checked) query = "DELETE FROM Ventas WHERE ID = @ID";
                    else
                    {
                        MessageBox.Show("Seleccione una tabla para eliminar datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Ejecutar el comando DELETE
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();

                    
                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                   
                    CargarTablas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedTable()
        {
            if (checkBox1.Checked) return "Clientes";
            if (checkBox2.Checked) return "FactoresDeCostos";
            if (checkBox3.Checked) return "Productos";
            if (checkBox4.Checked) return "Ubicaciones";
            if (checkBox5.Checked) return "Usuarios";
            if (checkBox6.Checked) return "Ventas";
            return "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Validar que haya una fila seleccionada en el DataGridView
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un registro del DataGridView para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el valor del ID del registro seleccionado
            object idValue = dataGridView1.CurrentRow.Cells["ID"].Value;

            if (idValue == null || string.IsNullOrEmpty(idValue.ToString()))
            {
                MessageBox.Show("El registro seleccionado no tiene un ID válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

         
            string tablaActiva = GetSelectedTable();
            if (string.IsNullOrEmpty(tablaActiva))
            {
                MessageBox.Show("No se ha seleccionado una tabla válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            edit formEditar = new edit();
            formEditar.TablaActiva = tablaActiva;
            formEditar.ID = idValue.ToString();
            formEditar.ShowDialog();

            
            CargarTablas();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para generar el reporte.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Ruta de carpeta Descargas
                string rutaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

                // Código automático con fecha y hora
                string codigo = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                // Nombre del archivo
                string nombreArchivo = $"ReporteTablas_{codigo}.pdf";

                // Ruta completa
                string rutaArchivo = Path.Combine(rutaDescargas, nombreArchivo);

                // Crear documento PDF
                Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                // Título
                Paragraph titulo = new Paragraph("REPORTE DE TABLAS SELECCIONADAS", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(new Paragraph(" "));

                // Crear tabla PDF
                PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                pdfTable.WidthPercentage = 100;

                // Encabezados
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pdfTable.AddCell(cell);
                }

                // Datos
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            pdfTable.AddCell(cell.Value?.ToString() ?? "");
                        }
                    }
                }

                doc.Add(pdfTable);
                doc.Close();

                // Mostrar mensaje de éxito
                MessageBox.Show($"Reporte generado en:\n{rutaArchivo}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir PDF sin que se cierre
                if (File.Exists(rutaArchivo))
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = rutaArchivo,
                        UseShellExecute = true // Abre con el visor por defecto (sin errores)
                    };
                    Process.Start(psi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //funcion para cruzar datos de las tablas ventas y clientes
        private void CargarReporteVentasPorCliente()
        {
            ConexionBD conexion = new ConexionBD();
            DataTable tablaResumen = new DataTable();

            try
            {
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    string query = @"
                SELECT 
                    c.ID AS [ID Cliente],
                    c.Nombre AS [Nombre Cliente],
                    COUNT(v.ID) AS [Cantidad de Ventas],
                    SUM(v.Total) AS [Importe Total]
                FROM 
                    Clientes c
                INNER JOIN 
                    Ventas v ON c.ID = v.Cliente
                GROUP BY 
                    c.ID, c.Nombre";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(tablaResumen);
                            dataGridView1.DataSource = tablaResumen;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte combinado de ventas por cliente:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarVista()
        {
            // Si ambos checkboxes están activos: Clientes y Ventas
            if (checkBox1.Checked && checkBox6.Checked)
            {
                CargarReporteVentasPorCliente(); // Muestra resumen cruzado
            }
            else
            {
                CargarTablas(); // Muestra las tablas seleccionadas de forma individual
            }
        }
    }
}
