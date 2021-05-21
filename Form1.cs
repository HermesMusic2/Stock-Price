using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;



namespace PreciosInvMKPL
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void Label3_Click(object sender, EventArgs e)
		{
			// I n v e n t a r i o s
			this.Hide();
			frmInventarios objfrmInventarios = new frmInventarios();
			objfrmInventarios.ShowDialog();
			this.Show();
		}

		private void Label4_Click(object sender, EventArgs e)
		{
			// P r e c i o s
			this.Hide();
			FrmPrecios objfrmPrecios = new FrmPrecios();
			objfrmPrecios.ShowDialog();
			this.Show();
		}

		private int iGetIDProcces(string nameProcces)
		{
			try
			{
				Process[] asProccess = Process.GetProcessesByName(nameProcces);

				foreach (Process pProccess in asProccess)
				{
					if (pProccess.MainWindowTitle == "")
					{
						pProccess.Kill();
					}
				}

				return -1;
			}
			catch (Exception ex)
			{
				return -1;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			iGetIDProcces("Excel");
		}
	}
}
