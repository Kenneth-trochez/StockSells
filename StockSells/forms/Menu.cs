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
using System.Windows.Forms.DataVisualization.Charting;

namespace StockSells
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        public string RolUsuario { get; set; }

        public List<Chart> graficosGenerados = new List<Chart>();

        public string TablaActiva { get; set; }

        private void CargarTablas()
        {
            
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
            ConfigurarPermisos(RolUsuario);
        }

        private void ConfigurarPermisos(string rol)
        {
            if (rol == "Admin")
            {
                // Permisos para Administrador: Acceso completo
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
            }
            else if (rol == "Usuario")
            {
                // Permisos para Usuario: Acceso limitado
                button1.Enabled = false;  
                button2.Enabled = false; 
                button3.Enabled = false;
                checkBox5.Enabled = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarVista();
            // Si checkBox5 (Usuarios) está seleccionado, deshabilitar los demás checkboxes
            if (checkBox5.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false; 
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox6.Enabled = false;
            }
            else if (checkBox2.Checked) // Si checkBox2 (FactoresDeCostos) está seleccionado
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                // Si ninguno de los dos está seleccionado, habilitar todos los checkboxes
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CargarTablas();
            // Si checkBox5 (Usuarios) está seleccionado, deshabilitar los demás checkboxes
            if (checkBox5.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox6.Enabled = false;
            }
            else if (checkBox2.Checked) // Si checkBox2 (FactoresDeCostos) está seleccionado
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                // Si ninguno de los dos está seleccionado, habilitar todos los checkboxes
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CargarTablas();
            // Si checkBox5 (Usuarios) está seleccionado, deshabilitar los demás checkboxes
            if (checkBox5.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox6.Enabled = false;
            }
            else if (checkBox2.Checked) // Si checkBox2 (FactoresDeCostos) está seleccionado
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                // Si ninguno de los dos está seleccionado, habilitar todos los checkboxes
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CargarTablas();
            // Si checkBox5 (Usuarios) está seleccionado, deshabilitar los demás checkboxes
            if (checkBox5.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox6.Enabled = false;
            }
            else if (checkBox2.Checked) // Si checkBox2 (FactoresDeCostos) está seleccionado
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                // Si ninguno de los dos está seleccionado, habilitar todos los checkboxes
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            CargarTablas();
            // Si checkBox5 (Usuarios) está seleccionado, deshabilitar los demás checkboxes
            if (checkBox5.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox6.Enabled = false;
            }
            else if (checkBox2.Checked) // Si checkBox2 (FactoresDeCostos) está seleccionado
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                // Si ninguno de los dos está seleccionado, habilitar todos los checkboxes
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarVista();
            // Si checkBox5 (Usuarios) está seleccionado, deshabilitar los demás checkboxes
            if (checkBox5.Checked)
            {
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox6.Enabled = false;
            }
            else if (checkBox2.Checked) // Si checkBox2 (FactoresDeCostos) está seleccionado
            {
                checkBox1.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                // Si ninguno de los dos está seleccionado, habilitar todos los checkboxes
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }
        }

        public void ActualizarTablaActiva()
        {
            if (checkBox1.Checked) TablaActiva = "Clientes";
            else if (checkBox2.Checked) TablaActiva = "FactoresDeCostos";
            else if (checkBox3.Checked) TablaActiva = "Productos";
            else if (checkBox4.Checked) TablaActiva = "Ubicaciones";
            else if (checkBox5.Checked) TablaActiva = "Usuarios";
            else if (checkBox6.Checked) TablaActiva = "Ventas";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario FormAgregar
            Agreg formAgregar = new Agreg();

            // Verificar qué checkbox está seleccionado
            if (checkBox1.Checked) formAgregar.TablaActiva = "Clientes";
            else if (checkBox2.Checked) formAgregar.TablaActiva = "FactoresDeCostos";
            else if (checkBox3.Checked) formAgregar.TablaActiva = "Productos";
            else if (checkBox4.Checked) formAgregar.TablaActiva = "Ubicaciones";
            else if (checkBox5.Checked) formAgregar.TablaActiva = "Usuarios";
            else if (checkBox6.Checked) formAgregar.TablaActiva = "Ventas";
            else
            {
                MessageBox.Show("Seleccione una tabla antes de agregar datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
            try
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

                // Obtener la tabla activa seleccionada
                string tablaActiva = GetSelectedTable();
                if (string.IsNullOrEmpty(tablaActiva))
                {
                    MessageBox.Show("No se ha seleccionado una tabla válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Instanciar la clase de conexión
                ConexionBD conexion = new ConexionBD();

                // Probar la conexión antes de continuar
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open(); // Abre la conexión

                    MessageBox.Show($"Conexión exitosa a la base de datos para editar la tabla: {tablaActiva}", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Enviar datos al formulario Editar
                edit formEditar = new edit
                {
                    TablaActiva = tablaActiva,
                    ID = idValue.ToString()
                };

                formEditar.ShowDialog();

                // Recargar los datos en el DataGridView
                CargarTablas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al conectar a la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var tablas = new Dictionary<CheckBox, string>
    {
        { checkBox1, "Clientes" },
        { checkBox2, "FactoresDeCostos" },
        { checkBox3, "Productos" },
        { checkBox4, "Ubicaciones" },
        { checkBox5, "Usuarios" },
        { checkBox6, "Ventas" }
    };

            var seleccionadas = tablas.Where(t => t.Key.Checked).Select(t => t.Value).ToList();

            if (seleccionadas.Count == 0)
            {
                MessageBox.Show("Selecciona al menos una tabla para graficar.");
                return;
            }

            List<SerieDatos> series = new List<SerieDatos>();

            foreach (string tabla in seleccionadas)
            {
                switch (tabla)
                {
                    case "Ventas":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Ventas",
                            Consulta = "SELECT Fecha, Total FROM Ventas",
                            CampoX = "Fecha",
                            CampoY = "Total"
                        });
                        break;

                    case "Productos":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Productos más vendidos por Ciudad",
                            Consulta = "SELECT P.Nombre AS Producto, U.Ciudad AS Ciudad, SUM(V.Cantidad) AS TotalVendido " +
                                       "FROM Ventas V " +
                                       "INNER JOIN Productos P ON V.Producto = P.Nombre " +
                                       "INNER JOIN Ubicaciones U ON V.UbicacionID = U.ID " +
                                       "GROUP BY P.Nombre, U.Ciudad " +
                                       "ORDER BY U.Ciudad, TotalVendido DESC",
                            CampoX = "Producto",
                            CampoY = "TotalVendido"
                        });
                        break;

                    case "Ubicaciones":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Ventas por Ubicación",
                            Consulta = "SELECT U.Ciudad AS Ciudad, SUM(V.Total) AS TotalVentas " +
                                       "FROM Ventas V " +
                                       "INNER JOIN Ubicaciones U ON V.UbicacionID = U.ID " +
                                       "GROUP BY U.Ciudad",
                            CampoX = "Ciudad",
                            CampoY = "TotalVentas"
                        });
                        break;

                    case "Clientes":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Clientes y sus compras",
                            Consulta = "SELECT C.Nombre AS Cliente, SUM(V.Total) AS TotalCompras " +
                                       "FROM Ventas V " +
                                       "INNER JOIN Clientes C ON V.Cliente = C.ID " +
                                       "GROUP BY C.Nombre " +
                                       "ORDER BY TotalCompras DESC",
                            CampoX = "Cliente",
                            CampoY = "TotalCompras"
                        });
                        break;

                      
                }
            }

            // Mostrar el formulario y pasar los datos
            if (series.Count > 0)
            {
                FormGraficos grafi = new FormGraficos();
                grafi.Conectar(series); // Conectar primero
                grafi.Show(); // Luego mostrar
                graficosGenerados.AddRange(grafi.Controls.OfType<Chart>());

            }
            else
            {
                MessageBox.Show("No hay series válidas para graficar.");
            }
            
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
                string rutaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string codigo = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string nombreArchivo = $"ReporteTablas_{codigo}.pdf";
                string rutaArchivo = Path.Combine(rutaDescargas, nombreArchivo);

                // Crear documento PDF
                Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                Paragraph titulo = new Paragraph("REPORTE DE TABLAS SELECCIONADAS", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                titulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(titulo);
                doc.Add(new Paragraph(" "));

                // Crear tabla PDF desde DataGridView
                PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                pdfTable.WidthPercentage = 100;

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    pdfTable.AddCell(cell);
                }

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

                // Buscar si hay un gráfico en otro formulario abierto
                FormGraficos graficoForm = Application.OpenForms.OfType<FormGraficos>().FirstOrDefault();

                foreach (Chart chart in graficosGenerados)
                {
                    string imagenRuta = Path.Combine(Path.GetTempPath(), $"Grafico_{codigo}_{graficosGenerados.IndexOf(chart)}.png");
                    chart.SaveImage(imagenRuta, ChartImageFormat.Png);

                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(imagenRuta);
                    imagen.ScaleToFit(700f, 400f);
                    imagen.Alignment = Element.ALIGN_CENTER;
                    doc.Add(new Paragraph("\n\n"));
                    doc.Add(imagen);
                }

                doc.Close();

                MessageBox.Show($"Reporte generado en:\n{rutaArchivo}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (File.Exists(rutaArchivo))
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = rutaArchivo,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            graficosGenerados.Clear();
        }


        //funcion para cruzar datos de las tablas ventas y clientes
        private void CargarReporteClientesConVentas()
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
                c.Nombre AS [Cliente],
                COUNT(v.ID) AS [Cantidad de Ventas],
                SUM(v.Total) AS [Total Ventas]
            FROM 
                Clientes c
            INNER JOIN 
                Ventas v ON c.ID = v.Cliente
            GROUP BY 
                c.Nombre";

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
                MessageBox.Show("Error al cargar el reporte combinado de clientes con ventas:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReporteClientesPorProductos()
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
                c.Nombre AS [Cliente],
                p.Nombre AS [Producto],
                SUM(v.Total) AS [Total Comprado]
            FROM 
                Ventas v
            INNER JOIN 
                Clientes c ON v.Cliente = c.ID
            INNER JOIN 
                Productos p ON v.Producto = p.Nombre
            GROUP BY 
                c.Nombre, p.Nombre
            ORDER BY 
                c.Nombre, [Total Comprado] DESC";

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
                MessageBox.Show("Error al cargar el reporte combinado de clientes por productos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReporteClientesPorUbicaciones()
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
                c.Nombre AS [Cliente],
                u.Ciudad AS [Ubicación],
                COUNT(v.ID) AS [Cantidad de Ventas],
                SUM(v.Total) AS [Total Ventas]
            FROM 
                Ventas v
            INNER JOIN 
                Clientes c ON v.Cliente = c.ID
            INNER JOIN 
                Ubicaciones u ON v.UbicacionID = u.ID
            GROUP BY 
                c.Nombre, u.Ciudad";

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
                MessageBox.Show("Error al cargar el reporte combinado de clientes por ubicaciones:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReporteVentasPorProductos()
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
                p.Nombre AS [Producto],
                SUM(v.Cantidad) AS [Cantidad Vendida],
                SUM(v.Total) AS [Total Ventas]
            FROM 
                Ventas v
            INNER JOIN 
                Productos p ON v.Producto = p.Nombre
            GROUP BY 
                p.Nombre";

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
                MessageBox.Show("Error al cargar el reporte combinado de ventas por productos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReporteVentasPorUbicaciones()
        {
            ConexionBD conexion = new ConexionBD();
            DataTable tablaResumen = new DataTable();

            try
            {
                using (SqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    // Consulta SQL para combinar Ventas con Ubicaciones
                    string query = @"
            SELECT 
                u.Ciudad AS [Ubicación],
                SUM(v.Total) AS [Total Ventas]
            FROM 
                Ventas v
            INNER JOIN 
                Ubicaciones u ON v.UbicacionID = u.ID
            GROUP BY 
                u.Ciudad";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            // Cargar el resultado en el DataTable
                            adapter.Fill(tablaResumen);

                            // Mostrar los datos en el DataGridView
                            dataGridView1.DataSource = tablaResumen;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show("Error al cargar el reporte combinado de ventas por ubicación:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarVista()
        {
            if (checkBox1.Checked && checkBox6.Checked) // Clientes y Ventas
            {
                CargarReporteClientesConVentas();
            }
            else if (checkBox1.Checked && checkBox3.Checked) // Clientes y Productos
            {
                CargarReporteClientesPorProductos();
            }
            else if (checkBox1.Checked && checkBox4.Checked) // Clientes y Ubicaciones
            {
                CargarReporteClientesPorUbicaciones();
            }
            else if (checkBox3.Checked && checkBox6.Checked) // Ventas y Productos
            {
                CargarReporteVentasPorProductos();
            }
            else if (checkBox4.Checked && checkBox6.Checked) // Ventas y Ubicaciones
            {
                CargarReporteVentasPorUbicaciones();
            }
            else
            {
                CargarTablas(); // Muestra las tablas seleccionadas de forma individual
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Confirmar si el usuario realmente desea cerrar sesión
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro que deseas cerrar sesión?",
                "Confirmar cierre de sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Si el usuario confirma cerrar sesión
            if (resultado == DialogResult.Yes)
            {
                // Abrir el formulario de Login
                Login loginForm = new Login();
                loginForm.Show();

                // Cerrar el formulario principal
                this.Close();
            }
        }
    }
}
