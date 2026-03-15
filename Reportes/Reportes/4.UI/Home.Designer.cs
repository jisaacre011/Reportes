namespace Reportes
{
    partial class Home
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmCliente = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPrestamos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPagos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmConsultas = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMoras = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClientes = new System.Windows.Forms.ToolStripTextBox();
            this.btnPrestamos = new System.Windows.Forms.ToolStripTextBox();
            this.btnPagos = new System.Windows.Forms.ToolStripTextBox();
            this.btnConsultas = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox5 = new System.Windows.Forms.ToolStripTextBox();
            this.btnMoras = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox7 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox8 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox9 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox10 = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCliente,
            this.tsmPrestamos,
            this.tsmPagos,
            this.tsmConsultas,
            this.tsmReportes,
            this.tsmMoras});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(552, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmCliente
            // 
            this.tsmCliente.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClientes});
            this.tsmCliente.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmCliente.Name = "tsmCliente";
            this.tsmCliente.Size = new System.Drawing.Size(88, 28);
            this.tsmCliente.Text = "Clientes";
            this.tsmCliente.Click += new System.EventHandler(this.tsmCliente_Click);
            // 
            // tsmPrestamos
            // 
            this.tsmPrestamos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrestamos});
            this.tsmPrestamos.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmPrestamos.Name = "tsmPrestamos";
            this.tsmPrestamos.Size = new System.Drawing.Size(108, 28);
            this.tsmPrestamos.Text = "Prestamos";
            // 
            // tsmPagos
            // 
            this.tsmPagos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPagos});
            this.tsmPagos.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmPagos.Name = "tsmPagos";
            this.tsmPagos.Size = new System.Drawing.Size(71, 28);
            this.tsmPagos.Text = "Pagos";
            // 
            // tsmConsultas
            // 
            this.tsmConsultas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConsultas});
            this.tsmConsultas.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmConsultas.Name = "tsmConsultas";
            this.tsmConsultas.Size = new System.Drawing.Size(103, 28);
            this.tsmConsultas.Text = "Consultas";
            // 
            // tsmReportes
            // 
            this.tsmReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox5,
            this.toolStripTextBox7,
            this.toolStripTextBox8,
            this.toolStripTextBox9,
            this.toolStripTextBox10});
            this.tsmReportes.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmReportes.Name = "tsmReportes";
            this.tsmReportes.Size = new System.Drawing.Size(96, 28);
            this.tsmReportes.Text = "Reportes";
            // 
            // tsmMoras
            // 
            this.tsmMoras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMoras});
            this.tsmMoras.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmMoras.Name = "tsmMoras";
            this.tsmMoras.Size = new System.Drawing.Size(74, 28);
            this.tsmMoras.Text = "Moras";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Garamond", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(108, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 36);
            this.label1.TabIndex = 4;
            this.label1.Text = "Administracion financiera";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Garamond", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(152, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 30);
            this.label2.TabIndex = 5;
            this.label2.Text = "Gestion de prestamos";
            // 
            // btnClientes
            // 
            this.btnClientes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(100, 23);
            this.btnClientes.Text = "Clientes";
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnPrestamos
            // 
            this.btnPrestamos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPrestamos.Name = "btnPrestamos";
            this.btnPrestamos.Size = new System.Drawing.Size(100, 23);
            this.btnPrestamos.Text = "Prestamos";
            this.btnPrestamos.Click += new System.EventHandler(this.btnPrestamos_Click);
            // 
            // btnPagos
            // 
            this.btnPagos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPagos.Name = "btnPagos";
            this.btnPagos.Size = new System.Drawing.Size(100, 23);
            this.btnPagos.Text = "Pagos";
            this.btnPagos.Click += new System.EventHandler(this.btnPagos_Click);
            // 
            // btnConsultas
            // 
            this.btnConsultas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnConsultas.Name = "btnConsultas";
            this.btnConsultas.Size = new System.Drawing.Size(100, 23);
            this.btnConsultas.Text = "Consultas";
            this.btnConsultas.Click += new System.EventHandler(this.btnConsultas_Click);
            // 
            // toolStripTextBox5
            // 
            this.toolStripTextBox5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox5.Name = "toolStripTextBox5";
            this.toolStripTextBox5.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox5.Text = "Tabla de amortizacion";
            // 
            // btnMoras
            // 
            this.btnMoras.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnMoras.Name = "btnMoras";
            this.btnMoras.Size = new System.Drawing.Size(100, 23);
            this.btnMoras.Text = "Moras";
            this.btnMoras.Click += new System.EventHandler(this.btnMoras_Click);
            // 
            // toolStripTextBox7
            // 
            this.toolStripTextBox7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox7.Name = "toolStripTextBox7";
            this.toolStripTextBox7.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox7.Text = "Informacion de cliente";
            // 
            // toolStripTextBox8
            // 
            this.toolStripTextBox8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox8.Name = "toolStripTextBox8";
            this.toolStripTextBox8.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox8.Text = "Total prestado";
            // 
            // toolStripTextBox9
            // 
            this.toolStripTextBox9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox9.Name = "toolStripTextBox9";
            this.toolStripTextBox9.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox9.Text = "Moras acumuladas";
            // 
            // toolStripTextBox10
            // 
            this.toolStripTextBox10.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox10.Name = "toolStripTextBox10";
            this.toolStripTextBox10.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox10.Text = "Clientes morosos";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 240);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Home";
            this.Text = "Home";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmCliente;
        private System.Windows.Forms.ToolStripMenuItem tsmPrestamos;
        private System.Windows.Forms.ToolStripMenuItem tsmPagos;
        private System.Windows.Forms.ToolStripMenuItem tsmConsultas;
        private System.Windows.Forms.ToolStripMenuItem tsmReportes;
        private System.Windows.Forms.ToolStripMenuItem tsmMoras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripTextBox btnClientes;
        private System.Windows.Forms.ToolStripTextBox btnPrestamos;
        private System.Windows.Forms.ToolStripTextBox btnPagos;
        private System.Windows.Forms.ToolStripTextBox btnConsultas;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox5;
        private System.Windows.Forms.ToolStripTextBox btnMoras;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox7;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox8;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox9;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox10;
    }
}

