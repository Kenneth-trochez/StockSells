namespace StockSells.forms
{
    partial class FormGraficoFiltrado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGraficoFiltrado));
            this.chartFiltrado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_salir = new System.Windows.Forms.Button();
            this.cmbTipoSerie = new System.Windows.Forms.ComboBox();
            this.btnAplicarFiltros = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartFiltrado)).BeginInit();
            this.SuspendLayout();
            // 
            // chartFiltrado
            // 
            chartArea1.Name = "ChartArea1";
            this.chartFiltrado.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartFiltrado.Legends.Add(legend1);
            this.chartFiltrado.Location = new System.Drawing.Point(82, 12);
            this.chartFiltrado.Name = "chartFiltrado";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartFiltrado.Series.Add(series1);
            this.chartFiltrado.Size = new System.Drawing.Size(435, 410);
            this.chartFiltrado.TabIndex = 0;
            this.chartFiltrado.Text = "chart1";
            // 
            // btn_salir
            // 
            this.btn_salir.BackColor = System.Drawing.Color.Transparent;
            this.btn_salir.BackgroundImage = global::StockSells.Properties.Resources.salida;
            this.btn_salir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_salir.FlatAppearance.BorderSize = 0;
            this.btn_salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_salir.Location = new System.Drawing.Point(575, 402);
            this.btn_salir.Margin = new System.Windows.Forms.Padding(2);
            this.btn_salir.Name = "btn_salir";
            this.btn_salir.Size = new System.Drawing.Size(42, 37);
            this.btn_salir.TabIndex = 2;
            this.btn_salir.UseVisualStyleBackColor = false;
            this.btn_salir.Click += new System.EventHandler(this.btn_salir_Click);
            // 
            // cmbTipoSerie
            // 
            this.cmbTipoSerie.FormattingEnabled = true;
            this.cmbTipoSerie.Location = new System.Drawing.Point(523, 29);
            this.cmbTipoSerie.Name = "cmbTipoSerie";
            this.cmbTipoSerie.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoSerie.TabIndex = 3;
            this.cmbTipoSerie.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnAplicarFiltros
            // 
            this.btnAplicarFiltros.BackColor = System.Drawing.Color.Transparent;
            this.btnAplicarFiltros.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAplicarFiltros.BackgroundImage")));
            this.btnAplicarFiltros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAplicarFiltros.FlatAppearance.BorderSize = 0;
            this.btnAplicarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarFiltros.Font = new System.Drawing.Font("Bodoni MT", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicarFiltros.Location = new System.Drawing.Point(550, 343);
            this.btnAplicarFiltros.Name = "btnAplicarFiltros";
            this.btnAplicarFiltros.Size = new System.Drawing.Size(94, 37);
            this.btnAplicarFiltros.TabIndex = 16;
            this.btnAplicarFiltros.TabStop = false;
            this.btnAplicarFiltros.Text = "Actualizar";
            this.btnAplicarFiltros.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAplicarFiltros.UseVisualStyleBackColor = false;
            this.btnAplicarFiltros.Click += new System.EventHandler(this.btnAplicarFiltros_Click);
            // 
            // FormGraficoFiltrado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 450);
            this.Controls.Add(this.btnAplicarFiltros);
            this.Controls.Add(this.cmbTipoSerie);
            this.Controls.Add(this.btn_salir);
            this.Controls.Add(this.chartFiltrado);
            this.Name = "FormGraficoFiltrado";
            this.Text = "FormGraficoFiltrado";
            this.Load += new System.EventHandler(this.FormGraficoFiltrado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartFiltrado)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartFiltrado;
        private System.Windows.Forms.Button btn_salir;
        private System.Windows.Forms.ComboBox cmbTipoSerie;
        private System.Windows.Forms.Button btnAplicarFiltros;
    }
}