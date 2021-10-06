using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodingExercise.Helpers
{
    public static class Export
    {
        public static void ExportToPdf(DataTable dt, string filePath)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();
            iTextSharp.text.Font headerFontStyle = FontFactory.GetFont(FontFactory.HELVETICA, 5, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font bodyFontStyle = FontFactory.GetFont(FontFactory.HELVETICA, 5);

            PdfPTable table = new PdfPTable(dt.Columns.Count);
            float[] widths = new float[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
                widths[i] = 1f;

            table.SetWidths(widths);

            table.WidthPercentage = 90;

            PdfPCell cell = new PdfPCell();
            cell.Colspan = dt.Columns.Count;

            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, headerFontStyle));
            }

            foreach (DataRow row in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        table.AddCell(new Phrase(row[i].ToString(), bodyFontStyle));
                    }
                }
            }
            document.Add(table);
            document.Close();
        }

        public static void ExportToExcel(DataSet ds, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage objExcelPackage = new ExcelPackage())
            {
                foreach (DataTable dtSrc in ds.Tables)
                {
                    //Create the worksheet    
                    ExcelWorksheet objWorksheet = objExcelPackage.Workbook.Worksheets.Add(dtSrc.TableName);
                    //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1    
                    objWorksheet.Cells["A1"].LoadFromDataTable(dtSrc, true);
                    objWorksheet.Cells.Style.Font.SetFromFont(new System.Drawing.Font("Calibri", 10));
                    objWorksheet.Cells.AutoFitColumns();
                    //Format the header    
                    using (ExcelRange objRange = objWorksheet.Cells["A1:XFD1"])
                    {
                        objRange.Style.Font.Bold = true;
                        objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        objRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        objRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 0, 100));
                        objRange.Style.Font.Color.SetColor(Color.White);
                    }
                }

                //Write it back to the client    
                if (File.Exists(filePath))
                    File.Delete(filePath);

                //Create excel file on physical disk    
                FileStream objFileStrm = File.Create(filePath);
                objFileStrm.Close();

                //Write content to excel file    
                File.WriteAllBytes(filePath, objExcelPackage.GetAsByteArray());
            }
        } //ExportExcel

        public static DataTable PopulateDataTable<T>(IEnumerable<T> data) where T : class
        {
            Type objType = typeof(T);
            PropertyInfo[] propertyInfos = objType.GetProperties();

            var props = new List<string>();

            var includedColumns = propertyInfos.Where(p => p.CustomAttributes.All(ca => ca.AttributeType.Name != "ExcludeFromExportAttribute"));

            DataTable dataTable = new DataTable("Table");

            foreach (var item in includedColumns)
            {
                DataColumn dc = new DataColumn(item.Name, item.PropertyType); 
                dataTable.Columns.Add(dc);
            }

            List<string> list = new List<string>();

            foreach (var row in data)
            {
                Type rt = row.GetType();
                var rtPropinfos = rt.GetProperties();

                list = new List<string>();
                foreach (var item in rtPropinfos)
                {
                    if (includedColumns.Select(p => p.Name).Contains(item.Name))
                    {
                        list.Add(item.GetValue(row).ToString());
                    }
                }

                dataTable.Rows.Add(list.ToArray());
            }

            return dataTable;
        }
    }
}