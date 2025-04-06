using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Windows.Forms.DataVisualization.Charting;


namespace StockSells.forms

{

    public partial class FormGraficos : Form
    {
        SqlConnection conectar;
        public FormGraficos()
        {
            InitializeComponent();
        }


        public void Conectar(List<SerieDatos> series)
        {
            try
            {
                conectar = new SqlConnection("Server=MSI\\SQLEXPRESS;Database=API;Integrated Security=True;");
                conectar.Open();

                chart1.Series.Clear();//Limpiar datos previos
                chart1.ChartAreas.Clear(); // Limpiar áreas anteriores

                ChartArea area = new ChartArea("AreaPrincipal");
                chart1.ChartAreas.Add(area);

                foreach (var serieData in series)
                {
                    DataTable datos = EnviarDatos(serieData.Consulta);
                    Series serie = new Series(serieData.NombreSerie);
                    serie.ChartType = SeriesChartType.Column; // Puedes cambiar a .Line, .Bar, etc.

                    foreach (DataRow row in datos.Rows)
                    {
                        object x = row[serieData.CampoX];
                        double y = Convert.ToDouble(row[serieData.CampoY]);
                        serie.Points.AddXY(x, y);
                    }

                    chart1.Series.Add(serie);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
            }
        }

        private DataTable EnviarDatos(string consulta)
        {
            DataTable tabla = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(consulta, conectar);
            sda.Fill(tabla);
            return tabla;
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class SerieDatos
    {
        public string NombreSerie { get; set; }
        public string Consulta { get; set; }
        public string CampoX { get; set; }
        public string CampoY { get; set; }
    }
}
