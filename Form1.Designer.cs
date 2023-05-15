
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
            this.PBIcono = new System.Windows.Forms.PictureBox();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.gboxTabla = new System.Windows.Forms.GroupBox();
            this.dgvtabladatos = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvErrores = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.sbtnTema = new AnalisisLexico.Clases.SlideButton();
            this.dgvTraduccion = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gboxCodigo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBIcono)).BeginInit();
            this.gboxTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtabladatos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraduccion)).BeginInit();
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
            this.gboxCodigo.Location = new System.Drawing.Point(11, 11);
            this.gboxCodigo.Margin = new System.Windows.Forms.Padding(2);
            this.gboxCodigo.Name = "gboxCodigo";
            this.gboxCodigo.Padding = new System.Windows.Forms.Padding(2);
            this.gboxCodigo.Size = new System.Drawing.Size(952, 276);
            this.gboxCodigo.TabIndex = 0;
            this.gboxCodigo.TabStop = false;
            this.gboxCodigo.Text = "Introduce Codigo:";
            // 
            // lblnumero
            // 
            this.lblnumero.BackColor = System.Drawing.Color.Transparent;
            this.lblnumero.ForeColor = System.Drawing.Color.Black;
            this.lblnumero.Location = new System.Drawing.Point(5, 23);
            this.lblnumero.Name = "lblnumero";
            this.lblnumero.Size = new System.Drawing.Size(33, 269);
            this.lblnumero.TabIndex = 1;
            // 
            // Pizarra
            // 
            this.Pizarra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pizarra.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Pizarra.ForeColor = System.Drawing.Color.Black;
            this.Pizarra.Location = new System.Drawing.Point(43, 18);
            this.Pizarra.Margin = new System.Windows.Forms.Padding(2);
            this.Pizarra.Name = "Pizarra";
            this.Pizarra.Size = new System.Drawing.Size(905, 254);
            this.Pizarra.TabIndex = 0;
            this.Pizarra.Text = "";
            this.Pizarra.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // PBIcono
            // 
            this.PBIcono.Image = global::AnalisisLexico.Properties.Resources.luna1;
            this.PBIcono.Location = new System.Drawing.Point(871, 355);
            this.PBIcono.Margin = new System.Windows.Forms.Padding(2);
            this.PBIcono.Name = "PBIcono";
            this.PBIcono.Size = new System.Drawing.Size(61, 41);
            this.PBIcono.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PBIcono.TabIndex = 7;
            this.PBIcono.TabStop = false;
            // 
            // btnBorrar
            // 
            this.btnBorrar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnBorrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBorrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnBorrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnBorrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrar.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrar.ForeColor = System.Drawing.Color.Black;
            this.btnBorrar.Location = new System.Drawing.Point(689, 354);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(119, 41);
            this.btnBorrar.TabIndex = 4;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = false;
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // btnProcesar
            // 
            this.btnProcesar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnProcesar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcesar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnProcesar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnProcesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcesar.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.ForeColor = System.Drawing.Color.Black;
            this.btnProcesar.Location = new System.Drawing.Point(555, 356);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(119, 40);
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
            this.gboxTabla.Location = new System.Drawing.Point(12, 430);
            this.gboxTabla.Name = "gboxTabla";
            this.gboxTabla.Size = new System.Drawing.Size(130, 64);
            this.gboxTabla.TabIndex = 2;
            this.gboxTabla.TabStop = false;
            this.gboxTabla.Text = "Distribucion";
            this.gboxTabla.Visible = false;
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
            this.Column5,
            this.Column3,
            this.Column4});
            this.dgvtabladatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvtabladatos.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dgvtabladatos.Location = new System.Drawing.Point(3, 19);
            this.dgvtabladatos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvtabladatos.Name = "dgvtabladatos";
            this.dgvtabladatos.RowHeadersWidth = 51;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvtabladatos.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvtabladatos.RowTemplate.Height = 24;
            this.dgvtabladatos.Size = new System.Drawing.Size(124, 42);
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
            // Column5
            // 
            this.Column5.HeaderText = "Id";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
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
            // dgvErrores
            // 
            this.dgvErrores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column8,
            this.Column7});
            this.dgvErrores.Location = new System.Drawing.Point(39, 304);
            this.dgvErrores.Name = "dgvErrores";
            this.dgvErrores.RowHeadersWidth = 20;
            this.dgvErrores.Size = new System.Drawing.Size(500, 120);
            this.dgvErrores.TabIndex = 3;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "NO.";
            this.Column6.Name = "Column6";
            this.Column6.Width = 50;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "DESCRIPCION";
            this.Column8.Name = "Column8";
            this.Column8.Width = 325;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "UBICACION";
            this.Column7.Name = "Column7";
            this.Column7.Width = 110;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 290);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "ERRORES";
            // 
            // sbtnTema
            // 
            this.sbtnTema.Location = new System.Drawing.Point(813, 356);
            this.sbtnTema.Margin = new System.Windows.Forms.Padding(2);
            this.sbtnTema.MinimumSize = new System.Drawing.Size(34, 18);
            this.sbtnTema.Name = "sbtnTema";
            this.sbtnTema.OffBackColor = System.Drawing.Color.LightGray;
            this.sbtnTema.OffToggleColor = System.Drawing.Color.White;
            this.sbtnTema.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.sbtnTema.OnToggleColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.sbtnTema.Size = new System.Drawing.Size(119, 40);
            this.sbtnTema.TabIndex = 6;
            this.sbtnTema.UseVisualStyleBackColor = true;
            this.sbtnTema.CheckedChanged += new System.EventHandler(this.sbtnTema_CheckedChanged);
            // 
            // dgvTraduccion
            // 
            this.dgvTraduccion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraduccion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column10});
            this.dgvTraduccion.Location = new System.Drawing.Point(289, 430);
            this.dgvTraduccion.Name = "dgvTraduccion";
            this.dgvTraduccion.Size = new System.Drawing.Size(343, 150);
            this.dgvTraduccion.TabIndex = 9;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Palabra Ingresada";
            this.Column9.Name = "Column9";
            this.Column9.Width = 150;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Traduccion";
            this.Column10.Name = "Column10";
            this.Column10.Width = 150;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(975, 605);
            this.Controls.Add(this.dgvTraduccion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PBIcono);
            this.Controls.Add(this.dgvErrores);
            this.Controls.Add(this.gboxTabla);
            this.Controls.Add(this.sbtnTema);
            this.Controls.Add(this.gboxCodigo);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.btnBorrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compilador UMG";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gboxCodigo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PBIcono)).EndInit();
            this.gboxTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvtabladatos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraduccion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gboxCodigo;
        private System.Windows.Forms.RichTextBox Pizarra;
        private System.Windows.Forms.GroupBox gboxTabla;
        private System.Windows.Forms.DataGridView dgvtabladatos;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Label lblnumero;
        private System.Windows.Forms.Button btnBorrar;
        private Clases.SlideButton sbtnTema;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.PictureBox PBIcono;
        private System.Windows.Forms.DataGridView dgvErrores;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTraduccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    }
}

