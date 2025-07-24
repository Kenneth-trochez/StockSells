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
using MySqlX.XDevAPI.Relational;

namespace StockSells.forms
{
    public partial class FRMCards : Form
    {
        private int totalRegistrosGlobal = 0;
        private decimal sumaGlobal = 0;
        private int cantidadParaPromedio = 0;
        private List<string> tablasSeleccionadas;
        public DataTable Datos { get; set; }

        public FRMCards(List<string> tablas)
        {
            InitializeComponent();
            tablasSeleccionadas = tablas;
        }

        private void LoadCardsFromMultipleTables(List<string> tablas)
        {
            flowLayoutPanel1.Controls.Clear();

            string connectionString = "Server=127.0.0.1;Database=sistema_ventas;User ID=root;Password=mdot;Port=3306;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                foreach (string tableName in tablas)
                {
                    string query = $"SELECT * FROM {tableName}";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Crear cards para cada fila
                    foreach (DataRow row in dt.Rows)
                    {
                        Panel card = new Panel
                        {
                            BorderStyle = BorderStyle.FixedSingle,
                            Width = 250,
                            Height = 100,
                            Margin = new Padding(10)
                        };

                        Label label = new Label
                        {
                            AutoSize = false,
                            Dock = DockStyle.Fill,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Text = string.Join(Environment.NewLine, row.ItemArray.Select(i => i.ToString()))
                        };

                        card.Controls.Add(label);
                        flowLayoutPanel1.Controls.Add(card);
                    }

                    // Acumuladores por tabla
                    switch (tableName.ToLower())
                    {
                        case "compras":
                            using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) AS C, SUM(total) AS S FROM compras", conn))
                            using (MySqlDataReader r = countCmd.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    totalRegistrosGlobal += Convert.ToInt32(r["C"]);
                                    sumaGlobal += Convert.ToDecimal(r["S"] == DBNull.Value ? 0 : r["S"]);
                                    cantidadParaPromedio += Convert.ToInt32(r["C"]);
                                }
                            }
                            break;

                        case "ventas":
                            using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) AS C, SUM(total) AS S FROM ventas", conn))
                            using (MySqlDataReader r = countCmd.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    totalRegistrosGlobal += Convert.ToInt32(r["C"]);
                                    sumaGlobal += Convert.ToDecimal(r["S"] == DBNull.Value ? 0 : r["S"]);
                                    cantidadParaPromedio += Convert.ToInt32(r["C"]);
                                }
                            }
                            break;

                        case "productos":
                            using (MySqlCommand countCmd = new MySqlCommand("SELECT COUNT(*) AS C, SUM(precio) AS S FROM productos", conn))
                            using (MySqlDataReader r = countCmd.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    totalRegistrosGlobal += Convert.ToInt32(r["C"]);
                                    sumaGlobal += Convert.ToDecimal(r["S"] == DBNull.Value ? 0 : r["S"]);
                                    cantidadParaPromedio += Convert.ToInt32(r["C"]);
                                }
                            }
                            break;

                        default:
                            using (MySqlCommand countCmd = new MySqlCommand($"SELECT COUNT(*) FROM {tableName}", conn))
                            {
                                totalRegistrosGlobal += Convert.ToInt32(countCmd.ExecuteScalar());
                            }
                            break;
                    }
                }

                conn.Close();
            }

            decimal promedioGlobal = cantidadParaPromedio > 0 ? (sumaGlobal / cantidadParaPromedio) : 0;

            lblSuma.Text = $"Total registros: {totalRegistrosGlobal}\n" +
                           $"Suma total: {sumaGlobal:C}\n" +
                           $"Promedio: {promedioGlobal:C}";
        }


        
        private void GenerarCardsDesdeDataTable(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                Panel card = new Panel
                {
                    Width = 250,
                    Height = 150,
                    BackColor = Color.WhiteSmoke,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10)
                };

                Label titulo = new Label
                {
                    Text = row[0].ToString(),
                    Dock = DockStyle.Top,
                    Height = 30,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                card.Controls.Add(titulo);

                FlowLayoutPanel contenido = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.TopDown,
                    AutoScroll = true
                };

                foreach (DataColumn col in data.Columns)
                {
                    if (col.ColumnName == "id" || col.Ordinal == 0) continue;

                    Label campo = new Label
                    {
                        Text = $"{col.ColumnName}: {row[col]}",
                        AutoSize = true
                    };
                    contenido.Controls.Add(campo);
                }

                card.Controls.Add(contenido);
                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private void MostrarResumenEstadistico(DataTable data)
        {
            // Total de registros
            int total = data.Rows.Count;
            lblTotalRegistros.Text = $"Registros: {total}";

            // Intentamos encontrar una columna numérica
            string columnaNumerica = null;

            foreach (DataColumn col in data.Columns)
            {
                if (col.DataType == typeof(int) || col.DataType == typeof(decimal) || col.DataType == typeof(double))
                {
                    columnaNumerica = col.ColumnName;
                    break;
                }
            }

            if (columnaNumerica != null)
            {
                double suma = 0;

                foreach (DataRow row in data.Rows)
                {
                    if (double.TryParse(row[columnaNumerica].ToString(), out double valor))
                    {
                        suma += valor;
                    }
                }

                double promedio = total > 0 ? suma / total : 0;

                lblSuma.Text = $"Suma ({columnaNumerica}): {suma:N2}";
                lblPromedio.Text = $"Promedio ({columnaNumerica}): {promedio:N2}";
            }
            else
            {
                lblSuma.Text = "No hay columna numérica";
                lblPromedio.Text = "";
            }
        }

        private void FRMCards_Load(object sender, EventArgs e)
        {
            LoadCardsFromMultipleTables(tablasSeleccionadas);
        }
    }
}
