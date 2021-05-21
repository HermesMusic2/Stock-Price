namespace PreciosInvMKPL
{
	partial class FrmPrecios
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
			this.picFileCDatosAct = new System.Windows.Forms.PictureBox();
			this.picFileStockP = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtFileCDatosAct = new System.Windows.Forms.TextBox();
			this.txtFileStockP = new System.Windows.Forms.TextBox();
			this.btnAceptar = new System.Windows.Forms.Button();
			this.btnSalir = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.picFileCDatosAct)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picFileStockP)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(5, 3);
			this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(867, 40);
			this.label1.TabIndex = 7;
			this.label1.Text = "Funciones Generación de Plantilla de Precios";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// picFileCDatosAct
			// 
			this.picFileCDatosAct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picFileCDatosAct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picFileCDatosAct.Image = global::PreciosInvMKPL.Properties.Resources.PuntosSusp;
			this.picFileCDatosAct.Location = new System.Drawing.Point(823, 178);
			this.picFileCDatosAct.Name = "picFileCDatosAct";
			this.picFileCDatosAct.Size = new System.Drawing.Size(33, 22);
			this.picFileCDatosAct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picFileCDatosAct.TabIndex = 49;
			this.picFileCDatosAct.TabStop = false;
			// 
			// picFileStockP
			// 
			this.picFileStockP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picFileStockP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picFileStockP.Image = global::PreciosInvMKPL.Properties.Resources.PuntosSusp;
			this.picFileStockP.Location = new System.Drawing.Point(823, 94);
			this.picFileStockP.Name = "picFileStockP";
			this.picFileStockP.Size = new System.Drawing.Size(33, 22);
			this.picFileStockP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picFileStockP.TabIndex = 48;
			this.picFileStockP.TabStop = false;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(2, 223);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(159, 17);
			this.label3.TabIndex = 47;
			this.label3.Text = "(Ruta y nombre del archivo)";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(13, 86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(102, 41);
			this.label2.TabIndex = 46;
			this.label2.Text = "STOCK y PRECIO:";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(0, 128);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(159, 17);
			this.label5.TabIndex = 45;
			this.label5.Text = "(Ruta y nombre del archivo)";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(13, 168);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(102, 41);
			this.label4.TabIndex = 44;
			this.label4.Text = "PRECIOS UPDATE";
			// 
			// txtFileCDatosAct
			// 
			this.txtFileCDatosAct.AllowDrop = true;
			this.txtFileCDatosAct.BackColor = System.Drawing.SystemColors.Window;
			this.txtFileCDatosAct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFileCDatosAct.Location = new System.Drawing.Point(121, 176);
			this.txtFileCDatosAct.Name = "txtFileCDatosAct";
			this.txtFileCDatosAct.Size = new System.Drawing.Size(688, 26);
			this.txtFileCDatosAct.TabIndex = 43;
			this.txtFileCDatosAct.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtFileCDatosAct_DragDrop);
			this.txtFileCDatosAct.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtFileCDatosAct_DragEnter);
			// 
			// txtFileStockP
			// 
			this.txtFileStockP.AllowDrop = true;
			this.txtFileStockP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFileStockP.Location = new System.Drawing.Point(121, 94);
			this.txtFileStockP.Name = "txtFileStockP";
			this.txtFileStockP.Size = new System.Drawing.Size(688, 26);
			this.txtFileStockP.TabIndex = 42;
			this.txtFileStockP.DragDrop += new System.Windows.Forms.DragEventHandler(this.TxtFileStockP_DragDrop);
			this.txtFileStockP.DragEnter += new System.Windows.Forms.DragEventHandler(this.TxtFileStockP_DragEnter);
			// 
			// btnAceptar
			// 
			this.btnAceptar.Location = new System.Drawing.Point(343, 390);
			this.btnAceptar.Name = "btnAceptar";
			this.btnAceptar.Size = new System.Drawing.Size(79, 30);
			this.btnAceptar.TabIndex = 41;
			this.btnAceptar.Text = "Aceptar";
			this.btnAceptar.UseVisualStyleBackColor = true;
			this.btnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
			// 
			// btnSalir
			// 
			this.btnSalir.Location = new System.Drawing.Point(468, 390);
			this.btnSalir.Name = "btnSalir";
			this.btnSalir.Size = new System.Drawing.Size(79, 30);
			this.btnSalir.TabIndex = 40;
			this.btnSalir.Text = "Salir";
			this.btnSalir.UseVisualStyleBackColor = true;
			this.btnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
			// 
			// FrmPrecios
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(879, 469);
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
			this.Name = "FrmPrecios";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FrmPrecios";
			((System.ComponentModel.ISupportInitialize)(this.picFileCDatosAct)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picFileStockP)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox picFileCDatosAct;
		private System.Windows.Forms.PictureBox picFileStockP;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtFileCDatosAct;
		private System.Windows.Forms.TextBox txtFileStockP;
		private System.Windows.Forms.Button btnAceptar;
		private System.Windows.Forms.Button btnSalir;
	}
}