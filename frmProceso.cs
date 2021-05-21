using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Threading;


namespace PreciosInvMKPL
{
	public partial class frmProceso : Form
	{
		private string _strPathFilePrec=string.Empty;
		private string _strPathFIleUpd = string.Empty;
		private string _strTipoOperacion = string.Empty;

		public string strPathFilePrec
		{ get { return _strPathFilePrec; }
		  set { _strPathFilePrec = value; }
		}
		public string strPathFIleUpd
		{
			get { return _strPathFIleUpd; }
			set { _strPathFIleUpd = value; }
		}
		public string strTipoOperacion
		{
			get { return _strTipoOperacion; }
			set { _strTipoOperacion = value; }
		}

		public frmProceso()
		{
			InitializeComponent();
		}


		private void FrmProceso_Load(object sender, EventArgs e)
		{
			this.textBox1.Focus();
		}
		public void CrearPlantillaInv(string strPathFIleUpd, string strPathFileInv)
		{
			Procesos objProcesos = new Procesos(strPathFIleUpd, strPathFileInv);
			
			ThreadStart delegadoprinc = new ThreadStart(objProcesos.CrearPlantillaInv2);
			//Creamos la instancia del hilo 
			Thread hilo = new Thread(delegadoprinc);
			CheckForIllegalCrossThreadCalls = false;
			//Iniciamos el hilo 
			hilo.Start();
			while (hilo.IsAlive)
			{
				for (int i = 0; i < 100; i++)
				{
					progressBar1.Value = i;
					progressBar1.Refresh();
					//esperaremos medio segundo en cada iteración
					////////if ((pvNombreRep.Contains("MARCA") == true && pvNombreRep.Contains("PROD") == true) && pbImpMens == 1)
					Thread.Sleep(50);
					////////else
					/////    Thread.Sleep(35);
				}
			}
			// workerObject.RequestStop();
			// Use the Join method to block the current thread 
			// until the object's thread terminates.
			hilo.Join();
			//
			// Encontrar la hoja del libro que se necesita
			//
			progressBar1.Value = 100;
			Close();

		}
		public void CrearPlantillaPrecios(string strPathFIleUpd, string strPathFilePrec)
		{
			Procesos objProcesos = new Procesos(strPathFIleUpd, strPathFilePrec);

			ThreadStart delegadoprinc = new ThreadStart(objProcesos.CrearPlantillaPrecios2);
			//Creamos la instancia del hilo 
			Thread hilo = new Thread(delegadoprinc);
			CheckForIllegalCrossThreadCalls = false;
			//Iniciamos el hilo 
			hilo.Start();
			while (hilo.IsAlive)
			{
				for (int i = 0; i < 100; i++)
				{
					progressBar1.Value = i;
					progressBar1.Refresh();
					//esperaremos medio segundo en cada iteración
					Thread.Sleep(50);
				}
			}
			// workerObject.RequestStop();
			// Use the Join method to block the current thread 
			// until the object's thread terminates.
			hilo.Join();
			//
			// Encontrar la hoja del libro que se necesita
			//
			progressBar1.Value = 100;
			Close();
		}

		private void TrackBar1_Enter(object sender, EventArgs e)
		{
			CrearPlantillaPrecios(strPathFIleUpd, strPathFilePrec);

		}

		private void TextBox1_Enter(object sender, EventArgs e)
		{
			if (strTipoOperacion == "UPDPREC")
			{
				CrearPlantillaPrecios(strPathFIleUpd, strPathFilePrec);
			}
			else if (strTipoOperacion == "UPDINV")
			{
				CrearPlantillaInv(strPathFIleUpd, strPathFilePrec);
			}
		}

		private void TextBox1_TextChanged(object sender, EventArgs e)
		{
			//CrearPlantillaPrecios(strPathFIleUpd, strPathFilePrec);
		}
	}
	public class Procesos
	{
		public string strPathFilePrec;
		public string strPathFIleUpd;
		private int iColsFilePrec;
		public string strPathFileInv;

		public struct PosVApp
		{
			public int isSku;
			public int isModelo;
			public int iPrecBaseWebHer;
			public int iPrecPromWebHer;
			public int iPrecBaseMarketplace;
			public int iPrecPromMarketplace;
			public int iPrecAmazonPos;
			public int iPrecMLPos;
			public int iPrecClasroShopPos;
			public int iPrecWalmartPos;
			public int iPrecCoppelPos;
			public int iPrecElektraPos;
			public int iPrecLinioPos;
			public int iPrecLiverpoolPos;
		}
		struct PIHerm
		{
			public int intBinFileInv;
			public int intPartFileInv;
			public int intSkuFileInv;
			public int intDispFileInv;
		}
		PIHerm oPIHerm = new PIHerm();

		public Procesos(string sPathFileFuente, string sPathFIleUpd)
		{
			strPathFilePrec = sPathFileFuente; // se refirá a los precios cuando se actualicen Prec
			strPathFileInv = sPathFileFuente; // se refirá al inventario cuando se actualicen los inv
			strPathFIleUpd = sPathFIleUpd;
		}

