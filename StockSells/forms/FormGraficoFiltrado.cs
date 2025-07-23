using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockSells.forms
{
    public partial class FormGraficoFiltrado : Form
    {
        public FormGraficoFiltrado()
        {
            InitializeComponent();

        }

        private DataTable datosBackup;
        private string tipoGraficoBackup;

        private void FormGraficoFiltrado_Load(object sender, EventArgs e)
        {
            cmbTipoSerie.Items.AddRange(new string[] {
        "Column", "Bar", "Pie", "Line"
    });
            cmbTipoSerie.SelectedIndex = 0; // por defecto


        }


        public void MostrarGrafico(DataTable datosFiltrados, string tipoGraficoSeleccionado)
        {
            // Leer tipo de gráfico desde ComboBox
            string tipoSerieSeleccionado = cmbTipoSerie.SelectedItem?.ToString();
            SeriesChartType tipoSerie = SeriesChartType.Column;

            if (!string.IsNullOrEmpty(tipoSerieSeleccionado) &&
                Enum.TryParse(tipoSerieSeleccionado, out SeriesChartType tipoParsed))
            {
                tipoSerie = tipoParsed;
            }

            chartFiltrado.Series.Clear();
            chartFiltrado.Titles.Clear();
            chartFiltrado.Titles.Add("Gráfico: " + tipoGraficoSeleccionado);

            Series serie = new Series(tipoGraficoSeleccionado)
            {
                ChartType = tipoSerie,
                IsValueShownAsLabel = true
            };

            // Mapear nombre correcto según opción seleccionada
            string origenTabla = "";
            if (tipoGraficoSeleccionado.StartsWith("Ventas")) origenTabla = "Ventas";
            else if (tipoGraficoSeleccionado.StartsWith("Compras")) origenTabla = "Compras";
            else if (tipoGraficoSeleccionado.StartsWith("Clientes")) origenTabla = "Clientes";
            else if (tipoGraficoSeleccionado.StartsWith("Productos")) origenTabla = "Productos";
            else if (tipoGraficoSeleccionado.StartsWith("Usuarios")) origenTabla = "Usuarios";

            var filas = datosFiltrados.Rows.Cast<DataRow>()
                .Where(r => r["OrigenTabla"].ToString().Equals(origenTabla, StringComparison.OrdinalIgnoreCase));

            if (!filas.Any())
            {
                MessageBox.Show("No hay datos para graficar esta opción.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            switch (tipoGraficoSeleccionado)
            {
                case "Ventas por producto":
                    foreach (var grupo in filas.GroupBy(r => r["nombre_producto"].ToString()))
                    {
                        decimal total = grupo.Sum(r => Convert.ToDecimal(r["total"]));
                        serie.Points.AddXY(grupo.Key, total);
                    }
                    break;

                case "Compras por proveedor":
                    foreach (var grupo in filas.GroupBy(r => r["proveedor_id"].ToString()))
                    {
                        decimal total = grupo.Sum(r => Convert.ToDecimal(r["total"]));
                        serie.Points.AddXY(grupo.Key, total);
                    }
                    break;

                case "Clientes por país":
                    foreach (var grupo in filas.GroupBy(r => r["pais"].ToString()))
                    {
                        int total = grupo.Count();
                        serie.Points.AddXY(grupo.Key, total);
                    }
                    break;

                case "Productos por categoría":
                    foreach (var grupo in filas.GroupBy(r => r["categoria_id"].ToString()))
                    {
                        int total = grupo.Count();
                        serie.Points.AddXY(grupo.Key, total);
                    }
                    break;

                case "Usuarios por rol":
                    foreach (var grupo in filas.GroupBy(r => r["id_rol"].ToString()))
                    {
                        int total = grupo.Count();
                        serie.Points.AddXY(grupo.Key, total);
                    }
                    break;
            }

            chartFiltrado.Series.Add(serie);

            datosBackup = datosFiltrados;
            tipoGraficoBackup = tipoGraficoSeleccionado;
        }


        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            if (datosBackup != null && !string.IsNullOrEmpty(tipoGraficoBackup))
            {
                MostrarGrafico(datosBackup, tipoGraficoBackup);
            }
        }
    }
}
