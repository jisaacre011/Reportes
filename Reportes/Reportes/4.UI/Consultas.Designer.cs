namespace Reportes._4.UI
{
    partial class Consultas
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
            this.btnVerprestamos = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnVerclientes = new System.Windows.Forms.Button();
            this.btnVerpagos = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVerprestamos
            // 
            this.btnVerprestamos.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerprestamos.Location = new System.Drawing.Point(276, 89);
            this.btnVerprestamos.Name = "btnVerprestamos";
            this.btnVerprestamos.Size = new System.Drawing.Size(227, 37);
            this.btnVerprestamos.TabIndex = 88;
            this.btnVerprestamos.Text = "Ver préstamos";
            this.btnVerprestamos.UseVisualStyleBackColor = true;
            this.btnVerprestamos.Click += new System.EventHandler(this.btnVerprestamos_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(34, 144);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(712, 167);
            this.dataGridView1.TabIndex = 84;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnVerclientes
            // 
            this.btnVerclientes.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerclientes.Location = new System.Drawing.Point(34, 89);
            this.btnVerclientes.Name = "btnVerclientes";
            this.btnVerclientes.Size = new System.Drawing.Size(226, 37);
            this.btnVerclientes.TabIndex = 83;
            this.btnVerclientes.Text = "Ver clientes";
            this.btnVerclientes.UseVisualStyleBackColor = true;
            this.btnVerclientes.Click += new System.EventHandler(this.btnVerclientes_Click);
            // 
            // btnVerpagos
            // 
            this.btnVerpagos.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerpagos.Location = new System.Drawing.Point(519, 89);
            this.btnVerpagos.Name = "btnVerpagos";
            this.btnVerpagos.Size = new System.Drawing.Size(227, 37);
            this.btnVerpagos.TabIndex = 92;
            this.btnVerpagos.Text = "Ver pagos";
            this.btnVerpagos.UseVisualStyleBackColor = true;
            this.btnVerpagos.Click += new System.EventHandler(this.btnVerpagos_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Garamond", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(308, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 39);
            this.label8.TabIndex = 93;
            this.label8.Text = "Consultas";
            // 
            // Consultas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 338);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnVerpagos);
            this.Controls.Add(this.btnVerprestamos);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnVerclientes);
            this.Name = "Consultas";
            this.Text = "Consultas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVerprestamos;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnVerclientes;
        private System.Windows.Forms.Button btnVerpagos;
        private System.Windows.Forms.Label label8;
    }
}