		public void CrearPlantillaPrecios2()
		{

			//strPathFIleUpd = @"C:\E\PreciosInvMKPL\PreciosInvMKPL\bin\Debug\Plantillas\Archivo_stock_precio_21_4_2021_14163 RevSkusV2.xlsx";
			//strPathFilePrec = @"C:\E\PreciosInvMKPL\PreciosInvMKPL\bin\Debug\Plantillas\Maestro de Precios e Inventarios (002).xlsx";
			// ------------------------------------------------------
			// ------------------------------------------------------  
			// ON
			// INSTANCIAR EXCEL Y ABRIR HOJA DE EXCEL

			//System.Data.DataTable dt = ds.Tables[0];
			string strPathFIleUpdCp = strPathFIleUpd.Substring(strPathFIleUpd.LastIndexOf(@"\")).ToUpper().Replace(".XLSX", "")
				 + DateTime.Now.ToString("HHmmss").ToString().Trim();

			string sfileDest = @"C:\temps\" + strPathFIleUpdCp;
			File.Copy(strPathFIleUpd, sfileDest);


			object oOpt = System.Reflection.Missing.Value; //for optional arguments
			Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
			Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Open(sfileDest);
			Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

			app.DisplayAlerts = false;

			//app.Visible = true;

			int cantHojas = workbook.Sheets.Count;
			var sNomHoja = "Productos";
			Sheets hojas = null;

			// SELECCIONAR LA HOJA
			workbook.Sheets[sNomHoja].Activate();   // Sheets["Hoja1"] 
			worksheet = workbook.ActiveSheet;
			//app.ActiveWindow.DisplayGridlines = false;


			//Get a range of data.
			Microsoft.Office.Interop.Excel.Range rangeArchivUpd;
			///////////////////////////////////////////
			///////////////////////////////////////////
			///////////////////////////////////////////
			rangeArchivUpd = worksheet.UsedRange;

			//Retrieve the data from the range.
			Object[,] arrArchivUpd;
			arrArchivUpd = (System.Object[,])rangeArchivUpd.get_Value(Missing.Value);
			//Determine the dimensions of the array.
			//long iRowsArrArchivUpd = arrArchivUpd.GetUpperBound(0);
			long iColsArrArchivUpd = arrArchivUpd.GetUpperBound(1);

			worksheet.Cells[1, iColsArrArchivUpd + 1] = "Comentarios";
			worksheet.Cells[2, iColsArrArchivUpd + 1] = "Comentarios";
			//--------------------------------------------------------
			//--------------------------------------------------------
			rangeArchivUpd = worksheet.UsedRange;
			arrArchivUpd = (System.Object[,])rangeArchivUpd.get_Value(Missing.Value);
			//Determine the dimensions of the array.
			long iRowsArrArchivUpd = arrArchivUpd.GetUpperBound(0);
			iColsArrArchivUpd = arrArchivUpd.GetUpperBound(1);


			///////////////////////////////////////////
			/////////////////////////////////////////////////////
			/////////////////////////////////////////////////////
			// Web
			//rangeWeb = wkrShWeb.get_Range(sRangoWeb);



			//-----------------------------------------------------
			//----------------------------------------------------
			// Apertura de archivos que entrega costos

			string strPathFilePrecCp = strPathFilePrec.Substring(strPathFilePrec.LastIndexOf(@"\")).ToUpper().Replace(".XLSX", "")
				 + DateTime.Now.ToString("HHmmss").ToString().Trim();

			string sfileDestCp = @"C:\temps\" + strPathFilePrecCp;
			File.Copy(strPathFilePrec, sfileDestCp);


			Object[,] arrObjFilePrec;
			Microsoft.Office.Interop.Excel.Workbook workbookPrec = app.Workbooks.Open(sfileDestCp);
			Microsoft.Office.Interop.Excel.Worksheet worksheetPrec = null;
			app.DisplayAlerts = false;


			//app.Visible = true;
			// SELECCIONAR LA HOJA


			workbookPrec.Sheets[1].Activate();   // Sheets["Hoja1"] 
			worksheetPrec = workbookPrec.ActiveSheet;

			//	app.ActiveWindow.DisplayGridlines = false;

			//Get a range of data.
			Range rangeFilePrec = worksheetPrec.UsedRange; ;
			///////////////////////////////////////////
			///////////////////////////////////////////
			///////////////////////////////////////////

			//Retrieve the data from the range.
			arrObjFilePrec = (System.Object[,])rangeFilePrec.get_Value(Missing.Value);

			// Cerrar el libro del SAT. Se descargaron los datos en la matriz << saRetSat1 >> y
			// Se obtuvieron 

			////long iRowsArrArchivUpd = arrArchivUpd.GetUpperBound(0);
			////iColsArrArchivUpd = arrArchivUpd.GetUpperBound(1);

			//Determine the dimensions of the array.

			int iRowsFilePrec = arrObjFilePrec.GetUpperBound(0);
			int iColsFilePrec = arrObjFilePrec.GetUpperBound(1);

			worksheetPrec.Cells[1, iColsFilePrec + 1] = "Comentarios";

			//--------------------------------------------------------
			//--------------------------------------------------------
			rangeFilePrec = worksheetPrec.UsedRange;
			arrObjFilePrec = (System.Object[,])rangeFilePrec.get_Value(Missing.Value);
			app.Visible = true;
			//Determine the dimensions of the array.

			iRowsFilePrec = arrObjFilePrec.GetUpperBound(0);
			iColsFilePrec = arrObjFilePrec.GetUpperBound(1);

			int isSku = 0;
			int isModelo = 0;
			int iPrecBaseWebHer = 0;
			int iPrecPromWebHer = 0;
			int iPrecBaseMarketplace = 0;
			int iPrecPromMarketplace = 0;
			int iPrecAmazonPos = 0;
			int iPrecMLPos = 0;
			int iPrecClasroShopPos = 0;
			int iPrecWalmartPos = 0;
			int iPrecCoppelPos = 0;
			int iPrecElektraPos = 0;
			int iPrecLinioPos = 0;
			int iPrecLiverpoolPos = 0;



			//decimal decImpSat1 = saRetArchivAValidar[i, intPosImp] == null ? 0m : Convert.ToDecimal(saRetArchivAValidar[i, intPosImp]);

			for (long i = 1; i <= 3; i++)
			{
				if (i == 1)
				{
					// Obtner la posicion 

					for (int j = 1; j <= iColsFilePrec; j++)
					{
						object VarObj = arrObjFilePrec[i, 1] == null ? string.Empty : arrObjFilePrec[i, 1];

						if (arrObjFilePrec[i, j].ToString().ToUpper() == "SKU")
						{
							isSku = j;
						}
						if (arrObjFilePrec[i, j].ToString().ToUpper() == "MODELO")
						{
							isModelo = j;
						}

						iPrecBaseWebHer = 4;
						iPrecPromWebHer = 5;
						iPrecBaseMarketplace = 6;
						iPrecPromMarketplace = 7;
						iPrecAmazonPos = 8;
						iPrecMLPos = 9;
						iPrecClasroShopPos = 10;
						iPrecWalmartPos = 11;
						iPrecCoppelPos = 12;
						iPrecElektraPos = 13;
						iPrecLinioPos = 14;
						iPrecLiverpoolPos = 15;


						//else if (arrObjFilePrec[i, j].ToString().ToUpper().Contains("BASE") %%)
						//{
						//	intSkuFileInv = j;
						//}
						//else if (arrObjFilePrec[i, j].ToString().ToUpper().Contains("DISPONIBLE"))
						//{
						//	intDispFileInv = j;
						//}
					}

				}//(intDispFileInv == 0)
			}


			int iSkuVA = 2;
			int iMLStock = 3;
			int iMLPrec = 4;
			int iLinioStock = 5;
			int iLinioPrec = 6;
			int iLinioPrecOfer = 7;
			int iClaroSStock = 8;
			int iClaroSPrec = 9;
			int iClaroSPrecOfer = 10;
			int iWishStock = 11;
			int iWishPrec = 12;
			int iShopifyStock = 13;
			int iShopifyPrec = 14;
			int iShopifyPrecOfer = 15;
			int iAmazonStock = 16;
			int iAmazonPrec = 17;
			int iElektraStock = 18;
			int iElektraPrec = 19;
			int iWalMartStock = 20;
			int iWalMartPrec = 21;
			int iLiverpStock = 22;
			int iLiverpPrec = 23;



			RevisionFilePrec(arrObjFilePrec, arrArchivUpd, iSkuVA, iRowsFilePrec, iColsFilePrec, iRowsArrArchivUpd,
			 iPrecBaseWebHer,
			 iPrecPromWebHer,
			 iPrecBaseMarketplace,
			 iPrecPromMarketplace,
			 iPrecAmazonPos,
			 iPrecMLPos,
			 iPrecClasroShopPos,
			 iPrecWalmartPos,
			 iPrecCoppelPos,
			 iPrecElektraPos,
			 iPrecLinioPos,
			 iPrecLiverpoolPos);

			string strLetraUltCol = ExelConvertToLetter(iColsFilePrec);
			Microsoft.Office.Interop.Excel.Range oRngPrec = worksheetPrec.get_Range("A1:" + strLetraUltCol + iRowsFilePrec.ToString());
			//	= iRowsArrArchivUpd.ToString().Trim());
			oRngPrec.set_Value(oOpt, arrObjFilePrec);


			//-------------------------------------------------
			//  -- P r o c e s o ------
			//-------------------------------------------------

			// Afectacion de los Precios
			iRowsFilePrec = arrObjFilePrec.GetUpperBound(0);
			iColsFilePrec = arrObjFilePrec.GetUpperBound(1);


			for (int i = 3; i <= iRowsArrArchivUpd; i++)
			{
				object ostrSku = arrArchivUpd[i, iSkuVA];
				int indexPrecAct = -1;
				indexPrecAct = findPrecValor(arrObjFilePrec, ostrSku, isSku, iRowsFilePrec);
				if (indexPrecAct <= -1)
				{
					// No se encontro en la relacion de precios de costos
					continue;
				}
				if (arrObjFilePrec[indexPrecAct, iColsFilePrec] == null)
				{
					// No tiene problema el renglón con los precios
				}
				else
				{
					continue;
				}
				// Marcar los renglones en la matriz arrArchivUpd con Modif es equivalente a decir que en
				// si se encuentra en el excel de la relacion de precios que pasa costos
				arrArchivUpd[i, iColsArrArchivUpd] = "Modif";


				for (int j = 3; j < iColsArrArchivUpd; j++)
				{

					// Hay 3 plataformas (linio, claroShop y shopify) que pueden tener el precio 
					// oferta en el excel que sale de VentiApp en null pero que si tienen precio
					// Por lo tanto antes de descartarlo hay que preguntar si el precio base
					// no es null

					if (j == iClaroSPrecOfer)
					{
						if (arrArchivUpd[i, iClaroSPrec] == null)
							continue;
					}
					else if (j == iLinioPrecOfer)
					{
						if (arrArchivUpd[i, iLinioPrec] == null)
							continue;
					}
					else if (j == iShopifyPrecOfer)
					{
						if (arrArchivUpd[i, iShopifyPrec] == null)
							continue;
					}
					else if (arrArchivUpd[i, j] == null)
					{
						continue;
					}


					// Afectaciones a la matriz proveniente del excel de shopify
					decimal decprecioBaseMP = Math.Round(arrObjFilePrec[indexPrecAct, iPrecBaseMarketplace] == null ? 0.0m : Convert.ToDecimal(arrObjFilePrec[indexPrecAct, iPrecBaseMarketplace]));
					decimal decPrecPromMP = Math.Round(arrObjFilePrec[indexPrecAct, iPrecPromMarketplace] == null ? 0.0m : Convert.ToDecimal(arrObjFilePrec[indexPrecAct, iPrecPromMarketplace]));

					if (j == iMLStock)
					{

					}
					else if (j == iMLPrec)
					{
						// Es nulo
						decimal decPrecPos = Math.Round((arrObjFilePrec[indexPrecAct, iPrecMLPos] == null || arrObjFilePrec[indexPrecAct, iPrecMLPos] == string.Empty)? 0.0m : Convert.ToDecimal(arrObjFilePrec[indexPrecAct, iPrecMLPos]));

						if (decPrecPos == 0m && decPrecPromMP == 0m)
						{
							arrArchivUpd[i, j] = decprecioBaseMP;// arrObjFilePrec[indexPrecAct, iPrecBaseMarketplace];
						}
						else if (decPrecPos == 0m || decPrecPromMP == 0m)
						{
							arrArchivUpd[i, j] = decPrecPos + decPrecPromMP;
						}
						else if (decPrecPos <= decPrecPromMP)
						{
							arrArchivUpd[i, j] = decPrecPos;//arrObjFilePrec[indexPrecAct, iPrecPromMarketplace];
						}
						else
						{
							arrArchivUpd[i, j] = decPrecPromMP;// arrObjFilePrec[indexPrecAct, iPrecMLPos];
						}

					}
					else if (j == iLinioStock)
					{

					}
					else if (j == iLinioPrec)
					{
						arrArchivUpd[i, j] = decprecioBaseMP;
					}


					else if (j == iLinioPrecOfer)
					{
						decimal decPrecPos = Math.Round(arrObjFilePrec[indexPrecAct, iPrecLinioPos] == null ? 0.0m : Convert.ToDecimal(arrObjFilePrec[indexPrecAct, iPrecLinioPos]));

						if (decPrecPos == 0.0m && decPrecPromMP == 0.0m)
						{
							arrArchivUpd[i, j] = decPrecPromMP;
						}
						else if (decPrecPos == 0.0m || decPrecPromMP == 0.0m)
						{
							arrArchivUpd[i, j] = decPrecPromMP + decPrecPos;
						}
						else if (decPrecPos <= decPrecPromMP)
						{
							arrArchivUpd[i, j] = decPrecPos;
						}
						else
						{
							arrArchivUpd[i, j] = decPrecPromMP;
						}
					}
					else if (j == iClaroSStock)
					{

					}

					else if (j == iClaroSPrec)
					{
						arrArchivUpd[i, j] = decprecioBaseMP;
					}
					else if (j == iClaroSPrecOfer)
					{
						decimal decPrecPos = Math.Round(arrObjFilePrec[indexPrecAct, iPrecClasroShopPos] == null ? 0.0m : Convert.ToDecimal(arrObjFilePrec[indexPrecAct, iPrecClasroShopPos]));

						if (decPrecPos == 0.0m && decPrecPromMP == 0.0m)
						{
							if (arrArchivUpd[i, j] == null)
							{
								// ---------------------------
							}
							else
							{
								arrArchivUpd[i, j] = decPrecPromMP;
							}
						}
						else if (decPrecPos == 0.0m || decPrecPromMP == 0.0m)
						{
							arrArchivUpd[i, j] = decPrecPromMP + decPrecPos;
						}
						else if (decPrecPos <= decPrecPromMP)
						{
							arrArchivUpd[i, j] = decPrecPos;
						}
						else
						{
							arrArchivUpd[i, j] = decPrecPromMP;
						}
					}
					else if (j == iWishStock)
					{

					}

					else if (j == iWishPrec)
					{
						arrArchivUpd[i, j] = decprecioBaseMP;
					}
					else if (j == iShopifyStock)
					{

					}

					else if (j == iShopifyPrec)
					{
						decimal decprecioBaseMPSf = Math.Round(arrObjFilePrec[indexPrecAct, iPrecBaseWebHer] == null ? 0.0m : Convert.ToDecimal(arrObjFilePrec[indexPrecAct, iPrecBaseWebHer]));
						arrArchivUpd[i, j] = decprecioBaseMPSf;
					}
					else if (j == iShopifyPrecOfer)
					{
						decimal decPrecPromMPSf = Math.Round(arrObjFilePrec[indexPrecAct, iPrecPromWebHer] == null ? 0.0m : Convert.ToDecimal(arrObjFilePrec[indexPrecAct, iPrecPromWebHer]));
						if (arrArchivUpd[i, j] == null && decPrecPromMPSf == 0.0m)
						{
							// ------
						}
						else
						{
							arrArchivUpd[i, j] = decPrecPromMPSf;
						}
					}
					else if (j == iAmazonStock)
					{

					}

					//int isSku = 0;
					//int isModelo = 0;
					//int iPrecBaseWebHer = 0;
					//int iPrecPromWebHer = 0;
					//int iPrecBaseMarketplace = 0;
					//int iPrecPromMarketplace = 0;
					//int iPrecAmazonPos = 0;
					//int iPrecMLPos = 0;
					//int iPrecClasroShopPos = 0;
					//int iPrecWalmartPos = 0;
					//int iPrecCoppelPos = 0;
					//int iPrecElektraPos = 0;
					//int iPrecLinioPos = 0;
					//int iPrecLiverpoolPos = 0;

					else if (j == iAmazonPrec)
					{
						// --------
					}
					else if (j == iElektraStock)
					{

					}
					else if (j == iElektraPrec)
					{
						// Es nulo
						decimal decPrecPos = Math.Round(arrObjFilePrec[indexPrecAct, iPrecElektraPos] == null ? 0.0m : Convert.ToDecimal(arrObjFilePrec[indexPrecAct, iPrecElektraPos]));

						if (decPrecPos == 0m && decPrecPromMP == 0m)
						{
							arrArchivUpd[i, j] = decprecioBaseMP;// arrObjFilePrec[indexPrecAct, iPrecBaseMarketplace];
						}
						else if (decPrecPos == 0m || decPrecPromMP == 0m)
						{
							arrArchivUpd[i, j] = decPrecPos + decPrecPromMP;
						}
						else if (decPrecPos <= decPrecPromMP)
						{
							arrArchivUpd[i, j] = decPrecPos;//arrObjFilePrec[indexPrecAct, iPrecPromMarketplace];
						}
						else
						{
							arrArchivUpd[i, j] = decPrecPromMP;// arrObjFilePrec[indexPrecAct, iPrecMLPos];
						}

					}
					else if (j == iWalMartStock)
					{
						// Es nulo
					}
					else if (j == iWalMartPrec)
					{
						decimal decPrecPos = Math.Round(arrObjFilePrec[indexPrecAct, iPrecWalmartPos] == null ? 0.0m : Convert.ToDecimal(arrObjFilePrec[indexPrecAct, iPrecWalmartPos]));

						if (decPrecPos == 0m && decPrecPromMP == 0m)
						{
							arrArchivUpd[i, j] = decprecioBaseMP;// arrObjFilePrec[indexPrecAct, iPrecBaseMarketplace];
						}
						else if (decPrecPos == 0m || decPrecPromMP == 0m)
						{
							arrArchivUpd[i, j] = decPrecPos + decPrecPromMP;
						}
						else if (decPrecPos <= decPrecPromMP)
						{
							arrArchivUpd[i, j] = decPrecPos;//arrObjFilePrec[indexPrecAct, iPrecPromMarketplace];
						}
						else
						{
							arrArchivUpd[i, j] = decPrecPromMP;// arrObjFilePrec[indexPrecAct, iPrecMLPos];
						}
					}
					else if (j == iLiverpStock)
					{

					}
					else if (j == iLiverpPrec)
					{
						arrArchivUpd[i, j] = decprecioBaseMP;
					}

				}


			}
			workbook.Sheets[sNomHoja].Activate();   // Sheets["Hoja1"] 
			worksheet = workbook.ActiveSheet;

			// Descargar la matriz en la hoja de excel
			string sCoordRang = "$A$1:$X";
			string sCoordRangFilter = "$A$3:$X";

			string sCoordRangAmazon = "$P$3:$Q4";
			string sCoordRangWish = "$K$3:$L4";

			Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range(sCoordRang + iRowsArrArchivUpd.ToString().Trim());
			oRng.set_Value(oOpt, arrArchivUpd);

			// Filtro 

			//oRng = worksheet.get_Range(sCoordRangFilter + iRowsArrArchivUpd.ToString().Trim());

			worksheet.Range[sCoordRangFilter + (iRowsArrArchivUpd).ToString().Trim()].AutoFilter(Field: iColsArrArchivUpd, Criteria1: "<>Modif");// Operator: xlAnd);
			worksheet.Range[sCoordRangFilter + (iRowsArrArchivUpd).ToString().Trim()].Select();
			worksheet.Range[app.Selection, app.ActiveCell.SpecialCells(XlCellType.xlCellTypeLastCell)].Select();
			app.Selection.EntireRow.Delete(Type.Missing);

			string sLetraColum = ExelConvertToLetter(iColsArrArchivUpd);
			worksheet.Range[sLetraColum + (3).ToString().Trim()].Select();
			app.Selection.EntireColumn.Delete(Type.Missing);

			// Borrar las columnas de Amazon
			worksheet.Range[sCoordRangAmazon].Select();
			app.Selection.EntireColumn.Delete(Type.Missing);

			// Borrar las columnas de Wish
			worksheet.Range[sCoordRangWish].Select();
			app.Selection.EntireColumn.Delete(Type.Missing);


			//app.Selection.AutoFilter();
			Thread.Sleep(5250);
			app.Visible = true;
			app = null;

		}


		private void RevisionFilePrec(object[,] arrFilePrec, object[,] arrArchivUpd, int iSkuVA, int iRowsFilePrec, int iColsFilePrec, long iRowsArrArchivUpd,

			int iPrecBaseWebHer,
			int iPrecPromWebHer,
			int iPrecBaseMarketplace,
			int iPrecPromMarketplace,
			int iPrecAmazonPos,
			int iPrecMLPos,
			int iPrecClasroShopPos,
			int iPrecWalmartPos,
			int iPrecCoppelPos,
			int iPrecElektraPos,
			int iPrecLinioPos,
			int iPrecLiverpoolPos)
		{
			// -----------------------
			for (int rR = 2; rR <= iRowsFilePrec; rR++)
			{
				//numString = "27.3"; //"27" is also a valid decimal
				//canConvert = decimal.TryParse(numString, out number3);
				//if (canConvert == true)

				string strComentario01 = string.Empty;
				string strComentario02 = string.Empty;
				string strComentario03 = string.Empty;
				string strComentario04 = string.Empty;
				string strComentario05 = string.Empty;
				string strComentario06 = string.Empty;
				string strComentario07 = string.Empty;
				string strComentario08 = string.Empty;
				string strComentario09 = string.Empty;
				string strComentario10 = string.Empty;
				string strComentarioReng = string.Empty;




				decimal decNumeroOut = 0.0m;


				bool boolPrecBaseWebHer = arrFilePrec[rR, iPrecBaseWebHer]           == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecBaseWebHer].ToString(), out decNumeroOut);
				bool boolPrecPromWebHer = arrFilePrec[rR, iPrecPromWebHer]           == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecPromWebHer].ToString(), out decNumeroOut);
				bool boolPrecBaseMarketplace = arrFilePrec[rR, iPrecBaseMarketplace] == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecBaseMarketplace].ToString(), out decNumeroOut);
				bool boolPrecPromMarketplace = arrFilePrec[rR, iPrecPromMarketplace] == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecPromMarketplace].ToString(), out decNumeroOut);
				bool boolPrecAmazonPos = arrFilePrec[rR, iPrecAmazonPos]             == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecAmazonPos].ToString(), out decNumeroOut);
				bool boolPrecMLPos = (arrFilePrec[rR, iPrecMLPos]                    == null || arrFilePrec[rR, iPrecMLPos] == string.Empty ) ? true : decimal.TryParse(arrFilePrec[rR, iPrecMLPos].ToString(), out decNumeroOut);
				bool boolPrecClasroShopPos = arrFilePrec[rR, iPrecClasroShopPos]     == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecClasroShopPos].ToString(), out decNumeroOut);
				bool boolPrecWalmartPos = arrFilePrec[rR, iPrecWalmartPos]           == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecWalmartPos].ToString(), out decNumeroOut);
				bool boolPrecCoppelPos = arrFilePrec[rR, iPrecCoppelPos]             == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecCoppelPos].ToString(), out decNumeroOut);
				bool boolPrecElektraPos = arrFilePrec[rR, iPrecElektraPos]           == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecElektraPos].ToString(), out decNumeroOut);
				bool boolPrecLinioPos = arrFilePrec[rR, iPrecLinioPos]               == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecLinioPos].ToString(), out decNumeroOut);
				bool boolPrecLiverpoolPos = arrFilePrec[rR, iPrecLiverpoolPos]       == null ? true : decimal.TryParse(arrFilePrec[rR, iPrecLiverpoolPos].ToString(), out decNumeroOut);

				if (
				  boolPrecBaseWebHer == false ||
				  boolPrecPromWebHer == false ||
				  boolPrecBaseMarketplace == false ||
				  boolPrecPromMarketplace == false ||
				  boolPrecAmazonPos == false ||
				  boolPrecMLPos == false ||
				  boolPrecClasroShopPos == false ||
				  boolPrecWalmartPos == false ||
				  boolPrecCoppelPos == false ||
				  boolPrecElektraPos == false ||
				  boolPrecLinioPos == false ||
				  boolPrecLiverpoolPos == false)
				{
					arrFilePrec[rR, iColsFilePrec] = "Algunas de las celdas no tiene valor Númerico.";
				}
				else if (findPrecValor(arrArchivUpd, arrFilePrec[rR, 1], iSkuVA, iRowsArrArchivUpd) < 0)
				{
					arrFilePrec[rR, iColsFilePrec] = "El sku no se encuentra.";
				}
				else
				{
					strComentarioReng = strComentario01;

					decimal decPrecBaseWebHer = Math.Round(arrFilePrec[rR, iPrecBaseWebHer] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecBaseWebHer]));
					decimal decPrecPromWebHer = Math.Round(arrFilePrec[rR, iPrecPromWebHer] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecPromWebHer]));
					decimal decPrecBaseMarketplace = Math.Round(arrFilePrec[rR, iPrecBaseMarketplace] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecBaseMarketplace]));
					decimal decPrecPromMarketplace = Math.Round(arrFilePrec[rR, iPrecPromMarketplace] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecPromMarketplace]));
					decimal decPrecAmazonPos = Math.Round(arrFilePrec[rR, iPrecAmazonPos] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecAmazonPos]));
					decimal decPrecMLPos = Math.Round((arrFilePrec[rR, iPrecMLPos] == null || arrFilePrec[rR, iPrecMLPos] == string.Empty) ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecMLPos]));
					decimal decPrecClasroShopPos = Math.Round(arrFilePrec[rR, iPrecClasroShopPos] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecClasroShopPos]));
					decimal decPrecWalmartPos = Math.Round(arrFilePrec[rR, iPrecWalmartPos] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecWalmartPos]));
					decimal decPrecCoppelPos = Math.Round(arrFilePrec[rR, iPrecCoppelPos] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecCoppelPos]));
					decimal decPrecElektraPos = Math.Round(arrFilePrec[rR, iPrecElektraPos] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecElektraPos]));
					decimal decPrecLinioPos = Math.Round(arrFilePrec[rR, iPrecLinioPos] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecLinioPos]));
					decimal decPrecLiverpoolPos = Math.Round(arrFilePrec[rR, iPrecLiverpoolPos] == null ? 0.0m : Convert.ToDecimal(arrFilePrec[rR, iPrecLiverpoolPos]));


					if (decPrecBaseWebHer < decPrecPromWebHer)
						strComentario01 = "prec promoción Hermes es mayor que su prec base.";

					if (decPrecBaseMarketplace < decPrecPromMarketplace)
						strComentario02 = "prec promoción MKPL es mayor que su prec base.";


					if (decPrecAmazonPos > 0.0m && decPrecAmazonPos > decPrecBaseMarketplace)
						strComentario03 = "prec POS Amazon es mayor que su prec base MKTPL.";

					if (decPrecMLPos > 0.0m && decPrecMLPos > decPrecBaseMarketplace)
						strComentario04 = "prec POS ML es mayor que su prec base MKTPL.";

					if (decPrecClasroShopPos > 0.0m && decPrecClasroShopPos > decPrecBaseMarketplace)
						strComentario05 = "prec POS CLAROSHOP es mayor que su prec base MKTPL.";

					if (decPrecWalmartPos > 0.0m && decPrecWalmartPos > decPrecBaseMarketplace)
						strComentario06 = "prec POS WALMART es mayor que su prec base MKTPL.";

					if (decPrecCoppelPos > 0.0m && decPrecCoppelPos > decPrecBaseMarketplace)
						strComentario07 = "prec POS COPPEL es mayor que su prec base MKTPL.";

					if (decPrecElektraPos > 0.0m && decPrecElektraPos > decPrecBaseMarketplace)
						strComentario08 = "prec POS ELEKTRA es mayor que su prec base MKTPL.";

					if (decPrecLinioPos > 0.0m && decPrecLinioPos > decPrecBaseMarketplace)
						strComentario09 = "prec POS LINIO es mayor que su prec base MKTPL.";

					if (decPrecLiverpoolPos > 0.0m && decPrecLiverpoolPos > decPrecBaseMarketplace)
						strComentario10 = "prec POS LIVERP es mayor que su prec base MKTPL.";

					strComentarioReng += strComentario01 + strComentario02 + strComentario03 +
						strComentario04 + strComentario05 + strComentario06 + strComentario07 +
						strComentario08 + strComentario09 + strComentario10;
					//------------------------------------

					strComentario01 = string.Empty;
					strComentario02 = string.Empty;
					strComentario03 = string.Empty;
					strComentario04 = string.Empty;
					strComentario05 = string.Empty;
					strComentario06 = string.Empty;
					strComentario07 = string.Empty;
					strComentario08 = string.Empty;
					strComentario09 = string.Empty;
					strComentario10 = string.Empty;


					if (decPrecBaseWebHer < 0.0m)
						strComentario01 = "prec base Hermes es negativo.";

					if (decPrecBaseMarketplace < 0.0m)
						strComentario02 = "prec promoción MKPL es negativo.";

					if (decPrecAmazonPos < 0.0m)
						strComentario03 = "prec POS Amazon es negativo.";

					if (decPrecMLPos < 0.0m)
						strComentario04 = "prec POS ML es negativo.";

					if (decPrecClasroShopPos < 0.0m)
						strComentario05 = "prec POS CLAROSHOP es negativo.";

					if (decPrecWalmartPos < 0.0m)
						strComentario06 = "prec POS WALMART es negativo.";

					if (decPrecCoppelPos < 0.0m)
						strComentario07 = "prec POS COPPEL es negativo.";

					if (decPrecElektraPos < 0.0m)
						strComentario08 = "prec POS ELEKTRA es negativo.";

					if (decPrecLinioPos < 0.0m)
						strComentario09 = "prec POS LINIO es negativo.";

					if (decPrecLiverpoolPos < 0.0m)
						strComentario10 = "prec POS LIVERP es negativo.";

					strComentarioReng += strComentario01 + strComentario02 + strComentario03 +
						strComentario04 + strComentario05 + strComentario06 + strComentario07 +
						strComentario08 + strComentario09 + strComentario10;


					if (strComentarioReng != string.Empty)
					{
						arrFilePrec[rR, iColsFilePrec] = strComentarioReng;
					}

				}

			}
		}
		public void CrearPlantillaInv2()
		{

			//strPathFIleUpd = @"C:\E\PreciosInvMKPL\PreciosInvMKPL\bin\Debug\Plantillas\Archivo_stock_precio_21_4_2021_14163 RevSkusV2.xlsx";
			//strPathFileInv = @"C:\E\PreciosInvMKPL\PreciosInvMKPL\bin\Debug\Plantillas\VENTAS 2019 HMU(Recuperado automáticamente).xlsx";
			// ------------------------------------------------------
			// ------------------------------------------------------  
			// INSTANCIAR EXCEL Y ABRIR HOJA DE EXCEL


			string strPathFIleUpdCp = strPathFIleUpd.Substring(strPathFIleUpd.LastIndexOf(@"\")).ToUpper().Replace(".XLSX", "")
				 + DateTime.Now.ToString("HHmmss").ToString().Trim();

			string sfileDest = @"C:\temps\" + strPathFIleUpdCp;
			File.Copy(strPathFIleUpd, sfileDest);



			object oOpt = System.Reflection.Missing.Value; //for optional arguments
			Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
			Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Open(sfileDest);
			Microsoft.Office.Interop.Excel._Worksheet worksheet = null;


			app.DisplayAlerts = false;
			app.Visible = true;
			int cantHojas = workbook.Sheets.Count;
			var sNomHoja = "Productos";
			Sheets hojas = null;

			// SELECCIONAR LA HOJA
			workbook.Sheets[sNomHoja].Activate();   // Sheets["Hoja1"] 
			worksheet = workbook.ActiveSheet;
			//	app.ActiveWindow.DisplayGridlines = false;



			//Get a range of data.
			Microsoft.Office.Interop.Excel.Range rangeArchivUpd;
			///////////////////////////////////////////
			///////////////////////////////////////////
			///////////////////////////////////////////
			rangeArchivUpd = worksheet.UsedRange;

			//Retrieve the data from the range.
			Object[,] arrArchivUpd;
			arrArchivUpd = (System.Object[,])rangeArchivUpd.get_Value(Missing.Value);
			//Determine the dimensions of the array.
			//long iRowsArrArchivUpd = arrArchivUpd.GetUpperBound(0);
			long iColsArrArchivUpd = arrArchivUpd.GetUpperBound(1);

			worksheet.Cells[1, iColsArrArchivUpd + 1] = "Comentarios";
			worksheet.Cells[2, iColsArrArchivUpd + 1] = "Comentarios";

			//--------------------------------------------------------
			//--------------------------------------------------------
			rangeArchivUpd = worksheet.UsedRange;
			arrArchivUpd = (System.Object[,])rangeArchivUpd.get_Value(Missing.Value);
			//Determine the dimensions of the array.
			long iRowsArrArchivUpd = arrArchivUpd.GetUpperBound(0);
			iColsArrArchivUpd = arrArchivUpd.GetUpperBound(1);


			/////////////////////////////////////////////////////
			/////////////////////////////////////////////////////
			// Web

			//-----------------------------------------------------
			//----------------------------------------------------
			// Apertura de archivos de Inventarios

			Object[,] arrObjFileInv;
			Microsoft.Office.Interop.Excel.Workbook workbookInv = app.Workbooks.Open(strPathFileInv);
			Microsoft.Office.Interop.Excel.Worksheet worksheetInv = null;
			app.DisplayAlerts = false;
			app.Visible = true;
			// SELECCIONAR LA HOJA
			workbookInv.Sheets[1].Activate();   // Sheets["Hoja1"] 
			worksheetInv = workbookInv.ActiveSheet;

			//	app.ActiveWindow.DisplayGridlines = false;

			//Get a range of data.
			Range rangeFileInv = worksheetInv.UsedRange; ;
			///////////////////////////////////////////
			///////////////////////////////////////////
			///////////////////////////////////////////

			//Retrieve the data from the range.
			arrObjFileInv = (System.Object[,])rangeFileInv.get_Value(Missing.Value);

			// Cerrar el libro del SAT. Se descargaron los datos en la matriz << saRetSat1 >> y
			// Se obtuvieron 

			//workbookInv.Close();
			//worksheetInv = null;

			//Determine the dimensions of the array.
			long iRowsFileInv = arrObjFileInv.GetUpperBound(0);
			long iColsFileInv = arrObjFileInv.GetUpperBound(1);



			// Agregar una columna de comentarios
			worksheetInv.Cells[1, iColsFileInv + 1] = "Comentarios";
			//--------------------------------------------------------
			//--------------------------------------------------------

			rangeFileInv = worksheetInv.UsedRange;
			arrObjFileInv = (System.Object[,])rangeFileInv.get_Value(Missing.Value);
			//Determine the dimensions of the array.
			iRowsFileInv = arrObjFileInv.GetUpperBound(0);
			iColsFileInv = arrObjFileInv.GetUpperBound(1);
			//--------------------------------------------------------


			int iSkuVA = 2;
			int iMLStock = 3;
			int iMLPrec = 4;
			int iLinioStock = 5;
			int iLinioPrec = 6;
			int iLinioPrecOfer = 7;
			int iClaroSStock = 8;
			int iClaroSPrec = 9;
			int iClaroSPrecOfer = 10;
			int iWishStock = 11;
			int iWishPrec = 12;
			int iShopifyStock = 13;
			int iShopifyPrec = 14;
			int iShopifyPrecOfer = 15;
			int iAmazonStock = 16;
			int iAmazonPrec = 17;
			int iElektraStock = 18;
			int iElektraPrec = 19;
			int iWalMartStock = 20;
			int iWalMartPrec = 21;
			int iLiverpStock = 22;
			int iLiverpPrec = 23;


			AsignarValoresPosInv(arrObjFileInv);

			RevisionFileInv(arrObjFileInv, arrArchivUpd, iSkuVA, iRowsArrArchivUpd);





			// Afectacion de la Existencia


			//////////RevisionFilePrec(arrObjFilePrec, arrArchivUpd, iSkuVA, iRowsFilePrec, iColsFilePrec, iRowsArrArchivUpd,
			////////// iPrecBaseWebHer,
			////////// iPrecPromWebHer,
			////////// iPrecBaseMarketplace,
			////////// iPrecPromMarketplace,
			////////// iPrecAmazonPos,
			////////// iPrecMLPos,
			////////// iPrecClasroShopPos,
			////////// iPrecWalmartPos,
			////////// iPrecCoppelPos,
			////////// iPrecElektraPos,
			////////// iPrecLinioPos,
			////////// iPrecLiverpoolPos);

			string strLetraUltCol = ExelConvertToLetter(iColsFileInv);
			Microsoft.Office.Interop.Excel.Range oRngPrec = worksheetInv.get_Range("A1:" + strLetraUltCol + iRowsFileInv.ToString());
			//	= iRowsArrArchivUpd.ToString().Trim());
			oRngPrec.set_Value(oOpt, arrObjFileInv);


			//-------------------------------------------------
			//  -- P r o c e s o ------
			//-------------------------------------------------

			// Afectacion de los Precios
			iRowsFileInv = arrObjFileInv.GetUpperBound(0);
			iColsFileInv = arrObjFileInv.GetUpperBound(1);


			for (int i = 3; i <= iRowsArrArchivUpd; i++)
			{
				long lStock;
				object ostrSku = arrArchivUpd[i, iSkuVA];
				int indexFileInv = -1;
				indexFileInv = findPrecValor(arrObjFileInv, ostrSku,oPIHerm.intSkuFileInv, iRowsFileInv);
				if (indexFileInv <= -1)
				{
					// No se encontro en la relacion del archivo de inventarios
					lStock = 0;
				}
				else
				{
					long lngNumeroOut = 0;
					lStock = arrObjFileInv[indexFileInv, oPIHerm.intDispFileInv] == null ? 0 : Convert.ToInt64(arrObjFileInv[indexFileInv, oPIHerm.intDispFileInv]);
				}

				//////// Marcar los renglones en la matriz arrArchivUpd con Modif es equivalente a decir que en
				/////// si se encuentra en el excel de la relacion de precios que pasa costos
				//////arrArchivUpd[i, iColsArrArchivUpd] = "Modif";

				for (int j = 3; j < iColsArrArchivUpd; j++)
				{
					// Hay 3 plataformas (linio, claroShop y shopify) que pueden tener el precio 
					// oferta en el excel que sale de VentiApp en null pero que si tienen precio
					// Por lo tanto antes de descartarlo hay que preguntar si el precio base
					// no es null
					if (arrArchivUpd[i, j] == null)
					{
						continue;
					}

					//intBinFileInv = j;
					//intPartFileInv = j;
					//intSkuFileInv = j;
					//intDispFileInv = j;

					//		long.TryParse(arrObjFileInv[i, intDispFileInv].ToString(), out lngNumeroOut);

					// Afectaciones a la matriz proveniente del excel de shopify
					if (j == iMLStock)
					{
						arrArchivUpd[i, j] = lStock;
					}
					//else if (j == iMLPrec)
					//{
					//}

					else if (j == iLinioStock)
					{
						arrArchivUpd[i, j] = lStock;
					}
					//else if (j == iLinioPrec)
					//{
					//}
					//else if (j == iLinioPrecOfer)
					//{
					//}
					else if (j == iClaroSStock)
					{
						arrArchivUpd[i, j] = lStock;
					}

					//else if (j == iClaroSPrec)
					//{
					//}
					//else if (j == iClaroSPrecOfer)
					//{
					//}

					else if (j == iWishStock)
					{
						arrArchivUpd[i, j] = lStock;
					}
					//else if (j == iWishPrec)
					//{
					//}
					else if (j == iShopifyStock)
					{
						arrArchivUpd[i, j] = lStock;
					}

					//else if (j == iShopifyPrec)
					//{
					//}
					//else if (j == iShopifyPrecOfer)
					//{
					//}

					else if (j == iAmazonStock)
					{
						arrArchivUpd[i, j] = lStock;
					}
					//else if (j == iAmazonPrec)
					//{
					//	// --------
					//}
					else if (j == iElektraStock)
					{
						arrArchivUpd[i, j] = lStock;
					}
					//else if (j == iElektraPrec)
					//{

					//}
					else if (j == iWalMartStock)
					{
						arrArchivUpd[i, j] = lStock;
					}
					//else if (j == iWalMartPrec)
					//{
					//}
					else if (j == iLiverpStock)
					{
						arrArchivUpd[i, j] = lStock;
					}
					//else if (j == iLiverpPrec)
					//{

					//}

				}


			}
			workbook.Sheets[sNomHoja].Activate();   // Sheets["Hoja1"] 
			worksheet = workbook.ActiveSheet;

			// Descargar la matriz en la hoja de excel
			string sCoordRang = "$A$1:$X";
			//string sCoordRangFilter = "$A$3:$X";

			string sCoordRangAmazon = "$P$3:$Q4";
			string sCoordRangWish = "$K$3:$L4";

			Microsoft.Office.Interop.Excel.Range oRng = worksheet.get_Range(sCoordRang + iRowsArrArchivUpd.ToString().Trim());
			oRng.set_Value(oOpt, arrArchivUpd);

			// Filtro 

			//oRng = worksheet.get_Range(sCoordRangFilter + iRowsArrArchivUpd.ToString().Trim());

			//worksheet.Range[sCoordRangFilter + (iRowsArrArchivUpd).ToString().Trim()].AutoFilter(Field: iColsArrArchivUpd, Criteria1: "<>Modif");// Operator: xlAnd);
			//worksheet.Range[sCoordRangFilter + (iRowsArrArchivUpd).ToString().Trim()].Select();
			//worksheet.Range[app.Selection, app.ActiveCell.SpecialCells(XlCellType.xlCellTypeLastCell)].Select();
			//app.Selection.EntireRow.Delete(Type.Missing);

			string sLetraColum = ExelConvertToLetter(iColsArrArchivUpd);
			worksheet.Range[sLetraColum + (3).ToString().Trim()].Select();
			app.Selection.EntireColumn.Delete(Type.Missing);

			// Borrar las columnas de Amazon
			worksheet.Range[sCoordRangAmazon].Select();
			app.Selection.EntireColumn.Delete(Type.Missing);

			// Borrar las columnas de Wish
			worksheet.Range[sCoordRangWish].Select();
			app.Selection.EntireColumn.Delete(Type.Missing);


			//app.Selection.AutoFilter();
			Thread.Sleep(5250);
			app.Visible = true;
			app = null;




			////////////appAV = null;
			//////////////  workbookAV.Close();
			////////////workbookAV = null;
			////////////worksheetAV = null;

			////////////MessageBox.Show("Terminé");

		}
		///////////////////////////////////////////
		///////////////////////////////////////////
		// Activar el libro a Validar

		private void RevisionFileInv(object[,] arrFileInv, object[,] arrArchivUpd, int iSkuVA, long iRowsArrArchivUpd)
		{


			AsignarValoresPosInv(arrFileInv);

			int iRowsFileInv = arrFileInv.GetUpperBound(0);
			int iColsFileInv = arrFileInv.GetUpperBound(1);

			// a) checar que cada sku arrFileInv  exista en arrArchivUpd
			// b) checar que el disponible en arrFileInv sea mayor que cero
			// c) checar que linea de arrFileInv tenga sku. Es decir que no este en blanco.

			// -----------------------
			for (int rR = 2; rR <= iRowsFileInv; rR++)
			{
				if (arrFileInv[rR, oPIHerm.intSkuFileInv] == null)
				{
					arrFileInv[rR, iColsFileInv ] = "Sin SKU";
					continue;
				}
				else if (arrFileInv[rR, oPIHerm.intSkuFileInv] == "")
				{
					arrFileInv[rR, iColsFileInv ] = "Sin SKU";
					continue;
				}

				if (arrFileInv[rR, oPIHerm.intBinFileInv].ToString().ToUpper().Equals ("WEB"))
				{
					// Continua con el flujo
				}
				else
				{
					arrFileInv[rR, iColsFileInv] = "No es inventario WEB";
					continue;
				}


				long lStock = Convert.ToInt64(arrFileInv[rR, oPIHerm.intDispFileInv]);
				if (lStock < 0)
				{
					arrFileInv[rR, iColsFileInv ] = "Existencia Menor que cero";
					continue;
				}
				// Existe en Stock&Precio de VentiApp
				int intIndex = findArrArchivUpd(arrArchivUpd, arrFileInv[rR, oPIHerm.intSkuFileInv], iSkuVA);
                if (intIndex < 0)
				{
					arrFileInv[rR, iColsFileInv ] = "No existe el SKU en el archivo de Stock&Precio";
				}

			}
		}

	
		private object findInvValor(object[,] arrInv, object obSku, int intDispFileInv, int intSkuFileInv, long iRowsFileInv)
		{
			
			object objDisp = null;
			// -----------------------
			for (int rR = 1; rR <= iRowsFileInv; rR++)
			{
				if (obSku.ToString().Trim().Equals(arrInv[rR, intSkuFileInv].ToString().Trim()))
				{
					return arrInv[rR, intDispFileInv];
					break;
				}
			}
			return objDisp;
		}
		private bool AsignarValoresPosInv(object[,] arrObjFileInv)
		{

			long iRowsFileInv = arrObjFileInv.GetUpperBound(0);
			long iColsFileInv = arrObjFileInv.GetUpperBound(1);

			oPIHerm.intDispFileInv = 0;
			oPIHerm.intSkuFileInv = 0;
			oPIHerm.intPartFileInv = 0;
			oPIHerm.intBinFileInv = 0;

			//decimal decImpSat1 = saRetArchivAValidar[i, intPosImp] == null ? 0m : Convert.ToDecimal(saRetArchivAValidar[i, intPosImp]);

			for (long i = 1; i <= iRowsFileInv; i++)
			{
				if (i == 1)
				{
					// Obtner la posicion 

					string strNomColum = "BIN,PART,SKU_C,DISPONIBLE,";
					object VarObj = arrObjFileInv[i, 1] == null ? string.Empty : arrObjFileInv[i, 1];

					for (int j = 1; j < iColsFileInv - 2; j++)
					{
						if (strNomColum.Contains(arrObjFileInv[i, j].ToString().ToUpper().Trim()))
						{
							if (arrObjFileInv[i, j].ToString().ToUpper() == "BIN")
							{
								oPIHerm.intBinFileInv = j;

							}
							if (arrObjFileInv[i, j].ToString().ToUpper() == "PART")
							{
								oPIHerm.intPartFileInv = j;
							}
							else if (arrObjFileInv[i, j].ToString().ToUpper().Contains("SKU_C"))
							{
								oPIHerm.intSkuFileInv = j;
							}
							else if (arrObjFileInv[i, j].ToString().ToUpper().Contains("DISPONIBLE"))
							{
								oPIHerm.intDispFileInv = j;
							}
						}
					}

				}//(intDispFileInv == 0)
			}
			if (oPIHerm.intDispFileInv == 0)
			{
				MessageBox.Show("El archivo de carga de inventario que se obtiene de Epicor se modificó.");
				return false;
			}
			else
			{
				return true;
			}

		}

		private int findPrecValor(object[,] arrPrec, object obSku, int isSku, long iRowsFilePrec)
		{
			int iIndex = -1;
			if (obSku == null)
			{
				return iIndex;
			}
			// -----------------------
			for (int rR = 1; rR <= iRowsFilePrec; rR++)
			{
				if (arrPrec[rR, isSku] != null)
				{
					if (obSku.ToString().Trim().Equals(arrPrec[rR, isSku].ToString().Trim()))
					{
						iIndex = rR;
						return iIndex;
						break;
					}
				}
			}
			return iIndex;
		}

		private int findArrArchivUpd(object[,] ArrArchivUpd, object obSku, int isSku)
		{
			long iRowsArchivUpd = ArrArchivUpd.GetUpperBound(0);
			int iIndex = -1;
			if (obSku == null)
			{
				return iIndex;
			}
			// -----------------------
			for (int rR = 1; rR <= iRowsArchivUpd; rR++)
			{
				if (ArrArchivUpd[rR, isSku] != null)
				{
					if (obSku.ToString().Trim().Equals(ArrArchivUpd[rR, isSku].ToString().Trim()))
					{
						iIndex = rR;
						return iIndex;
						break;
					}
				}
			}
			return iIndex;
		}

		

		private string ExelConvertToLetter(long iCol)
		{
			long a = 0;
			long b = 0;
			a = iCol;
			string ConvertToLetter = string.Empty;
			while (iCol > 0)
			{
				a = Convert.ToInt32(Math.Truncate(Convert.ToDecimal((iCol - 1) / 26)));

				b = (iCol - 1) % 26;

				ConvertToLetter += (char)(b + 65);
				iCol = a;
			}
			return ConvertToLetter;
		}




	}
}
