using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreciosInvMKPL
{
	public partial class FrmPrecios : Form
	{
		public FrmPrecios()
		{
			InitializeComponent();
		}

		private void BtnSalir_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void TxtFileStockP_DragDrop(object sender, DragEventArgs e)
		{
			string[] txtFileStockP = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			this.txtFileStockP.Text = txtFileStockP[0];

		}

		private void TxtFileStockP_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
		}

		private void TxtFileCDatosAct_DragDrop(object sender, DragEventArgs e)
		{
			string[] TxtFileCDatosAct = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			this.txtFileCDatosAct.Text = TxtFileCDatosAct[0];
		}

		private void TxtFileCDatosAct_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
		}

		private void BtnAceptar_Click(object sender, EventArgs e)
		{

			if (this.txtFileStockP.Text.Trim() == "" || this.txtFileCDatosAct.Text.Trim() == "")
			{
				MessageBox.Show("Falta ruta y nombre e archivo(s)");
				return;
			}


			//Validar que existan los archivos
			string extension = System.IO.Path.GetExtension(this.txtFileStockP.Text);
			if (extension.ToUpper() != ".XLSX")
			{
				MessageBox.Show("Tipo de archivo incorrecto en Stock y Precio");
				return;
			}
			extension = System.IO.Path.GetExtension(this.txtFileCDatosAct.Text);
			if (extension.ToUpper() != ".XLSX")
			{
				MessageBox.Show("Tipo de archivo incorrecto en Inventario");
				return;
			}



			frmProceso objfrmProceso = new frmProceso();
			objfrmProceso.strTipoOperacion = "UPDPREC";
			objfrmProceso.strPathFilePrec = this.txtFileStockP.Text;
			objfrmProceso.strPathFIleUpd = this.txtFileCDatosAct.Text;

		    objfrmProceso.Location = new Point(this.Left , Math.Abs(this.Bottom) - 120 );
			objfrmProceso.Width = this.Width;

			objfrmProceso.ShowDialog();
			//objfrmProceso.CrearPlantillaPrecios(string.Empty, string.Empty);
		}
	}
}
