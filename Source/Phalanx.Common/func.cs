using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace Phalanx.Common
{
    public static class func
    {
        public static GridHeaderLayoutGroup readXMLGridHeader(String XMLname)
        {

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(GridHeaderLayoutGroup));
            System.IO.FileStream fs = new System.IO.FileStream("..\\..\\GridLayout\\" + XMLname + ".xml", System.IO.FileMode.Open);
            GridHeaderLayoutGroup ret = serializer.Deserialize(fs) as GridHeaderLayoutGroup;
            fs.Close();
            return ret;

        }

        public static void SerializeXMLGridHeader(GridHeaderLayoutGroup ee)
        {

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(GridHeaderLayoutGroup));
            System.IO.FileStream fs = new System.IO.FileStream("serializou", System.IO.FileMode.CreateNew);
            serializer.Serialize(fs,ee);

        }


        public static void Mens(String Mensagem, String Titulo = "Aviso")
        {
            MessageBox.Show(Mensagem, Titulo);
        }


        public static bool SimOuNao(String Mensagem, String Titulo = "Aviso") {
            return MessageBox.Show(Mensagem, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public static string PutFile(string Extensions, string Title = "Save file")
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = Extensions; // "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = Title; //"Save an Image File";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.ShowDialog();
            return saveFileDialog1.FileName;
        }

        public static GridHeaderLayout GetDescColumnNameByXMLFile(string ColumnName, string XMLFile)
        {
            GridHeaderLayout ret = null;

            if (XMLFile == "")
                return new GridHeaderLayout { descricao=ColumnName, nome=ColumnName, tamanho=100};


            GridHeaderLayoutGroup group = readXMLGridHeader(XMLFile);
            foreach (GridHeaderLayout item in group.headers)
            {
                if (item.nome.ToUpper() == ColumnName)
                {
                    ret = item;
                    break;
                }
            }

            return ret;
        }

        public static void DataTableToExcel(System.Data.DataTable Tbl, string ExcelFilePath = null, string headerXmlFile="")
        {
            try
            {
                GridHeaderLayoutGroup headerGroup;
                if (headerXmlFile != "")
                    headerGroup = readXMLGridHeader(headerXmlFile);


                if (Tbl == null || Tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                GridHeaderLayout header;
                // column headings
                for (int i = 0; i < Tbl.Columns.Count; i++)
                {
                    header =  GetDescColumnNameByXMLFile( Tbl.Columns[i].ColumnName, headerXmlFile);

                    if (header != null)
                    {
                        workSheet.Cells[1, (i + 1)] = header.descricao;
                        workSheet.Cells[1, (i + 1)].Interior.Color = System.Drawing.Color.LightGray.ToArgb();

                       // if (headerXmlFile != "")
                         //   workSheet.Cells[1, (i + 1)].ColumnWidth = header.tamanho * 72 / 96; //excel calcula em points nao pixels

                    }
                }
                workSheet.Columns.AutoFit();

                // rows
                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {
                        header = GetDescColumnNameByXMLFile(Tbl.Columns[j].ColumnName, headerXmlFile);

                        if (header != null)
                            workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                    }
                }

                // check fielpath
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        workSheet.SaveAs(ExcelFilePath);
                        excelApp.Quit();
                        //MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                            + ex.Message);
                    }
                }
                else    // no filepath is given
                {
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }



        public static void DataTableToPdf(DataTable dt, string path, string headerXmlFile = "")
        {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable;
            GridHeaderLayoutGroup headerGroup;


            if (headerXmlFile == "")
                pdfTable = new PdfPTable(dt.Columns.Count);
            else
            {
                headerGroup = readXMLGridHeader(headerXmlFile);
                pdfTable = new PdfPTable(headerGroup.headers.Count);

                float[] Widths = new float[headerGroup.headers.Count];

                for (int i = 0; i < headerGroup.headers.Count; i++)
                {
                    Widths[i] = (float)headerGroup.headers[i].tamanho;
                }
                pdfTable.SetWidths(Widths);
            }

            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
           


            GridHeaderLayout header;
            //Adding Header row
            foreach (DataColumn column in dt.Columns)
            {
                header = GetDescColumnNameByXMLFile(column.ColumnName, headerXmlFile);

                if (header != null)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header.descricao));
                    cell.BackgroundColor = new BaseColor(240, 240, 240);
                   
                    pdfTable.AddCell(cell);
                }
            }

            //Adding DataRow
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn cell in dt.Columns)
                {
                    header = GetDescColumnNameByXMLFile(cell.ColumnName, headerXmlFile);

                    if (header!=null)
                        pdfTable.AddCell(row[cell.ColumnName].ToString());
                }
            }

            //Exporting to PDF
         
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }

        }


        public static void DataTableToCsv(DataTable dt, string path, string headerXmlFile = "")
        {
            GridHeaderLayout header;
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                header = GetDescColumnNameByXMLFile(dt.Columns[k].ColumnName, headerXmlFile);

                //add separator
                if (header!=null)
                    sb.Append(header.descricao+ ';');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    header = GetDescColumnNameByXMLFile(dt.Columns[k].ColumnName, headerXmlFile);

                    //add separator
                    if (header!=null)
                    sb.Append(dt.Rows[i][k].ToString().Replace(";", ",") + ';');
                }
                //append new line
                sb.Append("\r\n");

            }

            using (StreamWriter outfile = new StreamWriter(path,true))
            {
                outfile.Write(sb.ToString());
            }
          

         }

    }



}
