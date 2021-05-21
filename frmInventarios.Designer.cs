namespace PreciosInvMKPL
{
	partial class frmInventarios
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
			this.label1 = new System.Windows.Forms.Label();
			this.btnSalir = new System.Windows.Forms.Button();
			this.btnAceptar = new System.Windows.Forms.Button();
			this.txtFileStockP = new System.Windows.Forms.TextBox();
			this.txtFileCDatosAct = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.picFileStockP = new System.Windows.Forms.PictureBox();
			this.picFileCDatosAct = new System.Windows.Forms.PictureBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.picFileStockP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picFileCDatosAct)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(1, 2);
			this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(907, 41);
			this.label1.TabIndex = 6;
			this.label1.Text = "Generación de Plantilla de Inventarios";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnSalir
			// 
			this.btnSalir.Location = new System.Drawing.Point(499, 383);
			this.btnSalir.Name = "btnSalir";
			this.btnSalir.Size = new System.Drawing.Size(79, 30);
			this.btnSalir.TabIndex = 7;
			this.btnSalir.Text = "Salir";
			this.btnSalir.UseVisualStyleBackColor = true;
			this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
			// 
			// btnAceptar
			// 
			this.btnAceptar.Location = new System.Drawing.Point(374, 383);
			this.btnAceptar.Name = "btnAceptar";
			this.btnAceptar.Size = new System.Drawing.Size(79, 30);
			this.btnAceptar.TabIndex = 8;
			this.btnAceptar.Text = "Aceptar";
			this.btnAceptar.UseVisualStyleBackColor = true;
			this.btnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
			// 
			// txtFileStockP
			// 
			this.txtFileStockP.AllowDrop = true;
			this.txtFileStockP.Location = new System.Drawing.Point(167, 94);
			this.txtFileStockP.Name = "txtFileStockP";
			this.txtFileStockP.Size = new System.Drawing.Size(670, 26);
			this.txtFileStockP.TabIndex = 11;
			this.txtFileStockP.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtFileStockP_DragDrop);
			this.txtFileStockP.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtFileStockP_DragEnter);
			// 
			// txtFileCDatosAct
			// 
			this.txtFileCDatosAct.AllowDrop = true;
			this.txtFileCDatosAct.Location = new System.Drawing.Point(167, 176);
			this.txtFileCDatosAct.Name = "txtFileCDatosAct";
			this.txtFileCDatosAct.Size = new System.Drawing.Size(670, 26);
			this.txtFileCDatosAct.TabIndex = 12;
			this.txtFileCDatosAct.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtFileCDatosAct_DragDrop);
			this.txtFileCDatosAct.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtFileCDatosAct_DragEnter);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(5, 177);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(135, 23);
			this.label4.TabIndex = 13;
			this.label4.Text = "INVENTARIOS:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(5, 93);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(159, 23);
			this.label2.TabIndex = 15;
			this.label2.Text = "STOCK y PRECIO:";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(5, 116);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(159, 17);
			this.label5.TabIndex = 14;
			this.label5.Text = "(Ruta y nombre del archivo)";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(5, 200);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(159, 17);
			this.label3.TabIndex = 16;
			this.label3.Text = "(Ruta y nombre del archivo)";
			// 
			// picFileStockP
			// 
			this.picFileStockP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picFileStockP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picFileStockP.Image = global::PreciosInvMKPL.Properties.Resources.PuntosSusp;
			this.picFileStockP.Location = new System.Drawing.Point(846, 94);
			this.picFileStockP.Name = "picFileStockP";
			this.picFileStockP.Size = new System.Drawing.Size(33, 22);
			this.picFileStockP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picFileStockP.TabIndex = 38;
			this.picFileStockP.TabStop = false;
			this.picFileStockP.Click += new System.EventHandler(this.PicFileStockP_Click);
			// 
			// picFileCDatosAct
			// 
			this.picFileCDatosAct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picFileCDatosAct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picFileCDatosAct.Image = global::PreciosInvMKPL.Properties.Resources.PuntosSusp;
			this.picFileCDatosAct.Location = new System.Drawing.Point(846, 178);
			this.picFileCDatosAct.Name = "picFileCDatosAct";
			this.picFileCDatosAct.Size = new System.Drawing.Size(33, 22);
			this.picFileCDatosAct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picFileCDatosAct.TabIndex = 39;
			this.picFileCDatosAct.TabStop = false;
			this.picFileCDatosAct.Click += new System.EventHandler(this.PicFileCDatosAct_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// frmInventarios
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(914, 457);
			this.Controls.Add(this.picFileCDatosAct);
			this.Controls.Add(this.picFileStockP);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtFileCDatosAct);
			this.Controls.Add(this.txtFileStockP);
			this.Controls.Add(this.btnAceptar);
			this.Controls.Add(this.btnSalir);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "frmInventarios";
			this.Text = "frmInventarios";
			((System.ComponentModel.ISupportInitialize)(this.picFileStockP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picFileCDatosAct)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSalir;
		private System.Windows.Forms.Button btnAceptar;
		private System.Windows.Forms.TextBox txtFileStockP;
		private System.Windows.Forms.TextBox txtFileCDatosAct;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox picFileStockP;
		private System.Windows.Forms.PictureBox picFileCDatosAct;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}