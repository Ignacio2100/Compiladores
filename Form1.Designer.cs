﻿
namespace AnalisisLexico
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gboxCodigo = new System.Windows.Forms.GroupBox();
            this.lblnumero = new System.Windows.Forms.Label();
            this.Pizarra = new System.Windows.Forms.RichTextBox();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.gboxTabla = new System.Windows.Forms.GroupBox();
            this.dgvtabladatos = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.sbtnTema = new AnalisisLexico.Clases.SlideButton();
            this.gboxCodigo.SuspendLayout();
            this.gboxTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtabladatos)).BeginInit();
            this.SuspendLayout();
            // 
            // gboxCodigo
            // 
            this.gboxCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxCodigo.BackColor = System.Drawing.Color.Transparent;
            this.gboxCodigo.Controls.Add(this.lblnumero);
            this.gboxCodigo.Controls.Add(this.Pizarra);
            this.gboxCodigo.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gboxCodigo.ForeColor = System.Drawing.Color.Black;
            this.gboxCodigo.Location = new System.Drawing.Point(15, 14);
            this.gboxCodigo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gboxCodigo.Name = "gboxCodigo";
            this.gboxCodigo.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gboxCodigo.Size = new System.Drawing.Size(1271, 560);
            this.gboxCodigo.TabIndex = 0;
            this.gboxCodigo.TabStop = false;
            this.gboxCodigo.Text = "Introduce Codigo:";
            // 
            // lblnumero
            // 
            this.lblnumero.BackColor = System.Drawing.Color.Transparent;
            this.lblnumero.ForeColor = System.Drawing.Color.Black;
            this.lblnumero.Location = new System.Drawing.Point(7, 28);
            this.lblnumero.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblnumero.Name = "lblnumero";
            this.lblnumero.Size = new System.Drawing.Size(44, 513);
            this.lblnumero.TabIndex = 1;
            // 
            // Pizarra
            // 
            this.Pizarra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pizarra.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Pizarra.ForeColor = System.Drawing.Color.Black;
            this.Pizarra.Location = new System.Drawing.Point(57, 28);
            this.Pizarra.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Pizarra.Name = "Pizarra";
            this.Pizarra.Size = new System.Drawing.Size(1191, 513);
            this.Pizarra.TabIndex = 0;
            this.Pizarra.Text = "";
            this.Pizarra.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // btnProcesar
            // 
            this.btnProcesar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnProcesar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcesar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProcesar.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.ForeColor = System.Drawing.Color.Black;
            this.btnProcesar.Location = new System.Drawing.Point(72, 589);
            this.btnProcesar.Margin = new System.Windows.Forms.Padding(4);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(159, 51);
            this.btnProcesar.TabIndex = 3;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = false;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // gboxTabla
            // 
            this.gboxTabla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxTabla.BackColor = System.Drawing.Color.Transparent;
            this.gboxTabla.Controls.Add(this.dgvtabladatos);
            this.gboxTabla.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gboxTabla.ForeColor = System.Drawing.Color.Black;
            this.gboxTabla.Location = new System.Drawing.Point(15, 660);
            this.gboxTabla.Margin = new System.Windows.Forms.Padding(4);
            this.gboxTabla.Name = "gboxTabla";
            this.gboxTabla.Padding = new System.Windows.Forms.Padding(4);
            this.gboxTabla.Size = new System.Drawing.Size(1271, 159);
            this.gboxTabla.TabIndex = 2;
            this.gboxTabla.TabStop = false;
            this.gboxTabla.Text = "Distribucion";
            // 
            // dgvtabladatos
            // 
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvtabladatos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvtabladatos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvtabladatos.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvtabladatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvtabladatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvtabladatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvtabladatos.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvtabladatos.Location = new System.Drawing.Point(4, 24);
            this.dgvtabladatos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvtabladatos.Name = "dgvtabladatos";
            this.dgvtabladatos.RowHeadersWidth = 51;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvtabladatos.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvtabladatos.RowTemplate.Height = 24;
            this.dgvtabladatos.Size = new System.Drawing.Size(1263, 131);
            this.dgvtabladatos.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Componente léxico";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Palabra Ingresada";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Columna";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Fila";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            // 
            // btnBorrar
            // 
            this.btnBorrar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnBorrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBorrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBorrar.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrar.ForeColor = System.Drawing.Color.Black;
            this.btnBorrar.Location = new System.Drawing.Point(597, 589);
            this.btnBorrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(159, 51);
            this.btnBorrar.TabIndex = 4;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = false;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // sbtnTema
            // 
            this.sbtnTema.Location = new System.Drawing.Point(1085, 591);
            this.sbtnTema.MinimumSize = new System.Drawing.Size(45, 22);
            this.sbtnTema.Name = "sbtnTema";
            this.sbtnTema.OffBackColor = System.Drawing.Color.LightGray;
            this.sbtnTema.OffToggleColor = System.Drawing.Color.White;
            this.sbtnTema.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.sbtnTema.OnToggleColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.sbtnTema.Size = new System.Drawing.Size(159, 52);
            this.sbtnTema.TabIndex = 6;
            this.sbtnTema.UseVisualStyleBackColor = true;
            this.sbtnTema.CheckedChanged += new System.EventHandler(this.sbtnTema_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 832);
            this.Controls.Add(this.sbtnTema);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.gboxTabla);
            this.Controls.Add(this.gboxCodigo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compilador UMG";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gboxCodigo.ResumeLayout(false);
            this.gboxTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvtabladatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gboxCodigo;
        private System.Windows.Forms.RichTextBox Pizarra;
        private System.Windows.Forms.GroupBox gboxTabla;
        private System.Windows.Forms.DataGridView dgvtabladatos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Label lblnumero;
        private System.Windows.Forms.Button btnBorrar;
        private Clases.SlideButton sbtnTema;
    }
}

