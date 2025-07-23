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
using MySql.Data.MySqlClient;


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
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    // Lista de tablas válidas en tu base actual
                    var tablas = new Dictionary<CheckBox, string>
        {
              { checkBox1, "clientes" },
              { checkBox2, "proveedores" },
              { checkBox3, "productos" },
              { checkBox4, "compras" },
              { checkBox5, "usuarios" },
              { checkBox6, "ventas" },
        };

                    foreach (var item in tablas)
                    {
                        if (item.Key.Checked)
                        {
                            string query = $"SELECT * FROM {item.Value}";

                            using (MySqlCommand command = new MySqlCommand(query, connection))
                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                            {
                                DataTable tempTable = new DataTable();
                                adapter.Fill(tempTable);

                                tempTable.Columns.Add("Tabla", typeof(string));
                                foreach (DataRow row in tempTable.Rows)
                                {
                                    row["Tabla"] = item.Value;
                                }

                                combinedTable.Merge(tempTable);
                            }
                        }
                    }

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

            cmbTipoGrafico.Items.AddRange(new string[]
{
    "Ventas por producto",
    "Compras por proveedor",
    "Clientes por país",
    "Productos por categoría",
    "Usuarios por rol"
});
            cmbTipoGrafico.SelectedIndex = 0; 
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

                string idSeleccionado = fila.Cells["id"].Value.ToString();

                // Crear instancia del formulario de edición
                edit editarForm = new edit();

                // Pasar el ID como propiedad pública
                editarForm.IDSeleccionado = idSeleccionado;

                // Mostrar el formulario de edición
                editarForm.Show();
            }
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
            Agreg formAgregar = new Agreg();

            // Determinar la tabla activa según los checkbox válidos
            if (checkBox1.Checked)
            {
                formAgregar.TablaActiva = "clientes";
                formAgregar.Text = "Agregar Nuevo Registro a Clientes";
            }
            else if (checkBox2.Checked)
            {
                formAgregar.TablaActiva = "proveedores";
                formAgregar.Text = "Agregar Nuevo Registro a Proveedores";
            }
            else if (checkBox3.Checked)
            {
                formAgregar.TablaActiva = "productos";
                formAgregar.Text = "Agregar Nuevo Registro a Productos";
            }
            else if (checkBox4.Checked)
            {
                formAgregar.TablaActiva = "compras";
                formAgregar.Text = "Agregar Nuevo Registro a Compras";
            }
            else if (checkBox5.Checked)
            {
                formAgregar.TablaActiva = "usuarios";
                formAgregar.Text = "Agregar Nuevo Registro a Usuarios";
            }
            else if (checkBox6.Checked)
            {
                formAgregar.TablaActiva = "ventas";
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
                CargarTablas(); // Refresca la vista del DataGridView
            }

        }

        private bool TieneDependencias(MySqlConnection conexion, string tablaPadre, string campoPadreID, object valorID)
        {
            var dependencias = new Dictionary<string, string>();

            // Relación entre tabla padre → tabla hija (y columna foránea en hija)
            switch (tablaPadre)
            {
                case "Proveedores":
                    dependencias.Add("Compras", "proveedor_id");
                    break;

                case "Clientes":
                    dependencias.Add("Ventas", "cliente");
                    break;

                case "Productos":
                    dependencias.Add("Ventas", "id_producto");
                    dependencias.Add("Compras", "id_producto");
                    break;

                    // Podés agregar más si querés controlar otras dependencias
            }

            foreach (var dependencia in dependencias)
            {
                string query = $"SELECT COUNT(*) FROM {dependencia.Key} WHERE {dependencia.Value} = @ID";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID", valorID);
                    var resultado = Convert.ToInt32(cmd.ExecuteScalar());

                    if (resultado > 0)
                        return true; // Tiene dependencias
                }
            }

            return false; // No tiene dependencias
        }

        private Dictionary<string, int> ObtenerResumenDependencias(MySqlConnection conexion, string tablaPadre, string campoID, object valorID)
        {
            var dependencias = new Dictionary<string, List<(string tablaHija, string campoForaneo)>>()
    {
        { "Proveedores", new List<(string, string)> { ("Compras", "proveedor_id") } },
        { "Clientes", new List<(string, string)> { ("Ventas", "cliente") } },
        { "Productos", new List<(string, string)> { ("Compras", "id_producto"), ("Ventas", "id_producto") } }
    };

            var resumen = new Dictionary<string, int>();

            if (!dependencias.ContainsKey(tablaPadre)) return resumen;

            foreach (var (tablaHija, campoForaneo) in dependencias[tablaPadre])
            {
                string query = $"SELECT COUNT(*) FROM {tablaHija} WHERE {campoForaneo} = @ID";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ID", valorID);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                        resumen.Add(tablaHija, count);
                }
            }

            return resumen;
        }



            private void button3_Click(object sender, EventArgs e)
        {
            var tablas = new Dictionary<CheckBox, string>
    {
        { checkBox1, "Clientes" },
        { checkBox2, "Proveedores" },
        { checkBox3, "Productos" },
        { checkBox4, "Compras" },
        { checkBox5, "Usuarios" },
        { checkBox6, "Ventas" }
    };

            var seleccionadas = tablas.Where(t => t.Key.Checked).Select(t => t.Value).ToList();

            if (seleccionadas.Count == 0)
            {
                MessageBox.Show("Selecciona una tabla para eliminar registros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (seleccionadas.Count > 1)
            {
                MessageBox.Show("Solo se puede eliminar registros de UNA tabla a la vez. Desmarca las demás.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tabla = seleccionadas[0];
            string campoID = "id"; // ← campo único usado en todas las tablas

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un registro para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!dataGridView1.Columns.Contains(campoID))
            {
                MessageBox.Show($"La columna '{campoID}' no está presente en los datos actuales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            object idValue = dataGridView1.CurrentRow.Cells[campoID].Value;

            if (idValue == null || string.IsNullOrEmpty(idValue.ToString()))
            {
                MessageBox.Show("El ID del registro está vacío o nulo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConexionBD conexion = new ConexionBD();

            try
            {
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    // Verificar dependencias antes de eliminar
                    var resumen = ObtenerResumenDependencias(connection, tabla, campoID, idValue);

                    if (resumen.Any())
                    {
                        string detalle = string.Join("\n", resumen.Select(r => $"{r.Key}: {r.Value} registro(s) relacionado(s)"));
                        MessageBox.Show($"Este registro está vinculado con:\n{detalle}\n\nDebes eliminar esos datos primero.", "Relaciones activas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult confirmacion = MessageBox.Show($"¿Seguro que deseas eliminar el registro de {tabla}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmacion != DialogResult.Yes) return;

                    string query = $"DELETE FROM {tabla} WHERE {campoID} = @ID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", idValue);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarTablas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedTable()
        {
            if (checkBox1.Checked) return "clientes";
            if (checkBox2.Checked) return "proveedores";
            if (checkBox3.Checked) return "productos";
            if (checkBox4.Checked) return "compras";
            if (checkBox5.Checked) return "usuarios";
            if (checkBox6.Checked) return "ventas";
            return "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que hay una fila seleccionada en el DataGridView
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Por favor, seleccione un registro del DataGridView para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Determinar la tabla activa
                string tablaActiva = GetSelectedTable(); // Este método debe retornar el nombre correcto: "clientes", "productos", etc.
                if (string.IsNullOrEmpty(tablaActiva))
                {
                    MessageBox.Show("No se ha seleccionado una tabla válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Campo clave (ID) estándar para todas las tablas
                string campoID = "id";

                // Validar que el campo exista en el DataGridView
                if (!dataGridView1.Columns.Contains(campoID))
                {
                    MessageBox.Show($"La columna '{campoID}' no está presente en el DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                object idValue = dataGridView1.CurrentRow.Cells[campoID].Value;

                if (idValue == null || string.IsNullOrWhiteSpace(idValue.ToString()))
                {
                    MessageBox.Show("El registro seleccionado no tiene un ID válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Abrir el formulario de edición y pasarle los datos
                edit formEditar = new edit
                {
                    TablaActiva = tablaActiva,
                    IDSeleccionado = idValue.ToString()
                };

                formEditar.StartPosition = FormStartPosition.CenterParent; // Que se abra centrado
                formEditar.ShowDialog(); // Espera a que el formulario se cierre

                // Refrescar el DataGridView
                CargarTablas(); // Método que ya tienes para recargar datos
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al abrir el formulario de edición: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var tablas = new Dictionary<CheckBox, string>
{
    { checkBox1, "clientes" },
    { checkBox2, "proveedores" },
    { checkBox3, "productos" },
    { checkBox4, "compras" },
    { checkBox5, "usuarios" },
    { checkBox6, "ventas" }
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
                    case "ventas":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Ventas por producto",
                            Consulta = "SELECT nombre_producto AS Producto, SUM(total) AS TotalVentas FROM ventas GROUP BY nombre_producto",
                            CampoX = "Producto",
                            CampoY = "TotalVentas"
                        });
                        break;

                    case "productos":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Productos por categoría",
                            Consulta = "SELECT categoria_id AS Categoria, COUNT(*) AS Total FROM productos GROUP BY categoria_id",
                            CampoX = "Categoria",
                            CampoY = "Total"
                        });
                        break;

                    case "clientes":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Clientes y total de compras",
                            Consulta = "SELECT cliente AS ClienteID, SUM(total) AS TotalCompras FROM ventas GROUP BY cliente",
                            CampoX = "ClienteID",
                            CampoY = "TotalCompras"
                        });
                        break;

                    case "usuarios":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Usuarios por rol",
                            Consulta = "SELECT id_rol AS RolID, COUNT(*) AS TotalUsuarios FROM usuarios GROUP BY id_rol",
                            CampoX = "RolID",
                            CampoY = "TotalUsuarios"
                        });
                        break;

                    case "roles":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Cantidad de roles registrados",
                            Consulta = "SELECT nombre_rol AS Rol, COUNT(*) AS Total FROM roles GROUP BY nombre_rol",
                            CampoX = "Rol",
                            CampoY = "Total"
                        });
                        break;

                    case "proveedores":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Proveedores por país",
                            Consulta = "SELECT pais AS Pais, COUNT(*) AS Total FROM proveedores GROUP BY pais",
                            CampoX = "Pais",
                            CampoY = "Total"
                        });
                        break;

                    case "compras":
                        series.Add(new SerieDatos
                        {
                            NombreSerie = "Compras por proveedor",
                            Consulta = "SELECT proveedor_id AS Proveedor, SUM(total) AS TotalCompras FROM compras GROUP BY proveedor_id",
                            CampoX = "Proveedor",
                            CampoY = "TotalCompras"
                        });
                        break;
                }
            }

            // Mostrar el formulario y pasar los datos
            if (series.Count > 0)
            {
                FormGraficos grafi = new FormGraficos();
                grafi.Conectar(series);
                grafi.Show();
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
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    string query = @"
SELECT 
    c.Nombre AS Cliente,
    COUNT(v.id_ventas) AS Cantidad_Ventas,
    SUM(v.total) AS Total_Ventas
FROM 
    clientes c
INNER JOIN 
    ventas v ON c.Nombre = v.nombre_producto  -- Este JOIN puede ajustarse según lo que realmente conecta
GROUP BY 
    c.Nombre";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(tablaResumen);
                        dataGridView1.DataSource = tablaResumen;
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
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    string query = @"
SELECT 
    c.Nombre AS Cliente,
    v.nombre_producto AS Producto,
    SUM(v.total) AS Total_Comprado
FROM 
    ventas v
INNER JOIN 
    clientes c ON c.Nombre = (SELECT u.nombre FROM usuarios u WHERE u.id_usuario = v.cliente)
GROUP BY 
    c.Nombre, v.nombre_producto
ORDER BY 
    c.Nombre, Total_Comprado DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(tablaResumen);
                        dataGridView1.DataSource = tablaResumen;
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
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    string query = @"
SELECT 
    u.nombre AS Cliente,
    p.nombre_pais AS Ubicacion,
    COUNT(v.id_ventas) AS Cantidad_Ventas,
    SUM(v.total) AS Total_Ventas
FROM 
    ventas v
INNER JOIN 
    usuarios u ON v.cliente = u.id_usuario
INNER JOIN 
    pais p ON v.ubicacion = p.id_pais
GROUP BY 
    u.nombre, p.nombre_pais
ORDER BY 
    u.nombre, Total_Ventas DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(tablaResumen);
                        dataGridView1.DataSource = tablaResumen;
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
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    string query = @"
SELECT 
    p.nombre AS Producto,
    SUM(v.cantidad) AS Cantidad_Vendida,
    SUM(v.total) AS Total_Ventas
FROM 
    ventas v
INNER JOIN 
    productos p ON v.nombre_producto = p.nombre
GROUP BY 
    p.nombre
ORDER BY 
    Total_Ventas DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(tablaResumen);
                        dataGridView1.DataSource = tablaResumen;
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
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();

                    string query = @"
SELECT 
    p.nombre_pais AS Ubicacion,
    SUM(v.total) AS Total_Ventas
FROM 
    ventas v
INNER JOIN 
    pais p ON v.ubicacion = p.id_pais
GROUP BY 
    p.nombre_pais
ORDER BY 
    Total_Ventas DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(tablaResumen);
                        dataGridView1.DataSource = tablaResumen;
                    }
                }
            }
            catch (Exception ex)
            {
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

        private void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new ConexionBD();

            var tablas = new Dictionary<CheckBox, string>
    {
        { checkBox1, "Clientes" },
        { checkBox2, "Proveedores" },
        { checkBox3, "Productos" },
        { checkBox4, "Compras" },
        { checkBox5, "Usuarios" },
        { checkBox6, "Ventas" }
    };

            var seleccionadas = tablas.Where(t => t.Key.Checked).Select(t => t.Value).ToList();

            if (seleccionadas.Count == 0)
            {
                MessageBox.Show("Selecciona al menos una tabla para aplicar filtros.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection connection = conexion.ObtenerConexion())
                {
                    connection.Open();
                    DataTable datosFiltrados = new DataTable();

                    foreach (string tabla in seleccionadas)
                    {
                        string query = $"SELECT * FROM {tabla}";

                        // 🔎 Filtros personalizados por tabla (puedes modificarlos según tu lógica)
                        if (tabla == "Clientes")
                            query += " WHERE pais = 'Honduras'";

                        if (tabla == "Proveedores")
                            query += " WHERE pais = 'Honduras'";

                        if (tabla == "Productos")
                            query += " WHERE precio > 100";

                        if (tabla == "Usuarios")
                            query += " WHERE id_rol = 1";

                        if (tabla == "Compras")
                            query += " WHERE total > 500";

                        if (tabla == "Ventas")
                            query += " WHERE total > 200";

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                        {
                            DataTable resultado = new DataTable();
                            adapter.Fill(resultado);

                            // 🧩 Añadir columna de origen para saber de dónde viene cada fila
                            resultado.Columns.Add("OrigenTabla");
                            foreach (DataRow row in resultado.Rows)
                            {
                                row["OrigenTabla"] = tabla;
                            }

                            datosFiltrados.Merge(resultado);
                        }
                    }

                    dgvFiltrado.DataSource = datosFiltrados;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar filtros: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;

            dgvFiltrado.DataSource = null;
        }

        private void btnGraficarDesdeCombo_Click(object sender, EventArgs e)
        {
            string tipoGrafico = cmbTipoGrafico.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(tipoGrafico))
            {
                MessageBox.Show("Selecciona un tipo de gráfico desde la lista.");
                return;
            }

            if (dgvFiltrado.DataSource == null)
            {
                MessageBox.Show("No hay datos filtrados para graficar.");
                return;
            }

            DataTable datosFiltrados = (DataTable)dgvFiltrado.DataSource;

            // Crear y mostrar el formulario de gráfico
            FormGraficoFiltrado graficoForm = new FormGraficoFiltrado();
            graficoForm.MostrarGrafico(datosFiltrados, tipoGrafico);
            graficoForm.Show();
        }

        private void cmbTipoGrafico_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
